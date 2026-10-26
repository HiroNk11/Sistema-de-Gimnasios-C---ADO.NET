using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAplicacion;
using CapaIU.Controles;
using CapaIU.Presentacion;
namespace CapaIU
{
    public class FrmInicio : PantallaGestion
    {
        readonly Label[] valores=new Label[4];
        readonly DataGridView pendientes=TemaVisual.Grilla();
        readonly Button cobrar=TemaVisual.Boton("Ver membresías / cobrar",true);
        readonly bool preview;
        readonly Action<int> abrirCobros;
        bool cargado;
        internal FrmInicio(Action socios,Action operacion,bool negocio):this(socios,operacion,negocio,null){}
        internal FrmInicio(Action socios,Action operacion,bool negocio,Action altaUsuarios):this(socios,operacion,negocio,altaUsuarios,null,null,null,true){}
        internal FrmInicio(Action socios,Action operacion,bool negocio,Action altaUsuarios,Action calendario,Action saldos,Action<int> abrirCobros,bool preview=false)
        {
            this.preview=preview;this.abrirCobros=abrirCobros;
            Text="Inicio";FormBorderStyle=FormBorderStyle.None;ClientSize=new Size(1080,680);AutoScaleMode=AutoScaleMode.Font;
            if(!negocio)
            {
                var titulo=Cabecera("Administración de cuentas","Tu perfil permite dar de alta usuarios con sus roles definidos.");Cuerpo.Controls.Add(titulo);
                if(altaUsuarios!=null){var alta=TemaVisual.Boton("Crear usuario",true);alta.Location=new Point(20,120);alta.Width=230;alta.Click+=(s,e)=>altaUsuarios();Cuerpo.Controls.Add(alta);}
                return;
            }
            var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=5};
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute,82));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,110));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,65));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,45));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));Cuerpo.Controls.Add(layout);
            layout.Controls.Add(Cabecera("Tu gimnasio, de un vistazo",DateTime.Today.ToString("dddd d 'de' MMMM")+" · Indicadores generales y tareas de recepción."),0,0);
            var tarjetas=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=4,RowCount=1};
            string[] nombres={"Socios registrados","Socios con saldo","Saldo pendiente","Sesiones de hoy"};
            for(int i=0;i<4;i++)
            {
                tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));var card=new Panel{Dock=DockStyle.Fill,BackColor=Color.White,Padding=new Padding(14),Margin=new Padding(0,0,12,8)};
                var label=TemaVisual.Etiqueta(nombres[i],10);label.ForeColor=TemaVisual.Tenue;label.Dock=DockStyle.Top;
                valores[i]=TemaVisual.Etiqueta("—",23,true);valores[i].Dock=DockStyle.Bottom;valores[i].ForeColor=i==1 || i==2?TemaVisual.Azul:TemaVisual.Primario;
                card.Controls.Add(label);card.Controls.Add(valores[i]);tarjetas.Controls.Add(card,i,0);
                if(i==1 || i==2){card.Cursor=Cursors.Hand;card.Click+=(s,e)=>saldos?.Invoke();valores[i].Click+=(s,e)=>saldos?.Invoke();label.Click+=(s,e)=>saldos?.Invoke();}
            }
            layout.Controls.Add(tarjetas,0,1);
            var accesos=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};
            Action<string,Action,int> boton=(texto,accion,ancho)=>{var b=TemaVisual.Boton(texto);b.Width=ancho;b.Enabled=accion!=null;b.Click+=(s,e)=>accion();accesos.Controls.Add(b);};
            boton("Calendario semanal",calendario,185);boton("Saldos pendientes",saldos,175);boton("Socios",socios,100);boton("Operación diaria",operacion,165);
            var actualizar=TemaVisual.Boton("Actualizar");actualizar.Width=120;actualizar.Click+=async(s,e)=>await Ejecutar(Cargar);accesos.Controls.Add(actualizar);layout.Controls.Add(accesos,0,2);
            var barra=new Panel{Dock=DockStyle.Fill};var tituloPendientes=TemaVisual.Etiqueta("Socios con saldo pendiente",13,true);tituloPendientes.Location=new Point(0,8);barra.Controls.Add(tituloPendientes);cobrar.Dock=DockStyle.Right;cobrar.Width=230;cobrar.Enabled=false;cobrar.Click+=(s,e)=>Cobrar();barra.Controls.Add(cobrar);layout.Controls.Add(barra,0,3);
            layout.Controls.Add(pendientes,0,4);pendientes.AccessibleName="Socios con saldo pendiente desde Inicio";
            pendientes.SelectionChanged+=(s,e)=>cobrar.Enabled=pendientes.SelectedRows.Count==1 && !preview && abrirCobros!=null;
            pendientes.CellDoubleClick+=(s,e)=>{if(e.RowIndex>=0)Cobrar();};
            Shown+=async(s,e)=>await Ejecutar(Cargar);Activated+=async(s,e)=>{if(cargado && !preview)await Ejecutar(Cargar);};
        }
        void Cobrar(){if(cobrar.Enabled)abrirCobros(Convert.ToInt32(pendientes.SelectedRows[0].Cells["Id_Socio"].Value));}
        async Task Cargar()
        {
            var ds=preview?MonitoreoMuestra.Inicio():await Task.Run(()=>new MonitoreoBLL().Inicio());
            var r=ds.Tables["Resumen"].Rows[0];valores[0].Text=Convert.ToInt32(r["Socios"]).ToString("N0");valores[1].Text=Convert.ToInt32(r["SociosConSaldo"]).ToString("N0");valores[2].Text=Convert.ToDecimal(r["SaldoPendiente"]).ToString("C2");valores[3].Text=Convert.ToInt32(r["SesionesHoy"]).ToString("N0");
            pendientes.DataSource=ds.Tables["Pendientes"];pendientes.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;pendientes.Columns["Id_Socio"].HeaderText="Nº socio";pendientes.Columns["Socio"].FillWeight=180;pendientes.Columns["MembresiasPendientes"].HeaderText="Planes con saldo";pendientes.Columns["PrimerVencimiento"].HeaderText="Primer vencimiento";pendientes.Columns["Saldo"].DefaultCellStyle.Format="C2";pendientes.Columns["PrimerVencimiento"].DefaultCellStyle.Format="dd/MM/yyyy";pendientes.ClearSelection();pendientes.CurrentCell=null;cobrar.Enabled=false;cargado=true;
            Aviso(preview?"Vista previa con datos ficticios.":"Actualizado "+DateTime.Now.ToString("HH:mm")+" · "+(pendientes.Rows.Count==0?"No hay socios con saldo pendiente.":"Los saldos incluyen planes vencidos, actuales y futuros. Seleccioná un socio para cobrar."));
        }
    }
}
