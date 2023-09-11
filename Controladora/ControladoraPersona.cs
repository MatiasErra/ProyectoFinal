using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Clases;
using Persistencia;

namespace Controladoras
{
    public class ControladoraPersona
    {

        #region Instancia

        private static ControladoraPersona _instancia;

        public static ControladoraPersona obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraPersona();
            }
            return _instancia;
        }

        #endregion

        #region Personas


        public List<Persona> lstIdPersonas()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Persona> lst = inst.lstIdPersonas();
            return lst;

        }

        public List<string> userRepetidoAdm()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<string> lst = inst.userRepetidoAdm();
            return lst;
        }


     
        #region Admins

        public int iniciarSesionAdm(string user, string pass)
        {
            ControladoraP ins = ControladoraP.obtenerInstancia();
            int id = ins.iniciarSesionAdm(user, pass);
            return id;
        }

   

        public List<Admin> buscarAdminFiltro(Admin adminBuscar, string fchDesde, string fchHasta, string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Admin> lst = inst.buscarAdminFiltro(adminBuscar, fchDesde, fchHasta, ordenar);
            return lst;
        }



        public Admin buscarAdm(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();

            Admin admin = inst.buscarAdm(id);
            return admin;

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


        public bool bajaAdmin(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.bajaAdmin(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modificarAdm(Admin admin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modificarAdm(admin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Clientes



        public List<string> userRepetidoCli()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<string> lst = inst.userRepetidoCli();
            return lst;
        }






        public int iniciarSesionCli(string user, string pass)
        {
            ControladoraP ins = ControladoraP.obtenerInstancia();
            int id = ins.iniciarSesionCli(user, pass);
            return id;
        }



        public List<Cliente> buscarCliFiltro(Cliente cliente, string fchDesde, string fchHasta, string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Cliente> lst = inst.buscarCliFiltro(cliente,fchDesde,fchHasta, ordenar);
            return lst;
        }



        public Cliente buscarCli(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();

            Cliente cli = inst.buscarCli(id);
            return cli;

        }

        public bool altaCli(Cliente cli)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.altaCli(cli))
            {
                return true;
            }
            else
                return false;
        }


        public bool bajaCli(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.bajaCli(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modificarCli(Cliente cli)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modificarCli(cli))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Camioneros

       
        public List<Camionero> buscarCamioneroFiltro(Camionero pCamionero, string fchNacDesde, string fchNacHasta, string fchVencDesde, string fchVencHasta, string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Camionero> lst = inst.buscarCamioneroFiltro( pCamionero, fchNacDesde, fchNacHasta, fchVencDesde, fchVencHasta, ordenar);
            return lst;
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

        public bool modCamionero(Camionero Camionero)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modCamionero(Camionero))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion

    }
}
