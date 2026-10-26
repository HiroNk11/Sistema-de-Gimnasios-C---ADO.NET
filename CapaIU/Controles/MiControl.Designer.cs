namespace CapaIU
{
    partial class MiControl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbletiqueta = new System.Windows.Forms.Label();
            this.txttexto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbletiqueta
            // 
            this.lbletiqueta.AutoSize = true;
            this.lbletiqueta.Location = new System.Drawing.Point(0, 2);
            this.lbletiqueta.Name = "lbletiqueta";
            this.lbletiqueta.Size = new System.Drawing.Size(43, 13);
            this.lbletiqueta.TabIndex = 1;
            this.lbletiqueta.Text = "TEXTO";
            // 
            // txttexto
            // 
            this.txttexto.Location = new System.Drawing.Point(3, 18);
            this.txttexto.Name = "txttexto";
            this.txttexto.Size = new System.Drawing.Size(250, 20);
            this.txttexto.TabIndex = 5;
            // 
            // MiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txttexto);
            this.Controls.Add(this.lbletiqueta);
            this.Name = "MiControl";
            this.Size = new System.Drawing.Size(255, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbletiqueta;
        private System.Windows.Forms.TextBox txttexto;
    }
}
