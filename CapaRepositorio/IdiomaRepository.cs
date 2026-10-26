using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaInterfaces.INTERFACES;
using CapaServicios;
using CapaServicios.MultiIdioma;
using CapaDominio;

namespace CapaRepositorio
{
    public class IdiomaRepository
    {
        AccesoRepository AR = new AccesoRepository();

        public IIdioma ObtenerIdiomaDefault()
        {
            return ObtenerIdiomas().Where(i => i.Default).FirstOrDefault();
        }
        public IList<IIdioma> ObtenerIdiomas()
        {

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;

            IList<IIdioma> _idiomas = new List<IIdioma>();
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select * from Idioma";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    _idiomas.Add(
                     new Idioma()
                     {
                         Id = int.Parse(reader["Id_Idioma"].ToString()),
                         Nombre = reader["Nombre"].ToString(),
                         Default = bool.Parse(reader["Idioma_Default"].ToString())

                     });
                }
                return _idiomas;

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
        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma = null)
        {
            //si no hay idioma definido, traigo el idioma por default
            if (idioma == null)
            {
                idioma = ObtenerIdiomaDefault();
            }

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            IDictionary<string, ITraduccion> _traducciones = new Dictionary<string, ITraduccion>();
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select t.Id_Idioma,t.Traduccion as traduccion_traduccion, e.Id_Etiqueta,e.Nombre as nombre_etiqueta from Traduccion t inner join Etiqueta e on t.Id_Etiqueta=e.Id_Etiqueta where t.Id_Idioma = @id_idioma";
                cmd.Parameters.AddWithValue("id_idioma", idioma.Id);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var etiqueta = reader["nombre_etiqueta"].ToString();
                    _traducciones.Add(etiqueta,
                     new Traduccion()
                     {

                         Texto = reader["traduccion_traduccion"].ToString(),

                         Etiqueta = new Etiqueta()
                         {
                             Id = int.Parse(reader["Id_Etiqueta"].ToString()),
                             Nombre = etiqueta
                         }

                     });
                }
                return _traducciones;

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

        public List<Etiquetas> ObtenerEtiquetasYTraduccion(string n)
        {

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            List<Etiquetas> tra = new List <Etiquetas>();
 
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select et.Nombre as Nombre_Etiqueta,Traduccion from Etiqueta et inner join Traduccion t on t.Id_Etiqueta = et.Id_Etiqueta inner join Idioma i on i.Id_Idioma = t.Id_Idioma where i.Nombre =@n";
                cmd.Parameters.AddWithValue("n", n);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    tra.Add(
                     new Etiquetas()
                     {
                        Nombre_Etiqueta = reader["Nombre_Etiqueta"].ToString(),
                         Traduccion = reader["Traduccion"].ToString()

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

        public List<Etiqueta> GetAllEtiquetas()
        {

            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            List<Etiqueta> tra = new List<Etiqueta>();

            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandText = "select * from Etiqueta";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    tra.Add(
                     new Etiqueta()
                     {
                         Id = int.Parse(reader["Id_Etiqueta"].ToString()),
                         Nombre = reader["Nombre"].ToString()

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
        public int ObtenerTraduccion(string nombre)
        {

            int id;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("ObtenerIdTraduccion", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                reader = cmd.ExecuteReader(); 
                if (!reader.Read()) return 0;     
                id = int.Parse(reader["Id_Etiqueta"].ToString());

                return id;

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
        public string EditarTraduccion(string t,string n, int id)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@Trad", t);
            parametros[1] = new SqlParameter("@Nombre", n);
            parametros[2] = new SqlParameter("@id", id);
            mensaje = AR.Escribir("Editar_Traduccion", parametros);
            return mensaje;
        }
        public string InsertarIdioma(Idioma id)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@nombre", id.Nombre);
            parametros[1] = new SqlParameter("@default", id.Default);
            mensaje = AR.Escribir("Insertar_Idioma", parametros);
            return mensaje;
        }

        public string InsertarE(int idI,int idE, string tra)
        {
            string m = "";
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@IdI", idI);
            parametros[1] = new SqlParameter("@idE", idE);
            parametros[2] = new SqlParameter("@T", tra);
            m = AR.Escribir("Insertar_Etiquetas", parametros);
            return m;
        }

    }
}