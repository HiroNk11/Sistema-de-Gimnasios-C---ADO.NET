using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaServicios.Composite;
using CapaDominio;


namespace CapaRepositorio
{
    public class PermisosRepository
    {
        AccesoRepository Ar = new AccesoRepository();


        public IList<Patente> GetAllPatentes()
        {

            var cnn = new SqlConnection(Ar.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from permiso p where p.permiso is not null and Nombre not in('Baja Usuario');";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Patente>();

            while (reader.Read())
            {

                var id = reader.GetInt32(reader.GetOrdinal("Id_Permiso"));
                var nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                var permiso = reader.GetString(reader.GetOrdinal("Permiso"));


                Patente c = new Patente();
                c.Id = id;
                c.Nombre = nombre;
                c.Permiso = (TipoPermiso)Enum.Parse(typeof(TipoPermiso), permiso);
                lista.Add(c);

            }
            reader.Close();
            cnn.Close();


            return lista;
        }

        public IList<Familia> GetAllFamilias()
        {
            var cnn = new SqlConnection(Ar.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from permiso p where p.permiso is  null;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Familia>();

            while (reader.Read())
            {

                var id = reader.GetInt32(reader.GetOrdinal("Id_Permiso"));
                var nombre = reader.GetString(reader.GetOrdinal("Nombre"));

                Familia c = new Familia();
                c.Id = id;
                c.Nombre = nombre;
                lista.Add(c);
            }
            reader.Close();
            cnn.Close();
            return lista;
        }

        public Componente GuardarComponente(Componente p, bool esfamilia)
        {
            try
            {
                var cnn = new SqlConnection(Ar.GetConnectionString());
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;
                var sql = $@"insert into Permiso (Nombre,Permiso) values (@nombre,@permiso);  SELECT Id_Permiso AS LastId_permiso FROM Permiso WHERE Id_Permiso = @@Identity;       ";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));


                if (esfamilia)
                    cmd.Parameters.Add(new SqlParameter("permiso", DBNull.Value));

                else
                    cmd.Parameters.Add(new SqlParameter("permiso", p.Permiso.ToString()));

                var id = cmd.ExecuteScalar();
                p.Id = (int)id;
                return p;
            }
            catch (Exception) { throw; }
        }
        public Array GetAllPermission()
        {
            return Enum.GetValues(typeof(TipoPermiso));
        }

        public IList<Componente> GetAll(string familia)
        {
            var where = "is NULL";

            if (!String.IsNullOrEmpty(familia))
            {
                where = familia;
            }

         
            var cnn = new SqlConnection(Ar.GetConnectionString());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"with recursivo as (
                        select sp2.id_permiso_padre, sp2.id_permiso_hijo  from Permiso_Permiso SP2
                        where sp2.id_permiso_padre {where} --acá se va variando la familia que busco
                        UNION ALL 
                        select sp.id_permiso_padre, sp.id_permiso_hijo from Permiso_Permiso sp 
                        inner join recursivo r on r.id_permiso_hijo= sp.id_permiso_padre
                        )
                        select r.id_permiso_padre,r.id_permiso_hijo,p.Id_Permiso,p.Nombre, p.Permiso
                        from recursivo r 
                        inner join Permiso p on r.id_permiso_hijo = p.Id_Permiso
						
                        ";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Componente>();

            while (reader.Read())
            {
                int id_padre = 0;
                if (reader["id_permiso_padre"] != DBNull.Value)
                {
                    id_padre = reader.GetInt32(reader.GetOrdinal("id_permiso_padre"));
                }

                var id = reader.GetInt32(reader.GetOrdinal("Id_Permiso"));
                var nombre = reader.GetString(reader.GetOrdinal("Nombre"));

                var permiso = string.Empty;
                if (reader["Permiso"] != DBNull.Value)
                    permiso = reader.GetString(reader.GetOrdinal("Permiso"));


                Componente c;

                if (string.IsNullOrEmpty(permiso))//usamos este campo para identificar. Solo las patentes van a tener un permiso del sistema relacionado
                    c = new Familia();

                else
                    c = new Patente();

                c.Id = id;
                c.Nombre = nombre;
                if (!string.IsNullOrEmpty(permiso))
                    c.Permiso = (TipoPermiso)Enum.Parse(typeof(TipoPermiso), permiso);

                var padre = GetComponent(id_padre, lista);

                if (padre == null)
                {
                    lista.Add(c);
                }
                else
                {
                    padre.AgregarHijo(c);
                }



            }
            reader.Close();
            cnn.Close();


            return lista;
        }

        private Componente GetComponent(int id, IList<Componente> lista)
        {

            Componente component = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (component == null && lista != null)
            {
                foreach (var c in lista)
                {
                    var l = GetComponent(id, c.Hijos);
                    if (l != null && l.Id == id) return l;
                    else
                    if (l != null)
                        return GetComponent(id, l.Hijos);
                }
            }
            return component;
        }

