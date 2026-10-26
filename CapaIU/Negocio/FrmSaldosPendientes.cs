using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaIU.Controles;
using CapaIU.Presentacion;

namespace CapaIU.Negocio
{
    public sealed class FrmSaldosPendientes : PantallaGestion
    {
        readonly bool preview;
        readonly Action<int> cobrar;
        readonly DataGridView grilla=TemaVisual.Grilla();
        readonly TextBox buscar=TemaVisual.Texto(100);
        readonly Label resumen=TemaVisual.Etiqueta("Actualizando saldos…",12,true);
        readonly Button abrir=TemaVisual.Boton("Ver membresías / cobrar",true);
        DataTable datos;
        public FrmSaldosPendientes(Action<int> cobrar):this(cobrar,false){}
        internal FrmSaldosPendientes(Action<int> cobrar,bool preview)
        {
            this.cobrar=cobrar;this.preview=preview;
            if(!preview)Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            Text="Socios con saldo pendiente";
            var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4};
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute,84));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,62));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,42));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));Cuerpo.Controls.Add(layout);
            layout.Controls.Add(Cabecera(Text,"Una fila por socio. Incluye saldos de membresías vencidas, actuales y futuras."),0,0);
            var barra=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};barra.Controls.Add(TemaVisual.Campo("Buscar por nombre o número de socio",buscar));barra.Controls[0].Width=340;
            var actualizar=TemaVisual.Boton("Actualizar");actualizar.Click+=async(s,e)=>await Ejecutar(Cargar);barra.Controls.Add(actualizar);abrir.Width=230;abrir.Enabled=false;abrir.Click+=(s,e)=>Abrir();barra.Controls.Add(abrir);layout.Controls.Add(barra,0,1);
            layout.Controls.Add(resumen,0,2);layout.Controls.Add(grilla,0,3);grilla.AccessibleName="Socios con saldo pendiente";
            buscar.TextChanged+=(s,e)=>Filtrar();grilla.SelectionChanged+=(s,e)=>abrir.Enabled=grilla.SelectedRows.Count==1 && !preview && cobrar!=null;
            grilla.CellDoubleClick+=(s,e)=>{if(e.RowIndex>=0)Abrir();};Shown+=async(s,e)=>await Ejecutar(Cargar);
            Activated+=async(s,e)=>{if(datos!=null && !preview)await Ejecutar(Cargar);};
        }
        void Abrir(){if(abrir.Enabled)cobrar(Convert.ToInt32(grilla.SelectedRows[0].Cells["Id_Socio"].Value));}
        async Task Cargar()
        {
            var ds=preview?MonitoreoMuestra.Inicio():await Task.Run(()=>new MonitoreoBLL().Inicio());
            datos=ds.Tables["Pendientes"];Filtrar();Aviso(preview?"Vista previa: datos ficticios, cobros deshabilitados.":"Saldos actualizados. Elegí un socio para consultar sus membresías y registrar un pago.");
        }
        void Filtrar()
        {
            if(datos==null)return;
            var vista=datos.Clone();string texto=buscar.Text.Trim();
            foreach(DataRow fila in datos.Rows)
            if(System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(Convert.ToString(fila["Socio"]),texto,System.Globalization.CompareOptions.IgnoreCase|System.Globalization.CompareOptions.IgnoreNonSpace)>=0 || Convert.ToString(fila["Id_Socio"]).Contains(texto))vista.ImportRow(fila);
            grilla.DataSource=vista;grilla.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns["Id_Socio"].HeaderText="Nº socio";grilla.Columns["MembresiasPendientes"].HeaderText="Planes con saldo";grilla.Columns["PrimerVencimiento"].HeaderText="Primer vencimiento";
            grilla.Columns["Saldo"].DefaultCellStyle.Format="C2";grilla.Columns["PrimerVencimiento"].DefaultCellStyle.Format="dd/MM/yyyy";
            grilla.Columns["Socio"].FillWeight=180;grilla.ClearSelection();grilla.CurrentCell=null;abrir.Enabled=false;
            decimal suma=0;foreach(DataRow row in vista.Rows)suma+=Convert.ToDecimal(row["Saldo"]);
            resumen.Text=vista.Rows.Count+" socios en este listado · Saldo: "+suma.ToString("C2");
        }
    }
}
