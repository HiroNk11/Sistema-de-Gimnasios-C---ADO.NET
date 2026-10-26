using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CapaAplicacion;
class PruebasMonitoreo
{
    static int pruebas;
    static Assembly app;
    static void Check(bool ok,string texto){if(!ok)throw new Exception(texto);pruebas++;Console.WriteLine("OK: "+texto);}
    static T Campo<T>(object o,string nombre){return (T)o.GetType().GetField(nombre,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(o);}
    static object Valor(object o,string nombre){return o.GetType().GetField(nombre).GetValue(o);}
    static void Preparar(Form f){f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-25000,-25000);f.ShowInTaskbar=false;f.Show();Application.DoEvents();}
    static void Imagen(Form f,string nombre,string dir){Application.DoEvents();using(var bmp=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(bmp,new Rectangle(Point.Empty,f.Size));bmp.Save(Path.Combine(dir,nombre+".png"));}}
    static DataTable Tabla(){var t=new DataTable();t.Columns.Add("Id",typeof(int));t.Columns.Add("Inicio",typeof(DateTime));t.Columns.Add("Fin",typeof(DateTime));return t;}
    static object[] Distribuir(DataTable t,DateTime lunes){return ((IEnumerable)app.GetType("CapaIU.Controles.CalendarioSemanal").GetMethod("Distribuir",BindingFlags.NonPublic|BindingFlags.Static).Invoke(null,new object[]{t,lunes})).Cast<object>().ToArray();}
    static void Denegado(Action a,string mensaje){try{a();}catch(UnauthorizedAccessException){Check(true,mensaje);return;}throw new Exception(mensaje);}
    [STAThread]static int Main(string[] args)
    {
        try
        {
            Application.EnableVisualStyles();app=Assembly.LoadFrom(args[0]);Directory.CreateDirectory(args[1]);var lunes=new DateTime(2026,10,26);
            Check(MonitoreoBLL.Lunes(lunes.AddDays(6).AddHours(20))==lunes,"domingo pertenece a la semana del lunes anterior");
            Check(MonitoreoBLL.Lunes(new DateTime(2027,1,1))==new DateTime(2026,12,28),"semana que cruza de año");
            Check(MonitoreoBLL.Lunes(lunes)==lunes,"lunes no cambia de semana");
            Denegado(()=>new MonitoreoBLL().Inicio(),"inicio exige permiso antes de consultar SQL");Denegado(()=>new MonitoreoBLL().Semana(lunes),"calendario exige permiso antes de consultar SQL");
            var tabla=Tabla();Check(Distribuir(tabla,lunes).Length==0,"semana sin sesiones");
            tabla.Rows.Add(1,lunes.AddHours(18),lunes.AddHours(19));tabla.Rows.Add(2,lunes.AddHours(18.25),lunes.AddHours(19.5));tabla.Rows.Add(3,lunes.AddHours(19.5),lunes.AddHours(20));
            var bloques=Distribuir(tabla,lunes);Check(bloques.Length==3,"actividades simultaneas se conservan");
            Check((int)Valor(bloques[0],"Carril")!=(int)Valor(bloques[1],"Carril") && (int)Valor(bloques[0],"Carriles")==2,"sesiones simultaneas se distribuyen sin taparse");
            Check((int)Valor(bloques[2],"Carriles")==1,"sesion consecutiva recupera el ancho completo");
            tabla.Clear();tabla.Rows.Add(1,lunes.AddDays(-1).AddHours(23),lunes.AddHours(1));tabla.Rows.Add(2,lunes.AddDays(6).AddHours(23),lunes.AddDays(7).AddHours(1));
            bloques=Distribuir(tabla,lunes);Check(bloques.Length==2,"se incluyen sesiones que cruzan limites semanales");Check((DateTime)Valor(bloques[0],"Inicio")==lunes,"entrada desde domingo anterior se recorta al lunes");Check((DateTime)Valor(bloques[1],"Fin")==lunes.AddDays(7),"salida al lunes siguiente se recorta al domingo");
            tabla.Clear();tabla.Rows.Add(1,lunes.AddHours(23),lunes.AddDays(1).AddHours(1));Check(Distribuir(tabla,lunes).Length==2,"sesion nocturna aparece en ambos dias");
            var tipo=app.GetType("CapaIU.Negocio.FrmCalendarioSemanal");
            using(var f=(Form)tipo.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(bool)},null).Invoke(new object[]{true}))
            {
                Preparar(f);var filtro=Campo<ComboBox>(f,"actividad");var canvas=Campo<Control>(f,"calendario");
                Check(filtro.Items.Count==5,"filtro incluye actividades de la semana y todas");
                var seleccion=tipo.GetMethod("Seleccionar",BindingFlags.NonPublic|BindingFlags.Instance);
                var primerBloque=((IList)canvas.GetType().GetField("bloques",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(canvas))[0];var rect=(Rectangle)Valor(primerBloque,"Rectangulo");
                canvas.GetType().GetMethod("OnMouseClick",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(canvas,new object[]{new MouseEventArgs(MouseButtons.Left,1,rect.X+5,rect.Y+5,0)});Application.DoEvents();
                var personas=Campo<DataGridView>(f,"participantes");Check(personas.Rows.Count==12,"consulta participantes de la sesion seleccionada");Check(personas.Columns["Id_Sesion"].Visible==false,"identificadores internos ocultos");Check(Convert.ToString(personas.Rows[0].Cells["Asistencia"].Value)=="Sin registrar","asistencia sin carga no se confunde con ausencia");
                Check(Campo<Label>(f,"detalle").Text.Contains("12 / 20"),"detalle muestra ocupacion y capacidad");
                filtro.SelectedValue=2;Application.DoEvents();Check(personas.DataSource==null,"cambiar filtro limpia los participantes anteriores");
                Check(((IList)canvas.GetType().GetField("bloques",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(canvas)).Count==5,"filtro de actividad reduce sesiones");
                filtro.SelectedValue=0;Campo<CheckBox>(f,"canceladas").Checked=true;Application.DoEvents();Check(((IList)canvas.GetType().GetField("bloques",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(canvas)).Count==14,"sesiones canceladas solo con filtro explicito");
                canvas.GetType().GetMethod("OnMouseClick",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(canvas,new object[]{new MouseEventArgs(MouseButtons.Left,1,rect.X+5,rect.Y+5,0)});
                canvas.GetType().GetMethod("OnKeyDown",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(canvas,new object[]{new KeyEventArgs(Keys.Right)});Application.DoEvents();
                Check(Campo<Label>(f,"detalle").Text.Contains("Pilates"),"teclado selecciona y desplaza a la proxima sesion");Imagen(f,"calendario_semanal",args[1]);
                f.ClientSize=new Size(1080,740);Imagen(f,"calendario_compacto",args[1]);Check(((ScrollableControl)canvas).AutoScroll,"calendario desplazable en ventana compacta");
            }
            tipo=app.GetType("CapaIU.Negocio.FrmSaldosPendientes");
            using(var f=(Form)tipo.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(Action<int>),typeof(bool)},null).Invoke(new object[]{new Action<int>(id=>{}),true}))
            {
                Preparar(f);var g=Campo<DataGridView>(f,"grilla");Check(g.Rows.Count==4,"listado pendiente una fila por socio");Campo<TextBox>(f,"buscar").Text="maria";Application.DoEvents();Check(g.Rows.Count==1,"busqueda de pendientes ignora tildes y mayusculas");Campo<TextBox>(f,"buscar").Text="'";Check(g.Rows.Count==0,"busqueda con apostrofo no rompe el filtro");Campo<TextBox>(f,"buscar").Text="11";Check(g.Rows.Count==1,"busqueda de pendientes por numero");Check(!Campo<Button>(f,"abrir").Enabled,"vista previa no abre cobros reales");Campo<TextBox>(f,"buscar").Clear();Imagen(f,"saldos_pendientes",args[1]);
            }
            tipo=app.GetType("CapaIU.FrmInicio");
            using(var f=(Form)tipo.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,new[]{typeof(Action),typeof(Action),typeof(bool)},null).Invoke(new object[]{new Action(()=>{}),new Action(()=>{}),true}))
            {
                Preparar(f);var numeros=Campo<Label[]>(f,"valores");Check(numeros[0].Text=="150" && numeros[1].Text=="4","inicio distingue total de socios y personas con saldo");Check(Campo<DataGridView>(f,"pendientes").Rows.Count==4,"inicio muestra acceso a socios deudores");Check(!Campo<Button>(f,"cobrar").Enabled,"inicio de muestra no permite cobros");Imagen(f,"inicio_monitoreo",args[1]);
            }
            Console.WriteLine(pruebas+" pruebas aprobadas. SQL real y concurrencia no ejecutados.");return 0;
        }
        catch(Exception e){Console.Error.WriteLine(e);return 1;}
    }
}
