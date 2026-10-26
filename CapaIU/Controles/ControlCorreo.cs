using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace CapaIU.Controles
{
    public partial class ControlCorreo : MiControl
    {
        public ControlCorreo()
        {
            InitializeComponent();
        }
        public override bool Validar()
        {
            bool ok = base.Validar();
            Regex rx = new Regex(@"\A[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z");

            if (ok)
            {
                ok = rx.IsMatch(this.Texto);
                if (!ok)
                {
                    this.CambiarColor(Color.Red);

                }
            }

            return ok;
        }

    }
}
