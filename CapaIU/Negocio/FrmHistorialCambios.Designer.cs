namespace CapaIU.Negocio
{
    partial class FrmHistorialCambios
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
            this.grillacambios = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnborar = new System.Windows.Forms.Button();
            this.lblidsocio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnrestaurar = new System.Windows.Forms.Button();
            this.txtfn = new System.Windows.Forms.TextBox();
            this.txtapellido = new CapaIU.Controles.ControlSoloLetras();
            this.txtnombre = new CapaIU.Controles.ControlSoloLetras();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grillacambios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grillacambios
            // 
            this.grillacambios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillacambios.Location = new System.Drawing.Point(6, 19);
            this.grillacambios.Name = "grillacambios";
            this.grillacambios.Size = new System.Drawing.Size(793, 214);
            this.grillacambios.TabIndex = 0;
            this.grillacambios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnborar);
            this.groupBox1.Controls.Add(this.lblidsocio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.grillacambios);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 271);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Historial de Cambios";
            // 
            // btnborar
            // 
            this.btnborar.Location = new System.Drawing.Point(585, 239);
            this.btnborar.Name = "btnborar";
            this.btnborar.Size = new System.Drawing.Size(214, 23);
            this.btnborar.TabIndex = 15;
            this.btnborar.Text = "Borrar Historial de Cambios";
            this.btnborar.UseVisualStyleBackColor = true;
            this.btnborar.Click += new System.EventHandler(this.btnborar_Click);
            // 
            // lblidsocio
            // 
            this.lblidsocio.AutoSize = true;
            this.lblidsocio.Location = new System.Drawing.Point(55, 236);
            this.lblidsocio.Name = "lblidsocio";
            this.lblidsocio.Size = new System.Drawing.Size(22, 13);
            this.lblidsocio.TabIndex = 14;
            this.lblidsocio.Text = ".....";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 13;
            this.label2.Tag = "id socio";
            this.label2.Text = "IdSocio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 15;
            this.label3.Tag = "fecha nacimiento ";
            this.label3.Text = "Fecha Nacimiento :";
            // 
            // btnrestaurar
            // 
            this.btnrestaurar.Location = new System.Drawing.Point(12, 166);
            this.btnrestaurar.Name = "btnrestaurar";
            this.btnrestaurar.Size = new System.Drawing.Size(255, 23);
            this.btnrestaurar.TabIndex = 18;
            this.btnrestaurar.Text = "Restaurar Valores";
            this.btnrestaurar.UseVisualStyleBackColor = true;
            this.btnrestaurar.Click += new System.EventHandler(this.btnrestaurar_Click);
            // 
            // txtfn
            // 
            this.txtfn.Location = new System.Drawing.Point(12, 130);
            this.txtfn.Name = "txtfn";
            this.txtfn.Size = new System.Drawing.Size(252, 20);
            this.txtfn.TabIndex = 19;
            // 
            // txtapellido
            // 
            this.txtapellido.Etiqueta = "Apellido";
            this.txtapellido.Location = new System.Drawing.Point(9, 68);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(255, 43);
            this.txtapellido.TabIndex = 17;
            this.txtapellido.Tag = "apellido socio";
            this.txtapellido.Texto = "";
            // 
            // txtnombre
            // 
            this.txtnombre.Etiqueta = "Nombre";
            this.txtnombre.Location = new System.Drawing.Point(9, 19);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(255, 43);
            this.txtnombre.TabIndex = 16;
            this.txtnombre.Tag = "nombre socio";
            this.txtnombre.Texto = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtnombre);
            this.groupBox2.Controls.Add(this.btnrestaurar);
            this.groupBox2.Controls.Add(this.txtfn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtapellido);
            this.groupBox2.Location = new System.Drawing.Point(12, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(805, 195);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Restaurar Valores";
            // 
            // FrmHistorialCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 508);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmHistorialCambios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHistorialCambios";
            ((System.ComponentModel.ISupportInitialize)(this.grillacambios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grillacambios;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblidsocio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnborar;
        private Controles.ControlSoloLetras txtapellido;
        private Controles.ControlSoloLetras txtnombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnrestaurar;
        private System.Windows.Forms.TextBox txtfn;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}