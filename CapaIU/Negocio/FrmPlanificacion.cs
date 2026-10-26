using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaDominio;
using CapaIU.Controles;
namespace CapaIU.Negocio
{
    public sealed class FrmPlanificacion : PantallaGestion
    {
        readonly bool preview;
        readonly ComboBox actividad=TemaVisual.Lista();
        readonly DateTimePicker desde=TemaVisual.Fecha(),hasta=TemaVisual.Fecha(),inicio=TemaVisual.Fecha(),fin=TemaVisual.Fecha();
        readonly CheckedListBox dias=new CheckedListBox {CheckOnClick=true,Height=160,BorderStyle=BorderStyle.None};
        readonly NumericUpDown cupo=new NumericUpDown {Minimum=1,Maximum=1000,Value=20};
        readonly DataGridView propuesta=TemaVisual.Grilla(),agenda=TemaVisual.Grilla();
        readonly Button guardar=TemaVisual.Boton("Guardar sesiones",true),cancelar=TemaVisual.Boton("Cancelar sesión",false,true);
        readonly Label detalle=TemaVisual.Etiqueta("Seleccioná la actividad."),contador=TemaVisual.Etiqueta("Primero generá una vista previa.");
        readonly TabControl tabs=new TabControl {Dock=DockStyle.Fill};
        readonly DayOfWeek[] orden={DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday,DayOfWeek.Sunday};
        List<SesionPlanificada> borrador;
        DataTable sesiones;
        public FrmPlanificacion():this(false){}
        internal FrmPlanificacion(bool vistaPrevia)
        {
            preview=vistaPrevia;if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            Text="Programar actividades";
            desde.Value=DateTime.Today.AddDays(1);hasta.Value=desde.Value.AddDays(27);
            foreach(var h in new[]{inicio,fin}){h.CustomFormat="HH:mm";h.ShowUpDown=true;}
            inicio.Value=DateTime.Today.AddHours(18);fin.Value=DateTime.Today.AddHours(19);
            dias.Items.AddRange(new object[]{"Lunes","Martes","Miércoles","Jueves","Viernes","Sábado","Domingo"});
            var contenido=new Panel {Dock=DockStyle.Fill};Cuerpo.Controls.Add(contenido);Cuerpo.Controls.Add(Cabecera("Programar actividades","Elegí los días, revisá la propuesta y guardá todo en una operación."));
            var split=new SplitContainer {Dock=DockStyle.Fill,Size=new Size(1100,600),SplitterDistance=350,Panel1MinSize=330,Panel2MinSize=500,SplitterWidth=16};contenido.Controls.Add(split);
            var editor=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=1,Padding=new Padding(16),BackColor=Color.White};
            actividad.DisplayMember="Etiqueta";actividad.ValueMember="Id_Clase";
            editor.Controls.Add(TemaVisual.Campo("Actividad",actividad));editor.Controls.Add(Doble("Desde",desde,"Hasta (inclusive)",hasta));
            detalle.MaximumSize=new Size(295,0);editor.Controls.Add(detalle);editor.Controls.Add(TemaVisual.Etiqueta("Días de la semana",9,true));dias.Dock=DockStyle.Top;editor.Controls.Add(dias);
            editor.Controls.Add(Doble("Hora de inicio",inicio,"Hora de fin",fin));editor.Controls.Add(TemaVisual.Campo("Cupo por sesión",cupo));
            var generar=TemaVisual.Boton("Revisar propuesta",true);generar.Dock=DockStyle.Fill;generar.Click+=(s,e)=>{try{Generar();}catch(ArgumentException ex){Aviso(ex.Message,true);}};
            var scroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true};scroll.Controls.Add(editor);var accionesEditor=new Panel {Dock=DockStyle.Bottom,Height=56,Padding=new Padding(8),BackColor=Color.White};accionesEditor.Controls.Add(generar);split.Panel1.Controls.Add(scroll);split.Panel1.Controls.Add(accionesEditor);scroll.Resize+=(s,e)=>editor.Width=Math.Max(310,scroll.ClientSize.Width-20);
            var nueva=new TabPage("1. Propuesta") {BackColor=Color.White,Padding=new Padding(12)};var pie=new FlowLayoutPanel {Dock=DockStyle.Bottom,Height=94,FlowDirection=FlowDirection.TopDown,WrapContents=false};contador.AutoSize=true;pie.Controls.Add(contador);pie.Controls.Add(guardar);nueva.Controls.Add(propuesta);nueva.Controls.Add(pie);tabs.TabPages.Add(nueva);
            var existente=new TabPage("2. Agenda guardada") {BackColor=Color.White,Padding=new Padding(12)};var acciones=new FlowLayoutPanel {Dock=DockStyle.Bottom,Height=54};var refrescar=TemaVisual.Boton("Actualizar");refrescar.Click+=async(s,e)=>await Ejecutar(Cargar);acciones.Controls.Add(refrescar);acciones.Controls.Add(cancelar);existente.Controls.Add(agenda);existente.Controls.Add(acciones);tabs.TabPages.Add(existente);split.Panel2.Controls.Add(tabs);
            propuesta.AutoSizeColumnsMode=agenda.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            propuesta.DataBindingComplete+=(s,e)=>{foreach(DataGridViewColumn c in propuesta.Columns)c.DefaultCellStyle.Format="ddd dd/MM/yyyy HH:mm";};
            agenda.DataBindingComplete+=(s,e)=>{foreach(DataGridViewColumn c in agenda.Columns){if(c.Name=="Id" || c.Name=="Id_Clase" || c.Name=="Inscriptos" || c.Name=="EnEspera")c.Visible=false;if(c.Name=="Capacidad")c.HeaderText="Cupo";if(c.Name=="Disponibles")c.HeaderText="Libres";if(c.Name=="Inicio" || c.Name=="Fin")c.DefaultCellStyle.Format="dd/MM/yyyy HH:mm";}agenda.ClearSelection();agenda.CurrentCell=null;};
            agenda.SelectionChanged+=(s,e)=>cancelar.Enabled=!preview && Seleccion()!=null && Convert.ToString(Seleccion()["Estado"])=="Programada" && Convert.ToDateTime(Seleccion()["Inicio"])>DateTime.Now;
            guardar.Click+=async(s,e)=>await Ejecutar(async()=>{
                if(preview || borrador==null) return;
                int id=IdActividad(),capacidad=(int)cupo.Value;var lote=borrador.ToList();
                if(MessageBox.Show(this,"Se crearán "+lote.Count+" sesiones para "+actividad.Text+". ¿Guardar?","Confirmar programación",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;
                int total=await Task.Run(()=>new PlanificacionBLL().Guardar(id,lote,capacidad));Invalidar();await Cargar();tabs.SelectedIndex=1;Aviso(total+" sesiones programadas. Podés reservar y tomar asistencia en Operación diaria.");
            });
            cancelar.Click+=async(s,e)=>await Ejecutar(async()=>{var fila=Seleccion();if(preview || fila==null)return;int id=Convert.ToInt32(fila["Id"]);if(MessageBox.Show(this,"Se cancelarán esta sesión y sus reservas. ¿Continuar?","Cancelar sesión",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;await Task.Run(()=>new OperacionBLL().CancelarSesion(id));await Cargar();Aviso("Sesión y reservas canceladas.");});
            actividad.SelectedIndexChanged+=(s,e)=>{Invalidar();Filtrar();var fila=actividad.SelectedItem as DataRowView;detalle.Text=fila==null?"Seleccioná la actividad.":Convert.ToString(fila["Entrenador"])+" · "+Convert.ToString(fila["Espacio"]);};
            foreach(var d in new[]{desde,hasta,inicio,fin})d.ValueChanged+=(s,e)=>Invalidar();cupo.ValueChanged+=(s,e)=>Invalidar();dias.ItemCheck+=(s,e)=>Invalidar();
            guardar.Enabled=cancelar.Enabled=false;
            Shown+=async(s,e)=>{if(preview)DatosPrueba();else await Ejecutar(Cargar);};
        }
        static Panel Doble(string tituloA,Control a,string tituloB,Control b){var p=new TableLayoutPanel {Dock=DockStyle.Top,Height=66,ColumnCount=2,Margin=new Padding(0)};p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));var ca=TemaVisual.Campo(tituloA,a);var cb=TemaVisual.Campo(tituloB,b);ca.Dock=cb.Dock=DockStyle.Fill;p.Controls.Add(ca,0,0);p.Controls.Add(cb,1,0);return p;}
        public void MostrarAgenda(){tabs.SelectedIndex=1;}
        int IdActividad(){return actividad.SelectedValue is int?(int)actividad.SelectedValue:0;}
        DataRowView Seleccion(){return agenda.CurrentRow==null?null:agenda.CurrentRow.DataBoundItem as DataRowView;}
        void Invalidar(){borrador=null;propuesta.DataSource=null;guardar.Enabled=false;contador.Text="La propuesta se actualiza con Revisar propuesta.";}
        void Generar(){if(IdActividad()<=0)throw new ArgumentException("Elegí una actividad.");var elegidos=dias.CheckedIndices.Cast<int>().Select(i=>orden[i]);borrador=Planificador.Generar(desde.Value,hasta.Value,inicio.Value.TimeOfDay,fin.Value.TimeOfDay,elegidos,DateTime.Now);propuesta.DataSource=borrador;contador.Text=borrador.Count+" sesiones propuestas · cupo "+cupo.Value+" por sesión";guardar.Enabled=!preview;tabs.SelectedIndex=0;Aviso("Propuesta lista. Al guardar se comprobarán los cruces de profesor y espacio.");}
        void Filtrar(){if(sesiones==null)return;var vista=new DataView(sesiones);if(IdActividad()>0)vista.RowFilter="Id_Clase="+IdActividad();agenda.DataSource=vista;}
        async Task Cargar(){int previo=IdActividad();var datos=await Task.Run(()=>new object[]{new ActividadesBLL().Cargar(),new OperacionBLL().Listar("Sesiones")});Enlazar(((DataSet)datos[0]).Tables[0],(DataTable)datos[1],previo);Aviso(actividad.Items.Count==0?"Primero creá una actividad con profesor y espacio.":"Información actualizada. Elegí la actividad para ver su agenda.");}
        void Enlazar(DataTable clases,DataTable lista,int seleccionado){if(!clases.Columns.Contains("Etiqueta"))clases.Columns.Add("Etiqueta",typeof(string),"Descripcion + ' (#' + Convert(Id_Clase, 'System.String') + ')' ");sesiones=lista;actividad.DataSource=clases;if(seleccionado>0)actividad.SelectedValue=seleccionado;else actividad.SelectedIndex=-1;Filtrar();}
        void DatosPrueba(){var clases=new DataTable();clases.Columns.Add("Id_Clase",typeof(int));foreach(var n in new[]{"Descripcion","Entrenador","Espacio"})clases.Columns.Add(n);clases.Rows.Add(1,"Funcional","Alex Ejemplo","Sala principal");var lista=new DataTable();lista.Columns.Add("Id",typeof(int));lista.Columns.Add("Id_Clase",typeof(int));lista.Columns.Add("Clase");lista.Columns.Add("Inicio",typeof(DateTime));lista.Columns.Add("Fin",typeof(DateTime));lista.Columns.Add("Estado");Enlazar(clases,lista,1);dias.SetItemChecked(0,true);dias.SetItemChecked(2,true);Generar();Aviso("Vista previa con datos ficticios. No se modifica la base.");}
    }
}
