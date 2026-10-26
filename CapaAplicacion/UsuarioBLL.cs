using CapaDominio;
using CapaRepositorio;
using CapaServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaServicios.Composite;
using CapaInterfaces.INTERFACES;
using CapaServicios.DV;

namespace CapaAplicacion
{
    public class UsuarioBLL 
    {
        UsuarioRepository mapperrepo;
        Usuario usu;
        PermisoUsuario u2;
        PermisosRepository repo;
        IdiomaRepository trepo;

        public UsuarioBLL()
        {
            mapperrepo = new UsuarioRepository();
            usu = new Usuario();
            u2 = new PermisoUsuario();
            repo = new PermisosRepository();
            trepo = new IdiomaRepository();
        }
        public string Login(string usu, string pass)
        {
            if (SingletonSesion.Instancia.IsLogged()) throw new InvalidOperationException("Ya hay una sesión iniciada.");
            if (string.IsNullOrWhiteSpace(usu) || string.IsNullOrEmpty(pass)) return "Ingrese usuario y contraseña.";
            IntentosLogin.Validar(usu);
            var u = mapperrepo.GetByUserName(usu.Trim());
            string hash = u == null ? null : new CredencialRepository().Obtener(u.idusuario);
            if (!Encriptador.Verify(pass, hash))
            {
                IntentosLogin.Fallar(usu);
                return "Usuario o contraseña incorrectos. Revisá los datos e intentá nuevamente.";
            }
            IntentosLogin.Limpiar(usu);
            SingletonSesion.Instancia.LogIn(u);
            return "Bienvenido: " + u.usuario;
        }
        public bool RequiereCambiarClave(){if(!SingletonSesion.Instancia.IsLogged())throw new UnauthorizedAccessException("Inicie sesión.");return new CredencialRepository().DebeCambiarClave(SingletonSesion.Instancia.Usuario.idusuario);}
        public void Logout()
        {
            if (!SingletonSesion.Instancia.IsLogged())
                throw new Exception("No hay sesión iniciada"); //doble validación, anulo en boton en formulario y valido en la bll
            SingletonSesion.Instancia.Logout();
        }
        static void ValidarDatos(Usuario usu)
        {
            if(usu==null || string.IsNullOrWhiteSpace(usu.usuario) || string.IsNullOrWhiteSpace(usu.nombre) || string.IsNullOrWhiteSpace(usu.apellido))throw new ArgumentException("Complete usuario, nombre y apellido.");
            if(usu.usuario.Length>50 || usu.nombre.Length>50 || usu.apellido.Length>50 || usu.email==null || usu.email.Length>254)throw new ArgumentException("Usuario, nombre y apellido admiten hasta 50 caracteres; correo hasta 254.");
            try {new System.Net.Mail.MailAddress(usu.email);}catch(Exception){throw new ArgumentException("Ingrese un correo válido.");}
        }
        public string Editar(Usuario usu)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeGestionarUsuarios);

