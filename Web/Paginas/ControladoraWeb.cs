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

        #region Instancia

        private static Controladora _controladora;
        private static ControladoraWeb _instancia;

        public static Controladora Controladora { get => _controladora; set => _controladora = value; }

        public static ControladoraWeb obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraWeb();
                _controladora = Controladora.obtenerInstancia();

            }
            return _instancia;
        }

        #endregion

        #region Personas

        public List<Persona> lstIdPersonas()
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Persona> lst = ins.lstIdPersonas();
            return lst;

        }

        #region Admins

        public List<Admin> lstAdmin()
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Admin> lst = ins.lstAdmin();
            return lst;
        }

        public List<Admin> buscarVarAdmin(string var)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Admin> lst = ins.buscarVarAdmin(var);
            return lst;
        }



        public Admin buscarAdm(int id)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();

            Admin admin = ins.buscarAdm(id);
            return admin;

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

        public bool modificarAdm(Admin admin)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
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

        public List<Cliente> lstCli()
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Cliente> lst = ins.lstCli();
            return lst;
        }
        public List<Cliente> buscarVarCli(string var)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Cliente> lst = ins.buscarVarCli(var);
            return lst;
        }




        public Cliente buscarCli(int id)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();

            Cliente cli = ins.buscarCli(id);
            return cli;

        }

        public bool altaCli(Cliente cli)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            if (ins.altaCli(cli))
            {
                return true;
            }
            else
                return false;
        }


        public bool bajaCli(int id)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();


            if (ins.bajaCli(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modificarCli(Cliente cli)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            if (ins.modificarCli(cli))
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

        public List<Camionero> listCamionero()
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            return inst.listCamionero();
        }

        public Camionero buscarCamionero(int id)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            return inst.buscarCamionero(id);
        }


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

        public bool modCamionero(Camionero camionero)
        {
            ControladoraPersona inst = ControladoraPersona.obtenerInstancia();
            if (inst.modCamionero(camionero))
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

        #region Depositos

        public List<Deposito> listIdDeps()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Deposito> lst = inst.listIdDeps();
            return lst;

        }

        public List<Deposito> listDeps()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Deposito> lst = inst.listDeps();
            return lst;

        }

        public Deposito buscarDeps(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Deposito deposito = inst.buscarDeps(id);
            return deposito;

        }

        public bool altaDeps(Deposito deposito)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaDeps(deposito))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaDeps(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaDeps(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modDeps(Deposito deposito)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modDeps(deposito))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

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