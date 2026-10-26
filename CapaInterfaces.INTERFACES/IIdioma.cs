using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaInterfaces.INTERFACES
{
    public interface IIdioma
    {
        int Id { get; set; }
        string Nombre { get; set; }
        bool Default { get; set; }
    }
}