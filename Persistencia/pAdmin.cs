using persistenciaDB;
using System;
using System.Data.SqlClient;
using System.Data;
using Clases;

namespace Persistencia
{
    class pAdmin
    {
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


                //Se ejecuta el procedimiento y retorna un int (si es mayor a cero es que se ejecutó correctamente)
                int resBD = cmd.ExecuteNonQuery();

                if (resBD > 0)
                {

                    resultado = true;
                }
                if (conect.State == ConnectionState.Open)
                {   
                    conect.Close();

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
