using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace CapaRepositorio
{
    public class AccesoRepository
    {
        public string GetConnectionString()
        {
            string value = Environment.GetEnvironmentVariable("GYM_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(value)) value = ConfigurationManager.ConnectionStrings["cadena_conexion"] == null ? null : ConfigurationManager.ConnectionStrings["cadena_conexion"].ConnectionString;
            if (string.IsNullOrWhiteSpace(value)) throw new InvalidOperationException("Configure la conexión a SQL Server.");
            var builder = new SqlConnectionStringBuilder(value);
            bool local = builder.DataSource.StartsWith(@"(localdb)\", StringComparison.OrdinalIgnoreCase);
            if (!local && (!builder.Encrypt || builder.TrustServerCertificate))
                throw new InvalidOperationException("SQL remoto requiere Encrypt=True y TrustServerCertificate=False.");
            builder.PersistSecurityInfo = false;
            if (builder.ConnectTimeout == 0 || builder.ConnectTimeout > 30) builder.ConnectTimeout = 15;
            return builder.ConnectionString;
        }
        public DataTable Leer(string procedimiento, SqlParameter[] parametros)
        {
            using (var sql = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(procedimiento, sql))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                var table = new DataTable(); adapter.Fill(table); return table;
            }
        }
        public string Escribir(string procedimiento, SqlParameter[] parametros)
        {
            using (var sql = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(procedimiento, sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                sql.Open(); cmd.ExecuteNonQuery();
                return "Operación Realizada con Exito";
            }
        }
    }
}
