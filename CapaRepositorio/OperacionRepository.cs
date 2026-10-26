using System;
using System.Data;
using System.Data.SqlClient;
namespace CapaRepositorio
{
    // Tablas App_* independientes de las inscripciones semanales heredadas.
    public class OperacionRepository
    {
        readonly AccesoRepository acceso=new AccesoRepository();
        SqlConnection Conectar() {var cn=new SqlConnection(acceso.GetConnectionString());cn.Open();return cn;}
        static SqlCommand Comando(SqlConnection cn,SqlTransaction tx,string sql,params object[] args)
        {
            var cmd=new SqlCommand(sql,cn,tx);
            for(int i=0;i<args.Length;i++)cmd.Parameters.AddWithValue("@p"+i,args[i]??DBNull.Value);
            return cmd;
        }
        static void BloquearReservas(SqlConnection cn,SqlTransaction tx)
        {
            // Un orden comun en altas, cancelaciones y promociones evita dos reservas
            // simultaneas incompatibles. Adecuado para la recepcion de uno o dos puestos.
            using(var cmd=Comando(cn,tx,"SELECT COUNT_BIG(*) FROM dbo.App_Reserva WITH(TABLOCKX,HOLDLOCK)"))cmd.ExecuteScalar();
        }
        public DataTable Listar(string seccion,int socio=0)
        {
            string sql;
            switch(seccion)
            {
                case "Sesiones":sql=@"SELECT s.Id,s.Id_Clase,c.Descripcion AS Clase,s.Inicio,s.Fin,s.Capacidad,
(SELECT COUNT(*) FROM App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Confirmada') AS Inscriptos,
s.Capacidad-(SELECT COUNT(*) FROM App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Confirmada') AS Disponibles,
(SELECT COUNT(*) FROM App_Reserva r WHERE r.Id_Sesion=s.Id AND r.Estado='Espera') AS EnEspera,s.Estado FROM App_Sesion s JOIN Clase c ON c.Id_Clase=s.Id_Clase ORDER BY s.Inicio DESC";break;
                case "Membresias":sql=@"SELECT m.Id,m.Id_Socio,s.Nombre+' '+s.Apellido AS Socio,m.[Plan],m.Desde,m.Hasta,m.Importe,
COALESCE((SELECT SUM(p.Importe) FROM App_Pago p WHERE p.Id_Membresia=m.Id),0) AS Pagado,
m.Importe-COALESCE((SELECT SUM(p.Importe) FROM App_Pago p WHERE p.Id_Membresia=m.Id),0) AS Saldo
FROM App_Membresia m JOIN Socio s ON s.Id_Socio=m.Id_Socio WHERE @p0=0 OR m.Id_Socio=@p0 ORDER BY m.Id DESC";break;
                case "Reservas":sql=@"SELECT r.Id,r.Id_Socio,so.Nombre+' '+so.Apellido AS Socio,c.Descripcion AS Clase,s.Inicio,r.Estado,r.Asistio
FROM App_Reserva r JOIN App_Sesion s ON s.Id=r.Id_Sesion JOIN Clase c ON c.Id_Clase=s.Id_Clase JOIN Socio so ON so.Id_Socio=r.Id_Socio
WHERE @p0=0 OR r.Id_Socio=@p0 ORDER BY s.Inicio DESC,r.Id";break;
                case "Pagos":sql=@"SELECT p.Id,p.Id_Membresia,s.Nombre+' '+s.Apellido AS Socio,p.Importe,p.Medio,p.Referencia,p.CreadoUtc FROM App_Pago p JOIN App_Membresia m ON m.Id=p.Id_Membresia JOIN Socio s ON s.Id_Socio=m.Id_Socio WHERE @p0=0 OR m.Id_Socio=@p0 ORDER BY p.Id DESC";break;
                case "Panel":sql=@"SELECT 'Membresías por vencer (7 días)' AS Indicador,CONVERT(decimal(18,2),COUNT(*)) AS Valor FROM App_Membresia WHERE Hasta>=CONVERT(date,GETDATE()) AND Hasta<=DATEADD(day,7,CONVERT(date,GETDATE()))
UNION ALL SELECT 'Saldo pendiente',COALESCE(SUM(m.Importe-COALESCE(p.Pagado,0)),0) FROM App_Membresia m OUTER APPLY(SELECT SUM(Importe) AS Pagado FROM App_Pago WHERE Id_Membresia=m.Id)p
UNION ALL SELECT 'Sesiones futuras',COUNT(*) FROM App_Sesion WHERE Inicio>=GETDATE() AND Estado='Programada'
UNION ALL SELECT 'Reservas en espera',COUNT(*) FROM App_Reserva WHERE Estado='Espera'";break;
                default:throw new ArgumentException("Sección desconocida.");
            }
            using(var cn=Conectar())using(var cmd=Comando(cn,null,sql,socio))using(var da=new SqlDataAdapter(cmd))
            {var dt=new DataTable();da.Fill(dt);return dt;}
        }
        public void Membresia(int socio,string plan,DateTime desde,DateTime hasta,decimal importe,int usuario)
        {
            using(var cn=Conectar())using(var cmd=Comando(cn,null,"INSERT App_Membresia(Id_Socio,[Plan],Desde,Hasta,Importe,Id_Usuario) VALUES(@p0,@p1,@p2,@p3,@p4,@p5)",socio,plan,desde.Date,hasta.Date,importe,usuario))cmd.ExecuteNonQuery();
        }
        public void Pago(int membresia,decimal importe,string medio,string referencia,int usuario)
        {
            using(var cn=Conectar())using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
            {
                decimal saldo;
                using(var cmd=Comando(cn,tx,@"SELECT m.Importe-COALESCE((SELECT SUM(p.Importe) FROM App_Pago p WHERE p.Id_Membresia=m.Id),0) FROM App_Membresia m WITH(UPDLOCK,HOLDLOCK) WHERE m.Id=@p0",membresia))
                {object value=cmd.ExecuteScalar();if(value==null)throw new InvalidOperationException("Seleccione una membresía existente.");saldo=Convert.ToDecimal(value);}
                if(importe>saldo)throw new InvalidOperationException("El pago supera el saldo pendiente.");
                using(var cmd=Comando(cn,tx,"INSERT App_Pago(Id_Membresia,Importe,Medio,Referencia,Id_Usuario) VALUES(@p0,@p1,@p2,@p3,@p4)",membresia,importe,medio,referencia,usuario))cmd.ExecuteNonQuery();
                tx.Commit();
            }
        }
        public void Sesion(int clase,DateTime inicio,DateTime fin,int capacidad,int usuario)
        {
            new PlanificacionRepository().Guardar(clase,new[] {new CapaDominio.SesionPlanificada {Inicio=inicio,Fin=fin}},capacidad,usuario);
        }
        public string Reservar(int sesion,int socio,int usuario)
        {
            using(var cn=Conectar())using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
            {
                BloquearReservas(cn,tx);
                int capacidad;DateTime inicio,fin;string estado;
                using(var cmd=Comando(cn,tx,"SELECT Capacidad,Inicio,Fin,Estado FROM App_Sesion WITH(UPDLOCK,HOLDLOCK) WHERE Id=@p0",sesion))
                using(var reader=cmd.ExecuteReader())
                {if(!reader.Read())throw new ArgumentException("La sesión no existe.");capacidad=reader.GetInt32(0);inicio=reader.GetDateTime(1);fin=reader.GetDateTime(2);estado=reader.GetString(3);}
                if(estado!="Programada" || inicio<=DateTime.Now)throw new InvalidOperationException("La sesión no admite nuevas reservas.");
                using(var cmd=Comando(cn,tx,"SELECT COUNT(*) FROM App_Reserva WHERE Id_Sesion=@p0 AND Id_Socio=@p1 AND Estado IN('Confirmada','Espera')",sesion,socio))
                if(Convert.ToInt32(cmd.ExecuteScalar())>0)throw new InvalidOperationException("El socio ya tiene una reserva para esa sesión.");
                using(var cmd=Comando(cn,tx,@"SELECT COUNT(*) FROM App_Membresia m WHERE Id_Socio=@p0 AND Desde<=@p1 AND Hasta>=@p1 AND m.Importe<=COALESCE((SELECT SUM(p.Importe) FROM App_Pago p WHERE p.Id_Membresia=m.Id),0)",socio,inicio.Date))
                if(Convert.ToInt32(cmd.ExecuteScalar())==0)throw new InvalidOperationException("El socio necesita una membresía vigente y pagada para la fecha de la sesión.");
                using(var cmd=Comando(cn,tx,@"SELECT COUNT(*) FROM App_Reserva r JOIN App_Sesion s ON s.Id=r.Id_Sesion WHERE r.Id_Socio=@p0 AND r.Estado IN('Confirmada','Espera') AND s.Estado='Programada' AND s.Inicio<@p2 AND s.Fin>@p1",socio,inicio,fin))
                if(Convert.ToInt32(cmd.ExecuteScalar())>0)throw new InvalidOperationException("El socio ya tiene una reserva confirmada o en espera que se cruza con ese horario. Puede elegir actividades consecutivas o cancelar primero la otra reserva.");
                int ocupados;
                using(var cmd=Comando(cn,tx,"SELECT COUNT(*) FROM App_Reserva WHERE Id_Sesion=@p0 AND Estado='Confirmada'",sesion))ocupados=Convert.ToInt32(cmd.ExecuteScalar());
                string nuevo=ocupados<capacidad?"Confirmada":"Espera";
                using(var cmd=Comando(cn,tx,"INSERT App_Reserva(Id_Sesion,Id_Socio,Estado,Id_Usuario) VALUES(@p0,@p1,@p2,@p3)",sesion,socio,nuevo,usuario))cmd.ExecuteNonQuery();
                tx.Commit();return nuevo=="Confirmada"?"Reserva confirmada.":"Sesión completa: socio agregado a la lista de espera.";
            }
        }
        public void CancelarReserva(int reserva,int usuario)
        {
            using(var cn=Conectar())
            {
                int sesion;
                using(var cmd=Comando(cn,null,"SELECT Id_Sesion FROM App_Reserva WHERE Id=@p0",reserva))
                {object value=cmd.ExecuteScalar();if(value==null)throw new ArgumentException("Reserva inexistente.");sesion=Convert.ToInt32(value);}
                using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    BloquearReservas(cn,tx);
                    DateTime inicio;
                    using(var cmd=Comando(cn,tx,"SELECT Inicio FROM App_Sesion WITH(UPDLOCK,HOLDLOCK) WHERE Id=@p0",sesion))inicio=Convert.ToDateTime(cmd.ExecuteScalar());
                    if(inicio<=DateTime.Now)throw new InvalidOperationException("Solo se pueden cancelar reservas futuras.");
                    string anterior;
                    using(var cmd=Comando(cn,tx,"SELECT Estado FROM App_Reserva WITH(UPDLOCK,HOLDLOCK) WHERE Id=@p0",reserva))anterior=Convert.ToString(cmd.ExecuteScalar());
                    if(anterior=="Cancelada")throw new InvalidOperationException("La reserva ya fue cancelada.");
                    using(var cmd=Comando(cn,tx,"UPDATE App_Reserva SET Estado='Cancelada',ActualizadoUtc=SYSUTCDATETIME(),Id_Usuario=@p1 WHERE Id=@p0",reserva,usuario))cmd.ExecuteNonQuery();
                    if(anterior=="Confirmada")
                    using(var cmd=Comando(cn,tx,@"UPDATE App_Reserva SET Estado='Confirmada',ActualizadoUtc=SYSUTCDATETIME(),Id_Usuario=@p1 WHERE Id=(SELECT TOP(1) r.Id FROM App_Reserva r JOIN App_Sesion s ON s.Id=r.Id_Sesion WHERE r.Id_Sesion=@p0 AND r.Estado='Espera'
AND EXISTS(SELECT 1 FROM App_Membresia m WHERE m.Id_Socio=r.Id_Socio AND m.Desde<=CONVERT(date,s.Inicio) AND m.Hasta>=CONVERT(date,s.Inicio) AND m.Importe<=COALESCE((SELECT SUM(p.Importe) FROM App_Pago p WHERE p.Id_Membresia=m.Id),0))
AND NOT EXISTS(SELECT 1 FROM App_Reserva r2 JOIN App_Sesion s2 ON s2.Id=r2.Id_Sesion WHERE r2.Id<>r.Id AND r2.Id_Socio=r.Id_Socio AND r2.Estado IN('Confirmada','Espera') AND s2.Estado='Programada' AND s2.Inicio<s.Fin AND s2.Fin>s.Inicio)
ORDER BY r.Id)",sesion,usuario))cmd.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }
        public void CancelarSesion(int sesion,int usuario)
        {
            using(var cn=Conectar())using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
            {
                BloquearReservas(cn,tx);
                using(var cmd=Comando(cn,tx,"UPDATE App_Sesion WITH(UPDLOCK,HOLDLOCK) SET Estado='Cancelada',Id_Usuario=@p1 WHERE Id=@p0 AND Inicio>GETDATE() AND Estado='Programada'",sesion,usuario))
                if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("Seleccione una sesión futura programada.");
                using(var cmd=Comando(cn,tx,"UPDATE App_Reserva SET Estado='Cancelada',ActualizadoUtc=SYSUTCDATETIME(),Id_Usuario=@p1 WHERE Id_Sesion=@p0 AND Estado IN('Confirmada','Espera')",sesion,usuario))cmd.ExecuteNonQuery();
                tx.Commit();
            }
        }
        public void Asistencia(int reserva,bool asistio,int usuario)
        {
            using(var cn=Conectar())using(var cmd=Comando(cn,null,@"UPDATE r SET Asistio=@p1,ActualizadoUtc=SYSUTCDATETIME(),Id_Usuario=@p2 FROM App_Reserva r JOIN App_Sesion s ON s.Id=r.Id_Sesion WHERE r.Id=@p0 AND r.Estado='Confirmada' AND s.Inicio<=GETDATE() AND s.Estado='Programada'",reserva,asistio,usuario))
            if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("Solo se registra asistencia en reservas confirmadas de sesiones iniciadas.");
        }
    }
}
