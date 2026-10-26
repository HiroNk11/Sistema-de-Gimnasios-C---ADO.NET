using CapaAplicacion;
using CapaDominio;
using CapaServicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using CapaServicios.Composite;
using CapaInterfaces.INTERFACES;
using CapaServicios.DV;

namespace CapaIU
{
    public partial class FrmGestiónUsuarios : Form, IIdiomaObserver
    {
        UsuarioBLL gestorusu;
        Usuario tmp;
        Sesion usu = new Sesion();
        PermisoUsuario pu;
        PermisosBLL permisosbll;
        TraductorBLL tradu;
        Bitacora bit;
        BitacoraBLL bitbll = new BitacoraBLL();
        public FrmGestiónUsuarios()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            gestorusu = new UsuarioBLL();
            permisosbll = new PermisosBLL();
            tradu = new TraductorBLL();
            ValidarPermisos();
            if (SingletonSesion.Instancia.IsLogged())
                Traducir(SingletonSesion.Instancia.Usuario.Idioma);
            else
                Traducir(); //trae el idioma por default

        }

        private void Traducir(IIdioma idioma = null)
        {
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

            if (txtnombreusuario.Tag != null && traducciones.ContainsKey(txtnombreusuario.Tag.ToString()))
                txtnombreusuario.Etiqueta = traducciones[txtnombreusuario.Tag.ToString()].Texto;

            if (txtapellido.Tag != null && traducciones.ContainsKey(txtapellido.Tag.ToString()))
                txtapellido.Etiqueta = traducciones[txtapellido.Tag.ToString()].Texto;

            if (txtnombre.Tag != null && traducciones.ContainsKey(txtnombre.Tag.ToString()))
                txtnombre.Etiqueta = traducciones[txtnombre.Tag.ToString()].Texto;

            if (txtcorreo.Tag != null && traducciones.ContainsKey(txtcorreo.Tag.ToString()))
                txtcorreo.Etiqueta = traducciones[txtcorreo.Tag.ToString()].Texto;

            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;


            if (lbltextid.Tag != null && traducciones.ContainsKey(lbltextid.Tag.ToString()))
                lbltextid.Text = traducciones[lbltextid.Tag.ToString()].Texto;

            if (btnAlta.Tag != null && traducciones.ContainsKey(btnAlta.Tag.ToString()))
                btnAlta.Text = traducciones[btnAlta.Tag.ToString()].Texto;

            if (btnBaja.Tag != null && traducciones.ContainsKey(btnBaja.Tag.ToString()))
                btnBaja.Text = traducciones[btnBaja.Tag.ToString()].Texto;

            if (btnModificar.Tag != null && traducciones.ContainsKey(btnModificar.Tag.ToString()))
                btnModificar.Text = traducciones[btnModificar.Tag.ToString()].Texto;

        }

            public void Enlazar()
        {
            GrillaUsuarios.DataSource=null;
            var tabla=new DataTable();tabla.Columns.Add("Id_Usuario",typeof(int));tabla.Columns.Add("LoginName");tabla.Columns.Add("FirstName");tabla.Columns.Add("LastName");tabla.Columns.Add("Email");
            foreach(var u in gestorusu.Listar())tabla.Rows.Add(u.idusuario,u.usuario,u.nombre,u.apellido,u.email);
            GrillaUsuarios.DataSource=tabla;
        }

        private void FrmGestiónUsuarios_Load(object sender, EventArgs e)
        {
            Enlazar();
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            bool ok = true;
            foreach (Control ctr in this.Controls)
            {
                if (ctr is MiControl)
                {
                    ok = ((MiControl)ctr).Validar() && ok;
                }
            }
            if (ok )
            {
                if(txtnombreusuario.Texto != gestorusu.GetUserName(txtnombreusuario.Texto) && txtcorreo.Texto != gestorusu.GetEmail(txtcorreo.Texto))
                {
                    tmp = new Usuario();
                    tmp.usuario = txtnombreusuario.Texto;
                    var pass = GenerarContraseñaAutomatica();
                    tmp.password = Encriptador.Hash(pass);
                    tmp.nombre = txtnombre.Texto;
                    tmp.apellido = txtapellido.Texto;
                    tmp.email = txtcorreo.Texto;
                    tmp.dvh = "";
                    MessageBox.Show(gestorusu.Insertar(tmp));
                    try { gestorusu.recoverPasswrod(tmp.usuario); }
                    catch(Exception ex) { Program.Error(ex); MessageBox.Show("El usuario fue creado. Configure el correo y utilice Recuperar contraseña para activar el acceso."); }
                    Actualizaddv();
                    string bit = "Alta de usuario realizado por : " + SingletonSesion.Instancia.Usuario.usuario;
                    GrabarBitacora(bit);
                    Enlazar();
                    Limpiar();
                }
                if (txtnombre.Texto == gestorusu.GetUserName(txtnombreusuario.Texto)) 
                {
                    MessageBox.Show("Este usuario ya esta registrado");
                }
                if(txtcorreo.Texto == gestorusu.GetEmail(txtcorreo.Texto))
                {
                    MessageBox.Show("Este email ya esta registrado");
                }
                                           
            }
            if (!ok)
            {             
                MessageBox.Show("Verifique los campos, Por favor");
            }           
        }

