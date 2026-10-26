using System;
using System.IO;
using System.Data.SqlClient;
namespace CapaRepositorio
{
    public class BackupRestoreRepository
    {
        readonly AccesoRepository acceso=new AccesoRepository();
        public string Backup(string ruta)
        {
            if(string.IsNullOrWhiteSpace(ruta))throw new ArgumentException("Seleccione una carpeta del servidor SQL.");
            var builder=new SqlConnectionStringBuilder(acceso.GetConnectionString());
            string db=builder.InitialCatalog.Replace("]","]]");
            using(var cn=new SqlConnection(builder.ConnectionString))
            using(var cmd=new SqlCommand("BACKUP DATABASE ["+db+"] TO DISK=@path WITH CHECKSUM",cn))
            {
                cmd.CommandTimeout=600;
                cmd.Parameters.AddWithValue("@path",Path.Combine(ruta,"BodySport-"+DateTime.Now.ToString("yyyyMMdd-HHmmss")+".bak"));
                cn.Open();cmd.ExecuteNonQuery();
            }
            return "Respaldo creado. La ruta corresponde al servidor SQL.";
        }
        public string Restore(string ruta)
        {
            if(string.IsNullOrWhiteSpace(ruta) || !ruta.EndsWith(".bak",StringComparison.OrdinalIgnoreCase))throw new ArgumentException("Seleccione un archivo .bak del servidor SQL.");
            var builder=new SqlConnectionStringBuilder(acceso.GetConnectionString());
            string db=builder.InitialCatalog;
            if(string.IsNullOrWhiteSpace(db) || new[]{"master","model","msdb","tempdb"}.ContainsIgnoreCase(db))throw new ArgumentException("La base de destino no es válida.");
            string quoted="["+db.Replace("]","]]")+"]";builder.InitialCatalog="master";
            SqlConnection.ClearAllPools();
            using(var cn=new SqlConnection(builder.ConnectionString))
            {
                cn.Open();
                using(var verify=new SqlCommand("RESTORE VERIFYONLY FROM DISK=@path WITH CHECKSUM",cn))
                {verify.CommandTimeout=600;verify.Parameters.AddWithValue("@path",ruta);verify.ExecuteNonQuery();}
                try
                {
                    using(var single=new SqlCommand("ALTER DATABASE "+quoted+" SET SINGLE_USER WITH ROLLBACK IMMEDIATE",cn))single.ExecuteNonQuery();
                    using(var restore=new SqlCommand("RESTORE DATABASE "+quoted+" FROM DISK=@path WITH REPLACE,CHECKSUM",cn))
                    {restore.CommandTimeout=600;restore.Parameters.AddWithValue("@path",ruta);restore.ExecuteNonQuery();}
                }
                finally
                {
                    using(var multi=new SqlCommand("ALTER DATABASE "+quoted+" SET MULTI_USER",cn))multi.ExecuteNonQuery();
                    SqlConnection.ClearAllPools();
                }
            }
            return "Base restaurada y disponible para múltiples usuarios.";
        }
    }
    internal static class ArrayExtensions
    {
        internal static bool ContainsIgnoreCase(this string[] items,string value)
        { foreach(string item in items)if(string.Equals(item,value,StringComparison.OrdinalIgnoreCase))return true;return false; }
    }
}
