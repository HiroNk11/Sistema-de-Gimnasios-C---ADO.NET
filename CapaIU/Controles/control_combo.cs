using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU.Controles
{
    public partial class control_combo : UserControl
    {
        public control_combo()
        {
            InitializeComponent();
        }

        public string Texto
        {
            get { return cmbidioma.Text; }
            set { cmbidioma.Text = value; }
        }


        public string Etiqueta
        {
            get { return lbletiqueta.Text; }
            set { lbletiqueta.Text = value; }
        }

        public virtual bool Validar()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(cmbidioma.Text))
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
