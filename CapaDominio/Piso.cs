using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDominio
{
    public class Piso
    {
        private int id_piso;

        public int Id_Piso
        {
            get { return id_piso; }
            set { id_piso = value; }
        }

        private string nivel;

        public string Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public override string ToString()
        {
            return Nivel;
        }

    }
}