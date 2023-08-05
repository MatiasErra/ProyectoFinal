using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

        public List<string> userRepetidoAdm()
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<string> lst = ins.userRepetidoAdm();
            return lst;
        }

        public int iniciarSesionAdm(string user, string pass)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            int id = ins.iniciarSesionAdm(user, pass);
            return id;
        }
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

        public List<string> userRepetidoCli()
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<string> lst = ins.userRepetidoCli();
            return lst;
        }





        public int iniciarSesionCli(string user, string pass)
        {
        ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
        int id = ins. iniciarSesionCli(user, pass);
            return id;
        }
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

        public List<Camionero> buscarVarCamionero(string var)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Camionero> lst = ins.buscarVarCamionero(var);
            return lst;
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

        public List<Deposito> buscarVarDeps(string var)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Deposito> lst = ins.buscarVarDeps(var);
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

        #region Granjas

        public List<Granja> listIdGranjas()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Granja> lst = inst.listIdGranjas();
            return lst;

        }

        public List<Granja> listGranjas()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Granja> lst = inst.listGranjas();
            return lst;

        }

        public List<Granja> buscarVarGranjas(string var)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Granja> lst = ins.buscarVarGranjas(var);
            return lst;
        }

        public Granja buscarGranja(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Granja granja = inst.buscarGranja(id);
            return granja;

        }

        public bool altaGranja(Granja granja)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaGranja(granja))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaGranja(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaGranja(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modGranja(Granja granja)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modGranja(granja))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Productos

        public List<Producto> listIdProductos()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Producto> lst = inst.listIdProductos();
            return lst;

        }

        public List<Producto> listProductos()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Producto> lst = inst.listProductos();
            return lst;

        }

        public List<Producto> buscarVarProductos(string var)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Producto> lst = ins.buscarVarProductos(var);
            return lst;
        }

        public Producto buscarProducto(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Producto producto = inst.buscarProducto(id);
            return producto;

        }

        public bool altaProducto(Producto producto)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaProducto(producto))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProducto(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaProducto(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProducto(Producto producto)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modProducto(producto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Pesticida

        public List<Pesticida> listIdPesti()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pesticida> lst = inst.listIdPesti();
            return lst;

        }

        public List<Pesticida> lstPesti()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pesticida> lst = inst.lstPesti();
            return lst;
        }

        public List<Pesticida> buscarVarPesti(string var)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pesticida> lst = inst.buscarVarPesti(var);
            return lst;
        }

        public Pesticida buscarPesti(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Pesticida pesti = inst.buscarPesti(id);
            return pesti;

        }

        public bool altaPesti(Pesticida pesticida)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaPesti(pesticida))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaPesti(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();


            if (inst.bajaPesti(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modPesti(Pesticida pesticida)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modPesti(pesticida))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion

        #region Fertilizantes

        public List<Fertilizante> listIdFert()
        {
           
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Fertilizante> lst = inst.listIdFert();
            return lst;

        }

        public List<Fertilizante> lstFerti()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Fertilizante> lst = inst.lstFerti();
            return lst;
        }

        public List<Fertilizante> buscarVarFerti(string var)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Fertilizante> lst = inst.buscarVarFerti(var);
            return lst;
        }

        public Fertilizante buscarFerti(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Fertilizante fertilizante = inst.buscarFerti(id);
            return fertilizante;

        }

        public bool altaFerti(Fertilizante fertilizante)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaFerti(fertilizante))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaFerti(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();


            if (inst.bajaFerti(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modFerti(Fertilizante fertilizante)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modFerti(fertilizante))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion

        #region Producen
        public List<Produce> listIdProducen()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Produce> lst = inst.listIdProducen();
            return lst;

        }

        public List<Produce> listProducen()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Produce> lst = inst.listProducen();
            return lst;

        }

        public List<Produce> buscarVarProducen(string var)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Produce> lst = ins.buscarVarProducen(var);
            return lst;
        }

        public Produce buscarProduce(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Produce produce = inst.buscarProduce(idGranja, idProducto, fchProduccion);
            return produce;

        }

        public bool altaProduce(Produce produce)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaProduce(produce))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProduce(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaProduce(idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProduce(Produce produce)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modProduce(produce))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Camiones
        public List<Camion> listIdCam()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Camion> lst = inst.listIdCam();
            return lst;

        }

        public List<Camion> lstCam()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Camion> lst = inst.lstCam();
            return lst;
        }

        public List<Camion> buscarVarCam(string var)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Camion> lst = inst.buscarVarCam(var);
            return lst;
        }

        public Camion buscarCam(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Camion pesti = inst.buscarCam(id);
            return pesti;

        }

        public bool altaCam(Camion camion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaCam(camion))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaCam(int id)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();


            if (inst.bajaCam(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modCam(Camion camion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modCam(camion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion




    }
}