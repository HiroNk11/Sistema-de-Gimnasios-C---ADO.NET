using CapaDominio;
using CapaRepositorio;
using CapaServicios;
using CapaServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion
{
    public class PermisosBLL
    {
        PermisosRepository _permisos;
        UsuarioRepository user = new UsuarioRepository();
        public PermisosBLL()
        {
            _permisos = new PermisosRepository();
        }
        public Componente GuardarComponente(Componente p, bool esfamilia)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeConfigurarSeguridadPatentesyFamilias);

            return _permisos.GuardarComponente(p, esfamilia);
        }

        public IList<Patente> GetAllPatentes()
        {
            return _permisos.GetAllPatentes();
        }

        public IList<Familia> GetAllFamilias()
        {
            return _permisos.GetAllFamilias();
        }

        public Array GetAllPermission()
        {
            return _permisos.GetAllPermission();
        }

        public IList<Componente> GetAll(string familia)
        {
            return _permisos.GetAll(familia);
        }

        public bool Existe(Componente c, int id)
        {
            bool existe = false;

            if (c.Id.Equals(id))
                existe = true;
            else
                foreach (var item in c.Hijos)
                {
                    existe = Existe(item, id);
                    if (existe) return true;
                }
            return existe;
        }
        public void GuardarFamilia(Familia c)
        {
            CapaAplicacion.Autorizacion.Exigir(CapaServicios.Composite.TipoPermiso.PuedeConfigurarSeguridadPatentesyFamilias);

            _permisos.GuardarFamilia(c);
        }
        public void FillFamilyComponents(Familia familia)
        {
            _permisos.FillFamilyComponents(familia);
        }
        public void FillUserComponents(PermisoUsuario u)
        {
            _permisos.FillUserComponents(u);

        }
        public bool BuscarPermisos(TipoPermiso permiso, PermisoUsuario u)
        {
            return _permisos.BuscarPermisos(permiso,u);
        }

    }
}
