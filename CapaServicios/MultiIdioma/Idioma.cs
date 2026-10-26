using CapaInterfaces.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicios
{
    public class Idioma : IIdioma
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Default { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}