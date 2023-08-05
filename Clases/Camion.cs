using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Camion
    {
        private int _idCamion;
        private string _marca;
        private string _modelo;
        private double _carga;
        private string _dispoinible;

        public int IdCamion { get => _idCamion; set => _idCamion = value; }
        public string Marca { get => _marca; set => _marca = value; }
        public string Modelo { get => _modelo; set => _modelo = value; }
        public double Carga { get => _carga; set => _carga = value; }
        public string Disponible { get => _dispoinible; set => _dispoinible = value; }

        public Camion() { }

        public Camion(int idCamion, string marca, string modelo, double carga, string disponible)
        {
            IdCamion = idCamion;
            Marca = marca;
            Modelo = modelo;
            Carga = carga;
            Disponible = disponible;
        }

        
    }
}
