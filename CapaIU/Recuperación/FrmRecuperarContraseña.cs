using CapaAplicacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU
{
    public partial class FrmRecuperarContraseña : Form
    {
        UsuarioBLL tmpuser = new UsuarioBLL();
        public FrmRecuperarContraseña()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
         
        }

        private void btnenviar_Click(object sender, EventArgs e)
        {
            lblmensaje.Text=tmpuser.recoverPasswrod(txtcorreo.Text);
            lblcodigo.Visible=txtcodigo.Visible=btningresar.Visible=true;
            btnenviar.Enabled=false;txtcorreo.Enabled=false;
        }
     
        private void btningresar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtcodigo.Text)) {MessageBox.Show("Ingrese el código recibido.");return;}
            gboucambiarcontraseña.Visible=true;
            // La validación real se hace en la transacción que consume el código.
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tmpuser.RestablecerPassword(txtcorreo.Text,txtcodigo.Text.Trim(),txtconnueva.Text,txtconfir.Text));
            Close();
        }
    }
    
}
