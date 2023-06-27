using System;
using Clases;

namespace Clases
{
    public class Admin : Persona
    {

        private string _email;
        private string _contraseña;
        private char _tipoDeAdmin;

        public string Email { get => _email; set => _email = value; }
        public string Contraseña { get => _contraseña; set => _contraseña = value; }
        public char TipoDeAdmin { get => _tipoDeAdmin; set => _tipoDeAdmin = value; }


        public Admin() { }

    }

}