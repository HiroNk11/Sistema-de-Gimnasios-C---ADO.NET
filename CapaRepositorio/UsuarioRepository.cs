using CapaDominio;
using CapaServicios;
using CapaServicios.Composite;
using CapaServicios.DV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace CapaRepositorio
{
    public class UsuarioRepository
    {
        AccesoRepository AR = new AccesoRepository();
        // involucrado con Login
        public Usuario GetByUserName(string user)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Search_User", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu", user);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                return new Usuario()
                {

                    idusuario = int.Parse(reader["Id_Usuario"].ToString()),
                    usuario = reader["LoginName"].ToString(),
                    password = reader["Password"].ToString()

                };

            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }
        // involucrado con gestion de usuario
        public List<Usuario> Listar()
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();
            DataTable tabla = AR.Leer("Listar_Usuario", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Usuario usu = new Usuario();
                usu.idusuario = int.Parse(registro["Id_Usuario"].ToString());
                usu.usuario = registro["LoginName"].ToString();
                usu.nombre = registro["FirstName"].ToString();
                usu.apellido = registro["LastName"].ToString();
                usu.email = registro["Email"].ToString();
                ListaUsuarios.Add(usu);
            }

            return ListaUsuarios;
        }
        public string Insertar(Usuario usu)
        {
            if (usu.password == null || !usu.password.StartsWith("PBKDF2$")) throw new ArgumentException("Formato de contraseña no válido.");
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open(); using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    // Password heredada queda inutilizable; la credencial real vive en la tabla nueva.
                    string marcador="RESTABLECER";
                    var dv = new UsuDv {usuario=usu.usuario, contraseña=marcador,nombre=usu.nombre,apellido=usu.apellido,email=usu.email};
                    using(var cmd=new SqlCommand("Insertar_Usuario",cn,tx))
                    {
                        cmd.CommandType=CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginName",usu.usuario);
                        cmd.Parameters.AddWithValue("@Password",marcador);
                        cmd.Parameters.AddWithValue("@FirstName",usu.nombre);
                        cmd.Parameters.AddWithValue("@LastName",usu.apellido);
                        cmd.Parameters.AddWithValue("@Email",usu.email);
                        cmd.Parameters.AddWithValue("@Dvh",DV.GenerarDVH(dv));cmd.ExecuteNonQuery();
                    }
                    using(var cmd=new SqlCommand("SELECT Id_Usuario FROM dbo.Usuario WHERE LoginName=@u",cn,tx))
                    {
                        cmd.Parameters.AddWithValue("@u",usu.usuario);
                        object id=cmd.ExecuteScalar();if(id==null)throw new InvalidOperationException("No se pudo crear el usuario.");
                        CredencialRepository.Guardar(cn,tx,Convert.ToInt32(id),usu.password);
                    }
                    ActualizarConteo(cn,tx);tx.Commit(); return "Operación Realizada con Exito";
                }
            }
        }
        public string Borrar(Usuario usu)
        {
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction())
                {
                    using(var cmd=new SqlCommand("DELETE Recuperacion_Acceso WHERE Usuario IN(SELECT LoginName FROM Usuario WHERE Id_Usuario=@id); DELETE Usuario_Permiso WHERE Id_Usuario=@id;",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",usu.idusuario);cmd.ExecuteNonQuery();}
                    using(var cmd=new SqlCommand("Borrar_Usuario",cn,tx))
                    {cmd.CommandType=CommandType.StoredProcedure;cmd.Parameters.AddWithValue("@UserId",usu.idusuario);cmd.ExecuteNonQuery();}
                    ActualizarConteo(cn,tx);tx.Commit();return "Usuario eliminado correctamente.";
                }
            }
        }
        static void ActualizarConteo(SqlConnection cn,SqlTransaction tx)
        {
            using(var cmd=new SqlCommand("UPDATE Digito_Verificador SET DVV=(SELECT COUNT(*) FROM Usuario)*80000 WHERE IdTabla=1",cn,tx))cmd.ExecuteNonQuery();
        }
        public string Editar(Usuario usu)
        {
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    using(var cmd=new SqlCommand("DELETE Recuperacion_Acceso WHERE Usuario IN(SELECT LoginName FROM Usuario WHERE Id_Usuario=@id) OR Usuario=@nuevo",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",usu.idusuario);cmd.Parameters.AddWithValue("@nuevo",usu.usuario);cmd.ExecuteNonQuery();}
                    using(var cmd=new SqlCommand("Editar_Usuario",cn,tx))
                    {
                        cmd.CommandType=CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreUsuario",usu.usuario);cmd.Parameters.AddWithValue("@Nombre",usu.nombre);cmd.Parameters.AddWithValue("@Apellido",usu.apellido);cmd.Parameters.AddWithValue("@Email",usu.email);cmd.Parameters.AddWithValue("@id",usu.idusuario);cmd.ExecuteNonQuery();
                    }
                    string hash;
                    using(var cmd=new SqlCommand("SELECT Password FROM Usuario WHERE Id_Usuario=@id",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",usu.idusuario);hash=Convert.ToString(cmd.ExecuteScalar());}
                    var campos=new UsuDv {usuario=usu.usuario,contraseña=hash,nombre=usu.nombre,apellido=usu.apellido,email=usu.email};
                    using(var cmd=new SqlCommand("UPDATE Usuario SET DVH=@h WHERE Id_Usuario=@id",cn,tx))
                    {cmd.Parameters.AddWithValue("@h",DV.GenerarDVH(campos));cmd.Parameters.AddWithValue("@id",usu.idusuario);cmd.ExecuteNonQuery();}
                    tx.Commit();return "Usuario actualizado correctamente.";
                }
            }
        }

        public DataTable Buscar_User(Usuario u)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            SqlCommand cmd = new SqlCommand("Buscar_User", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@l", u.usuario);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }

        // involucrado con permisos
        public void GuardarPermisos(PermisoUsuario u)
        {
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction())
                {
                    using(var cmd=new SqlCommand("DELETE FROM Usuario_Permiso WHERE Id_Usuario=@id",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",u.idusuario);cmd.ExecuteNonQuery();}
                    foreach(var permiso in u.Permisos)
                    using(var cmd=new SqlCommand("INSERT Usuario_Permiso(Id_Usuario,Id_Permiso) VALUES(@id,@p)",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",u.idusuario);cmd.Parameters.AddWithValue("@p",permiso.Id);cmd.ExecuteNonQuery();}
                    tx.Commit();
                }
            }
        }

        public string BorrarPermisos(Usuario usu)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", usu.idusuario);
            mensaje = AR.Escribir("Borrar_Permisos_Usuario", parametros);
            return mensaje;
        }

        public List<UsuDv> GetAllCamposDV()
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from Usuario;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<UsuDv>();

            while (reader.Read())
            {
                UsuDv c = new UsuDv();
                c.usuario = reader.GetString(reader.GetOrdinal("LoginName"));
                c.contraseña = reader.GetString(reader.GetOrdinal("Password"));
                c.nombre = reader.GetString(reader.GetOrdinal("FirstName"));
                c.apellido = reader.GetString(reader.GetOrdinal("LastName"));
                c.email = reader.GetString(reader.GetOrdinal("Email"));
                lista.Add(c);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }


        public List<PermisoUsuario> GetAll()
        {
            var cnn = new SqlConnection(AR.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from Usuario;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<PermisoUsuario>();

            while (reader.Read())
            {
                PermisoUsuario c = new PermisoUsuario();
                c.idusuario = reader.GetInt32(reader.GetOrdinal("Id_Usuario"));
                c.Nombre = reader.GetString(reader.GetOrdinal("LoginName"));
                lista.Add(c);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }
        // involucrado con lenguaje
        public Usuario Languaje(string username)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select Id_Usuario,LoginName, i.Id_Idioma,Nombre as nombre_idioma from Usuario u inner join Idioma i on u.Id_Idioma=i.Id_Idioma where LoginName = @username";
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                return new Usuario()
                {
                    idusuario = int.Parse(reader["id_usuario"].ToString()),
                    usuario = reader["username"].ToString(),

                    Idioma = new Idioma()
                    {
                        Nombre = reader["nombre_idioma"].ToString(),
                    }
                };
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }

        // Recuperar y Cambiar Contraseña
        public string recoverPassword(string userRequesting) { return new CredencialRepository().Solicitar(userRequesting); }

        public string recoverUser(string userRequesting)
        {
            const string respuesta = "Si el correo corresponde a una cuenta, recibirá un mensaje con su usuario.";
            string usuario = null, correo = null;
            using (var cn = new SqlConnection(AR.GetConnectionString()))
            using (var cmd = new SqlCommand("SELECT TOP(2) LoginName,Email FROM dbo.Usuario WHERE Email=@mail", cn))
            {
                cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 254).Value = userRequesting;
                cn.Open(); using (var reader = cmd.ExecuteReader())
                    if (reader.Read()) { usuario = Convert.ToString(reader[0]); correo = Convert.ToString(reader[1]); if (reader.Read()) return respuesta; }
            }
            if (usuario != null)
                try { new CapaServicios.ServicesMail.SystemSupportMail().sendEmail("Recuperación de usuario", "Su usuario es: " + usuario, new List<string> { correo }); }
                catch(Exception ex) { System.Diagnostics.Trace.TraceWarning("Recuperación: envío no completado; tipo {0}; HResult {1}.",ex.GetType().Name,ex.HResult); }
            return respuesta;
        }
        public string GenerarCodigo() { return Encriptador.Token(); }

        public string ChangePassword(string user) 
        {
            string password="";
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Change_Password", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", user);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                password = reader["Password"].ToString();
                return password;
              
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }
        public string UpdatePassword(string usu, string pass) { throw new UnauthorizedAccessException("Debe verificar contraseña actual o código de recuperación."); }

        // Validar Usuario
        public string GetUserName(string username) // obtengo el entrenamiento por el id 
        {
            string user= "";
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Search_User", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu", username);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                user = reader["LoginName"].ToString();
                return user;
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }

        public string GetEmail(string email) // obtengo el entrenamiento por el id 
        {
            string em = "";
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Search_EmailUser", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@em", email);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                em = reader["Email"].ToString();
                return em;
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }

        public string GetUserNameByUserOrEmail(string userRequesting)
        {
            string user;
            var sql = new SqlConnection(AR.GetConnectionString());
            sql.Open();
            var cmd = new SqlCommand();
            cmd.Connection = sql;
            cmd.CommandText = "select LoginName from Usuario where LoginName=@user or Email=@mail";
            cmd.Parameters.AddWithValue("@user", userRequesting);
            cmd.Parameters.AddWithValue("@mail", userRequesting);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            try
            { 
                if (!reader.Read()) return null;
                user = reader["LoginName"].ToString();
                return user;
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }

        }

        // digitos verificadores
          public List<object> DVHUsuario()
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string cadena = "select DVH from Usuario";
            sql.Open();
            SqlCommand cmd = new SqlCommand(cadena, sql);
            SqlDataReader reader = cmd.ExecuteReader();
            List<object> listadvh = new List<object>();
            while (reader.Read())
            {
                listadvh.Add(reader["DVH"]);
            }
            sql.Close();
            return listadvh;
        }
        public List<int> VerificarDigitosPorId()
        {
            var alterados=new List<int>();
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            using(var cmd=new SqlCommand("SELECT Id_Usuario,LoginName,Password,FirstName,LastName,Email,DVH FROM Usuario",cn))
            {
                cn.Open();using(var reader=cmd.ExecuteReader())while(reader.Read())
                {
                    var campos=new UsuDv {usuario=Convert.ToString(reader[1]),contraseña=Convert.ToString(reader[2]),nombre=Convert.ToString(reader[3]),apellido=Convert.ToString(reader[4]),email=Convert.ToString(reader[5])};
                    if(!string.Equals(DV.GenerarDVH(campos),Convert.ToString(reader[6]),StringComparison.OrdinalIgnoreCase))alterados.Add(Convert.ToInt32(reader[0]));
                }
            }
            return alterados;
        }
        public void RepararDigitos()
        {
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    var filas=new List<Tuple<int,string>>();
                    using(var cmd=new SqlCommand("SELECT Id_Usuario,LoginName,Password,FirstName,LastName,Email FROM Usuario WITH(UPDLOCK,HOLDLOCK)",cn,tx))
                    using(var reader=cmd.ExecuteReader())while(reader.Read())
                    {
                        var u=new UsuDv {usuario=Convert.ToString(reader[1]),contraseña=Convert.ToString(reader[2]),nombre=Convert.ToString(reader[3]),apellido=Convert.ToString(reader[4]),email=Convert.ToString(reader[5])};
                        filas.Add(Tuple.Create(Convert.ToInt32(reader[0]),DV.GenerarDVH(u)));
                    }
                    foreach(var fila in filas)using(var cmd=new SqlCommand("UPDATE Usuario SET DVH=@h WHERE Id_Usuario=@id",cn,tx))
                    {cmd.Parameters.AddWithValue("@h",fila.Item2);cmd.Parameters.AddWithValue("@id",fila.Item1);cmd.ExecuteNonQuery();}
                    using(var cmd=new SqlCommand("UPDATE Digito_Verificador SET DVV=(SELECT COUNT(*) FROM Usuario)*80000 WHERE IdTabla=1",cn,tx))cmd.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }
        public string ActualizarDVH(string dvh, int id)
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string cadena = "UPDATE Usuario set DVH = @dvh where Id_Usuario = @id";
            sql.Open();
            SqlCommand cmd = new SqlCommand(cadena, sql);
            cmd.Parameters.AddWithValue("@dvh", dvh);
            cmd.Parameters.AddWithValue("@id", id);
            mensaje = cmd.ExecuteNonQuery().ToString();
            sql.Close();
            return mensaje;
        }

        public string DVVUsuario()
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string cadena = "select count(*) * 80000 from Usuario";
            sql.Open();
            SqlCommand cmd = new SqlCommand(cadena, sql);
            mensaje = cmd.ExecuteScalar().ToString();
            sql.Close();
            return mensaje;
        }
        public string ActualizarDVV()
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string cadena = "select DVV from Digito_Verificador where IdTabla = 1";
            sql.Open();
            SqlCommand cmd = new SqlCommand(cadena, sql);
            mensaje = cmd.ExecuteScalar().ToString();
            sql.Close();
            return mensaje;
        }

        public string ActualizarDigitos()
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string cadena = "Update Digito_Verificador set DVV = (select count(*) from Usuario) *80000 where IdTabla = 1";
            sql.Open();
            SqlCommand cmd = new SqlCommand(cadena, sql);
            mensaje = cmd.ExecuteNonQuery().ToString();
            sql.Close();
            return mensaje;
        }
    }
    }
