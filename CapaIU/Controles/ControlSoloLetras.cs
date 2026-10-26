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
    public partial class ControlSoloLetras : MiControl
    {
        public ControlSoloLetras()
        {
            InitializeComponent();
        }

        public override bool Validar()
        {
            bool ok = base.Validar();
            Regex rx = new Regex(@"^[a-zA-Z\s]+$"); //Te permitirá ingresar letras de la hasta la z, un con un mínimo de 2 caracteres y máximo 254

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
