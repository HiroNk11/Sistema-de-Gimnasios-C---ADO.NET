using CapaInterfaces.INTERFACES;
using CapaServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios
{
    public class Sesion
    {
       
        // Instancia de objeto de Clase Usuario de la capa BE
        static IUsuario _user;
        static IList<IIdiomaObserver> _observers = new List<IIdiomaObserver>();
        public IUsuario Usuario
        {
            get { return _user; }
        }

    
        // metodos de LogIn LogOut
        public void LogIn(IUsuario usuario)
        {
            _user = usuario;
        }
        public void Logout()
        {
            _user = null;
        }
        public bool IsLogged()
        {
            return _user != null;
        }

        // Lenguaje

        public  void SuscribirObservador(IIdiomaObserver o)
        {
            _observers.Add(o);
        }
        public void DesuscribirObservador(IIdiomaObserver o)
        {
            _observers.Remove(o);
        }

        private void Notificar(IIdioma idioma)
        {
            foreach (var o in _observers)
            {
                o.UpdateLanguage(idioma);
            }
        }
        public  void CambiarIdioma(IIdioma idioma)
        {
            if (_user != null)
            {
                _user.Idioma = idioma;
                Notificar(idioma);
            }
        }

      
    }
}
