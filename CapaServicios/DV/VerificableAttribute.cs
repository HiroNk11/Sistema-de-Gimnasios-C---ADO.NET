using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios.DV
{
    public class VerificableAttribute : Attribute
    {
        string _prefix;
        public VerificableAttribute(string prefix)
        {
            _prefix = prefix;
        }

        public string Prefix { get { return _prefix; } }
    }
}
