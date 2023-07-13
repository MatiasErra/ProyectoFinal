using System;
using Clases;





namespace Controladoras
{ 
	public class Controladora
	{
		private static Controladora _instancia;
		private static ControladoraPersona _controladoraPersona;
		private static ControladoraItem _controladoraItem;
        internal static ControladoraPersona ControladoraPersona { get => _controladoraPersona; set => _controladoraPersona = value; }
        internal static ControladoraItem ControladoraItem { get => _controladoraItem; set => _controladoraItem = value; }

        public static Controladora obtenerInstancia()
		{
			if( _instancia == null )
			{
				_instancia = new Controladora();
				_controladoraPersona = ControladoraPersona.obtenerInstancia();
				_controladoraItem = ControladoraItem.obtenerInstancia();


			}
			return _instancia;

		}


	
	
	}
}
