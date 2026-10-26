using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using CapaDominio;
using CapaRepositorio;

class PruebasBitacora
{
    static int comprobaciones;
    static void Check(bool valor, string mensaje)
    {
        if (!valor) throw new Exception(mensaje);
        comprobaciones++;
        Console.WriteLine("OK: " + mensaje);
    }
    static SqlParameter[] Parametros(Bitacora evento)
    {
        return (SqlParameter[])typeof(BitacoraRepository).GetMethod("CrearParametrosEvento", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] {evento});
    }
    static int Main()
    {
        try
        {
            var evento = new Bitacora {id_usuario=7, actividad="Cierre de Sesion", fecha="26/10/2026", hora="16:40:00"};
            var parametros = Parametros(evento);
            Check(parametros.Length == 6 && parametros.Select(p=>p.ParameterName).Distinct().Count()==6, "los seis parametros del procedimiento son unicos");
            Check(parametros.All(p=>p.Value != null), "el evento sin criticidad no omite ningun parametro SQL");
            Check((string)parametros.Single(p=>p.ParameterName=="@Criticidad").Value=="Baja", "criticidad omitida usa Baja");
            foreach(var valor in new[] {"", "  ", "Alta"})
            {
                evento.criticidad=valor;
                Check((string)Parametros(evento).Single(p=>p.ParameterName=="@Criticidad").Value==(string.IsNullOrWhiteSpace(valor)?"Baja":valor), "criticidad vacia tiene valor y la explicita se conserva");
            }
            Check((int)parametros.Single(p=>p.ParameterName=="@Id_Usuario").Value==7, "se conserva el autor del evento");
            var momento=parametros.Single(p=>p.ParameterName=="@MomentoUtc");
            Check(momento.SqlDbType==SqlDbType.DateTime2 && ((DateTime)momento.Value).Kind==DateTimeKind.Utc, "momento de auditoria UTC tipado");
            Check(Parametros(new Bitacora()).Where(p=>new[]{"@Actividad","@Fecha","@Hora"}.Contains(p.ParameterName)).All(p=>p.Value==DBNull.Value), "los nulos se representan explicitamente sin omitir parametros");
            Console.WriteLine(comprobaciones+" comprobaciones aprobadas. No se ejecuto SQL real.");
            return 0;
        }
        catch(Exception error) {Console.Error.WriteLine(error);return 1;}
    }
}
