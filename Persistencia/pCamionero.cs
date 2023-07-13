using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;
using persistenciaDB;

namespace Persistencia
{
     class pCamionero
    {
        public bool altaCamionero(Camionero camionero)
        {
            bool resultado = false;

            try
            {
                SqlConnection connect = Conexion.Conectar();
                SqlCommand cmd = new SqlCommand("AltaCamionero", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", camionero.IdPersona));
                cmd.Parameters.Add(new SqlParameter("@nombre", camionero.Nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", camionero.Apellido));
                cmd.Parameters.Add(new SqlParameter("@email", camionero.Email));
                cmd.Parameters.Add(new SqlParameter("@tele", camionero.Telefono));
                cmd.Parameters.Add(new SqlParameter("@fchNac", camionero.FchNacimiento));
                cmd.Parameters.Add(new SqlParameter("@cedula", camionero.Cedula));

                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {
                    resultado = true;
                }
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}
