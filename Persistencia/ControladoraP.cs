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

        public List<Admin> lstAdmin()
        {
            return new pAdmin().lstAdmin();
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

        #region Camioneros

        public List<Camionero> listCamionero()
        {
            return new pCamionero().listCamionero();
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
