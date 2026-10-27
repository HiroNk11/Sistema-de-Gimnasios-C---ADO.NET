using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaIU.Controles;
namespace CapaIU
{
    internal sealed class FrmActivarClave : Form
    {
        public FrmActivarClave()
        {
            Text="Cambiar clave inicial";Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;ClientSize=new Size(530,410);StartPosition=FormStartPosition.CenterParent;FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;MinimizeBox=false;
            var panel=new TableLayoutPanel {Dock=DockStyle.Fill,Padding=new Padding(24),ColumnCount=1};Controls.Add(panel);panel.Controls.Add(TemaVisual.Etiqueta("Elegí tu contraseña personal",18,true));
            var actual=TemaVisual.Texto(128);var nueva=TemaVisual.Texto(128);var repetir=TemaVisual.Texto(128);foreach(var t in new[]{actual,nueva,repetir})t.UseSystemPasswordChar=true;
            panel.Controls.Add(TemaVisual.Campo("Clave inicial recibida",actual));panel.Controls.Add(TemaVisual.Campo("Nueva clave de 15 a 128 caracteres",nueva));panel.Controls.Add(TemaVisual.Campo("Repetir nueva clave",repetir));var estado=TemaVisual.Etiqueta("");estado.MaximumSize=new Size(470,0);panel.Controls.Add(estado);
            var guardar=TemaVisual.Boton("Guardar y continuar",true);panel.Controls.Add(guardar);AcceptButton=guardar;
            guardar.Click+=async(s,e)=>{panel.Enabled=false;try{string a=actual.Text,n=nueva.Text,c=repetir.Text;await Task.Run(()=>new UsuarioBLL().CambiarPassword(a,n,c));DialogResult=DialogResult.OK;Close();}catch(ArgumentException ex){estado.Text=ex.Message;estado.ForeColor=TemaVisual.Rojo;}catch(Exception ex){Program.Error(ex);}finally{if(!IsDisposed)panel.Enabled=true;}};
        }
    }
}
