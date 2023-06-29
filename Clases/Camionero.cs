using Clases;
using System;

namespace Clases { 
    public class Camionero : Persona
    {
	
		private string _cedula;

        public string Cedula { get => _cedula; set => _cedula = value; }

        public Camionero(int idUsuario, string nombre, string apellido, string telefono, DateTime fchNacimiento, string cedula) : base(idUsuario, nombre, apellido, telefono, fchNacimiento)
        {
            Cedula = cedula;
        }
    }

}