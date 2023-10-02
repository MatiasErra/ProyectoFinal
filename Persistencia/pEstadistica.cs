using Clases;
using persistenciaDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    class pEstadistica
    {
        public List<Auditoria> buscarAuditoriaFiltro(Auditoria pAuditoria, string fchMenor, string fchMayor, string ordenar)
        {
            List<Auditoria> resultado = new List<Auditoria>();
            try
            {
                Auditoria auditoria;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarAuditoriaFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@admin", pAuditoria.IdAdmin));
                cmd.Parameters.Add(new SqlParameter("@tabla", pAuditoria.Tabla));
                cmd.Parameters.Add(new SqlParameter("@tipo", pAuditoria.Tipo));
                cmd.Parameters.Add(new SqlParameter("@fchMenor", fchMenor));
                cmd.Parameters.Add(new SqlParameter("@fchMayor", fchMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        auditoria = new Auditoria();
                        auditoria.IdAuditoria = int.Parse(reader["idAuditoria"].ToString());
                        auditoria.IdAdmin = int.Parse(reader["idAdmin"].ToString());
                        auditoria.NombreAdmin = reader["nombreAdmin"].ToString();
                        auditoria.ApellidoAdmin = reader["apellidoAdmin"].ToString();
                        auditoria.Fecha = reader["fecha"].ToString().Split(' ')[0];
                        auditoria.Tabla = reader["tabla"].ToString();
                        auditoria.Tipo = reader["tipo"].ToString();

                        resultado.Add(auditoria);
                    }
                }

                conect.Close();
            }
            catch (Exception)
            {
                return resultado;
            }
            return resultado;
        }
    }
}
