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
    public partial class FrmRecuperarUsuario : Form
    {
        UsuarioBLL gestorusu = new UsuarioBLL();
        public FrmRecuperarUsuario()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
        }

        private void btnenviar_Click(object sender, EventArgs e)
        {
            lblmensaje.Text = gestorusu.recoverUser(txtcorreo.Text);
            if(lblmensaje.Text != "Disculpe, este email no está registrado en el sistema")
            {
                txtcorreo.Enabled = false;
                btnenviar.Enabled = false;
                txtcorreo.Text = "";
            }      
            
        }
    }
}
