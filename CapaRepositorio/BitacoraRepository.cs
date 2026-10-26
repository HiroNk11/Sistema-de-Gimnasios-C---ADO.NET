using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace CapaRepositorio
{
    public class BitacoraRepository
    {
        AccesoRepository AR = new AccesoRepository();
        public string InsertarEvento(Bitacora bit)
        {
            return AR.Escribir("Grabar_Bitacora", CrearParametrosEvento(bit));
        }

        internal static SqlParameter[] CrearParametrosEvento(Bitacora bit)
        {
            if (bit == null) throw new ArgumentNullException(nameof(bit));
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@Id_Usuario", bit.id_usuario);
            parametros[1] = new SqlParameter("@Actividad", SqlDbType.NVarChar, 512) {Value=(object)bit.actividad ?? DBNull.Value};
            // Value=null omite el parametro al ejecutar un procedimiento (SQL 201).
            parametros[2] = new SqlParameter("@Criticidad", SqlDbType.NVarChar, 50) {Value=string.IsNullOrWhiteSpace(bit.criticidad) ? "Baja" : bit.criticidad};
            parametros[3] = new SqlParameter("@Fecha", SqlDbType.NVarChar, 50) {Value=(object)bit.fecha ?? DBNull.Value};
            parametros[4] = new SqlParameter("@Hora", SqlDbType.NVarChar, 50) {Value=(object)bit.hora ?? DBNull.Value};
            parametros[5] = new SqlParameter("@MomentoUtc", SqlDbType.DateTime2) {Value=DateTime.UtcNow};
            return parametros;
        }

        public List<Detalle_Bitacora> Listar_Bitacora()
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select LoginName,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuario usu on bita.Id_Usuario = usu.Id_Usuario";
            cmd.CommandText = sql;
            var reader = cmd.ExecuteReader();
            var lista = new List<Detalle_Bitacora>();
            while (reader.Read())
            {
                Detalle_Bitacora db = new Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("LoginName"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));

                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        public List<Detalle_Bitacora> GetBitacoraUser(string user)
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select LoginName,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuario usu on bita.Id_Usuario = usu.Id_Usuario where LoginName = @f";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@f", user);
            var reader = cmd.ExecuteReader();
            var lista = new List<Detalle_Bitacora>();
            while (reader.Read())
            {
                Detalle_Bitacora db = new Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("LoginName"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));
                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        public List<Detalle_Bitacora> GetBitacoraUserByDate(string f1,string f2)
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select LoginName,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuario usu on bita.Id_Usuario = usu.Id_Usuario where Fecha BETWEEN @f1 and @f2";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@f1", f1);
            cmd.Parameters.AddWithValue("@f2", f2);
            var reader = cmd.ExecuteReader();
            var lista = new List<Detalle_Bitacora>();
            while (reader.Read())
            {
                Detalle_Bitacora db = new Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("LoginName"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));
                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }

        public List<Detalle_Bitacora> GetBitacoraUserByFilter(string u,string f1,string f2,string c)
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;
            var sql = $@"select LoginName,Actividad,Criticidad,Fecha,Hora from Bitacora bita inner join Usuario usu on bita.Id_Usuario = usu.Id_Usuario  where LoginName = @u and Fecha BETWEEN @f1 and @f2 and Criticidad=@c";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@f1", f1);
            cmd.Parameters.AddWithValue("@f2", f2);
            cmd.Parameters.AddWithValue("@c", c);
            var reader = cmd.ExecuteReader();
            var lista = new List<Detalle_Bitacora>();
            while (reader.Read())
            {
                Detalle_Bitacora db = new Detalle_Bitacora();
                db.LoginName = reader.GetString(reader.GetOrdinal("LoginName"));
                db.Actividad = reader.GetString(reader.GetOrdinal("Actividad"));
                db.Criticidad = reader.GetString(reader.GetOrdinal("Criticidad"));
                db.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));
                db.Hora = reader.GetString(reader.GetOrdinal("Hora"));

                lista.Add(db);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }

        public DataTable FiltrarCriticidad(string c)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            SqlCommand cmd = new SqlCommand("crit_bitacora", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@c",c);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }
    }
}
