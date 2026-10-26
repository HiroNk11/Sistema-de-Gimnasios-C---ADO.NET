using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CapaIU.Controles
{
    public abstract class PantallaGestion : Form
    {
        protected readonly Panel Cuerpo=new Panel {Dock=DockStyle.Fill,Padding=new Padding(20)};
        readonly Label estado=new Label {Dock=DockStyle.Bottom,Height=46,Padding=new Padding(20,6,20,6),TextAlign=ContentAlignment.MiddleLeft};
        bool ocupado;
        protected PantallaGestion(){Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;ClientSize=new Size(1140,740);MinimumSize=new Size(1000,700);Controls.Add(Cuerpo);Controls.Add(estado);FormClosing+=(s,e)=>{if(ocupado && e.CloseReason==CloseReason.UserClosing){e.Cancel=true;Aviso("Esperá a que termine la operación antes de cerrar.",true);}};}
        protected void Aviso(string texto,bool error=false){estado.Text=texto;estado.ForeColor=error?TemaVisual.Rojo:TemaVisual.Primario;}
        protected async Task Ejecutar(Func<Task> accion)
        {
            if(ocupado)return;ocupado=true;Cuerpo.Enabled=false;UseWaitCursor=true;
            try{await accion();}
            catch(ArgumentException e){Aviso(e.Message,true);}
            catch(InvalidOperationException e){Aviso(e.Message.Split('\n')[0],true);MessageBox.Show(this,e.Message,"Revisar operación",MessageBoxButtons.OK,MessageBoxIcon.Information);}
            catch(UnauthorizedAccessException ex){Aviso(ex.Message,true);}
            catch(Exception e){Aviso("No se pudo completar la operación. Revisá el aviso de error.",true);Program.Error(e);}
            finally{ocupado=false;Cuerpo.Enabled=true;UseWaitCursor=false;}
        }
        protected Panel Cabecera(string titulo,string ayuda){var p=new Panel {Dock=DockStyle.Top,Height=86};p.Controls.Add(TemaVisual.Etiqueta(titulo,22,true));var l=TemaVisual.Etiqueta(ayuda,10);l.Top=44;p.Controls.Add(l);return p;}
    }
}
