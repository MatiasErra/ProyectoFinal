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
    class pLote_Pesti
    {
 
        public List<Lote_Pesti> PestisEnLote(int idGranja, int idProducto, string fchProduccion, string buscar, string ord)
        {
            List<Lote_Pesti> resultado = new List<Lote_Pesti>();


            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstPestiEnLoteFiltro", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
                    cmd.Parameters.Add(new SqlParameter("@buscar", buscar));
                    cmd.Parameters.Add(new SqlParameter("@ordenar", ord));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lote_Pesti pest = new Lote_Pesti();
                            pest.IdPesticida = int.Parse(reader["idPesti"].ToString());
                            pest.NombrePesti = reader["nombre"].ToString();
                            pest.TipoPesti  = reader["tipo"].ToString();
                            pest.Cantidad = reader["cantidad"].ToString();
                            pest.PHPesti = double.Parse(reader["pH"].ToString());

                            resultado.Add(pest);
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

       
        

        public Lote_Pesti buscarLotePesti(int idPesticida, int idGranja, int idProducto, string fchProduccion)
        {
            Lote_Pesti loteP = new Lote_Pesti();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarLote_Pesti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPesticida", idPesticida));
                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loteP.IdPesticida = int.Parse(reader["idPesticida"].ToString());
                            loteP.IdGranja = int.Parse(reader["idGranja"].ToString());
                            loteP.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            loteP.FchProduccion = DateArr[0];
                            loteP.Cantidad = reader["cantidad"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return loteP;

                }
            }
            return loteP;
        }

        public bool altaLotePesti(Lote_Pesti loteP)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaLotePesti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPesticida", loteP.IdPesticida));
                cmd.Parameters.Add(new SqlParameter("@idGranja", loteP.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", loteP.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", loteP.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", loteP.Cantidad));


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


        public bool bajaLotePesti(int idPesticida, int idGranja, int idProducto, string fchProduccion)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaLotePesti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPesticida", idPesticida));
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

        public bool modLotePesti(Lote_Pesti loteP)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarLotePesti ", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPesticida", loteP.IdPesticida));
                cmd.Parameters.Add(new SqlParameter("@idGranja", loteP.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", loteP.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", loteP.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", loteP.Cantidad));

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
