using CapaDominio;
using CapaRepositorio.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Negocio
{
    public class SocioBLL
    {
        SocioRepository mapperent = new SocioRepository();
        public List<Socio> Listar()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            List<Socio> listas = mapperent.Listar();
            return listas;
        }
        public string Insertar(Socio s)
        {
            if (s == null || string.IsNullOrWhiteSpace(s.Nombre)) throw new ArgumentException("Ingrese un nombre válido.");
            if (string.IsNullOrWhiteSpace(s.Apellido) || s.Fecha.Date > DateTime.Today) throw new ArgumentException("Revise apellido y fecha de nacimiento.");

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Insertar(s);
        }
        public string Borrar(Socio s)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeEliminarSocios);

            return mapperent.Borrar(s);
        }
        public DataTable Buscar_Socio(string nombre)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperent.Buscar_Socio(nombre);
        }
        public string Editar(Socio s)
        {
            if (s == null || string.IsNullOrWhiteSpace(s.Nombre)) throw new ArgumentException("Ingrese un nombre válido.");
            if (string.IsNullOrWhiteSpace(s.Apellido) || s.Fecha.Date > DateTime.Today) throw new ArgumentException("Revise apellido y fecha de nacimiento.");

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Editar(s);
        }
        public string Insertar_Socio_Clase(Socio_Clase sc)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            if(sc==null || sc.Id_Socio<=0 || sc.Id_Clase<=0 || sc.Id_piso<=0)throw new ArgumentException("Seleccione un socio y una clase válidos.");
            if(sc.Precio<0 || sc.Precio>100000000 || sc.cantidad_dias<1 || sc.FechaFin<=sc.FechaInicio)throw new ArgumentException("Revise precio, cantidad de clases y fechas.");
            return mapperent.Insertar_Socio_Clase(sc);
        }

        public int GetIdPisoByDescription(string d)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperent.GetIdPisoByDescription(d);
        }

        public DataTable Listar_HistorialCambios()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            DataTable dt = mapperent.Listar_HistorialCambios();
            return dt;
        }

        public void LimpiarHistorial()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeVerhistorialCambios);

            mapperent.LimpiarHistorial();
        }

       public DataTable ListarSociosIncriptos(string c)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            DataTable dt = mapperent.ListarSociosIncriptos(c);
            return dt;
        }

        public DataTable ListarSociosNOIncriptos(int idpiso)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            DataTable dt = mapperent.ListarSociosNOIncriptos(idpiso);
            return dt;
        }

        public void CrearXML(string txt)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeSerializar);

             mapperent.CrearXML(txt);
        }

        public List<Socio_Clase> listar_socioclase()
        {
            List<Socio_Clase> listas = mapperent.listar_socioclase();
            return listas;
        }

        public string Insertar_Reserva(Reserva r)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Insertar_Reserva(r);
        }

        public string Borrar_socioclase(Socio_Clase s)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Borrar_socioclase(s);
        }

    }
}
