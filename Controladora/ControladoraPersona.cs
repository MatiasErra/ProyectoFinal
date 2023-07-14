using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Persistencia;

namespace Controladoras
{
    public class ControladoraPersona
    {
        private static ControladoraPersona _instancia;

        public static ControladoraPersona obtenerInstancia()
        {
            if(_instancia == null)
            {
                _instancia = new ControladoraPersona();
            }
            return _instancia;
        }

        public bool altaAdmin(Admin Admin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
    

            if (inst.altaAdmin(Admin))
            {
                return true;
            }
            else
                return false;
        }

        public List<Camionero> listarCamioneros()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();

            return inst.listarCamioneros();
        }

        public Camionero buscarCamionero(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            return inst.buscarCamionero(id);
        }

        public bool altaCamionero(Camionero Camionero)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.altaCamionero(Camionero))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bajaCamionero(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.bajaCamionero(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modificarCamionero(Camionero Camionero)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modificarCamionero(Camionero))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
