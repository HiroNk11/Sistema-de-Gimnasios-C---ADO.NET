using CapaDominio;
using CapaRepositorio;
using CapaServicios;
using CapaServicios.MultiIdioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion
{
    public class IdiomaBLL
    {
        IdiomaRepository repoidioma = new IdiomaRepository();

        public string InsertarIdioma(Idioma id)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeGestionarIdioma);

            return repoidioma.InsertarIdioma(id);
        }
        public int ObtenerTraduccion(string n)
        {
            return repoidioma.ObtenerTraduccion(n);
        }
        public string EditarTraduccion(string t,string n,int i)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeGestionarIdioma);

            return repoidioma.EditarTraduccion(t,n,i);
        }
        public List<Etiquetas>ObtenerEyT(string nombre)
        {
            return repoidioma.ObtenerEtiquetasYTraduccion(nombre);
        }

        public string InsertarEtiquetas(int idI,int idE, string t)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeGestionarIdioma);

           return repoidioma.InsertarE(idI,idE,t);
        }

        public List<Etiqueta> GetAllEtiquetas()
        {
            return repoidioma.GetAllEtiquetas();
        }


    }
}
