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
        public List<Lote_Ferti> listLotesFertis()
        {
            List<Lote_Ferti> resultado = new List<Lote_Ferti>();

            Lote_Ferti loteF;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstLotes_Fertis", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loteF = new Lote_Ferti();
                            loteF.IdFertilizante = int.Parse(reader["idFertilizante"].ToString());
                            loteF.IdGranja = int.Parse(reader["idGranja"].ToString());
                            loteF.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            loteF.FchProduccion = DateArr[0];
                            loteF.Cantidad = reader["cantidad"].ToString();

                            resultado.Add(loteF);
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

        public List<string[]> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            List<string[]> resultado = new List<string[]>();

         
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstFertisEnLote", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] fert = new string[5];
                            int IdFertilizante = int.Parse(reader["idFerti"].ToString());
                            string Nombre = reader["nombre"].ToString();
                            string Tipo = reader["tipo"].ToString();
                            string Cantidad = reader["cantidad"].ToString();

                            fert[0] = IdFertilizante.ToString();
                            fert[1] = Nombre;
                            fert[2] = Tipo;
                            fert[3] = Cantidad;

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

        public List<Lote_Ferti> buscarVarLotesFertis(string var)
        {
            List<Lote_Ferti> resultado = new List<Lote_Ferti>();
            try
            {
                Lote_Ferti loteF;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarLote_Ferti", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loteF = new Lote_Ferti();
                        loteF.IdFertilizante = int.Parse(reader["idFertilizante"].ToString());
                        loteF.IdGranja = int.Parse(reader["idGranja"].ToString());
                        loteF.IdProducto = int.Parse(reader["idProducto"].ToString());
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        loteF.FchProduccion = DateArr[0];
                        loteF.Cantidad = reader["cantidad"].ToString();

                        resultado.Add(loteF);
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

        public bool altaLoteFerti(Lote_Ferti loteF)
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


        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
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

        public bool modLoteFerti(Lote_Ferti loteF)
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
