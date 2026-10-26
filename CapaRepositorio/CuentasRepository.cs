using System;
using System.Data;
using System.Data.SqlClient;
using CapaDominio;
using CapaServicios.DV;
namespace CapaRepositorio
{
    public sealed class CuentasRepository
    {
        public static string NombrePerfil(PerfilCuenta perfil){switch(perfil){case PerfilCuenta.AdministracionDeCuentas:return "APP Administracion de cuentas";case PerfilCuenta.Recepcion:return "APP Recepcion";default:throw new ArgumentException("Perfil no válido.");}}
        public void Crear(Usuario usuario,PerfilCuenta perfil,int autor)
        {
            string familia=NombrePerfil(perfil);
            using(var cn=new SqlConnection(new AccesoRepository().GetConnectionString())){
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable)){
                    int rol;
                    using(var cmd=new SqlCommand("SELECT Id_Permiso FROM dbo.Permiso WITH(UPDLOCK,HOLDLOCK) WHERE Nombre=@nombre AND Permiso IS NULL",cn,tx)){
                        cmd.Parameters.Add("@nombre",SqlDbType.NVarChar,100).Value=familia;object v=cmd.ExecuteScalar();if(v==null)throw new InvalidOperationException("Falta configurar los perfiles. Ejecutá el asistente ConfigurarPerfil.ps1.");rol=Convert.ToInt32(v);
                    }
                    using(var cmd=new SqlCommand("SELECT COUNT(*) FROM dbo.Usuario WITH(UPDLOCK,HOLDLOCK) WHERE LoginName=@u OR Email=@e",cn,tx)){
                        cmd.Parameters.Add("@u",SqlDbType.NVarChar,50).Value=usuario.usuario;cmd.Parameters.Add("@e",SqlDbType.NVarChar,254).Value=usuario.email;
                        if(Convert.ToInt32(cmd.ExecuteScalar())>0)throw new ArgumentException("El usuario o correo ya está registrado.");
                    }
                    var dv=new UsuDv {usuario=usuario.usuario,contraseña="RESTABLECER",nombre=usuario.nombre,apellido=usuario.apellido,email=usuario.email};
                    using(var cmd=new SqlCommand("Insertar_Usuario",cn,tx)){
                        cmd.CommandType=CommandType.StoredProcedure;cmd.Parameters.AddWithValue("@LoginName",usuario.usuario);cmd.Parameters.AddWithValue("@Password","RESTABLECER");cmd.Parameters.AddWithValue("@FirstName",usuario.nombre);cmd.Parameters.AddWithValue("@LastName",usuario.apellido);cmd.Parameters.AddWithValue("@Email",usuario.email);cmd.Parameters.AddWithValue("@Dvh",DV.GenerarDVH(dv));cmd.ExecuteNonQuery();
                    }
                    int id;
                    using(var cmd=new SqlCommand("SELECT Id_Usuario FROM dbo.Usuario WHERE LoginName=@u",cn,tx)){cmd.Parameters.Add("@u",SqlDbType.NVarChar,50).Value=usuario.usuario;id=Convert.ToInt32(cmd.ExecuteScalar());}
                    CredencialRepository.Guardar(cn,tx,id,usuario.password);
                    using(var cmd=new SqlCommand(@"UPDATE dbo.Usuario_Credencial SET DebeCambiarClave=1 WHERE Id_Usuario=@id;
INSERT dbo.Usuario_Permiso(Id_Usuario,Id_Permiso) VALUES(@id,@rol);
UPDATE dbo.Digito_Verificador SET DVV=(SELECT COUNT(*) FROM dbo.Usuario)*80000 WHERE IdTabla=1;
INSERT dbo.App_Auditoria(Tabla,Registro,Operacion,Id_Usuario) VALUES('Usuario',@id,'INSERT',@autor);",cn,tx)){
                        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cmd.Parameters.Add("@rol",SqlDbType.Int).Value=rol;cmd.Parameters.Add("@autor",SqlDbType.Int).Value=autor;cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }
    }
}
