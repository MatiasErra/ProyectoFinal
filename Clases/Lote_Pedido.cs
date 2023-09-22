using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Lote_Pedido
    {
        private int _idPedido;
        private int _idProducto;
        private int _idGranja;
        private string _fchProduccion;
        private string _cant;

        public Lote_Pedido(int idPedido, int idProducto, int idGrnaja, string fchProduccion, string cant)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            IdGranja = idGrnaja;
            FchProduccion = fchProduccion;
            Cant = cant;
        }
        public Lote_Pedido()
        { }

        public int IdPedido { get => _idPedido; set => _idPedido = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public string Cant { get => _cant; set => _cant = value; }
    }
}
