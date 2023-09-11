using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pedido_Prod
    {

        private int _idPedido;
        private int _idProducto;
        private string _cantidad;
   

        public int IdPedido { get => _idPedido; set => _idPedido = value; }

        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string Cantidad { get => _cantidad; set => _cantidad = value; }


        public Pedido_Prod()
        { }
        public Pedido_Prod(int idPedido,  int idProducto, string cantidad)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
 
        }
    }
}
