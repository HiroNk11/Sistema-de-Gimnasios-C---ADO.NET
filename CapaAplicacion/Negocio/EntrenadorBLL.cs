using CapaDominio;
using CapaRepositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapaAplicacion
{
    public class EntrenadorBLL
    {
        EntrenadorRepository mapperent = new EntrenadorRepository();
        public List<Entrenador> Listar()
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
            List<Entrenador> listaent = mapperent.Listar();
            return listaent;
        }
        public string Insertar(Entrenador ent)
        {
            Validar(ent);

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);

            return mapperent.Insertar(ent);
        }

        public string Editar(Entrenador e) {
            Validar(e);
            if (e.Id_Entrenador <= 0) throw new ArgumentException("Seleccione un entrenador.");

            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio);
 return mapperent.Editar(e);}

        public string Borrar(Entrenador e) {
            if (e == null || e.Id_Entrenador <= 0) throw new ArgumentException("Seleccione un entrenador.");
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeEliminarEntrenadores);
 return mapperent.Borrar(e); }
        static void Validar(Entrenador e)
        {
            if(e == null || string.IsNullOrWhiteSpace(e.Nombre) || string.IsNullOrWhiteSpace(e.Apellido)) throw new ArgumentException("Completá nombre y apellido.");
            e.Nombre=e.Nombre.Trim(); e.Apellido=e.Apellido.Trim();
            if(e.Nombre.Length>50 || e.Apellido.Length>50) throw new ArgumentException("Nombre y apellido admiten hasta 50 caracteres.");
        }
        public DataTable Buscar_Entrenador(Entrenador e) {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeAccederANegocio); return mapperent.Buscar_Entrenador(e); }
    }
}