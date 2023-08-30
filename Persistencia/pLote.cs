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
    class pLote
    {

      

        public List<string[]> buscarFiltrarLotes(string buscar, string ordenar)
        {
            List<string[]> resultado = new List<string[]>();
            try
            {

                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarFiltrarLotes", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@buscar", buscar));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] lote = new string[9];
                        lote[0] = reader["idGranja"].ToString();
                        lote[1] = reader["nombreGranja"].ToString();
                        lote[2] = reader["idProducto"].ToString();
                        lote[3] = reader["nombreProducto"].ToString();
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        lote[4] = DateArr[0];
                        lote[5] = reader["cantidad"].ToString();
                        lote[6] = reader["precio"].ToString();
                        lote[7] = reader["idDeposito"].ToString();
                        lote[8] = reader["ubicacionDeposito"].ToString();

                        resultado.Add(lote);
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

        public string[] buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            string[] lote = new string[9];

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarLote", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@nombreGranja", nombreGranja));
                    cmd.Parameters.Add(new SqlParameter("@nombreProducto", nombreProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            lote[0] = reader["idGranja"].ToString();
                            lote[1] = reader["nombreGranja"].ToString();
                            lote[2] = reader["idProducto"].ToString();
                            lote[3] = reader["nombreProducto"].ToString();
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            lote[4] = DateArr[0];
                            lote[5] = reader["cantidad"].ToString();
                            lote[6] = reader["precio"].ToString();
                            lote[7] = reader["idDeposito"].ToString();
                            lote[8] = reader["ubicacionDeposito"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return lote;

                }
            }
            return lote;
        }
        public bool altaLote(Lote lote, string cantTotal)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaLote", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idGranja", lote.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", lote.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", lote.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@cantTotal", cantTotal));


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


        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion,string cantTotal)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaLote", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@nombreGranja", nombreGranja));
                cmd.Parameters.Add(new SqlParameter("@nombreProducto", nombreProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantTotal", cantTotal));

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

        public bool modLote(Lote lote, string cantTotal)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarLote", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idGranja", lote.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", lote.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", lote.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@cantTotal", cantTotal));

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
