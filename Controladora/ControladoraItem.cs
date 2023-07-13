using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    internal class ControladoraItem
    {
        private static ControladoraItem _instancia;

        public static ControladoraItem obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraItem();

            }  
            return _instancia;

        }

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
