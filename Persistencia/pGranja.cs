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
    class pGranja
    {



        public List<Granja> buscarGranjaFiltro(Granja pGranja, string ordenar)
        {
            List<Granja> resultado = new List<Granja>();
            try
            {
                Granja granja;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarGranjaFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", pGranja.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", pGranja.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@idCli", pGranja.IdCliente));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        granja = new Granja();
                        granja.IdGranja = int.Parse(reader["idGranja"].ToString());
                        granja.Nombre = reader["nombre"].ToString();
                        granja.Ubicacion = reader["ubicacion"].ToString();
                        granja.IdCliente = int.Parse(reader["idCliente"].ToString());
                        granja.NombreCliente = reader["nombreCliente"].ToString();

                        resultado.Add(granja);
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

        public Granja buscarGranja(int id)
        {
            Granja granja = new Granja();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarGranja", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            granja.IdGranja = int.Parse(reader["idGranja"].ToString());
                            granja.Nombre = reader["nombre"].ToString();
                            granja.Ubicacion = reader["ubicacion"].ToString();
                            granja.IdCliente = int.Parse(reader["idCliente"].ToString());
                        }
                    }
                }
                catch (Exception)
                {

                    return granja;

                }
            }
            return granja;
        }

        public bool altaGranja(Granja granja, int idAdmin)    
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaGranja", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", granja.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@nombre", granja.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", granja.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@idCliente", granja.IdCliente));
                cmd.Parameters.Add(new SqlParameter("@idAdmin", idAdmin));


                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {
                    resultado = true;
                }
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                resultado = false;
                return resultado;
            }
            return resultado;
        }

        public bool bajaGranja(int id, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaGranja", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@idAdmin", idAdmin));

                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {
                    resultado = true;
                }
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                    resultado = true;

                }

            }
            catch (Exception)
            {
                resultado = false;
                return resultado;
            }

            return resultado;

        }

        public bool modGranja(Granja granja, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarGranja", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", granja.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@nombre", granja.Nombre));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", granja.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@idCliente", granja.IdCliente));
                cmd.Parameters.Add(new SqlParameter("@idAdmin", idAdmin));

                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {
                    resultado = true;
                }
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                    resultado = true;

                }

            }
            catch (Exception)
            {
                resultado = false;
                return resultado;

            }

            return resultado;

        }
    }
}
