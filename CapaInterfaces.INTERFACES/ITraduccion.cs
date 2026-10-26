using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaInterfaces.INTERFACES
{
    public interface ITraduccion
    {
            IEtiqueta Etiqueta { get; set; }
            string Texto { get; set; }
    }
}