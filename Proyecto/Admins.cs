using System;

public class Admins : Personas
{
	public Admins()
	{
        private string _email;
        private string _contraseña;
        private char _tipoDeAdmin;

        public string Email { get => return _email; set => _email = value; }
        public string Contraseña { get => _contraseña; set => _contraseña = value; }
        public char tipoDeAdmin { get => _tipoDeAdmin; set => _tipoDeAdmin = value; }

        public Admins() { }
}
}
