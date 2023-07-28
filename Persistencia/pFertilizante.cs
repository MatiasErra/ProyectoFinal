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
        public List<Fertilizante> listIdFert()
        {
            List<Fertilizante> resultado = new List<Fertilizante>();
            try
            {
                Fertilizante fertilizante;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstIdFerti", connect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fertilizante = new Fertilizante();
                        fertilizante.IdFertilizante = int.Parse(reader["idFerti"].ToString());


                        resultado.Add(fertilizante);
                    }
                }

                connect.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return resultado;
        }

        public List<Fertilizante> lstFerti()
        {
            List<Fertilizante> fertilizantes= new List<Fertilizante>();

            Fertilizante fertilizante;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstFerti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fertilizante = new Fertilizante();
                            fertilizante.IdFertilizante = int.Parse(reader["idFerti"].ToString());
                            fertilizante.Nombre = reader["nombre"].ToString();
                            fertilizante.Origen = reader["origen"].ToString();
                            fertilizante.CompQuimica = reader["compQuimica"].ToString();
                            fertilizante.PH = short.Parse(reader["pH"].ToString());
                            fertilizante.Impacto = reader["impacto"].ToString();

                            fertilizantes.Add(fertilizante);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
            return fertilizantes;
        }

        public List<Fertilizante> buscarVarFerti(string var)
        {
            List<Fertilizante> resultado = new List<Fertilizante>();
            try
            {
                Fertilizante fertilizante;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarFerti", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fertilizante = new Fertilizante();
                        fertilizante.IdFertilizante = int.Parse(reader["idFerti"].ToString());
                        fertilizante.Nombre = reader["nombre"].ToString();
                        fertilizante.Origen = reader["origen"].ToString();
                        fertilizante.CompQuimica = reader["compQuimica"].ToString();
                        fertilizante.PH = short.Parse(reader["pH"].ToString());
                        fertilizante.Impacto = reader["impacto"].ToString();

                        resultado.Add(fertilizante);
                    }
                }

                conect.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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
                            fertilizante.Origen = reader["origen"].ToString();
                            fertilizante.CompQuimica = reader["compQuimica"].ToString();
                            fertilizante.PH = short.Parse(reader["pH"].ToString());
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
                cmd.Parameters.Add(new SqlParameter("@origen", fertilizante.Origen));
                cmd.Parameters.Add(new SqlParameter("@compQuimica", fertilizante.CompQuimica));
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
            catch (Exception ex)
            {
                throw ex;
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
                cmd.Parameters.Add(new SqlParameter("@origen", fertilizante.Origen));
                cmd.Parameters.Add(new SqlParameter("@compQuimica", fertilizante.CompQuimica));
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
                return resultado;

            }

            return resultado;

        }

    }
}
