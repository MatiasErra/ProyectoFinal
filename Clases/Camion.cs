using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    internal class Camion
    {
        private int _idCamion;
        private string _marca;
        private string _modelo;
        private string _carga;
        private string _dispoinible;

        public int IdCamion { get => _idCamion; set => _idCamion = value; }
        public string Marca { get => _marca; set => _marca = value; }
        public string Modelo { get => _modelo; set => _modelo = value; }
        public string Carga { get => _carga; set => _carga = value; }
        public string Dispoinible { get => _dispoinible; set => _dispoinible = value; }
    }
}
