using CapaAplicacion;
using CapaInterfaces.INTERFACES;
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

namespace CapaIU
{
    public partial class FrmCambiarContraseña : Form, IIdiomaObserver
    {
        UsuarioBLL gestorusu = new UsuarioBLL();
        TraductorBLL tradu;
        public FrmCambiarContraseña()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            tradu = new TraductorBLL();
            if (SingletonSesion.Instancia.IsLogged())
                Traducir(SingletonSesion.Instancia.Usuario.Idioma);
            else
                Traducir(); //trae el idioma por default
        }

        private void Traducir(IIdioma idioma=null)
        {
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

            if (grpcc.Tag != null && traducciones.ContainsKey(grpcc.Tag.ToString()))
                grpcc.Text = traducciones[grpcc.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (btnAceptar.Tag != null && traducciones.ContainsKey(btnAceptar.Tag.ToString()))
                btnAceptar.Text = traducciones[btnAceptar.Tag.ToString()].Texto;

            if (btncancelar.Tag != null && traducciones.ContainsKey(btncancelar.Tag.ToString()))
                btncancelar.Text = traducciones[btncancelar.Tag.ToString()].Texto;
        }
    
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (gestorusu.ChangePassword(SingletonSesion.Instancia.Usuario.ToString(), txtconactual.Text) == "ok")
            {
                MessageBox.Show(gestorusu.CambiarPassword(txtconactual.Text, txtconnueva.Text, txtconfir.Text));
            }
            else
            {
                MessageBox.Show("La contraseña actual no coincide");
            }
            Limpiar();
        }

        public void Limpiar()
        {
            txtconactual.Clear();
            txtconnueva.Clear();
            txtconfir.Clear();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmCambiarContraseña_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
        }
        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }
        private void FrmCambiarContraseña_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }
    }
}
