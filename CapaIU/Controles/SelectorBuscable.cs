using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace CapaIU.Controles
{
    internal sealed class OpcionBusqueda
    {
        public int Indice {get;set;}
        public string Texto {get;set;}
        public string Clave {get;set;}
    }
    internal static class SelectorBuscable
    {
        sealed class Configuracion {public string Titulo;public Action Cambio;}
        static readonly ConditionalWeakTable<ComboBox,Configuracion> configuraciones=new ConditionalWeakTable<ComboBox,Configuracion>();
        public static void Habilitar(ComboBox combo,string titulo=null,Action cambio=null)
        {
            Configuracion config;
            if(!configuraciones.TryGetValue(combo,out config)){
                config=new Configuracion();configuraciones.Add(combo,config);
                combo.KeyDown+=(s,e)=>{if(e.KeyCode==Keys.F3 || (e.Control && e.KeyCode==Keys.F)){e.Handled=true;e.SuppressKeyPress=true;Abrir(combo);}};
            }
            if(titulo!=null)config.Titulo=titulo;
            if(cambio!=null)config.Cambio=cambio;
        }
        public static Button Boton(ComboBox combo,string titulo,string texto="…",Action cambio=null)
        {
            Habilitar(combo,titulo,cambio);
            var b=TemaVisual.Boton(texto);b.Width=34;b.Height=combo.Height;b.Margin=new Padding(0,5,8,0);b.AccessibleName="Buscar "+titulo;b.TabStop=true;
            b.Enabled=combo.Enabled;combo.EnabledChanged+=(s,e)=>b.Enabled=combo.Enabled;
            b.Click+=(s,e)=>Abrir(combo);return b;
        }
        internal static List<OpcionBusqueda> Opciones(ComboBox combo)
        {
            var resultado=new List<OpcionBusqueda>();
            for(int i=0;i<combo.Items.Count;i++){
                object item=combo.Items[i];string texto=combo.GetItemText(item);string id="";
                if(!string.IsNullOrEmpty(combo.ValueMember)){
                    var propiedad=TypeDescriptor.GetProperties(item).Find(combo.ValueMember,true);
                    if(propiedad!=null)id=Convert.ToString(propiedad.GetValue(item),CultureInfo.CurrentCulture);
                }
                string etiqueta=texto+(id.Length>0?" · N.º "+id:"");
                resultado.Add(new OpcionBusqueda {Indice=i,Texto=etiqueta,Clave=Normalizar(etiqueta)});
            }
            return resultado;
        }
        internal static string Normalizar(string valor){var s=(valor??"").Normalize(NormalizationForm.FormD);var b=new StringBuilder();foreach(char c in s)if(CharUnicodeInfo.GetUnicodeCategory(c)!=UnicodeCategory.NonSpacingMark)b.Append(char.ToLowerInvariant(c));return b.ToString().Normalize(NormalizationForm.FormC);}
        internal static List<OpcionBusqueda> Filtrar(IEnumerable<OpcionBusqueda> opciones,string texto){var palabras=Normalizar(texto).Split(new[]{' ','\t'},StringSplitOptions.RemoveEmptyEntries);return opciones.Where(x=>palabras.All(p=>x.Clave.Contains(p))).ToList();}
        static void Abrir(ComboBox combo)
        {
            if(combo.IsDisposed || !combo.Enabled)return;
            var config=configuraciones.GetOrCreateValue(combo);
            using(var dialogo=new DialogoBusqueda(config.Titulo??combo.AccessibleName??"opciones",Opciones(combo))){
                if(dialogo.ShowDialog(combo.FindForm())!=DialogResult.OK)return;
                int indice=dialogo.IndiceElegido;if(indice<0 || indice>=combo.Items.Count)return;
                bool distinto=combo.SelectedIndex!=indice;combo.SelectedIndex=indice;
                if(distinto && config.Cambio!=null)config.Cambio();
            }
        }
    }
    internal sealed class DialogoBusqueda : Form
    {
        readonly TextBox buscar=TemaVisual.Texto(150);
        readonly ListBox resultados=new ListBox {Dock=DockStyle.Fill,BorderStyle=BorderStyle.FixedSingle,IntegralHeight=false,ItemHeight=30,HorizontalScrollbar=true};
        readonly Label cantidad=TemaVisual.Etiqueta("");
        readonly Button elegir=TemaVisual.Boton("Seleccionar",true);
        readonly List<OpcionBusqueda> opciones;
        public int IndiceElegido {get;private set;}=-1;
        public DialogoBusqueda(string titulo,List<OpcionBusqueda> opciones)
        {
            this.opciones=opciones;Text="Buscar "+titulo;Font=TemaVisual.Fuente;BackColor=TemaVisual.Fondo;ClientSize=new Size(650,520);MinimumSize=new Size(500,400);StartPosition=FormStartPosition.CenterParent;ShowInTaskbar=false;MinimizeBox=false;
            var layout=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=5,Padding=new Padding(18)};
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute,34));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,42));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,30));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));Controls.Add(layout);
            layout.Controls.Add(TemaVisual.Etiqueta("Escribí parte del nombre o el número para buscar.",11,true),0,0);buscar.Dock=DockStyle.Fill;buscar.AccessibleName="Texto de búsqueda";layout.Controls.Add(buscar,0,1);layout.Controls.Add(cantidad,0,2);
            resultados.DisplayMember="Texto";resultados.Font=TemaVisual.Fuente;resultados.AccessibleName="Resultados de búsqueda";layout.Controls.Add(resultados,0,3);
            var acciones=new FlowLayoutPanel {Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,WrapContents=false};var cancelar=TemaVisual.Boton("Cancelar");cancelar.DialogResult=DialogResult.Cancel;acciones.Controls.Add(cancelar);acciones.Controls.Add(elegir);layout.Controls.Add(acciones,0,4);
            CancelButton=cancelar;AcceptButton=elegir;
            buscar.TextChanged+=(s,e)=>Actualizar();buscar.KeyDown+=(s,e)=>{if(e.KeyCode==Keys.Down && resultados.Items.Count>0){resultados.SelectedIndex=0;resultados.Focus();e.Handled=true;e.SuppressKeyPress=true;}};
            resultados.SelectedIndexChanged+=(s,e)=>elegir.Enabled=resultados.SelectedItem!=null;
            elegir.Click+=(s,e)=>Seleccionar();resultados.DoubleClick+=(s,e)=>{if(resultados.IndexFromPoint(resultados.PointToClient(Cursor.Position))>=0)Seleccionar();};
            Shown+=(s,e)=>buscar.Focus();Actualizar();
        }
        void Actualizar(){var lista=SelectorBuscable.Filtrar(opciones,buscar.Text);resultados.DataSource=lista;resultados.ClearSelected();elegir.Enabled=false;cantidad.Text=lista.Count==0?"Sin coincidencias. Probá con otro nombre o número.":lista.Count+" de "+opciones.Count+" resultados. Elegí uno para continuar.";}
        void Seleccionar(){var opcion=resultados.SelectedItem as OpcionBusqueda;if(opcion==null)return;IndiceElegido=opcion.Indice;DialogResult=DialogResult.OK;Close();}
    }
}
