using System;
using System.Collections.Generic;
using System.Data;
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
    public class FrmOperacionDiaria : Form
    {
        readonly bool preview;
        readonly OperacionBLL negocio=new OperacionBLL();
        readonly TabControl tabs=new TabControl {Dock=DockStyle.Fill,Font=TemaVisual.Fuente,Padding=new Point(17,10)};
        readonly ComboBox socio=TemaVisual.Lista(),clase=TemaVisual.Lista(),medio=TemaVisual.Lista();
        readonly DataGridView membresias=TemaVisual.Grilla(),sesiones=TemaVisual.Grilla(),reservas=TemaVisual.Grilla(),pagos=TemaVisual.Grilla();
        readonly TextBox plan=TemaVisual.Texto(100),referencia=TemaVisual.Texto(100);
        readonly DateTimePicker desde=TemaVisual.Fecha(),hasta=TemaVisual.Fecha(),inicio=TemaVisual.Fecha(true),fin=TemaVisual.Fecha(true);
        readonly NumericUpDown importe=TemaVisual.Dinero(),pago=TemaVisual.Dinero(),capacidad=new NumericUpDown {Minimum=1,Maximum=1000,Value=20,Font=TemaVisual.Fuente};
        readonly ErrorProvider errores=new ErrorProvider {BlinkStyle=ErrorBlinkStyle.NeverBlink};
        readonly Label estado=new Label {Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft,ForeColor=TemaVisual.Tenue,Padding=new Padding(20,0,12,0)};
        readonly Label cuota=TemaVisual.Etiqueta("Seleccioná una membresía en la tabla.",10,true),sesionElegida=TemaVisual.Etiqueta("Seleccioná una sesión en la tabla.",10,true);
        readonly Label[] valores=new Label[4];
        readonly Button cobrar=TemaVisual.Boton("Registrar pago",true),completar=TemaVisual.Boton("Usar saldo pendiente"),reservar=TemaVisual.Boton("Reservar para el socio",true),cancelarSesion=TemaVisual.Boton("Cancelar sesión",false,true),cancelarReserva=TemaVisual.Boton("Cancelar reserva",false,true),presente=TemaVisual.Boton("Marcar presente",true),ausente=TemaVisual.Boton("Marcar ausente"),actualizar=TemaVisual.Boton("Actualizar");
        bool ocupado;
        int socioInicial;
        TabControl editorMembresias,editorSesiones;
        public FrmOperacionDiaria() : this(false) {}
        public FrmOperacionDiaria(int idSocio) : this(false){socioInicial=idSocio;AbrirSeccion(1);}
        public void AbrirCobrosSocio(int idSocio)
        {
            if(ocupado){Aviso("Esperá a que termine la operación y volvé a abrir el socio desde Saldos pendientes.",true);return;}
            socioInicial=idSocio;AbrirSeccion(1);_ = Ejecutar(CargarDatos);
        }
        internal FrmOperacionDiaria(bool vistaPrevia)
        {
            preview=vistaPrevia;
            if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            FormClosing+=(s,e)=>{if(ocupado && e.CloseReason==CloseReason.UserClosing){e.Cancel=true;Aviso("Esperá a que termine la operación antes de cerrar.",true);}};
            Text="Operación diaria";Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;ClientSize=new Size(1180,740);MinimumSize=new Size(930,640);AutoScaleMode=AutoScaleMode.Font;
            errores.ContainerControl=this;
            desde.Value=DateTime.Today;hasta.Value=DateTime.Today.AddMonths(1);inicio.Value=DateTime.Today.AddDays(1).AddHours(18);fin.Value=inicio.Value.AddHours(1);
            medio.Items.AddRange(new object[]{"Efectivo","Transferencia","Tarjeta"});medio.SelectedIndex=0;
            var layout=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=4,Padding=new Padding(18,0,18,0)};
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute,83));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,62));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,38));Controls.Add(layout);
            var cabecera=new Panel {Dock=DockStyle.Fill};var title=TemaVisual.Etiqueta("Operación diaria",23,true);title.Location=new Point(0,12);cabecera.Controls.Add(title);
            var sub=TemaVisual.Etiqueta("Membresías, agenda y asistencia en un solo lugar.");sub.ForeColor=TemaVisual.Tenue;sub.Location=new Point(2,54);cabecera.Controls.Add(sub);layout.Controls.Add(cabecera,0,0);
            var filtro=new FlowLayoutPanel {Dock=DockStyle.Fill,BackColor=Color.White,Padding=new Padding(12,8,8,6),WrapContents=false};
            var socioLabel=TemaVisual.Etiqueta("Socio",10,true);socioLabel.Margin=new Padding(0,8,12,0);filtro.Controls.Add(socioLabel);socio.Width=280;socio.Margin=new Padding(0,5,12,0);socio.DisplayMember="NombreCompleto";socio.ValueMember="Id_Socio";filtro.Controls.Add(socio);
            var buscarSocio=SelectorBuscable.Boton(socio,"socio","Buscar socio",async()=>await Ejecutar(CargarDatos));buscarSocio.Width=125;buscarSocio.Height=38;filtro.Controls.Add(buscarSocio);
            var todos=TemaVisual.Boton("Ver todos");todos.Width=110;todos.Click+=async(s,e)=>await Ejecutar(async()=>{socio.SelectedIndex=-1;await CargarDatos();});filtro.Controls.Add(todos);
            actualizar.Width=115;actualizar.Click+=async(s,e)=>await Ejecutar(CargarDatos);filtro.Controls.Add(actualizar);layout.Controls.Add(filtro,0,1);
            layout.Controls.Add(tabs,0,2);layout.Controls.Add(estado,0,3);
            CrearResumen();CrearMembresias();CrearSesiones();CrearReservas();CrearPagos();
            tabs.Selected+=(s,e)=>BeginInvoke(new Action(()=>{foreach(var g in new[]{membresias,sesiones,reservas,pagos}){g.ClearSelection();g.CurrentCell=null;}ActualizarAcciones();}));
            socio.SelectionChangeCommitted+=async(s,e)=>await Ejecutar(CargarDatos);
            membresias.SelectionChanged+=(s,e)=>{pago.Value=0;if(editorMembresias!=null)editorMembresias.SelectedIndex=Fila(membresias)!=null?1:0;ActualizarAcciones();};sesiones.SelectionChanged+=(s,e)=>ActualizarAcciones();reservas.SelectionChanged+=(s,e)=>ActualizarAcciones();
            foreach(var grid in new[]{membresias,sesiones,reservas,pagos})
            {
                grid.DataBindingComplete+=(s,e)=>{Formato((DataGridView)s);((DataGridView)s).ClearSelection();((DataGridView)s).CurrentCell=null;ActualizarAcciones();};
                grid.CellFormatting+=ColorearEstado;
            }
            Shown+=async(s,e)=>await Ejecutar(CargarDatos);
            if(preview){CargarMuestra();ActualizarAcciones();}
        }
        protected override void Dispose(bool disposing){if(disposing)errores.Dispose();base.Dispose(disposing);}
        public void AbrirSeccion(int index){if(index>=0 && index<tabs.TabCount)tabs.SelectedIndex=index;}
        static Panel Campo(string titulo,Control control){return TemaVisual.Campo(titulo,control);}
        TableLayoutPanel Editor(string title,string description)
        {
            var p=new TableLayoutPanel {ColumnCount=1,Dock=DockStyle.Top,AutoSize=true,BackColor=Color.White,Padding=new Padding(16),Margin=new Padding(0,0,0,12)};
            p.Controls.Add(TemaVisual.Etiqueta(title,13,true));
            var help=TemaVisual.Etiqueta(description,9);help.ForeColor=TemaVisual.Tenue;help.MaximumSize=new Size(300,0);help.Margin=new Padding(0,3,0,15);p.Controls.Add(help);return p;
        }
        static void Agregar(TableLayoutPanel panel,Control control){control.Dock=DockStyle.Top;control.Margin=new Padding(0,3,0,8);panel.Controls.Add(control);}
        TabPage Pagina(string name,DataGridView grid,out FlowLayoutPanel lateral)
        {
            grid.AccessibleName="Listado de "+name;
            var page=new TabPage(name){BackColor=TemaVisual.Fondo,Padding=new Padding(12)};tabs.TabPages.Add(page);
            var split=new SplitContainer {Dock=DockStyle.Fill,Size=new Size(1100,580),Panel1MinSize=300,Panel2MinSize=315,SplitterWidth=12,SplitterDistance=735,BackColor=TemaVisual.Fondo};
            var listado=new Panel {Dock=DockStyle.Fill,BackColor=Color.White,Padding=new Padding(12)};listado.Controls.Add(grid);split.Panel1.Controls.Add(listado);
            lateral=new FlowLayoutPanel {Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,BackColor=TemaVisual.Fondo,Padding=new Padding(0)};
            lateral.SizeChanged+=(s,e)=>{foreach(Control c in ((FlowLayoutPanel)s).Controls)c.Width=Math.Max(270,((FlowLayoutPanel)s).ClientSize.Width-24);};split.Panel2.Controls.Add(lateral);page.Controls.Add(split);return page;
        }
        TabControl ReemplazarLateral(FlowLayoutPanel lateral)
        {
            Control parent=lateral.Parent;parent.Controls.Remove(lateral);lateral.Dispose();
            var editores=new TabControl {Dock=DockStyle.Fill,Font=new Font("Segoe UI",9),Padding=new Point(10,9)};parent.Controls.Add(editores);return editores;
        }
        void PaginaEditor(TabControl tabsEditor,string titulo,TableLayoutPanel editor)
        {
            var page=new TabPage(titulo) {BackColor=Color.White,Padding=new Padding(2)};
            var scroll=new Panel {Dock=DockStyle.Fill,AutoScroll=true,BackColor=Color.White};
            var principal=editor.Controls.OfType<Button>().FirstOrDefault(b=>Convert.ToString(b.Tag)=="primario");
            if(principal!=null)
            {
                editor.Controls.Remove(principal);principal.Dock=DockStyle.Fill;principal.Margin=new Padding(0);
                var footer=new Panel {Dock=DockStyle.Bottom,Height=65,Padding=new Padding(16,12,16,13),BackColor=Color.White};footer.Controls.Add(principal);
                page.Controls.Add(scroll);page.Controls.Add(footer);
            }
            else page.Controls.Add(scroll);
            editor.Dock=DockStyle.Top;editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            Action ajustar=()=>{
                int ancho=Math.Max(220,scroll.ClientSize.Width-SystemInformation.VerticalScrollBarWidth-4);
                editor.MaximumSize=new Size(ancho,0);editor.Width=ancho;
                foreach(Control control in editor.Controls)
                {
                    if(control is Label)((Label)control).MaximumSize=new Size(Math.Max(150,ancho-editor.Padding.Horizontal-3),0);
                    if(control is Panel)control.Width=Math.Max(150,ancho-editor.Padding.Horizontal-3);
                }
            };
            scroll.Controls.Add(editor);scroll.SizeChanged+=(s,e)=>ajustar();tabsEditor.TabPages.Add(page);ajustar();
        }
        void CrearResumen()
        {
            var page=new TabPage("Resumen"){BackColor=TemaVisual.Fondo,Padding=new Padding(16)};tabs.TabPages.Add(page);
            var stack=new TableLayoutPanel {Dock=DockStyle.Top,AutoSize=true,ColumnCount=1};page.Controls.Add(stack);
            var titulo=TemaVisual.Etiqueta("Así está tu gimnasio hoy",18,true);titulo.Margin=new Padding(0,9,0,4);stack.Controls.Add(titulo);
            var fecha=TemaVisual.Etiqueta(DateTime.Today.ToString("dddd d 'de' MMMM"),10);fecha.ForeColor=TemaVisual.Tenue;fecha.Margin=new Padding(0,0,0,22);stack.Controls.Add(fecha);
            var tarjetas=new TableLayoutPanel {Dock=DockStyle.Top,Height=140,ColumnCount=4,RowCount=1};for(int i=0;i<4;i++)tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));
            string[] nombres={"Vencen en 7 días","Saldo pendiente","Próximas sesiones","En lista de espera"};
            for(int i=0;i<4;i++)
            {
                var tarjeta=new Panel {Dock=DockStyle.Fill,BackColor=Color.White,Margin=new Padding(0,0,12,12),Padding=new Padding(18)};
                var l=TemaVisual.Etiqueta(nombres[i],10);l.ForeColor=TemaVisual.Tenue;l.Dock=DockStyle.Top;
                valores[i]=TemaVisual.Etiqueta("—",27,true);valores[i].ForeColor=i==1?TemaVisual.Azul:TemaVisual.Primario;valores[i].Dock=DockStyle.Bottom;
                tarjeta.Controls.Add(l);tarjeta.Controls.Add(valores[i]);tarjetas.Controls.Add(tarjeta,i,0);
            }
            stack.Controls.Add(tarjetas);
            var card=Editor("¿Qué necesitás hacer?","Elegí una tarea. Para crear membresías o reservar, seleccioná primero un socio en el filtro superior.");card.Margin=new Padding(0,16,0,0);
            var acciones=new FlowLayoutPanel {AutoSize=true,Dock=DockStyle.Top,WrapContents=true};
            foreach(var par in new[]{Tuple.Create("Gestionar membresías",1),Tuple.Create("Programar o reservar",2),Tuple.Create("Tomar asistencia",3)})
            {int indice=par.Item2;var b=TemaVisual.Boton(par.Item1,true);b.Width=210;b.Click+=(s,e)=>AbrirSeccion(indice);acciones.Controls.Add(b);}
            card.Controls.Add(acciones);stack.Controls.Add(card);
            var nota=TemaVisual.Etiqueta("Los indicadores son generales. El filtro de socio se aplica a membresías, pagos y reservas.",9);nota.ForeColor=TemaVisual.Tenue;nota.Margin=new Padding(0,24,0,0);stack.Controls.Add(nota);
        }
        void CrearMembresias()
        {
            FlowLayoutPanel lateral;Pagina("Membresías",membresias,out lateral);
            editorMembresias=ReemplazarLateral(lateral);
            var alta=Editor("Nueva membresía","Creá el período y el importe para el socio seleccionado.");Agregar(alta,Campo("Nombre del plan",plan));Agregar(alta,Campo("Vigente desde",desde));Agregar(alta,Campo("Vigente hasta",hasta));Agregar(alta,Campo("Importe de la membresía",importe));
            var crear=Accion("Crear membresía",async()=>{
                errores.Clear();if(!Validar(socio,SocioId()>0,"Elegí un socio.") || !Validar(plan,!string.IsNullOrWhiteSpace(plan.Text),"Ingresá el nombre del plan.") || !Validar(hasta,hasta.Value.Date>=desde.Value.Date,"La fecha final debe ser posterior al inicio."))return;
                int id=SocioId();string nombre=plan.Text.Trim();DateTime d=desde.Value,h=hasta.Value;decimal valor=importe.Value;
                await Task.Run(()=>negocio.Membresia(id,nombre,d,h,valor));await CargarDatos();plan.Clear();importe.Value=0;Aviso("Membresía creada. Seleccionala para registrar un pago.");
            },true);Agregar(alta,crear);PaginaEditor(editorMembresias,"Nueva membresía",alta);
            var caja=Editor("Registrar un pago","Seleccioná la membresía a cobrar en la tabla.");cuota.MaximumSize=new Size(290,0);Agregar(caja,cuota);Agregar(caja,Campo("Importe a cobrar",pago));
            completar.Click+=(s,e)=>{if(Fila(membresias)!=null)pago.Value=Math.Min(pago.Maximum,Math.Max(0,Convert.ToDecimal(Fila(membresias).Cells["Saldo"].Value)));};Agregar(caja,completar);
            Agregar(caja,Campo("Medio de pago",medio));Agregar(caja,Campo("Referencia / comprobante (opcional)",referencia));
            cobrar.Click+=async(s,e)=>await Ejecutar(async()=>{
                if(!Validar(pago,pago.Value>0,"Ingresá el importe a cobrar."))return;
                int id=Id(membresias);decimal valor=pago.Value;string m=medio.Text,refe=referencia.Text.Trim();await Task.Run(()=>negocio.Pago(id,valor,m,refe));await CargarDatos();pago.Value=0;referencia.Clear();Aviso("Pago registrado correctamente.");
            });Agregar(caja,cobrar);PaginaEditor(editorMembresias,"Registrar pago",caja);
        }
        void CrearSesiones()
        {
            FlowLayoutPanel lateral;Pagina("Agenda y cupos",sesiones,out lateral);
            editorSesiones=ReemplazarLateral(lateral);
            var reserva=Editor("Reservar un lugar","Seleccioná una sesión en la tabla y un socio en el filtro superior.");sesionElegida.MaximumSize=new Size(290,0);Agregar(reserva,sesionElegida);
            reservar.Click+=async(s,e)=>await Ejecutar(async()=>{int id=Id(sesiones),socioId=SocioId();if(!Validar(socio,socioId>0,"Elegí el socio que va a reservar."))return;string mensaje=await Task.Run(()=>negocio.Reservar(id,socioId));await CargarDatos();Aviso(mensaje);});Agregar(reserva,reservar);
            cancelarSesion.Click+=async(s,e)=>await Ejecutar(async()=>{int id=Id(sesiones);if(MessageBox.Show(this,"Se cancelarán la sesión seleccionada y sus reservas. ¿Continuar?","Cancelar sesión",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;await Task.Run(()=>negocio.CancelarSesion(id));await CargarDatos();Aviso("Sesión y reservas canceladas.");});Agregar(reserva,cancelarSesion);PaginaEditor(editorSesiones,"Reservar",reserva);
            var agenda=Editor("Programar actividades","Elegí días de la semana y período. Revisá todas las sesiones antes de guardarlas.");
            var abrir=TemaVisual.Boton("Abrir planificación",true);abrir.Click+=(s,e)=>{if(preview)return;if(MdiParent!=null)new CapaIU.Presentacion.NavegadorMdi(MdiParent).Abrir(()=>new FrmPlanificacion());else new FrmPlanificacion().Show(this);};Agregar(agenda,abrir);PaginaEditor(editorSesiones,"Programar",agenda);
        }
        void CrearReservas()
        {
            FlowLayoutPanel lateral;Pagina("Reservas y asistencia",reservas,out lateral);
            var acciones=Editor("Gestionar reserva","Seleccioná una reserva. La asistencia se habilita cuando la sesión ya comenzó.");
            presente.Click+=async(s,e)=>await Ejecutar(()=>MarcarAsistencia(true));ausente.Click+=async(s,e)=>await Ejecutar(()=>MarcarAsistencia(false));
            cancelarReserva.Click+=async(s,e)=>await Ejecutar(async()=>{int id=Id(reservas);if(MessageBox.Show(this,"¿Cancelar la reserva seleccionada? Si hay lista de espera, se asignará el lugar al primer socio elegible.","Cancelar reserva",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;await Task.Run(()=>negocio.CancelarReserva(id));await CargarDatos();Aviso("Reserva cancelada.");});
            Agregar(acciones,presente);Agregar(acciones,ausente);Agregar(acciones,cancelarReserva);lateral.Controls.Add(acciones);
        }
        void CrearPagos()
        {
            FlowLayoutPanel lateral;Pagina("Pagos",pagos,out lateral);
            var ayuda=Editor("Historial de cobros","Consultá fecha, medio, importe y referencia de los pagos. Elegí un socio arriba para ver solo sus movimientos.");
            var b=TemaVisual.Boton("Ir a membresías",true);b.Click+=(s,e)=>AbrirSeccion(1);Agregar(ayuda,b);lateral.Controls.Add(ayuda);
        }
        async Task MarcarAsistencia(bool asistio){int id=Id(reservas);await Task.Run(()=>negocio.Asistencia(id,asistio));await CargarDatos();Aviso(asistio?"Asistencia registrada: presente.":"Asistencia registrada: ausente.");}
        Button Accion(string text,Func<Task> action,bool primary=false){var b=TemaVisual.Boton(text,primary);b.Click+=async(s,e)=>await Ejecutar(action);if(preview)b.Enabled=false;return b;}
        bool Validar(Control c,bool condicion,string text){errores.SetError(c,condicion?"":text);if(!condicion){c.Focus();Aviso(text,true);}return condicion;}
        static DataGridViewRow Fila(DataGridView g){return g.SelectedRows.Count==1?g.SelectedRows[0]:null;}
        static int Id(DataGridView g){var f=Fila(g);if(f==null)throw new ArgumentException("Seleccioná una fila en la tabla.");return Convert.ToInt32(f.Cells["Id"].Value);}
        int SocioId(){return socio.SelectedValue is int?(int)socio.SelectedValue:0;}
        void Aviso(string text,bool error=false){estado.Text=text;estado.ForeColor=error?TemaVisual.Rojo:TemaVisual.Primario;estado.AccessibleName=text;}
        async Task Ejecutar(Func<Task> action)
        {
            if(ocupado)return;if(preview){Aviso("Vista previa con datos ficticios. Las operaciones están deshabilitadas.");return;}
            ocupado=true;socio.Enabled=false;tabs.Enabled=false;actualizar.Enabled=false;UseWaitCursor=true;
            try{await action();}
            catch(ArgumentException e){Aviso(e.Message,true);}
            catch(InvalidOperationException e){Aviso(e.Message,true);}
            catch(UnauthorizedAccessException ex){Aviso(ex.Message,true);}
            catch(Exception e){Aviso("No se pudo completar la operación. Revisá el aviso de error.",true);Program.Error(e);}
            finally{ocupado=false;socio.Enabled=true;tabs.Enabled=true;actualizar.Enabled=true;UseWaitCursor=false;ActualizarAcciones();}
        }
        async Task CargarDatos()
        {
            Aviso("Actualizando información…");int id=socioInicial>0?socioInicial:SocioId();socioInicial=0;int seleccionClase=clase.SelectedValue is int?(int)clase.SelectedValue:0;
            var datos=await Task.Run(()=>new object[]{new SocioBLL().Listar(),new ClaseBLL().ListarClase(),negocio.Listar("Membresias",id),negocio.Listar("Sesiones"),negocio.Listar("Reservas",id),negocio.Listar("Pagos",id),negocio.Listar("Panel")});
            socio.DataSource=(List<Socio>)datos[0];socio.SelectedValue=id;if(id==0)socio.SelectedIndex=-1;
            clase.DataSource=(List<Clase>)datos[1];if(seleccionClase>0)clase.SelectedValue=seleccionClase;
            Enlazar((DataTable)datos[2],(DataTable)datos[3],(DataTable)datos[4],(DataTable)datos[5],(DataTable)datos[6]);
            Aviso(((DataTable)datos[2]).Rows.Count==0 && id>0?"Este socio todavía no tiene membresías. Podés crear una desde el panel derecho.":"Información actualizada. Seleccioná una fila para ver las acciones disponibles.");
        }
        void Enlazar(DataTable m,DataTable s,DataTable r,DataTable p,DataTable resumen)
        {
            membresias.DataSource=m;sesiones.DataSource=s;reservas.DataSource=r;pagos.DataSource=p;
            for(int i=0;i<4;i++)if(resumen.Rows.Count>i){decimal value=Convert.ToDecimal(resumen.Rows[i]["Valor"]);valores[i].Text=i==1?value.ToString("C0"):value.ToString("N0");}
            ActualizarAcciones();
        }
        void ActualizarAcciones()
        {
            var m=Fila(membresias);decimal saldo=m==null?0:Convert.ToDecimal(m.Cells["Saldo"].Value);
            cuota.Text=m==null?"Seleccioná una membresía en la tabla.":Convert.ToString(m.Cells["Socio"].Value)+"\nTotal: "+Convert.ToDecimal(m.Cells["Importe"].Value).ToString("C2")+"\nAbonado: "+Convert.ToDecimal(m.Cells["Pagado"].Value).ToString("C2")+"\nPendiente: "+saldo.ToString("C2");
            cobrar.Enabled=completar.Enabled=!preview && !ocupado && m!=null && saldo>0;
            var s=Fila(sesiones);bool futura=s!=null && Convert.ToString(s.Cells["Estado"].Value)=="Programada" && Convert.ToDateTime(s.Cells["Inicio"].Value)>DateTime.Now;
            sesionElegida.Text=s==null?"Seleccioná una sesión en la tabla.":Convert.ToString(s.Cells["Clase"].Value)+"\n"+Convert.ToDateTime(s.Cells["Inicio"].Value).ToString("dd/MM/yyyy HH:mm")+" · Lugares: "+s.Cells["Disponibles"].Value;
            reservar.Enabled=!preview && !ocupado && futura && SocioId()>0;cancelarSesion.Enabled=!preview && !ocupado && futura;
            var r=Fila(reservas);bool confirmada=r!=null && Convert.ToString(r.Cells["Estado"].Value)=="Confirmada";
            presente.Enabled=ausente.Enabled=!preview && !ocupado && confirmada && Convert.ToDateTime(r.Cells["Inicio"].Value)<=DateTime.Now;
            cancelarReserva.Enabled=!preview && !ocupado && r!=null && Convert.ToString(r.Cells["Estado"].Value)!="Cancelada" && Convert.ToDateTime(r.Cells["Inicio"].Value)>DateTime.Now;
        }
        void Formato(DataGridView grid)
        {
            var nombres=new Dictionary<string,string>{{"Id","#"},{"Id_Membresia","Membresía"},{"Desde","Desde"},{"Hasta","Vence"},{"Importe","Importe"},{"Pagado","Pagado"},{"Saldo","Saldo"},{"Inicio","Inicio"},{"Fin","Fin"},{"Capacidad","Cupo"},{"Inscriptos","Reservados"},{"Disponibles","Libres"},{"EnEspera","Espera"},{"Asistio","Presente"},{"CreadoUtc","Fecha del pago"}};
            grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            foreach(DataGridViewColumn c in grid.Columns)
            {
                if(c.Name=="Id_Clase" || c.Name=="Id_Socio" || c.Name=="Id" || (grid==membresias && (c.Name=="Desde" || c.Name=="Pagado")) || (grid==sesiones && (c.Name=="Fin" || c.Name=="Inscriptos"))){c.Visible=false;continue;}if(nombres.ContainsKey(c.Name))c.HeaderText=nombres[c.Name];
                c.MinimumWidth=c.Name=="Clase"?195:c.Name=="Socio"?125:c.Name=="Importe" || c.Name=="Pagado" || c.Name=="Saldo"?100:c.Name=="Inicio" || c.Name=="Fin"?150:c.Name=="Desde" || c.Name=="Hasta"?110:c.Name=="Estado"?95:65;
                c.FillWeight=c.MinimumWidth;
                if(c.Name=="Importe" || c.Name=="Pagado" || c.Name=="Saldo"){c.DefaultCellStyle.Format="C2";c.DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleRight;}
                if(c.ValueType==typeof(DateTime))c.DefaultCellStyle.Format=c.Name=="Desde" || c.Name=="Hasta"?"dd/MM/yyyy":"dd/MM/yyyy HH:mm";
            }
        }
        void ColorearEstado(object sender,DataGridViewCellFormattingEventArgs e)
        {
            var grid=(DataGridView)sender;if(e.Value==null || e.RowIndex<0)return;
            if(grid.Columns[e.ColumnIndex].Name=="Estado")
            {
                string value=Convert.ToString(e.Value);e.CellStyle.ForeColor=value=="Cancelada"?TemaVisual.Tenue:value=="Espera"?Color.FromArgb(156,104,13):TemaVisual.Primario;
                e.CellStyle.Font=TemaVisual.Semi;
            }
            if(e.Value is DateTime && grid.Columns[e.ColumnIndex].Name!="CreadoUtc")
            {string col=grid.Columns[e.ColumnIndex].Name;e.Value=((DateTime)e.Value).ToString(col=="Desde" || col=="Hasta"?"dd/MM/yyyy":"dd/MM/yy HH:mm");e.FormattingApplied=true;}
            if(grid.Columns[e.ColumnIndex].Name=="CreadoUtc" && e.Value is DateTime)
            {e.Value=DateTime.SpecifyKind((DateTime)e.Value,DateTimeKind.Utc).ToLocalTime().ToString("dd/MM/yyyy HH:mm");e.FormattingApplied=true;}
        }
        static DataTable Tabla(string[] columnas,params object[][] filas){var t=new DataTable();foreach(var c in columnas)t.Columns.Add(c,typeof(object));foreach(var f in filas)t.Rows.Add(f);return t;}
        void CargarMuestra()
        {
            socio.DataSource=new List<Socio>{new Socio {Id_Socio=1,Nombre="Alex",Apellido="Ejemplo"},new Socio {Id_Socio=2,Nombre="Sam",Apellido="Muestra"}};socio.SelectedIndex=-1;
            clase.DataSource=new List<Clase>{new Clase {Id_Clase=1,Descripcion="Entrenamiento funcional"}};
            Enlazar(Tabla(new[]{"Id","Id_Socio","Socio","Plan","Desde","Hasta","Importe","Pagado","Saldo"},new object[]{101,1,"Alex Ejemplo","Mensual",DateTime.Today,DateTime.Today.AddMonths(1),25000m,15000m,10000m},new object[]{102,2,"Sam Muestra","Mensual",DateTime.Today,DateTime.Today.AddMonths(1),25000m,25000m,0m}),
            Tabla(new[]{"Id","Clase","Inicio","Fin","Capacidad","Inscriptos","Disponibles","EnEspera","Estado"},new object[]{21,"Entrenamiento funcional",inicio.Value,fin.Value,20,16,4,0,"Programada"},new object[]{22,"Movilidad",inicio.Value.AddHours(2),fin.Value.AddHours(2),12,12,0,3,"Programada"}),
            Tabla(new[]{"Id","Id_Socio","Socio","Clase","Inicio","Estado","Asistio"},new object[]{41,1,"Alex Ejemplo","Entrenamiento funcional",inicio.Value,"Confirmada",DBNull.Value},new object[]{42,2,"Sam Muestra","Movilidad",inicio.Value.AddHours(2),"Espera",DBNull.Value}),
            Tabla(new[]{"Id","Id_Membresia","Socio","Importe","Medio","Referencia","CreadoUtc"},new object[]{81,101,"Alex Ejemplo",15000m,"Transferencia","Pago de prueba",DateTime.UtcNow}),
            Tabla(new[]{"Indicador","Valor"},new object[]{"Vencimientos",8},new object[]{"Saldo",85000m},new object[]{"Sesiones",12},new object[]{"Espera",3}));
            Aviso("Vista previa con datos ficticios. No se consulta ni modifica la base.");
        }
    }
}
