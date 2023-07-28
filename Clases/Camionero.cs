using Clases;
using System;

namespace Clases
{
    public class Camionero : Persona
    {

        private string _cedula;
        private string _disponible;
        private string _fchManejo;

        public string Cedula { get => _cedula; set => _cedula = value; }
        public string Disponible { get => _disponible; set => _disponible = value; }
        public string FchManejo { get => _fchManejo; set => _fchManejo = value; }

        public override string ToString()
        {
            return IdPersona + " - " + Nombre + " - " + Apellido + " - " + Email + " - " + Telefono + " - " + DateTime.Parse(FchNacimiento).ToString("dd-MM-yyyy") + " - " + Cedula + " - " + Disponible + " - " + DateTime.Parse(FchManejo).ToString("dd-MM-yyyy");
        }

        public Camionero(int idPersona, string nombre, string apellido, string email, string telefono, string fchNacimiento, string cedula,
            string disponible, string fchManejo )
            : base(idPersona, nombre, apellido, email, telefono, fchNacimiento)
        {
            Cedula = cedula;
            Disponible = disponible;
            FchManejo = fchManejo;
        }

        public Camionero()
        {

        }
    }

}