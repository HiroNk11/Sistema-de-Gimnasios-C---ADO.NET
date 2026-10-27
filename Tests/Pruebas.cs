using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CapaServicios;
using CapaServicios.Composite;
using CapaAplicacion;
using CapaAplicacion.Negocio;
using CapaRepositorio;
using CapaRepositorio.Negocio;
using CapaDominio;
class Pruebas
{
    static int aprobadas;
    static void Check(bool value,string nombre) {if(!value)throw new Exception("FALLO: "+nombre);aprobadas++;Console.WriteLine("OK: "+nombre);}
    static void Rechaza<T>(Action action,string nombre) where T:Exception
    {try{action();}catch(T){Check(true,nombre);return;}throw new Exception("No rechazó: "+nombre);}
    static object Scalar(string sql)
    {using(var cn=new SqlConnection(Environment.GetEnvironmentVariable("GYM_TEST_CONNECTION_STRING")))using(var cmd=new SqlCommand(sql,cn)){cn.Open();return cmd.ExecuteScalar();}}
    static void Script(string path)
    {foreach(string batch in Regex.Split(File.ReadAllText(path),@"^\s*GO\s*$",RegexOptions.Multiline|RegexOptions.IgnoreCase))if(!string.IsNullOrWhiteSpace(batch))Scalar(batch);}
    static int Count(string sql) {return Convert.ToInt32(Scalar(sql));}
    static void Retry(Action action)
    {for(int i=0;;i++){try{action();return;}catch(SqlException ex){if(ex.Number!=1205 || i>=4)throw;System.Threading.Thread.Sleep(100*(i+1));}}}
    static void Integracion(string root)
    {
        string cs=Environment.GetEnvironmentVariable("GYM_TEST_CONNECTION_STRING");
        if(string.IsNullOrWhiteSpace(cs)){Console.WriteLine("NO EJECUTADO: integración SQL. Configure GYM_TEST_CONNECTION_STRING con una base VACÍA cuyo nombre empiece por GymTests_.");return;}
        var builder=new SqlConnectionStringBuilder(cs);
        if(!builder.InitialCatalog.StartsWith("GymTests_",StringComparison.Ordinal))throw new Exception("La integración solo admite bases GymTests_.");
        Check(Count("SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped=0")==0,"base de pruebas vacía");
        Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING",cs);
        Script(Path.Combine(root,"Database","000_esquema_original.sql"));
        Script(Path.Combine(root,"Tests","fixture.sql"));
        Script(Path.Combine(root,"Database","001_actualizacion.sql"));
        Script(Path.Combine(root,"Database","003_compatibilidad_DatabaseGym.sql"));
        Script(Path.Combine(root,"Database","001_actualizacion.sql"));
        Script(Path.Combine(root,"Database","003_compatibilidad_DatabaseGym.sql"));Check(true,"migraciones repetibles sobre el esquema real");
        Check(Convert.ToDateTime(Scalar("SELECT Fecha_Inicio FROM Reserva WHERE Id_Reserva=1"))==new DateTime(2022,11,24),"fecha inglesa original preservada");
        Check(Convert.ToDateTime(Scalar("SELECT Fecha_Fin FROM Reserva WHERE Id_Reserva=2"))==new DateTime(2022,12,7),"fecha dd/mm preservada sin invertir mes y día");
        Check(Convert.ToString(Scalar("SELECT TYPE_NAME(system_type_id) FROM sys.columns WHERE object_id=OBJECT_ID('Reserva') AND name='Id_Clase'"))=="int","ID de clase entero en historial");
        var repo=new OperacionRepository();DateTime inicio=DateTime.Today.AddDays(3).AddHours(18);
        for(int socio=1;socio<=12;socio++){repo.Membresia(socio,"Mensual",DateTime.Today,DateTime.Today.AddMonths(1),100.50m,1);repo.Pago(socio,100.50m,"Efectivo","prueba",1);}
        repo.Sesion(1,inicio,inicio.AddHours(1),1,1);
        Rechaza<InvalidOperationException>(()=>repo.Sesion(1,inicio,inicio.AddHours(1),2,1),"solapamiento profesor/espacio");
        Parallel.For(1,9,s=>Retry(()=>repo.Reservar(1,s,1)));
        Check(Count("SELECT COUNT(*) FROM App_Reserva WHERE Estado='Confirmada'")==1,"último cupo con ocho reservas concurrentes");
        Check(Count("SELECT COUNT(*) FROM App_Reserva WHERE Estado='Espera'")==7,"sobrantes en lista de espera");
        int ganador=Count("SELECT Id_Socio FROM App_Reserva WHERE Estado='Confirmada'");
        Rechaza<InvalidOperationException>(()=>repo.Reservar(1,ganador,1),"reserva duplicada rechazada");
        int reserva=Count("SELECT Id FROM App_Reserva WHERE Estado='Confirmada'");repo.CancelarReserva(reserva,1);
        Check(Count("SELECT COUNT(*) FROM App_Reserva WHERE Estado='Confirmada'")==1,"promoción automática al cancelar");
        Rechaza<InvalidOperationException>(()=>repo.CancelarReserva(reserva,1),"cancelación repetida rechazada");
        Rechaza<InvalidOperationException>(()=>repo.Asistencia(reserva,true,1),"asistencia cancelada rechazada");
        repo.CancelarSesion(1,1);Check(Count("SELECT COUNT(*) FROM App_Reserva WHERE Estado<>'Cancelada'")==0,"cancelación de sesión y reservas");
        repo.Membresia(12,"Parcial",DateTime.Today,DateTime.Today.AddMonths(1),150,1);
        repo.Pago(13,50,"Transferencia","parcial",1);
        Rechaza<InvalidOperationException>(()=>repo.Pago(13,101,"Efectivo","exceso",1),"pago mayor al saldo rechazado");
        Check(Count("SELECT SUM(Importe) FROM App_Pago WHERE Id_Membresia=13")==50,"pago rechazado no modifica saldo");
        var viejo=new SocioRepository();var sc=new Socio_Clase {Id_Socio=1,Id_Clase=1,Id_piso=1,Precio=50.25m,cantidad_dias=2,FechaInicio=DateTime.Today,FechaFin=DateTime.Today.AddDays(7)};
        viejo.Insertar_Socio_Clase(sc);Check(Count("SELECT Capacidad FROM Clase WHERE Id_Clase=1")==1,"inscripción semanal descuenta un cupo");
        Rechaza<InvalidOperationException>(()=>viejo.Insertar_Socio_Clase(sc),"inscripción semanal duplicada revierte descuento");
        Check(Count("SELECT Capacidad FROM Clase WHERE Id_Clase=1")==1,"rollback conserva cupo");
        Check(Count("SELECT COUNT(*) FROM App_Auditoria")>0,"auditoría de operaciones");
        var credencial=new CredencialRepository();string token=Encriptador.Token();
        using(var cn=new SqlConnection(cs))using(var cmd=new SqlCommand("INSERT Recuperacion_Acceso(Usuario,TokenHash,ExpiraUtc) VALUES('admin',@hash,DATEADD(minute,15,SYSUTCDATETIME()))",cn))
        {cmd.Parameters.AddWithValue("@hash",Encriptador.TokenHash(token));cn.Open();cmd.ExecuteNonQuery();}
        string hash=Encriptador.Hash("Contraseña de prueba 123");credencial.Restablecer("admin",token,hash);
        Check(Encriptador.Verify("Contraseña de prueba 123",credencial.Obtener(1)),"restablecimiento persiste credencial");
        Rechaza<ArgumentException>(()=>credencial.Restablecer("admin",token,hash),"código no se puede reutilizar");
        var permisos=new PermisosRepository();Check(permisos.BuscarPermisos(TipoPermiso.PuedeAccederANegocio,new PermisoUsuario {idusuario=1}),"permiso heredado desde familia");
        Check(!permisos.BuscarPermisos(TipoPermiso.PuedeCambiarIdioma,new PermisoUsuario {idusuario=1}),"familia no concede permiso cero por defecto");
        var usuarios=new UsuarioRepository();
        string claveNueva="Nueva clave de prueba 2026";
        string hashNuevo=Encriptador.Hash(claveNueva);
        Parallel.For(0,6,i=>Retry(()=>usuarios.Insertar(new Usuario {usuario="usuario"+i,nombre="Prueba",apellido="Concurrente",email="usuario"+i+"@example.invalid",password=hashNuevo})));
        Check(Count("SELECT COUNT(*) FROM Usuario")==7,"seis altas concurrentes con secuencia");
        Check(Count("SELECT COUNT(DISTINCT Id_Usuario) FROM Usuario")==7,"IDs de usuario únicos sin MAX más uno");
        var nuevo=usuarios.GetByUserName("usuario0");Check(nuevo!=null,"Search_User compatible con esquema real");
        Check(Encriptador.Verify(claveNueva,credencial.Obtener(nuevo.idusuario)),"alta conserva PBKDF2 en tabla de credenciales");
        using(var cn=new SqlConnection(cs))using(var cmd=new SqlCommand("Listar_Usuario",cn))using(var da=new SqlDataAdapter(cmd))
        {cmd.CommandType=CommandType.StoredProcedure;var tabla=new DataTable();da.Fill(tabla);Check(!tabla.Columns.Contains("Password"),"listado SQL no expone contraseñas");}
        foreach(string nombre in new[]{"PuedeAccederBackup","PuedeAccederRestore"})Check(Enum.IsDefined(typeof(TipoPermiso),nombre),"permiso original compatible: "+nombre);
        usuarios.Insertar(new Usuario {usuario="correoLargo",nombre="Prueba",apellido="Correo",email=new string('a',55)+"@example.invalid",password=hashNuevo});
        Check(usuarios.GetByUserName("correoLargo")!=null,"correo mayor a 50 caracteres soportado");
        new BitacoraRepository().InsertarEvento(new Bitacora {id_usuario=1,actividad=new string('a',100),criticidad="Baja",fecha="21/10/2026",hora="12:00:00"});
        Check(Count("SELECT COUNT(*) FROM Bitacora WHERE MomentoUtc IS NOT NULL AND LEN(Actividad)=100")==1,"bitácora admite actividad extensa y timestamp UTC");
        IntegracionMonitoreo(repo);
        Console.WriteLine("La base de prueba se conserva para inspección: "+builder.InitialCatalog);
    }
    static void IntegracionMonitoreo(OperacionRepository repo)
    {
        repo.Membresia(12,"Otra deuda",DateTime.Today,DateTime.Today.AddDays(5),50,1);
        var monitor=new MonitoreoRepository();var inicio=monitor.Inicio();
        Check(Convert.ToInt32(inicio.Tables["Resumen"].Rows[0]["Socios"])==12,"resumen cuenta socios registrados");
        Check(Convert.ToInt32(inicio.Tables["Resumen"].Rows[0]["SociosConSaldo"])==1,"dos membresias pendientes cuentan una sola persona");
        Check(Convert.ToDecimal(inicio.Tables["Resumen"].Rows[0]["SaldoPendiente"])==150m,"saldos suman deudas sin multiplicar los pagos");
        Check(inicio.Tables["Pendientes"].Rows.Count==1 && Convert.ToInt32(inicio.Tables["Pendientes"].Rows[0]["Id_Socio"])==12,"listado agrupa deuda por socio");
        DateTime dia=DateTime.Today.AddDays(5).AddHours(10);
        repo.Sesion(1,dia,dia.AddHours(1),1,1);int a=Count("SELECT MAX(Id) FROM App_Sesion");
        repo.Sesion(2,dia.AddMinutes(30),dia.AddMinutes(90),1,1);int b=Count("SELECT MAX(Id) FROM App_Sesion");
        repo.Sesion(1,dia.AddHours(1),dia.AddHours(2),2,1);int c=Count("SELECT MAX(Id) FROM App_Sesion");
        repo.Reservar(a,1,1);repo.Reservar(a,2,1);
        Rechaza<InvalidOperationException>(()=>repo.Reservar(b,1,1),"rechaza cruce parcial de confirmada");
        Rechaza<InvalidOperationException>(()=>repo.Reservar(b,2,1),"rechaza cruce parcial de lista de espera");
        Check(repo.Reservar(c,2,1).Contains("confirmada"),"permite actividades consecutivas aunque la primera este en espera");
        int reserva=Count("SELECT Id FROM App_Reserva WHERE Id_Sesion="+a+" AND Id_Socio=1 AND Estado='Confirmada'");repo.CancelarReserva(reserva,1);
        Check(Count("SELECT COUNT(*) FROM App_Reserva WHERE Id_Sesion="+a+" AND Id_Socio=2 AND Estado='Confirmada'")==1,"promocion no confunde actividades consecutivas con cruces");
        Check(repo.Reservar(b,1,1).Contains("confirmada"),"reserva cancelada libera el horario del socio");
        int aceptadas=0,rechazadas=0;
        Parallel.ForEach(new[]{a,b},id=>{try{Retry(()=>repo.Reservar(id,3,1));System.Threading.Interlocked.Increment(ref aceptadas);}catch(InvalidOperationException){System.Threading.Interlocked.Increment(ref rechazadas);}});
        Check(aceptadas==1 && rechazadas==1,"dos altas concurrentes superpuestas solo aceptan una, incluso en espera");
        var semana=monitor.Semana(MonitoreoBLL.Lunes(dia));
        Check(semana.Tables["Sesiones"].Rows.Cast<DataRow>().Any(r=>Convert.ToInt32(r["Id"])==b),"calendario consulta sesiones por semana");
        Check(semana.Tables["Participantes"].Rows.Cast<DataRow>().Any(r=>Convert.ToInt32(r["Id_Sesion"])==b && Convert.ToInt32(r["Id_Socio"])==1),"calendario conserva participantes y sesion correspondientes");
    }
    static int Main(string[] args)
    {
        try
        {
            string root=args.Length>0?Path.GetFullPath(args[0]):Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..",".."));
            string clave="Contraseña segura ☀ 2026";string hash=Encriptador.Hash(clave),otro=Encriptador.Hash(clave);
            Check(hash.StartsWith("PBKDF2$600000$"),"formato PBKDF2 SHA256");Check(hash!=otro,"sal aleatoria por contraseña");
            Check(Encriptador.Verify(clave,hash),"contraseña correcta");Check(!Encriptador.Verify(clave+"x",hash),"contraseña incorrecta");
            Check(!Encriptador.Verify("Contrasena segura ☀ 2026",hash),"Unicode no se degrada a ASCII");
            foreach(string malformed in new[]{"","MD5-antiguo","PBKDF2$1$a$b","PBKDF2$600000$???$???","PBKDF2$99999999$a$b"})Check(!Encriptador.Verify(clave,malformed),"hash inválido rechazado");
            Check(!Encriptador.Verify(null,hash),"clave nula rechazada");Check(!Encriptador.Verify(new string('a',129),hash),"límite de longitud al verificar");
            Rechaza<ArgumentException>(()=>Encriptador.Hash("corta"),"mínimo de 15 caracteres");Rechaza<ArgumentException>(()=>Encriptador.Hash(new string('a',129)),"máximo de 128 caracteres");
            string token=Encriptador.Token();Check(Convert.FromBase64String(token).Length==24,"token con 192 bits aleatorios");Check(token!=Encriptador.Token(),"tokens diferentes");
            Check(Encriptador.TokenHash(token)==Encriptador.TokenHash(token),"hash de token estable");Check(Encriptador.TokenHash(token)!=Encriptador.TokenHash(token+"x"),"hash de token sensible al contenido");
            Rechaza<UnauthorizedAccessException>(()=>Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio),"operación sin sesión rechazada");
            Rechaza<UnauthorizedAccessException>(()=>new OperacionBLL().Listar("Panel"),"panel protegido en BLL");
            Rechaza<UnauthorizedAccessException>(()=>new OperacionBLL().Pago(1,10,"Efectivo",""),"cobro protegido en BLL");
            Rechaza<UnauthorizedAccessException>(()=>new UsuarioBLL().UpdatePassword("Nueva contraseña 123","admin","Nueva contraseña 123"),"atajo de cambio sin verificación deshabilitado");
            Rechaza<UnauthorizedAccessException>(()=>new UsuarioBLL().CambiarPassword("x","Nueva contraseña 123","Nueva contraseña 123"),"cambio de clave sin sesión rechazado");
            Rechaza<UnauthorizedAccessException>(()=>new UsuarioBLL().Insertar(new Usuario()),"alta de usuario sin permiso rechazada");
            Rechaza<InvalidOperationException>(()=>new SocioRepository().Borrar_socioclase(new Socio_Clase()),"borrado masivo por fecha deshabilitado");
            Rechaza<ArgumentException>(()=>new BackupRestoreRepository().Restore("archivo.txt"),"restauración requiere archivo bak");
            string previous=Environment.GetEnvironmentVariable("GYM_CONNECTION_STRING");
            try {Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING","Data Source=test;Initial Catalog=GymTests_Config;Integrated Security=True;Encrypt=True;TrustServerCertificate=False");Check(new SqlConnectionStringBuilder(new AccesoRepository().GetConnectionString()).InitialCatalog=="GymTests_Config","conexión configurable por entorno");}
            finally {Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING",previous);}
            Check(Encriptador.TokenValido(token,Encriptador.TokenHash(token)),"token correcto validado");
            Check(!Encriptador.TokenValido(token+"x",Encriptador.TokenHash(token)),"token incorrecto rechazado");
            Check(!Encriptador.TokenValido(new string('a',129),Encriptador.TokenHash(token)),"token excesivo rechazado");
            Check(!Encriptador.Verify(clave,new string('a',257)),"hash excesivo rechazado antes de parsear");
            Rechaza<ArgumentException>(()=>Encriptador.Hash(new string('a',14)),"clave de catorce caracteres rechazada");
            Rechaza<ArgumentException>(()=>Encriptador.Hash("passwordpassword"),"clave común rechazada");
            string emoji=string.Concat(Enumerable.Repeat(char.ConvertFromUtf32(0x1F642),14));
            Rechaza<ArgumentException>(()=>Encriptador.Hash(emoji),"Unicode cuenta caracteres y no pares UTF16");
            Encriptador.ValidarPassword(emoji+"a");Check(true,"Unicode de quince caracteres permitido");
            Rechaza<UnauthorizedAccessException>(()=>new UsuarioBLL().GetEmail("prueba@example.invalid"),"consulta de correo requiere permiso");
            Rechaza<UnauthorizedAccessException>(()=>new UsuarioBLL().GetAll(),"listado de usuarios requiere permiso");
            try
            {
                Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING","Data Source=test;Initial Catalog=GymTests_Config;Integrated Security=True;Encrypt=False");
                Rechaza<InvalidOperationException>(()=>new AccesoRepository().GetConnectionString(),"SQL remoto sin TLS rechazado");
                Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING","Data Source=test;Initial Catalog=GymTests_Config;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
                Rechaza<InvalidOperationException>(()=>new AccesoRepository().GetConnectionString(),"SQL remoto con bypass de certificado rechazado");
                Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING",@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GymTests_Config;Integrated Security=True;Encrypt=False;Persist Security Info=True;Connect Timeout=0");
                var segura=new SqlConnectionStringBuilder(new AccesoRepository().GetConnectionString());
                Check(!segura.PersistSecurityInfo&&segura.ConnectTimeout==15,"LocalDB conserva excepción local y limita conexión");
            }
            finally {Environment.SetEnvironmentVariable("GYM_CONNECTION_STRING",previous);}
            Check(Enum.IsDefined(typeof(TipoPermiso),"PuedeAccederBackup"),"nombre de permiso Backup del script original");
            Check(Enum.IsDefined(typeof(TipoPermiso),"PuedeAccederRestore"),"nombre de permiso Restore del script original");
            Rechaza<UnauthorizedAccessException>(()=>Autorizacion.ExigirCualquiera(TipoPermiso.PuedeAccederBackup,TipoPermiso.PuedeAccederBackupRestore),"permisos alternativos requieren sesión");
            var script=File.ReadAllText(Path.Combine(root,"Database","003_compatibilidad_DatabaseGym.sql"));
            Check(!script.Contains("UPDATE dbo.Permiso SET Permiso"),"migración no convierte Backup en permiso de Restore");
            Integracion(root);
            Console.WriteLine("APROBADAS: "+aprobadas);return 0;
        }
        catch(Exception ex){Console.WriteLine(ex);return 1;}
    }
}
