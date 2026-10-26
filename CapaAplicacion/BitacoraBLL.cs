using CapaDominio;
using CapaRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace CapaAplicacion
{
    public class BitacoraBLL
    {
        BitacoraRepository mpbitacora = new BitacoraRepository();
        public string InsertarEvento(Bitacora bit)
        {
            return mpbitacora.InsertarEvento(bit);
        }

        public List<Detalle_Bitacora> Listar_Bitacora()
        {
            return mpbitacora.Listar_Bitacora();
        }

        public List<Detalle_Bitacora> GetBitacoraUser(string user)
        {
            return mpbitacora.GetBitacoraUser(user);
        }

        public List<Detalle_Bitacora> GetBitacoraUserByDate(string f1, string f2)
        {
            return mpbitacora.GetBitacoraUserByDate(f1, f2);
        }

        public List<Detalle_Bitacora> GetBitacoraUserByFilter(string u, string f1, string f2, string c)
        {
            return mpbitacora.GetBitacoraUserByFilter(u,f1,f2 ,c);
        }
        public DataTable FiltrarCriticidad(string c)
        {
            DataTable dt = mpbitacora.FiltrarCriticidad(c);
            return dt;
        }
    }
}
