using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Lote_Pesti
    {
        private int _idPesticida;
        private int _idGranja;
        private int _idProducto;
        private string _fchProduccion;
        private string _cantidad;

        public Lote_Pesti() { }
        public Lote_Pesti(int idPesticida, int idGranja, int idProducto, string fchProduccion, string cantidad)
        {
            IdPesticida = idPesticida;
            IdGranja = idGranja;
            IdProducto = idProducto;
            FchProduccion = fchProduccion;
            _cantidad = cantidad;
        }

        public int IdPesticida { get => _idPesticida; set => _idPesticida = value; }
        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string FchProduccion { get => _fchProduccion; set => _fchProduccion = value; }
        public string Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}

