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
    class pLote
    {

        public List<Lote> listLotes()
        {
            List<Lote> resultado = new List<Lote>();

            Lote lote;
            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("LstLotes", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lote = new Lote();
                            lote.IdGranja = int.Parse(reader["idGranja"].ToString());
                            lote.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            lote.FchProduccion = DateArr[0];
                            lote.Cantidad = int.Parse(reader["cantidad"].ToString());
                            lote.Precio = double.Parse(reader["precio"].ToString());
                            lote.IdDeposito = int.Parse(reader["idDeposito"].ToString());

                            resultado.Add(lote);
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

        public List<Lote> buscarVarLotes(string var)
        {
            List<Lote> resultado = new List<Lote>();
            try
            {
                Lote lote;


                SqlConnection conect = Conexion.Conectar();

                SqlCommand cmd = new SqlCommand("BuscarVarLote", conect);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@var", var));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lote = new Lote();
                        lote.IdGranja = int.Parse(reader["idGranja"].ToString());
                        lote.IdProducto = int.Parse(reader["idProducto"].ToString());
                        string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                        lote.FchProduccion = DateArr[0];
                        lote.Cantidad = int.Parse(reader["cantidad"].ToString());
                        lote.Precio = double.Parse(reader["precio"].ToString());
                        lote.IdDeposito = int.Parse(reader["idDeposito"].ToString());

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

        public Lote buscarLote(int idGranja, int idProducto, string fchProduccion)
        {
            Lote lote = new Lote();

            using (SqlConnection connect = Conexion.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarLote", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idGranja", idGranja));
                    cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                    cmd.Parameters.Add(new SqlParameter("@fchProduccion", fchProduccion));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lote.IdGranja = int.Parse(reader["idGranja"].ToString());
                            lote.IdProducto = int.Parse(reader["idProducto"].ToString());
                            string[] DateArr = reader["fchProduccion"].ToString().Split(' ');
                            lote.FchProduccion = DateArr[0];
                            lote.Cantidad = int.Parse(reader["cantidad"].ToString());
                            lote.Precio = double.Parse(reader["precio"].ToString());
                            lote.IdDeposito = int.Parse(reader["idDeposito"].ToString());
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
        public bool altaLote(Lote lote)
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
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));
        


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


        public bool bajaLote(int idGranja, int idProducto, string fchProduccion)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("BajaLote", connect);
                cmd.CommandType = CommandType.StoredProcedure;

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

        public bool modLote(Lote lote)
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
                cmd.Parameters.Add(new SqlParameter("@cantidad", lote.Cantidad));
                cmd.Parameters.Add(new SqlParameter("@precio", lote.Precio));
                cmd.Parameters.Add(new SqlParameter("@idDeposito", lote.IdDeposito));

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