﻿using System;
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

        #region Admins

        public List<Admin> lstAdmin()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Admin> lst = inst.lstAdmin();
            return lst;
        }


        public List<Admin> buscarVarAdmin(string var)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Admin> lst = inst.buscarVarAdmin(var);
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

        public List<Cliente> lstCli()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Cliente> lst = inst.lstCliente();
            return lst;
        }




        public List<Cliente> buscarVarCli(string var)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Cliente> lst = inst.buscarVarCli(var);
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

        public List<Camionero> listCamionero()
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();

            return inst.listCamionero();
        }

        public List<Camionero> buscarVarCamionero(string var)
        {
            ControladoraP inst = ControladoraP.obtenerInstancia();
            List<Camionero> lst = inst.buscarVarCamionero(var);
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
