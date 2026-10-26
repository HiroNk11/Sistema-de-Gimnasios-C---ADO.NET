using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaRepositorio
{
    public class EntrenamientoRepository
    {
        AccesoRepository AR = new AccesoRepository();
        public List<Entrenamiento> Listar()
        {
            List<Entrenamiento> ListaEntrenamiento = new List<Entrenamiento>();
            DataTable tabla = AR.Leer("Listar_Entrenamiento", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Entrenamiento ent = new Entrenamiento();
                ent.Id_Entrenamiento = int.Parse(registro["Id_Entrenamiento"].ToString());
                ent.Nombre = registro["Nombre"].ToString();
                ListaEntrenamiento.Add(ent);
            }
            return ListaEntrenamiento;
        }
        public string Insertar(Entrenamiento ent)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@nombre", ent.Nombre);
            mensaje = AR.Escribir("Insertar_Entrenamiento", parametros);
            return mensaje;
        }
        public string Borrar(Entrenamiento ent)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@eId", ent.Id_Entrenamiento);
            mensaje = AR.Escribir("Borrar_Entrenamiento", parametros);
            return mensaje;
        }

    public string Editar (Entrenamiento ent)
    {
        string mensaje = "";
        SqlParameter[] parametros = new SqlParameter[2];
        parametros[0] = new SqlParameter("@Nombre", ent.Nombre);
        parametros[1] = new SqlParameter("@Ide", ent.Id_Entrenamiento);
        mensaje = AR.Escribir("Editar_Entrenamiento", parametros);
        return mensaje;
    }
        public DataTable Buscar_Entrenamiento(Entrenamiento ent)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            SqlCommand cmd = new SqlCommand("Buscar_Entrenamiento", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", ent.Nombre);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }
        public List<Piso> ListarPiso()
        {
            List<Piso> ListaPiso = new List<Piso>();
            DataTable tabla = AR.Leer("Listar_Piso", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Piso p = new Piso();
                p.Id_Piso = int.Parse(registro["Id_Piso"].ToString());
                p.Nivel = registro["Nivel"].ToString();
                p.Descripcion = registro["Descripcion"].ToString();
                ListaPiso.Add(p);
            }
            return ListaPiso;
        }
    }
}