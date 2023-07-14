using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clases;
using Controladoras;

namespace Web.Paginas
{
    public class ControladoraWeb
    {
        private static Controladora _controladora;
        private static ControladoraWeb _instancia;

        public static Controladora Controladora { get => _controladora; set => _controladora = value; }

        public static ControladoraWeb obtenerInstancia()
        {
            if(_instancia == null)
            {
                _instancia = new ControladoraWeb();
                _controladora = Controladora.obtenerInstancia();

            }
            return _instancia;
        }

        

        public bool altaAdmin(Admin admin)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();

            if (inst.altaAdmin(admin))
            {
                return true;
            }
            else
                return false;
        }

<<<<<<< HEAD
        public bool bajaAdmin(int id)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();

            if (ins.bajaAdmin(id))
            {
                return true;
            }
            else
                return false;
        }

        public Admin buscarAdm(int id)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();

                Admin admin = ins.buscarAdm(id);
                return admin;
            
        }

        public List<Admin> lstAdmin() 
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List <Admin> lst = ins.lstAdmin();
            return lst;
        
        }


















=======
        public List<Camionero> listarCamioneros()
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            return inst.listarCamioneros();
        }

        public Camionero buscarCamionero(int id)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            return inst.buscarCamionero(id);
        }

>>>>>>> c26bc18d9d525507cb46910ad5c5d4457465ebf4
        public bool altaCamionero(Camionero camionero)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            if (inst.altaCamionero(camionero))
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
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            if (inst.bajaCamionero(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modificarCamionero(Camionero camionero)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            if (inst.modificarCamionero(camionero))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*public bool altaCamion(Camion camion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.altaCamion(camion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

    }
}