using Clases;
using System.Collections.Generic;

namespace Persistencia
{
    public class ControladoraP
    {

        private static ControladoraP _instancia;
        
        public static ControladoraP obtenerInstancia()
        {
            if( _instancia == null )
            {
                _instancia = new ControladoraP();
            }
            return _instancia;
        }

        public bool altaAdmin(Admin admin)
        {
            return new pAdmin().altaAdmin(admin);
        }

        public bool bajaAdmin(int id)
        {
            return new pAdmin().bajaAdmin(id);
        }

        public Admin buscarAdm(int id)
        {
            return new pAdmin().buscarAdm(id);
        }

        public List<Admin> lstAdmin() 
        {
            return new pAdmin().lstAdmin();
        }


        public bool altaCamionero(Camionero camionero)
        {
            return new pCamionero().altaCamionero(camionero);
        }
    }
}