        public void GrabarBitacora(string detallebit)
        {
            bit = new Bitacora();
            bit.id_usuario = SingletonSesion.Instancia.Usuario.idusuario;
            bit.actividad = detallebit;
            bit.criticidad = "Baja";
            bit.fecha = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            bit.hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            bitbll.InsertarEvento(bit);
            bit = null;
        }
        public void Actualizaddv() { /* Los cambios de usuario actualizan sus DV dentro de la transacción. */ }
        public string GenerarContraseñaAutomatica() { return Encriptador.Token(); }

        public void EnviarCorreo(string user,string pass,string email)
        { gestorusu.recoverPasswrod(user); }

        public void Limpiar()
        {          
            txtapellido.Texto = "";
            txtnombreusuario.Texto = "";
            txtcorreo.Texto = "";
            txtnombre.Texto = "";
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {         
                bool ok = true;
                foreach (Control ctr in this.Controls)
                {
                    if (ctr is MiControl)
                    {
                        ok = ((MiControl)ctr).Validar() && ok;
                    }
                }
                if (ok)
                {
                if (SingletonSesion.Instancia.Usuario.usuario == txtnombreusuario.Texto)
                {
                    MessageBox.Show("No puede dar de baja al usuario si Inicio Sesión");
                }
                else
                {               
                    tmp = new Usuario();
                    tmp.idusuario = int.Parse(lblidusuario.Text);
                    // La baja elimina permisos y usuario dentro de la misma transacción.
                    MessageBox.Show(gestorusu.Borrar(tmp));
                    tmp = null;
                    string bit = "Baja de usuario realizado por : " + SingletonSesion.Instancia.Usuario.usuario;
                    GrabarBitacora(bit);
                    Enlazar();
                    Limpiar();                                   
                }
              
                }
                if (!ok)
                {
                    MessageBox.Show("Seleccione un usuario en la lista por favor", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              
        }

        private void GrillaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex<0 || GrillaUsuarios.CurrentRow==null)return;
            var row=GrillaUsuarios.CurrentRow;
            lblidusuario.Text=Convert.ToString(row.Cells["Id_Usuario"].Value);
            txtnombreusuario.Texto=Convert.ToString(row.Cells["LoginName"].Value);
            txtnombre.Texto=Convert.ToString(row.Cells["FirstName"].Value);
            txtapellido.Texto=Convert.ToString(row.Cells["LastName"].Value);
            txtcorreo.Texto=Convert.ToString(row.Cells["Email"].Value);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id;
            if(!int.TryParse(lblidusuario.Text,out id))throw new ArgumentException("Seleccione un usuario en la lista.");
            var usuario=new Usuario {idusuario=id,usuario=txtnombreusuario.Texto,nombre=txtnombre.Texto,apellido=txtapellido.Texto,email=txtcorreo.Texto};
            MessageBox.Show(gestorusu.Editar(usuario));
            Enlazar();Limpiar();
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar.Text != "")
            {
                tmp = new Usuario();
                tmp.usuario = txtbuscar.Text;
                DataTable dt = new DataTable();
                dt = gestorusu.Buscar_User(tmp);
                tmp = null;
                GrillaUsuarios.DataSource = dt;
            }
            else
            {
                Enlazar();
            }
        }

        public void ValidarPermisos()
        {
            var lista = gestorusu.Listar();
            foreach (var i in lista)
            {
                if (SingletonSesion.Instancia.Usuario.usuario == i.usuario)
                {
                    pu = new PermisoUsuario();
                    pu.idusuario = i.idusuario;
                    pu.Nombre = i.usuario;
                    btnBaja.Visible = permisosbll.BuscarPermisos(TipoPermiso.DarBajaUsuario,pu);
                }
            }
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void FrmGestiónUsuarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }
    }
}
