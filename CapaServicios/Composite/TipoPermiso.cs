using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios.Composite
{
     public enum TipoPermiso
    {
        PuedeCambiarIdioma,
        PuedeGestionarIdioma,
        PuedeConfigurarSeguridadPatentesyFamilias,
        PuedeConfigurarSeguridadUsuarios,
        PuedeAccederBitacora,
        PuedeAccederBackupRestore,
        PuedeGestionarUsuarios,
        PuedeAccederANegocio,
        PuedeRestaurardv,
        PuedeSerializar,
        PuedeVerhistorialCambios,
        DarBajaUsuario,
        PuedeAccederBackup,
        PuedeAccederRestore,
        PuedeCrearUsuarios,
        PuedeEliminarSocios,
        PuedeEliminarEntrenadores
    }
}
