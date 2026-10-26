using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Día
    {
        private int id_dia;

        public int Id_Dia
        {
            get { return id_dia; }
            set { id_dia = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}