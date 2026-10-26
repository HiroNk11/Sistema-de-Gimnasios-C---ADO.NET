using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDominio;
using CapaIU.Presentacion;
namespace CapaIU { internal static class Program { public static void Error(Exception e){throw new Exception("Error inesperado",e);} } }
class ServicioFalso : IServicioEntrenadores
{
 public List<Entrenador> Datos=new List<Entrenador>{new Entrenador{Id_Entrenador=1,Nombre="Alex",Apellido="Ejemplo"},new Entrenador{Id_Entrenador=2,Nombre="Sam",Apellido="Muestra"}};
 public int Escrituras; public Entrenador Ultimo; public bool Nuevo;
 public List<Entrenador> Listar(){return Datos.ToList();}
 public void Guardar(Entrenador e,bool nuevo){Escrituras++;Ultimo=e;Nuevo=nuevo;}
 public void Eliminar(Entrenador e){Escrituras++;Ultimo=e;}
}
class PruebasPatrones
{
 static int n;
 static void Check(bool ok,string mensaje){if(!ok)throw new Exception(mensaje);Console.WriteLine("OK: "+mensaje);n++;}
 static void Rechaza(Action a,string mensaje){try{a();}catch(ArgumentException){Check(true,mensaje);return;}throw new Exception(mensaje);}
 static T Campo<T>(object x,string nombre){return (T)x.GetType().GetField(nombre,BindingFlags.Instance|BindingFlags.NonPublic).GetValue(x);}
 [STAThread] static int Main(string[] args)
 {
  try{
   Application.EnableVisualStyles();var f=new ServicioFalso();var p=new EntrenadoresPresentador(f);p.Cargar().GetAwaiter().GetResult();
   Check(p.Buscar(" SAM ").Count==1,"búsqueda sin distinción de mayúsculas y con espacios");Check(p.Buscar("2").Count==1,"búsqueda por número");Check(p.Buscar(null).Count==2,"búsqueda vacía");
   Rechaza(()=>p.Guardar(null," ","Muestra"),"rechaza nombre vacío");Rechaza(()=>p.Guardar(null,"Alex"," "),"rechaza apellido vacío");Rechaza(()=>p.Guardar(null,new string('a',51),"Muestra"),"rechaza longitud excesiva");Check(f.Escrituras==0,"validación evita escrituras inválidas");
   p.Guardar(null," Alex "," Ejemplo ").GetAwaiter().GetResult();Check(f.Nuevo && f.Ultimo.Nombre=="Alex","alta elimina espacios");p.Guardar(1,"Alex","Ejemplo").GetAwaiter().GetResult();Check(!f.Nuevo && f.Ultimo.Id_Entrenador==1,"edición conserva el identificador");
   Rechaza(()=>p.Eliminar(null),"no elimina sin selección");p.Eliminar(2).GetAwaiter().GetResult();Check(f.Ultimo.Id_Entrenador==2,"elimina solamente el identificador elegido");
   using(var principal=new Form{IsMdiContainer=true}){var nav=new NavegadorMdi(principal);var a=nav.Abrir(()=>new Form());var b=nav.Abrir(()=>new Form());Check(ReferenceEquals(a,b)&&principal.MdiChildren.Length==1,"navegación reutiliza ventana");a.Close();var c=nav.Abrir(()=>new Form());Check(!ReferenceEquals(a,c),"ventana cerrada se puede volver a abrir");}
   using(var form=new CapaIU.Negocio.FrmEntrenadores(new ServicioFalso(),true)){
    form.StartPosition=FormStartPosition.Manual;form.Location=new Point(-25000,-25000);form.ShowInTaskbar=false;form.Show();
    var hasta=DateTime.UtcNow.AddSeconds(5);var grid=Campo<DataGridView>(form,"grilla");while(grid.Rows.Count<2 && DateTime.UtcNow<hasta){Application.DoEvents();System.Threading.Thread.Sleep(10);}
    Check(grid.Rows.Count==2,"formulario carga servicio falso sin SQL");Check(!Campo<Button>(form,"eliminar").Enabled,"eliminación deshabilitada sin selección");
    Campo<TextBox>(form,"buscar").Text="Sam";Application.DoEvents();Check(grid.Rows.Count==1,"filtro visual de entrenadores");Campo<TextBox>(form,"buscar").Clear();Application.DoEvents();
    Directory.CreateDirectory(args[0]);using(var bmp=new Bitmap(form.Width,form.Height)){form.DrawToBitmap(bmp,new Rectangle(0,0,form.Width,form.Height));bmp.Save(Path.Combine(args[0],"entrenadores.png"));}
    form.Size=new Size(900,600);Application.DoEvents();Check(Campo<Button>(form,"guardar").Visible,"guardado disponible en tamaño compacto");
   }
   Console.WriteLine(n+" comprobaciones correctas. Datos ficticios; sin conexión a SQL.");return 0;
  }catch(Exception e){Console.WriteLine(e);return 1;}
 }
}
