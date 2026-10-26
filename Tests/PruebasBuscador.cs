using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CapaIU.Controles;
class Persona {public int Id {get;set;} public string Nombre {get;set;}}
class PruebasBuscador
{
 static int n;
 static void Check(bool ok,string s){if(!ok)throw new Exception(s);n++;Console.WriteLine("OK: "+s);}
 static T Campo<T>(object o,string s){return (T)o.GetType().GetField(s,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(o);}
 [STAThread]static int Main(string[] args){try{
  Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
  using(var host=new Form())using(var combo=TemaVisual.Lista()){
   host.Controls.Add(combo);host.ShowInTaskbar=false;host.Location=new Point(-25000,-25000);host.Show();
   combo.DisplayMember="Nombre";combo.ValueMember="Id";var personas=Enumerable.Range(1,100).Select(i=>new Persona{Id=i,Nombre="Socio Ejemplo "+i}).ToList();personas[41].Nombre="María Gómez";personas[42].Nombre="María Gómez";combo.DataSource=personas;combo.SelectedIndex=-1;
   var opciones=SelectorBuscable.Opciones(combo);Check(opciones.Count==100,"lista de cien socios");Check(SelectorBuscable.Filtrar(opciones,"maria").Count==2,"búsqueda sin tildes");Check(SelectorBuscable.Filtrar(opciones,"GÓMEZ MARIA").Count==2,"palabras en cualquier orden y mayúsculas");
   var exacta=SelectorBuscable.Filtrar(opciones,"maria 42");Check(exacta.Count==1 && exacta[0].Indice==41,"homónimos distinguibles por número");Check(SelectorBuscable.Filtrar(opciones,"inexistente").Count==0,"sin coincidencias");Check(SelectorBuscable.Filtrar(opciones,"  ").Count==100,"búsqueda vacía muestra todos");Check(combo.Items.Count==100 && combo.SelectedIndex==-1,"filtrar no modifica origen ni selección");
   using(var dialogo=new DialogoBusqueda("socio",opciones)){
    dialogo.StartPosition=FormStartPosition.Manual;dialogo.Location=new Point(-25000,-25000);dialogo.ShowInTaskbar=false;dialogo.Show();Application.DoEvents();Check(!Campo<Button>(dialogo,"elegir").Enabled,"no confirma una fila automáticamente");
    Campo<TextBox>(dialogo,"buscar").Text="maria";Application.DoEvents();Check(Campo<ListBox>(dialogo,"resultados").Items.Count==2,"filtrado visual");Directory.CreateDirectory(args[0]);using(var bmp=new Bitmap(dialogo.Width,dialogo.Height)){dialogo.DrawToBitmap(bmp,new Rectangle(0,0,dialogo.Width,dialogo.Height));bmp.Save(Path.Combine(args[0],"buscador_socios.png"));}
    Campo<ListBox>(dialogo,"resultados").SelectedIndex=1;Campo<Button>(dialogo,"elegir").PerformClick();Check(dialogo.IndiceElegido==42,"resultado mantiene índice e identidad original");
   }
   int cambios=0;var boton=SelectorBuscable.Boton(combo,"socio", "Buscar",()=>cambios++);
   using(var timer=new Timer {Interval=50}){timer.Tick+=(s,e)=>{var ventana=Application.OpenForms.OfType<DialogoBusqueda>().FirstOrDefault();if(ventana==null)return;timer.Stop();Campo<TextBox>(ventana,"buscar").Text="maria 42";Campo<ListBox>(ventana,"resultados").SelectedIndex=0;Campo<Button>(ventana,"elegir").PerformClick();};timer.Start();boton.PerformClick();}
   Check(Convert.ToInt32(combo.SelectedValue)==42,"selección del buscador aplica el ID al combo original");Check(cambios==1,"selección notifica al filtro de operación diaria");
   using(var timer=new Timer {Interval=50}){timer.Tick+=(s,e)=>{var ventana=Application.OpenForms.OfType<DialogoBusqueda>().FirstOrDefault();if(ventana==null)return;timer.Stop();ventana.DialogResult=DialogResult.Cancel;ventana.Close();};timer.Start();boton.PerformClick();}
   Check(Convert.ToInt32(combo.SelectedValue)==42 && cambios==1,"cancelar preserva selección y no dispara consultas");combo.Enabled=false;Check(!boton.Enabled,"no permite buscar durante carga");combo.Enabled=true;Check(boton.Enabled,"búsqueda vuelve a habilitarse");boton.Dispose();
  }
  Console.WriteLine(n+" pruebas aprobadas. Sin SQL.");return 0;
 }catch(Exception e){Console.WriteLine(e);return 1;}}
}
