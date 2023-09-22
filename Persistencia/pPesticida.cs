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
    internal class pPesticida
    {



        public List<Pesticida> buscarPesticidaFiltro(Pesticida pPesticida, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            List<Pesticida> resultado = new List<Pesticida>();
            try
            {
                Pesticida pesticida;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarPesticidaFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", pPesticida.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", pPesticida.Tipo));
                cmd.Parameters.Add(new SqlParameter("@impacto", pPesticida.Impacto));
                cmd.Parameters.Add(new SqlParameter("@phMenor", phMenor));
                cmd.Parameters.Add(new SqlParameter("@phMayor", phMayor));
                cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pesticida = new Pesticida();
                        pesticida.IdPesticida = int.Parse(reader["idPesti"].ToString());
                        pesticida.Nombre = reader["nombre"].ToString();
                        pesticida.Tipo = reader["tipo"].ToString();
                        pesticida.PH = double.Parse(reader["pH"].ToString());
                        pesticida.Impacto = reader["impacto"].ToString();

                        resultado.Add(pesticida);
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
        public Pesticida buscarPesti(int id)
        {
            Pesticida pesticida = new Pesticida();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarPesti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pesticida.IdPesticida = int.Parse(reader["idPesti"].ToString());
                            pesticida.Nombre = reader["nombre"].ToString();
                            pesticida.Tipo = reader["tipo"].ToString();
                            pesticida.PH = double.Parse(reader["pH"].ToString());
                            pesticida.Impacto = reader["impacto"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return pesticida;

                }
            }
            return pesticida;
        }

        public bool altaPesti(Pesticida pesticida)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("altaPesti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", pesticida.IdPesticida));
                cmd.Parameters.Add(new SqlParameter("@nombre", pesticida.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", pesticida.Tipo));
                cmd.Parameters.Add(new SqlParameter("@pH", pesticida.PH));
                cmd.Parameters.Add(new SqlParameter("@impacto", pesticida.Impacto));


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
            catch (Exception )
            {
                resultado = false;
                return resultado;
            }
            return resultado;
        }

        public bool bajaPesti(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaPesti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));

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
                resultado= false;
                return resultado;
            }

            return resultado;

        }

        public bool modPesti(Pesticida pesticida)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarPesti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", pesticida.IdPesticida));
                cmd.Parameters.Add(new SqlParameter("@nombre", pesticida.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", pesticida.Tipo));
                cmd.Parameters.Add(new SqlParameter("@pH", pesticida.PH));
                cmd.Parameters.Add(new SqlParameter("@impacto", pesticida.Impacto));

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
                resultado=false;
                return resultado;

            }

            return resultado;

        }

    }
}

