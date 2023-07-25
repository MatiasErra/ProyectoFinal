using persistenciaDB;
using System;
using System.Data.SqlClient;
using System.Data;
using Clases;
using System.Collections.Generic;

namespace Persistencia
{
    class pAdmin
    {

        public List<Admin> lstAdmin()
        {
            List<Admin> resultado = new List<Admin>();
            try
            {
                Admin admin;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstAdmin", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admin = new Admin();
                        admin.IdPersona = int.Parse(reader["idPersona"].ToString());
                        admin.Nombre = reader["nombre"].ToString();
                        admin.Apellido = reader["apellido"].ToString();
                        admin.Email = reader["email"].ToString();
                        admin.Telefono = reader["telefono"].ToString();
                        admin.FchNacimiento = reader["fchNacimiento"].ToString();
                        admin.User = reader["usuario"].ToString();
                        admin.TipoDeAdmin = reader["tipoDeAdmin"].ToString();

                        resultado.Add(admin);
                    }
                }

                conect.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return resultado;
        }

        public List<Admin> buscarVarAdmin(string var)
        {
            List<Admin> resultado = new List<Admin>();
            try
            {
                Admin admin;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarAdmin", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admin = new Admin();
                        admin.IdPersona = int.Parse(reader["idPersona"].ToString());
                        admin.Nombre = reader["nombre"].ToString();
                        admin.Apellido = reader["apellido"].ToString();
                        admin.Email = reader["email"].ToString();
                        admin.Telefono = reader["telefono"].ToString();
                        admin.FchNacimiento = reader["fchNacimiento"].ToString();
                        admin.User = reader["usuario"].ToString();
                        admin.TipoDeAdmin = reader["tipoDeAdmin"].ToString();

                        resultado.Add(admin);
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



        public List<Persona> lstIdPersonas()
        {
            List<Persona> resultado = new List<Persona>();
            try
            {
                Persona persona;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("lstIdPersonas", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        persona = new Admin();
                        persona.IdPersona = int.Parse(reader["idPersona"].ToString());


                        resultado.Add(persona);
                    }
                }

                conect.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return resultado;
        }



        public bool altaAdmin(Admin admin)
        {
            bool resultado = false;

            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaAdmin", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", admin.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@nombre", admin.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", admin.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", admin.Email));
                cmd.Parameters.Add(new SqlParameter("@tele", admin.Telefono));
                cmd.Parameters.Add(new SqlParameter("@fchNac", admin.FchNacimiento));
                cmd.Parameters.Add(new SqlParameter("@user", admin.User));
                cmd.Parameters.Add(new SqlParameter("@pass", admin.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@TipoAdm", admin.TipoDeAdmin));

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
            }

            return resultado;

        }


        public bool bajaAdmin(int id)
        {
            bool resultado = false;
            try
            {
                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BajaAdmin", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {

                        cmd.Parameters.Add(new SqlParameter("@id", id));

                    }

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
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public bool modificarAdm(Admin admin)
        {
            bool resultado = true;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("ModificarAdm", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", admin.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@nombre", admin.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", admin.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", admin.Email));
                cmd.Parameters.Add(new SqlParameter("@tele", admin.Telefono));
                cmd.Parameters.Add(new SqlParameter("@fchNac", admin.FchNacimiento));
                cmd.Parameters.Add(new SqlParameter("@user", admin.User));
                cmd.Parameters.Add(new SqlParameter("@TipoAdm", admin.TipoDeAdmin));




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



        public Admin buscarAdm (int id)
        {
            Admin admin = new Admin();

            try
            {


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarAdm", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int idAdm = int.Parse(reader["idPersona"].ToString());
                        string nombre = reader["nombre"].ToString();
                        string apellido = reader["apellido"].ToString();
                        string eMail = reader["email"].ToString();
                        string tel = reader["telefono"].ToString();
                        string fchNacimiento = reader["fchNacimiento"].ToString();

                      
                        string User = reader["usuario"].ToString();
                        string Password = reader["contrasena"].ToString();
                          string tpoAdm = reader["tipoDeAdmin"].ToString();

                        
  
                        Admin resultado = new Admin(idAdm, nombre, apellido, eMail , tel, fchNacimiento, User, Password, tpoAdm);
                        admin = resultado;
                    }


                }

                conect.Close();



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return admin;

        }



    }
}
