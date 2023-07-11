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
        private int _idCliente;

        public int IdGranja { get => _idGranja; set => _idGranja = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }

        public Granja() { }

        public Granja(int idGranja, string nombre, int idCliente)
        {
            IdGranja = idGranja;
            Nombre = nombre;
            IdCliente = idCliente;
        }
    }
}
