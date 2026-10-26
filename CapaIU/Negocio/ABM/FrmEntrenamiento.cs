using CapaAplicacion;
using CapaDominio;
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
    public partial class FrmEntrenamiento : Form
    {
        Entrenamiento tmp;
        EntrenamientoBLL gestorEntrenamiento = new EntrenamientoBLL();
        public FrmEntrenamiento()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            ListarEntrenamiento();
            Limpiar();
        }
        public void ListarEntrenamiento()
        {
            GrillaEntrenamiento.DataSource = null;
            GrillaEntrenamiento.DataSource = gestorEntrenamiento.Listar();
        }

        private void FrmEntrenamiento_Load(object sender, EventArgs e)
        {
            ListarEntrenamiento();
        }

        private void btnagregar_Click(object sender, EventArgs e)
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
                tmp = new Entrenamiento();
                tmp.Nombre = txtNombre.Texto;
                gestorEntrenamiento.Insertar(tmp);
                tmp = null;
                MessageBox.Show("Entrenamiento Agregado con exito");
                ListarEntrenamiento();
                Limpiar();
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (lblidentrenamiento.Text != "")
            {
                tmp = new Entrenamiento();
                tmp.Id_Entrenamiento = int.Parse(lblidentrenamiento.Text);
                MessageBox.Show(gestorEntrenamiento.Borrar(tmp));
                ListarEntrenamiento();
                Limpiar();
            }

            else
            {
                MessageBox.Show("Seleccione el usuario");
            }
        }

        public void Limpiar ()
            {
            txtNombre.Texto = "";
            txtbuscar.Text = "";
            lblidentrenamiento.Text = "";
            }

        private void GrillaEntrenamiento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrillaEntrenamiento.CurrentCell != null)
                {
                    int fila;
                    fila = GrillaEntrenamiento.CurrentCell.RowIndex;
                    lblidentrenamiento.Text = GrillaEntrenamiento[0, fila].Value.ToString();
                    txtNombre.Texto = GrillaEntrenamiento[1, fila].Value.ToString();

                }
                else
                {
                    MessageBox.Show("No existen Socios");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un usuario, Por Favor");
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
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
                tmp = new Entrenamiento();
                tmp.Nombre = txtNombre.Texto;
                tmp.Id_Entrenamiento = int.Parse(lblidentrenamiento.Text);
                gestorEntrenamiento.Editar(tmp);
                MessageBox.Show("Se han actualizado los datos correctamente");
                ListarEntrenamiento();
                Limpiar();
            }


            if (!ok)
            {
                MessageBox.Show("Revise los campos", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar.Text != "")
            {
                tmp = new Entrenamiento();
                tmp.Nombre = txtbuscar.Text;
                DataTable dt = new DataTable();
                dt = gestorEntrenamiento.Buscar_Entrenamiento(tmp);
                tmp = null;
                GrillaEntrenamiento.DataSource = dt;
            }
            else
            {
                ListarEntrenamiento();
            }
        }
    }
}
