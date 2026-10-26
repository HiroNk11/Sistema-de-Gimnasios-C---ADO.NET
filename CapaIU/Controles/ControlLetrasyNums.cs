using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaIU.Controles
{
    public partial class ControlLetrasyNums : MiControl
    {
        public ControlLetrasyNums()
        {
            InitializeComponent();
        }

        public override bool Validar()
        {
            bool ok = base.Validar();
            Regex rx = new Regex("^[A-Za-z]\\w{5,29}$");

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
