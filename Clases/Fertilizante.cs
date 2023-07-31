using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Fertilizante
    {
        private int _idFertilizante;
        private string _nombre;
        private string _tipo;
        private short _pH;
        private string _impacto;

        public int IdFertilizante { get => _idFertilizante; set => _idFertilizante = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public short PH { get => _pH; set => _pH = value; }
        public string Impacto { get => _impacto; set => _impacto = value; }


        public override string ToString()
        {
            return IdFertilizante + " - " + Nombre + " - " + Tipo + " - " + PH + " - " + Impacto;
        }

        public Fertilizante () { }

        public Fertilizante(int idFertilizante, string nombre, string tipo, short pH, string impacto)
        {
            IdFertilizante = idFertilizante;
            Nombre = nombre;
            Tipo = tipo;
            PH = pH;
            Impacto = impacto;
        }
    }
}
