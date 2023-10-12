using Clases;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class ControladoraItem
    {
        #region Instancia

        private static ControladoraItem _instancia;

        public static ControladoraItem obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraItem();

            }
            return _instancia;

        }

        #endregion


        #region Pedido

        public List<Pedido_Prod> listPedidoCli_Prod(int idProducto)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pedido_Prod> lst = inst.listPedidoCli_Prod(idProducto);
            return lst;

        }

        public List<Pedido> BuscarPedidoFiltro(string NombreCli, string Estado, string Viaje,double CostoMin, double CostoMax, string fchPedidoMenor, string fchPedidoMayor, string fchEntregaMenor, string fchEntregaMayor, string Ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pedido> lst = inst.BuscarPedidoFiltro(NombreCli, Estado, Viaje, CostoMin, CostoMax, fchPedidoMenor, fchPedidoMayor, fchEntregaMenor, fchEntregaMayor, Ordenar);
            return lst;

        }

        public List<Pedido> listPedidoCli(int idCli)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pedido> lst = inst.listPedidoCli(idCli);
            return lst;
        }

        public List<Pedido> listPedido()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pedido> lst = inst.listPedido();
            return lst;
        }
     
        public bool altaPedido(Pedido pedido)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaPedido(pedido))
            {
                return true;
            }
            else
                return false;
        }
        public Pedido_Prod buscarProductoCli(int idProducto, int idCliente)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            Pedido_Prod lst = inst.buscarProductoCli(idProducto, idCliente);
            return lst;
        }

        public List<string[]> buscarPedidoProd(int idPedido)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<string[]> lst = inst.buscarPedidoProd(idPedido);
            return lst;
        }

        public List<string[]> buscarPedidoLote(int idPedido)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<string[]> lst = inst.buscarPedidoLote(idPedido);
            return lst;
        }


        public string[] buscarProductoClixNom(int idPedido, string nomProd)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            string[] lst = inst.buscarProductoClixNom(idPedido, nomProd);
            return lst;
        }
        public bool altaPedido_Prod(Pedido_Prod pedido, string CantRes, double precio)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaPedido_Prod(pedido, CantRes, precio))
            {
                return true;
            }
            else
                return false;
        }
        public bool altaPedido_Lote(Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            {
                ControladoraI inst = ControladoraI.obtenerInstancia();

                if (inst.altaPedido_Lote(lote_Pedido, CantLote, CantDisp, CantRess, idAdmin))
                {
                    return true;
                }
                else
                    return false;
            }
        }


        public bool bajaPedido(int idPedido)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.bajaPedido(idPedido))
            {
                return true;
            }
            else
                return false;
        }




        public bool bajaPedidoProd(int idPedido, int idProducto, string cantRess, double precio)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.bajaPedidoProd(idPedido, idProducto, cantRess, precio))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLotesPedido(int idPedido, int idGranja, int idProducto, string fchProduccion, string cantLote, string CantDisp, string CantRess, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.bajaLotesPedido(idPedido, idGranja, idProducto, fchProduccion, cantLote, CantDisp, CantRess, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modCantPedidoCli(int idPedido, int idProducto, string cantidad, string cantRess, double precio)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modCantPedidoCli(idPedido, idProducto, cantidad, cantRess, precio))
            {
                return true;
            }
            else
                return false;
        }

       public bool modCantPedidoLote(Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modCantPedidoLote(lote_Pedido, CantLote, CantDisp, CantRess, idAdmin))
            {
                return true;
            }
            else
                return false;



        }

        public bool cambiarEstadoPed(int idPedido, string estado)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.cambiarEstadoPed(idPedido, estado))
            {
                return true;
            }
            else
                return false;
        }


        public bool modPedViajeEst(int idPedido, string estado, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modPedViajeEst(idPedido, estado, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Depositos




        public List<Deposito> buscarDepositoFiltro(Deposito pDeposito, int capacidadMenor, int capacidadMayor, int temperaturaMenor, int temperaturaMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Deposito> lst = inst.buscarDepositoFiltro(pDeposito, capacidadMenor, capacidadMayor, temperaturaMenor, temperaturaMayor, ordenar);
            return lst;
        }

        public Deposito buscarDeps(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Deposito deposito = inst.buscarDeps(id);
            return deposito;

        }

        public bool altaDeps(Deposito deposito, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaDeps(deposito, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaDeps(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaDeps(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modDeps(Deposito deposito, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modDeps(deposito, idAdmin))
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



        public List<Granja> buscarGranjaFiltro(Granja pGranja, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Granja> lst = inst.buscarGranjaFiltro(pGranja, ordenar);
            return lst;
        }

        public Granja buscarGranja(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Granja granja = inst.buscarGranja(id);
            return granja;

        }

        public bool altaGranja(Granja granja, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaGranja(granja, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaGranja(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaGranja(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modGranja(Granja granja, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modGranja(granja, idAdmin))
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

        public List<Producto> buscarProductoFiltro(Producto producto, int precioMenor, int precioMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.buscarProductoFiltro(producto, precioMenor, precioMayor, ordenar);
            return lst;
        }

        public List<Producto> buscarProductoCatFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.buscarProductoCatFiltro(buscar, tipo, tipoVen, ordenar);
            return lst;
        }



        public Producto buscarProducto(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Producto producto = inst.buscarProducto(id);
            return producto;

        }

        public bool altaProducto(Producto producto, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaProducto(producto, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProducto(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaProducto(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProducto(Producto producto, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modProducto(producto, idAdmin))
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





        public List<Pesticida> buscarPesticidaFiltro(Pesticida pPesticida, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pesticida> lst = inst.buscarPesticidaFiltro(pPesticida, phMenor, phMayor, ordenar, idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Pesticida buscarPesti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Pesticida pesti = inst.buscarPesti(id);
            return pesti;

        }

        public bool altaPesti(Pesticida pesticida, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaPesti(pesticida, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaPesti(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaPesti(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modPesti(Pesticida pesticida, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modPesti(pesticida, idAdmin))
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



        public List<Fertilizante> buscarFertilizanteFiltro(Fertilizante pFertilizante, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Fertilizante> lst = inst.buscarFertilizanteFiltro(pFertilizante, phMenor, phMayor, ordenar, idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Fertilizante buscarFerti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Fertilizante fertilizante = inst.buscarFerti(id);
            return fertilizante;

        }

        public bool altaFerti(Fertilizante fertilizante, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaFerti(fertilizante, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaFerti(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaFerti(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modFerti(Fertilizante fertilizante, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modFerti(fertilizante, idAdmin))
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


        public List<Lote> buscarFiltrarLotes(Lote lote, double precioMenor, double precioMayor, string fchProduccionMenor, string fchProduccionMayor, string fchCaducidadMenor, string fchCaducidadMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Lote> lst = inst.buscarFiltrarLotes(lote, precioMenor, precioMayor, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);
            return lst;
        }

        public Lote buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Lote lote = inst.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            return lote;

        }

        public bool altaLote(Lote lote, string cantTotal, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLote(lote, cantTotal, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLote(nombreGranja, nombreProducto, fchProduccion, cantTotal, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLote(Lote lote, string cantTotal, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modLote(lote, cantTotal, idAdmin))
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


        public List<Lote_Pesti> PestisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Lote_Pesti> lst = inst.PestisEnLote(idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Lote_Pesti buscarLotePesti(int idPesicida, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Lote_Pesti loteP = inst.buscarLotePesti(idPesicida, idGranja, idProducto, fchProduccion);
            return loteP;

        }

        public bool altaLotePesti(Lote_Pesti loteP, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLotePesti(loteP, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLotePesti(idFertilizante, idGranja, idProducto, fchProduccion, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLotePesti(Lote_Pesti loteP, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modLotePesti(loteP, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Lotes_Fertis

        public List<Lote_Ferti> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Lote_Ferti> lst = inst.FertisEnLote(idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Lote_Ferti loteF = inst.buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
            return loteF;

        }

        public bool altaLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLoteFerti(loteF, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modLoteFerti(loteF, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Camiones



        public List<Camion> buscarFiltroCam(Camion pCamion, double cargaMenor, double cargaMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Camion> lst = inst.buscarFiltroCam(pCamion, cargaMenor, cargaMayor, ordenar);
            return lst;
        }

        public Camion buscarCam(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Camion pesti = inst.buscarCam(id);
            return pesti;

        }

        public bool altaCam(Camion camion, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaCam(camion, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaCam(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaCam(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modCam(Camion camion, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modCam(camion, idAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Viajes

        public List<Viaje> buscarViajeFiltro(Viaje pViaje, int costoMenor, int costoMayor, string fechaMenor, string fechaMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Viaje> lst = inst.buscarViajeFiltro(pViaje, costoMenor, costoMayor, fechaMenor, fechaMayor, ordenar);
            return lst;
        }

        public Viaje buscarViaje(int idViaje)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Viaje viaje = inst.buscarViaje(idViaje);
            return viaje;

        }

        public bool altaViaje(Viaje viaje, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaViaje(viaje, idAdmin))
            {
                return true;
            }
            else
                return false;
        }
        public List<Viaje_Lot_Ped> buscarViajePedLote(int idPedido, int idViaje)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Viaje_Lot_Ped> lst = inst.buscarViajePedLote( idPedido, idViaje);
            return lst;
        }
        public bool altaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantViajeAct, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaViajePedido_Lote(viaje_Lot_Ped, CantViajeAct, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantTotal, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.bajaViajePedido_Lote(viaje_Lot_Ped, CantTotal, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaViaje(int id, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaViaje(id, idAdmin))
            {
                return true;
            }
            else
                return false;
        }

        public bool modViaje(Viaje viaje, int idAdmin)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modViaje(viaje, idAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Estadisticas

        public List<Auditoria> buscarAuditoriaFiltro(Auditoria auditoria, string fchMenor, string fchMayor, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Auditoria> lst = inst.buscarAuditoriaFiltro(auditoria, fchMenor, fchMayor, ordenar);
            return lst;
        }

        #endregion

    }
}
