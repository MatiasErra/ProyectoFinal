﻿using Clases;
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

        public List<Cliente> lstCli()
        {
            List<Cliente> resultado = new List<Cliente>();
            try
            {
                Cliente cliente;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("LstCliente", conect);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente = new Cliente();
                        cliente.IdPersona = int.Parse(reader["idPersona"].ToString());
                        cliente.Nombre = reader["nombre"].ToString();
                        cliente.Apellido = reader["apellido"].ToString();
                        cliente.User = reader["usuario"].ToString();
                        cliente.Direccion = reader["direccion"].ToString();

                        resultado.Add(cliente);
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
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;

        }


        public bool bajaCli(int id)
        {
            bool resultado = false;
            try
            {
                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BajaCli", conect);

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
            catch (Exception ex)
            {
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

