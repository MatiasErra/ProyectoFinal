using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Produce
    {
        private int _idGranja;
        private int _idProducto;
        private string _fchProduccion;
        private int _stock;
        private double _precio;
        private int _idDeposito;

        public override string ToString()
        {
            return IdGranja + " - " + IdProducto + " - " + FchProduccion + " - " + Stock + " - " + Precio + " - " + IdDeposito;
        }

        public Produce() { }

        public Produce(int idGranja, int idProducto, string fchProduccion, int stock, double precio, int idDeposito)
        {
            IdGranja = idGranja;
            IdProducto = idProducto;
            FchProduccion = fchProduccion;
            Stock = stock;
            Precio = precio;
            IdDeposito = idDeposito;
        }

        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public int Stock { get => _stock; set => _stock = value; }
        public double Precio { get => _precio; set => _precio = value; }
        public int IdDeposito { get => _idDeposito; set => _idDeposito = value; }
    }
}
