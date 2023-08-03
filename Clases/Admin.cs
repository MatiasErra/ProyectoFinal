using System;
using Clases;

namespace Clases
{
    public class Admin : Persona
    {

        private string _estado;
        private string _user;
        private string _contrasena;
        private string _tipoDeAdmin;

      
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string TipoDeAdmin { get => _tipoDeAdmin; set => _tipoDeAdmin = value; }
        public string User { get => _user; set => _user = value; }
        public string Estado { get => _estado; set => _estado = value; }

        public override string ToString()
        {
            return IdPersona + " - " + Nombre + " - " + Apellido +" - " + Email +" - " + Telefono + " - " + DateTime.Parse(FchNacimiento).ToString("dd-MM-yyyy") + " - " + User + " - " + TipoDeAdmin.ToString();
        }


        public Admin() { }

        public Admin(int idPersona, string nombre, string apellido, string email, string telefono, string fchNacimiento, string user, string contrasena, string tipoDeAdmin,string estado)
            : base(idPersona, nombre, apellido,email ,telefono, fchNacimiento)
        {
           Contrasena = contrasena;
            TipoDeAdmin = tipoDeAdmin;
            User = user;
            Estado = estado;
        }
    }

}