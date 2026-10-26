using CapaDominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaRepositorio
{
    public class EntrenadorRepository
    {
        AccesoRepository AR = new AccesoRepository();
        public List<Entrenador> Listar()
        {
            List<Entrenador> ListaEntrenamiento = new List<Entrenador>();
            DataTable tabla = AR.Leer("Listar_Entrenador", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Entrenador ent = new Entrenador();
                ent.Id_Entrenador = int.Parse(registro["Id_Entrenador"].ToString());
                ent.Nombre = registro["Nombre"].ToString();
                ent.Apellido = registro["Apellido"].ToString();
                ListaEntrenamiento.Add(ent);
            }
            return ListaEntrenamiento;
        }
        public string Insertar(Entrenador ent)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@nombre", ent.Nombre);
            parametros[1] = new SqlParameter("@apellido", ent.Apellido);
            mensaje = AR.Escribir("Insertar_Entrenador", parametros);
            return mensaje;
        }

        public string Borrar(Entrenador e)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@eId", e.Id_Entrenador);
            mensaje = AR.Escribir("Borrar_Entrenador", parametros);
            return mensaje;
        }
        public string Editar(Entrenador e)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@nombre", e.Nombre);
            parametros[1] = new SqlParameter("@apellido", e.Apellido);
            parametros[2] = new SqlParameter("@eId", e.Id_Entrenador);
            mensaje = AR.Escribir("Editar_Entrenador", parametros);
            return mensaje;
        }

        public DataTable Buscar_Entrenador(Entrenador e)
        {
            return AR.Leer("Buscar_Entrenador", new[] { new SqlParameter("@nombre", e.Nombre) });
        }
    }
}