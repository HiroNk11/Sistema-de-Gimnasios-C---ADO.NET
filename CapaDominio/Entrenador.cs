using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{   public class Entrenador
    {
        private int id_entrenador;

        public int Id_Entrenador
        {
            get { return id_entrenador; }
            set { id_entrenador = value; }
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

        public override string ToString()
        {
            return Nombre;
        }
    }
}