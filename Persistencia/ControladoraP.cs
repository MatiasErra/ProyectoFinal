using Clases;


namespace Persistencia
{
    public class ControladoraP
    {

        private static ControladoraP _instancia;
        
        public static ControladoraP obtenerInstancia()
        {
            if( _instancia == null )
            {
                _instancia = new ControladoraP();
            }
            return _instancia;
        }

        public bool altaAdmin(Admin admin)
        {
            return new pAdmin().altaAdmin(admin);
        }
    }
}
