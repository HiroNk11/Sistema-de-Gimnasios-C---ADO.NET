using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaInterfaces.INTERFACES;
namespace CapaServicios.Composite
{
    public abstract class Componente
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
        public TipoPermiso Permiso { get; set; }
        public abstract IList<Componente> Hijos { get; }
        public abstract void AgregarHijo(Componente c);
        public abstract void VaciarHijos();

        public override string ToString()
        {
            return Nombre;
        }

    }
}
