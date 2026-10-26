namespace CapaIU.Seguridad
{
    partial class FrmGestionarIdioma
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
            this.btnagregar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.GrillaIdioma = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbidioma2 = new System.Windows.Forms.ComboBox();
            this.btnguardar = new System.Windows.Forms.Button();
            this.lble = new System.Windows.Forms.Label();
            this.txtetiqueta = new System.Windows.Forms.TextBox();
            this.txttraduccion = new CapaIU.Controles.ControlSoloLetras();
            this.cmbidioma = new CapaIU.Controles.control_combo_idioma();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaIdioma)).BeginInit();
            this.SuspendLayout();
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(609, 97);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(75, 23);
            this.btnagregar.TabIndex = 2;
            this.btnagregar.Tag = "agregar";
            this.btnagregar.Text = "Agregar";
            this.btnagregar.UseVisualStyleBackColor = true;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnbuscar);
            this.groupBox2.Controls.Add(this.GrillaIdioma);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbidioma2);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 286);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "gestionar idioma";
            this.groupBox2.Text = "Gestionar Idioma";
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(316, 44);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(75, 23);
            this.btnbuscar.TabIndex = 3;
            this.btnbuscar.Tag = "buscar";
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = true;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // GrillaIdioma
            // 
            this.GrillaIdioma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaIdioma.Location = new System.Drawing.Point(39, 73);
            this.GrillaIdioma.Name = "GrillaIdioma";
            this.GrillaIdioma.ReadOnly = true;
            this.GrillaIdioma.Size = new System.Drawing.Size(352, 200);
            this.GrillaIdioma.TabIndex = 2;
            this.GrillaIdioma.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaIdioma_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Tag = "seleccionar idioma";
            this.label2.Text = "Seleccionar Idioma";
            // 
            // cmbidioma2
            // 
            this.cmbidioma2.FormattingEnabled = true;
            this.cmbidioma2.Location = new System.Drawing.Point(39, 46);
            this.cmbidioma2.Name = "cmbidioma2";
            this.cmbidioma2.Size = new System.Drawing.Size(271, 21);
            this.cmbidioma2.TabIndex = 0;
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(563, 259);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(121, 23);
            this.btnguardar.TabIndex = 11;
            this.btnguardar.Tag = "guardar cambios";
            this.btnguardar.Text = "Guardar Cambios";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click_1);
            // 
            // lble
            // 
            this.lble.AutoSize = true;
            this.lble.Location = new System.Drawing.Point(429, 168);
            this.lble.Name = "lble";
            this.lble.Size = new System.Drawing.Size(117, 13);
            this.lble.TabIndex = 14;
            this.lble.Tag = "etiqueta idioma default";
            this.lble.Text = "Etiqueta Idioma Default";
            // 
            // txtetiqueta
            // 
            this.txtetiqueta.Enabled = false;
            this.txtetiqueta.Location = new System.Drawing.Point(432, 184);
            this.txtetiqueta.Name = "txtetiqueta";
            this.txtetiqueta.Size = new System.Drawing.Size(252, 20);
            this.txtetiqueta.TabIndex = 15;
            // 
            // txttraduccion
            // 
            this.txttraduccion.Enabled = false;
            this.txttraduccion.Etiqueta = "Traducción";
            this.txttraduccion.Location = new System.Drawing.Point(429, 210);
            this.txttraduccion.Name = "txttraduccion";
            this.txttraduccion.Size = new System.Drawing.Size(255, 43);
            this.txttraduccion.TabIndex = 13;
            this.txttraduccion.Tag = "traduccion";
            this.txttraduccion.Texto = "";
            // 
            // cmbidioma
            // 
            this.cmbidioma.Etiqueta = "Idioma ";
            this.cmbidioma.Location = new System.Drawing.Point(12, 2);
            this.cmbidioma.Name = "cmbidioma";
            this.cmbidioma.Size = new System.Drawing.Size(674, 89);
            this.cmbidioma.TabIndex = 16;
            this.cmbidioma.Tag = "Idioma ";
            this.cmbidioma.Texto = "";
            // 
            // FrmGestionarIdioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 411);
            this.Controls.Add(this.cmbidioma);
            this.Controls.Add(this.txtetiqueta);
            this.Controls.Add(this.lble);
            this.Controls.Add(this.txttraduccion);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.btnagregar);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmGestionarIdioma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmGestionarIdioma_FormClosed);
            this.Load += new System.EventHandler(this.FrmGestionarIdioma_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaIdioma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbidioma2;
        private System.Windows.Forms.DataGridView GrillaIdioma;
        private System.Windows.Forms.Button btnbuscar;
        private Controles.ControlSoloLetras txttraduccion;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Label lble;
        private System.Windows.Forms.TextBox txtetiqueta;
        private Controles.control_combo_idioma cmbidioma;
    }
}