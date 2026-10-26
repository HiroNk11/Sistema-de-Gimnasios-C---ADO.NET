using CapaAplicacion;
using CapaDominio;
using CapaInterfaces.INTERFACES;
using CapaIU.Controles;
using CapaServicios;
using CapaServicios.MultiIdioma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU.Seguridad
{
    public partial class FrmGestionarIdioma : Form, IIdiomaObserver
    {
        IdiomaBLL gestoridioma = new IdiomaBLL();
        TraductorBLL tradu = new TraductorBLL();
        Idioma tmp;
        Idioma id = new Idioma();
        Etiquetas et;
        Etiqueta et2 = new Etiqueta();
        Bitacora bit;
        BitacoraBLL bitbll = new BitacoraBLL();
        public FrmGestionarIdioma()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            ObtenerIdiomas();
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


            if (this.cmbidioma.Tag != null && traducciones.ContainsKey(this.cmbidioma.Tag.ToString()))
                this.cmbidioma.Etiqueta = traducciones[this.cmbidioma.Tag.ToString()].Texto;

            if (this.btnagregar.Tag != null && traducciones.ContainsKey(this.btnagregar.Tag.ToString()))
                this.btnagregar.Text = traducciones[this.btnagregar.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (txttraduccion.Tag != null && traducciones.ContainsKey(txttraduccion.Tag.ToString()))
                txttraduccion.Etiqueta = traducciones[txttraduccion.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (btnbuscar.Tag != null && traducciones.ContainsKey(btnbuscar.Tag.ToString()))
                btnbuscar.Text = traducciones[btnbuscar.Tag.ToString()].Texto;

            if (lble.Tag != null && traducciones.ContainsKey(lble.Tag.ToString()))
                lble.Text = traducciones[lble.Tag.ToString()].Texto;

            if (txttraduccion.Tag != null && traducciones.ContainsKey(txttraduccion.Tag.ToString()))
                txttraduccion.Text = traducciones[txttraduccion.Tag.ToString()].Texto;

            if (btnguardar.Tag != null && traducciones.ContainsKey(btnguardar.Tag.ToString()))
                btnguardar.Text = traducciones[btnguardar.Tag.ToString()].Texto;
        }

        public void GrabarBitacora(string detallebit)
        {
            bit = new Bitacora();
            bit.id_usuario = SingletonSesion.Instancia.Usuario.idusuario;
            bit.actividad = detallebit;
            bit.criticidad = "Media";
            bit.fecha = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            bit.hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            bitbll.InsertarEvento(bit);
            bit = null;
        }
        private void btnagregar_Click(object sender, EventArgs e)
        {
            bool ok = true;

            foreach (Control ctr in this.Controls)
            {
                if (ctr is control_combo)
                {
                    ok = ((control_combo)ctr).Validar() && ok;
                }
            }
            if (ok)
            {
                tmp = new Idioma();
                tmp.Nombre = cmbidioma.Texto;
                tmp.Default = false;
                MessageBox.Show(gestoridioma.InsertarIdioma(tmp));
                string bit = "Se agrego un nuevo idioma por : " + SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);
                tmp = null;
                // insertar etiquetas   
                // gestoridioma.InsertarEtiquetas(3, 1, "default");
                var t = gestoridioma.GetAllEtiquetas();
                var i = tradu.ObtenerIdiomas();
                foreach (var etiquetas in t)
                {
                    foreach (var idiomas in i)
                    {
                        if (cmbidioma.Texto == idiomas.Nombre)
                        {
                            gestoridioma.InsertarEtiquetas(idiomas.Id, etiquetas.Id, etiquetas.Nombre);
                        }

                    }

                }
            }
            if (!ok)
            {
                MessageBox.Show("Revise los campos", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ObtenerIdiomas();
            cmbidioma.Texto = "";
        }
              
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            
                GrillaIdioma.DataSource = null;
                GrillaIdioma.DataSource = gestoridioma.ObtenerEyT(cmbidioma2.Text);
            
        }

        private void GrillaIdioma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                et = (Etiquetas)GrillaIdioma.Rows[e.RowIndex].DataBoundItem;
                txtetiqueta.Text = et.Nombre_Etiqueta.ToString();
                txttraduccion.Texto = et.Traduccion.ToString();
                txttraduccion.Enabled = true;
            }
            catch (Exception)
            {
                txtetiqueta.Enabled = false;
                txttraduccion.Enabled = false;
                MessageBox.Show("Seleccione un usuario, Por Favor");
            }
            
        }

 
        public void ObtenerIdiomas()
        {
            cmbidioma2.DataSource = tradu.ObtenerIdiomas();
        }

        private void btnguardar_Click_1(object sender, EventArgs e)
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
                int i = gestoridioma.ObtenerTraduccion(txtetiqueta.Text);
                gestoridioma.EditarTraduccion(txttraduccion.Texto, cmbidioma2.Text, i);
                MessageBox.Show("Traduccion Agregada");
                GrillaIdioma.DataSource = gestoridioma.ObtenerEyT(cmbidioma2.Text);
                string bit = "Se han modificado etiquetas de : " + cmbidioma2.Text + " realizado por : " + SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);
            }
            if (!ok)
            {
                MessageBox.Show("Revise los campos", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void FrmGestionarIdioma_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        private void FrmGestionarIdioma_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }
    }
}
