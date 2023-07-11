
using System;
using Clases;

namespace Clases
{

	public class Cliente : Persona
	{
		private string _user;
		private string _contraseña;
		private string _direccion;

		public string Contraseña { get => _contraseña; set => _contraseña = value; }
		public string Direccion { get => _direccion; set => _direccion = value; }
        public string User { get => _user; set => _user = value; }
   

        public Cliente() { }

        public Cliente(int idPersona, string nombre, string apellido,string email, string telefono, string fchNacimiento, string user, string contraseña, string direccion) 
            : base(idPersona, nombre, apellido, email, telefono, fchNacimiento)
        {
            User = user;
            Email = email;
            Contraseña = contraseña;
            Direccion = direccion;
        }
    }
}
