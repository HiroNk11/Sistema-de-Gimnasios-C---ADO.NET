using System;
using CapaServicios;
using CapaServicios.Composite;
namespace CapaAplicacion
{
    public static class Autorizacion
    {
        public static void Exigir(TipoPermiso permiso) { ExigirCualquiera(permiso); }
        public static void ExigirCualquiera(params TipoPermiso[] permisos)
        {
            if (!SingletonSesion.Instancia.IsLogged()) throw new UnauthorizedAccessException("Debe iniciar sesión.");
            var user = new PermisoUsuario { idusuario = SingletonSesion.Instancia.Usuario.idusuario };
            var gestor=new PermisosBLL();
            foreach(var permiso in permisos)if(gestor.BuscarPermisos(permiso,user))return;
            throw new UnauthorizedAccessException("No tiene permiso para realizar esta operación.");
        }
    }
}
