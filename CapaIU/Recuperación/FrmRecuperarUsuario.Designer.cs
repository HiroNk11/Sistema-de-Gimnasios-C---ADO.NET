namespace CapaIU
{
    partial class FrmRecuperarUsuario
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
            this.SuspendLayout();
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(12, 30);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(259, 20);
            this.txtcorreo.TabIndex = 11;
            // 
            // btnenviar
            // 
            this.btnenviar.Location = new System.Drawing.Point(196, 53);
            this.btnenviar.Name = "btnenviar";
            this.btnenviar.Size = new System.Drawing.Size(75, 23);
            this.btnenviar.TabIndex = 10;
            this.btnenviar.Text = "Enviar";
            this.btnenviar.UseVisualStyleBackColor = true;
            this.btnenviar.Click += new System.EventHandler(this.btnenviar_Click);
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.Location = new System.Drawing.Point(277, 37);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(43, 13);
            this.lblmensaje.TabIndex = 9;
            this.lblmensaje.Text = "............";
            // 
            // IngreseCorreo
            // 
            this.IngreseCorreo.AutoSize = true;
            this.IngreseCorreo.Location = new System.Drawing.Point(9, 11);
            this.IngreseCorreo.Name = "IngreseCorreo";
            this.IngreseCorreo.Size = new System.Drawing.Size(148, 13);
            this.IngreseCorreo.TabIndex = 8;
            this.IngreseCorreo.Text = "Ingrese Su Correo Electronico";
            // 
            // FrmRecuperarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 137);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.btnenviar);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.IngreseCorreo);
            this.Name = "FrmRecuperarUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperar Usuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.Button btnenviar;
        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.Label IngreseCorreo;
    }
}