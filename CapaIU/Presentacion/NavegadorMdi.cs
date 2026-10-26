using System;
using System.Linq;
using System.Windows.Forms;
namespace CapaIU.Presentacion
{
    // Un único punto de apertura: reutiliza ventanas y permite reemplazar sus fábricas.
    internal sealed class NavegadorMdi
    {
        readonly Form principal;
        public NavegadorMdi(Form principal) { this.principal = principal ?? throw new ArgumentNullException(nameof(principal)); }
        public T Abrir<T>(Func<T> crear) where T : Form
        {
            var ventana = principal.MdiChildren.OfType<T>().FirstOrDefault(x => !x.IsDisposed);
            if (ventana == null) { ventana = crear(); ventana.MdiParent = principal; ventana.Show(); }
            ventana.WindowState = FormWindowState.Maximized;
            ventana.Activate();
            return ventana;
        }
    }
}
