using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Reserva
    {
        public int idsocio { get; set; }
        public int idclase { get; set; }

        public DateTime fi { get; set; }
        public DateTime ff { get; set; }

        public decimal pc { get; set; }

        public decimal t { get; set; }
    }
}
