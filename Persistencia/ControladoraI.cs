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

        #region Depositos

        public List<Deposito> listIdDeps()
        {
            return new pDeposito().listIdDeps();
        }

        public List<Deposito> listDeps()
        {
            return new pDeposito().listDeps();
        }
        public List<Deposito> buscarVarDeps(string var)
        {
            return new pDeposito().buscarVarDeps(var);
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

        public List<Granja> listIdGranjas()
        {
            return new pGranja().listIdGranjas();
        }

        public List<Granja> listGranjas()
        {
            return new pGranja().listGranjas();
        }
        public List<Granja> buscarVarGranjas(string var)
        {
            return new pGranja().buscarVarGranjas(var);
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

        public List<Camion> listIdCam()
        {
            return new pCamion().listIdCam();
        }

        public List<Camion> lstCam()
        {
            return new pCamion().lstCam();
        }
        public List<Camion> buscarVarCam(string var)
        {
            return new pCamion().buscarVarCam(var);
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

        public List<Producto> listIdProductos()
        {
            return new pProducto().listIdProductos();
        }

        public List<Producto> listProductos()
        {
            return new pProducto().listProductos();
        }
        public List<Producto> buscarVarProductos(string var)
        {
            return new pProducto().buscarVarProductos(var);
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

        public List<Pesticida> listIdPesti()
        {
            return new pPesticida().listIdPesti();
        }

        public List<Pesticida> lstPesti()
        {
            return new pPesticida().lstPesti();
        }
        public List<Pesticida> buscarVarPesti(string var)
        {
            return new pPesticida().buscarVarPesti(var);
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

        public List<Fertilizante> listIdFert()
        {
            return new pFertilizante().listIdFert();
        }

        public List<Fertilizante> lstFerti()
        {
            return new pFertilizante().lstFerti();
        }
        public List<Fertilizante> buscarVarFerti(string var)
        {
            return new pFertilizante().buscarVarFerti(var);
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

        public List<Lote> listLotes()
        {
            return new pLote().listLotes();
        } 

        public List<Lote> buscarVarLotes(string var)
        {
            return new pLote().buscarVarLotes(var);
        }

        public Lote buscarLote(int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote().buscarLote(idGranja, idProducto, fchProduccion);
        }

        public bool altaLote(Lote lote)
        {
            return new pLote().altaLote(lote);
        }

        public bool bajaLote(int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote().bajaLote(idGranja, idProducto, fchProduccion);
        }

        public bool modLote(Lote lote)
        {
            return new pLote().modLote(lote);
        }

        #endregion

        #region Lotes_Fertis

        public List<Lote_Ferti> listLotesFertis()
        {
            return new pLote_Ferti().listLotesFertis();
        }

        public List<string[]> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            return new pLote_Ferti().FertisEnLote(idGranja, idProducto, fchProduccion);
        }

        public List<Lote_Ferti> buscarVarLotesFertis(string var)
        {
            return new pLote_Ferti().buscarVarLotesFertis(var);
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

    }
}