        public void GuardarFamilia(Familia c)
        {
            if(c.Hijos.Any(x=>x.Id==c.Id))throw new ArgumentException("Una familia no puede incluirse a sí misma.");
            using(var cn=new SqlConnection(Ar.GetConnectionString()))
            {
                cn.Open();using(var tx=cn.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    using(var cmd=new SqlCommand("DELETE Permiso_Permiso WHERE id_permiso_padre=@id",cn,tx))
                    {cmd.Parameters.AddWithValue("@id",c.Id);cmd.ExecuteNonQuery();}
                    foreach(var item in c.Hijos)
                    using(var cmd=new SqlCommand(@";WITH Descendientes AS(SELECT id_permiso_hijo AS Id FROM Permiso_Permiso WHERE id_permiso_padre=@h UNION ALL SELECT p.id_permiso_hijo FROM Permiso_Permiso p JOIN Descendientes d ON p.id_permiso_padre=d.Id)
SELECT COUNT(*) FROM Descendientes WHERE Id=@p OPTION(MAXRECURSION 100)",cn,tx))
                    {
                        cmd.Parameters.AddWithValue("@p",c.Id);cmd.Parameters.AddWithValue("@h",item.Id);
                        if(Convert.ToInt32(cmd.ExecuteScalar())>0)throw new ArgumentException("La relación produciría un ciclo de permisos.");
                        cmd.CommandText="INSERT Permiso_Permiso(id_permiso_padre,id_permiso_hijo) VALUES(@p,@h)";cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void FillUserComponents(PermisoUsuario u)
        {
            var cnn = new SqlConnection(Ar.GetConnectionString());
            cnn.Open();
            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from Usuario_Permiso up inner join Permiso p on up.Id_Permiso=p.Id_Permiso where Id_Usuario=@id";
            cmd2.Parameters.AddWithValue("@id", u.idusuario);
            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())                                                                                                       
            {
                
                var idp = reader.GetInt32(reader.GetOrdinal("Id_Permiso"));
                var nombrep = reader.GetString(reader.GetOrdinal("Nombre"));
                var permisop = String.Empty;
                if (reader["Permiso"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("Permiso"));

                Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (TipoPermiso)Enum.Parse(typeof(TipoPermiso), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            reader.Close();
        }

        public bool BuscarPermisos(TipoPermiso permiso, PermisoUsuario u)
        {
            using(var cn=new SqlConnection(Ar.GetConnectionString()))
            using(var cmd=new SqlCommand(@";WITH Arbol AS (
SELECT p.Id_Permiso,p.Permiso,CAST('/'+CAST(p.Id_Permiso AS varchar(20))+'/' AS varchar(max)) AS Ruta
FROM Usuario_Permiso up JOIN Permiso p ON p.Id_Permiso=up.Id_Permiso WHERE up.Id_Usuario=@u
UNION ALL
SELECT hijo.Id_Permiso,hijo.Permiso,CAST(a.Ruta+CAST(hijo.Id_Permiso AS varchar(20))+'/' AS varchar(max))
FROM Arbol a JOIN Permiso_Permiso rel ON rel.id_permiso_padre=a.Id_Permiso JOIN Permiso hijo ON hijo.Id_Permiso=rel.id_permiso_hijo
WHERE a.Ruta NOT LIKE '%/'+CAST(hijo.Id_Permiso AS varchar(20))+'/%')
SELECT COUNT(*) FROM Arbol WHERE Permiso=@p OPTION(MAXRECURSION 100)",cn))
            {
                cmd.Parameters.AddWithValue("@u",u.idusuario);cmd.Parameters.AddWithValue("@p",permiso.ToString());cn.Open();return Convert.ToInt32(cmd.ExecuteScalar())>0;
            }
        }

        bool isInRole(Componente c, TipoPermiso permiso, bool existe)
        {

            if (c.Permiso.Equals(permiso))
                existe = true;
            else
            {
                foreach (var item in c.Hijos)
                {
                    existe = isInRole(item, permiso, existe);
                    if (existe) return true;
                }

            }

            return existe;
        }

        public void ValidarPermisos(PermisoUsuario u)
        {
            var cnn = new SqlConnection(Ar.GetConnectionString());
            cnn.Open();
            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from Usuario_Permiso up inner join Permiso p on up.Id_Permiso=p.Id_Permiso where Id_Usuario=@id";
            cmd2.Parameters.AddWithValue("@id", u.idusuario);
            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())
            {
                var nombrep = reader.GetString(reader.GetOrdinal("Nombre"));
                var idp = reader.GetInt32(reader.GetOrdinal("Id_Permiso"));
                var permisop = String.Empty;
                if (reader["Permiso"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("Permiso"));

                Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (TipoPermiso)Enum.Parse(typeof(TipoPermiso), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            reader.Close();
        }
        public void FillFamilyComponents(Familia familia)
        {
            familia.VaciarHijos();
            foreach (var item in GetAll("=" + familia.Id))
            {
                familia.AgregarHijo(item);
            }
        }

    }
}
