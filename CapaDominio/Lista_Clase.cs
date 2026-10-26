using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Lista_Clase
    {
        public string Descripcion { get; set; }
        public string Descripcion_Piso { get; set; }
        public string Nivel { get; set; }

        public int Capacidad { get; set; }
        public string Entrenador { get; set; }
        public string Actividad { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}