using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Auditoria
    {
        private int _idAuditoria;
        private int _idAdmin;
        private string _nombreAdmin;
        private string _apellidoAdmin;
        private string _fecha;
        private string _tabla;
        private string _tipo;

        public Auditoria(int idAuditoria, int idAdmin, string fecha, string tabla, string tipo)
        {
            IdAuditoria = idAuditoria;
            IdAdmin = idAdmin;
            Fecha = fecha;
            Tabla = tabla;
            Tipo = tipo;
        }

        public Auditoria() { }

        public int IdAuditoria { get => _idAuditoria; set => _idAuditoria = value; }
        public int IdAdmin { get => _idAdmin; set => _idAdmin = value; }
        public string NombreAdmin { get => _nombreAdmin; set => _nombreAdmin = value; }
        public string ApellidoAdmin { get => _apellidoAdmin; set => _apellidoAdmin = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public string Tabla { get => _tabla; set => _tabla = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        
    }
}
