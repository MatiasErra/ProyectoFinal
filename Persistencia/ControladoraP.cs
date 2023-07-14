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

<<<<<<< HEAD
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


=======
        public List<Camionero> listarCamioneros()
        {
            return new pCamionero().listarCamioneros();
        }

        public Camionero buscarCamionero(int id)
        {
            return new pCamionero().buscarCamionero(id);
        }

>>>>>>> c26bc18d9d525507cb46910ad5c5d4457465ebf4
        public bool altaCamionero(Camionero camionero)
        {
            return new pCamionero().altaCamionero(camionero);
        }

        public bool bajaCamionero(int id)
        {
            return new pCamionero().bajaCamionero(id);
        }

        public bool modificarCamionero(Camionero camionero)
        {
            return new pCamionero().modificarCamionero(camionero);
        }
    }
}
