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
    public class pFertilizante
    {



        public List<Fertilizante> buscarFertilizanteFiltro(Fertilizante pFertilizante, double phMenor, double phMayor, string ordenar, int idGranja, int idProducto, string fchProduccion)
        {
            List<Fertilizante> resultado = new List<Fertilizante>();
            try
            {
                Fertilizante fertilizante;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarFertilizanteFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", pFertilizante.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", pFertilizante.Tipo));
                cmd.Parameters.Add(new SqlParameter("@phMenor", phMenor));
                cmd.Parameters.Add(new SqlParameter("@phMayor", phMayor));
                cmd.Parameters.Add(new SqlParameter("@impacto", pFertilizante.Impacto));
                cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fertilizante = new Fertilizante();
                        fertilizante.IdFertilizante = int.Parse(reader["idFerti"].ToString());
                        fertilizante.Nombre = reader["nombre"].ToString();
                        fertilizante.Tipo = reader["tipo"].ToString();
                        fertilizante.PH = double.Parse(reader["pH"].ToString());
                        fertilizante.Impacto = reader["impacto"].ToString();

                        resultado.Add(fertilizante);
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

        public Fertilizante buscarFerti(int id)
        {
            Fertilizante fertilizante = new Fertilizante();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarFerti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fertilizante.IdFertilizante = int.Parse(reader["idFerti"].ToString());
                            fertilizante.Nombre = reader["nombre"].ToString();
                            fertilizante.Tipo = reader["tipo"].ToString();
                            fertilizante.PH = double.Parse(reader["pH"].ToString());
                            fertilizante.Impacto = reader["impacto"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return fertilizante;

                }
            }
            return fertilizante;
        }

        public bool altaFerti(Fertilizante fertilizante)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaFerti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", fertilizante.IdFertilizante));
                cmd.Parameters.Add(new SqlParameter("@nombre", fertilizante.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", fertilizante.Tipo));
                cmd.Parameters.Add(new SqlParameter("@pH", fertilizante.PH));
                cmd.Parameters.Add(new SqlParameter("@impacto", fertilizante.Impacto));


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

        public bool bajaFerti(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaFerti", connect);
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
                resultado = false;
                return resultado;
            }

            return resultado;

        }

        public bool modFerti(Fertilizante fertilizante)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarFerti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", fertilizante.IdFertilizante));
                cmd.Parameters.Add(new SqlParameter("@nombre", fertilizante.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", fertilizante.Tipo));
                cmd.Parameters.Add(new SqlParameter("@pH", fertilizante.PH));
                cmd.Parameters.Add(new SqlParameter("@impacto", fertilizante.Impacto));

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
