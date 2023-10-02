using Clases;
using persistenciaDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    class pPedido
    {

        public List<Pedido> BuscarPedidoFiltro(string NombreCli, string Estado, string Viaje , double CostoMin, double CostoMax, string fchPedidoMenor, string fchPedidoMayor, string fchEntregaMenor, string fchEntregaMayor, string Ordenar)
        {
            List<Pedido> resultado = new List<Pedido>();
            try
            {
                Pedido pedido;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarPedidoFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@NombreCli", NombreCli));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Viaje", Viaje));
                cmd.Parameters.Add(new SqlParameter("@CostoMin", CostoMin));
                cmd.Parameters.Add(new SqlParameter("@CostoMax", CostoMax));
                cmd.Parameters.Add(new SqlParameter("@fchPedidoMenor", fchPedidoMenor));
                cmd.Parameters.Add(new SqlParameter("@fchPedidoMayor", fchPedidoMayor));
                cmd.Parameters.Add(new SqlParameter("@fchEntregaMenor", fchEntregaMenor));
                cmd.Parameters.Add(new SqlParameter("@fchEntregaMayor", fchEntregaMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", Ordenar));



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedido = new Pedido();

                        pedido.IdPedido = int.Parse(reader["idPedido"].ToString());
                        pedido.IdCliente = int.Parse(reader["idCliente"].ToString());
                        pedido.NombreCli = reader["NombreCli"].ToString();
                        pedido.Estado = reader["estado"].ToString();
                        pedido.Viaje = reader["viaje"].ToString();
                        pedido.Costo = double.Parse(reader["costo"].ToString());
                        pedido.FechaPedido = reader["fchPedido"].ToString();
                        pedido.FechaEntre = reader["fechaEntre"].ToString();
     
                        pedido.InformacionEnvio = reader["informacionEntrega"].ToString();

                        resultado.Add(pedido);
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








        public List<Pedido_Prod> listPedidoCli_Prod(int idProducto)
        {

            List<Pedido_Prod> resultado = new List<Pedido_Prod>();
            try
            {
                Pedido_Prod pedido_Prod;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("listPedidoCli_Prod", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedido_Prod = new Pedido_Prod();

                        pedido_Prod.IdPedido = int.Parse(reader["idPedido"].ToString());
                        pedido_Prod.IdProducto = int.Parse(reader["idProducto"].ToString());
                        pedido_Prod.Cantidad = reader["cantidad"].ToString();


                        resultado.Add(pedido_Prod);
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
        public List<Pedido> listPedidoCli(int idCli)
        {
            List<Pedido> resultado = new List<Pedido>();
            try
            {
                Pedido pedido;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("listPedidoCli", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idCli", idCli));


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedido = new Pedido();

                        pedido.IdPedido = int.Parse(reader["idPedido"].ToString());
                        pedido.IdCliente = int.Parse(reader["idCliente"].ToString());
                        pedido.Estado = reader["estado"].ToString();
                        string[] DateArr = reader["fchPedido"].ToString().Split(' ');
                        pedido.FechaPedido = DateArr[0];
                        pedido.Costo = double.Parse(reader["costo"].ToString());
                        pedido.InformacionEnvio = reader["informacionEntrega"].ToString();



                        resultado.Add(pedido);
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

        public List<Pedido> listPedido()
        {
            List<Pedido> resultado = new List<Pedido>();
            try
            {
                Pedido pedido;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("listPedido", conect);

                cmd.CommandType = CommandType.StoredProcedure;



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedido = new Pedido();

                        pedido.IdPedido = int.Parse(reader["idPedido"].ToString());
                        pedido.IdCliente = int.Parse(reader["idCliente"].ToString());
                        pedido.Estado = reader["estado"].ToString();
                        pedido.FechaPedido = reader["fchPedido"].ToString();

                        resultado.Add(pedido);
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





        public bool altaPedido(Pedido pedido)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaPedido", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdPedido", pedido.IdPedido));
                cmd.Parameters.Add(new SqlParameter("@IdCliente", pedido.IdCliente));
                cmd.Parameters.Add(new SqlParameter("@infoEnv", pedido.InformacionEnvio));


                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {

                    resultado = true;
                }
                if (conect.State == ConnectionState.Open)
                {
                    conect.Close();
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


        public bool bajaPedido(int IdPedido)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaPedido", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdPedido", IdPedido));




                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {

                    resultado = true;
                }
                if (conect.State == ConnectionState.Open)
                {
                    conect.Close();
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

        public bool altaPedido_Lote(Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaPedido_Lote", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdPedido", lote_Pedido.IdPedido));
                cmd.Parameters.Add(new SqlParameter("@IdProducto", lote_Pedido.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@IdGranja", lote_Pedido.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", lote_Pedido.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", lote_Pedido.Cant));
                cmd.Parameters.Add(new SqlParameter("@CantLote", CantLote));
                cmd.Parameters.Add(new SqlParameter("@CantDisp", CantDisp));
                cmd.Parameters.Add(new SqlParameter("@CantRess", CantRess));
                cmd.Parameters.Add(new SqlParameter("@idAdmin", idAdmin));

                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)  
                {

                    resultado = true;
                }
                if (conect.State == ConnectionState.Open)
                {
                    conect.Close();
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
        public bool altaPedido_Prod(Pedido_Prod pedido_Prod, string CantidadRes, double precio)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaPedido_Prod", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdPedido", pedido_Prod.IdPedido));
                cmd.Parameters.Add(new SqlParameter("@IdProducto", pedido_Prod.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", pedido_Prod.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@CantidadRes", CantidadRes));
                cmd.Parameters.Add(new SqlParameter("@precio", precio));

                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {

                    resultado = true;
                }
                if (conect.State == ConnectionState.Open)
                {
                    conect.Close();
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
        public Pedido_Prod buscarProductoCli(int idProducto, int idCliente)
        {
            Pedido_Prod pedido = new Pedido_Prod();
            try
            {



                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarProductoCli", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        pedido.IdPedido = int.Parse(reader["idPedido"].ToString());




                    }
                }

                conect.Close();
            }
            catch (Exception)
            {
                return pedido;
            }
            return pedido;
        }




        public bool cambiarEstadoPed(int idPedido, string estado)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("CambiarEstadoPed", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                cmd.Parameters.Add(new SqlParameter("@estado", estado));

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


        public List<string[]> buscarPedidoProd(int idPedido)
        {
            {
                List<string[]> resultado = new List<string[]>();
                try
                {
                    string[] producto;


                    SqlConnection conect = Conexion.Conectar();

                    SqlCommand cmd = new SqlCommand("BuscarPedidoProd", conect);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto = new string[9];

                            producto[0] = reader["idPedido"].ToString();
                            producto[1] = reader["idProducto"].ToString();
                            producto[2] = reader["nombre"].ToString();
                            producto[3] = reader["tipo"].ToString();
                            producto[4] = reader["precio"].ToString();
                            producto[5] = reader["imagen"].ToString();
                            producto[6] = reader["cantidad"].ToString();
                            producto[7] = reader["cantTotal"].ToString();
                            producto[8] = reader["cantRes"].ToString();
                            resultado.Add(producto);
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



        }

        public List<string[]> buscarPedidoLote(int idPedido)
        {
            {
                List<string[]> resultado = new List<string[]>();
                try
                {
                    string[] producto;


                    SqlConnection conect = Conexion.Conectar();

                    SqlCommand cmd = new SqlCommand("BuscarPedidoLote", conect);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto = new string[13];

                            producto[0] = reader["idPedido"].ToString();
                            producto[1] = reader["idProducto"].ToString();
                            producto[2] = reader["nombreProd"].ToString();
                            producto[3] = reader["idGranja"].ToString();
                            producto[4] = reader["nomGranja"].ToString();
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            producto[5] = DateArr[0];
                            producto[6] = reader["tipo"].ToString();
                            producto[7] = reader["precio"].ToString();
                            producto[8] = reader["imagen"].ToString();
                            producto[9] = reader["cantidad"].ToString();
                            producto[10] = reader["cantTotal"].ToString();
                            producto[11] = reader["cantRes"].ToString();
                            producto[12] = reader["cantidadViaje"].ToString();
                         
                            resultado.Add(producto);
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



        }



        public string[] buscarProductoClixNom(int idPedido, string nomProd)
        {
            {
                string[] producto = new string[3];
                try
                {



                    SqlConnection conect = Conexion.Conectar();

                    SqlCommand cmd = new SqlCommand("[BuscarProductoClixNom]", conect);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                    cmd.Parameters.Add(new SqlParameter("@nomProd", nomProd));


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto = new string[4];

                            producto[0] = reader["idPedido"].ToString();
                            producto[1] = reader["idProducto"].ToString();
                            producto[2] = reader["nombre"].ToString();
                            producto[3] = reader["cantidad"].ToString();


                        }
                    }

                    conect.Close();
                }
                catch (Exception)
                {
                    return producto;
                }
                return producto;
            }



        }

        public bool modCantPedidoCli(int idPedido, int idProducto, string cantidad, string cantRess, double precio)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModCantPedidoCli", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@cant", cantidad));
                cmd.Parameters.Add(new SqlParameter("@cantRess", cantRess));
                cmd.Parameters.Add(new SqlParameter("@precio", precio));


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

        public bool modCantPedidoLote (Lote_Pedido lote_Pedido, string CantLote, string CantDisp, string CantRess, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModCantPedidoLote", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", lote_Pedido.IdPedido));
                cmd.Parameters.Add(new SqlParameter("@idGranja", lote_Pedido.IdGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", lote_Pedido.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", lote_Pedido.FchProduccion));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", lote_Pedido.Cant));
                cmd.Parameters.Add(new SqlParameter("@CantLote", CantLote));
                cmd.Parameters.Add(new SqlParameter("@CantDisp", CantDisp));
                cmd.Parameters.Add(new SqlParameter("@CantRess", CantRess));
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

        public bool bajaPedidoProd(int idPedido, int idProducto, string cantRess, double precio)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaPedidoProd", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@cantRess", cantRess));
                cmd.Parameters.Add(new SqlParameter("@precio", precio));


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


        public bool bajaLotesPedido(int idPedido, int idGranja, int idProducto, string fchProduccion, string cantLote, string CantDisp, string CantRess, int idAdmin) 
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaLotesPedido", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));
                cmd.Parameters.Add(new SqlParameter("@cantLote", cantLote));
                cmd.Parameters.Add(new SqlParameter("@CantDisp", CantDisp));
                cmd.Parameters.Add(new SqlParameter("@CantRess", CantRess));
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

        public bool modPedViajeEst(int idPedido, string estado, int idAdmin)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModPedViajeEst", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@idPedido", idPedido));
                cmd.Parameters.Add(new SqlParameter("@estado", estado));
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
