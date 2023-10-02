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



        public List<Lote> buscarFiltrarLotes(Lote pLote, double precioMenor, double precioMayor, string fchProduccionMenor, string fchProduccionMayor, string fchCaducidadMenor, string fchCaducidadMayor, string ordenar)
        {
            List<Lote> resultado = new List<Lote>();
            try
            {

                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarFiltrarLotes", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@granja", pLote.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@producto", pLote.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@deposito", pLote.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@precioMenor", precioMenor));
                cmd.Parameters.Add(new SqlParameter("@precioMayor", precioMayor));
                cmd.Parameters.Add(new SqlParameter("@fchProduccionMenor", fchProduccionMenor));
                cmd.Parameters.Add(new SqlParameter("@fchProduccionMayor", fchProduccionMayor));
                cmd.Parameters.Add(new SqlParameter("@fchCaducidadMenor", fchCaducidadMenor));
                cmd.Parameters.Add(new SqlParameter("@fchCaducidadMayor", fchCaducidadMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lote lote = new Lote();
                        lote.IdGranja = int.Parse(reader["idGranja"].ToString());
                        lote.NombreGranja = reader["nombreGranja"].ToString();
                        lote.IdProducto = int.Parse(reader["idProducto"].ToString());
                        lote.NombreProducto = reader["nombreProducto"].ToString();
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        lote.FchProduccion = DateArr[0];
                        string[] DateCadArr = reader["fchCaducidad"].ToString().Split(' ');
                        lote.FchCaducidad = DateCadArr[0];
                        lote.Cantidad = reader["cantidad"].ToString();
                        lote.Precio = double.Parse(reader["precio"].ToString());
                        lote.IdDeposito = int.Parse(reader["idDeposito"].ToString());
                        lote.UbicacionDeps = reader["ubicacionDeposito"].ToString();

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

        public Lote buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            Lote lote = new Lote();

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

                            lote.IdGranja = int.Parse(reader["idGranja"].ToString());
                            lote.NombreGranja = reader["nombreGranja"].ToString();
                            lote.IdProducto = int.Parse(reader["idProducto"].ToString());
                            lote.NombreProducto = reader["nombreProducto"].ToString();
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            lote.FchProduccion = DateArr[0];
                            string[] DateCadArr = reader["fchCaducidad"].ToString().Split(' ');
                            lote.FchCaducidad = DateCadArr[0];
                            lote.Cantidad = reader["cantidad"].ToString();
                            lote.Precio = double.Parse(reader["precio"].ToString());
                            lote.IdDeposito = int.Parse(reader["idDeposito"].ToString());
                            lote.UbicacionDeps = reader["ubicacionDeposito"].ToString();

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
        public bool altaLote(Lote lote, string cantTotal, int idAdmin)
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
                cmd.Parameters.Add(new SqlParameter("@fchCaducidad", lote.FchCaducidad));
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@cantTotal", cantTotal));
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


        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal, int idAdmin)
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

        public bool modLote(Lote lote, string cantTotal, int idAdmin)
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
                cmd.Parameters.Add(new SqlParameter("@fchCaducidad", lote.FchCaducidad));
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));
                cmd.Parameters.Add(new SqlParameter("@cantTotal", cantTotal));
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
