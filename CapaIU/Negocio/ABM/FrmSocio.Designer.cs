namespace CapaIU.Negocio
{
    partial class FrmSocio
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
            this.btnserializar = new System.Windows.Forms.Button();
            this.btnver = new System.Windows.Forms.Button();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.lblidsocio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnmodificar = new System.Windows.Forms.Button();
            this.btnagregar = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            this.grillasocios = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dtfn = new System.Windows.Forms.DateTimePicker();
            this.rEGIST = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtapellido = new CapaIU.Controles.ControlSoloLetras();
            this.txtnombre = new CapaIU.Controles.ControlSoloLetras();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.btnxml = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grillasocios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnxml);
            this.groupBox2.Controls.Add(this.btnserializar);
            this.groupBox2.Controls.Add(this.btnver);
            this.groupBox2.Controls.Add(this.txtbuscar);
            this.groupBox2.Controls.Add(this.lblidsocio);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnmodificar);
            this.groupBox2.Controls.Add(this.btnagregar);
            this.groupBox2.Controls.Add(this.btneliminar);
            this.groupBox2.Controls.Add(this.grillasocios);
            this.groupBox2.Location = new System.Drawing.Point(273, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 327);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "lista socios";
            this.groupBox2.Text = "Listado de Socios";
            // 
            // btnserializar
            // 
            this.btnserializar.Location = new System.Drawing.Point(167, 46);
            this.btnserializar.Name = "btnserializar";
            this.btnserializar.Size = new System.Drawing.Size(163, 23);
            this.btnserializar.TabIndex = 16;
            this.btnserializar.Text = "Serializar";
            this.btnserializar.UseVisualStyleBackColor = true;
            this.btnserializar.Click += new System.EventHandler(this.btnserializar_Click);
            // 
            // btnver
            // 
            this.btnver.Location = new System.Drawing.Point(336, 46);
            this.btnver.Name = "btnver";
            this.btnver.Size = new System.Drawing.Size(194, 23);
            this.btnver.TabIndex = 15;
            this.btnver.Text = "Ver historial de Cambios";
            this.btnver.UseVisualStyleBackColor = true;
            this.btnver.Click += new System.EventHandler(this.btnver_Click);
            // 
            // txtbuscar
            // 
            this.txtbuscar.Location = new System.Drawing.Point(70, 20);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(461, 20);
            this.txtbuscar.TabIndex = 15;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);
            // 
            // lblidsocio
            // 
            this.lblidsocio.AutoSize = true;
            this.lblidsocio.Location = new System.Drawing.Point(73, 293);
            this.lblidsocio.Name = "lblidsocio";
            this.lblidsocio.Size = new System.Drawing.Size(22, 13);
            this.lblidsocio.TabIndex = 12;
            this.lblidsocio.Text = ".....";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Tag = "id socio";
            this.label2.Text = "IdSocio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 10;
            this.label5.Tag = "buscar socios";
            this.label5.Text = "Buscar :";
            // 
            // btnmodificar
            // 
            this.btnmodificar.Location = new System.Drawing.Point(457, 293);
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(75, 23);
            this.btnmodificar.TabIndex = 5;
            this.btnmodificar.Tag = "modificar socio";
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.UseVisualStyleBackColor = true;
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(295, 293);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(75, 23);
            this.btnagregar.TabIndex = 2;
            this.btnagregar.Tag = "agregar socio";
            this.btnagregar.Text = "Agregar";
            this.btnagregar.UseVisualStyleBackColor = true;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(376, 293);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(75, 23);
            this.btneliminar.TabIndex = 1;
            this.btneliminar.Tag = "eliminar socio";
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // grillasocios
            // 
            this.grillasocios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillasocios.Location = new System.Drawing.Point(27, 76);
            this.grillasocios.Name = "grillasocios";
            this.grillasocios.ReadOnly = true;
            this.grillasocios.Size = new System.Drawing.Size(504, 211);
            this.grillasocios.TabIndex = 0;
            this.grillasocios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grillasocios_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 11;
            this.label3.Tag = "fecha nacimiento ";
            this.label3.Text = "Fecha Nacimiento :";
            // 
            // dtfn
            // 
            this.dtfn.Location = new System.Drawing.Point(15, 150);
            this.dtfn.Name = "dtfn";
            this.dtfn.Size = new System.Drawing.Size(252, 20);
            this.dtfn.TabIndex = 10;
            // 
            // rEGIST
            // 
            this.rEGIST.Location = new System.Drawing.Point(12, 12);
            this.rEGIST.Name = "rEGIST";
            this.rEGIST.Size = new System.Drawing.Size(255, 22);
            this.rEGIST.TabIndex = 14;
            this.rEGIST.TabStop = false;
            this.rEGIST.Tag = "registro socios";
            this.rEGIST.Text = "REGISTRO DE SOCIOS";
            // 
            // txtapellido
            // 
            this.txtapellido.Etiqueta = "Apellido";
            this.txtapellido.Location = new System.Drawing.Point(12, 88);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(255, 43);
            this.txtapellido.TabIndex = 13;
            this.txtapellido.Tag = "apellido socio";
            this.txtapellido.Texto = "";
            // 
            // txtnombre
            // 
            this.txtnombre.Etiqueta = "Nombre";
            this.txtnombre.Location = new System.Drawing.Point(12, 39);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(255, 43);
            this.txtnombre.TabIndex = 12;
            this.txtnombre.Tag = "nombre socio";
            this.txtnombre.Texto = "";
            // 
            // btnxml
            // 
            this.btnxml.Location = new System.Drawing.Point(27, 47);
            this.btnxml.Name = "btnxml";
            this.btnxml.Size = new System.Drawing.Size(134, 23);
            this.btnxml.TabIndex = 17;
            this.btnxml.Text = "XML";
            this.btnxml.UseVisualStyleBackColor = true;
            this.btnxml.Click += new System.EventHandler(this.btnxml_Click);
            // 
            // FrmSocio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 351);
            this.Controls.Add(this.rEGIST);
            this.Controls.Add(this.txtapellido);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtfn);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmSocio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSocio_FormClosed);
            this.Load += new System.EventHandler(this.FrmSocio_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grillasocios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnmodificar;
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.DataGridView grillasocios;
        private System.Windows.Forms.Label label5;
        private Controles.ControlSoloLetras txtapellido;
        private Controles.ControlSoloLetras txtnombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtfn;
        private System.Windows.Forms.Label lblidsocio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.GroupBox rEGIST;
        private System.Windows.Forms.Button btnver;
        private System.Windows.Forms.Button btnserializar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnxml;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}