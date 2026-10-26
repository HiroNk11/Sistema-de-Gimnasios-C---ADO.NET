using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU
{
    public partial class MiControl : UserControl
    {
        public MiControl()
        {
            InitializeComponent();
        }


        public string Texto
        {
            get { return txttexto.Text; }
            set { txttexto.Text = value; }
        }


        public string Etiqueta
        {
            get { return lbletiqueta.Text; }
            set { lbletiqueta.Text = value; }
        }

        public virtual bool Validar()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txttexto.Text))
            {
                ok = false;
                CambiarColor(Color.Red);
            }

            else
            {
                CambiarColor(Color.Green);
            }

            return ok;
        }

        public void CambiarColor(Color Uncolor)
        {
            lbletiqueta.ForeColor = Uncolor;
        }
    }
}
