using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaRepositorio
{
    public interface IMonitoreoRepository
    {
        DataSet Inicio();
        DataSet Semana(DateTime lunes);
    }

    public sealed class MonitoreoRepository : IMonitoreoRepository
    {
        DataSet Consultar(string sql, bool bloquearReservas, params SqlParameter[] parametros)
        {
            using(var cn=new SqlConnection(new AccesoRepository().GetConnectionString()))
            using(var cmd=new SqlCommand(sql,cn))
            using(var adapter=new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(parametros);
                cn.Open();
                using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    cmd.Transaction=tx;
                    if(bloquearReservas)
                    using(var bloqueo=new SqlCommand("SELECT COUNT_BIG(*) FROM dbo.App_Reserva WITH(TABLOCK,HOLDLOCK)",cn,tx))bloqueo.ExecuteScalar();
                    // Las cifras y sus listados corresponden a la misma lectura.
                    var datos=new DataSet();adapter.Fill(datos);tx.Commit();return datos;
                }
            }
        }
        public DataSet Inicio()
        {
            var datos=Consultar(@"SELECT
 (SELECT COUNT(*) FROM dbo.Socio) AS Socios,
 (SELECT COUNT(DISTINCT m.Id_Socio) FROM dbo.App_Membresia m
  WHERE m.Importe>COALESCE((SELECT SUM(p.Importe) FROM dbo.App_Pago p WHERE p.Id_Membresia=m.Id),0)) AS SociosConSaldo,
 COALESCE((SELECT SUM(m.Importe-COALESCE(p.Pagado,0)) FROM dbo.App_Membresia m
  OUTER APPLY(SELECT SUM(p.Importe) AS Pagado FROM dbo.App_Pago p WHERE p.Id_Membresia=m.Id)p
  WHERE m.Importe>COALESCE(p.Pagado,0)),0) AS SaldoPendiente,
 (SELECT COUNT(*) FROM dbo.App_Sesion WHERE Estado='Programada' AND Inicio>=CONVERT(date,GETDATE()) AND Inicio<DATEADD(day,1,CONVERT(date,GETDATE()))) AS SesionesHoy;

 SELECT s.Id_Socio,s.Nombre+' '+s.Apellido AS Socio,COUNT(*) AS MembresiasPendientes,
 SUM(m.Importe-COALESCE(p.Pagado,0)) AS Saldo,MIN(m.Hasta) AS PrimerVencimiento
 FROM dbo.App_Membresia m JOIN dbo.Socio s ON s.Id_Socio=m.Id_Socio
 OUTER APPLY(SELECT SUM(p.Importe) AS Pagado FROM dbo.App_Pago p WHERE p.Id_Membresia=m.Id)p
 WHERE m.Importe>COALESCE(p.Pagado,0)
 GROUP BY s.Id_Socio,s.Nombre,s.Apellido ORDER BY Saldo DESC,s.Apellido,s.Nombre,s.Id_Socio;",false);
            datos.Tables[0].TableName="Resumen";datos.Tables[1].TableName="Pendientes";return datos;
        }
        public DataSet Semana(DateTime lunes)
        {
            var datos=Consultar(@"SELECT s.Id,s.Id_Clase,c.Descripcion AS Actividad,
 e.Nombre+' '+e.Apellido AS Profesor,p.Nivel+' - '+p.Descripcion AS Espacio,s.Inicio,s.Fin,s.Capacidad,s.Estado,
 (SELECT COUNT(*) FROM dbo.App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Confirmada') AS Confirmados,
 (SELECT COUNT(*) FROM dbo.App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Espera') AS EnEspera
 FROM dbo.App_Sesion s JOIN dbo.Clase c ON c.Id_Clase=s.Id_Clase
 JOIN dbo.Entrenador e ON e.Id_Entrenador=c.Id_Entrenador JOIN dbo.Piso p ON p.Id_Piso=c.Id_Piso
 WHERE s.Inicio<@hasta AND s.Fin>@desde ORDER BY s.Inicio,c.Descripcion,s.Id;

 SELECT r.Id AS Id_Reserva,r.Id_Sesion,r.Id_Socio,so.Nombre+' '+so.Apellido AS Socio,
 r.Estado,r.Asistio FROM dbo.App_Reserva r JOIN dbo.Socio so ON so.Id_Socio=r.Id_Socio
 JOIN dbo.App_Sesion s ON s.Id=r.Id_Sesion WHERE s.Inicio<@hasta AND s.Fin>@desde
 ORDER BY CASE r.Estado WHEN 'Confirmada' THEN 0 WHEN 'Espera' THEN 1 ELSE 2 END,so.Apellido,so.Nombre,r.Id;",true,
                new SqlParameter("@desde",SqlDbType.DateTime2){Value=lunes.Date},
                new SqlParameter("@hasta",SqlDbType.DateTime2){Value=lunes.Date.AddDays(7)});
            datos.Tables[0].TableName="Sesiones";datos.Tables[1].TableName="Participantes";return datos;
        }
    }
}
