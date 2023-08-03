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
    class pProduce
    {
        public List<Produce> listIdProducen()
        {
            List<Produce> resultado = new List<Produce>();
            try
            {
                Produce produce;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstIdProducen", connect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produce = new Produce();
                        produce.IdGranja = int.Parse(reader["idGranja"].ToString());
                        produce.IdProducto = int.Parse(reader["idProducto"].ToString());
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        produce.FchProduccion = DateArr[0];


                        resultado.Add(produce);
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

        public List<Produce> listProducen()
        {
            List<Produce> resultado = new List<Produce>();

            Produce produce;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstProducen", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produce = new Produce();
                            produce.IdGranja = int.Parse(reader["idGranja"].ToString());
                            produce.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            produce.FchProduccion = DateArr[0];
                            produce.Stock = int.Parse(reader["stock"].ToString());
                            produce.Precio = double.Parse(reader["precio"].ToString());
                            produce.IdDeposito = int.Parse(reader["idDeposito"].ToString());

                            resultado.Add(produce);
                        }
                    }
                }
                catch (Exception)
                {

                    return resultado;

                }
            }
            return resultado;
        }

        public List<Produce> buscarVarProducen(string var)
        {
            List<Produce> resultado = new List<Produce>();
            try
            {
                Produce produce;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarProduce", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produce = new Produce();
                        produce.IdGranja = int.Parse(reader["idGranja"].ToString());
                        produce.IdProducto = int.Parse(reader["idProducto"].ToString());
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        produce.FchProduccion = DateArr[0];
                        produce.Stock = int.Parse(reader["stock"].ToString());
                        produce.Precio = double.Parse(reader["precio"].ToString());
                        produce.IdDeposito = int.Parse(reader["idDeposito"].ToString());

                        resultado.Add(produce);
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

        public Produce buscarProduce(int idGranja, int idProducto, string fchProduccion)
        {
            Produce produce = new Produce();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarProduce", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produce.IdGranja = int.Parse(reader["idGranja"].ToString());
                            produce.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            produce.FchProduccion = DateArr[0];
                            produce.Stock = int.Parse(reader["stock"].ToString());
                            produce.Precio = double.Parse(reader["precio"].ToString());
                            produce.IdDeposito = int.Parse(reader["idDeposito"].ToString());
                        }
                    }
                }
                catch (Exception)
                {

                    return produce;

                }
            }
            return produce;
        }

        public bool altaProduce(Produce produce)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaProduce", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idGranja", produce.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", produce.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", produce.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@stock", produce.Stock));
                cmd.Parameters.Add(new SqlParameter("@precio", produce.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", produce.IdDeposito));


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

        public bool bajaProduce(int idGranja, int idProducto, string fchProduccion)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaProduce", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

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

        public bool modProduce(Produce produce)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarProduce", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idGranja", produce.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", produce.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", produce.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@stock", produce.Stock));
                cmd.Parameters.Add(new SqlParameter("@precio", produce.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", produce.IdDeposito));

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
