using CapaRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion
{

    public class BackupRestoreBLL
    {
        BackupRestoreRepository brmapper = new BackupRestoreRepository();
        public string Backup(string ruta)
        {
            CapaAplicacion.Autorizacion.ExigirCualquiera(CapaServicios.Composite.TipoPermiso.PuedeAccederBackupRestore,CapaServicios.Composite.TipoPermiso.PuedeAccederBackup);

            return brmapper.Backup(ruta);
        }

        public string Restore(string ruta)
        {
            CapaAplicacion.Autorizacion.ExigirCualquiera(CapaServicios.Composite.TipoPermiso.PuedeAccederBackupRestore,CapaServicios.Composite.TipoPermiso.PuedeAccederRestore);

            return brmapper.Restore(ruta);
        }

    }
}
