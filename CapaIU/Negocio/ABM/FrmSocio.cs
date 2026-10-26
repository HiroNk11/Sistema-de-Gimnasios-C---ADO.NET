using CapaAplicacion;
using CapaAplicacion.Negocio;
using CapaDominio;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Xml.Serialization;
using CapaServicios.Composite;

namespace CapaIU.Negocio
{
    public partial class FrmSocio : Form, IIdiomaObserver
    {
        Socio tmp;
        SocioBLL gestorsocio = new SocioBLL();
        TraductorBLL tradu;
        UsuarioBLL _usuarioBLL;
        PermisoUsuario usu;
        PermisosBLL permisosbll;
        public FrmSocio()
        {
   
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            _usuarioBLL = new UsuarioBLL();
            permisosbll = new PermisosBLL();
            ValidarPermisos();
            ListarSocio();
            tradu = new TraductorBLL();
            if (SingletonSesion.Instancia.IsLogged())
                Traducir(SingletonSesion.Instancia.Usuario.Idioma);
            else
                Traducir(); //trae el idioma por default
        }
        public void ValidarPermisos()
        {
            var lista = _usuarioBLL.Listar();
            foreach (var i in lista)
            {
                if (SingletonSesion.Instancia.Usuario.usuario == i.usuario)
                {
                    usu = new PermisoUsuario();
                    usu.idusuario = i.idusuario;
                    usu.Nombre = i.usuario;
                    btnserializar.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeSerializar, usu);
                    btnxml.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeSerializar, usu);
                    btnver.Visible = permisosbll.BuscarPermisos(TipoPermiso.PuedeVerhistorialCambios, usu);
                }
            }
        }
        private void Traducir(IIdioma idioma = null)
        {
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

            if (rEGIST.Tag != null && traducciones.ContainsKey(rEGIST.Tag.ToString()))
                rEGIST.Text = traducciones[rEGIST.Tag.ToString()].Texto;

            if (txtnombre.Tag != null && traducciones.ContainsKey(txtnombre.Tag.ToString()))
                txtnombre.Etiqueta = traducciones[txtnombre.Tag.ToString()].Texto;

            if (txtapellido.Tag != null && traducciones.ContainsKey(txtapellido.Tag.ToString()))
                txtapellido.Etiqueta = traducciones[txtapellido.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (btnagregar.Tag != null && traducciones.ContainsKey(btnagregar.Tag.ToString()))
                btnagregar.Text = traducciones[btnagregar.Tag.ToString()].Texto;

            if (btneliminar.Tag != null && traducciones.ContainsKey(btneliminar.Tag.ToString()))
                btneliminar.Text = traducciones[btneliminar.Tag.ToString()].Texto;


            if (btnmodificar.Tag != null && traducciones.ContainsKey(btnmodificar.Tag.ToString()))
                btnmodificar.Text = traducciones[btnmodificar.Tag.ToString()].Texto;
        }

        public void ListarSocio()
        {
            grillasocios.DataSource = null;
            grillasocios.DataSource = gestorsocio.Listar();
        
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
                tmp = new Socio();
                tmp.Nombre = txtnombre.Texto;
                tmp.Apellido = txtapellido.Texto;
                tmp.Fecha = dtfn.Value;
                gestorsocio.Insertar(tmp);
                tmp = null;
                ListarSocio();
                Limpiar();
            }
            if (!ok)
            {
                MessageBox.Show("Revise los campos", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Limpiar()
        {
            txtnombre.Texto = "";
            txtapellido.Texto = "";
            lblidsocio.Text = "";
            txtbuscar.Text = "";
        }
        private void FrmSocio_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
            ListarSocio();
            Limpiar();
        }

        private void grillasocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grillasocios.CurrentCell != null)
                {
                    int fila;
                    fila = grillasocios.CurrentCell.RowIndex;
                    lblidsocio.Text = grillasocios[0, fila].Value.ToString();
                    txtnombre.Texto = grillasocios[1, fila].Value.ToString();
                    txtapellido.Texto = grillasocios[2, fila].Value.ToString();
                    dtfn.Text = grillasocios[3, fila].Value.ToString();
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

        private void btneliminar_Click(object sender, EventArgs e)
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
                tmp = new Socio();
                tmp.Id_Socio = int.Parse(lblidsocio.Text);
                MessageBox.Show(gestorsocio.Borrar(tmp));
                tmp = null;
                ListarSocio();
                Limpiar();
            }
            if (!ok)
            {
                MessageBox.Show("Revise los campos", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                tmp = new Socio();
                tmp.Id_Socio = int.Parse(lblidsocio.Text);
                tmp.Nombre = txtnombre.Texto;
                tmp.Apellido = txtapellido.Texto;
                tmp.Fecha = Convert.ToDateTime(dtfn.Text);
                MessageBox.Show(gestorsocio.Editar(tmp));
                tmp = null;
                ListarSocio();
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
                string Nombre = txtbuscar.Text;
                DataTable dt = new DataTable();
                dt = gestorsocio.Buscar_Socio(Nombre);
                grillasocios.DataSource = null;
                grillasocios.DataSource = dt;
            }
            else
            {
                ListarSocio();
            }
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void FrmSocio_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }

        private void btnver_Click(object sender, EventArgs e)
        {
            FrmHistorialCambios fhc = new FrmHistorialCambios();
            fhc.ShowDialog();
        }

        private void btnserializar_Click(object sender, EventArgs e)
        {
            try 
            { 
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        string txt = saveFileDialog1.FileName + ".txt";                   
                        StreamWriter textguardar = File.CreateText(txt);
                    
                  
                        textguardar.Flush();
                        textguardar.Close();
                        string mijson = JsonSerializer.Serialize(gestorsocio.Listar());
                        File.WriteAllText(txt, mijson);
                    }
                    else
                    {
                        string txt = saveFileDialog1.FileName + ".txt";
                        StreamWriter textguardar = File.CreateText(txt);
                      
                        textguardar.Flush();
                        textguardar.Close();

                        string mijson = JsonSerializer.Serialize(gestorsocio.Listar());
                        File.WriteAllText(txt, mijson);
                    }
                }
            }
            catch
            {

            }
          
        }

        private void btnxml_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        string txt = saveFileDialog1.FileName+".txt";
                        StreamWriter textguardar = File.CreateText(txt);
                        textguardar.Flush();
                        textguardar.Close();
                        gestorsocio.CrearXML(txt);

                    }
                    else
                    {
                        string txt = saveFileDialog1.FileName+".txt";
                        StreamWriter textguardar = File.CreateText(txt);

                        textguardar.Flush();
                        textguardar.Close();
                        gestorsocio.CrearXML(txt);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
