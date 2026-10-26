using CapaDominio;
using CapaRepositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaAplicacion
{
    public class EntrenamientoBLL
    {
        EntrenamientoRepository mapperent = new EntrenamientoRepository();
        public List<Entrenamiento> Listar()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            List<Entrenamiento> listaent = mapperent.Listar();
            return listaent;
        }
        public List<Piso> ListarPiso()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            List<Piso> listaent = mapperent.ListarPiso();
            return listaent;
        }
        public string Insertar(Entrenamiento ent)
        {
            if (ent == null || string.IsNullOrWhiteSpace(ent.Nombre)) throw new ArgumentException("Ingrese un nombre válido.");

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Insertar(ent);
        }

        public string Borrar(Entrenamiento ent)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Borrar(ent);
        }
        public string Editar(Entrenamiento ent)
        {
            if (ent == null || string.IsNullOrWhiteSpace(ent.Nombre)) throw new ArgumentException("Ingrese un nombre válido.");

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Editar(ent);
        }

        public DataTable Buscar_Entrenamiento(Entrenamiento ent)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            return mapperent.Buscar_Entrenamiento(ent);
        }
    }
}