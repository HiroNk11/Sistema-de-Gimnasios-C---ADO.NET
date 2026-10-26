using CapaAplicacion;
using CapaDominio;
using CapaInterfaces.INTERFACES;
using CapaServicios;
using CapaServicios.Composite;
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
    public partial class FrmSegUsuarios : Form, IIdiomaObserver
    {
        UsuarioBLL repo;
        PermisoUsuario seleccion;
        PermisoUsuario tmp;
        PermisosBLL repoPermiso;
        Bitacora bit;
        BitacoraBLL bitbll = new BitacoraBLL();
        TraductorBLL tradu;
        public FrmSegUsuarios()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            //permisos
            repo = new UsuarioBLL();
            repoPermiso = new PermisosBLL();
            cmbusers.DataSource = repo.GetAll();
            this.cboFamilias.DataSource = repoPermiso.GetAllFamilias();
            this.cboPatentes.DataSource = repoPermiso.GetAllPatentes();
            cboPatentes.Items.Remove(cboPatentes.Items.Equals("Baja Usuario"));
            //traduccion
            tradu = new TraductorBLL();
            if (SingletonSesion.Instancia.IsLogged())
                Traducir(SingletonSesion.Instancia.Usuario.Idioma);
            else
                Traducir(); //trae el idioma por default

        }

        //permisos
        void LlenarTreeView(TreeNode padre, Componente c)
        {
            TreeNode hijo = new TreeNode(c.Nombre);
            hijo.Tag = c;
            padre.Nodes.Add(hijo);

            foreach (var item in c.Hijos)
            {
                LlenarTreeView(hijo, item);
            }

        }

        void MostrarPermisos(PermisoUsuario u)
        {
            this.treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(u.Nombre);

            foreach (var item in u.Permisos)
            {
                LlenarTreeView(root, item);
            }

            this.treeView1.Nodes.Add(root);
            this.treeView1.ExpandAll();
        }

        private void cmdConfigurar_Click(object sender, EventArgs e)
        {
            seleccion = (PermisoUsuario)cmbusers.SelectedItem;

            //hago una copia del objeto para no modificr el que esta en el combo.
            tmp = new PermisoUsuario();
            tmp.idusuario = seleccion.idusuario;
            tmp.Nombre = seleccion.Nombre;
            repoPermiso.FillUserComponents(tmp);
            MostrarPermisos(tmp);
        }

   

        private void btnagregarpatente_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = false;

                    foreach (var item in tmp.Permisos)
                    {
                        if (repoPermiso.Existe(item, patente.Id))
                        {
                            esta = true;
                            break;
                        }
                    }
                    if (esta)
                        MessageBox.Show("El usuario ya tiene la patente indicada");
                    else
                    {
                        {
                            tmp.Permisos.Add(patente);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void btnagregarfamilia_Click(object sender, EventArgs e)
        {

            if (tmp != null)
            {
                var flia = (Familia)cboFamilias.SelectedItem;
                if (flia != null)
                {
                    var esta = false;
                    //verifico que ya no tenga el permiso. TODO: Esto debe ser parte de otra capa.
                    foreach (var item in tmp.Permisos)
                    {
                        if (repoPermiso.Existe(item, flia.Id))
                        {
                            esta = true;
                        }
                    }

                    if (esta)
                        MessageBox.Show("El usuario ya tiene la familia indicada");
                    else
                    {
                        {
                            repoPermiso.FillFamilyComponents(flia);

                            tmp.Permisos.Add(flia);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }


        private void cmdGuardarFamilia_Click_1(object sender, EventArgs e)
        {
            try
            {
                repo.GuardarPermisos(tmp);
                MessageBox.Show("Usuario guardado correctamente");
                string bit = "Se han modificado permisos de : " + cmbusers.Text + "por el usuario : " + SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar el usuario");
            }
        }

        //traduccion
        private void Traducir(IIdioma idioma = null)
        {
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

            if (grpPatentes.Tag != null && traducciones.ContainsKey(grpPatentes.Tag.ToString()))
                grpPatentes.Text = traducciones[grpPatentes.Tag.ToString()].Texto;

            if (lblallusers.Tag != null && traducciones.ContainsKey(lblallusers.Tag.ToString()))
                lblallusers.Text = traducciones[lblallusers.Tag.ToString()].Texto;
           
            if (cmdConfigurar.Tag != null && traducciones.ContainsKey(cmdConfigurar.Tag.ToString()))
                cmdConfigurar.Text = traducciones[cmdConfigurar.Tag.ToString()].Texto;

            if (lblagrepat.Tag != null && traducciones.ContainsKey(lblagrepat.Tag.ToString()))
                lblagrepat.Text = traducciones[lblagrepat.Tag.ToString()].Texto;

            if (btnagregarpatente.Tag != null && traducciones.ContainsKey(btnagregarpatente.Tag.ToString()))
                btnagregarpatente.Text = traducciones[btnagregarpatente.Tag.ToString()].Texto;

            if (lblagrefam.Tag != null && traducciones.ContainsKey(lblagrefam.Tag.ToString()))
                lblagrefam.Text = traducciones[lblagrefam.Tag.ToString()].Texto;

            if (btnagregarfamilia.Tag != null && traducciones.ContainsKey(btnagregarfamilia.Tag.ToString()))
                btnagregarfamilia.Text = traducciones[btnagregarfamilia.Tag.ToString()].Texto;

            if (cmdGuardar.Tag != null && traducciones.ContainsKey(cmdGuardar.Tag.ToString()))
                cmdGuardar.Text = traducciones[cmdGuardar.Tag.ToString()].Texto;
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
        private void FrmSegUsuarios_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            Traducir(idioma);
        }

        private void FrmSegUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }
    }
}
