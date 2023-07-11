using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pedido
    {
        private int _idPedido;
        private int _idCliente;
        private string _estado;
        private DateTime _fechaEntre;
        private DateTime _fechaEspe;
        private string _retraso;
        private double _costo;
        private string _informacionEnvio;
        private int _idCamion;
        private int _idCamionero;

        public Pedido(int idPedido, int idCliente, string estado, DateTime fechaEntre, DateTime fechaEspe, string retraso, double costo, string informacionEnvio, int idCamion, int idCamionero)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            Estado = estado;
            FechaEntre = fechaEntre;
            FechaEspe = fechaEspe;
            Retraso = retraso;
            Costo = costo;
            InformacionEnvio = informacionEnvio;
            IdCamion = idCamion;
            IdCamionero = idCamionero;
        }

        public int IdPedido { get => _idPedido; set => _idPedido = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaEntre { get => _fechaEntre; set => _fechaEntre = value; }
        public DateTime FechaEspe { get => _fechaEspe; set => _fechaEspe = value; }
        public string Retraso { get => _retraso; set => _retraso = value; }
        public double Costo { get => _costo; set => _costo = value; }
        public string InformacionEnvio { get => _informacionEnvio; set => _informacionEnvio = value; }
        public int IdCamion { get => _idCamion; set => _idCamion = value; }
        public int IdCamionero { get => _idCamionero; set => _idCamionero = value; }
    }
}
