using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Viaje
    {
        private int _idViaje;
        private int _costo;
        private string _fecha;
        private int _idCamion;
        private string _marcaCamion;
        private string _modeloCamion;
        private int _idCamionero;
        private string _nombreCamionero;
        private string _estado;

        public int IdViaje { get => _idViaje; set => _idViaje = value; }
        public int Costo { get => _costo; set => _costo = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public int IdCamion { get => _idCamion; set => _idCamion = value; }
        public string MarcaCamion { get => _marcaCamion; set => _marcaCamion = value; }
        public string ModeloCamion { get => _modeloCamion; set => _modeloCamion = value; }
        public int IdCamionero { get => _idCamionero; set => _idCamionero = value; }
        public string NombreCamionero { get => _nombreCamionero; set => _nombreCamionero = value; }
        public string Estado { get => _estado; set => _estado = value; }

        public Viaje(int idViaje, int costo, string fecha, int idCamion, int idCamionero, string estado)
        {
            IdViaje = idViaje;
            Costo = costo;
            Fecha = fecha;
            IdCamion = idCamion;
            IdCamionero = idCamionero;
            Estado = estado;
        }

        public Viaje() { }

        
    }
}
