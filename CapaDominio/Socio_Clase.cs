using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Socio_Clase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int id_socio;

        public int Id_Socio
        {
            get { return id_socio; }
            set { id_socio = value; }
        }

        private int id_piso;

        public int Id_piso
        {
            get { return id_piso; }
            set { id_piso = value; }
        }


        private int id_clase;

        public int Id_Clase
        {
            get { return id_clase; }
            set { id_clase = value; }
        }

        private decimal precio;

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public int cantidad_dias { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}