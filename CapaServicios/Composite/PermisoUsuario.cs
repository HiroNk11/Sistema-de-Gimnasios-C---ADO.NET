using CapaInterfaces.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios.Composite
{
    public class PermisoUsuario 
    {
        public PermisoUsuario()
        {
            _permisos = new List<Componente>();
        }

        public int idusuario { get; set; }
        public string Nombre { get; set; }


        List<Componente> _permisos;
        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
