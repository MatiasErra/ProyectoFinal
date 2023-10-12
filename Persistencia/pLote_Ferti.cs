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
    class pLote_Ferti
    {
        

        public List<Lote_Ferti> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            List<Lote_Ferti> resultado = new List<Lote_Ferti>();

         
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstFertisEnLoteFiltro", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
         


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lote_Ferti fert = new Lote_Ferti ();
                            fert.IdFertilizante= int.Parse(reader["idFerti"].ToString());
                         
                            fert.NombreFert  = reader["nombre"].ToString();
                            fert.TipoFert  = reader["tipo"].ToString();
                            fert.Cantidad = reader["cantidad"].ToString();
                            fert.PHFert = double.Parse(reader["pH"].ToString());
                     

                            resultado.Add(fert);
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

 
        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            Lote_Ferti loteF = new Lote_Ferti();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarLote_Ferti", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idFertilizante", idFertilizante));
                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loteF.IdFertilizante = int.Parse(reader["idFertilizante"].ToString());
                            loteF.IdGranja = int.Parse(reader["idGranja"].ToString());
                            loteF.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            loteF.FchProduccion = DateArr[0];
                            loteF.Cantidad = reader["cantidad"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return loteF;

                }
            }
            return loteF;
        }

        public bool altaLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaLoteFerti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idFertilizante", loteF.IdFertilizante));
                cmd.Parameters.Add(new SqlParameter("@idGranja", loteF.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", loteF.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", loteF.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", loteF.Cantidad));
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
                resultado = false;
                return resultado;
            }
            return resultado;
        }


        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaLoteFerti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idFertilizante", idFertilizante));
                cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
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
                resultado = false;
                return resultado;
            }

            return resultado;

        }

        public bool modLoteFerti(Lote_Ferti loteF, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarLoteFerti", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idFertilizante", loteF.IdFertilizante));
                cmd.Parameters.Add(new SqlParameter("@idGranja", loteF.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", loteF.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", loteF.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantidad", loteF.Cantidad));
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
                resultado = false;
                return resultado;

            }

            return resultado;

        }


    }
}
