using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Producto
    {
        private int _idProducto;
        private string _nombre;
        private string _tipo;
        private string _tipoVenta;
        private string _imagen;
        private int _precio;
        private string _cantTotal;
        private string _cantRes;
        private string _cantDisp;


        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string TipoVenta { get => _tipoVenta; set => _tipoVenta = value; }
        public string Imagen { get => _imagen; set => _imagen = value; }
        public string CantTotal { get => _cantTotal; set => _cantTotal = value; }
        public string CantRes { get => _cantRes; set => _cantRes = value; }
        public int Precio { get => _precio; set => _precio = value; }
        public string CantDisp { get => _cantDisp; set => _cantDisp = value; }

        public override string ToString()
        {
            return IdProducto + " - " + Nombre + " - " + Tipo + " - " + TipoVenta + " - " + CantTotal;
        }


        public Producto() { }

        public Producto(int idProducto, string nombre, string tipo, string tipoVenta, string imagen, int precio)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Tipo = tipo;
            TipoVenta = tipoVenta;
            Imagen = imagen;
            Precio = precio;
        }
    }
}
