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


        public List<Admin> buscarAdminFiltro(Admin adminBuscar, string fchDesde, string fchHasta, string ordenar)
        {
            return new pAdmin().buscarAdminFiltro(adminBuscar, fchDesde, fchHasta, ordenar);
        }


        public Admin buscarAdm(int id)
        {
            return new pAdmin().buscarAdm(id);
        }

        public bool altaAdmin(Admin admin, int idAdmin)
        {
            return new pAdmin().altaAdmin(admin, idAdmin);
        }

        public bool bajaAdmin(int id, int idAdmin)
        {
            return new pAdmin().bajaAdmin(id, idAdmin);
        }

        public bool modificarAdm(Admin admin, int idAdmin)
        {
            return new pAdmin().modificarAdm(admin, idAdmin);
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


        
        public List<Cliente> buscarCliFiltro(Cliente cliente, string fchDesde, string fchHasta, string ordenar)
        {
            return new pCliente().buscarCliFiltro(cliente, fchDesde, fchHasta, ordenar);
        }


    

        public Cliente buscarCli(int id)
        {
            return new pCliente().buscarCli(id);
        }

  

        public bool altaCli(Cliente cli)
        {
            return new pCliente().altaCli(cli);
        }

        public bool bajaCli(int id, int idAdmin)
        {
            return new pCliente().bajaCli(id, idAdmin);
        }

        public bool modificarCli(Cliente cli)
        {
            return new pCliente().modificarCli(cli);
        }




        #endregion

        #region Camioneros

      
        public List<Camionero> buscarCamioneroFiltro(Camionero pCamionero, string fchNacDesde, string fchNacHasta, string fchVencDesde, string fchVencHasta, string ordenar)
        {
            return new pCamionero().buscarCamioneroFiltro(pCamionero, fchNacDesde, fchNacHasta, fchVencDesde, fchVencHasta,  ordenar);
        }

        public Camionero buscarCamionero(int id)
        {
            return new pCamionero().buscarCamionero(id);
        }


        public bool altaCamionero(Camionero camionero, int idAdmin)
        {
            return new pCamionero().altaCamionero(camionero, idAdmin);
        }

        public bool bajaCamionero(int id, int idAdmin)
        {
            return new pCamionero().bajaCamionero(id, idAdmin);
        }

        public bool modCamionero(Camionero camionero, int idAdmin)
        {
            return new pCamionero().modCamionero(camionero, idAdmin);
        }

        #endregion

        #endregion

    }
}
