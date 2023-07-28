
using System;
using Clases;

namespace Clases
{

	public class Cliente : Persona
	{
		private string _user;
		private string _contrasena;
		private string _direccion;

		public string Contrasena { get => _contrasena; set => _contrasena = value; }
		public string Direccion { get => _direccion; set => _direccion = value; }
        public string User { get => _user; set => _user = value; }


        public override string ToString()
        {
            return IdPersona + " - " + Nombre + " - " + Apellido + " - " + Email + " - " + Telefono + " - " + DateTime.Parse(FchNacimiento).ToString("dd-MM-yyyy") + " - " + User + " - " + Direccion.ToString();
        }


        public Cliente() { }

        public Cliente(int idPersona, string nombre, string apellido,string email, string telefono, string fchNacimiento, string user, string contraseña, string direccion) 
            : base(idPersona, nombre, apellido, email, telefono, fchNacimiento)
        {
            User = user;
            Email = email;
            Contrasena = contraseña;
            Direccion = direccion;
        }
    }
}
