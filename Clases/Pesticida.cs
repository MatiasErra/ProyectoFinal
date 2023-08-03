using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pesticida
    {
        private int _idPesticida;
        private string _nombre;
        private string _tipo;
        private short _pH;       
        private string _impacto;

        public int IdPesticida { get => _idPesticida; set => _idPesticida = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
     
        public string Impacto { get => _impacto; set => _impacto = value; }
        public short PH { get => _pH; set => _pH = value; }

        public Pesticida() { }

        public Pesticida(int idPesticida, string nombre, string tipo, short ph, string impacto)
        {
            IdPesticida = idPesticida;
            Nombre = nombre;
            Tipo = tipo;
            PH = ph;
            Impacto = impacto;
        }
    }
}
