using Clases;
using System;

namespace Clases
{
    public class Camionero : Persona
    {

        private string _cedula;

        public string Cedula { get => _cedula; set => _cedula = value; }

        public override string ToString()
        {
            return this.IdPersona + " " + this.Nombre + " " + this.Apellido + " " + this.Email + " " + this.Telefono + " " + this.FchNacimiento + " " + this.Cedula;
        }

        public Camionero(int idPersona, string nombre, string apellido, string email, string telefono, string fchNacimiento, string cedula)
            : base(idPersona, nombre, apellido, email, telefono, fchNacimiento)
        {
            Cedula = cedula;
        }

        public Camionero()
        {

        }
    }

}