using System;
using System.Drawing;
using System.Windows.Forms;
using CapaIU.Controles;
namespace CapaIU
{
    public class FrmInicio : Form
    {
        internal FrmInicio(Action socios,Action operacion,bool negocio):this(socios,operacion,negocio,null){}
        internal FrmInicio(Action socios,Action operacion,bool negocio,Action altaUsuarios)
        {
            Text="Inicio";Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;FormBorderStyle=FormBorderStyle.None;ClientSize=new Size(1080,680);AutoScaleMode=AutoScaleMode.Font;
            var stack=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=4,Padding=new Padding(36)};
            stack.RowStyles.Add(new RowStyle(SizeType.Absolute,80));stack.RowStyles.Add(new RowStyle(SizeType.Absolute,145));stack.RowStyles.Add(new RowStyle(SizeType.Absolute,205));stack.RowStyles.Add(new RowStyle(SizeType.Percent,100));Controls.Add(stack);
            var head=new Panel {Dock=DockStyle.Fill};var title=TemaVisual.Etiqueta("Todo listo para una buena jornada.",24,true);head.Controls.Add(title);var date=TemaVisual.Etiqueta(DateTime.Today.ToString("dddd d 'de' MMMM"),11);date.ForeColor=TemaVisual.Tenue;date.Location=new Point(1,49);head.Controls.Add(date);stack.Controls.Add(head,0,0);
            var hero=new Panel {Dock=DockStyle.Fill,BackColor=TemaVisual.Oscuro,Padding=new Padding(24),Margin=new Padding(0,10,0,16)};
            var h=TemaVisual.Etiqueta(!negocio && altaUsuarios!=null?"Administración de cuentas":"La recepción, más simple.",21,true);h.ForeColor=Color.White;h.Location=new Point(24,24);hero.Controls.Add(h);
            var t=TemaVisual.Etiqueta(!negocio && altaUsuarios!=null?"Tu perfil permite dar de alta usuarios con sus roles definidos.":"Gestioná personas, membresías y clases con un recorrido claro.",11);t.ForeColor=Color.FromArgb(178,203,220);t.Location=new Point(25,75);hero.Controls.Add(t);stack.Controls.Add(hero,0,1);
            var cards=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=2,RowCount=1};cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));stack.Controls.Add(cards,0,2);
            if(!negocio && altaUsuarios!=null){var abrir=TemaVisual.Boton("Crear usuario",true);abrir.Margin=new Padding(20);abrir.Width=230;abrir.Click+=(s,e)=>altaUsuarios();cards.Controls.Add(abrir);return;}
            var nombres=new[]{"Socios","Operación diaria"};var descripciones=new[]{"Encontrá una ficha o registrá a una persona nueva.\nActualizá sus datos desde un único panel.","Consultá vencimientos, registrá pagos y organizá\nlas reservas y la asistencia de cada sesión."};
            for(int i=0;i<2;i++)
            {
                int indice=i;var card=new Panel {Dock=DockStyle.Fill,BackColor=Color.White,Margin=new Padding(0,0,16,0),Padding=new Padding(22)};
                var l=TemaVisual.Etiqueta(nombres[i],15,true);l.Location=new Point(22,20);card.Controls.Add(l);var desc=TemaVisual.Etiqueta(descripciones[i],10);desc.ForeColor=TemaVisual.Tenue;desc.Location=new Point(22,57);card.Controls.Add(desc);
                var b=TemaVisual.Boton("Abrir "+nombres[i].ToLowerInvariant(),true);b.Location=new Point(22,125);b.Width=220;b.Enabled=negocio;b.Click+=(s,e)=>{if(indice==0)socios();else operacion();};card.Controls.Add(b);cards.Controls.Add(card,i,0);
            }
            var pie=TemaVisual.Etiqueta("Accedé a las demás herramientas desde el menú superior. Las opciones disponibles dependen de tu perfil.",9);pie.ForeColor=TemaVisual.Tenue;pie.Margin=new Padding(0,25,0,0);stack.Controls.Add(pie,0,3);
        }
    }
}
