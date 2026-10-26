using System;
using System.Drawing;
using System.Windows.Forms;
namespace CapaIU.Controles
{
    public static class TemaVisual
    {
        public static readonly Color Fondo=Color.FromArgb(243,246,250),Tinta=Color.FromArgb(28,43,65),Tenue=Color.FromArgb(103,118,139),Primario=Color.FromArgb(20,112,97),Azul=Color.FromArgb(48,91,218),Oscuro=Color.FromArgb(19,37,57),Linea=Color.FromArgb(224,231,239),Rojo=Color.FromArgb(174,49,64);
        public static readonly Font Semi=new Font("Segoe UI Semibold",9F);
        public static readonly Font Fuente=new Font("Segoe UI",10F),Titulo=new Font("Segoe UI Semibold",22F),Subtitulo=new Font("Segoe UI Semibold",12F);
        public static Label Etiqueta(string text,int size=10,bool bold=false)
        {return new Label {Text=text,AutoSize=true,Font=new Font("Segoe UI",size,bold?FontStyle.Bold:FontStyle.Regular),ForeColor=Tinta,Margin=new Padding(0,0,0,6)};}
        public static Button Boton(string text,bool primary=false,bool danger=false)
        {
            var b=new Button {Text=text,AutoSize=false,Height=38,Width=170,FlatStyle=FlatStyle.Flat,Font=Fuente,Cursor=Cursors.Hand,Margin=new Padding(0,5,8,5),UseVisualStyleBackColor=false};
            b.BackColor=primary?Primario:Color.White;b.ForeColor=primary?Color.White:danger?Rojo:Tinta;b.FlatAppearance.BorderColor=primary?Primario:danger?Color.FromArgb(231,193,198):Linea;
            b.FlatAppearance.MouseOverBackColor=primary?Color.FromArgb(16,92,80):Fondo;
            b.Tag=primary?"primario":danger?"peligro":null;
            Color fondo=b.BackColor,tinta=b.ForeColor,borde=b.FlatAppearance.BorderColor;
            b.EnabledChanged+=(s,e)=>{b.BackColor=b.Enabled?fondo:Color.White;b.ForeColor=b.Enabled?tinta:Tenue;b.FlatAppearance.BorderColor=b.Enabled?borde:Linea;};
            b.AccessibleName=text;return b;
        }
        public static void EstiloGrilla(DataGridView grid)
        {
            grid.Font=Fuente;grid.BackgroundColor=Color.White;grid.BorderStyle=BorderStyle.None;grid.GridColor=Linea;grid.EnableHeadersVisualStyles=false;
            grid.ColumnHeadersDefaultCellStyle.BackColor=Fondo;grid.ColumnHeadersDefaultCellStyle.ForeColor=Tenue;grid.ColumnHeadersDefaultCellStyle.Font=new Font("Segoe UI Semibold",9F);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor=Fondo;grid.ColumnHeadersHeight=42;grid.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.DefaultCellStyle.BackColor=Color.White;grid.DefaultCellStyle.ForeColor=Tinta;grid.DefaultCellStyle.SelectionBackColor=Color.FromArgb(223,239,235);grid.DefaultCellStyle.SelectionForeColor=Tinta;
            grid.DefaultCellStyle.Padding=new Padding(8,0,8,0);grid.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(249,251,253);grid.RowTemplate.Height=38;grid.RowHeadersVisible=false;
            grid.AllowUserToAddRows=false;grid.AllowUserToDeleteRows=false;grid.AllowUserToResizeRows=false;grid.MultiSelect=false;grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly=true;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells;grid.AutoGenerateColumns=true;
        }
        public static DataGridView Grilla(){var g=new DataGridView {Dock=DockStyle.Fill};EstiloGrilla(g);return g;}
        public static TextBox Texto(int max=100){return new TextBox {Font=Fuente,MaxLength=max,BorderStyle=BorderStyle.FixedSingle,Width=260,Height=30};}
        public static ComboBox Lista(){var lista=new ComboBox {Font=Fuente,DropDownStyle=ComboBoxStyle.DropDownList,Width=260,IntegralHeight=false,DropDownHeight=240,AutoCompleteSource=AutoCompleteSource.ListItems,AutoCompleteMode=AutoCompleteMode.SuggestAppend};SelectorBuscable.Habilitar(lista);return lista;}
        public static NumericUpDown Dinero(){return new NumericUpDown {Font=Fuente,DecimalPlaces=2,Minimum=0,Maximum=100000000,ThousandsSeparator=true,Width=260};}
        public static DateTimePicker Fecha(bool hora=false){return new DateTimePicker {Font=Fuente,Format=DateTimePickerFormat.Custom,CustomFormat=hora?"dd/MM/yyyy  HH:mm":"dd/MM/yyyy",Width=260};}
        public static Panel Campo(string label,Control input)
        {
            var p=new Panel {Height=60,Dock=DockStyle.Top,Padding=new Padding(0)};var l=Etiqueta(label,9,true);l.Location=new Point(0,1);
            input.Location=new Point(0,24);input.Anchor=AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top;input.Width=260;input.AccessibleName=label;
            p.Controls.Add(l);p.Controls.Add(input);
            var combo=input as ComboBox;Button buscar=combo==null?null:SelectorBuscable.Boton(combo,label);
            if(buscar!=null){buscar.Margin=new Padding(0);p.Controls.Add(buscar);}
            Action ajustar=()=>{input.Width=Math.Max(60,p.ClientSize.Width-(buscar==null?2:42));if(buscar!=null){buscar.Location=new Point(input.Right+6,24);buscar.Size=new Size(34,input.Height);}};
            p.Resize+=(s,e)=>ajustar();ajustar();return p;
        }
        public static void Aplicar(Form form)
        {
            form.Font=Fuente;form.BackColor=Fondo;form.ForeColor=Tinta;form.StartPosition=FormStartPosition.CenterParent;
            Pintar(form);
        }
        static void Pintar(Control parent)
        {
            foreach(Control c in parent.Controls)
            {
                if(c is DataGridView){EstiloGrilla((DataGridView)c);continue;}
                if(c is TextBoxBase){c.BackColor=Color.White;c.ForeColor=Tinta;c.Font=Fuente;}
                else if(c is ComboBox || c is DateTimePicker || c is NumericUpDown){c.Font=Fuente;c.ForeColor=Tinta;if(c is ComboBox)SelectorBuscable.Habilitar((ComboBox)c);}
                else if(c is Button)
                {
                    var b=(Button)c;bool danger=b.Text.ToLowerInvariant().Contains("eliminar") || b.Text.ToLowerInvariant().Contains("baja");
                    b.FlatStyle=FlatStyle.Flat;b.FlatAppearance.BorderColor=Linea;b.BackColor=Color.White;b.ForeColor=danger?Rojo:Tinta;b.Font=new Font("Segoe UI",9F);b.UseVisualStyleBackColor=false;
                }
                else if(c is Label || c is GroupBox){c.ForeColor=Tinta;c.Font=new Font("Segoe UI",9F);}
                else if(c is Panel || c is TabPage){c.BackColor=Color.White;}
                Pintar(c);
            }
        }
    }
}
