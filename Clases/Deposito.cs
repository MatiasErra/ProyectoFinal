using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    internal class Deposito
    {
        private int _idDeposito;
        private string _capacidad;
        private string _ubicacion;
        private short _temperatura;
        private string _condiciones;

        public Deposito(int idDeposito, string capacidad, string ubicacion, short temperatura, string condiciones)
        {
            IdDeposito = idDeposito;
            Capacidad = capacidad;
            Ubicacion = ubicacion;
            Temperatura = temperatura;
            Condiciones = condiciones;
        }

        public int IdDeposito { get => _idDeposito; set => _idDeposito = value; }
        public string Capacidad { get => _capacidad; set => _capacidad = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public short Temperatura { get => _temperatura; set => _temperatura = value; }
        public string Condiciones { get => _condiciones; set => _condiciones = value; }
    }
}
