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
    class pViaje
    {
        public List<Viaje> buscarViajeFiltro(Viaje pViaje, int costoMenor, int costoMayor, string fechaMenor, string fechaMayor, string ordenar)
        {
            List<Viaje> resultado = new List<Viaje>();
            try
            {
                Viaje viaje;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarViajeFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@camion", pViaje.IdCamion));
                cmd.Parameters.Add(new SqlParameter("@camionero", pViaje.IdCamionero));
                cmd.Parameters.Add(new SqlParameter("@estado", pViaje.Estado));
                cmd.Parameters.Add(new SqlParameter("@costoMenor", costoMenor));
                cmd.Parameters.Add(new SqlParameter("@costoMayor", costoMayor));
                cmd.Parameters.Add(new SqlParameter("@fechaMenor", fechaMenor));
                cmd.Parameters.Add(new SqlParameter("@fechaMayor", fechaMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        viaje = new Viaje();
                        viaje.IdViaje = int.Parse(reader["idViaje"].ToString());
                        viaje.Costo = int.Parse(reader["costo"].ToString());
                        string[] DateArr = reader["fecha"].ToString().Split(' ');
                        viaje.Fecha = DateArr[0];
                        viaje.IdCamion = int.Parse(reader["idCamion"].ToString());
                        viaje.MarcaCamion = reader["marcaCamion"].ToString();
                        viaje.ModeloCamion = reader["modeloCamion"].ToString();
                        viaje.IdCamionero = int.Parse(reader["idCamionero"].ToString());
                        viaje.NombreCamionero = reader["nombreCamionero"].ToString();
                        viaje.Estado = reader["estado"].ToString();


                        resultado.Add(viaje);
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

        public Viaje buscarViaje(int id)
        {
            Viaje viaje = new Viaje();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarViaje", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viaje.IdViaje = int.Parse(reader["idViaje"].ToString());
                            viaje.Costo = int.Parse(reader["costo"].ToString());
                            string[] DateArr = reader["fecha"].ToString().Split(' ');
                            viaje.Fecha = DateArr[0];
                            viaje.IdCamion = int.Parse(reader["idCamion"].ToString());
                            viaje.MarcaCamion = reader["marcaCamion"].ToString();
                            viaje.ModeloCamion = reader["modeloCamion"].ToString();
                            viaje.IdCamionero = int.Parse(reader["idCamionero"].ToString());
                            viaje.NombreCamionero = reader["nombreCamionero"].ToString();
                            viaje.Estado = reader["estado"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return viaje;

                }
            }
            return viaje;
        }

        public bool altaViaje(Viaje viaje)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaViaje", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", viaje.IdViaje));
                cmd.Parameters.Add(new SqlParameter("@costo", viaje.Costo));
                cmd.Parameters.Add(new SqlParameter("@fecha", viaje.Fecha));
                cmd.Parameters.Add(new SqlParameter("@camion", viaje.IdCamion));
                cmd.Parameters.Add(new SqlParameter("@camionero", viaje.IdCamionero));
                cmd.Parameters.Add(new SqlParameter("@estado", viaje.Estado));


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

        public bool bajaViaje(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaViaje", connect);
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

        public bool modViaje(Viaje viaje)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarViaje", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", viaje.IdViaje));
                cmd.Parameters.Add(new SqlParameter("@costo", viaje.Costo));
                cmd.Parameters.Add(new SqlParameter("@fecha", viaje.Fecha));
                cmd.Parameters.Add(new SqlParameter("@camion", viaje.IdCamion));
                cmd.Parameters.Add(new SqlParameter("@camionero", viaje.IdCamionero));
                cmd.Parameters.Add(new SqlParameter("@estado", viaje.Estado));

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
