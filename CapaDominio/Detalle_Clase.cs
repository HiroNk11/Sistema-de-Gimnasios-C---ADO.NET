using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Detalle_Clase
    {
        private string clase_descripcion;

        public string Clase_Descripcion
        {
            get { return clase_descripcion; }
            set { clase_descripcion = value; }
        }

        private string actividad;

        public string Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }

        private string entrenador;

        public string Entrenador
        {
            get { return entrenador; }
            set { entrenador = value; }
        }

        private string nivel_piso;

        public string Nivel_Piso
        {
            get { return nivel_piso; }
            set { nivel_piso = value; }
        }

        private string descripcion_oiso;

        public string Descripcion_Piso
        {
            get { return descripcion_oiso; }
            set { descripcion_oiso = value; }
        }

        private string dia;

        public string Dia
        {
            get { return dia; }
            set { dia = value; }
        }

        private string horario;

        public string Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public int capacidad { get; set; }

        public override string ToString()
        {
            return Clase_Descripcion;
        }
    }
}