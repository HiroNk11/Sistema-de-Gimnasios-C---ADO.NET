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
    public partial class FrmBitacora : Form
    {
        BitacoraBLL gestorbitacora;
        UsuarioBLL _usuariobll;
        public FrmBitacora()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            gestorbitacora = new BitacoraBLL();
            _usuariobll = new UsuarioBLL();
            ListarBitacora();
            cmbusuario2.DataSource = _usuariobll.GetAll();
        }
        public void ListarBitacora()
        {
            grillabitacora.DataSource = null;
            grillabitacora.DataSource = gestorbitacora.Listar_Bitacora();
        }

        private void cmbusuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbusuario2.Text != "")
            {
                var bit = gestorbitacora.GetBitacoraUser(cmbusuario2.Text);
                grillabitacora.DataSource = null;
                grillabitacora.DataSource = bit;
            }          
        }
        private void btnfiltrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                var bit = gestorbitacora.GetBitacoraUserByFilter(cmbusuario2.Text, dateTimePicker2.Text, dateTimePicker3.Text,cmbcriticidad.Text);
                grillabitacora.DataSource = null;
                grillabitacora.DataSource = bit;
            }
            catch
            {
                MessageBox.Show("Por favor especifique cada uno de los detalles");
            }
        
        }

        private void cmbusuario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbusuario2.Text != "")
            {
                var bit = gestorbitacora.GetBitacoraUser(cmbusuario2.Text);
                grillabitacora.DataSource = null;
                grillabitacora.DataSource = bit;

            }
        }

        private void cmbcriticidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbcriticidad.Text != "")
            {
                var bit = gestorbitacora.FiltrarCriticidad(cmbcriticidad.Text);
                grillabitacora.DataSource = null;
                grillabitacora.DataSource = bit;
            }
        }
    }
}
