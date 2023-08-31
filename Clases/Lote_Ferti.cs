using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Lote_Ferti
    {
        private int _idFertilizante;
        private int _idGranja;
        private int _idProducto;
        private string _fchProduccion;
        private string _cantidad;
        private string _nombreFert;
        private string _tipoFert;
        private double _pHFert;
        public Lote_Ferti() { }
        public Lote_Ferti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, string cantidad)
        {
            IdFertilizante = idFertilizante;
            IdGranja = idGranja;
            IdProducto = idProducto;
            FchProduccion = fchProduccion;
            _cantidad = cantidad;
        }

        public int IdFertilizante { get => _idFertilizante; set => _idFertilizante = value; }
        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public string Cantidad { get => _cantidad; set => _cantidad = value; }
        public string NombreFert { get => _nombreFert; set => _nombreFert = value; }
        public string TipoFert { get => _tipoFert; set => _tipoFert = value; }
        public double PHFert { get => _pHFert; set => _pHFert = value; }
    }
}
