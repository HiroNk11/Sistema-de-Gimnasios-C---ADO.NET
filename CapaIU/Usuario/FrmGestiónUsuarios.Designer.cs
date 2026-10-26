namespace CapaIU
{
    partial class FrmGestiónUsuarios
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
            this.GrillaUsuarios = new System.Windows.Forms.DataGridView();
            this.btnAlta = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBaja = new System.Windows.Forms.Button();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblidusuario = new System.Windows.Forms.Label();
            this.lbltextid = new System.Windows.Forms.Label();
            this.txtnombreusuario = new CapaIU.Controles.ControlLetrasyNums();
            this.txtapellido = new CapaIU.MiControl();
            this.txtnombre = new CapaIU.MiControl();
            this.txtcorreo = new CapaIU.Controles.ControlCorreo();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // GrillaUsuarios
            // 
            this.GrillaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaUsuarios.Location = new System.Drawing.Point(288, 39);
            this.GrillaUsuarios.Name = "GrillaUsuarios";
            this.GrillaUsuarios.ReadOnly = true;
            this.GrillaUsuarios.Size = new System.Drawing.Size(569, 174);
            this.GrillaUsuarios.TabIndex = 10;
            this.GrillaUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaUsuarios_CellClick);
            // 
            // btnAlta
            // 
            this.btnAlta.Location = new System.Drawing.Point(863, 39);
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(75, 23);
            this.btnAlta.TabIndex = 3;
            this.btnAlta.Tag = "alta usuario";
            this.btnAlta.Text = "ALTA";
            this.btnAlta.UseVisualStyleBackColor = true;
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(863, 107);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.Tag = "modificar usuario";
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnBaja
            // 
            this.btnBaja.Location = new System.Drawing.Point(863, 73);
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.Size = new System.Drawing.Size(75, 23);
            this.btnBaja.TabIndex = 7;
            this.btnBaja.Tag = "baja usuario";
            this.btnBaja.Text = "BAJA";
            this.btnBaja.UseVisualStyleBackColor = true;
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            // 
            // txtbuscar
            // 
            this.txtbuscar.Location = new System.Drawing.Point(338, 12);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(519, 20);
            this.txtbuscar.TabIndex = 23;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 22;
            this.label5.Tag = "buscar usuario";
            this.label5.Text = "Buscar ";
            // 
            // lblidusuario
            // 
            this.lblidusuario.AutoSize = true;
            this.lblidusuario.Location = new System.Drawing.Point(335, 216);
            this.lblidusuario.Name = "lblidusuario";
            this.lblidusuario.Size = new System.Drawing.Size(58, 13);
            this.lblidusuario.TabIndex = 25;
            this.lblidusuario.Text = ".................";
            // 
            // lbltextid
            // 
            this.lbltextid.AutoSize = true;
            this.lbltextid.Location = new System.Drawing.Point(285, 217);
            this.lbltextid.Name = "lbltextid";
            this.lbltextid.Size = new System.Drawing.Size(52, 13);
            this.lbltextid.TabIndex = 24;
            this.lbltextid.Tag = "id usuario";
            this.lbltextid.Text = "IdUsuario";
            // 
            // txtnombreusuario
            // 
            this.txtnombreusuario.Etiqueta = "Nombre de Usuario";
            this.txtnombreusuario.Location = new System.Drawing.Point(12, 26);
            this.txtnombreusuario.Name = "txtnombreusuario";
            this.txtnombreusuario.Size = new System.Drawing.Size(255, 43);
            this.txtnombreusuario.TabIndex = 21;
            this.txtnombreusuario.Tag = "nombre de usuario";
            this.txtnombreusuario.Texto = "";
            // 
            // txtapellido
            // 
            this.txtapellido.Etiqueta = "Apellido";
            this.txtapellido.Location = new System.Drawing.Point(12, 123);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(255, 43);
            this.txtapellido.TabIndex = 20;
            this.txtapellido.Tag = "apellido:";
            this.txtapellido.Texto = "";
            // 
            // txtnombre
            // 
            this.txtnombre.Etiqueta = "Nombre";
            this.txtnombre.Location = new System.Drawing.Point(10, 74);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(255, 43);
            this.txtnombre.TabIndex = 19;
            this.txtnombre.Tag = "nombre:";
            this.txtnombre.Texto = "";
            // 
            // txtcorreo
            // 
            this.txtcorreo.Etiqueta = "Email";
            this.txtcorreo.Location = new System.Drawing.Point(12, 172);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(270, 54);
            this.txtcorreo.TabIndex = 17;
            this.txtcorreo.Tag = "email";
            this.txtcorreo.Texto = "";
            // 
            // FrmGestiónUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 239);
            this.Controls.Add(this.lblidusuario);
            this.Controls.Add(this.lbltextid);
            this.Controls.Add(this.txtbuscar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtnombreusuario);
            this.Controls.Add(this.txtapellido);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.btnBaja);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAlta);
            this.Controls.Add(this.GrillaUsuarios);
            this.Name = "FrmGestiónUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "formulario gestion usuarios";
            this.Text = "Gestión Usuarios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmGestiónUsuarios_FormClosed);
            this.Load += new System.EventHandler(this.FrmGestiónUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrillaUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView GrillaUsuarios;
        private System.Windows.Forms.Button btnAlta;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBaja;
        private Controles.ControlCorreo txtcorreo;
        private MiControl txtnombre;
        private MiControl txtapellido;
        private Controles.ControlLetrasyNums txtnombreusuario;
        private System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblidusuario;
        private System.Windows.Forms.Label lbltextid;
    }
}