using CapaAplicacion;
using CapaAplicacion.Negocio;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
namespace CapaIU.Negocio
{
    public partial class FrmVerClasesSocio : Form, IIdiomaObserver
    {
        SocioBLL gestorsocio = new SocioBLL();
        ClaseBLL gestorclase = new ClaseBLL();
        TraductorBLL tradu;
        public FrmVerClasesSocio()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            tradu = new TraductorBLL();
            cmbseleccionarsocio.DisplayMember="NombreCompleto";
            cmbseleccionarsocio.ValueMember="Id_Socio";
            cmbseleccionarsocio.DataSource=gestorsocio.Listar();
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

            if (this.groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                this.groupBox1.Text = traducciones[groupBox1.Tag.ToString()].Texto;

            if (this.groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                this.groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (this.btnbuscar.Tag != null && traducciones.ContainsKey(btnbuscar.Tag.ToString()))
                this.btnbuscar.Text = traducciones[btnbuscar.Tag.ToString()].Texto;
        }
            private void btnbuscar_Click(object sender, EventArgs e)
        {
            if(cmbseleccionarsocio.SelectedValue==null)throw new ArgumentException("Seleccione un socio.");
            grillaclases.DataSource=gestorclase.GetClasesDeSocioId(Convert.ToInt32(cmbseleccionarsocio.SelectedValue));
        }

        private void FrmVerClasesSocio_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        private void FrmVerClasesSocio_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));
            string PaginaHTML_Texto = Properties.Resources.HTML.ToString();
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@Cliente", cmbseleccionarsocio.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
            string filas = string.Empty;
            foreach (DataGridViewRow row in grillaclases.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Descripcion_Clase"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Actividad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Nivel"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Descripcion_Piso"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Dia"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Horario"].Value.ToString() + "</td>";
                filas += "</tr>";

            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    //Agregamos la imagen del banner al documento
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.shop, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(60, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    //img.SetAbsolutePosition(10,100);
                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                    pdfDoc.Add(img);


                    //pdfDoc.Add(new Phrase("Hola Mundo"));
                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

            }
        }
    }
}
