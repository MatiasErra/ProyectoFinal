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

        public List<Pesticida> listIdPesti()
        {
            List<Pesticida> resultado = new List<Pesticida>();
            try
            {
                Pesticida pesticida;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstIdPesti", connect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pesticida = new Pesticida();
                        pesticida.IdPesticida = int.Parse(reader["idPesti"].ToString());


                        resultado.Add(pesticida);
                    }
                }

                connect.Close();
            }
            catch (Exception)
            {
                return resultado;
            }
            return resultado;
        }

        public List<Pesticida> lstPesti()
        {
            List<Pesticida> pesticidas = new List<Pesticida>();

            Pesticida pesticida;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstPesti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pesticida = new Pesticida();
                            pesticida.IdPesticida = int.Parse(reader["idPesti"].ToString());
                            pesticida.Nombre = reader["nombre"].ToString();
                            pesticida.Tipo = reader["tipo"].ToString();
                            pesticida.PH = short.Parse(reader["pH"].ToString());
                            pesticida.Impacto = reader["impacto"].ToString();

                            pesticidas.Add(pesticida);
                        }
                    }
                }
                catch (Exception)
                {
                    return pesticidas;

                }
            }
            return pesticidas;
        }

        public List<Pesticida> buscarVarPesti(string var)
        {
            List<Pesticida> resultado = new List<Pesticida>();
            try
            {
                Pesticida pesticida;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarPesti", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pesticida = new Pesticida();
                        pesticida.IdPesticida = int.Parse(reader["idPesti"].ToString());
                        pesticida.Nombre = reader["nombre"].ToString();
                        pesticida.Tipo = reader["tipo"].ToString();
                        pesticida.PH = short.Parse(reader["pH"].ToString());
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
                            pesticida.PH = short.Parse(reader["pH"].ToString());
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

