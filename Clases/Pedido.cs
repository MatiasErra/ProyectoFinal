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

        private string _fechaPedido;
        private string _fechaEntre;
        private string _fechaEspe;
      
        private double _costo;
        private string _informacionEnvio;
        private string _nombreCli;

 


        public Pedido(int idPedido, int idCliente, string estado, string fechaEntre, string fechaEspe, double costo, string informacionEnvio)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            Estado = estado;
            FechaEntre = fechaEntre;
            FechaEspe = fechaEspe;
  
            Costo = costo;
            InformacionEnvio = informacionEnvio;
          
        }

        public Pedido() { }

        public int IdPedido { get => _idPedido; set => _idPedido = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string FechaEntre { get => _fechaEntre; set => _fechaEntre = value; }
        public string FechaEspe { get => _fechaEspe; set => _fechaEspe = value; }
    
        public double Costo { get => _costo; set => _costo = value; }
        public string InformacionEnvio { get => _informacionEnvio; set => _informacionEnvio = value; }
        public string FechaPedido { get => _fechaPedido; set => _fechaPedido = value; }
        public string NombreCli { get => _nombreCli; set => _nombreCli = value; }
    }
}
