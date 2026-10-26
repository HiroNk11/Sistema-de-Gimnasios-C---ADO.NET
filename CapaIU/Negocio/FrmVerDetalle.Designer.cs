namespace CapaIU.Negocio
{
    partial class FrmVerDetalle
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
            this.btnfiltrar = new System.Windows.Forms.Button();
            this.GrillaClase = new System.Windows.Forms.DataGridView();
            this.cmbclase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaClase)).BeginInit();
            this.SuspendLayout();
            // 
            // btnfiltrar
            // 
            this.btnfiltrar.Location = new System.Drawing.Point(814, 10);
            this.btnfiltrar.Name = "btnfiltrar";
            this.btnfiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnfiltrar.TabIndex = 43;
            this.btnfiltrar.Text = "Filtrar";
            this.btnfiltrar.UseVisualStyleBackColor = true;
            this.btnfiltrar.Click += new System.EventHandler(this.btnfiltrar_Click);
            // 
            // GrillaClase
            // 
            this.GrillaClase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaClase.Location = new System.Drawing.Point(12, 39);
            this.GrillaClase.Name = "GrillaClase";
            this.GrillaClase.Size = new System.Drawing.Size(877, 242);
            this.GrillaClase.TabIndex = 42;
            // 
            // cmbclase
            // 
            this.cmbclase.FormattingEnabled = true;
            this.cmbclase.Location = new System.Drawing.Point(107, 12);
            this.cmbclase.Name = "cmbclase";
            this.cmbclase.Size = new System.Drawing.Size(701, 21);
            this.cmbclase.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Seleccionar Clase";
            // 
            // FrmVerDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 306);
            this.Controls.Add(this.btnfiltrar);
            this.Controls.Add(this.GrillaClase);
            this.Controls.Add(this.cmbclase);
            this.Controls.Add(this.label1);
            this.Name = "FrmVerDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.GrillaClase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnfiltrar;
        private System.Windows.Forms.DataGridView GrillaClase;
        private System.Windows.Forms.ComboBox cmbclase;
        private System.Windows.Forms.Label label1;
    }
}