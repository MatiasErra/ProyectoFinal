
using System;
using Clases;

namespace Clases
{

	public class Cliente : Persona
	{
		private string _email;
		private string _contraseña;
		private string _direccion;

		public string Email { get => _email; set => _email = value; }
		public string Contraseña { get => _contraseña; set => _contraseña = value; }
		public string Direccion { get => _direccion; set => _direccion = value; }

		public Cliente() { }
	}
}
