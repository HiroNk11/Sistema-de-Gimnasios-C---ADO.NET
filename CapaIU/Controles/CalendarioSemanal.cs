using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaIU.Controles
{
    internal sealed class BloqueSemanal
    {
        public DataRow Sesion;
        public int Dia,Carril,Carriles;
        public DateTime Inicio,Fin;
        public Rectangle Rectangulo;
    }
    public sealed class CalendarioSemanal : ScrollableControl
    {
        const int Cabecera=42,Eje=56,AnchoDia=150,AltoHora=64;
        readonly ToolTip ayuda=new ToolTip();
        readonly List<BloqueSemanal> bloques=new List<BloqueSemanal>();
        DateTime lunes;
        int primeraHora=7,ultimaHora=22,seleccion;
        public event Action<int> SesionSeleccionada;
        public CalendarioSemanal()
        {
            DoubleBuffered=true;AutoScroll=true;BackColor=Color.White;TabStop=true;
            AccessibleName="Calendario semanal. Flechas para recorrer sesiones y Enter para ver participantes.";
            SetStyle(ControlStyles.Selectable,true);
        }
        protected override void Dispose(bool disposing){if(disposing)ayuda.Dispose();base.Dispose(disposing);}
        internal static List<BloqueSemanal> Distribuir(DataTable sesiones,DateTime lunes)
        {
            var resultado=new List<BloqueSemanal>();
            for(int dia=0;dia<7;dia++)
            {
                var desde=lunes.AddDays(dia);var hasta=desde.AddDays(1);
                var grupo=sesiones.AsEnumerable().Where(r=>r.Field<DateTime>("Inicio")<hasta && r.Field<DateTime>("Fin")>desde).OrderBy(r=>r.Field<DateTime>("Inicio")).ThenBy(r=>r.Field<int>("Id")).ToList();
                var activos=new List<BloqueSemanal>();var conjunto=new List<BloqueSemanal>();
                Action cerrar=()=>{if(conjunto.Count==0)return;int n=conjunto.Max(b=>b.Carril)+1;foreach(var b in conjunto)b.Carriles=n;conjunto.Clear();};
                foreach(var row in grupo)
                {
                    DateTime inicio=row.Field<DateTime>("Inicio")<desde?desde:row.Field<DateTime>("Inicio"),fin=row.Field<DateTime>("Fin")>hasta?hasta:row.Field<DateTime>("Fin");
                    activos.RemoveAll(b=>b.Fin<=inicio);if(activos.Count==0)cerrar();
                    int carril=0;while(activos.Any(b=>b.Carril==carril))carril++;
                    var bloque=new BloqueSemanal{Sesion=row,Dia=dia,Inicio=inicio,Fin=fin,Carril=carril};activos.Add(bloque);conjunto.Add(bloque);resultado.Add(bloque);
                }
                cerrar();
            }
            return resultado;
        }
        public void Mostrar(DataTable sesiones,DateTime inicioSemana)
        {
            lunes=inicioSemana;seleccion=0;bloques.Clear();bloques.AddRange(Distribuir(sesiones,lunes));
            primeraHora=bloques.Count==0?7:Math.Max(0,(int)Math.Floor(bloques.Min(b=>b.Inicio.TimeOfDay.TotalHours))-1);
            ultimaHora=bloques.Count==0?22:Math.Min(24,(int)Math.Ceiling(bloques.Max(b=>b.Fin.Date>b.Inicio.Date?24:b.Fin.TimeOfDay.TotalHours))+1);
            AutoScrollMinSize=new Size(Eje+7*AnchoDia,Cabecera+(ultimaHora-primeraHora)*AltoHora+10);
            foreach(var b in bloques)
            {
                float ancho=(AnchoDia-8f)/b.Carriles;
                float y=Cabecera+((float)(b.Inicio-lunes.AddDays(b.Dia)).TotalHours-primeraHora)*AltoHora;
                float alto=(float)(b.Fin-b.Inicio).TotalHours*AltoHora;
                b.Rectangulo=new Rectangle((int)(Eje+b.Dia*AnchoDia+4+b.Carril*ancho),(int)y,Math.Max(1,(int)ancho-3),Math.Max(2,(int)alto-2));
            }
            AutoScrollPosition=Point.Empty;Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);var g=e.Graphics;var posicion=AutoScrollPosition;
            var save=g.Save();g.SetClip(new Rectangle(0,Cabecera,ClientSize.Width,Math.Max(0,ClientSize.Height-Cabecera)));g.TranslateTransform(posicion.X,posicion.Y);
            using(var line=new Pen(TemaVisual.Linea))using(var tinta=new SolidBrush(TemaVisual.Tenue))using(var fondo=new SolidBrush(TemaVisual.Fondo))
            {
                for(int dia=0;dia<7;dia++)
                {
                    if(lunes.AddDays(dia)==DateTime.Today)g.FillRectangle(fondo,Eje+dia*AnchoDia,Cabecera,AnchoDia,(ultimaHora-primeraHora)*AltoHora);
                    g.DrawLine(line,Eje+dia*AnchoDia,Cabecera,Eje+dia*AnchoDia,AutoScrollMinSize.Height);
                }
                for(int hora=primeraHora;hora<=ultimaHora;hora++)
                {int y=Cabecera+(hora-primeraHora)*AltoHora;g.DrawLine(line,Eje,y,Eje+7*AnchoDia,y);if(hora<24)g.DrawString(hora.ToString("00")+":00",Font,tinta,1,y+2);}
            }
            foreach(var b in bloques)
            {
                var r=b.Sesion;bool cancelada=Convert.ToString(r["Estado"])=="Cancelada";
                Color color=cancelada?TemaVisual.Tenue:Convert.ToInt32(r["Confirmados"])>=Convert.ToInt32(r["Capacidad"])?Color.FromArgb(163,100,25):Convert.ToInt32(r["Id_Clase"])%2==0?TemaVisual.Azul:TemaVisual.Primario;
                using(var fill=new SolidBrush(Color.FromArgb(234+(color.R%12),239,244)))g.FillRectangle(fill,b.Rectangulo);
                using(var pen=new Pen(Convert.ToInt32(r["Id"])==seleccion?TemaVisual.Tinta:color,Convert.ToInt32(r["Id"])==seleccion?3:1))g.DrawRectangle(pen,b.Rectangulo);
                var texto=new Rectangle(b.Rectangulo.X+4,b.Rectangulo.Y+3,Math.Max(1,b.Rectangulo.Width-8),Math.Max(1,b.Rectangulo.Height-5));
                string contenido=b.Inicio.ToString("HH:mm")+"–"+b.Fin.ToString("HH:mm")+"\n"+r["Actividad"]+"\n"+r["Confirmados"]+"/"+r["Capacidad"]+" · espera "+r["EnEspera"];
                TextRenderer.DrawText(g,contenido,Font,texto,color,TextFormatFlags.WordBreak|TextFormatFlags.EndEllipsis|TextFormatFlags.NoPadding|TextFormatFlags.PreserveGraphicsTranslateTransform|TextFormatFlags.PreserveGraphicsClipping);
            }
            g.Restore(save);
            using(var fondo=new SolidBrush(TemaVisual.Fondo))g.FillRectangle(fondo,0,0,ClientSize.Width,Cabecera);
            for(int dia=0;dia<7;dia++)
            {
                var fecha=lunes.AddDays(dia);var rect=new Rectangle(Eje+dia*AnchoDia+posicion.X,0,AnchoDia,Cabecera);
                TextRenderer.DrawText(g,fecha.ToString("ddd dd/MM"),TemaVisual.Semi,rect,fecha==DateTime.Today?TemaVisual.Primario:TemaVisual.Tinta,TextFormatFlags.HorizontalCenter|TextFormatFlags.VerticalCenter);
            }
        }
        BloqueSemanal Bajo(Point p){p.Offset(-AutoScrollPosition.X,-AutoScrollPosition.Y);return bloques.LastOrDefault(b=>b.Rectangulo.Contains(p));}
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);Focus();if(e.Y<Cabecera)return;var b=Bajo(e.Location);if(b!=null)Elegir(b);
        }
        void Elegir(BloqueSemanal bloque){seleccion=Convert.ToInt32(bloque.Sesion["Id"]);Invalidate();SesionSeleccionada?.Invoke(seleccion);}
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);var b=e.Y<Cabecera?null:Bajo(e.Location);
            string texto=b==null?"":b.Sesion["Actividad"]+" · "+b.Sesion["Estado"]+"\n"+Convert.ToDateTime(b.Sesion["Inicio"]).ToString("dd/MM HH:mm")+" a "+Convert.ToDateTime(b.Sesion["Fin"]).ToString("dd/MM HH:mm")+"\nProfesor: "+b.Sesion["Profesor"]+"\nEspacio: "+b.Sesion["Espacio"]+"\nConfirmados: "+b.Sesion["Confirmados"]+" / "+b.Sesion["Capacidad"]+" · En espera: "+b.Sesion["EnEspera"];
            if(ayuda.GetToolTip(this)!=texto)ayuda.SetToolTip(this,texto);Cursor=b==null?Cursors.Default:Cursors.Hand;
        }
        protected override bool IsInputKey(Keys keyData){return keyData==Keys.Left || keyData==Keys.Right || keyData==Keys.Up || keyData==Keys.Down || base.IsInputKey(keyData);}
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);if(bloques.Count==0)return;
            var orden=bloques.GroupBy(b=>b.Sesion["Id"]).Select(g=>g.First()).OrderBy(b=>b.Inicio).ThenBy(b=>b.Dia).ToList();
            int indice=orden.FindIndex(b=>Convert.ToInt32(b.Sesion["Id"])==seleccion);
            if(e.KeyCode==Keys.Left || e.KeyCode==Keys.Up)indice=indice<0?0:(indice-1+orden.Count)%orden.Count;
            else if(e.KeyCode==Keys.Right || e.KeyCode==Keys.Down)indice=(indice+1)%orden.Count;
            else if(e.KeyCode!=Keys.Enter)return;
            var elegido=orden[Math.Max(0,indice)];AutoScrollPosition=new Point(Math.Max(0,elegido.Rectangulo.Right-ClientSize.Width+20),Math.Max(0,elegido.Rectangulo.Y-Cabecera-15));Elegir(elegido);e.Handled=true;
        }
    }
}
