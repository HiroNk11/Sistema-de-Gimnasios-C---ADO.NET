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
using CapaDominio;

namespace CapaIU.Negocio
{
    public partial class FrmHistorialCambios : Form
    {
        SocioBLL gestorsocio = new SocioBLL();
        Socio tmp;
        public FrmHistorialCambios()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            grillacambios.DataSource = gestorsocio.Listar_HistorialCambios();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grillacambios.CurrentCell != null)
                {
                    int fila;
                    fila = grillacambios.CurrentCell.RowIndex;
                    lblidsocio.Text = grillacambios[1, fila].Value.ToString();
                    txtnombre.Texto = grillacambios[2, fila].Value.ToString();
                    txtapellido.Texto = grillacambios[3, fila].Value.ToString();
                    txtfn.Text = grillacambios[4, fila].Value.ToString();
                }
                else
                {
                    MessageBox.Show("No existen Cambios");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un dato en la tabla, Por Favor");
            }
        }

        private void btnrestaurar_Click(object sender, EventArgs e)
        {
            tmp = new Socio();
            tmp.Id_Socio = int.Parse(lblidsocio.Text);
            tmp.Nombre = txtnombre.Texto;
            tmp.Apellido = txtapellido.Texto;
            tmp.Fecha = Convert.ToDateTime(txtfn.Text);
            MessageBox.Show(gestorsocio.Editar(tmp));
            tmp = null;
        }

        private void btnborar_Click(object sender, EventArgs e)
        {
            gestorsocio.LimpiarHistorial();
            grillacambios.DataSource = gestorsocio.Listar_HistorialCambios();
        }
    }
}
