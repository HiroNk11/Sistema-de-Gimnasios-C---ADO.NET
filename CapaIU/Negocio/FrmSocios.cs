using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaAplicacion.Negocio;
using CapaDominio;
using CapaIU.Controles;
namespace CapaIU.Negocio
{
    public class FrmSocios : Form
    {
        readonly bool preview;
        readonly SocioBLL gestor=new SocioBLL();
        readonly DataGridView grid=TemaVisual.Grilla();
        readonly TextBox buscar=TemaVisual.Texto(100),nombre=TemaVisual.Texto(50),apellido=TemaVisual.Texto(50);
        readonly DateTimePicker nacimiento=TemaVisual.Fecha();
        readonly Button guardar=TemaVisual.Boton("Crear socio",true),eliminar=TemaVisual.Boton("Eliminar socio",false,true);
        readonly Label tituloEditor=TemaVisual.Etiqueta("Nuevo socio",15,true),estado=TemaVisual.Etiqueta("",9);
        readonly ErrorProvider errores=new ErrorProvider {BlinkStyle=ErrorBlinkStyle.NeverBlink};
        List<Socio> datos=new List<Socio>();int? seleccionado;bool ocupado;
        public FrmSocios():this(false){}
        internal FrmSocios(bool vistaPrevia)
        {
            preview=vistaPrevia;if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            FormClosing+=(s,e)=>{if(ocupado && e.CloseReason==CloseReason.UserClosing){e.Cancel=true;Aviso("Esperá a que termine la operación antes de cerrar.",true);}};
            Text="Socios";Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;ClientSize=new Size(1120,710);MinimumSize=new Size(900,620);errores.ContainerControl=this;
            grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            nacimiento.MaxDate=DateTime.Today;nacimiento.Value=DateTime.Today;nacimiento.ShowCheckBox=true;nacimiento.Checked=false;grid.AccessibleName="Listado de socios";
            var layout=new TableLayoutPanel {Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=1,RowCount=4};layout.RowStyles.Add(new RowStyle(SizeType.Absolute,80));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,58));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,32));Controls.Add(layout);
            var head=new Panel {Dock=DockStyle.Fill};var title=TemaVisual.Etiqueta("Socios",23,true);title.Location=new Point(0,0);head.Controls.Add(title);var sub=TemaVisual.Etiqueta("Buscá, registrá o actualizá los datos de cada persona.");sub.Location=new Point(1,45);sub.ForeColor=TemaVisual.Tenue;head.Controls.Add(sub);layout.Controls.Add(head,0,0);
            var toolbar=new FlowLayoutPanel {Dock=DockStyle.Fill,BackColor=Color.White,Padding=new Padding(12,7,12,6),WrapContents=false};var l=TemaVisual.Etiqueta("Buscar",10,true);l.Margin=new Padding(0,6,10,0);toolbar.Controls.Add(l);buscar.Width=330;buscar.Margin=new Padding(0,4,16,0);buscar.AccessibleName="Buscar socio por nombre, apellido o número";toolbar.Controls.Add(buscar);
            var nuevo=TemaVisual.Boton("Nuevo socio",true);nuevo.Width=135;nuevo.Click+=(s,e)=>Nuevo();toolbar.Controls.Add(nuevo);var actualizar=TemaVisual.Boton("Actualizar");actualizar.Width=115;actualizar.Click+=async(s,e)=>await Ejecutar(Cargar);toolbar.Controls.Add(actualizar);layout.Controls.Add(toolbar,0,1);
            var split=new SplitContainer {Dock=DockStyle.Fill,Size=new Size(1080,500),SplitterDistance=720,SplitterWidth=16,Panel1MinSize=300,Panel2MinSize=300,BackColor=TemaVisual.Fondo};var listPanel=new Panel {Dock=DockStyle.Fill,BackColor=Color.White,Padding=new Padding(12)};listPanel.Controls.Add(grid);split.Panel1.Controls.Add(listPanel);
            var editor=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=1,BackColor=Color.White,Padding=new Padding(18)};editor.Controls.Add(tituloEditor);tituloEditor.Margin=new Padding(0,0,0,18);
            editor.Controls.Add(TemaVisual.Campo("Nombre",nombre));editor.Controls.Add(TemaVisual.Campo("Apellido",apellido));editor.Controls.Add(TemaVisual.Campo("Fecha de nacimiento",nacimiento));
            guardar.Dock=DockStyle.Top;guardar.Margin=new Padding(0,10,0,6);guardar.Click+=async(s,e)=>await Ejecutar(Guardar);editor.Controls.Add(guardar);
            eliminar.Dock=DockStyle.Top;eliminar.Margin=new Padding(0,6,0,18);eliminar.Click+=async(s,e)=>await Ejecutar(Eliminar);editor.Controls.Add(eliminar);
            if(!preview)eliminar.Visible=new PermisosBLL().BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeEliminarSocios,new CapaServicios.Composite.PermisoUsuario {idusuario=CapaServicios.SingletonSesion.Instancia.Usuario.idusuario});
            var avanzado=TemaVisual.Boton("Historial y exportación");avanzado.Dock=DockStyle.Top;avanzado.Click+=(s,e)=>{if(preview)return;var form=new FrmSocio();if(MdiParent!=null)form.MdiParent=MdiParent;form.Show();};editor.Controls.Add(avanzado);
            if(!preview){var p=new CapaServicios.Composite.PermisoUsuario {idusuario=CapaServicios.SingletonSesion.Instancia.Usuario.idusuario};var permisos=new PermisosBLL();avanzado.Visible=permisos.BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeSerializar,p) || permisos.BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeVerhistorialCambios,p);}
            var editorScroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true};editorScroll.Controls.Add(editor);split.Panel2.Controls.Add(editorScroll);layout.Controls.Add(split,0,2);estado.Dock=DockStyle.Fill;estado.TextAlign=ContentAlignment.MiddleLeft;layout.Controls.Add(estado,0,3);
            buscar.TextChanged+=(s,e)=>Filtrar();grid.CellClick+=(s,e)=>{if(e.RowIndex>=0)Seleccionar();};
            grid.DataBindingComplete+=(s,e)=>{if(grid.Columns.Contains("Id_Socio"))grid.Columns["Id_Socio"].HeaderText="N.º socio";if(grid.Columns.Contains("Fecha")){grid.Columns["Fecha"].HeaderText="Nacimiento";grid.Columns["Fecha"].DefaultCellStyle.Format="dd/MM/yyyy";}if(grid.Columns.Contains("NombreCompleto"))grid.Columns["NombreCompleto"].Visible=false;grid.ClearSelection();grid.CurrentCell=null;};
            Shown+=async(s,e)=>{if(!preview)await Ejecutar(Cargar);};Nuevo();
            if(preview){datos=new List<Socio>{new Socio {Id_Socio=1,Nombre="Alex",Apellido="Ejemplo",Fecha=new DateTime(1997,4,12)},new Socio {Id_Socio=2,Nombre="Sam",Apellido="Muestra",Fecha=new DateTime(1990,8,23)},new Socio {Id_Socio=3,Nombre="Dana",Apellido="Prueba",Fecha=new DateTime(2001,2,6)}};Filtrar();guardar.Enabled=eliminar.Enabled=false;estado.Text="Vista previa con datos ficticios. No se modifica la base.";}
        }
        protected override void Dispose(bool disposing){if(disposing)errores.Dispose();base.Dispose(disposing);}
        async Task Ejecutar(Func<Task> accion)
        {
            if(ocupado || preview)return;ocupado=true;Controls[0].Enabled=false;guardar.Enabled=eliminar.Enabled=false;UseWaitCursor=true;
            try {await accion();}
            catch(ArgumentException e){Aviso(e.Message,true);}
            catch(SqlException e){if(e.Number==547)Aviso("Este socio tiene registros asociados. Conservá su ficha para mantener el historial.",true);else{Aviso("No se pudo guardar. Revisá el aviso de error.",true);Program.Error(e);}}
            catch(UnauthorizedAccessException ex){Aviso(ex.Message,true);}
            catch(Exception e){Aviso("No se pudo completar la operación.",true);Program.Error(e);}
            finally{ocupado=false;Controls[0].Enabled=true;UseWaitCursor=false;guardar.Enabled=true;eliminar.Enabled=seleccionado.HasValue;}
        }
        void Aviso(string text,bool error=false){estado.Text=text;estado.ForeColor=error?TemaVisual.Rojo:TemaVisual.Primario;}
        async Task Cargar(){datos=await Task.Run(()=>gestor.Listar());Filtrar();Nuevo();Aviso(datos.Count==0?"Todavía no hay socios. Completá la ficha para registrar el primero.":datos.Count+" socios registrados.");}
        void Filtrar(){string q=buscar.Text.Trim();grid.DataSource=datos.Where(x=>q.Length==0 || (x.Nombre+" "+x.Apellido+" "+x.Id_Socio).IndexOf(q,StringComparison.CurrentCultureIgnoreCase)>=0).ToList();}
        void Nuevo(){seleccionado=null;tituloEditor.Text="Nuevo socio";guardar.Text="Crear socio";nombre.Clear();apellido.Clear();nacimiento.Value=DateTime.Today;nacimiento.Checked=false;errores.Clear();eliminar.Enabled=false;grid.ClearSelection();grid.CurrentCell=null;nombre.Focus();}
        void Seleccionar(){var s=grid.CurrentRow==null?null:grid.CurrentRow.DataBoundItem as Socio;if(s==null)return;seleccionado=s.Id_Socio;nombre.Text=s.Nombre;apellido.Text=s.Apellido;nacimiento.Value=s.Fecha<=DateTime.Today?s.Fecha:DateTime.Today;nacimiento.Checked=true;tituloEditor.Text="Editar socio #"+s.Id_Socio;guardar.Text="Guardar cambios";eliminar.Enabled=!preview;errores.Clear();}
        async Task Guardar()
        {
            errores.Clear();if(string.IsNullOrWhiteSpace(nombre.Text)){errores.SetError(nombre,"Ingresá el nombre.");nombre.Focus();return;}if(string.IsNullOrWhiteSpace(apellido.Text)){errores.SetError(apellido,"Ingresá el apellido.");apellido.Focus();return;}
            if(!nacimiento.Checked){errores.SetError(nacimiento,"Indicá la fecha de nacimiento.");nacimiento.Focus();return;}
            var socio=new Socio {Id_Socio=seleccionado??0,Nombre=nombre.Text.Trim(),Apellido=apellido.Text.Trim(),Fecha=nacimiento.Value.Date};bool editar=seleccionado.HasValue;
            await Task.Run(()=>editar?gestor.Editar(socio):gestor.Insertar(socio));await Cargar();Aviso(editar?"Datos del socio actualizados.":"Socio registrado. Ya podés crear su membresía.");
        }
        async Task Eliminar(){if(!seleccionado.HasValue)return;var socio=new Socio {Id_Socio=seleccionado.Value};if(MessageBox.Show(this,"¿Eliminar la ficha de "+nombre.Text+" "+apellido.Text+"? Los socios con registros asociados no se pueden eliminar.","Eliminar socio",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;await Task.Run(()=>gestor.Borrar(socio));await Cargar();Aviso("Socio eliminado.");}
    }
}
