namespace CapaIU.Seguridad
{
    partial class FrmSegUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdGuardar = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.grpPatentes = new System.Windows.Forms.GroupBox();
            this.cmbusers = new System.Windows.Forms.ComboBox();
            this.btnagregarfamilia = new System.Windows.Forms.Button();
            this.cboFamilias = new System.Windows.Forms.ComboBox();
            this.lblagrefam = new System.Windows.Forms.Label();
            this.btnagregarpatente = new System.Windows.Forms.Button();
            this.cboPatentes = new System.Windows.Forms.ComboBox();
            this.lblagrepat = new System.Windows.Forms.Label();
            this.cmdConfigurar = new System.Windows.Forms.Button();
            this.lblallusers = new System.Windows.Forms.Label();
            this.grpPatentes.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdGuardar
            // 
            this.cmdGuardar.Location = new System.Drawing.Point(283, 253);
            this.cmdGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.cmdGuardar.Name = "cmdGuardar";
            this.cmdGuardar.Size = new System.Drawing.Size(110, 19);
            this.cmdGuardar.TabIndex = 10;
            this.cmdGuardar.Tag = "guardar cambios";
            this.cmdGuardar.Text = "Guardar cambios";
            this.cmdGuardar.UseVisualStyleBackColor = true;
            this.cmdGuardar.Click += new System.EventHandler(this.cmdGuardarFamilia_Click_1);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(283, 36);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(276, 213);
            this.treeView1.TabIndex = 9;
            // 
            // grpPatentes
            // 
            this.grpPatentes.Controls.Add(this.cmbusers);
            this.grpPatentes.Controls.Add(this.btnagregarfamilia);
            this.grpPatentes.Controls.Add(this.cboFamilias);
            this.grpPatentes.Controls.Add(this.lblagrefam);
            this.grpPatentes.Controls.Add(this.btnagregarpatente);
            this.grpPatentes.Controls.Add(this.cboPatentes);
            this.grpPatentes.Controls.Add(this.lblagrepat);
            this.grpPatentes.Controls.Add(this.cmdConfigurar);
            this.grpPatentes.Controls.Add(this.lblallusers);
            this.grpPatentes.Location = new System.Drawing.Point(11, 26);
            this.grpPatentes.Margin = new System.Windows.Forms.Padding(2);
            this.grpPatentes.Name = "grpPatentes";
            this.grpPatentes.Padding = new System.Windows.Forms.Padding(2);
            this.grpPatentes.Size = new System.Drawing.Size(256, 249);
            this.grpPatentes.TabIndex = 8;
            this.grpPatentes.TabStop = false;
            this.grpPatentes.Tag = "seleccion usuario";
            this.grpPatentes.Text = "Usuarios";
            // 
            // cmbusers
            // 
            this.cmbusers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbusers.FormattingEnabled = true;
            this.cmbusers.Location = new System.Drawing.Point(12, 39);
            this.cmbusers.Name = "cmbusers";
            this.cmbusers.Size = new System.Drawing.Size(233, 21);
            this.cmbusers.TabIndex = 14;
            // 
            // btnagregarfamilia
            // 
            this.btnagregarfamilia.Location = new System.Drawing.Point(11, 219);
            this.btnagregarfamilia.Margin = new System.Windows.Forms.Padding(2);
            this.btnagregarfamilia.Name = "btnagregarfamilia";
            this.btnagregarfamilia.Size = new System.Drawing.Size(83, 26);
            this.btnagregarfamilia.TabIndex = 13;
            this.btnagregarfamilia.Tag = "agregar";
            this.btnagregarfamilia.Text = "Agregar >>";
            this.btnagregarfamilia.UseVisualStyleBackColor = true;
            this.btnagregarfamilia.Click += new System.EventHandler(this.btnagregarfamilia_Click);
            // 
            // cboFamilias
            // 
            this.cboFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilias.FormattingEnabled = true;
            this.cboFamilias.Location = new System.Drawing.Point(11, 194);
            this.cboFamilias.Margin = new System.Windows.Forms.Padding(2);
            this.cboFamilias.Name = "cboFamilias";
            this.cboFamilias.Size = new System.Drawing.Size(234, 21);
            this.cboFamilias.TabIndex = 12;
            // 
            // lblagrefam
            // 
            this.lblagrefam.AutoSize = true;
            this.lblagrefam.Location = new System.Drawing.Point(9, 178);
            this.lblagrefam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblagrefam.Name = "lblagrefam";
            this.lblagrefam.Size = new System.Drawing.Size(84, 13);
            this.lblagrefam.TabIndex = 11;
            this.lblagrefam.Tag = "agregar familia";
            this.lblagrefam.Text = "Agregar Familias";
            // 
            // btnagregarpatente
            // 
            this.btnagregarpatente.Location = new System.Drawing.Point(11, 151);
            this.btnagregarpatente.Margin = new System.Windows.Forms.Padding(2);
            this.btnagregarpatente.Name = "btnagregarpatente";
            this.btnagregarpatente.Size = new System.Drawing.Size(83, 25);
            this.btnagregarpatente.TabIndex = 10;
            this.btnagregarpatente.Tag = "agregar patente";
            this.btnagregarpatente.Text = "Agregar >>";
            this.btnagregarpatente.UseVisualStyleBackColor = true;
            this.btnagregarpatente.Click += new System.EventHandler(this.btnagregarpatente_Click);
            // 
            // cboPatentes
            // 
            this.cboPatentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatentes.FormattingEnabled = true;
            this.cboPatentes.Location = new System.Drawing.Point(11, 127);
            this.cboPatentes.Margin = new System.Windows.Forms.Padding(2);
            this.cboPatentes.Name = "cboPatentes";
            this.cboPatentes.Size = new System.Drawing.Size(234, 21);
            this.cboPatentes.TabIndex = 9;
            // 
            // lblagrepat
            // 
            this.lblagrepat.AutoSize = true;
            this.lblagrepat.Location = new System.Drawing.Point(9, 110);
            this.lblagrepat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblagrepat.Name = "lblagrepat";
            this.lblagrepat.Size = new System.Drawing.Size(88, 13);
            this.lblagrepat.TabIndex = 8;
            this.lblagrepat.Tag = "agregar patentes";
            this.lblagrepat.Text = "Agregar patentes";
            // 
            // cmdConfigurar
            // 
            this.cmdConfigurar.Location = new System.Drawing.Point(11, 63);
            this.cmdConfigurar.Margin = new System.Windows.Forms.Padding(2);
            this.cmdConfigurar.Name = "cmdConfigurar";
            this.cmdConfigurar.Size = new System.Drawing.Size(86, 27);
            this.cmdConfigurar.TabIndex = 7;
            this.cmdConfigurar.Tag = "configurar usuario";
            this.cmdConfigurar.Text = "Configurar";
            this.cmdConfigurar.UseVisualStyleBackColor = true;
            this.cmdConfigurar.Click += new System.EventHandler(this.cmdConfigurar_Click);
            // 
            // lblallusers
            // 
            this.lblallusers.AutoSize = true;
            this.lblallusers.Location = new System.Drawing.Point(9, 23);
            this.lblallusers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblallusers.Name = "lblallusers";
            this.lblallusers.Size = new System.Drawing.Size(95, 13);
            this.lblallusers.TabIndex = 5;
            this.lblallusers.Tag = "todos los usuarios";
            this.lblallusers.Text = "Todos los usuarios";
            // 
            // FrmSegUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 310);
            this.Controls.Add(this.cmdGuardar);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.grpPatentes);
            this.Name = "FrmSegUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSegUsuarios_FormClosing);
            this.Load += new System.EventHandler(this.FrmSegUsuarios_Load);
            this.grpPatentes.ResumeLayout(false);
            this.grpPatentes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGuardar;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox grpPatentes;
        private System.Windows.Forms.Button btnagregarfamilia;
        private System.Windows.Forms.ComboBox cboFamilias;
        private System.Windows.Forms.Label lblagrefam;
        private System.Windows.Forms.Button btnagregarpatente;
        private System.Windows.Forms.ComboBox cboPatentes;
        private System.Windows.Forms.Label lblagrepat;
        private System.Windows.Forms.Button cmdConfigurar;
        private System.Windows.Forms.Label lblallusers;
        private System.Windows.Forms.ComboBox cmbusers;
    }
}