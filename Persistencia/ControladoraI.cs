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


        /*public bool altaCamion(Camion camion)
        {
            return new pCamion().altaCamion(camion);
        }*/
    }
}
