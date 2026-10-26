using System;
using System.Drawing;
using System.Windows.Forms;
using CapaIU.Controles;
namespace LogIn
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components=null;
        private Panel panel1;
        private TextBox txtusuario,txtcontraseña;
        private Label lbllogin,lblerrormessage;
        private Button btnacceder;
        private LinkLabel linkpass,linkusu;
        private PictureBox btnminimizar,btncerrar;
        protected override void Dispose(bool disposing){if(disposing && components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            SuspendLayout();
            Font=TemaVisual.Fuente;BackColor=Color.White;ForeColor=TemaVisual.Tinta;
            AutoScaleMode=AutoScaleMode.Font;ClientSize=new Size(900,550);FormBorderStyle=FormBorderStyle.FixedSingle;MaximizeBox=false;StartPosition=FormStartPosition.CenterScreen;Text="Body Sport Gym · Acceso";
            panel1=new Panel {Dock=DockStyle.Left,Width=310,BackColor=TemaVisual.Oscuro};
            var marca=new Label {Text="BODY\nSPORT",Font=new Font("Segoe UI",34,FontStyle.Bold),ForeColor=Color.White,Location=new Point(35,80),Size=new Size(240,140)};
            var linea=new Panel {Location=new Point(38,240),Size=new Size(55,5),BackColor=Color.FromArgb(61,199,167)};
            var claim=new Label {Text="Tu gimnasio,\nmejor organizado.",Font=new Font("Segoe UI",19),ForeColor=Color.White,Location=new Point(38,280),Size=new Size(245,90)};
            var pie=new Label {Text="SOCIOS  /  AGENDA  /  GESTIÓN",Font=new Font("Segoe UI",8),ForeColor=Color.FromArgb(157,183,204),Location=new Point(38,490),AutoSize=true};
            panel1.Controls.AddRange(new Control[]{marca,linea,claim,pie});Controls.Add(panel1);
            lbllogin=TemaVisual.Etiqueta("Bienvenido de nuevo",24,true);lbllogin.Location=new Point(358,64);Controls.Add(lbllogin);
            var intro=TemaVisual.Etiqueta("Ingresá con tu cuenta para comenzar.");intro.ForeColor=TemaVisual.Tenue;intro.Location=new Point(360,111);Controls.Add(intro);
            var lu=TemaVisual.Etiqueta("Usuario",10,true);lu.Location=new Point(360,174);Controls.Add(lu);
            txtusuario=TemaVisual.Texto(50);txtusuario.Location=new Point(360,201);txtusuario.Size=new Size(480,30);txtusuario.TabIndex=0;txtusuario.AccessibleName="Usuario";Controls.Add(txtusuario);
            var lp=TemaVisual.Etiqueta("Contraseña",10,true);lp.Location=new Point(360,253);Controls.Add(lp);
            txtcontraseña=TemaVisual.Texto(128);txtcontraseña.Location=new Point(360,280);txtcontraseña.Size=new Size(480,30);txtcontraseña.TabIndex=1;txtcontraseña.UseSystemPasswordChar=true;txtcontraseña.AccessibleName="Contraseña";Controls.Add(txtcontraseña);
            var mostrar=new CheckBox {Text="Mostrar contraseña",Location=new Point(360,320),AutoSize=true,ForeColor=TemaVisual.Tenue,TabIndex=2};mostrar.CheckedChanged+=(s,e)=>txtcontraseña.UseSystemPasswordChar=!mostrar.Checked;Controls.Add(mostrar);
            btnacceder=TemaVisual.Boton("Ingresar",true);btnacceder.Location=new Point(360,365);btnacceder.Size=new Size(480,43);btnacceder.TabIndex=3;btnacceder.Click+=btnacceder_Click;Controls.Add(btnacceder);AcceptButton=btnacceder;
            lblerrormessage=new Label {Location=new Point(360,415),Size=new Size(480,43),ForeColor=TemaVisual.Rojo,Font=new Font("Segoe UI",9),Visible=false};Controls.Add(lblerrormessage);
            linkpass=new LinkLabel {Text="Recuperar contraseña",Location=new Point(360,476),AutoSize=true,LinkColor=TemaVisual.Primario,TabIndex=4};linkpass.LinkClicked+=linkpass_LinkClicked;Controls.Add(linkpass);
            linkusu=new LinkLabel {Text="Olvidé mi usuario",Location=new Point(684,476),AutoSize=true,LinkColor=TemaVisual.Primario,TabIndex=5};linkusu.LinkClicked+=linkusu_LinkClicked;Controls.Add(linkusu);
            btnminimizar=new PictureBox {Visible=false};btncerrar=new PictureBox {Visible=false};
            FormClosed+=(s,e)=>{if(e.CloseReason==CloseReason.UserClosing)Application.Exit();};
            ResumeLayout(false);PerformLayout();Shown+=(s,e)=>txtusuario.Focus();
        }
    }
}
