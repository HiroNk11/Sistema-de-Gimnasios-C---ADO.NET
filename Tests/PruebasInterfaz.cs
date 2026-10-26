using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
class PreviewUi
{
 static Assembly app;
 static int tests;
 static void Check(bool value,string text){if(!value)throw new Exception(text);tests++;Console.WriteLine("OK: "+text);}
 static Form Crear(string tipo)
 {
  var t=app.GetType(tipo,true);
  return (Form)t.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(bool)},null).Invoke(new object[]{true});
 }
 static T Campo<T>(object obj,string name){return (T)obj.GetType().GetField(name,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(obj);}
 static void Capturar(Form f,string name,string dir)
 {
  Application.DoEvents();f.PerformLayout();
  using(var bmp=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(bmp,new Rectangle(0,0,f.Width,f.Height));bmp.Save(Path.Combine(dir,name+".png"));}
  Console.WriteLine("Imagen: "+name+" "+f.Width+"x"+f.Height);
 }
 static void Preparar(Form f){f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-25000,-25000);f.ShowInTaskbar=false;f.Show();Application.DoEvents();}
 [STAThread]static int Main(string[] args)
 {
  try
  {
   Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
   string exe=args[0],dir=args[1];Directory.CreateDirectory(dir);
   AppDomain.CurrentDomain.AssemblyResolve+=(s,e)=>{string p=Path.Combine(Path.GetDirectoryName(exe),new AssemblyName(e.Name).Name+".dll");return File.Exists(p)?Assembly.LoadFrom(p):null;};
   Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING","Data Source=servidor-inexistente;Initial Catalog=SoloVistaPrevia;Integrated Security=True;Connect Timeout=1");
   app=Assembly.LoadFrom(exe);
   using(var login=Crear("LogIn.FrmLogin"))
   {
    Preparar(login);var user=Campo<TextBox>(login,"txtusuario");var pass=Campo<TextBox>(login,"txtcontraseña");
    Check(user.Text=="" && pass.Text=="","login sin marcadores que se confundan con credenciales");Check(pass.UseSystemPasswordChar,"contraseña oculta por defecto");
    Check(login.AcceptButton==Campo<Button>(login,"btnacceder"),"Enter ejecuta ingreso");
    Campo<Button>(login,"btnacceder").PerformClick();Check(Campo<Label>(login,"lblerrormessage").Visible,"campos vacíos muestran validación local sin consultar SQL");Campo<Label>(login,"lblerrormessage").Visible=false;
    Capturar(login,"01_acceso",dir);
   }
   using(var daily=Crear("CapaIU.Negocio.FrmOperacionDiaria"))
   {
    Preparar(daily);Check(!Campo<Button>(daily,"cobrar").Enabled,"cobro deshabilitado sin selección y en vista previa");Check(!Campo<Button>(daily,"cancelarSesion").Enabled,"cancelación deshabilitada sin sesión elegida");
    Check(Campo<TabControl>(daily,"tabs").TabPages.Count==5,"cinco secciones operativas");Capturar(daily,"02_resumen",dir);
    daily.GetType().GetMethod("AbrirSeccion").Invoke(daily,new object[]{1});Application.DoEvents();Check(Campo<DataGridView>(daily,"membresias").SelectedRows.Count==0,"no se elige automáticamente una membresía al abrir la sección");Capturar(daily,"03_membresias",dir);
    var g=Campo<DataGridView>(daily,"membresias");g.CurrentCell=g.Rows[0].Cells["Socio"];g.Rows[0].Selected=true;Application.DoEvents();
    Check(Campo<TabControl>(daily,"editorMembresias").SelectedIndex==1,"seleccionar membresía abre panel de cobro");Check(Campo<Label>(daily,"cuota").Text.Contains("Alex"),"panel de cobro identifica al socio");Capturar(daily,"04_cobro",dir);
    daily.GetType().GetMethod("AbrirSeccion").Invoke(daily,new object[]{2});Campo<TabControl>(daily,"editorSesiones").SelectedIndex=1;Application.DoEvents();Capturar(daily,"05_agenda",dir);
    daily.GetType().GetMethod("AbrirSeccion").Invoke(daily,new object[]{3});Application.DoEvents();Capturar(daily,"06_asistencia",dir);
    daily.ClientSize=new Size(940,660);daily.GetType().GetMethod("AbrirSeccion").Invoke(daily,new object[]{1});Application.DoEvents();Capturar(daily,"07_pantalla_compacta",dir);Check(Campo<Button>(daily,"cobrar").Parent.Dock==DockStyle.Bottom,"acción de cobro fija y accesible en ventana compacta");
   }
   using(var socios=Crear("CapaIU.Negocio.FrmSocios")){Preparar(socios);Check(!Campo<DateTimePicker>(socios,"nacimiento").Checked,"nuevo socio exige elegir fecha de nacimiento, sin edad asumida");Check(Campo<DataGridView>(socios,"grid").Rows.Count==3,"socios de muestra sin acceso a SQL");Campo<TextBox>(socios,"buscar").Text="Sam";Application.DoEvents();Check(Campo<DataGridView>(socios,"grid").Rows.Count==1,"filtro de socios por nombre");Campo<TextBox>(socios,"buscar").Clear();Application.DoEvents();Capturar(socios,"08_socios",dir);}
   var homeType=app.GetType("CapaIU.FrmInicio");using(var home=(Form)homeType.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(Action),typeof(Action),typeof(bool)},null).Invoke(new object[]{new Action(()=>{}),new Action(()=>{}),true})){Preparar(home);Capturar(home,"09_inicio",dir);}
   using(var main=Crear("CapaIU.FrmMdiPrincipal")){main.WindowState=FormWindowState.Normal;main.ClientSize=new Size(1280,780);Preparar(main);Check(main.MdiChildren.Length==1,"pantalla principal abre inicio sin ventanas duplicadas");Check(main.ActiveMdiChild!=null && main.ActiveMdiChild.GetType().Name=="FrmInicio","navegación principal presenta inicio");Capturar(main,"10_navegacion",dir);}
   Console.WriteLine("PRUEBAS UI: "+tests+" aprobadas. SQL no utilizado.");return 0;
  }
  catch(Exception e){Console.WriteLine(e);return 1;}
 }
}
