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
            using (var destino = new SaveFileDialog { Filter="PDF (*.pdf)|*.pdf", DefaultExt="pdf", AddExtension=true, FileName="Clases-"+DateTime.Now.ToString("yyyyMMdd-HHmmss")+".pdf" })
            {
                if (destino.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var stream = new FileStream(destino.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (var documento = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25))
                    {
                        PdfWriter.GetInstance(documento, stream);
                        documento.Open();
                        documento.Add(new Paragraph("Clases del socio"));
                        documento.Add(new Paragraph(cmbseleccionarsocio.Text));
                        documento.Add(new Paragraph(DateTime.Today.ToString("dd/MM/yyyy")));
                        var tabla = new PdfPTable(6) { WidthPercentage=100, SpacingBefore=15 };
                        string[] columnas={"Descripcion_Clase","Actividad","Nivel","Descripcion_Piso","Dia","Horario"};
                        foreach(string titulo in new[]{"Clase","Actividad","Nivel","Piso","Día","Horario"})tabla.AddCell(new Phrase(titulo));
                        tabla.HeaderRows=1;
                        foreach(DataGridViewRow fila in grillaclases.Rows)
                            if(!fila.IsNewRow)foreach(string columna in columnas)
                                tabla.AddCell(new Phrase(Convert.ToString(fila.Cells[columna].Value)));
                        documento.Add(tabla);
                        documento.Close();
                    }
                }
                catch(Exception ex) { CapaIU.Program.Error(ex); }
            }
        }
    }
}
