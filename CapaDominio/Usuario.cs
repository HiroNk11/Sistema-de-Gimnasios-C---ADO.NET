using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaInterfaces.INTERFACES;


namespace CapaDominio
{
    public class Usuario : IUsuario
    {

        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }

        public IIdioma Idioma { get; set; }
        public string dvh { get; set; }
        public override string ToString()
        {
            return usuario;
        }

   

    }
}
