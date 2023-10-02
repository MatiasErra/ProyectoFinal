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
   class pCliente
    {




        public List<string> userRepetidoCli()
        {
            List<string> resultado = new List<string>();
            try
            {
                string user = "";
            
             SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("userRepetidoCli", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = "";
                        user = reader["usuario"].ToString();


                        resultado.Add(user);

                    

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

        public List<Cliente> buscarCliFiltro(Cliente cliente, string fchDesde, string fchHasta, string ordenar)
        {
            List<Cliente> resultado = new List<Cliente>();
            try
            {
                Cliente cli;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarCliFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", cliente.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", cliente.Email));
                cmd.Parameters.Add(new SqlParameter("@telefono", cliente.Telefono));
                cmd.Parameters.Add(new SqlParameter("@usuario", cliente.User));
                cmd.Parameters.Add(new SqlParameter("@direccion", cliente.Direccion));
                cmd.Parameters.Add(new SqlParameter("@fchDesde", fchDesde));
                cmd.Parameters.Add(new SqlParameter("@fchHasta", fchHasta));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cli = new Cliente();
                        cli.IdPersona = int.Parse(reader["idPersona"].ToString());
                        cli.Nombre = reader["nombre"].ToString();
                        cli.Apellido = reader["apellido"].ToString();
                        cli.Email = reader["email"].ToString();
                        cli.Telefono = reader["telefono"].ToString();
                        string Date = reader["fchNacimiento"].ToString();
                        string[] DateArr = Date.Split(' ');

                        cli.FchNacimiento = DateArr[0];
                        cli.User = reader["usuario"].ToString();
                        cli.Direccion = reader["direccion"].ToString();

                        resultado.Add(cli);
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

        public int iniciarSesionCli(string user, string pass)
        {
            int id = 0;

          
            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("IniciarSesionCli", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@User", user));
                cmd.Parameters.Add(new SqlParameter("@Pass", pass));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                            id = int.Parse(reader["idCliente"].ToString());
                        
                    }
                }
                conect.Close();
            }
            catch (Exception)
            {
                id = 0;
                
            }
            return id;

         }


        public bool altaCli(Cliente cli)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaCli", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", cli.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@nombre", cli.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", cli.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", cli.Email));
                cmd.Parameters.Add(new SqlParameter("@tele", cli.Telefono));
                cmd.Parameters.Add(new SqlParameter("@fchNac", cli.FchNacimiento));
                cmd.Parameters.Add(new SqlParameter("@user", cli.User));
                cmd.Parameters.Add(new SqlParameter("@pass", cli.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@dirr", cli.Direccion));

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
            catch (Exception )
            {
                resultado = false;
                return resultado;
            }

            return resultado;

        }


        public bool bajaCli(int id, int idAdmin)
        {
            bool resultado = false;
            try
            {
                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BajaCli", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
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

        public bool modificarCli(Cliente cliente)
        {
            bool resultado = true;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarCli", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", cliente.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", cliente.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", cliente.Email));
                cmd.Parameters.Add(new SqlParameter("@tele", cliente.Telefono));
                cmd.Parameters.Add(new SqlParameter("@fchNac", cliente.FchNacimiento));
                cmd.Parameters.Add(new SqlParameter("@user", cliente.User));
                cmd.Parameters.Add(new SqlParameter("@pass", cliente.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@dirr", cliente.Direccion));




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
            catch (Exception e)
            {
                string a = e.ToString();
                resultado = false;
                return resultado;

            }

            return resultado;

        }



        public Cliente buscarCli(int id)
        {
           Cliente cliente = new Cliente();

            try
            {


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarCli", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int idCli = int.Parse(reader["idPersona"].ToString());
                        string nombre = reader["nombre"].ToString();
                        string apellido = reader["apellido"].ToString();
                        string eMail = reader["email"].ToString();
                        string tel = reader["telefono"].ToString();
                        string fchNacimiento = reader["fchNacimiento"].ToString();


                        string User = reader["usuario"].ToString();
                        string Password = reader["contrasena"].ToString();
                        string dirr = reader["direccion"].ToString();



                        Cliente resultado = new Cliente(idCli, nombre, apellido, eMail, tel, fchNacimiento, User, Password, dirr);
                        cliente = resultado;
                    }


                }

                conect.Close();



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cliente;

        }




    }



}

