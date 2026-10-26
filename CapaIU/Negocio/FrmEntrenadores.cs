using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaDominio;
using CapaIU.Controles;
using CapaIU.Presentacion;
namespace CapaIU.Negocio
{
    public sealed class FrmEntrenadores : Form
    {
        readonly EntrenadoresPresentador presentador;
        readonly DataGridView grilla = TemaVisual.Grilla();
        readonly TextBox buscar = TemaVisual.Texto(100), nombre = TemaVisual.Texto(50), apellido = TemaVisual.Texto(50);
        readonly Button guardar = TemaVisual.Boton("Crear entrenador", true), eliminar = TemaVisual.Boton("Eliminar", false, true);
        readonly Label titulo = TemaVisual.Etiqueta("Nuevo entrenador", 15, true), estado = TemaVisual.Etiqueta("");
        readonly TableLayoutPanel contenido = new TableLayoutPanel();
        int? seleccionado; bool ocupado;
        public FrmEntrenadores() : this(new ServicioEntrenadores(), false) { }
        internal FrmEntrenadores(IServicioEntrenadores servicio, bool vistaPrevia)
        {
            if(!vistaPrevia) Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            presentador = new EntrenadoresPresentador(servicio);
            FormClosing+=(s,e)=>{if(ocupado && e.CloseReason==CloseReason.UserClosing){e.Cancel=true;Aviso("Esperá a que termine la operación antes de cerrar.",true);}};
            Text = "Entrenadores"; Font = TemaVisual.Fuente; BackColor = TemaVisual.Fondo;
            ClientSize = new Size(1100, 700); MinimumSize = new Size(900, 600);
            contenido.Dock = DockStyle.Fill; contenido.Padding = new Padding(20); contenido.ColumnCount = 1; contenido.RowCount = 4;
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 82)); contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 42)); Controls.Add(contenido);
            var cabecera = new Panel { Dock = DockStyle.Fill }; cabecera.Controls.Add(TemaVisual.Etiqueta("Entrenadores", 23, true));
            var sub = TemaVisual.Etiqueta("Administrá el equipo y mantené sus datos actualizados."); sub.Top = 44; cabecera.Controls.Add(sub); contenido.Controls.Add(cabecera, 0, 0);
            var barra = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, BackColor = Color.White, Padding = new Padding(10) };
            barra.Controls.Add(TemaVisual.Etiqueta("Buscar")); buscar.Width = 250; barra.Controls.Add(buscar);
            var nuevo = TemaVisual.Boton("Nuevo", true); nuevo.Width = 105; nuevo.Click += (s,e) => Nuevo(); barra.Controls.Add(nuevo);
            var actualizar = TemaVisual.Boton("Actualizar"); actualizar.Width = 110; actualizar.Click += async(s,e) => await Ejecutar(Cargar); barra.Controls.Add(actualizar); contenido.Controls.Add(barra,0,1);
            var split = new SplitContainer { Dock = DockStyle.Fill, Size = new Size(1060,450), SplitterDistance = 690, Panel1MinSize = 300, Panel2MinSize = 300, SplitterWidth = 12 };
            grilla.AutoGenerateColumns = false; grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var c in new[] { new[] {"Id_Entrenador", "N.º"}, new[] {"Nombre", "Nombre"}, new[] {"Apellido", "Apellido"} }) grilla.Columns.Add(new DataGridViewTextBoxColumn {DataPropertyName=c[0], HeaderText=c[1], Name=c[0]});
            split.Panel1.BackColor = Color.White; split.Panel1.Padding = new Padding(12); split.Panel1.Controls.Add(grilla);
            var editor = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1, Padding = new Padding(18), BackColor = Color.White };
            editor.Controls.Add(titulo); editor.Controls.Add(TemaVisual.Campo("Nombre", nombre)); editor.Controls.Add(TemaVisual.Campo("Apellido", apellido));
            guardar.Dock = DockStyle.Top; eliminar.Dock = DockStyle.Top; editor.Controls.Add(guardar); editor.Controls.Add(eliminar);
            split.Panel2.Resize += (s,e) => editor.Width=Math.Max(280,split.Panel2.ClientSize.Width-4);
            split.Panel2.AutoScroll = true; split.Panel2.Controls.Add(editor); contenido.Controls.Add(split,0,2); estado.Dock = DockStyle.Fill; estado.AutoSize = false; estado.TextAlign = ContentAlignment.MiddleLeft; contenido.Controls.Add(estado,0,3);
            buscar.TextChanged += (s,e) => Filtrar();
            grilla.CellClick += (s,e) => { if(e.RowIndex < 0) return; var x=grilla.Rows[e.RowIndex].DataBoundItem as Entrenador; if(x==null)return; seleccionado=x.Id_Entrenador; nombre.Text=x.Nombre; apellido.Text=x.Apellido; titulo.Text="Editar entrenador #"+seleccionado; guardar.Text="Guardar cambios"; eliminar.Enabled=true; };
            guardar.Click += async(s,e) => await Ejecutar(async()=>{await presentador.Guardar(seleccionado,nombre.Text,apellido.Text); await Cargar(); Aviso("Datos guardados.");});
            eliminar.Click += async(s,e) => { if(!seleccionado.HasValue)return; if(MessageBox.Show(this,"¿Eliminar a "+nombre.Text+" "+apellido.Text+"?", "Confirmar eliminación", MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return; await Ejecutar(async()=>{await presentador.Eliminar(seleccionado);await Cargar();Aviso("Entrenador eliminado.");}); };
            if(!vistaPrevia)eliminar.Visible=new PermisosBLL().BuscarPermisos(CapaServicios.Composite.TipoPermiso.PuedeEliminarEntrenadores,new CapaServicios.Composite.PermisoUsuario {idusuario=CapaServicios.SingletonSesion.Instancia.Usuario.idusuario});
            Shown += async(s,e) => await Ejecutar(Cargar); Nuevo();
        }
        void Filtrar() { var datos=presentador.Buscar(buscar.Text); grilla.DataSource=datos; grilla.ClearSelection(); grilla.CurrentCell=null; estado.Text=datos.Count+" entrenadores en la lista."; }
        void Nuevo(){seleccionado=null;nombre.Clear();apellido.Clear();titulo.Text="Nuevo entrenador";guardar.Text="Crear entrenador";eliminar.Enabled=false;grilla.ClearSelection();grilla.CurrentCell=null;nombre.Focus();}
        async Task Cargar(){await presentador.Cargar();Filtrar();Nuevo();}
        void Aviso(string texto,bool error=false){estado.Text=texto;estado.ForeColor=error?TemaVisual.Rojo:TemaVisual.Primario;}
        async Task Ejecutar(Func<Task> accion)
        {
            if(ocupado)return;ocupado=true;contenido.Enabled=false;UseWaitCursor=true;
            try{await accion();}
            catch(ArgumentException e){Aviso(e.Message,true);}
            catch(SqlException e){if(e.Number==547)Aviso("Tiene clases asociadas. Conservá su ficha para mantener el historial.",true);else{Aviso("No se pudo completar la operación.",true);Program.Error(e);}}
            catch(UnauthorizedAccessException ex){Aviso(ex.Message,true);}
            catch(Exception e){Aviso("No se pudo completar la operación.",true);Program.Error(e);}
            finally{ocupado=false;contenido.Enabled=true;UseWaitCursor=false;eliminar.Enabled=seleccionado.HasValue;}
        }
    }
}
