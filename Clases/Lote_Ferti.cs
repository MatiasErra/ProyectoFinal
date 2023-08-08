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
    }
}
