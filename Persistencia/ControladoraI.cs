using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace Persistencia
{
    public class ControladoraI
    {
        #region Instancia

        private static ControladoraI _instancia;

        public static ControladoraI obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraI();
            }
            return _instancia;
        }

        #endregion

        #region Pedidos

        public List<Pedido> BuscarPedidoFiltro(string NombreCli, string Estado, string Viaje, double CostoMin, double CostoMax, string fchPedidoMenor, string fchPedidoMayor, string fchEntregaMenor, string fchEntregaMayor, string Ordenar)
        {
            return new pPedido().BuscarPedidoFiltro(NombreCli, Estado, Viaje, CostoMin, CostoMax, fchPedidoMenor, fchPedidoMayor, fchEntregaMenor, fchEntregaMayor, Ordenar);
        }

        public List<Pedido_Prod> listPedidoCli_Prod(int idProducto)
        {
            return new pPedido().listPedidoCli_Prod(idProducto);
        }

        public Pedido_Prod buscarProductoCli(int idProducto, int idCliente)
        {
            return new pPedido().buscarProductoCli(idProducto, idCliente);
        }



        public List<Pedido> listPedidoCli(int idCli)
        {
            return new pPedido().listPedidoCli(idCli);
        }

        public List<Pedido> listPedido()
        {
            return new pPedido().listPedido();
        }

        public bool altaPedido(Pedido pedido)
        {
            return new pPedido().altaPedido(pedido);
        }

        public bool bajaPedido(int IdPedido)
        {
            return new pPedido().bajaPedido(IdPedido);
        }

        public bool altaPedido_Prod(Pedido_Prod pedido, string CantRes, double precio)
        {
            return new pPedido().altaPedido_Prod(pedido, CantRes, precio);
        }
        public bool modPedViajeEst(int idPedido, string estado, int idAdmin)
        {
            return new pPedido().modPedViajeEst(idPedido, estado, idAdmin);
        }

       public bool altaPedido_Lote(Lote_Pedido lote_Pedido,  string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            return new pPedido().altaPedido_Lote(lote_Pedido, CantLote, CantDisp, CantRess, idAdmin);
        }
       public bool cambiarEstadoPed(int idPedido, string estado)
        {
            return new pPedido().cambiarEstadoPed(idPedido, estado);
        }

        public List<string[]> buscarPedidoProd(int idPedido)
        {
            return new pPedido().buscarPedidoProd(idPedido);
        }

        public List<string[]> buscarPedidoLote(int idPedido)
        {
            return new pPedido().buscarPedidoLote(idPedido);
        }

        public string[] buscarProductoClixNom(int idPedido, string nomProd)
        {
            return new pPedido().buscarProductoClixNom(idPedido ,nomProd);
        }

       public bool modCantPedidoCli(int idPedido, int idProducto, string cantidad, string cantRess, double precio)
        {
            return new pPedido().modCantPedidoCli(idPedido, idProducto, cantidad, cantRess, precio);
        }
        public bool modCantPedidoLote(Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            return new pPedido().modCantPedidoLote(lote_Pedido, CantLote, CantDisp, CantRess, idAdmin);
        }


        public bool bajaPedidoProd(int idPedido, int idProducto, string cantRess, double precio)
        {
            return new pPedido().bajaPedidoProd(idPedido, idProducto, cantRess, precio);
        }

        public bool bajaLotesPedido(int idPedido, int idGranja, int idProducto, string fchProduccion, string cantLote, string CantDisp, string CantRess, int idAdmin)
        {
            return new pPedido().bajaLotesPedido(idPedido, idGranja, idProducto, fchProduccion, cantLote, CantDisp, CantRess, idAdmin);
        }


        #endregion

        #region Depositos


        public List<Deposito> buscarDepositoFiltro(Deposito pDeposito, int capacidadMenor, int capacidadMayor, int temperaturaMenor, int temperaturaMayor, string ordenar)
        {
            return new pDeposito().buscarDepositoFiltro(pDeposito, capacidadMenor, capacidadMayor, temperaturaMenor, temperaturaMayor, ordenar);
        }

        public Deposito buscarDeps(int id)
        {
            return new pDeposito().buscarDeps(id);
        }

        public bool altaDeps(Deposito deposito, int idAdmin)
        {
            return new pDeposito().altaDeps(deposito, idAdmin);
        }

        public bool bajaDeps(int id, int idAdmin)
        {
            return new pDeposito().bajaDeps(id, idAdmin);
        }

        public bool modDeps(Deposito deposito, int idAdmin)
        {
            return new pDeposito().modDeps(deposito, idAdmin);
        }

        #endregion

        #region Granjas


    
        public List<Granja> buscarGranjaFiltro(Granja pGranja, string ordenar)
        {
            return new pGranja().buscarGranjaFiltro(pGranja, ordenar);
        }

        public Granja buscarGranja(int id)
        {
            return new pGranja().buscarGranja(id);
        }

        public bool altaGranja(Granja granja, int idAdmin)
        {
            return new pGranja().altaGranja(granja, idAdmin);
        }

        public bool bajaGranja(int id, int idAdmin)
        {
            return new pGranja().bajaGranja(id, idAdmin);
        }

        public bool modGranja(Granja granja, int idAdmin)
        {
            return new pGranja().modGranja(granja, idAdmin);
        }

        #endregion

        #region Camiones

   


        public List<Camion> buscarFiltroCam(Camion pCamion, double cargaMenor, double cargaMayor, string ordenar)
        {
            return new pCamion().buscarFiltroCam(pCamion, cargaMenor, cargaMayor, ordenar);
        }

        public Camion buscarCam(int id)
        {
            return new pCamion().buscarCam(id);
        }

        public bool altaCam(Camion camion, int idAdmin)
        {
            return new pCamion().altaCam(camion, idAdmin);
        }

        public bool bajaCam(int id, int idAdmin)
        {
            return new pCamion().bajaCam(id, idAdmin);
        }

        public bool modCam(Camion camion, int idAdmin)
        {
            return new pCamion().modCam(camion, idAdmin);
        }

        #endregion

        #region Productos

        public List<Producto> buscarProductoFiltro(Producto producto, int precioMenor, int precioMayor, string ordenar)
        {
            return new pProducto().buscarProductoFiltro(producto, precioMenor, precioMayor, ordenar);
        }

        public List<Producto> buscarProductoCatFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            return new pProducto().buscarProductoCatFiltro(buscar, tipo, tipoVen, ordenar);
        }





        public Producto buscarProducto(int id)
        {
            return new pProducto().buscarProducto(id);
        }

        public bool altaProducto(Producto producto, int idAdmin)
        {
            return new pProducto().altaProducto(producto, idAdmin);
        }

        public bool bajaProducto(int id, int idAdmin)
        {
            return new pProducto().bajaProducto(id, idAdmin);
        }

        public bool modProducto(Producto producto, int idAdmin)
        {
            return new pProducto().modProducto(producto, idAdmin);
        }

        #endregion

        #region Pesticida

       
        public List<Pesticida> buscarPesticidaFiltro(Pesticida pPesticida, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            return new pPesticida().buscarPesticidaFiltro(pPesticida, phMenor, phMayor, ordenar, idGranja, idProducto, fchProduccion);
        }

        public Pesticida buscarPesti(int id)
        {
            return new pPesticida().buscarPesti(id);
        }

        public bool altaPesti(Pesticida pesticida, int idAdmin)
        {
            return new pPesticida().altaPesti(pesticida, idAdmin);
        }

        public bool bajaPesti(int id, int idAdmin)
        {
            return new pPesticida().bajaPesti(id, idAdmin);
        }

        public bool modPesti(Pesticida pesticida, int idAdmin)
        {
            return new pPesticida().modPesti(pesticida, idAdmin);
        }

        #endregion

        #region Fertilizantes


        public List<Fertilizante> buscarFertilizanteFiltro(Fertilizante pFertilizante, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            return new pFertilizante().buscarFertilizanteFiltro(pFertilizante, phMenor,phMayor,ordenar, idGranja, idProducto, fchProduccion);
        }

        public Fertilizante buscarFerti(int id)
        {
            return new pFertilizante().buscarFerti(id);
        }

        public bool altaFerti(Fertilizante fertilizante, int idAdmin)
        {
            return new pFertilizante().altaFerti(fertilizante, idAdmin);
        }

        public bool bajaFerti(int id, int idAdmin)
        {
            return new pFertilizante().bajaFerti(id, idAdmin);
        }

        public bool modFerti(Fertilizante fertilizante, int idAdmin)
        {
            return new pFertilizante().modFerti(fertilizante, idAdmin);
        }

        #endregion

        #region Lotes

  
        public List<Lote> buscarFiltrarLotes(Lote lote, double precioMenor, double precioMayor,  string fchProduccionMenor, string fchProduccionMayor, string fchCaducidadMenor, string fchCaducidadMayor, string ordenar)
        {
            return new pLote().buscarFiltrarLotes(lote, precioMenor, precioMayor, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);
        }

        public Lote buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            return new pLote().buscarLote(nombreGranja, nombreProducto, fchProduccion);
        }

        public bool altaLote(Lote lote, string cantTotal, int idAdmin)
        {
            return new pLote().altaLote(lote, cantTotal, idAdmin);
        }

        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal, int idAdmin)
        {
            return new pLote().bajaLote(nombreGranja, nombreProducto, fchProduccion, cantTotal, idAdmin);
        }

        public bool modLote(Lote lote, string cantTotal, int idAdmin)
        {
            return new pLote().modLote(lote, cantTotal, idAdmin);
        }

        #endregion

        #region Lotes_Fertis

        public List<Lote_Ferti> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Ferti().FertisEnLote(idGranja, idProducto, fchProduccion);
        }

        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Ferti().buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
        }
        public bool altaLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            return new pLote_Ferti().altaLoteFerti(loteF, idAdmin);
        }

        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, int idAdmin)
        {
            return new pLote_Ferti().bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion, idAdmin);
        }

        public bool modLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            return new pLote_Ferti().modLoteFerti(loteF, idAdmin);
        }

        #endregion

        #region Lotes_Pestis

        public List<Lote_Pesti> PestisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Pesti().PestisEnLote(idGranja, idProducto, fchProduccion);
        }

        public Lote_Pesti buscarLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Pesti().buscarLotePesti(idFertilizante, idGranja, idProducto, fchProduccion);
        }
        public bool altaLotePesti(Lote_Pesti loteF, int idAdmin)
        {
            return new pLote_Pesti().altaLotePesti(loteF, idAdmin);
        }

        public bool bajaLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, int idAdmin)
        {
            return new pLote_Pesti().bajaLotePesti(idFertilizante, idGranja, idProducto, fchProduccion, idAdmin);
        }

        public bool modLotePesti(Lote_Pesti loteP, int idAdmin)
        {
            return new pLote_Pesti().modLotePesti(loteP, idAdmin);
        }

        #endregion

        #region Viajes

        public List<Viaje> buscarViajeFiltro(Viaje pViaje, int costoMenor, int costoMayor, string fechaMenor, string fechaMayor, string ordenar)
        {
            return new pViaje().buscarViajeFiltro(pViaje, costoMenor, costoMayor, fechaMenor, fechaMayor, ordenar);
        }

        public Viaje buscarViaje(int idViaje)
        {
            return new pViaje().buscarViaje(idViaje);
        }

        public bool altaViaje(Viaje viaje, int idAdmin)
        {
            return new pViaje().altaViaje(viaje, idAdmin);
        }
        public bool altaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantViajeAct, int idAdmin)
        {
            return new pViaje().altaViajePedido_Lote(viaje_Lot_Ped, CantViajeAct, idAdmin) ;
        }

        public bool bajaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantTotal, int idAdmin)
        {
            return new pViaje().bajaViajePedido_Lote(viaje_Lot_Ped, CantTotal, idAdmin);
        }
        public List<Viaje_Lot_Ped> buscarViajePedLote(int idPedido, int idViaje)
        {
            return new pViaje().buscarViajePedLote(idPedido, idViaje);
        }
        public bool bajaViaje(int id, int idAdmin)
        {
            return new pViaje().bajaViaje(id, idAdmin);
        }

        public bool modViaje(Viaje viaje, int idAdmin)
        {
            return new pViaje().modViaje(viaje, idAdmin);
        }

        #endregion

        #region Estadisitcas

        public List<Auditoria> buscarAuditoriaFiltro(Auditoria auditoria, string fchMenor, string fchMayor, string ordenar)
        {
            return new pEstadistica().buscarAuditoriaFiltro(auditoria, fchMenor, fchMayor, ordenar);
        }

        #endregion

    }
}
