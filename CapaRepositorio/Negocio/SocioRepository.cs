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
    public class SocioRepository
    {
        AccesoRepository AR = new AccesoRepository();
        public List<Socio> Listar()
        {
            List<Socio> listasocios = new List<Socio>();
            DataTable tabla = AR.Leer("Listar_Socio", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Socio s = new Socio();
                s.Id_Socio = int.Parse(registro["Id_Socio"].ToString());
                s.Nombre = registro["Nombre"].ToString();
                s.Apellido = registro["Apellido"].ToString();
                s.Fecha = (DateTime)registro["Fecha_Nacimiento"];
                listasocios.Add(s);
            }
            return listasocios;
        }
        public List<Socio_Clase> listar_socioclase()
        {
            List<Socio_Clase> listasocioclase = new List<Socio_Clase>();
            DataTable tabla = AR.Leer("Listar_Sc", null);

            foreach (DataRow registro in tabla.Rows)
            {
                Socio_Clase s = new Socio_Clase();
                s.Id = int.Parse(registro["Id"].ToString());
                s.Id_Socio = int.Parse(registro["Id_Socio"].ToString());
                s.Id_piso = int.Parse(registro["Id_piso"].ToString());
                s.Id_Clase = int.Parse(registro["Id_Clase"].ToString());
                s.Precio = decimal.Parse(registro["Precio"].ToString());
                s.cantidad_dias = int.Parse(registro["Cant_Dias"].ToString());
                s.FechaInicio = Convert.ToDateTime(registro["Fecha_Inicio"].ToString());
                s.FechaFin = Convert.ToDateTime(registro["Fecha_Fin"].ToString());
                listasocioclase.Add(s);
            }
            return listasocioclase;
        }

        public string Insertar(Socio s)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@nombre", s.Nombre);
            parametros[1] = new SqlParameter("@apellido", s.Apellido);
            parametros[2] = new SqlParameter("@fecha", s.Fecha);
            mensaje = AR.Escribir("Insertar_Socio", parametros);
            return mensaje;
        }

        public string Insertar_Reserva(Reserva r)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@ids", r.idsocio);
            parametros[1] = new SqlParameter("@idc", r.idclase);
            parametros[2] = new SqlParameter("@fi", r.fi);
            parametros[3] = new SqlParameter("@ff", r.ff);
            parametros[4] = new SqlParameter("@pc", r.pc);
            parametros[5] = new SqlParameter("@t", r.t);
            mensaje = AR.Escribir("Insertar_Reserva", parametros);
            return mensaje;
        }

        public string Borrar(Socio s)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@sId", s.Id_Socio);
            mensaje = AR.Escribir("Borrar_Socio", parametros);
            return mensaje;
        }

        public string Borrar_socioclase(Socio_Clase s)
        {throw new InvalidOperationException("La eliminación masiva por fecha fue deshabilitada. Use las cancelaciones de Operación diaria.");}
        public string Editar(Socio s)
        {
            string mensaje = "";
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@Nombre", s.Nombre);
            parametros[1] = new SqlParameter("@Apellido", s.Apellido);
            parametros[2] = new SqlParameter("@Fecha_Nacimiento", s.Fecha);
            parametros[3] = new SqlParameter("@id", s.Id_Socio);
            mensaje = AR.Escribir("Editar_Socio", parametros);
            return mensaje;
        }

        public DataTable Buscar_Socio(string nombre)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            SqlCommand cmd = new SqlCommand("Buscar_Socio",sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre",nombre);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }
        public string Insertar_Socio_Clase(Socio_Clase sc)
        {
            using(var cn=new SqlConnection(AR.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    using(var cmd=new SqlCommand(@"UPDATE dbo.Clase WITH(UPDLOCK,HOLDLOCK) SET Capacidad=Capacidad-1 WHERE Id_Clase=@c AND Id_Piso=@p AND Capacidad>0",cn,tx))
                    {
                        cmd.Parameters.AddWithValue("@c",sc.Id_Clase);cmd.Parameters.AddWithValue("@p",sc.Id_piso);
                        if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("La clase ya no tiene cupo o no existe. Actualice la lista.");
                    }
                    using(var cmd=new SqlCommand(@"IF EXISTS(SELECT 1 FROM dbo.Socio_Clase WITH(UPDLOCK,HOLDLOCK) WHERE Id_Clase=@c AND Id_Socio=@s AND Fecha_Inicio<@ff AND Fecha_Fin>@fi)
THROW 51001,'El socio ya tiene una inscripción vigente para esta clase.',1;
INSERT dbo.Socio_Clase(Id_Socio,Id_Piso,Id_Clase,Precio,Cant_Dias,Fecha_Inicio,Fecha_Fin) VALUES(@s,@p,@c,@precio,@dias,@fi,@ff);",cn,tx))
                    {
                        cmd.Parameters.AddWithValue("@c",sc.Id_Clase);cmd.Parameters.AddWithValue("@s",sc.Id_Socio);cmd.Parameters.AddWithValue("@p",sc.Id_piso);
                        var precio=cmd.Parameters.Add("@precio",SqlDbType.Decimal);precio.Precision=18;precio.Scale=2;precio.Value=sc.Precio;
                        cmd.Parameters.AddWithValue("@dias",sc.cantidad_dias);cmd.Parameters.AddWithValue("@fi",sc.FechaInicio);cmd.Parameters.AddWithValue("@ff",sc.FechaFin);
                        try {cmd.ExecuteNonQuery();}catch(SqlException e){if(e.Number==51001)throw new InvalidOperationException("El socio ya está inscripto en esa clase.");throw;}
                    }
                    tx.Commit();return "Inscripción registrada correctamente.";
                }
            }
        }

        public int GetIdPisoByDescription(string d)
        {
            int id = 0;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Search_idPiso", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@d", d);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return 0;
                id = int.Parse(reader["Id_Piso"].ToString());
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

        public DataTable Listar_HistorialCambios()
        {
            DataTable tabla = AR.Leer("Listar_HistorialCambios", null);
            return tabla;
        }

        public void LimpiarHistorial()
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string consulta = "delete from Bitacora_Socio";
            SqlCommand cmd = new SqlCommand(consulta, sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public DataTable ListarSociosIncriptos(string c)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string consulta = "select s.Nombre,s.Apellido from Socio_Clase inner join Socio s  on Socio_Clase.Id_Socio = s.Id_Socio where Id_Clase in(select Id_Clase from Clase where Descripcion = @c)";
            SqlCommand cmd = new SqlCommand(consulta, sql);
            cmd.Parameters.AddWithValue("@c", c);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
         }
        public DataTable ListarSociosNOIncriptos(int idclase)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = AR.GetConnectionString();
            string consulta = "select Nombre,Apellido from Socio where Id_Socio in (select Id_Socio from Socio except select Id_Socio from Socio_Clase where Id_Clase=@idclase)";
            //select Nombre, Apellido from Socio where Id_Socio in (select Id_Socio from Socio except select Id_Socio from Socio_Clase)
            SqlCommand cmd = new SqlCommand(consulta, sql);
            cmd.Parameters.AddWithValue("@idclase", idclase);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            return dt;
        }

        public void CrearXML(string txt)
        {
            DataSet DS = new DataSet();
            SqlConnection CN = new SqlConnection();
            CN.ConnectionString = AR.GetConnectionString(); 
            CN.Open();
            SqlDataAdapter DA = new SqlDataAdapter("Select * from Socio", CN);
            DA.Fill(DS, "Socio");
            CN.Close();
            DS.WriteXml(txt);
        }
    }
    }

