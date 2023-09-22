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
    class pProducto
    {


        public List<Producto> buscarProductoFiltro(Producto pProducto, int precioMenor, int precioMayor, string ordenar)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                Producto producto;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarProductoFiltro", connect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", pProducto.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", pProducto.Tipo));
                cmd.Parameters.Add(new SqlParameter("@tipoVenta", pProducto.TipoVenta));
                cmd.Parameters.Add(new SqlParameter("@precioMenor", precioMenor));
                cmd.Parameters.Add(new SqlParameter("@precioMayor", precioMayor));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        producto = new Producto();
                        producto.IdProducto = int.Parse(reader["idProducto"].ToString());
                        producto.Nombre = reader["nombre"].ToString();
                        producto.Tipo = reader["tipo"].ToString();
                        producto.Precio = int.Parse(reader["precio"].ToString());
                        producto.TipoVenta = reader["tipoVenta"].ToString();
                        producto.Imagen = reader["imagen"].ToString();
                        producto.CantTotal = reader["cantTotal"].ToString();
                        producto.CantRes = reader["cantRes"].ToString();
                        resultado.Add(producto);
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

        public List<Producto> buscarProductoCatFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                Producto producto;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarProductoCatFiltro", connect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@buscar", buscar));
                cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                cmd.Parameters.Add(new SqlParameter("@tipoVen", tipoVen));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        producto = new Producto();
                        producto.IdProducto = int.Parse(reader["idProducto"].ToString());
                        producto.Nombre = reader["nombre"].ToString();
                        producto.Tipo = reader["tipo"].ToString();
                        producto.Precio = int.Parse(reader["precio"].ToString());
                        producto.TipoVenta = reader["tipoVenta"].ToString();
                        producto.Imagen = reader["imagen"].ToString();
                        producto.CantTotal = reader["cantTotal"].ToString();
                        producto.CantRes = reader["cantRes"].ToString();
                        resultado.Add(producto);
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




        public Producto buscarProducto(int id)
        {
            Producto producto = new Producto();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarProducto", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto.IdProducto = int.Parse(reader["idProducto"].ToString());
                            producto.Nombre = reader["nombre"].ToString();
                            producto.Tipo = reader["tipo"].ToString();
                            producto.Precio = int.Parse(reader["precio"].ToString());
                            producto.TipoVenta = reader["tipoVenta"].ToString();
                            producto.Imagen = reader["imagen"].ToString();
                            producto.CantTotal = reader["cantTotal"].ToString();
                            producto.CantRes = reader["cantRes"].ToString();
                        }
                    }
                }
                catch (Exception)
                {

                    return producto;

                }
            }
            return producto;
        }








        public bool altaProducto(Producto producto)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaProducto", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", producto.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@nombre", producto.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", producto.Tipo));
                cmd.Parameters.Add(new SqlParameter("@tipoVenta", producto.TipoVenta));
                cmd.Parameters.Add(new SqlParameter("@precio", producto.Precio));
                cmd.Parameters.Add(new SqlParameter("@imagen", producto.Imagen));


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

        public bool bajaProducto(int id)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaProducto", connect);
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

        public bool modProducto(Producto producto)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarProducto", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", producto.IdProducto));
                cmd.Parameters.Add(new SqlParameter("@nombre", producto.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", producto.Tipo));
                cmd.Parameters.Add(new SqlParameter("@tipoVenta", producto.TipoVenta));
                cmd.Parameters.Add(new SqlParameter("@precio", producto.Precio));
                cmd.Parameters.Add(new SqlParameter("@imagen", producto.Imagen));

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
