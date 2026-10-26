using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaDominio;
namespace CapaRepositorio
{
    public interface IPlanificacionRepository
    {
        int Guardar(int clase,IReadOnlyList<SesionPlanificada> sesiones,int capacidad,int usuario);
    }
    public sealed class PlanificacionRepository : IPlanificacionRepository
    {
        readonly AccesoRepository acceso=new AccesoRepository();
        public int Guardar(int clase,IReadOnlyList<SesionPlanificada> sesiones,int capacidad,int usuario)
        {
            using(var cn=new SqlConnection(acceso.GetConnectionString())) {
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable)) {
                    // Orden de bloqueo común para lotes y sesiones individuales.
                    using(var bloqueo=new SqlCommand("SELECT COUNT_BIG(*) FROM dbo.App_Sesion WITH(TABLOCKX,HOLDLOCK)",cn,tx))bloqueo.ExecuteScalar();
                    int entrenador,piso;
                    using(var cmd=new SqlCommand("SELECT Id_Entrenador,Id_Piso FROM dbo.Clase WITH(UPDLOCK,HOLDLOCK) WHERE Id_Clase=@id",cn,tx)) {
                        cmd.Parameters.Add("@id",SqlDbType.Int).Value=clase;
                        using(var rd=cmd.ExecuteReader()){if(!rd.Read())throw new ArgumentException("La actividad ya no existe. Actualizá la lista.");entrenador=rd.GetInt32(0);piso=rd.GetInt32(1);}
                    }
                    var conflictos=new List<string>();
                    foreach(var s in sesiones) {
                        using(var cmd=new SqlCommand(@"SELECT TOP(1) c.Descripcion,s.Inicio FROM dbo.App_Sesion s JOIN dbo.Clase c WITH(HOLDLOCK) ON c.Id_Clase=s.Id_Clase WHERE s.Estado='Programada' AND s.Inicio<@fin AND s.Fin>@inicio AND (c.Id_Entrenador=@entrenador OR c.Id_Piso=@piso) ORDER BY s.Inicio",cn,tx)) {
                            cmd.Parameters.Add("@inicio",SqlDbType.DateTime2).Value=s.Inicio;cmd.Parameters.Add("@fin",SqlDbType.DateTime2).Value=s.Fin;
                            cmd.Parameters.Add("@entrenador",SqlDbType.Int).Value=entrenador;cmd.Parameters.Add("@piso",SqlDbType.Int).Value=piso;
                            using(var rd=cmd.ExecuteReader())if(rd.Read())conflictos.Add(s.Inicio.ToString("dd/MM/yyyy HH:mm")+": "+rd.GetString(0));
                        }
                    }
                    if(conflictos.Count>0)throw new InvalidOperationException("No se guardó ninguna sesión. Hay cruces de profesor o espacio:\n"+string.Join("\n",conflictos.GetRange(0,Math.Min(8,conflictos.Count)))+(conflictos.Count>8?"\nY otros "+(conflictos.Count-8)+" conflictos.":""));
                    foreach(var s in sesiones) {
                        using(var cmd=new SqlCommand("INSERT dbo.App_Sesion(Id_Clase,Inicio,Fin,Capacidad,Id_Usuario) VALUES(@clase,@inicio,@fin,@capacidad,@usuario)",cn,tx)) {
                            cmd.Parameters.Add("@clase",SqlDbType.Int).Value=clase;cmd.Parameters.Add("@inicio",SqlDbType.DateTime2).Value=s.Inicio;cmd.Parameters.Add("@fin",SqlDbType.DateTime2).Value=s.Fin;
                            cmd.Parameters.Add("@capacidad",SqlDbType.Int).Value=capacidad;cmd.Parameters.Add("@usuario",SqlDbType.Int).Value=usuario;cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();return sesiones.Count;
                }
            }
        }
    }
}
