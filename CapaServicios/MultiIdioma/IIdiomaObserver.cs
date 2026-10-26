using CapaInterfaces.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaServicios
{
    public interface IIdiomaObserver
    {
        void UpdateLanguage(IIdioma idioma);
    }
}
