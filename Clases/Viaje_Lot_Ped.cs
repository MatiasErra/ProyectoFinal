using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Viaje_Lot_Ped
    {
        private int _idViaje;
        private int _idPedido;
        private int _idProducto;
        private int _idGranja;
        private string _nombreGranja;
        private string _nombreProducto;
        private string _fchProduccion;
        private string _cant;

        public int IdViaje { get => _idViaje; set => _idViaje = value; }
        public int IdPedido { get => _idPedido; set => _idPedido = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public string Cant { get => _cant; set => _cant = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public string NombreGranja { get => _nombreGranja; set => _nombreGranja = value; }
        public string NombreProducto { get => _nombreProducto; set => _nombreProducto = value; }

        public Viaje_Lot_Ped() { }
    }
}
