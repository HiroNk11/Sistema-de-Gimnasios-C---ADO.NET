using System;
using System.Collections.Generic;
using System.Linq;
namespace CapaAplicacion
{
    public sealed class AccesoTemporalmenteBloqueadoException : InvalidOperationException
    {
        public AccesoTemporalmenteBloqueadoException() : base("Demasiados intentos fallidos. Esperá hasta 15 minutos antes de volver a intentar.") {}
    }
    internal static class IntentosLogin
    {
        class Estado { public int Intentos; public DateTime Hasta = DateTime.UtcNow.AddMinutes(15); }
        static readonly Dictionary<string, Estado> estados = new Dictionary<string, Estado>(StringComparer.OrdinalIgnoreCase);
        static readonly object sincronizacion = new object();
        public static void Validar(string usuario)
        {
            lock(sincronizacion)
            {
                Estado estado;
                if(estados.TryGetValue(usuario.Trim(),out estado) && estado.Hasta > DateTime.UtcNow && estado.Intentos>=5)
                    throw new AccesoTemporalmenteBloqueadoException();
            }
        }
        public static void Fallar(string usuario)
        {
            lock(sincronizacion)
            {
                Estado estado; string key=usuario.Trim();
                foreach(string vencido in estados.Where(x=>x.Value.Hasta<=DateTime.UtcNow).Select(x=>x.Key).ToArray()) estados.Remove(vencido);
                if(estados.Count>=4096 && !estados.ContainsKey(key)) throw new AccesoTemporalmenteBloqueadoException();
                if(!estados.TryGetValue(key,out estado) || estado.Hasta <= DateTime.UtcNow) estados[key]=estado=new Estado();
                estado.Intentos++;
            }
        }
        public static void Limpiar(string usuario) { lock(sincronizacion) estados.Remove(usuario.Trim()); }
    }
}
