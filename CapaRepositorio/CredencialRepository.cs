using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CapaServicios;
namespace CapaRepositorio
{
    public class CredencialRepository
    {
        readonly AccesoRepository acceso = new AccesoRepository();
        public string Obtener(int id)
        {
            using (var cn = new SqlConnection(acceso.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT PasswordHash FROM dbo.Usuario_Credencial WHERE Id_Usuario=@id", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id; cn.Open(); return cmd.ExecuteScalar() as string;
            }
        }
        public bool DebeCambiarClave(int id)
        {
            using(var cn=new SqlConnection(acceso.GetConnectionString()))using(var cmd=new SqlCommand(@"IF COL_LENGTH('dbo.Usuario_Credencial','DebeCambiarClave') IS NULL SELECT CAST(0 AS bit); ELSE EXEC sys.sp_executesql N'SELECT DebeCambiarClave FROM dbo.Usuario_Credencial WHERE Id_Usuario=@id', N'@id int', @id;",cn)){
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cn.Open();object valor=cmd.ExecuteScalar();return valor!=null && valor!=DBNull.Value && Convert.ToBoolean(valor);
            }
        }
        public static void Guardar(SqlConnection cn, SqlTransaction tx, int id, string hash)
        {
            using (var cmd = new SqlCommand(@"UPDATE dbo.Usuario_Credencial WITH (UPDLOCK, SERIALIZABLE) SET PasswordHash=@hash, ActualizadoUtc=SYSUTCDATETIME() WHERE Id_Usuario=@id;
IF @@ROWCOUNT=0 INSERT dbo.Usuario_Credencial(Id_Usuario,PasswordHash) VALUES(@id,@hash);", cn, tx))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@hash", SqlDbType.NVarChar, 256).Value = hash; cmd.ExecuteNonQuery();
            }
        }
        public void Cambiar(string usuario, string hash)
        {
            using (var cn = new SqlConnection(acceso.GetConnectionString()))
            {
                cn.Open(); using (var tx = cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    Cambiar(cn, tx, usuario, hash);
                    using(var cmd=new SqlCommand("UPDATE dbo.Recuperacion_Acceso SET Usado=1 WHERE Usuario=@u",cn,tx))
                    {cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=usuario;cmd.ExecuteNonQuery();}
                    using(var cmd=new SqlCommand(@"IF COL_LENGTH('dbo.Usuario_Credencial','DebeCambiarClave') IS NOT NULL EXEC sys.sp_executesql N'UPDATE dbo.Usuario_Credencial SET DebeCambiarClave=0 WHERE Id_Usuario IN(SELECT Id_Usuario FROM dbo.Usuario WHERE LoginName=@u)',N'@u nvarchar(256)',@u;",cn,tx)){cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=usuario;cmd.ExecuteNonQuery();}
                    tx.Commit();
                }
            }
        }
        static void Cambiar(SqlConnection cn, SqlTransaction tx, string usuario, string hash)
        {
            using (var cmd = new SqlCommand("SELECT Id_Usuario FROM dbo.Usuario WITH (UPDLOCK,HOLDLOCK) WHERE LoginName=@u", cn, tx))
            {
                cmd.Parameters.Add("@u", SqlDbType.NVarChar, 256).Value = usuario;
                object value = cmd.ExecuteScalar();
                if (value == null) throw new ArgumentException("Usuario inexistente.");
                Guardar(cn, tx, Convert.ToInt32(value), hash);
            }
        }
        public string Solicitar(string solicitud)
        {
            string usuario = null, email = null, nombre = null;
            using (var cn = new SqlConnection(acceso.GetConnectionString()))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT LoginName, Email, FirstName FROM dbo.Usuario WHERE LoginName=@u OR Email=@u", cn))
                {
                    cmd.Parameters.Add("@u", SqlDbType.NVarChar,256).Value = solicitud;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) { usuario = Convert.ToString(reader[0]); email = Convert.ToString(reader[1]); nombre = Convert.ToString(reader[2]); }
                        if (reader.Read()) return "Si los datos corresponden a una cuenta, recibirá un código por correo.";
                    }
                }
                if (usuario == null) return "Si los datos corresponden a una cuenta, recibirá un código por correo.";
                string token = Encriptador.Token();
                using (var cmd = new SqlCommand(@"INSERT dbo.Recuperacion_Acceso(Usuario,TokenHash,ExpiraUtc) VALUES(@u,@h,DATEADD(MINUTE,15,SYSUTCDATETIME()))",cn))
                {
                    cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=usuario;
                    cmd.Parameters.Add("@h",SqlDbType.VarChar,64).Value=Encriptador.TokenHash(token);
                    cmd.ExecuteNonQuery();
                }
                try { new CapaServicios.ServicesMail.SystemSupportMail().sendEmail("Recuperación de acceso", "Hola " + nombre + ". Su código es: " + token + "\nVence en 15 minutos y solo se puede usar una vez.", new List<string>{email}); }
                catch(Exception ex) { System.Diagnostics.Trace.TraceWarning("Recuperación: envío no completado; tipo {0}; HResult {1}.",ex.GetType().Name,ex.HResult); }
            }
            return "Si los datos corresponden a una cuenta, recibirá un código por correo.";
        }
        public void Restablecer(string solicitud, string token, string hash)
        {
            if (string.IsNullOrWhiteSpace(solicitud) || solicitud.Length > 254 || string.IsNullOrEmpty(token) || token.Length > 128)
                throw new ArgumentException("Código inválido o vencido.");
            using (var cn = new SqlConnection(acceso.GetConnectionString()))
            {
                cn.Open(); using (var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    string usuario;
                    using(var cmd=new SqlCommand("SELECT LoginName FROM dbo.Usuario WHERE LoginName=@u OR Email=@u",cn,tx))
                    { cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=solicitud; usuario=cmd.ExecuteScalar() as string; }
                    if (usuario == null) throw new ArgumentException("Código inválido o vencido.");
                    int id=0, intentos=0; string esperado=null;
                    using(var cmd=new SqlCommand(@"SELECT TOP(1) Id, TokenHash, Intentos FROM dbo.Recuperacion_Acceso WITH(UPDLOCK,HOLDLOCK) WHERE Usuario=@u AND Usado=0 AND ExpiraUtc>SYSUTCDATETIME() ORDER BY Id DESC",cn,tx))
                    {
                        cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=usuario;
                        using(var reader=cmd.ExecuteReader()) if(reader.Read()) {id=reader.GetInt32(0); esperado=reader.GetString(1); intentos=reader.GetInt32(2);}
                    }
                    if(id==0 || intentos>=5) throw new ArgumentException("Código inválido o vencido. Solicite uno nuevo.");
                    bool correcto=Encriptador.TokenValido(token, esperado);
                    using(var cmd=new SqlCommand("UPDATE dbo.Recuperacion_Acceso SET Intentos=Intentos+1 WHERE Id=@id",cn,tx))
                    {cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cmd.ExecuteNonQuery();}
                    if(!correcto) {tx.Commit();throw new ArgumentException("Código inválido o vencido.");}
                    Cambiar(cn,tx,usuario,hash);
                    using(var cmd=new SqlCommand("UPDATE dbo.Recuperacion_Acceso SET Usado=1 WHERE Usuario=@u",cn,tx))
                    {cmd.Parameters.Add("@u",SqlDbType.NVarChar,256).Value=usuario;cmd.ExecuteNonQuery();}
                    tx.Commit();
                }
            }
        }
    }
}
