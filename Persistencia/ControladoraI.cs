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

        public List<Pedido> BuscarPedidoFiltro(string NombreCli, string Estado, string Viaje, double CostoMin, double CostoMax, string Ordenar)
        {
            return new pPedido().BuscarPedidoFiltro(NombreCli, Estado, Viaje, CostoMin, CostoMax, Ordenar);
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
        public bool modPedViajeEst(int idPedido, string estado)
        {
            return new pPedido().modPedViajeEst(idPedido, estado);
        }

       public bool altaPedido_Lote(Lote_Pedido lote_Pedido,  string CantLote, string CantDisp, string CantRess)
        {
            return new pPedido().altaPedido_Lote(lote_Pedido, CantLote, CantDisp, CantRess);
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
        public bool modCantPedidoLote(Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess)
        {
            return new pPedido().modCantPedidoLote(lote_Pedido, CantLote, CantDisp, CantRess);
        }


        public bool bajaPedidoProd(int idPedido, int idProducto, string cantRess, double precio)
        {
            return new pPedido().bajaPedidoProd(idPedido, idProducto, cantRess, precio);
        }

        public bool bajaLotesPedido(int idPedido, int idGranja, int idProducto, string fchProduccion, string cantLote, string CantDisp, string CantRess)
        {
            return new pPedido().bajaLotesPedido(idPedido, idGranja, idProducto, fchProduccion, cantLote, CantDisp, CantRess);
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

        public bool altaDeps(Deposito deposito)
        {
            return new pDeposito().altaDeps(deposito);
        }

        public bool bajaDeps(int id)
        {
            return new pDeposito().bajaDeps(id);
        }

        public bool modDeps(Deposito deposito)
        {
            return new pDeposito().modDeps(deposito);
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

        public bool altaGranja(Granja granja)
        {
            return new pGranja().altaGranja(granja);
        }

        public bool bajaGranja(int id)
        {
            return new pGranja().bajaGranja(id);
        }

        public bool modGranja(Granja granja)
        {
            return new pGranja().modGranja(granja);
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

        public bool altaCam(Camion camion)
        {
            return new pCamion().altaCam(camion);
        }

        public bool bajaCam(int id)
        {
            return new pCamion().bajaCam(id);
        }

        public bool modCam(Camion camion)
        {
            return new pCamion().modCam(camion);
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

        public bool altaProducto(Producto producto)
        {
            return new pProducto().altaProducto(producto);
        }

        public bool bajaProducto(int id)
        {
            return new pProducto().bajaProducto(id);
        }

        public bool modProducto(Producto producto)
        {
            return new pProducto().modProducto(producto);
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

        public bool altaPesti(Pesticida pesticida)
        {
            return new pPesticida().altaPesti(pesticida);
        }

        public bool bajaPesti(int id)
        {
            return new pPesticida().bajaPesti(id);
        }

        public bool modPesti(Pesticida pesticida)
        {
            return new pPesticida().modPesti(pesticida);
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

        public bool altaFerti(Fertilizante fertilizante)
        {
            return new pFertilizante().altaFerti(fertilizante);
        }

        public bool bajaFerti(int id)
        {
            return new pFertilizante().bajaFerti(id);
        }

        public bool modFerti(Fertilizante fertilizante)
        {
            return new pFertilizante().modFerti(fertilizante);
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

        public bool altaLote(Lote lote, string cantTotal)
        {
            return new pLote().altaLote(lote, cantTotal );
        }

        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal)
        {
            return new pLote().bajaLote(nombreGranja, nombreProducto, fchProduccion, cantTotal);
        }

        public bool modLote(Lote lote, string cantTotal)
        {
            return new pLote().modLote(lote, cantTotal);
        }

        #endregion

        #region Lotes_Fertis

        public List<Lote_Ferti> FertisEnLote(int idGranja, int idProducto, string fchProduccion, string buscar, string ord)
        {
            return new pLote_Ferti().FertisEnLote(idGranja, idProducto, fchProduccion, buscar, ord);
        }

        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Ferti().buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
        }
        public bool altaLoteFerti(Lote_Ferti loteF)
        {
            return new pLote_Ferti().altaLoteFerti(loteF);
        }

        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Ferti().bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
        }

        public bool modLoteFerti(Lote_Ferti loteF)
        {
            return new pLote_Ferti().modLoteFerti(loteF);
        }

        #endregion

        #region Lotes_Pestis

        public List<Lote_Pesti> PestisEnLote(int idGranja, int idProducto, string fchProduccion, string buscar, string ord)
        {
            return new pLote_Pesti().PestisEnLote(idGranja, idProducto, fchProduccion, buscar, ord);
        }

        public Lote_Pesti buscarLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Pesti().buscarLotePesti(idFertilizante, idGranja, idProducto, fchProduccion);
        }
        public bool altaLotePesti(Lote_Pesti loteF)
        {
            return new pLote_Pesti().altaLotePesti(loteF);
        }

        public bool bajaLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Pesti().bajaLotePesti(idFertilizante, idGranja, idProducto, fchProduccion);
        }

        public bool modLotePesti(Lote_Pesti loteP)
        {
            return new pLote_Pesti().modLotePesti(loteP);
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

        public bool altaViaje(Viaje viaje)
        {
            return new pViaje().altaViaje(viaje);
        }
        public bool altaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantViajeAct)
        {
            return new pViaje().altaViajePedido_Lote(viaje_Lot_Ped, CantViajeAct) ;
        }

        public bool bajaViajePedido_Lote(Viaje_Lot_Ped viaje_Lot_Ped, string CantTotal)
        {
            return new pViaje().bajaViajePedido_Lote(viaje_Lot_Ped, CantTotal);
        }
        public List<Viaje_Lot_Ped> buscarViajePedLote(int idPedido, int idViaje)
        {
            return new pViaje().buscarViajePedLote(idPedido, idViaje);
        }
        public bool bajaViaje(int id)
        {
            return new pViaje().bajaViaje(id);
        }

        public bool modViaje(Viaje viaje)
        {
            return new pViaje().modViaje(viaje);
        }

        #endregion

    }
}
