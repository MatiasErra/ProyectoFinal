using System;
using Clases;

namespace Clases
{
    public class Admin : Persona
    {


        private string _user;
        private string _contrasena;
        private string _tipoDeAdmin;

      
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string TipoDeAdmin { get => _tipoDeAdmin; set => _tipoDeAdmin = value; }
        public string User { get => _user; set => _user = value; }


        public override string ToString()
        {
            return IdPersona + " - " + Nombre + " - " + Apellido +" - " + TipoDeAdmin + " - " + User.ToString();
        }


        public Admin() { }

        public Admin(int idPersona, string nombre, string apellido, string email, string telefono, string fchNacimiento, string user, string contrasena, string tipoDeAdmin )
            : base(idPersona, nombre, apellido,email ,telefono, fchNacimiento)
        {
           Contrasena = contrasena;
            TipoDeAdmin = tipoDeAdmin;
            User = user;
        }
    }

}