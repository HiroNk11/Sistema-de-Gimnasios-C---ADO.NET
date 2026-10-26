using System;
using System.Data;
using CapaRepositorio;
using CapaServicios.Composite;

namespace CapaAplicacion
{
    public sealed class MonitoreoBLL
    {
        readonly IMonitoreoRepository repo;
        public MonitoreoBLL():this(new MonitoreoRepository()){}
        public MonitoreoBLL(IMonitoreoRepository repo){this.repo=repo ?? throw new ArgumentNullException(nameof(repo));}
        public DataSet Inicio(){Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);return repo.Inicio();}
        public DataSet Semana(DateTime fecha)
        {
            Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);
            return repo.Semana(Lunes(fecha));
        }
        public static DateTime Lunes(DateTime fecha)
        {
            var dia=fecha.Date.AddDays(-(((int)fecha.DayOfWeek+6)%7));
            if(dia.Year<1900 || dia>new DateTime(9999,12,24))throw new ArgumentException("Elegí una semana entre 1900 y el 24/12/9999.");
            return dia;
        }
    }
}
