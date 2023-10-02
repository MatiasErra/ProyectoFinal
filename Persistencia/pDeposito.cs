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
    class pDeposito
    {




        public List<Deposito> buscarDepositoFiltro(Deposito pDeposito, int capacidadMenor, int capacidadMayor, int temperaturaMenor, int temperaturaMayor, string ordenar)
        {
            List<Deposito> resultado = new List<Deposito>();
            try
            {
                Deposito deposito;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarDepositoFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ubicacion", pDeposito.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@condiciones", pDeposito.Condiciones));
                cmd.Parameters.Add(new SqlParameter("@capacidadMenor", capacidadMenor));
                cmd.Parameters.Add(new SqlParameter("@capacidadMayor", capacidadMayor));
                cmd.Parameters.Add(new SqlParameter("@temperaturaMenor", temperaturaMenor));
                cmd.Parameters.Add(new SqlParameter("@temperaturaMayor", temperaturaMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deposito = new Deposito();
                        deposito.IdDeposito = int.Parse(reader["idDeposito"].ToString());
                        deposito.Capacidad = reader["capacidad"].ToString();
                        deposito.Ubicacion = reader["ubicacion"].ToString();
                        deposito.Temperatura = short.Parse(reader["temperatura"].ToString());
                        deposito.Condiciones = reader["condiciones"].ToString();

                        resultado.Add(deposito);
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


        public Deposito buscarDeps(int id)
        {
            Deposito deposito = new Deposito();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarDeposito", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            deposito.IdDeposito = int.Parse(reader["idDeposito"].ToString());
                            deposito.Capacidad = reader["capacidad"].ToString();
                            deposito.Ubicacion = reader["ubicacion"].ToString();
                            deposito.Temperatura = short.Parse(reader["temperatura"].ToString());
                            deposito.Condiciones = reader["condiciones"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return deposito;

                }
            }
            return deposito;
        }

        public bool altaDeps(Deposito deposito, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaDeposito", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", deposito.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@capacidad", deposito.Capacidad));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", deposito.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@temperatura", deposito.Temperatura));
                cmd.Parameters.Add(new SqlParameter("@condiciones", deposito.Condiciones));
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
                resultado=false;
                return resultado;
            }
            return resultado;
        }

        public bool bajaDeps(int id, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaDeposito", connect);
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
                resultado=false;
                return resultado;
            }

            return resultado;

        }

        public bool modDeps(Deposito deposito, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarDeposito", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", deposito.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@capacidad", deposito.Capacidad));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", deposito.Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@temperatura", deposito.Temperatura));
                cmd.Parameters.Add(new SqlParameter("@condiciones", deposito.Condiciones));
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
                resultado=false;
                return resultado;

            }

            return resultado;

        }
    }
}
