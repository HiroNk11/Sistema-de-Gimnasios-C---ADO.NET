namespace CapaIU
{
    partial class FrmMdiPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnulogut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuidioma = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuIdiomas = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarListaIdiomasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuseg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnufyp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuusuario = new System.Windows.Forms.ToolStripMenuItem();
            this.backupRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restaurarDVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnugesusu = new System.Windows.Forms.ToolStripMenuItem();
            this.negocioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.socioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarSocioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarClaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verClasesSocioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entrenadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarEntrenadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entrenamientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarEntrenamientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearClaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarClaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarHorarioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblSession = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSesion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.verDetalleClaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSesion,
            this.mnuidioma,
            this.mnuseg,
            this.mnuges,
            this.negocioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(790, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuSesion
            // 
            this.mnuSesion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarContraseñaToolStripMenuItem,
            this.mnulogut});
            this.mnuSesion.Name = "mnuSesion";
            this.mnuSesion.Size = new System.Drawing.Size(53, 20);
            this.mnuSesion.Tag = "sesion";
            this.mnuSesion.Text = "Sesión";
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.cambiarContraseñaToolStripMenuItem.Tag = "cambiar_contraseña";
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // mnulogut
            // 
            this.mnulogut.Name = "mnulogut";
            this.mnulogut.Size = new System.Drawing.Size(182, 22);
            this.mnulogut.Tag = "cerrarsesion";
            this.mnulogut.Text = "Cerrar Sesión";
            this.mnulogut.Click += new System.EventHandler(this.itemLogout_Click);
            // 
            // mnuidioma
            // 
            this.mnuidioma.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuIdiomas,
            this.gestionarIdiomaToolStripMenuItem,
            this.actualizarListaIdiomasToolStripMenuItem});
            this.mnuidioma.Name = "mnuidioma";
            this.mnuidioma.Size = new System.Drawing.Size(56, 20);
            this.mnuidioma.Tag = "idioma";
            this.mnuidioma.Text = "Idioma";
            // 
            // MnuIdiomas
            // 
            this.MnuIdiomas.Name = "MnuIdiomas";
            this.MnuIdiomas.Size = new System.Drawing.Size(198, 22);
            this.MnuIdiomas.Tag = "seleccionar_idioma";
            this.MnuIdiomas.Text = "Seleccionar Idioma";
            // 
            // gestionarIdiomaToolStripMenuItem
            // 
            this.gestionarIdiomaToolStripMenuItem.Name = "gestionarIdiomaToolStripMenuItem";
            this.gestionarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.gestionarIdiomaToolStripMenuItem.Tag = "gestionar_idioma";
            this.gestionarIdiomaToolStripMenuItem.Text = "Gestionar Idioma";
            this.gestionarIdiomaToolStripMenuItem.Click += new System.EventHandler(this.gestionarIdiomaToolStripMenuItem_Click);
            // 
            // actualizarListaIdiomasToolStripMenuItem
            // 
            this.actualizarListaIdiomasToolStripMenuItem.Name = "actualizarListaIdiomasToolStripMenuItem";
            this.actualizarListaIdiomasToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.actualizarListaIdiomasToolStripMenuItem.Tag = "actualizar_lista_idiomas";
            this.actualizarListaIdiomasToolStripMenuItem.Text = "Actualizar Lista Idiomas";
            this.actualizarListaIdiomasToolStripMenuItem.Click += new System.EventHandler(this.actualizarListaIdiomasToolStripMenuItem_Click);
            // 
            // mnuseg
            // 
            this.mnuseg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnufyp,
            this.mnuusuario,
            this.backupRestoreToolStripMenuItem,
            this.bitacoraToolStripMenuItem,
            this.restaurarDVToolStripMenuItem});
            this.mnuseg.Name = "mnuseg";
            this.mnuseg.Size = new System.Drawing.Size(72, 20);
            this.mnuseg.Tag = "seguridad";
            this.mnuseg.Text = "Seguridad";
            // 
            // mnufyp
            // 
            this.mnufyp.Name = "mnufyp";
            this.mnufyp.Size = new System.Drawing.Size(207, 22);
            this.mnufyp.Tag = "famypat";
            this.mnufyp.Text = "Familias-Patentes";
            this.mnufyp.Click += new System.EventHandler(this.familiasYPatentesToolStripMenuItem_Click);
            // 
            // mnuusuario
            // 
            this.mnuusuario.Name = "mnuusuario";
            this.mnuusuario.Size = new System.Drawing.Size(207, 22);
            this.mnuusuario.Tag = "usuarios";
            this.mnuusuario.Text = "Usuarios-Familia-Patente";
            this.mnuusuario.Click += new System.EventHandler(this.usuariosToolStripMenuItem1_Click);
            // 
            // backupRestoreToolStripMenuItem
            // 
            this.backupRestoreToolStripMenuItem.Name = "backupRestoreToolStripMenuItem";
            this.backupRestoreToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.backupRestoreToolStripMenuItem.Text = "Backup/Restore";
            this.backupRestoreToolStripMenuItem.Click += new System.EventHandler(this.backupRestoreToolStripMenuItem_Click);
            // 
            // bitacoraToolStripMenuItem
            // 
            this.bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            this.bitacoraToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.bitacoraToolStripMenuItem.Text = "Bitacora";
            this.bitacoraToolStripMenuItem.Click += new System.EventHandler(this.bitacoraToolStripMenuItem_Click);
            // 
            // restaurarDVToolStripMenuItem
            // 
            this.restaurarDVToolStripMenuItem.Name = "restaurarDVToolStripMenuItem";
            this.restaurarDVToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.restaurarDVToolStripMenuItem.Text = "Restaurar DV";
            this.restaurarDVToolStripMenuItem.Click += new System.EventHandler(this.restaurarDVToolStripMenuItem_Click);
            // 
            // mnuges
            // 
            this.mnuges.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnugesusu});
            this.mnuges.Name = "mnuges";
            this.mnuges.Size = new System.Drawing.Size(59, 20);
            this.mnuges.Tag = "gestion";
            this.mnuges.Text = "Gestión";
            // 
            // mnugesusu
            // 
            this.mnugesusu.Name = "mnugesusu";
            this.mnugesusu.Size = new System.Drawing.Size(119, 22);
            this.mnugesusu.Tag = "usuarios";
            this.mnugesusu.Text = "Usuarios";
            this.mnugesusu.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // negocioToolStripMenuItem
            // 
            this.negocioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.socioToolStripMenuItem,
            this.entrenadorToolStripMenuItem,
            this.entrenamientoToolStripMenuItem,
            this.claseToolStripMenuItem,
            this.horarioToolStripMenuItem,
            this.calendarioToolStripMenuItem});
            this.negocioToolStripMenuItem.Name = "negocioToolStripMenuItem";
            this.negocioToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.negocioToolStripMenuItem.Tag = "negocio";
            this.negocioToolStripMenuItem.Text = "Negocio";
            // 
            // socioToolStripMenuItem
            // 
            this.socioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionarSocioToolStripMenuItem,
            this.administrarClaseToolStripMenuItem,
            this.verClasesSocioToolStripMenuItem});
            this.socioToolStripMenuItem.Name = "socioToolStripMenuItem";
            this.socioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.socioToolStripMenuItem.Tag = "socio";
            this.socioToolStripMenuItem.Text = "Socio";
            // 
            // gestionarSocioToolStripMenuItem
            // 
            this.gestionarSocioToolStripMenuItem.Name = "gestionarSocioToolStripMenuItem";
            this.gestionarSocioToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.gestionarSocioToolStripMenuItem.Tag = "ges_socio";
            this.gestionarSocioToolStripMenuItem.Text = "Gestionar Socio";
            this.gestionarSocioToolStripMenuItem.Click += new System.EventHandler(this.gestionarSocioToolStripMenuItem_Click);
            // 
            // administrarClaseToolStripMenuItem
            // 
            this.administrarClaseToolStripMenuItem.Name = "administrarClaseToolStripMenuItem";
            this.administrarClaseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.administrarClaseToolStripMenuItem.Text = "Inscribir a Clase";
            this.administrarClaseToolStripMenuItem.Click += new System.EventHandler(this.administrarClaseToolStripMenuItem_Click);
            // 
            // verClasesSocioToolStripMenuItem
            // 
            this.verClasesSocioToolStripMenuItem.Name = "verClasesSocioToolStripMenuItem";
            this.verClasesSocioToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.verClasesSocioToolStripMenuItem.Tag = "ver clases";
            this.verClasesSocioToolStripMenuItem.Text = "Ver Clases ";
            this.verClasesSocioToolStripMenuItem.Click += new System.EventHandler(this.verClasesSocioToolStripMenuItem_Click);
            // 
            // entrenadorToolStripMenuItem
            // 
            this.entrenadorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionarEntrenadorToolStripMenuItem});
            this.entrenadorToolStripMenuItem.Name = "entrenadorToolStripMenuItem";
            this.entrenadorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.entrenadorToolStripMenuItem.Tag = "entrenador";
            this.entrenadorToolStripMenuItem.Text = "Entrenador";
            // 
            // gestionarEntrenadorToolStripMenuItem
            // 
            this.gestionarEntrenadorToolStripMenuItem.Name = "gestionarEntrenadorToolStripMenuItem";
            this.gestionarEntrenadorToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.gestionarEntrenadorToolStripMenuItem.Tag = "gestionar_entrenador";
            this.gestionarEntrenadorToolStripMenuItem.Text = "Gestionar Entrenador";
            this.gestionarEntrenadorToolStripMenuItem.Click += new System.EventHandler(this.gestionarEntrenadorToolStripMenuItem_Click);
            // 
            // entrenamientoToolStripMenuItem
            // 
            this.entrenamientoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionarEntrenamientoToolStripMenuItem});
            this.entrenamientoToolStripMenuItem.Name = "entrenamientoToolStripMenuItem";
            this.entrenamientoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.entrenamientoToolStripMenuItem.Tag = "entrenamiento";
            this.entrenamientoToolStripMenuItem.Text = "Entrenamiento";
            // 
            // gestionarEntrenamientoToolStripMenuItem
            // 
            this.gestionarEntrenamientoToolStripMenuItem.Name = "gestionarEntrenamientoToolStripMenuItem";
            this.gestionarEntrenamientoToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.gestionarEntrenamientoToolStripMenuItem.Tag = "gestionar_entrenamiento";
            this.gestionarEntrenamientoToolStripMenuItem.Text = "Gestionar Entrenamiento";
            this.gestionarEntrenamientoToolStripMenuItem.Click += new System.EventHandler(this.gestionarEntrenamientoToolStripMenuItem_Click);
            // 
            // claseToolStripMenuItem
            // 
            this.claseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crearClaseToolStripMenuItem,
            this.gestionarClaseToolStripMenuItem,
            this.verDetalleClaseToolStripMenuItem});
            this.claseToolStripMenuItem.Name = "claseToolStripMenuItem";
            this.claseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.claseToolStripMenuItem.Tag = "clase";
            this.claseToolStripMenuItem.Text = "Clase";
            // 
            // crearClaseToolStripMenuItem
            // 
            this.crearClaseToolStripMenuItem.Name = "crearClaseToolStripMenuItem";
            this.crearClaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.crearClaseToolStripMenuItem.Tag = "crear clase";
            this.crearClaseToolStripMenuItem.Text = "Crear Clase";
            this.crearClaseToolStripMenuItem.Click += new System.EventHandler(this.crearClaseToolStripMenuItem_Click);
            // 
            // gestionarClaseToolStripMenuItem
            // 
            this.gestionarClaseToolStripMenuItem.Name = "gestionarClaseToolStripMenuItem";
            this.gestionarClaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionarClaseToolStripMenuItem.Text = "Gestionar Clase";
            this.gestionarClaseToolStripMenuItem.Click += new System.EventHandler(this.gestionarClaseToolStripMenuItem_Click);
            // 
            // horarioToolStripMenuItem
            // 
            this.horarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignarHorarioToolStripMenuItem1});
            this.horarioToolStripMenuItem.Name = "horarioToolStripMenuItem";
            this.horarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.horarioToolStripMenuItem.Text = "Horario";
            // 
            // asignarHorarioToolStripMenuItem1
            // 
            this.asignarHorarioToolStripMenuItem1.Name = "asignarHorarioToolStripMenuItem1";
            this.asignarHorarioToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.asignarHorarioToolStripMenuItem1.Text = "Asignar Horario";
            this.asignarHorarioToolStripMenuItem1.Click += new System.EventHandler(this.asignarHorarioToolStripMenuItem1_Click);
            // 
            // calendarioToolStripMenuItem
            // 
            this.calendarioToolStripMenuItem.Name = "calendarioToolStripMenuItem";
            this.calendarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.calendarioToolStripMenuItem.Text = "Calendario";
            this.calendarioToolStripMenuItem.Click += new System.EventHandler(this.calendarioToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSession,
            this.toolStripSesion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 220);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(790, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblSession
            // 
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(39, 17);
            this.lblSession.Text = "Status";
            // 
            // toolStripSesion
            // 
            this.toolStripSesion.Name = "toolStripSesion";
            this.toolStripSesion.Size = new System.Drawing.Size(116, 17);
            this.toolStripSesion.Text = "[ Sesión no iniciada ]";
            // 
            // verDetalleClaseToolStripMenuItem
            // 
            this.verDetalleClaseToolStripMenuItem.Name = "verDetalleClaseToolStripMenuItem";
            this.verDetalleClaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verDetalleClaseToolStripMenuItem.Text = "Ver detalle Clase";
            this.verDetalleClaseToolStripMenuItem.Click += new System.EventHandler(this.verDetalleClaseToolStripMenuItem_Click);
            // 
            // FrmMdiPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 242);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMdiPrincipal";
            this.Text = "Formulario Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMdiPrincipal_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMdiPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FrmMdiPrincipal_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmMdiPrincipal_HelpRequested);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSesion;
        private System.Windows.Forms.ToolStripMenuItem mnulogut;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSesion;
        private System.Windows.Forms.ToolStripMenuItem mnuges;
        private System.Windows.Forms.ToolStripMenuItem mnugesusu;
        private System.Windows.Forms.ToolStripMenuItem mnuseg;
        private System.Windows.Forms.ToolStripMenuItem mnufyp;
        private System.Windows.Forms.ToolStripMenuItem mnuusuario;
        private System.Windows.Forms.ToolStripStatusLabel lblSession;
        private System.Windows.Forms.ToolStripMenuItem negocioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarIdiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuidioma;
        public System.Windows.Forms.ToolStripMenuItem MnuIdiomas;
        private System.Windows.Forms.ToolStripMenuItem actualizarListaIdiomasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem socioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entrenadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarSocioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarEntrenadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verClasesSocioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entrenamientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarEntrenamientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearClaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupRestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarClaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarClaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignarHorarioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem calendarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restaurarDVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verDetalleClaseToolStripMenuItem;
    }
}

