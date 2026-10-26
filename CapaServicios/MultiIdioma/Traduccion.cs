using CapaInterfaces.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicios
{
    public class Traduccion : ITraduccion
    {
        public IEtiqueta Etiqueta { get; set; }
        public string Texto { get; set; }
    }
}