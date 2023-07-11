using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clases { 
	public abstract class Persona
	{
		private int _idPersona;
		private string _nombre;
		private string _apellido;
        private string _email;
        private string _telefono;
		private string _fchNacimiento;

		public int IdPersona { get => _idPersona; set => _idPersona = value; }
		public string Nombre { get => _nombre; set => _nombre = value; }
		public string Apellido { get => _apellido; set => _apellido = value; }
		public string Telefono { get => _telefono; set => _telefono = value; }
		public string FchNacimiento { get => _fchNacimiento; set => _fchNacimiento = value; }
        public string Email { get => _email; set => _email = value; }

        public Persona() { }

        public Persona(int idPersona, string nombre, string apellido, string email , string telefono, string fchaNacimiento)
        {
            IdPersona = idPersona;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            FchNacimiento = fchaNacimiento;
            Email = email;
        }




    }
}


