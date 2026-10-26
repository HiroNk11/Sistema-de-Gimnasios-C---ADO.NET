using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaDominio;
using CapaIU.Controles;
namespace CapaIU.Negocio
{
    public sealed class FrmActividades : PantallaGestion
    {
        readonly bool preview;
        readonly DataGridView grilla=TemaVisual.Grilla();
        readonly TextBox buscar=TemaVisual.Texto(50),nombre=TemaVisual.Texto(50);
        readonly ComboBox tipo=TemaVisual.Lista(),profesor=TemaVisual.Lista(),espacio=TemaVisual.Lista();
        readonly NumericUpDown cupo=new NumericUpDown {Minimum=1,Maximum=1000,Value=20};
        readonly Label titulo=TemaVisual.Etiqueta("Nueva actividad",15,true);
        readonly Button guardar=TemaVisual.Boton("Crear actividad",true);
        DataTable datos;int seleccionado;
        public FrmActividades():this(false){}
        internal FrmActividades(bool vistaPrevia)
        {
            preview=vistaPrevia;if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            Text="Actividades";
            var central=new Panel {Dock=DockStyle.Fill};Cuerpo.Controls.Add(central);Cuerpo.Controls.Add(Cabecera("Actividades","Definí la actividad, su profesor y el espacio antes de programar las sesiones."));
            var barra=new FlowLayoutPanel {Dock=DockStyle.Top,Height=58,BackColor=Color.White,Padding=new Padding(8)};barra.Controls.Add(TemaVisual.Etiqueta("Buscar"));buscar.Width=220;barra.Controls.Add(buscar);
            var nuevo=TemaVisual.Boton("Nueva",true);nuevo.Width=100;nuevo.Click+=(s,e)=>Nuevo();barra.Controls.Add(nuevo);var refrescar=TemaVisual.Boton("Actualizar");refrescar.Width=115;refrescar.Click+=async(s,e)=>await Ejecutar(Cargar);barra.Controls.Add(refrescar);
            var programar=TemaVisual.Boton("Programar horarios",true);programar.Click+=(s,e)=>{if(preview)return;if(MdiParent!=null)new CapaIU.Presentacion.NavegadorMdi(MdiParent).Abrir(()=>new FrmPlanificacion());else new FrmPlanificacion().Show(this);};barra.Controls.Add(programar);
            var split=new SplitContainer {Dock=DockStyle.Fill,Size=new Size(1100,560),SplitterDistance=720,SplitterWidth=16,Panel1MinSize=450,Panel2MinSize=310};central.Controls.Add(split);central.Controls.Add(barra);split.Panel1.Controls.Add(grilla);
            var editor=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=1,BackColor=Color.White,Padding=new Padding(16)};editor.Controls.Add(titulo);
            tipo.DisplayMember="Descripcion";tipo.ValueMember="Id_Entrenamiento";profesor.DisplayMember="Nombre";profesor.ValueMember="Id_Entrenador";espacio.DisplayMember="Nombre";espacio.ValueMember="Id_Piso";
            foreach(var c in new[]{TemaVisual.Campo("Nombre de la actividad",nombre),TemaVisual.Campo("Tipo",tipo),TemaVisual.Campo("Profesor",profesor),TemaVisual.Campo("Espacio",espacio),TemaVisual.Campo("Cupo inicial (solo alta)",cupo)})editor.Controls.Add(c);
            guardar.Dock=DockStyle.Top;editor.Controls.Add(guardar);var ayuda=TemaVisual.Etiqueta("El cupo de cada sesión se elige al programarla. Los cambios de profesor o espacio se protegen si existen horarios asociados.",9);ayuda.MaximumSize=new Size(275,0);editor.Controls.Add(ayuda);split.Panel2.AutoScroll=true;split.Panel2.Controls.Add(editor);split.Panel2.Resize+=(s,e)=>editor.Width=Math.Max(290,split.Panel2.ClientSize.Width-18);
            grilla.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            grilla.DataBindingComplete+=(s,e)=>{foreach(DataGridViewColumn c in grilla.Columns){if(c.Name.StartsWith("Id_") || c.Name=="Capacidad")c.Visible=false;if(c.Name=="Descripcion")c.HeaderText="Actividad";if(c.Name=="Actividad")c.HeaderText="Tipo";}grilla.ClearSelection();grilla.CurrentCell=null;};
            buscar.TextChanged+=(s,e)=>Filtrar();grilla.CellClick+=(s,e)=>{if(e.RowIndex<0)return;var fila=grilla.Rows[e.RowIndex].DataBoundItem as DataRowView;if(fila==null)return;seleccionado=Convert.ToInt32(fila["Id_Clase"]);nombre.Text=Convert.ToString(fila["Descripcion"]);tipo.SelectedValue=fila["Id_Entrenamiento"];profesor.SelectedValue=fila["Id_Entrenador"];espacio.SelectedValue=fila["Id_Piso"];cupo.Value=Math.Max(1,Math.Min(1000,Convert.ToInt32(fila["Capacidad"])));cupo.Enabled=false;titulo.Text="Editar actividad #"+seleccionado;guardar.Text="Guardar cambios";};
            guardar.Click+=async(s,e)=>await Ejecutar(async()=>{if(preview)return;var c=new Clase {Id_Clase=seleccionado,Descripcion=nombre.Text.Trim(),Id_Entrenamiento=Id(tipo),Id_Entrenador=Id(profesor),Id_piso=Id(espacio),Capacidad=(int)cupo.Value};ActividadesBLL.Validar(c);await Task.Run(()=>new ActividadesBLL().Guardar(c));await Cargar();Aviso("Actividad guardada. Ya podés programar sus horarios.");});
            Shown+=async(s,e)=>{if(preview)DatosPrueba();else await Ejecutar(Cargar);};Nuevo();if(preview)guardar.Enabled=false;
        }
        int Id(ComboBox c){return c.SelectedValue is int?(int)c.SelectedValue:0;}
        void Nuevo(){seleccionado=0;nombre.Clear();tipo.SelectedIndex=profesor.SelectedIndex=espacio.SelectedIndex=-1;cupo.Value=20;cupo.Enabled=true;titulo.Text="Nueva actividad";guardar.Text="Crear actividad";grilla.ClearSelection();grilla.CurrentCell=null;}
        void Filtrar(){if(datos==null)return;var vista=datos.Clone();string q=buscar.Text.Trim();foreach(DataRow fila in datos.Rows)if((fila["Descripcion"]+" "+fila["Entrenador"]+" "+fila["Espacio"]).IndexOf(q,StringComparison.CurrentCultureIgnoreCase)>=0)vista.ImportRow(fila);grilla.DataSource=vista;}
        async Task Cargar(){var ds=await Task.Run(()=>new ActividadesBLL().Cargar());Enlazar(ds);Aviso(datos.Rows.Count+" actividades. Seleccioná una fila para editarla.");}
        void Enlazar(DataSet ds){datos=ds.Tables[0];tipo.DataSource=ds.Tables[1];profesor.DataSource=ds.Tables[2];espacio.DataSource=ds.Tables[3];Filtrar();Nuevo();}
        void DatosPrueba(){var ds=new DataSet();var tabla=new DataTable();foreach(var n in new[]{"Id_Clase","Id_Entrenamiento","Id_Entrenador","Id_Piso","Capacidad"})tabla.Columns.Add(n,typeof(int));foreach(var n in new[]{"Descripcion","Actividad","Entrenador","Espacio"})tabla.Columns.Add(n);tabla.Rows.Add(1,1,1,1,20,"Funcional inicial","Funcional","Alex Ejemplo","Sala principal");ds.Tables.Add(tabla);foreach(var par in new[]{new[]{"Id_Entrenamiento","Descripcion","Funcional"},new[]{"Id_Entrenador","Nombre","Alex Ejemplo (#1)"},new[]{"Id_Piso","Nombre","Sala principal"}}){var t=new DataTable();t.Columns.Add(par[0],typeof(int));t.Columns.Add(par[1]);t.Rows.Add(1,par[2]);ds.Tables.Add(t);}Enlazar(ds);Aviso("Vista previa con datos ficticios. No se modifica la base.");}
    }
}
