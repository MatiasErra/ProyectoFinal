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
