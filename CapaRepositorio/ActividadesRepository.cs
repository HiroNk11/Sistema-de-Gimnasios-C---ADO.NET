using System;
using System.Data;
using System.Data.SqlClient;
using CapaDominio;
namespace CapaRepositorio
{
    public sealed class ActividadesRepository
    {
        readonly AccesoRepository acceso=new AccesoRepository();
        public DataSet Cargar()
        {
            using(var cn=new SqlConnection(acceso.GetConnectionString()))using(var cmd=new SqlCommand(@"
SELECT c.Id_Clase,c.Descripcion,c.Id_Entrenamiento,c.Id_Entrenador,c.Id_Piso,c.Capacidad,e.Nombre AS Actividad,ent.Nombre+' '+ent.Apellido AS Entrenador,p.Descripcion AS Espacio FROM dbo.Clase c JOIN dbo.Entrenamiento e ON e.Id_Entrenamiento=c.Id_Entrenamiento JOIN dbo.Entrenador ent ON ent.Id_Entrenador=c.Id_Entrenador JOIN dbo.Piso p ON p.Id_Piso=c.Id_Piso ORDER BY c.Descripcion,c.Id_Clase;
SELECT Id_Entrenamiento,Nombre AS Descripcion FROM dbo.Entrenamiento ORDER BY Nombre;
SELECT Id_Entrenador,Nombre+' '+Apellido+' (#'+CONVERT(varchar(12),Id_Entrenador)+')' AS Nombre FROM dbo.Entrenador ORDER BY Nombre,Apellido;
SELECT Id_Piso,Nivel+' - '+Descripcion AS Nombre FROM dbo.Piso ORDER BY Nivel,Descripcion;",cn))using(var da=new SqlDataAdapter(cmd)){var ds=new DataSet();da.Fill(ds);return ds;}
        }
        public void Guardar(Clase clase)
        {
            using(var cn=new SqlConnection(acceso.GetConnectionString())) {
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable)) {
                    using(var cmd=new SqlCommand("SELECT COUNT_BIG(*) FROM dbo.App_Sesion WITH(TABLOCKX,HOLDLOCK)",cn,tx))cmd.ExecuteScalar();
                    if(clase.Id_Clase>0) {
                        using(var cmd=new SqlCommand(@"SELECT Id_Entrenador,Id_Piso FROM dbo.Clase WITH(UPDLOCK,HOLDLOCK) WHERE Id_Clase=@id",cn,tx)) {
                            cmd.Parameters.Add("@id",SqlDbType.Int).Value=clase.Id_Clase;
                            using(var rd=cmd.ExecuteReader()){
                                if(!rd.Read())throw new ArgumentException("La actividad ya no existe.");
                                bool cambia=rd.GetInt32(0)!=clase.Id_Entrenador || rd.GetInt32(1)!=clase.Id_piso;rd.Close();
                                if(cambia)using(var validar=new SqlCommand("SELECT (SELECT COUNT(*) FROM dbo.App_Sesion WHERE Id_Clase=@id AND Estado='Programada' AND Fin>GETDATE())+(SELECT COUNT(*) FROM dbo.Horario WHERE Id_Clase=@id)",cn,tx)){
                                    validar.Parameters.Add("@id",SqlDbType.Int).Value=clase.Id_Clase;
                                    if(Convert.ToInt32(validar.ExecuteScalar())>0)throw new InvalidOperationException("No se puede cambiar profesor o espacio con horarios asociados. Creá otra actividad para la nueva configuración y conservá el historial.");
                                }
                            }
                        }
                    }
                    string sql=clase.Id_Clase==0?"INSERT dbo.Clase(Id_Entrenamiento,Id_Entrenador,Id_Piso,Descripcion,Capacidad) VALUES(@tipo,@entrenador,@piso,@nombre,@cupo)":"UPDATE dbo.Clase SET Id_Entrenamiento=@tipo,Id_Entrenador=@entrenador,Id_Piso=@piso,Descripcion=@nombre WHERE Id_Clase=@id";
                    using(var cmd=new SqlCommand(sql,cn,tx)){
                        cmd.Parameters.Add("@tipo",SqlDbType.Int).Value=clase.Id_Entrenamiento;cmd.Parameters.Add("@entrenador",SqlDbType.Int).Value=clase.Id_Entrenador;
                        cmd.Parameters.Add("@piso",SqlDbType.Int).Value=clase.Id_piso;cmd.Parameters.Add("@nombre",SqlDbType.NVarChar,50).Value=clase.Descripcion;
                        cmd.Parameters.Add("@cupo",SqlDbType.Int).Value=clase.Capacidad;cmd.Parameters.Add("@id",SqlDbType.Int).Value=clase.Id_Clase;cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }
    }
}
