using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaDominio;
using CapaInterfaces.INTERFACES;
using CapaServicios;
using CapaServicios.Composite;

namespace CapaIU.Seguridad
{
    public partial class FrmPatentesYFamilias : Form, IIdiomaObserver
    {
        PermisosBLL repo;
        Familia seleccion;
        TraductorBLL tradu;
        Bitacora bit;
        BitacoraBLL bitbll = new BitacoraBLL();
        public FrmPatentesYFamilias()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            repo = new PermisosBLL();
            tradu = new TraductorBLL();
            this.cboPermisos.DataSource = repo.GetAllPermission();
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

            if (grpPatentes.Tag != null && traducciones.ContainsKey(grpPatentes.Tag.ToString()))
                grpPatentes.Text = traducciones[grpPatentes.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (cmdAgregarPatente.Tag != null && traducciones.ContainsKey(cmdAgregarPatente.Tag.ToString()))
                cmdAgregarPatente.Text = traducciones[cmdAgregarPatente.Tag.ToString()].Texto;

            if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                groupBox1.Text = traducciones[groupBox1.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (txtNombrePatente.Tag != null && traducciones.ContainsKey(txtNombrePatente.Tag.ToString()))
                txtNombrePatente.Text = traducciones[txtNombrePatente.Tag.ToString()].Texto;

            if (btnGuardarPatente.Tag != null && traducciones.ContainsKey(btnGuardarPatente.Tag.ToString()))
                btnGuardarPatente.Text = traducciones[btnGuardarPatente.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (cmdSeleccionar.Tag != null && traducciones.ContainsKey(cmdSeleccionar.Tag.ToString()))
                cmdSeleccionar.Text = traducciones[cmdSeleccionar.Tag.ToString()].Texto;

            if (cmdAgregarFamilia.Tag != null && traducciones.ContainsKey(cmdAgregarFamilia.Tag.ToString()))
                cmdAgregarFamilia.Text = traducciones[cmdAgregarFamilia.Tag.ToString()].Texto;

             if (groupBox3.Tag != null && traducciones.ContainsKey(groupBox3.Tag.ToString()))
                groupBox3.Text = traducciones[groupBox3.Tag.ToString()].Texto;

            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;

            if (txtNombreFamilia.Tag != null && traducciones.ContainsKey(txtNombreFamilia.Tag.ToString()))
                txtNombreFamilia.Text = traducciones[txtNombreFamilia.Tag.ToString()].Texto;


            if (btnGuardarFamilia.Tag != null && traducciones.ContainsKey(btnGuardarFamilia.Tag.ToString()))
                btnGuardarFamilia.Text = traducciones[btnGuardarFamilia.Tag.ToString()].Texto;
            
            if (groupBox4.Tag != null && traducciones.ContainsKey(groupBox4.Tag.ToString()))
                groupBox4.Text = traducciones[groupBox4.Tag.ToString()].Texto;

            if (cmdGuardarFamilia.Tag != null && traducciones.ContainsKey(cmdGuardarFamilia.Tag.ToString()))
                cmdGuardarFamilia.Text = traducciones[cmdGuardarFamilia.Tag.ToString()].Texto;


        }

        private void btnGuardarPatente_Click(object sender, EventArgs e)
        {
            Patente p = new Patente()
            {
                Nombre = this.txtNombrePatente.Text,
                Permiso = (TipoPermiso)this.cboPermisos.SelectedItem

            };

            repo.GuardarComponente(p, false);
            LlenarPatentesFamilias();

            MessageBox.Show("Patente guardada correctamente");
            Limpiar();
        }

        private void LlenarPatentesFamilias()
        {
            this.cboPatentes.DataSource = repo.GetAllPatentes();
            this.cboFamilias.DataSource = repo.GetAllFamilias();
        }
        private void Limpiar()
        {
            txtNombreFamilia.Clear();
            txtNombrePatente.Clear();
        }

        private void FrmPatentesYFamilias_Load(object sender, EventArgs e)
        {
            LlenarPatentesFamilias();
            SingletonSesion.Instancia.SuscribirObservador(this);
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
        private void btnGuardarFamilia_Click(object sender, EventArgs e)
        {
            Familia p = new Familia()
            {
                Nombre = this.txtNombreFamilia.Text

            };
            repo.GuardarComponente(p, true);
            LlenarPatentesFamilias();
            MessageBox.Show("Familia guardada correctamente");
            string bit = "Se agrego una nueva familia :  " + txtNombreFamilia.Text + "por : "+ SingletonSesion.Instancia.Usuario.usuario;
            GrabarBitacora(bit);
        }

        private void cmdAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (seleccion != null)
            {
                var familia = (Familia)cboFamilias.SelectedItem;
                if (familia != null)
                {

                    var esta = repo.Existe(seleccion, familia.Id);
                    if (esta)
                        MessageBox.Show("ya exsite la familia indicada");
                    else
                    {

                        repo.FillFamilyComponents(familia);
                        seleccion.AgregarHijo(familia);
                        MostrarFamilia(false);
                    }


                }
            }
        }

        private void cmdSeleccionar_Click(object sender, EventArgs e)
        {
            var tmp = (Familia)this.cboFamilias.SelectedItem;
            seleccion = new Familia();
            seleccion.Id = tmp.Id;
            seleccion.Nombre = tmp.Nombre;

            MostrarFamilia(true);
        }

        void MostrarFamilia(bool init)
        {
            if (seleccion == null) return;


            IList<Componente> flia = null;
            if (init)
            {
                //traigo los hijos de la base
                flia = repo.GetAll("=" + seleccion.Id);


                foreach (var i in flia)
                    seleccion.AgregarHijo(i);
            }
            else
            {
                flia = seleccion.Hijos;
            }

            this.treeConfigurarFamilia.Nodes.Clear();

            TreeNode root = new TreeNode(seleccion.Nombre);
            root.Tag = seleccion;
            this.treeConfigurarFamilia.Nodes.Add(root);

            foreach (var item in flia)
            {
                MostrarEnTreeView(root, item);
            }

            treeConfigurarFamilia.ExpandAll();
        }

        void MostrarEnTreeView(TreeNode tn, Componente c)
        {
            TreeNode n = new TreeNode(c.Nombre);
            tn.Tag = c;
            tn.Nodes.Add(n);
            if (c.Hijos != null)
                foreach (var item in c.Hijos)
                {
                    MostrarEnTreeView(n, item);
                }

        }

        private void cmdAgregarPatente_Click(object sender, EventArgs e)
        {
            if (seleccion != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = repo.Existe(seleccion, patente.Id);
                    if (esta)
                        MessageBox.Show("ya existe la patente indicada");
                    else
                    {

                        {
                            seleccion.AgregarHijo(patente);
                            MostrarFamilia(false);
                        }
                    }
                }
            }
        }

        private void cmdGuardarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                repo.GuardarFamilia(seleccion);
                MessageBox.Show("Familia guardada correctamente");
                string bit = "Se agregaron nuevas patentes a una familia : " +cboFamilias.Text + "por el Usuario : "+ SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar la familia");
            }
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void FrmPatentesYFamilias_FormClosed(object sender, FormClosedEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }
    }
}
