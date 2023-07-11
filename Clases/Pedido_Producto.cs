using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    internal class Pedido_Producto
    {
        private int _idPedido;
        private int _idProducto;
        private int _idGrnja;

        public Pedido_Producto(int idPedido, int idProducto, int idGrnja)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            IdGrnja = idGrnja;
        }

        public int IdPedido { get => _idPedido; set => _idPedido = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public int IdGrnja { get => _idGrnja; set => _idGrnja = value; }
    }
}
