namespace CapaIU.Negocio
{
    partial class FrmEntrenamiento
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblidentrenamiento = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GrillaEntrenamiento = new System.Windows.Forms.DataGridView();
            this.rEGIST = new System.Windows.Forms.GroupBox();
            this.btnmodificar = new System.Windows.Forms.Button();
            this.btnagregar = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            this.txtNombre = new CapaIU.Controles.ControlSoloLetras();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaEntrenamiento)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblidentrenamiento);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtbuscar);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.GrillaEntrenamiento);
            this.groupBox2.Location = new System.Drawing.Point(273, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(566, 340);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Entrenamientos";
            // 
            // lblidentrenamiento
            // 
            this.lblidentrenamiento.AutoSize = true;
            this.lblidentrenamiento.Location = new System.Drawing.Point(96, 317);
            this.lblidentrenamiento.Name = "lblidentrenamiento";
            this.lblidentrenamiento.Size = new System.Drawing.Size(22, 13);
            this.lblidentrenamiento.TabIndex = 14;
            this.lblidentrenamiento.Text = ".....";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "IdEntrenamiento";
            // 
            // txtbuscar
            // 
            this.txtbuscar.Location = new System.Drawing.Point(56, 19);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(499, 20);
            this.txtbuscar.TabIndex = 17;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Buscar :";
            // 
            // GrillaEntrenamiento
            // 
            this.GrillaEntrenamiento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaEntrenamiento.Location = new System.Drawing.Point(7, 45);
            this.GrillaEntrenamiento.Name = "GrillaEntrenamiento";
            this.GrillaEntrenamiento.Size = new System.Drawing.Size(548, 269);
            this.GrillaEntrenamiento.TabIndex = 0;
            this.GrillaEntrenamiento.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaEntrenamiento_CellClick);
            // 
            // rEGIST
            // 
            this.rEGIST.Location = new System.Drawing.Point(12, 3);
            this.rEGIST.Name = "rEGIST";
            this.rEGIST.Size = new System.Drawing.Size(255, 22);
            this.rEGIST.TabIndex = 5;
            this.rEGIST.TabStop = false;
            this.rEGIST.Text = "REGISTRO DE ENTRENAMIENTOS";
            // 
            // btnmodificar
            // 
            this.btnmodificar.Location = new System.Drawing.Point(753, 349);
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(75, 23);
            this.btnmodificar.TabIndex = 20;
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.UseVisualStyleBackColor = true;
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(591, 349);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(75, 23);
            this.btnagregar.TabIndex = 19;
            this.btnagregar.Text = "Agregar";
            this.btnagregar.UseVisualStyleBackColor = true;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(672, 349);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(75, 23);
            this.btneliminar.TabIndex = 18;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Etiqueta = "NOMBRE";
            this.txtNombre.Location = new System.Drawing.Point(12, 31);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(255, 43);
            this.txtNombre.TabIndex = 4;
            this.txtNombre.Texto = "";
            // 
            // FrmEntrenamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 378);
            this.Controls.Add(this.rEGIST);
            this.Controls.Add(this.btnmodificar);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnagregar);
            this.Controls.Add(this.btneliminar);
            this.Name = "FrmEntrenamiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrenamiento";
            this.Load += new System.EventHandler(this.FrmEntrenamiento_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaEntrenamiento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView GrillaEntrenamiento;
        private Controles.ControlSoloLetras txtNombre;
        private System.Windows.Forms.GroupBox rEGIST;
        private System.Windows.Forms.Button btnmodificar;
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblidentrenamiento;
        private System.Windows.Forms.Label label2;
    }
}