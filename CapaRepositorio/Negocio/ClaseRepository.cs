using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaRepositorio.Negocio
{
    public class ClaseRepository
    {
        AccesoRepository AR = new AccesoRepository();
 /*REVISAR*/
        public List<Detalle_Clase> GetClaseALLDescription() /*Detalles de Clase tabla completa muestra todas las clases*/
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            List<Detalle_Clase> tra = new List<Detalle_Clase>();

            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select Clase.Descripcion,ent.Nombre as Actividad,e.Nombre +' '+e.Apellido as Entrenador,p.Nivel as Nivel_Piso,p.Descripcion as Descripcion_Piso,d.Nombre as Dia,h.Horario,Capacidad from Clase inner join Entrenamiento ent on ent.Id_Entrenamiento = Clase.Id_Entrenamiento inner join Entrenador e on e.Id_Entrenador = Clase.Id_Entrenador inner join Piso p on p.Id_Piso = Clase.Id_Piso inner join Horario h on h.Id_Clase = Clase.Id_Clase inner join Día d on d.Id_Dia = h.Id_Dia";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    tra.Add(
                     new Detalle_Clase()
                     {
                         Clase_Descripcion = reader["Descripcion"].ToString(),
                         Actividad = reader["Actividad"].ToString(),
                         Entrenador = reader["Entrenador"].ToString(),
                         Nivel_Piso = reader["Nivel_Piso"].ToString(),
                         Descripcion_Piso = reader["Descripcion_Piso"].ToString(),
                         Dia = reader["Dia"].ToString(),
                         Horario = reader["Horario"].ToString(),
                         capacidad = int.Parse(reader["Capacidad"].ToString()),
                     });
                }
                return tra;

            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();


            }
        }
        public List<Reserva_Socio> GetClasesDeSocioId(int id) // obtengo las clases incriptas a la que se inscribio el socio
        {

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            List<Reserva_Socio> tra = new List<Reserva_Socio>();

            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "Select c.Descripcion as Descripcion_Clase,e.Nombre as Actividad, p.Nivel,p.Descripcion as Descripcion_Piso,d.Nombre as Dia,Horario from Socio_Clase inner join Socio S on Socio_Clase.Id_Socio = S.Id_Socio inner join Piso p on p.Id_Piso = Socio_Clase.Id_Piso inner join Clase c on Socio_Clase.Id_Clase = c.Id_Clase inner join Entrenamiento e on c.Id_Entrenamiento = e.Id_Entrenamiento inner join Horario h on c.Id_Clase = h.Id_Clase inner join Día d on h.Id_Dia = d.Id_Dia where S.Id_Socio = @id";
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    tra.Add(
                     new Reserva_Socio()
                     {
                         Descripcion_Piso = reader["Descripcion_Piso"].ToString(),
                         Descripcion_Clase = reader["Descripcion_Clase"].ToString(),
                         Nivel = reader["Nivel"].ToString(),     
                         Actividad = reader["Actividad"].ToString(),
                         Dia = reader["Dia"].ToString(),
                         Horario = reader["Horario"].ToString(),

                     });
                }
                return tra;

            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }
        
        public List<Detalle_Clase> GetClaseDescription(string n) /*Detalles de Clase por una clase en especifico*/
        {

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            List<Detalle_Clase> tra = new List<Detalle_Clase>();

            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select Clase.Descripcion as Clase_Descripcion,ent.Nombre as Actividad,e.Nombre +' '+e.Apellido as Entrenador,p.Nivel as Nivel_Piso,p.Descripcion as Descripcion_Piso,d.Nombre as Dia,h.Horario,Capacidad from Clase inner join Entrenamiento ent on ent.Id_Entrenamiento = Clase.Id_Entrenamiento inner join Entrenador e on e.Id_Entrenador = Clase.Id_Entrenador inner join Piso p on p.Id_Piso = Clase.Id_Piso inner join Horario h on h.Id_Clase = Clase.Id_Clase inner join Día d on d.Id_Dia = h.Id_Dia where Clase.Descripcion =@n";
                cmd.Parameters.AddWithValue("@n", n);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    tra.Add(
                     new Detalle_Clase()
                     {
                         Clase_Descripcion = reader["Clase_Descripcion"].ToString(),
                         Actividad = reader["Actividad"].ToString(),
                         Entrenador = reader["Entrenador"].ToString(),
                         Nivel_Piso = reader["Nivel_Piso"].ToString(),
                         Descripcion_Piso = reader["Descripcion_Piso"].ToString(),
                         Dia = reader["Dia"].ToString(),
                         Horario = reader["Horario"].ToString(),
                         capacidad = int.Parse(reader["Capacidad"].ToString()),
                     });
                }
                return tra;

            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();


            }
        }
        public List<Lista_Clase> Listar_Clase()
        {
            List<Lista_Clase> ListaClases = new List<Lista_Clase>();
            DataTable tabla = AR.Leer("Listar_Clases", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Lista_Clase lc = new Lista_Clase();
                lc.Descripcion = registro["Descripcion"].ToString();
                lc.Descripcion_Piso = registro["Descripcion_Piso"].ToString();
                lc.Nivel = registro["Nivel"].ToString();
                lc.Capacidad = int.Parse(registro["Capacidad"].ToString());
                lc.Entrenador = registro["Entrenador"].ToString();
                lc.Actividad = registro["Actividad"].ToString();
                ListaClases.Add(lc);
            }

            return ListaClases;
        } //lista detallada de la clase incluyendo dias y horarios
        public List<Clase> Listar() // listo la clase tabla original
        {
            List<Clase> lc = new List<Clase>();
            DataTable tabla = AR.Leer("Listar_Clase", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Clase c = new Clase();
                c.Id_Clase = int.Parse(registro["Id_Clase"].ToString());
                c.Id_Entrenamiento = int.Parse(registro["Id_Entrenamiento"].ToString());
                c.Id_Entrenador = int.Parse(registro["Id_Entrenador"].ToString());
                c.Id_piso = int.Parse(registro["Id_Piso"].ToString());
                c.Descripcion = registro["Descripcion"].ToString();
                c.Capacidad = int.Parse(registro["Capacidad"].ToString());
                lc.Add(c);
            }
            return lc;
        }



    }
}
