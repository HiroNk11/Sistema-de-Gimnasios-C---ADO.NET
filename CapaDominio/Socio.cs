using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Socio
    {
        private int id_socio;

        public int Id_Socio
        {
            get { return id_socio; }
            set { id_socio = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public DateTime Fecha { get; set; }
        public string NombreCompleto { get { return Nombre + " " + Apellido + " (#" + Id_Socio + ")"; } }
    }
}