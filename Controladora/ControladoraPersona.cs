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

   

        public List<Admin> buscarAdminFiltro(Admin adminBuscar,  string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Admin> lst = inst.buscarAdminFiltro(adminBuscar, ordenar);
            return lst;
        }



        public Admin buscarAdm(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();

            Admin admin = inst.buscarAdm(id);
            return admin;

        }

        public bool altaAdmin(Admin Admin, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.altaAdmin(Admin, idAdmin))
            {
                return true;
            }
            else
                return false;
        }


        public bool bajaAdmin(int id, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.bajaAdmin(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modificarAdm(Admin admin, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modificarAdm(admin, idAdmin))
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



        public List<Cliente> buscarCliFiltro(Cliente cliente, string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Cliente> lst = inst.buscarCliFiltro(cliente, ordenar);
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


        public bool bajaCli(int id, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();


            if (inst.bajaCli(id, idAdmin))
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

       
        public List<Camionero> buscarCamioneroFiltro(Camionero pCamionero, string fchVencDesde, string fchVencHasta, string ordenar)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Camionero> lst = inst.buscarCamioneroFiltro( pCamionero, fchVencDesde, fchVencHasta, ordenar);
            return lst;
        }

        public Camionero buscarCamionero(int id)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            return inst.buscarCamionero(id);
        }

        public bool altaCamionero(Camionero Camionero, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.altaCamionero(Camionero, idAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bajaCamionero(int id, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.bajaCamionero(id, idAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modCamionero(Camionero Camionero, int idAdmin)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            if (inst.modCamionero(Camionero, idAdmin))
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
