 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Entrenamiento
    {
        private int id_entrenamiento;

        public int Id_Entrenamiento
        {
            get { return id_entrenamiento; }
            set { id_entrenamiento = value; }
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