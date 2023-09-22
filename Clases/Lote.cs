using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Lote
    {
        private int _idGranja;
        private int _idProducto;
        private string _fchProduccion;
        private string _fchCaducidad;
        private string _cantidad;
        private double _precio;
        private int _idDeposito;
        private string _nombreGranja;
        private string _nombreProducto;
        private string _ubicacionDeps;

        public override string ToString()
        {
            return IdGranja + " - " + IdProducto + " - " + FchProduccion + " - " + Cantidad + " - " + Precio + " - " + IdDeposito;
        }

        public Lote() { }

        public Lote(int idGranja, int idProducto, string fchProduccion, string fchCaducidad, string cantidad, double precio, int idDeposito)
        {
            IdGranja = idGranja;
            IdProducto = idProducto;
            FchProduccion = fchProduccion;
            FchCaducidad = fchCaducidad;
            Cantidad = cantidad;
            Precio = precio;
            IdDeposito = idDeposito;
        }

        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public string FchCaducidad { get => _fchCaducidad; set => _fchCaducidad = value; }
        public string Cantidad { get => _cantidad; set => _cantidad = value; }
        public double Precio { get => _precio; set => _precio = value; }
        public int IdDeposito { get => _idDeposito; set => _idDeposito = value; }
        public string NombreGranja { get => _nombreGranja; set => _nombreGranja = value; }
        public string NombreProducto { get => _nombreProducto; set => _nombreProducto = value; }
        public string UbicacionDeps { get => _ubicacionDeps; set => _ubicacionDeps = value; }
    }
}