            ValidarDatos(usu);
            return mapperrepo.Editar(usu);
        }
        public List<Usuario> Listar()
        {
            Autorizacion.ExigirCualquiera(TipoPermiso.PuedeGestionarUsuarios,TipoPermiso.PuedeConfigurarSeguridadUsuarios);
            List<Usuario> listausu = mapperrepo.Listar();
            return listausu;
        }
        public string Insertar(Usuario usu)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeGestionarUsuarios);

            ValidarDatos(usu);
            return mapperrepo.Insertar(usu);
        }
        public string Borrar(Usuario usu)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.DarBajaUsuario);

            if(usu.idusuario==SingletonSesion.Instancia.Usuario.idusuario)throw new InvalidOperationException("No puede eliminar su propia cuenta.");
            return mapperrepo.Borrar(usu);
        }
        public void GuardarPermisos(PermisoUsuario u)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeConfigurarSeguridadUsuarios);

            mapperrepo.GuardarPermisos(u);
        }

        public string BorrarPermisos(Usuario usu)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeConfigurarSeguridadUsuarios);

            return mapperrepo.BorrarPermisos(usu);
        }
        public DataTable Buscar_User(Usuario u)
        {
            return mapperrepo.Buscar_User(u);
        }
        public List<PermisoUsuario> GetAll()
        {
            return mapperrepo.GetAll();
        }
        public string recoverPasswrod(string userRequesting)
        {
            if (string.IsNullOrWhiteSpace(userRequesting)) throw new ArgumentException("Ingrese usuario o correo.");
            IntentosLogin.Validar("recuperacion:" + userRequesting);
            IntentosLogin.Fallar("recuperacion:" + userRequesting);
            return new CredencialRepository().Solicitar(userRequesting.Trim());
        }
        public string ChangePassword(string usu, string p)
        {
            if (!SingletonSesion.Instancia.IsLogged() || SingletonSesion.Instancia.Usuario.usuario != usu)
                throw new UnauthorizedAccessException("Debe iniciar sesión con la cuenta que desea modificar.");
            return Encriptador.Verify(p,new CredencialRepository().Obtener(SingletonSesion.Instancia.Usuario.idusuario)) ? "ok" : "La contraseña actual no es correcta.";
        }
        public string UpdatePassword(string passn, string usu, string passc)
        {
            throw new UnauthorizedAccessException("Utilice el cambio con contraseña actual o la recuperación con código.");
        }
        public string CambiarPassword(string actual, string nueva, string confirmar)
        {
            string usuario = SingletonSesion.Instancia.IsLogged() ? SingletonSesion.Instancia.Usuario.usuario : "";
            if (ChangePassword(usuario, actual) != "ok") throw new ArgumentException("La contraseña actual no es correcta.");
            if (nueva != confirmar) throw new ArgumentException("Las contraseñas no coinciden.");
            if(nueva==actual)throw new ArgumentException("Elegí una clave diferente de la inicial o anterior.");
            new CredencialRepository().Cambiar(usuario, Encriptador.Hash(nueva));
            return "Contraseña cambiada exitosamente.";
        }
        public string RestablecerPassword(string solicitud, string codigo, string nueva, string confirmar)
        {
            if (nueva != confirmar) throw new ArgumentException("Las contraseñas no coinciden.");
            new CredencialRepository().Restablecer(solicitud, codigo, Encriptador.Hash(nueva));
            return "Contraseña restablecida. Ya puede iniciar sesión.";
        }

        public string GetUserName(string username)
        {
            return mapperrepo.GetUserName(username);
        }

        public string GetEmail(string email)
        {
            return mapperrepo.GetEmail(email);
        }

        public string GetUserNameByUserOrEmail(string userRequesting)
        {
            return mapperrepo.GetUserNameByUserOrEmail(userRequesting);
        }
        public string recoverUser(string userRequesting)
        {
            return mapperrepo.recoverUser(userRequesting);
        }

        public List<int> VerificarDigitosPorId() { return mapperrepo.VerificarDigitosPorId(); }
        public void RepararDigitos()
        {
            Autorizacion.Exigir(TipoPermiso.PuedeRestaurardv);
            mapperrepo.RepararDigitos();
        }
        public List<object> ObtenerDVHUsuario()
        {
            List<object> lista = mapperrepo.DVHUsuario();
            return lista;
        }

        public string ObtenerDVVUsuario()
        {
            return mapperrepo.DVVUsuario();
        }

        public string ActualizarDVHUsuario(string dvh, int id)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeRestaurardv);
       
            return mapperrepo.ActualizarDVH(dvh,id);
        }

        public string ActualizarDVVUsuario()
        {
            return mapperrepo.ActualizarDVV();
        }

        public List<UsuDv> GetAllCamposDV()
        {
            List<UsuDv> listacampos = mapperrepo.GetAllCamposDV();
            return listacampos;
        }
        public string ActualizarDigitos()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeRestaurardv);

            return mapperrepo.ActualizarDigitos();
        }

        public List<object> GenerarDVH()
        {
            List<UsuDv> listausuario = new List<UsuDv>();
            List<object> lstdigitos = new List<object>();
            listausuario = mapperrepo.GetAllCamposDV();
            UsuDv usu = new UsuDv();
            string dvhusu = string.Empty;
            string dvvusu = string.Empty;
            foreach (var recorrer in listausuario)
            {
                usu.usuario = recorrer.usuario;
                usu.contraseña = recorrer.contraseña;
                usu.nombre = recorrer.nombre;
                usu.apellido = recorrer.apellido;
                usu.email = recorrer.email;
                dvhusu = DV.GenerarDVH(usu);
                lstdigitos.Add(dvhusu);
                dvvusu += dvhusu;
            }
            //int cont = 0;
            //foreach (var item in lstdigitos)
            //{
            //    cont++;
            //    ActualizarDVHUsuario(item.ToString(), cont);

            //}
            return lstdigitos;
        }
    }
}