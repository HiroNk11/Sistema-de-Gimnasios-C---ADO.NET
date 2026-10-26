namespace CapaIU
{
    partial class FrmRecuperarContraseña
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
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.btnenviar = new System.Windows.Forms.Button();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.IngreseCorreo = new System.Windows.Forms.Label();
            this.btningresar = new System.Windows.Forms.Button();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            this.lblcodigo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtconnueva = new System.Windows.Forms.TextBox();
            this.txtconfir = new System.Windows.Forms.TextBox();
            this.btnaceptar = new System.Windows.Forms.Button();
            this.gboucambiarcontraseña = new System.Windows.Forms.GroupBox();
            this.gboucambiarcontraseña.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(20, 38);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(259, 20);
            this.txtcorreo.TabIndex = 7;
            // 
            // btnenviar
            // 
            this.btnenviar.Location = new System.Drawing.Point(204, 61);
            this.btnenviar.Name = "btnenviar";
            this.btnenviar.Size = new System.Drawing.Size(75, 23);
            this.btnenviar.TabIndex = 6;
            this.btnenviar.Text = "Enviar";
            this.btnenviar.UseVisualStyleBackColor = true;
            this.btnenviar.Click += new System.EventHandler(this.btnenviar_Click);
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.Location = new System.Drawing.Point(26, 87);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(43, 13);
            this.lblmensaje.TabIndex = 5;
            this.lblmensaje.Text = "............";
            // 
            // IngreseCorreo
            // 
            this.IngreseCorreo.AutoSize = true;
            this.IngreseCorreo.Location = new System.Drawing.Point(17, 19);
            this.IngreseCorreo.Name = "IngreseCorreo";
            this.IngreseCorreo.Size = new System.Drawing.Size(180, 13);
            this.IngreseCorreo.TabIndex = 4;
            this.IngreseCorreo.Text = "Ingrese Usuario o Correo Electronico";
            // 
            // btningresar
            // 
            this.btningresar.Location = new System.Drawing.Point(204, 177);
            this.btningresar.Name = "btningresar";
            this.btningresar.Size = new System.Drawing.Size(75, 23);
            this.btningresar.TabIndex = 8;
            this.btningresar.Text = "Ingresar";
            this.btningresar.UseVisualStyleBackColor = true;
            this.btningresar.Visible = false;
            this.btningresar.Click += new System.EventHandler(this.btningresar_Click);
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(20, 151);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Size = new System.Drawing.Size(259, 20);
            this.txtcodigo.TabIndex = 9;
            this.txtcodigo.Visible = false;
            // 
            // lblcodigo
            // 
            this.lblcodigo.AutoSize = true;
            this.lblcodigo.Location = new System.Drawing.Point(17, 135);
            this.lblcodigo.Name = "lblcodigo";
            this.lblcodigo.Size = new System.Drawing.Size(198, 13);
            this.lblcodigo.TabIndex = 10;
            this.lblcodigo.Text = "Ingrese Codigo que enviamos por correo";
            this.lblcodigo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 15;
            this.label3.Tag = "confirmar_contraseña";
            this.label3.Text = "Confirmar Nueva Contraseña ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 14;
            this.label2.Tag = "contraseña_nueva";
            this.label2.Text = "Contraseña Nueva ";
            // 
            // txtconnueva
            // 
            this.txtconnueva.Location = new System.Drawing.Point(9, 35);
            this.txtconnueva.Name = "txtconnueva";
            this.txtconnueva.PasswordChar = '*';
            this.txtconnueva.Size = new System.Drawing.Size(325, 20);
            this.txtconnueva.TabIndex = 11;
            // 
            // txtconfir
            // 
            this.txtconfir.Location = new System.Drawing.Point(9, 84);
            this.txtconfir.Name = "txtconfir";
            this.txtconfir.PasswordChar = '*';
            this.txtconfir.Size = new System.Drawing.Size(325, 20);
            this.txtconfir.TabIndex = 12;
            // 
            // btnaceptar
            // 
            this.btnaceptar.Location = new System.Drawing.Point(259, 114);
            this.btnaceptar.Name = "btnaceptar";
            this.btnaceptar.Size = new System.Drawing.Size(75, 23);
            this.btnaceptar.TabIndex = 16;
            this.btnaceptar.Text = "Aceptar";
            this.btnaceptar.UseVisualStyleBackColor = true;
            this.btnaceptar.Click += new System.EventHandler(this.btnaceptar_Click);
            // 
            // gboucambiarcontraseña
            // 
            this.gboucambiarcontraseña.Controls.Add(this.label2);
            this.gboucambiarcontraseña.Controls.Add(this.txtconnueva);
            this.gboucambiarcontraseña.Controls.Add(this.btnaceptar);
            this.gboucambiarcontraseña.Controls.Add(this.label3);
            this.gboucambiarcontraseña.Controls.Add(this.txtconfir);
            this.gboucambiarcontraseña.Location = new System.Drawing.Point(313, 19);
            this.gboucambiarcontraseña.Name = "gboucambiarcontraseña";
            this.gboucambiarcontraseña.Size = new System.Drawing.Size(355, 152);
            this.gboucambiarcontraseña.TabIndex = 18;
            this.gboucambiarcontraseña.TabStop = false;
            this.gboucambiarcontraseña.Text = "CAMBIAR CONTRASEÑA";
            this.gboucambiarcontraseña.Visible = false;
            // 
            // FrmRecuperarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 215);
            this.Controls.Add(this.gboucambiarcontraseña);
            this.Controls.Add(this.lblcodigo);
            this.Controls.Add(this.txtcodigo);
            this.Controls.Add(this.btningresar);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.btnenviar);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.IngreseCorreo);
            this.Name = "FrmRecuperarContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperar Contraseña";
            this.gboucambiarcontraseña.ResumeLayout(false);
            this.gboucambiarcontraseña.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.Button btnenviar;
        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.Label IngreseCorreo;
        private System.Windows.Forms.Button btningresar;
        private System.Windows.Forms.TextBox txtcodigo;
        private System.Windows.Forms.Label lblcodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtconnueva;
        private System.Windows.Forms.TextBox txtconfir;
        private System.Windows.Forms.Button btnaceptar;
        private System.Windows.Forms.GroupBox gboucambiarcontraseña;
    }
}