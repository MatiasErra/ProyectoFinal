using System;

public class Personas
{
	public Personas()
	{
		private int _idUsuario;
		private string _nombre;
		private string _apellido;
		private string _telefono;
		private DateTime _fchNacimiento;

		public int idUsuario { get => _idUsuario; set => _idUsuario = value; }
		public string nombre { get => _nombre; set => _nombre = value; }
		public string apellido { get => _apellido; set => _apellido = value; }
		public string telefono { get => _telefono; set => _telefono = value; }
		public DateTime fchNacimiento { get => _fchNacimiento; set => _fchNacimiento = value; }

		public Personas () { }

	}
}
