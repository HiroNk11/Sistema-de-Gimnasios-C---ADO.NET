using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios.DV
{
    public class DV
    {
        public static string GenerarDVH(UsuDv usu)
        {

            Type t = usu.GetType();
            string dvh = string.Empty;
            var props = t.GetProperties();
            var attrs = t.GetCustomAttributes(false);
            var verificable = (VerificableAttribute)attrs.Where(i => i.GetType().Equals(typeof(VerificableAttribute))).FirstOrDefault();

            if (verificable != null)
                dvh += $"{verificable.Prefix}_";

            foreach (var item in props)
            {

                if (item.PropertyType.FullName.Equals(typeof(DateTime).FullName))
                {
                    DateTime a = (DateTime)item.GetValue(usu);
                    dvh += a.ToString("ddmmyyyyhhmmss");
                }
                else
                {
                    dvh += item.GetValue(usu).ToString();

                }
            }

            return Encriptador.GetMd5Hash(dvh);
        }

    }
}
