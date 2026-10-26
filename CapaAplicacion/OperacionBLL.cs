using System;
using System.Data;
using CapaRepositorio;
using CapaServicios;
using CapaServicios.Composite;
namespace CapaAplicacion
{
    public class OperacionBLL
    {
        readonly OperacionRepository repo=new OperacionRepository();
        int Autorizar() {Autorizacion.Exigir(TipoPermiso.PuedeAccederANegocio);return SingletonSesion.Instancia.Usuario.idusuario;}
        public DataTable Listar(string seccion,int socio=0) {Autorizar();return repo.Listar(seccion,socio);}
        public void Membresia(int socio,string plan,DateTime desde,DateTime hasta,decimal importe)
        {
            int usuario=Autorizar();
            if(socio<=0 || string.IsNullOrWhiteSpace(plan) || plan.Length>100 || hasta.Date<desde.Date || importe<0 || importe>100000000 || decimal.Round(importe,2)!=importe)throw new ArgumentException("Revise socio, plan, fechas e importe (hasta 2 decimales).");
            repo.Membresia(socio,plan.Trim(),desde,hasta,importe,usuario);
        }
        public void Pago(int membresia,decimal importe,string medio,string referencia)
        {
            int usuario=Autorizar();
            if(membresia<=0 || importe<=0 || importe>100000000 || decimal.Round(importe,2)!=importe || (medio!="Efectivo" && medio!="Transferencia" && medio!="Tarjeta") || referencia.Length>100)throw new ArgumentException("Revise la membresía, importe, medio de pago y referencia.");
            repo.Pago(membresia,importe,medio,referencia,usuario);
        }
        public void Sesion(int clase,DateTime inicio,DateTime fin,int capacidad)
        {
            int usuario=Autorizar();
            if(clase<=0 || inicio<=DateTime.Now || fin<=inicio || (fin-inicio).TotalHours>24 || capacidad<1 || capacidad>1000)throw new ArgumentException("Seleccione clase, horario futuro (duración hasta 24 horas) y capacidad de 1 a 1000.");
            repo.Sesion(clase,inicio,fin,capacidad,usuario);
        }
        public string Reservar(int sesion,int socio) {return repo.Reservar(sesion,socio,Autorizar());}
        public void CancelarReserva(int id) {repo.CancelarReserva(id,Autorizar());}
        public void CancelarSesion(int id) {repo.CancelarSesion(id,Autorizar());}
        public void Asistencia(int id,bool asistio) {repo.Asistencia(id,asistio,Autorizar());}
    }
}
