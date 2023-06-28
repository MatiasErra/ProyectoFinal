using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Manejan
    {
        private int _idCamion;
        private int _idCamionero;

    


        public Manejan() { }

        public Manejan(int idCamion, int idCamionero)
        {
            IdCamion = idCamion;
            IdCamionero = idCamionero;
        }

        public int IdCamion { get => _idCamion; set => _idCamion = value; }
        public int IdCamionero { get => _idCamionero; set => _idCamionero = value; }
    }
}
