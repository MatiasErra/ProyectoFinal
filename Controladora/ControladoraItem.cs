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

        #region Depositos

        public List<Deposito> listIdDeps()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Deposito> lst = inst.listIdDeps();
            return lst;

        }

        public List<Deposito> listDeps()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Deposito> lst = inst.listDeps();
            return lst;
        }

        public List<Deposito> buscarVarDeps(string var)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Deposito> lst = inst.buscarVarDeps(var);
            return lst;
        }

        public Deposito buscarDeps(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Deposito deposito = inst.buscarDeps(id);
            return deposito;

        }

        public bool altaDeps(Deposito deposito)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaDeps(deposito))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaDeps(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaDeps(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modDeps(Deposito deposito)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
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
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Granja> lst = inst.listIdGranjas();
            return lst;

        }

        public List<Granja> listGranjas()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Granja> lst = inst.listGranjas();
            return lst;
        }

        public List<Granja> buscarVarGranjas(string var)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Granja> lst = inst.buscarVarGranjas(var);
            return lst;
        }

        public Granja buscarGranja(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Granja granja = inst.buscarGranja(id);
            return granja;

        }

        public bool altaGranja(Granja granja)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaGranja(granja))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaGranja(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaGranja(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modGranja(Granja granja)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
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
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.listIdProductos();
            return lst;

        }

        public List<Producto> listProductos()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.listProductos();
            return lst;
        }

        public List<Producto> buscarVarProductos(string var)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.buscarVarProductos(var);
            return lst;
        }

        public Producto buscarProducto(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Producto producto = inst.buscarProducto(id);
            return producto;

        }

        public bool altaProducto(Producto producto)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaProducto(producto))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProducto(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaProducto(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProducto(Producto producto)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
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

        #region Fertilizantes

        public List<Fertilizante> listIdFert()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Fertilizante> lst = inst.listIdFert();
            return lst;

        }

        public List<Fertilizante> lstFerti()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Fertilizante> lst = inst.lstFerti();
            return lst;
        }

        public List<Fertilizante> buscarVarFerti(string var)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Fertilizante> lst = inst.buscarVarFerti(var);
            return lst;
        }

        public Fertilizante buscarFerti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Fertilizante fertilizante = inst.buscarFerti(id);
            return fertilizante;

        }

        public bool altaFerti(Fertilizante fertilizante)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaFerti(fertilizante))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaFerti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaFerti(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modFerti(Fertilizante fertilizante)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
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
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Produce> lst = inst.listIdProducen();
            return lst;

        }

        public List<Produce> listProducen()
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Produce> lst = inst.listProducen();
            return lst;
        }

        public List<Produce> buscarVarProducen(string var)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Produce> lst = inst.buscarVarProducen(var);
            return lst;
        }

        public Produce buscarProduce(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Produce produce = inst.buscarProduce(idGranja, idProducto, fchProduccion);
            return produce;

        }

        public bool altaProduce(Produce produce)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaProduce(produce))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProduce(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaProduce(idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProduce(Produce produce)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
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

        /*public bool altaCamion(Camion Camion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.altaCamion(Camion))
            {
                return true;
            }
            else
                return false;
        }*/

    }
}
