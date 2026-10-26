using CapaAplicacion;
using CapaDominio;
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
    public partial class FrmBackupRestore : Form
    {
        BackupRestoreBLL gestorbr = new BackupRestoreBLL();
        Bitacora bit;
        BitacoraBLL bitbll = new BitacoraBLL();
        public FrmBackupRestore()
        {
            InitializeComponent();
            CapaIU.Controles.TemaVisual.Aplicar(this);
            btnbackup.Enabled = false;
            btnrestore.Enabled = false;
            var permiso=new CapaServicios.Composite.PermisoUsuario {idusuario=SingletonSesion.Instancia.Usuario.idusuario};
            var gestor=new PermisosBLL();
            bool ambos=gestor.BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeAccederBackupRestore,permiso);
            btnbackup.Visible=ambos || gestor.BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeAccederBackup,permiso);
            btnrestore.Visible=ambos || gestor.BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeAccederRestore,permiso);
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtruta.Text = dlg.SelectedPath;
                btnbackup.Enabled = true;
            }
        }

        private void btnbrowserestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtruta2.Text = dlg.FileName;
                btnrestore.Enabled = true;
            }
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
        private void btnbackup_Click(object sender, EventArgs e)
        {
            if (txtruta.Text == string.Empty)
            {
                MessageBox.Show("Please enter backup file location");
            }
            else
            {
               MessageBox.Show(gestorbr.Backup(txtruta.Text));
                btnbackup.Enabled = false;
                string bit = "Se ha realizado un Backuo por : " + SingletonSesion.Instancia.Usuario.usuario;
                GrabarBitacora(bit);

            }
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("La restauración reemplazará los datos de la base configurada y desconectará otras sesiones. ¿Continuar?", "Restaurar respaldo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;
            MessageBox.Show(gestorbr.Restore(txtruta2.Text));
            MessageBox.Show("Cierre y vuelva a abrir la aplicación para validar la base restaurada.");
            Application.Exit();
        }
    }
}
