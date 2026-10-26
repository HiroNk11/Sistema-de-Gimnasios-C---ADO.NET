namespace CapaIU
{
    partial class FrmCambiarContraseña
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
            this.grpcc = new System.Windows.Forms.GroupBox();
            this.btncancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtconactual = new System.Windows.Forms.TextBox();
            this.txtconnueva = new System.Windows.Forms.TextBox();
            this.txtconfir = new System.Windows.Forms.TextBox();
            this.grpcc.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpcc
            // 
            this.grpcc.Controls.Add(this.btncancelar);
            this.grpcc.Controls.Add(this.label3);
            this.grpcc.Controls.Add(this.label2);
            this.grpcc.Controls.Add(this.label1);
            this.grpcc.Controls.Add(this.btnAceptar);
            this.grpcc.Controls.Add(this.txtconactual);
            this.grpcc.Controls.Add(this.txtconnueva);
            this.grpcc.Controls.Add(this.txtconfir);
            this.grpcc.Location = new System.Drawing.Point(12, 12);
            this.grpcc.Name = "grpcc";
            this.grpcc.Size = new System.Drawing.Size(223, 210);
            this.grpcc.TabIndex = 0;
            this.grpcc.TabStop = false;
            this.grpcc.Tag = "CambiarContraseña ";
            this.grpcc.Text = "CAMBIAR CONTRASEÑA";
            // 
            // btncancelar
            // 
            this.btncancelar.Location = new System.Drawing.Point(124, 181);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(75, 23);
            this.btncancelar.TabIndex = 8;
            this.btncancelar.Tag = "cancelar";
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 7;
            this.label3.Tag = "confirmar_contraseña";
            this.label3.Text = "Confirmar Nueva Contraseña :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 6;
            this.label2.Tag = "contraseña_nueva";
            this.label2.Text = "Contraseña Nueva :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 5;
            this.label1.Tag = "contraseña_actual";
            this.label1.Text = "Contraseña Actual :";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(19, 181);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Tag = "aceptar";
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtconactual
            // 
            this.txtconactual.Location = new System.Drawing.Point(19, 47);
            this.txtconactual.Name = "txtconactual";
            this.txtconactual.PasswordChar = '*';
            this.txtconactual.Size = new System.Drawing.Size(180, 20);
            this.txtconactual.TabIndex = 1;
            // 
            // txtconnueva
            // 
            this.txtconnueva.Location = new System.Drawing.Point(19, 92);
            this.txtconnueva.Name = "txtconnueva";
            this.txtconnueva.PasswordChar = '*';
            this.txtconnueva.Size = new System.Drawing.Size(180, 20);
            this.txtconnueva.TabIndex = 2;
            // 
            // txtconfir
            // 
            this.txtconfir.Location = new System.Drawing.Point(19, 141);
            this.txtconfir.Name = "txtconfir";
            this.txtconfir.PasswordChar = '*';
            this.txtconfir.Size = new System.Drawing.Size(180, 20);
            this.txtconfir.TabIndex = 3;
            // 
            // FrmCambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 234);
            this.Controls.Add(this.grpcc);
            this.Name = "FrmCambiarContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCambiarContraseña_FormClosed);
            this.Load += new System.EventHandler(this.FrmCambiarContraseña_Load);
            this.grpcc.ResumeLayout(false);
            this.grpcc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpcc;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtconactual;
        private System.Windows.Forms.TextBox txtconnueva;
        private System.Windows.Forms.TextBox txtconfir;
    }
}