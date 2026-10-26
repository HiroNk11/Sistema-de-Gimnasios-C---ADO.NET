using CapaAplicacion.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU.Negocio
{
    public partial class FrmVerDetalle : Form
    {
        ClaseBLL gestorc = new ClaseBLL();
        public FrmVerDetalle()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            cmbclase.DataSource = gestorc.Listar_Clase();
            GrillaClase.DataSource = null;
            GrillaClase.DataSource = gestorc.GetClaseALLDescription();
        }
       
        private void btnfiltrar_Click(object sender, EventArgs e)
        {
            var c = gestorc.ListarClase();
            foreach (var clase in c)
            {
                if (clase.Descripcion == cmbclase.Text)
                {
                    var i = gestorc.GetClaseDescription(cmbclase.Text);
                    GrillaClase.DataSource = null;
                    GrillaClase.DataSource = i;
                }
            }
        }
    }
}
