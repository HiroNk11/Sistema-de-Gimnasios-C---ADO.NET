using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Reserva_Socio
    {

        public string Descripcion_Clase
        {
            get { return descripcion_clase; }
            set { descripcion_clase = value; }
        }

        private string actividad;

        public string Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }

        private string nivel;

        public string  Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        private string descripcion_piso;

        public string Descripcion_Piso
        {
            get { return descripcion_piso; }
            set { descripcion_piso = value; }
        }
        private string descripcion_clase;

     

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


    }
}