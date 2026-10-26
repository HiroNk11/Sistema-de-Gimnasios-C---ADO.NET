using System;
using System.Data;
using CapaDominio;
using CapaRepositorio;
using CapaServicios.Composite;
namespace CapaAplicacion
{
    public sealed class ActividadesBLL
    {
        readonly ActividadesRepository repo=new ActividadesRepository();
        public DataSet Cargar(){Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);return repo.Cargar();}
        public void Guardar(Clase c){Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);Validar(c);repo.Guardar(c);}
        public static void Validar(Clase c){
            if(c==null || c.Id_Clase<0 || c.Id_Entrenamiento<=0 || c.Id_Entrenador<=0 || c.Id_piso<=0 || string.IsNullOrWhiteSpace(c.Descripcion) || c.Descripcion.Trim().Length>50 || c.Capacidad<1 || c.Capacidad>1000)throw new ArgumentException("Completá nombre, tipo, profesor, espacio y cupo de 1 a 1000.");
            c.Descripcion=c.Descripcion.Trim();
        }
    }
}
