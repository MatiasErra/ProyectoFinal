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
        private string _toxicidad;
        private string _resistencia;
        private string _impacto;

        public int IdPesticida { get => _idPesticida; set => _idPesticida = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Toxicidad { get => _toxicidad; set => _toxicidad = value; }
        public string Resistencia { get => _resistencia; set => _resistencia = value; }
        public string Impacto { get => _impacto; set => _impacto = value; }

        public Pesticida() { }

        public Pesticida(int idPesticida, string nombre, string tipo, string toxicidad, string resistencia, string impacto)
        {
            IdPesticida = idPesticida;
            Nombre = nombre;
            Tipo = tipo;
            Toxicidad = toxicidad;
            Resistencia = resistencia;
            Impacto = impacto;
        }
    }
}
