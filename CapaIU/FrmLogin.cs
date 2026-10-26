using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaAplicacion;
using CapaDominio;
using CapaIU;
using CapaServicios;
using CapaServicios.Composite;

namespace LogIn
{
    public partial class FrmLogin : Form
    {
  
        Usuario usu;
        UsuarioBLL _usuariobll;
        BitacoraBLL bitbll;
        Bitacora bit;
        public FrmLogin() : this(false) {}
        internal FrmLogin(bool vistaPrevia)
        {
            InitializeComponent();
            _usuariobll = new UsuarioBLL();
            usu = new Usuario();
            bitbll = new BitacoraBLL();
            // La base se consulta al ingresar, no antes de mostrar el formulario.
    
        }

        public void VerificarDigitos()
        {
            var ids=_usuariobll.VerificarDigitosPorId();
            if(ids.Count>0)MessageBox.Show("Hay diferencias en los verificadores de usuarios. IDs: "+string.Join(", ",ids)+". Solicite revisión al administrador.","Verificación de integridad",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void RealeaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
   
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            RealeaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            RealeaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private async void btnacceder_Click(object sender, EventArgs e)
        {
            lblerrormessage.Visible=false;
            if(string.IsNullOrWhiteSpace(txtusuario.Text) || string.IsNullOrEmpty(txtcontraseña.Text))
            {msgError("Completá usuario y contraseña.");if(string.IsNullOrWhiteSpace(txtusuario.Text))txtusuario.Focus();else txtcontraseña.Focus();return;}
            btnacceder.Enabled=false;btnacceder.Text="Ingresando…";txtusuario.Enabled=false;txtcontraseña.Enabled=false;
            try
            {
                string usuario=txtusuario.Text.Trim(),clave=txtcontraseña.Text;
                string resultado=await Task.Run(()=>_usuariobll.Login(usuario,clave));
                if(SingletonSesion.Instancia.IsLogged())
                {
                    if(await Task.Run(()=>_usuariobll.RequiereCambiarClave())){
                        using(var cambio=new FrmActivarClave())if(cambio.ShowDialog(this)!=DialogResult.OK){_usuariobll.Logout();msgError("Debés cambiar la clave inicial para continuar.");return;}
                        if(await Task.Run(()=>_usuariobll.RequiereCambiarClave())){_usuariobll.Logout();msgError("La clave inicial aún debe cambiarse.");return;}
                    }
                    VerificarDigitos();GrabarBitacora("Inicio de sesión de: "+SingletonSesion.Instancia.Usuario.usuario);
                    var frm=new FrmMdiPrincipal();frm.Show();Hide();txtcontraseña.Clear();
                }
                else {msgError(resultado);txtcontraseña.SelectAll();}
            }
            catch(AccesoTemporalmenteBloqueadoException ex){msgError(ex.Message);}
            catch(Exception ex){if(SingletonSesion.Instancia.IsLogged())_usuariobll.Logout();msgError("No se pudo iniciar sesión. Revisá el aviso de conexión.");CapaIU.Program.Error(ex);}
            finally{btnacceder.Enabled=true;btnacceder.Text="Ingresar";txtusuario.Enabled=true;txtcontraseña.Enabled=true;}
        }

        public void GrabarBitacora(string detallebit)
        {
            bit = new Bitacora();
            bit.id_usuario = SingletonSesion.Instancia.Usuario.idusuario;
            bit.actividad = detallebit;
            bit.criticidad = "Media";
            bit.fecha = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            bit.hora =  DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            bitbll.InsertarEvento(bit);
            bit = null;
        }
        private void msgError(string mg)
        {
            lblerrormessage.Text = mg;
            lblerrormessage.Visible = true;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtusuario_Leave(object sender, EventArgs e)
        {
            if (txtusuario.Text == "")
            {
                txtusuario.Text = "USUARIO";
                txtusuario.ForeColor = Color.DimGray;
            }
        }

        private void txtcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "CONTRASEÑA")
            {
                txtcontraseña.Text = "";
                txtcontraseña.ForeColor = Color.LightGray;
                txtcontraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtusuario_Enter(object sender, EventArgs e)
        {
            if (txtusuario.Text == "USUARIO")
            {
                txtusuario.Text = "";
                txtusuario.ForeColor = Color.LightGray;
            }
        }

        private void txtcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "")
            {
                txtcontraseña.Text = "CONTRASEÑA";
                txtcontraseña.ForeColor = Color.DimGray;
                txtcontraseña.UseSystemPasswordChar = false;
            }
        }

        private void linkpass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRecuperarContraseña rc = new FrmRecuperarContraseña();
            rc.ShowDialog();
        }

        private void linkusu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRecuperarUsuario ru = new FrmRecuperarUsuario();
            ru.ShowDialog();
        }
    }
}
