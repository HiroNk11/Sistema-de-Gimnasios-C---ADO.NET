using System;
using System.Collections.Generic;
using System.Linq;
using CapaDominio;
using CapaRepositorio;
using CapaServicios;
using CapaServicios.Composite;
namespace CapaAplicacion
{
    public sealed class PlanificacionBLL
    {
        readonly IPlanificacionRepository repo;
        public PlanificacionBLL():this(new PlanificacionRepository()){}
        public PlanificacionBLL(IPlanificacionRepository repo){this.repo=repo ?? throw new ArgumentNullException(nameof(repo));}
        public int Guardar(int clase,IReadOnlyList<SesionPlanificada> sesiones,int capacidad)
        {
            Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);
            Validar(clase,sesiones,capacidad,DateTime.Now);
            return repo.Guardar(clase,sesiones,capacidad,SingletonSesion.Instancia.Usuario.idusuario);
        }
        public static void Validar(int clase,IReadOnlyList<SesionPlanificada> sesiones,int capacidad,DateTime ahora)
        {
            if(clase<=0 || capacidad<1 || capacidad>1000)throw new ArgumentException("Seleccioná una actividad y un cupo de 1 a 1000.");
            if(sesiones==null || sesiones.Count==0 || sesiones.Count>200)throw new ArgumentException("Revisá entre 1 y 200 sesiones antes de guardar.");
            if(sesiones.Any(x=>x==null || x.Inicio<=ahora || x.Fin<=x.Inicio || (x.Fin-x.Inicio).TotalHours>24))throw new ArgumentException("Las sesiones deben ser futuras y tener una duración válida de hasta 24 horas.");
            var ordenadas=sesiones.OrderBy(x=>x.Inicio).ToList();
            for(int i=1;i<ordenadas.Count;i++)if(Planificador.SeSuperponen(ordenadas[i-1].Inicio,ordenadas[i-1].Fin,ordenadas[i].Inicio,ordenadas[i].Fin))throw new ArgumentException("La propuesta contiene sesiones duplicadas o superpuestas.");
        }
    }
}
