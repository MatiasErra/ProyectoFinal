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
        public List<Producto> listIdProductos()
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                Producto producto;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstIdProductos", connect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        producto = new Producto();
                        producto.IdProducto = int.Parse(reader["idProducto"].ToString());


                        resultado.Add(producto);
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

        public List<Producto> listProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            Producto producto;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstProductos", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto = new Producto();
                            producto.IdProducto = int.Parse(reader["idProducto"].ToString());
                            producto.Nombre = reader["nombre"].ToString();
                            producto.Tipo = reader["tipo"].ToString();
                            producto.TipoVenta = reader["tipoVenta"].ToString();
                            producto.Imagen = reader["imagen"].ToString();

                            listaProductos.Add(producto);
                        }
                    }
                }
                catch (Exception)
                {

                    return listaProductos;

                }
            }
            return listaProductos;
        }

        public List<Producto> buscarVarProductos(string var)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                Producto producto;


                SqlConnection connect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarProducto", connect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        producto = new Producto();
                        producto.IdProducto = int.Parse(reader["idProducto"].ToString());
                        producto.Nombre = reader["nombre"].ToString();
                        producto.Tipo = reader["tipo"].ToString();
                        producto.TipoVenta = reader["tipoVenta"].ToString();
                        producto.Imagen = reader["imagen"].ToString();

                        resultado.Add(producto);
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
                            producto.TipoVenta = reader["tipoVenta"].ToString();
                            producto.Imagen = reader["imagen"].ToString();
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
            catch (Exception ex)
            {
                throw ex;
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
                return resultado;

            }

            return resultado;

        }
    }
}
