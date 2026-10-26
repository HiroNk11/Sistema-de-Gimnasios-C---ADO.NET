using System;
using System.Collections.Generic;
using System.Linq;
namespace CapaDominio
{
    public sealed class SesionPlanificada
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }
    public static class Planificador
    {
        public static List<SesionPlanificada> Generar(DateTime desde, DateTime hasta, TimeSpan horaInicio, TimeSpan horaFin, IEnumerable<DayOfWeek> dias, DateTime ahora)
        {
            if(hasta.Date<desde.Date || (hasta.Date-desde.Date).TotalDays>366) throw new ArgumentException("Elegí un período de hasta un año, con fin posterior al inicio.");
            if(horaInicio<TimeSpan.Zero || horaFin>=TimeSpan.FromDays(1) || horaFin<=horaInicio) throw new ArgumentException("La hora final debe ser posterior al inicio, dentro del mismo día.");
            var elegidos=new HashSet<DayOfWeek>(dias ?? Enumerable.Empty<DayOfWeek>());
            if(elegidos.Count==0 || elegidos.Any(x=>(int)x<0 || (int)x>6))throw new ArgumentException("Seleccioná al menos un día de la semana.");
            var resultado=new List<SesionPlanificada>();
            for(var fecha=desde.Date;fecha<=hasta.Date;fecha=fecha.AddDays(1))
                if(elegidos.Contains(fecha.DayOfWeek)) {
                    var inicio=fecha.Add(horaInicio);
                    if(inicio<=ahora)throw new ArgumentException("El período incluye sesiones pasadas. Elegí una fecha de inicio futura.");
                    resultado.Add(new SesionPlanificada {Inicio=inicio,Fin=fecha.Add(horaFin)});
                }
            if(resultado.Count==0)throw new ArgumentException("No hay coincidencias entre el período y los días elegidos.");
            if(resultado.Count>200)throw new ArgumentException("Programá hasta 200 sesiones por operación. Acortá el período.");
            return resultado;
        }
        public static bool SeSuperponen(DateTime inicio,DateTime fin,DateTime otroInicio,DateTime otroFin)
        { return inicio<otroFin && fin>otroInicio; }
    }
}
