using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaIU.Controles;
using CapaIU.Presentacion;

namespace CapaIU.Negocio
{
    public sealed class FrmCalendarioSemanal : PantallaGestion
    {
        readonly bool preview;
        readonly CalendarioSemanal calendario=new CalendarioSemanal{Dock=DockStyle.Fill,Font=new Font("Segoe UI",9)};
        readonly DateTimePicker fecha=TemaVisual.Fecha();
        readonly ComboBox actividad=TemaVisual.Lista();
        readonly CheckBox canceladas=new CheckBox{Text="Incluir canceladas",AutoSize=true,Margin=new Padding(8,10,4,0)};
        readonly DataGridView participantes=TemaVisual.Grilla();
        readonly Label detalle=TemaVisual.Etiqueta("Seleccioná una sesión para ver sus socios.",10,true),periodo=TemaVisual.Etiqueta("",11,true);
        DataSet datos;
        bool enlazando;
        DateTime semanaCargada;
        public FrmCalendarioSemanal():this(false){}
        internal FrmCalendarioSemanal(bool preview)
        {
            this.preview=preview;if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            Text="Calendario semanal";MinimumSize=new Size(1080,740);ClientSize=new Size(1240,840);
            var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=5};layout.RowStyles.Add(new RowStyle(SizeType.Absolute,82));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,42));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,210));Cuerpo.Controls.Add(layout);
            layout.Controls.Add(Cabecera(Text,"Lunes a domingo. Elegí una sesión para consultar actividad, profesor, espacio e inscriptos."),0,0);
            var barra=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};
            foreach(var par in new[]{Tuple.Create("‹ Semana",-7),Tuple.Create("Hoy",0),Tuple.Create("Semana ›",7)})
            {int dias=par.Item2;var b=TemaVisual.Boton(par.Item1);b.Width=100;b.Click+=async(s,e)=>await Ejecutar(async()=>{fecha.Value=dias==0?DateTime.Today:fecha.Value.AddDays(dias);await Cargar();});barra.Controls.Add(b);}
            fecha.Width=145;fecha.Margin=new Padding(8,10,8,0);fecha.MinDate=new DateTime(1900,1,1);fecha.MaxDate=new DateTime(9998,12,24);barra.Controls.Add(fecha);
            var ir=TemaVisual.Boton("Ver semana",true);ir.Width=130;ir.Click+=async(s,e)=>await Ejecutar(Cargar);barra.Controls.Add(ir);barra.Controls.Add(periodo);periodo.Margin=new Padding(12,12,0,0);layout.Controls.Add(barra,0,1);
            var filtros=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};filtros.Controls.Add(TemaVisual.Etiqueta("Actividad",10,true));actividad.Width=280;actividad.DisplayMember="Nombre";actividad.ValueMember="Id";filtros.Controls.Add(actividad);filtros.Controls.Add(canceladas);var leyenda=TemaVisual.Etiqueta("Ámbar: cupo completo · Gris: cancelada",9);leyenda.Margin=new Padding(18,8,0,0);filtros.Controls.Add(leyenda);layout.Controls.Add(filtros,0,2);
            actividad.SelectedIndexChanged+=(s,e)=>Filtrar();canceladas.CheckedChanged+=(s,e)=>Filtrar();layout.Controls.Add(calendario,0,3);calendario.SesionSeleccionada+=Seleccionar;
            var inferior=new Panel{Dock=DockStyle.Fill,Padding=new Padding(0,10,0,0)};detalle.Dock=DockStyle.Top;detalle.Height=42;detalle.AutoSize=false;inferior.Controls.Add(participantes);inferior.Controls.Add(detalle);layout.Controls.Add(inferior,0,4);
            participantes.AccessibleName="Socios de la sesión seleccionada";Shown+=async(s,e)=>await Ejecutar(Cargar);
        }
        async Task Cargar()
        {
            var lunes=MonitoreoBLL.Lunes(fecha.Value);
            var nuevo=preview?MonitoreoMuestra.Semana(lunes):await Task.Run(()=>new MonitoreoBLL().Semana(lunes));
            datos=nuevo;semanaCargada=lunes;fecha.Value=lunes;periodo.Text=lunes.ToString("dd/MM")+" — "+lunes.AddDays(6).ToString("dd/MM/yyyy");
            int anterior=actividad.SelectedValue is int?(int)actividad.SelectedValue:0;
            var tipos=new DataTable();tipos.Columns.Add("Id",typeof(int));tipos.Columns.Add("Nombre",typeof(string));tipos.Rows.Add(0,"Todas las actividades");
            foreach(var row in datos.Tables["Sesiones"].AsEnumerable().GroupBy(r=>r.Field<int>("Id_Clase")).Select(g=>g.First()).OrderBy(r=>r.Field<string>("Actividad")))tipos.Rows.Add(row["Id_Clase"],row["Actividad"]);
            enlazando=true;try{actividad.DataSource=tipos;actividad.SelectedValue=tipos.Rows.Cast<DataRow>().Any(r=>(int)r["Id"]==anterior)?anterior:0;}finally{enlazando=false;}
            Filtrar();
        }
        void Filtrar()
        {
            if(datos==null || enlazando)return;int id=actividad.SelectedValue is int?(int)actividad.SelectedValue:0;
            var sesiones=datos.Tables["Sesiones"].Clone();foreach(DataRow row in datos.Tables["Sesiones"].Rows)if((id==0 || (int)row["Id_Clase"]==id) && (canceladas.Checked || (string)row["Estado"]!="Cancelada"))sesiones.ImportRow(row);
            calendario.Mostrar(sesiones,semanaCargada);participantes.DataSource=null;detalle.Text="Seleccioná una sesión para ver sus socios. Las horas sin tarjetas no tienen sesiones programadas.";
            Aviso((preview?"Vista previa con datos ficticios. ":"")+sesiones.Rows.Count+" sesiones visibles. "+(sesiones.Rows.Count==0?"Esta semana no tiene sesiones para el filtro elegido.":"Clic en una tarjeta o usá las flechas del teclado. Actualizá con Ver semana."));
        }
        void Seleccionar(int id)
        {
            var row=datos.Tables["Sesiones"].AsEnumerable().First(r=>r.Field<int>("Id")==id);
            detalle.Text=row["Actividad"]+" · "+row["Profesor"]+" · "+row["Espacio"]+" · "+Convert.ToDateTime(row["Inicio"]).ToString("dd/MM HH:mm")+"–"+Convert.ToDateTime(row["Fin"]).ToString("dd/MM HH:mm")+"\nConfirmados: "+row["Confirmados"]+" / "+row["Capacidad"]+" · En espera: "+row["EnEspera"]+" · "+row["Estado"];
            var tabla=datos.Tables["Participantes"].Clone();foreach(DataRow persona in datos.Tables["Participantes"].Rows)if((int)persona["Id_Sesion"]==id)tabla.ImportRow(persona);
            tabla.Columns.Add("Asistencia",typeof(string));foreach(DataRow persona in tabla.Rows)persona["Asistencia"]=persona.IsNull("Asistio")?"Sin registrar":(bool)persona["Asistio"]?"Presente":"Ausente";
            participantes.DataSource=tabla;participantes.Columns["Id_Reserva"].Visible=false;participantes.Columns["Id_Sesion"].Visible=false;participantes.Columns["Id_Socio"].HeaderText="Nº socio";participantes.Columns["Asistio"].Visible=false;participantes.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;participantes.Columns["Socio"].FillWeight=200;participantes.ClearSelection();
            if(tabla.Rows.Count==0)Aviso("Esta sesión todavía no tiene socios inscriptos.");
        }
    }
}
