using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CapaDominio;
using CapaAplicacion;
class PruebasPlanificacion
{
 static int tests;
 static void Check(bool ok,string text){if(!ok)throw new Exception(text);tests++;Console.WriteLine("OK: "+text);}
 static void Rechaza(Action a,string text){try{a();}catch(ArgumentException){Check(true,text);return;}throw new Exception(text);}
 static T Campo<T>(object obj,string name){return (T)obj.GetType().GetField(name,BindingFlags.Instance|BindingFlags.NonPublic).GetValue(obj);}
 static Form Crear(Assembly app,string tipo){return (Form)app.GetType(tipo,true).GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(bool)},null).Invoke(new object[]{true});}
 static void Capturar(Form f,string destino,string nombre){Application.DoEvents();using(var bmp=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(bmp,new Rectangle(0,0,f.Width,f.Height));bmp.Save(Path.Combine(destino,nombre+".png"));}}
 [STAThread]static int Main(string[] args)
 {
  try{
   var ahora=new DateTime(2026,1,1);var lunes=new DateTime(2026,1,5);var h=TimeSpan.FromHours(18);var fin=TimeSpan.FromHours(19);
   var sesiones=Planificador.Generar(lunes,lunes.AddDays(27),h,fin,new[]{DayOfWeek.Monday,DayOfWeek.Wednesday},ahora);
   Check(sesiones.Count==8,"lunes y miércoles en cuatro semanas generan ocho sesiones");Check(sesiones[0].Inicio==lunes.Add(h),"primera fecha y hora correctas");Check(sesiones.All(x=>x.Fin-x.Inicio==TimeSpan.FromHours(1)),"duración estable");Check(sesiones.All(x=>x.Inicio.DayOfWeek==DayOfWeek.Monday || x.Inicio.DayOfWeek==DayOfWeek.Wednesday),"solo días elegidos");
   Check(Planificador.Generar(lunes,lunes,h,fin,new[]{DayOfWeek.Monday,DayOfWeek.Monday},ahora).Count==1,"días duplicados no duplican sesiones");
   Rechaza(()=>Planificador.Generar(lunes,lunes,h,fin,new DayOfWeek[0],ahora),"requiere días");Rechaza(()=>Planificador.Generar(lunes,lunes.AddDays(-1),h,fin,new[]{DayOfWeek.Monday},ahora),"rechaza período invertido");Rechaza(()=>Planificador.Generar(lunes,lunes.AddDays(367),h,fin,new[]{DayOfWeek.Monday},ahora),"limita período");Rechaza(()=>Planificador.Generar(lunes,lunes,fin,h,new[]{DayOfWeek.Monday},ahora),"rechaza fin anterior");Rechaza(()=>Planificador.Generar(lunes,lunes,h,h,new[]{DayOfWeek.Monday},ahora),"rechaza duración cero");Rechaza(()=>Planificador.Generar(lunes,lunes,h,fin,new[]{DayOfWeek.Tuesday},ahora),"rechaza período sin coincidencias");Rechaza(()=>Planificador.Generar(lunes,lunes,h,fin,new[]{DayOfWeek.Monday},lunes.AddHours(20)),"rechaza sesiones pasadas");
   Check(!Planificador.SeSuperponen(lunes.AddHours(8),lunes.AddHours(9),lunes.AddHours(9),lunes.AddHours(10)),"horarios contiguos permitidos");Check(Planificador.SeSuperponen(lunes.AddHours(8),lunes.AddHours(10),lunes.AddHours(9),lunes.AddHours(11)),"detecta solapamiento parcial");Check(Planificador.SeSuperponen(lunes.AddHours(8),lunes.AddHours(12),lunes.AddHours(9),lunes.AddHours(10)),"detecta intervalo contenido");
   PlanificacionBLL.Validar(1,sesiones,20,ahora);Check(true,"valida lote correcto");Rechaza(()=>PlanificacionBLL.Validar(0,sesiones,20,ahora),"requiere actividad");Rechaza(()=>PlanificacionBLL.Validar(1,sesiones,0,ahora),"requiere cupo positivo");Rechaza(()=>PlanificacionBLL.Validar(1,new[]{sesiones[0],sesiones[0]},20,ahora),"rechaza lote duplicado");
   ActividadesBLL.Validar(new Clase{Descripcion=" Funcional ",Id_Entrenador=1,Id_Entrenamiento=1,Id_piso=1,Capacidad=20});Check(true,"valida actividad por identificadores");Rechaza(()=>ActividadesBLL.Validar(new Clase()),"rechaza ficha incompleta");
   Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);var app=Assembly.LoadFrom(args[0]);Directory.CreateDirectory(args[1]);
   using(var p=Crear(app,"CapaIU.Negocio.FrmPlanificacion")){
    p.StartPosition=FormStartPosition.Manual;p.Location=new Point(-25000,-25000);p.ShowInTaskbar=false;p.Show();Application.DoEvents();
    Check(Campo<DataGridView>(p,"propuesta").Rows.Count>0,"vista previa genera sesiones sin SQL");Check(!Campo<Button>(p,"guardar").Enabled,"vista previa no permite guardar");Check(Campo<TabControl>(p,"tabs").TabPages.Count==2,"separa propuesta de agenda guardada");Capturar(p,args[1],"planificacion");
    Campo<DateTimePicker>(p,"hasta").Value=Campo<DateTimePicker>(p,"hasta").Value.AddDays(1);Check(Campo<DataGridView>(p,"propuesta").Rows.Count==0,"cambiar período invalida propuesta anterior");
    p.Size=new Size(1000,700);Capturar(p,args[1],"planificacion_compacta");
   }
   using(var f=Crear(app,"CapaIU.Negocio.FrmActividades")){
    f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-25000,-25000);f.ShowInTaskbar=false;f.Show();Application.DoEvents();Check(Campo<DataGridView>(f,"grilla").Rows.Count==1,"catálogo carga datos ficticios");Check(!Campo<Button>(f,"guardar").Enabled,"catálogo de prueba sin escrituras");Capturar(f,args[1],"actividades");Campo<TextBox>(f,"buscar").Text="inexistente";Check(Campo<DataGridView>(f,"grilla").Rows.Count==0,"filtro del catálogo");
   }
   Console.WriteLine(tests+" pruebas aprobadas. SQL real no utilizado.");return 0;
  }catch(Exception e){Console.WriteLine(e);return 1;}
 }
}
