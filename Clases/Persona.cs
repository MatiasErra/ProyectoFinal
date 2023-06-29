using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clases { 
	public abstract class Persona
	{
		private int _idUsuario;
		private string _nombre;
		private string _apellido;
		private string _telefono;
		private DateTime _fchNacimiento;

		public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
		public string Nombre { get => _nombre; set => _nombre = value; }
		public string Apellido { get => _apellido; set => _apellido = value; }
		public string Telefono { get => _telefono; set => _telefono = value; }
		public DateTime FchNacimiento { get => _fchNacimiento; set => _fchNacimiento = value; }

        public Persona() { }

        public Persona(int idUsuario, string nombre, string apellido, string telefono, DateTime fchaNacimiento)
		{
			IdUsuario = idUsuario;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            FchNacimiento = fchaNacimiento;
		}



	
	}
}


