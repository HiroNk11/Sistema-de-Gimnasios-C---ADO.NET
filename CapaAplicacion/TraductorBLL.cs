using CapaInterfaces.INTERFACES;
using CapaRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaAplicacion
{
    
    public class TraductorBLL
    {
        IdiomaRepository irepo = new IdiomaRepository();
        public IIdioma ObtenerIdiomaDefault()
        {
            return irepo.ObtenerIdiomaDefault();
        }

        public IList<IIdioma> ObtenerIdiomas()
        {
            return irepo.ObtenerIdiomas();
        }

        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma = null)
        {
            return irepo.ObtenerTraducciones(idioma);
        }


    }
}