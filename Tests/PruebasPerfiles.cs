using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CapaAplicacion;
using CapaAplicacion.Negocio;
using CapaDominio;
using CapaServicios.Composite;
class PruebasPerfiles
{
 static int n;
 static void Check(bool v,string m){if(!v)throw new Exception(m);n++;Console.WriteLine("OK: "+m);}
 static void Rechaza(Action a,string m){try{a();}catch(ArgumentException){Check(true,m);return;}throw new Exception(m);}
 static void Deniega(Action a,string m){try{a();}catch(UnauthorizedAccessException){Check(true,m);return;}throw new Exception(m);}
 static Usuario Usuario(){return new Usuario{usuario="empleado.demo",nombre="Alex",apellido="Ejemplo",email="alex@example.invalid"};}
 static T Campo<T>(object x,string nombre){return (T)x.GetType().GetField(nombre,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(x);}
 static System.Collections.Generic.IEnumerable<Control> Todos(Control c){foreach(Control hijo in c.Controls){yield return hijo;foreach(var n in Todos(hijo))yield return n;}}
 [STAThread]static int Main(string[] args){try{
  Check((int)TipoPermiso.PuedeAccederANegocio==7 && (int)TipoPermiso.PuedeAccederRestore==13,"valores históricos de permisos preservados");Check((int)TipoPermiso.PuedeCrearUsuarios==14,"permiso de alta agregado al final");
  CuentasBLL.Validar(Usuario(),PerfilCuenta.Recepcion,"ClavePrueba1234","ClavePrueba1234");Check(true,"alta válida de recepción");CuentasBLL.Validar(Usuario(),PerfilCuenta.AdministracionDeCuentas,"ClavePrueba1234","ClavePrueba1234");Check(true,"alta válida de administración");
  Rechaza(()=>CuentasBLL.Validar(null,PerfilCuenta.Recepcion,"ClavePrueba1234","ClavePrueba1234"),"rechaza cuenta vacía");var u=Usuario();u.usuario=" ";Rechaza(()=>CuentasBLL.Validar(u,PerfilCuenta.Recepcion,"ClavePrueba1234","ClavePrueba1234"),"rechaza usuario vacío");
  u=Usuario();u.email="sin-correo";Rechaza(()=>CuentasBLL.Validar(u,PerfilCuenta.Recepcion,"ClavePrueba1234","ClavePrueba1234"),"rechaza correo inválido");
  Rechaza(()=>CuentasBLL.Validar(Usuario(),(PerfilCuenta)123,"ClavePrueba1234","ClavePrueba1234"),"rechaza perfiles fuera de la lista");Rechaza(()=>CuentasBLL.Validar(Usuario(),PerfilCuenta.Recepcion,"corta","corta"),"rechaza clave corta");Rechaza(()=>CuentasBLL.Validar(Usuario(),PerfilCuenta.Recepcion,"ClavePrueba1234","OtraClave12345"),"rechaza confirmación diferente");
  Deniega(()=>new CuentasBLL().Crear(Usuario(),PerfilCuenta.Recepcion,"ClavePrueba1234","ClavePrueba1234"),"alta denegada sin sesión antes de consultar SQL");Deniega(()=>new SocioBLL().Listar(),"consulta de socios denegada sin sesión");Deniega(()=>new ActividadesBLL().Cargar(),"consulta de actividades denegada sin sesión");Deniega(()=>new SocioBLL().Borrar(new Socio {Id_Socio=1}),"eliminación de socio denegada sin sesión");
  Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);var app=Assembly.LoadFrom(args[0]);Directory.CreateDirectory(args[1]);
  using(var f=(Form)app.GetType("CapaIU.FrmAltaUsuarios",true).GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(bool)},null).Invoke(new object[]{true})){
   f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-25000,-25000);f.ShowInTaskbar=false;f.Show();Application.DoEvents();Check(Campo<ComboBox>(f,"perfil").Items.Count==2,"solo dos perfiles predefinidos");Check(Campo<ComboBox>(f,"perfil").SelectedIndex==-1,"perfil requiere selección explícita");Check(Campo<TextBox>(f,"clave").UseSystemPasswordChar && Campo<TextBox>(f,"confirmar").UseSystemPasswordChar,"claves iniciales ocultas");Check(!Campo<Button>(f,"crear").Enabled,"vista previa no escribe usuarios");
   var ocupado=f.GetType().BaseType.GetField("ocupado",BindingFlags.NonPublic|BindingFlags.Instance);ocupado.SetValue(f,true);f.Close();Check(!f.IsDisposed,"no permite cerrar una operación pendiente para cambiar de usuario");ocupado.SetValue(f,false);
   using(var bmp=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(bmp,new Rectangle(0,0,f.Width,f.Height));bmp.Save(Path.Combine(args[1],"alta_usuarios.png"));}
  }
  var constructor=app.GetType("CapaIU.FrmInicio",true).GetConstructors(BindingFlags.NonPublic|BindingFlags.Instance).Single(x=>x.GetParameters().Length==4);int altas=0;
  using(var inicio=(Form)constructor.Invoke(new object[]{(Action)(()=>{}),(Action)(()=>{}),false,(Action)(()=>altas++)})){
   inicio.StartPosition=FormStartPosition.Manual;inicio.Location=new Point(-25000,-25000);inicio.ShowInTaskbar=false;inicio.Show();Application.DoEvents();var botones=Todos(inicio).OfType<Button>().ToList();Check(botones.Count==1 && botones[0].Text=="Crear usuario","inicio administrativo no ofrece operación del gimnasio");botones[0].PerformClick();Check(altas==1,"inicio administrativo abre el alta");
  }
  Console.WriteLine(n+" pruebas aprobadas. No se comprobaron permisos ni transacciones contra la base real.");return 0;
 }catch(Exception e){Console.WriteLine(e);return 1;}}
}
