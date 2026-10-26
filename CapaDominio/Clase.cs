using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Clase
    {
        private int id_clase;

        public int Id_Clase
        {
            get { return id_clase; }
            set { id_clase = value; }
        }

        private int id_entrenamiento;

        public int Id_Entrenamiento
        {
            get { return id_entrenamiento; }
            set { id_entrenamiento = value; }
        }

        private int id_entrenador;

        public int Id_Entrenador
        {
            get { return id_entrenador; }
            set { id_entrenador = value; }
        }

        private int id_piso;

        public int Id_piso
        {
            get { return id_piso; }
            set { id_piso = value; }
        }

        public string Descripcion { get; set; }

        private int capacidad;

        public int Capacidad
        {
            get { return capacidad; }
            set { capacidad = value; }
        }

        public override string ToString()
        {
            return Descripcion;
        }

    }
}