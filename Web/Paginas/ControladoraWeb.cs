using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using Clases;
using Controladoras;
using Web.Paginas.Granjas;
using Web.Paginas.Productos;

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


        #region Pedido

        public List<Pedido> listPedidoCli(int idCli)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pedido> lst = inst.listPedidoCli(idCli);
            return lst;
        }

        public List<Pedido> listPedido()
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pedido> lst = inst.listPedido();
            return lst;
        }
     

        public List<Pedido_Prod> listPedidoCli_Prod(int idProducto)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pedido_Prod> lst = inst.listPedidoCli_Prod(idProducto);
            return lst;
        }

        public List<string[]> buscarPedidoProd(int idPedido)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<string[]> lst = inst.buscarPedidoProd(idPedido);
            return lst;
        }


        public bool altaPedido(Pedido pedido)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaPedido(pedido))
            {
                return true;
            }
            else
                return false;
        }


        public bool modCantPediodCli(int idPedido, int idProducto, string cantidad, string cantRess, double precio)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.modCantPediodCli(idPedido, idProducto, cantidad, cantRess, precio))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaPedido(int idPedido)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaPedido(idPedido))
            {
                return true;
            }
            else
                return false;
        }


        public bool bajaPedidoProd(int idPedido, int idProducto, string cantRess, double precio)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaPedidoProd(idPedido, idProducto, cantRess, precio))
            {
                return true;
            }
            else
                return false;
        }



        public bool altaPedido_Prod(Pedido_Prod pedido, string CantRes, double precio)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaPedido_Prod(pedido, CantRes, precio))
            {
                return true;
            }
            else
                return false;


        }
        public Pedido_Prod buscarProductoCli(int idProducto, int idCliente)
        {

            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Pedido_Prod lst = inst.buscarProductoCli(idProducto, idCliente);
            return lst;
        }


        public bool cambiarEstadoPed(int idPedido, string estado)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.cambiarEstadoPed(idPedido, estado))
            {
                return true;
            }
            else
                return false;
        }


        public string[] buscarProductoClixNom(int idPedido, string nomProd)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            string[] lst = inst.buscarProductoClixNom(idPedido, nomProd);
            return lst;
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
   

        public List<Admin> buscarAdminFiltro(string buscar, string varEst, string varAdm, string ordenar)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Admin> lst = ins.buscarAdminFiltro(buscar, varEst, varAdm, ordenar);
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
            int id = ins.iniciarSesionCli(user, pass);
            return id;
        }
       
        public List<Cliente> buscarCliFiltro(string buscar, string ordenar)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Cliente> lst = ins.buscarCliFiltro(buscar, ordenar);
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


        public List<Camionero> buscarCamioneroFiltro(string buscar, string disp, string ordenar)
        {
            ControladoraPersona ins = ControladoraPersona.obtenerInstancia();
            List<Camionero> lst = ins.buscarCamioneroFiltro(buscar, disp, ordenar);
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



        public List<Deposito> buscarDepositoFiltro(string buscar, string ordenar)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Deposito> lst = ins.buscarDepositoFiltro(buscar, ordenar);
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


 

        public List<Granja> buscarGranjaFiltro(string buscar, string orden)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Granja> lst = ins.buscarGranjaFiltro(buscar, orden);
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

        public List<Producto> buscarProductoFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Producto> lst = inst.buscarProductoFiltro(buscar, tipo, tipoVen, ordenar);
            return lst;

        }

        public List<Producto> buscarProductoCatFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Producto> lst = inst.buscarProductoCatFiltro(buscar, tipo, tipoVen, ordenar);
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


        public List<Pesticida> buscarPesticidaFiltro(string buscar, string impact, string ordenar)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Pesticida> lst = inst.buscarPesticidaFiltro(buscar, impact, ordenar);
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


    

        public List<Fertilizante> buscarFertilizanteFiltro(string buscar, string impact, string ordenar)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Fertilizante> lst = inst.buscarFertilizanteFiltro(buscar, impact, ordenar);
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

        #region Lotes

      
        public List<Lote> buscarFiltrarLotes(string buscar, string ordenar)
        {
            ControladoraItem ins = ControladoraItem.obtenerInstancia();
            List<Lote> lst = ins.buscarFiltrarLotes(buscar, ordenar);
            return lst;
        }

        public Lote buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Lote lote = inst.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            return lote;

        }

        public bool altaLote(Lote lote, string cantTotal)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.altaLote(lote, cantTotal))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            if (inst.bajaLote(nombreGranja, nombreProducto, fchProduccion, cantTotal))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLote(Lote lote, string cantTotal)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modLote(lote, cantTotal))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Lotes_Fertis


        public List<Lote_Ferti> FertisEnLote(int idGranja, int idProducto, string fchProduccion, string buscar, string ord)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Lote_Ferti> lst = inst.FertisEnLote(idGranja, idProducto, fchProduccion, buscar, ord);
            return lst;
        }

        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Lote_Ferti loteF = inst.buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
            return loteF;

        }

        public bool altaLoteFerti(Lote_Ferti loteF)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.altaLoteFerti(loteF))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modLoteFerti(Lote_Ferti loteF)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modLoteFerti(loteF))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Lotes_Pestis

        public List<Lote_Pesti> PestisEnLote (int idGranja, int idProducto, string fchProduccion, string buscar, string ord)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Lote_Pesti> lst = inst.PestisEnLote(idGranja, idProducto, fchProduccion, buscar, ord);
            return lst;
        }

        public Lote_Pesti buscarLotePesti(int idPesticida, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();

            Lote_Pesti loteP = inst.buscarLotePesti(idPesticida, idGranja, idProducto, fchProduccion);
            return loteP;

        }

        public bool altaLotePesti(Lote_Pesti loteP)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.altaLotePesti(loteP))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bajaLotePesti(int idPesticida, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.bajaLotePesti(idPesticida, idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool modLotePesti(Lote_Pesti loteP)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            if (inst.modLotePesti(loteP))
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
 
 

        public List<Camion> buscarFiltroCam(string buscar, string disp, string ordenar)
        {
            ControladoraItem inst = ControladoraItem.obtenerInstancia();
            List<Camion> lst = inst.buscarFiltroCam(buscar, disp, ordenar);
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