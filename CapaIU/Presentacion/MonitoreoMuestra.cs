using System;
using System.Data;
using CapaAplicacion;
namespace CapaIU.Presentacion
{
    // Solamente para constructores de vista previa y pruebas; nunca se mezcla con SQL.
    internal static class MonitoreoMuestra
    {
        static DataTable Tabla(string nombre,string[] columnas,Type[] tipos)
        {
            var t=new DataTable(nombre);for(int i=0;i<columnas.Length;i++)t.Columns.Add(columnas[i],tipos[i]);return t;
        }
        public static DataSet Inicio()
        {
            var ds=new DataSet();
            var resumen=Tabla("Resumen",new[]{"Socios","SociosConSaldo","SaldoPendiente","SesionesHoy"},new[]{typeof(int),typeof(int),typeof(decimal),typeof(int)});
            resumen.Rows.Add(150,4,45000m,6);ds.Tables.Add(resumen);
            var pendientes=Tabla("Pendientes",new[]{"Id_Socio","Socio","MembresiasPendientes","Saldo","PrimerVencimiento"},new[]{typeof(int),typeof(string),typeof(int),typeof(decimal),typeof(DateTime)});
            pendientes.Rows.Add(11,"Alex Ejemplo",2,20000m,DateTime.Today.AddDays(-3));pendientes.Rows.Add(12,"Sam Muestra",1,15000m,DateTime.Today.AddDays(2));pendientes.Rows.Add(13,"Andrea Prueba",1,7000m,DateTime.Today.AddDays(8));pendientes.Rows.Add(14,"María Ensayo",1,3000m,DateTime.Today.AddDays(4));ds.Tables.Add(pendientes);return ds;
        }
        public static DataSet Semana(DateTime lunes)
        {
            var ds=new DataSet();var sesiones=Tabla("Sesiones",new[]{"Id","Id_Clase","Actividad","Profesor","Espacio","Inicio","Fin","Capacidad","Estado","Confirmados","EnEspera"},new[]{typeof(int),typeof(int),typeof(string),typeof(string),typeof(string),typeof(DateTime),typeof(DateTime),typeof(int),typeof(string),typeof(int),typeof(int)});
            int id=1;for(int dia=0;dia<7;dia++)
            {
                sesiones.Rows.Add(id++,1,"Funcional","Alex Profesor","Planta baja",lunes.AddDays(dia).AddHours(8),lunes.AddDays(dia).AddHours(9),20,"Programada",12,0);
                if(dia<5)sesiones.Rows.Add(id++,2,"Pilates","Sam Profesor","Sala 2",lunes.AddDays(dia).AddHours(18),lunes.AddDays(dia).AddHours(19),12,"Programada",12,2);
            }
            sesiones.Rows.Add(id++,3,"Movilidad","Andrea Profesora","Sala 3",lunes.AddHours(18.25),lunes.AddHours(19.5),10,"Programada",4,0);
            sesiones.Rows.Add(id++,4,"Sesión cancelada","Sam Profesor","Sala 2",lunes.AddDays(2).AddHours(10),lunes.AddDays(2).AddHours(11),10,"Cancelada",0,0);
            ds.Tables.Add(sesiones);
            var personas=Tabla("Participantes",new[]{"Id_Reserva","Id_Sesion","Id_Socio","Socio","Estado","Asistio"},new[]{typeof(int),typeof(int),typeof(int),typeof(string),typeof(string),typeof(bool)});
            foreach(DataRow s in sesiones.Rows)if((string)s["Estado"]=="Programada")
            {for(int i=1;i<=Convert.ToInt32(s["Confirmados"])+Convert.ToInt32(s["EnEspera"]);i++)personas.Rows.Add((int)s["Id"]*100+i,(int)s["Id"],i,"Socio Ejemplo "+i.ToString("00"),i<=Convert.ToInt32(s["Confirmados"])?"Confirmada":"Espera",DBNull.Value);}
            ds.Tables.Add(personas);return ds;
        }
    }
}
