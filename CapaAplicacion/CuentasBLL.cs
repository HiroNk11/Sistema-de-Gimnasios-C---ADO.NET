using System;
using System.Net.Mail;
using CapaDominio;
using CapaRepositorio;
using CapaServicios;
using CapaServicios.Composite;
namespace CapaAplicacion
{
    public sealed class CuentasBLL
    {
        public static void Validar(Usuario u,PerfilCuenta perfil,string clave,string confirmacion)
        {
            if(u==null || string.IsNullOrWhiteSpace(u.usuario) || string.IsNullOrWhiteSpace(u.nombre) || string.IsNullOrWhiteSpace(u.apellido))throw new ArgumentException("Completá usuario, nombre y apellido.");
            u.usuario=u.usuario.Trim();u.nombre=u.nombre.Trim();u.apellido=u.apellido.Trim();u.email=(u.email??"").Trim();
            if(u.usuario.Length>50 || u.nombre.Length>50 || u.apellido.Length>50 || u.email.Length>254)throw new ArgumentException("Revisá la longitud de los datos.");
            try{var correo=new MailAddress(u.email);if(correo.Address!=u.email)throw new FormatException();}catch(Exception){throw new ArgumentException("Ingresá un correo válido.");}
            CuentasRepository.NombrePerfil(perfil);
            Encriptador.ValidarPassword(clave);
            if(clave!=confirmacion)throw new ArgumentException("Las contraseñas no coinciden.");
        }
        public void Crear(Usuario usuario,PerfilCuenta perfil,string clave,string confirmacion)
        {
            Autorizacion.ExigirCualquiera(TipoPermiso.PuedeCrearUsuarios,TipoPermiso.PuedeGestionarUsuarios);
            Validar(usuario,perfil,clave,confirmacion);usuario.password=Encriptador.Hash(clave);
            try{new CuentasRepository().Crear(usuario,perfil,SingletonSesion.Instancia.Usuario.idusuario);}finally{usuario.password=null;}
        }
    }
}
