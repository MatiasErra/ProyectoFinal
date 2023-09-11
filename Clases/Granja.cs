using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Granja
    {
        private int _idGranja;
        private string _nombre;
        private string _ubicacion;
        private int _idCliente;
        private string _nombreCliente;

        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public string NombreCliente { get => _nombreCliente; set => _nombreCliente = value; }

        public override string ToString()
        {
            return IdGranja + " - " + Nombre + " - " + Ubicacion + " - " + IdCliente;
        }

        public Granja() { }

        public Granja(int idGranja, string nombre, string ubicacion, int idCliente)
        {
            IdGranja = idGranja;
            Nombre = nombre;
            Ubicacion = ubicacion;
            IdCliente = idCliente;
        }
    }
}
