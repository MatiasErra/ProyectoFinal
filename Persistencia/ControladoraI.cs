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

        #region Producen
        /*
        public List<Produce> listIdProducen()
        {
            return new pProduce().listIdProducen();
        }

        public List<Produce> listProducen()
        {
            return new pProduce().listProducen();
        }
        public List<Produce> buscarVarProducen(string var)
        {
            return new pProduce().buscarVarProducen(var);
        }

        public Produce buscarProduce(int idGranja, int idProducto, string fchProduccion)
        {
            return new pProduce().buscarProduce(idGranja, idProducto, fchProduccion);
        }

        public bool altaProduce(Produce produce)
        {
            return new pProduce().altaProduce(produce);
        }

        public bool bajaProduce(int idGranja, int idProducto, string fchProduccion)
        {
            return new pProduce().bajaProduce(idGranja, idProducto, fchProduccion);
        }

        public bool modProduce(Produce produce)
        {
            return new pProduce().modProduce(produce);
        }
        */
        #endregion


        /*public bool altaCamion(Camion camion)
        {
            return new pCamion().altaCamion(camion);
        }*/
    }
}
