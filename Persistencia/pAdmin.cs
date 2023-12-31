﻿using persistenciaDB;
using System;
using System.Data.SqlClient;
using System.Data;
using Clases;
using System.Collections.Generic;

namespace Persistencia
{
    class pAdmin
    {

        public int iniciarSesionAdm(string user, string pass)
        {
            int id = 0;


            try
            {
                SqlConnection conect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("IniciarSesionAdm", conect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@User", user));
                cmd.Parameters.Add(new SqlParameter("@Pass", pass));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        id = int.Parse(reader["idAdmin"].ToString());

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

        public List<string> userRepetidoAdm()
        {
            List<string> resultado = new List<string>();
            try
            {
                string user = "";

                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("userRepetidoAdm", conect);

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

        public List<Admin> buscarAdminFiltro(Admin adminBuscar, string ordenar)
        {
            List<Admin> resultado = new List<Admin>();
            try
            {
                Admin admin;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarAdminFiltro", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", adminBuscar.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", adminBuscar.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", adminBuscar.Email));
                cmd.Parameters.Add(new SqlParameter("@usuario", adminBuscar.User));
                cmd.Parameters.Add(new SqlParameter("@tipoAdmin", adminBuscar.TipoDeAdmin));
                cmd.Parameters.Add(new SqlParameter("@estado", adminBuscar.Estado));
                cmd.Parameters.Add(new SqlParameter("@ordenar", ordenar));
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
                        string Date = reader["fchNacimiento"].ToString();
                        string[] DateArr = Date.Split(' ');

                        admin.FchNacimiento = DateArr[0];
                        admin.User = reader["usuario"].ToString();
                        admin.TipoDeAdmin = reader["tipoDeAdmin"].ToString();
                        admin.Estado = reader["estado"].ToString();
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
            catch (Exception)
            {
                return resultado;
            }
            return resultado;
        }



        public bool altaAdmin(Admin admin, int idAdmin)
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
                cmd.Parameters.Add(new SqlParameter("@estado", admin.Estado));
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


        public bool bajaAdmin(int id, int idAdmin)
        {
            bool resultado = false;
            try
            {
                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BajaAdmin", conect);

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

        public bool modificarAdm(Admin admin, int idAdmin)
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

                cmd.Parameters.Add(new SqlParameter("@fchNac", admin.FchNacimiento));

                cmd.Parameters.Add(new SqlParameter("@TipoAdm", admin.TipoDeAdmin));
                cmd.Parameters.Add(new SqlParameter("@estado", admin.Estado));
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
                        string estado = reader["estado"].ToString();
                       

                        
  
                        Admin resultado = new Admin(idAdm, nombre, apellido, eMail , tel, fchNacimiento, User, Password, tpoAdm, estado);
                        admin = resultado;
                    }


                }

                conect.Close();



            }
            catch (Exception)
            {
                return admin;
            }
            return admin;

        }



    }
}
