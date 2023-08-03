using Clases;
using System.Collections.Generic;

namespace Persistencia
{
    public class ControladoraP
    {

        #region Instancia

        private static ControladoraP _instancia;

        public static ControladoraP obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraP();
            }
            return _instancia;
        }

        #endregion

        #region Personas

        public List<Persona> lstIdPersonas()
        {
            return new pAdmin().lstIdPersonas();
        }


        #region Admins

        public List<string> userRepetidoAdm()
        {
            return new pAdmin().userRepetidoAdm();
        }

        public int iniciarSesionAdm(string user, string pass)
        {
            return new pAdmin().iniciarSesionAdm(user, pass);
        }

        public List<Admin> lstAdmin()
        {
            return new pAdmin().lstAdmin();
        }
        public List<Admin> buscarVarAdmin(string var)
        {
            return new pAdmin().buscarVarAdmin(var);
        }


        public Admin buscarAdm(int id)
        {
            return new pAdmin().buscarAdm(id);
        }

        public bool altaAdmin(Admin admin)
        {
            return new pAdmin().altaAdmin(admin);
        }

        public bool bajaAdmin(int id)
        {
            return new pAdmin().bajaAdmin(id);
        }

        public bool modificarAdm(Admin admin)
        {
            return new pAdmin().modificarAdm(admin);
        }

        #endregion

        #region Clientes



        public int iniciarSesionCli(string user, string pass)
        {
            return new pCliente().iniciarSesionCli(user, pass);
        }
        public List<string> userRepetidoCli()
        {
            return new pCliente().userRepetidoCli();
        }


        public List<Cliente> lstCliente()
        {
            return new pCliente().lstCli();
        }
        public List<Cliente> buscarVarCli(string var)
        {
            return new pCliente().buscarVarCli(var);
        }


    

        public Cliente buscarCli(int id)
        {
            return new pCliente().buscarCli(id);
        }
       
        public bool altaCli(Cliente cli)
        {
            return new pCliente().altaCli(cli);
        }

        public bool bajaCli(int id)
        {
            return new pCliente().bajaCli(id);
        }

        public bool modificarCli(Cliente cli)
        {
            return new pCliente().modificarCli(cli);
        }




        #endregion

        #region Camioneros

        public List<Camionero> listCamionero()
        {
            return new pCamionero().listCamionero();
        }

        public List<Camionero> buscarVarCamionero(string var)
        {
            return new pCamionero().buscarVarCamionero(var);
        }

        public Camionero buscarCamionero(int id)
        {
            return new pCamionero().buscarCamionero(id);
        }


        public bool altaCamionero(Camionero camionero)
        {
            return new pCamionero().altaCamionero(camionero);
        }

        public bool bajaCamionero(int id)
        {
            return new pCamionero().bajaCamionero(id);
        }

        public bool modCamionero(Camionero camionero)
        {
            return new pCamionero().modCamionero(camionero);
        }

        #endregion

        #endregion

    }
}
