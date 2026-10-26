using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaAplicacion;
using CapaDominio;
namespace CapaIU.Presentacion
{
    internal interface IServicioEntrenadores
    {
        List<Entrenador> Listar();
        void Guardar(Entrenador entrenador, bool nuevo);
        void Eliminar(Entrenador entrenador);
    }
    // Adaptador: la presentación no depende del repositorio SQL.
    internal sealed class ServicioEntrenadores : IServicioEntrenadores
    {
        readonly EntrenadorBLL gestor = new EntrenadorBLL();
        public List<Entrenador> Listar() { return gestor.Listar(); }
        public void Guardar(Entrenador e, bool nuevo) { if (nuevo) gestor.Insertar(e); else gestor.Editar(e); }
        public void Eliminar(Entrenador e) { gestor.Borrar(e); }
    }
    internal sealed class EntrenadoresPresentador
    {
        readonly IServicioEntrenadores servicio;
        List<Entrenador> datos = new List<Entrenador>();
        public EntrenadoresPresentador(IServicioEntrenadores servicio) { this.servicio = servicio ?? throw new ArgumentNullException(nameof(servicio)); }
        public async Task Cargar() { datos = await Task.Run(() => servicio.Listar()); }
        public List<Entrenador> Buscar(string texto)
        {
            string q = (texto ?? "").Trim();
            return datos.Where(x => (x.Nombre + " " + x.Apellido + " " + x.Id_Entrenador).IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }
        public Task Guardar(int? id, string nombre, string apellido)
        {
            var e = new Entrenador { Id_Entrenador = id ?? 0, Nombre = (nombre ?? "").Trim(), Apellido = (apellido ?? "").Trim() };
            if (e.Nombre.Length == 0 || e.Apellido.Length == 0) throw new ArgumentException("Completá nombre y apellido.");
            if (e.Nombre.Length > 50 || e.Apellido.Length > 50) throw new ArgumentException("Nombre y apellido admiten hasta 50 caracteres.");
            return Task.Run(() => servicio.Guardar(e, !id.HasValue));
        }
        public Task Eliminar(int? id)
        {
            if (!id.HasValue || id.Value <= 0) throw new ArgumentException("Seleccioná un entrenador.");
            return Task.Run(() => servicio.Eliminar(new Entrenador { Id_Entrenador = id.Value }));
        }
    }
}
