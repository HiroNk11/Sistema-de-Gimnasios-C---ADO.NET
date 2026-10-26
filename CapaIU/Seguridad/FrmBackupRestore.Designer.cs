namespace CapaIU
{
    partial class FrmBackupRestore
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnbackup = new System.Windows.Forms.Button();
            this.txtruta = new System.Windows.Forms.TextBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnrestore = new System.Windows.Forms.Button();
            this.txtruta2 = new System.Windows.Forms.TextBox();
            this.btnbrowserestore = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Realizar Backup";
            // 
            // btnbackup
            // 
            this.btnbackup.Location = new System.Drawing.Point(115, 67);
            this.btnbackup.Name = "btnbackup";
            this.btnbackup.Size = new System.Drawing.Size(138, 23);
            this.btnbackup.TabIndex = 9;
            this.btnbackup.Text = "Backup";
            this.btnbackup.UseVisualStyleBackColor = true;
            this.btnbackup.Click += new System.EventHandler(this.btnbackup_Click);
            // 
            // txtruta
            // 
            this.txtruta.Enabled = false;
            this.txtruta.Location = new System.Drawing.Point(6, 41);
            this.txtruta.Name = "txtruta";
            this.txtruta.Size = new System.Drawing.Size(247, 20);
            this.txtruta.TabIndex = 8;
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(259, 41);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(138, 20);
            this.browse_button.TabIndex = 7;
            this.browse_button.Text = "Browse Backup";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.browse_button);
            this.groupBox1.Controls.Add(this.btnbackup);
            this.groupBox1.Controls.Add(this.txtruta);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 118);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BACKUP";
            // 
            // btnrestore
            // 
            this.btnrestore.Location = new System.Drawing.Point(115, 70);
            this.btnrestore.Name = "btnrestore";
            this.btnrestore.Size = new System.Drawing.Size(138, 23);
            this.btnrestore.TabIndex = 14;
            this.btnrestore.Text = "Restore";
            this.btnrestore.UseVisualStyleBackColor = true;
            this.btnrestore.Click += new System.EventHandler(this.btnrestore_Click);
            // 
            // txtruta2
            // 
            this.txtruta2.Enabled = false;
            this.txtruta2.Location = new System.Drawing.Point(9, 44);
            this.txtruta2.Name = "txtruta2";
            this.txtruta2.Size = new System.Drawing.Size(244, 20);
            this.txtruta2.TabIndex = 13;
            // 
            // btnbrowserestore
            // 
            this.btnbrowserestore.Location = new System.Drawing.Point(259, 44);
            this.btnbrowserestore.Name = "btnbrowserestore";
            this.btnbrowserestore.Size = new System.Drawing.Size(138, 20);
            this.btnbrowserestore.TabIndex = 12;
            this.btnbrowserestore.Text = "Browse Restore";
            this.btnbrowserestore.UseVisualStyleBackColor = true;
            this.btnbrowserestore.Click += new System.EventHandler(this.btnbrowserestore_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtruta2);
            this.groupBox2.Controls.Add(this.btnbrowserestore);
            this.groupBox2.Controls.Add(this.btnrestore);
            this.groupBox2.Location = new System.Drawing.Point(12, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 118);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RESTORE";
            // 
            // FrmBackupRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 270);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBackupRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnbackup;
        private System.Windows.Forms.TextBox txtruta;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnrestore;
        private System.Windows.Forms.TextBox txtruta2;
        private System.Windows.Forms.Button btnbrowserestore;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}