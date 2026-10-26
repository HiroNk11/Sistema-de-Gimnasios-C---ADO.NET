using CapaDominio;
using CapaRepositorio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace CapaAplicacion.Negocio
{
    public class ClaseBLL
    {
        ClaseRepository mapperc = new ClaseRepository();




        public List<Clase> ListarClase()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            List<Clase> listaclase = mapperc.Listar();
            return listaclase;
        }


        public List<Reserva_Socio> GetClasesDeSocioId(int id)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperc.GetClasesDeSocioId(id);
        }

        public List<Detalle_Clase> GetClaseDescription(string n)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperc.GetClaseDescription(n);
        }

        public List<Detalle_Clase> GetClaseALLDescription()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperc.GetClaseALLDescription();
        }



        public List<Lista_Clase> Listar_Clase()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperc.Listar_Clase();
        }





    }
}
