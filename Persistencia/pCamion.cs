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
    class pCamion
    {


        public List<Camion> buscarFiltroCam(Camion pCamion, double cargaMenor, double cargaMayor, string ordenar)
        {
            List<Camion> resultado = new List<Camion>();
            try
            {
                Camion camion;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarFiltroCam", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@marca", pCamion.Marca));
                cmd.Parameters.Add(new SqlParameter("@modelo", pCamion.Modelo));
                cmd.Parameters.Add(new SqlParameter("@cargaMenor", cargaMenor));
                cmd.Parameters.Add(new SqlParameter("@cargaMayor", cargaMayor));
                cmd.Parameters.Add(new SqlParameter("@disponible", pCamion.Disponible));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        camion = new Camion();
                        camion.IdCamion = int.Parse(reader["idCamion"].ToString());
                        camion.Marca = reader["marca"].ToString();
                        camion.Modelo = reader["modelo"].ToString();
                        camion.Carga = double.Parse(reader["carga"].ToString());
                        camion.Disponible = reader["disponible"].ToString();

                        resultado.Add(camion);
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



        public Camion buscarCam(int id)
        {
            Camion camion = new Camion();


            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarCam", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            camion.IdCamion = int.Parse(reader["idCamion"].ToString());
                            camion.Marca = reader["marca"].ToString();
                            camion.Modelo = reader["modelo"].ToString();
                            camion.Carga = double.Parse(reader["carga"].ToString());
                            camion.Disponible = reader["disponible"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return camion;

                }
            }
            return camion;
        }

        public bool altaCam(Camion camion)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaCam", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", camion.IdCamion));
                cmd.Parameters.Add(new SqlParameter("@marca", camion.Marca));
                cmd.Parameters.Add(new SqlParameter("@modelo", camion.Modelo));
                cmd.Parameters.Add(new SqlParameter("@carga", camion.Carga));
                cmd.Parameters.Add(new SqlParameter("@disponible", camion.Disponible));


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
                resultado =false;
                return resultado;
            }
            return resultado;
        }

        public bool bajaCam(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaCam", connect);
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
                resultado=false;
                return resultado;
            }

            return resultado;

        }

        public bool modCam(Camion camion)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarCam", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", camion.IdCamion));
                cmd.Parameters.Add(new SqlParameter("@marca", camion.Marca));
                cmd.Parameters.Add(new SqlParameter("@modelo", camion.Modelo));
                cmd.Parameters.Add(new SqlParameter("@carga", camion.Carga));
                cmd.Parameters.Add(new SqlParameter("@disponible", camion.Disponible));

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
