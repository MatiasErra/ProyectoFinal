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
        public List<Deposito> listIdDeps()
        {
            List<Deposito> resultado = new List<Deposito>();
            try
            {
                Deposito deposito;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstIdDepositos", connect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deposito = new Deposito();
                        deposito.IdDeposito = int.Parse(reader["idDeposito"].ToString());


                        resultado.Add(deposito);
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

        public List<Deposito> listDeps()
        {
            List<Deposito> listaDepositos = new List<Deposito>();

            Deposito deposito;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstDepositos", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
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

                            listaDepositos.Add(deposito);
                        }
                    }
                }
                catch (Exception)
                {

                    return listaDepositos;

                }
            }
            return listaDepositos;
        }

        public List<Deposito> buscarVarDeps(string var)
        {
            List<Deposito> resultado = new List<Deposito>();
            try
            {
                Deposito deposito;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarDeposito", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

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
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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

        public bool altaDeps(Deposito deposito)
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

        public bool bajaDeps(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaDeposito", connect);
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

        public bool modDeps(Deposito deposito)
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
