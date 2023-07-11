using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace persistenciaDB
{
    public class Conexion
    {
        protected static string CadenadaDeConexion
        {
            get
            {

                return @"Server=DESKTOP-UQQ6CCV\SQLEXPRESS;Database=obligatorioProg; User=sa; Password=123456;";
            }
        }

        public static SqlConnection Conectar()
        {
            SqlConnection conectar = new SqlConnection(CadenadaDeConexion);
            try
            {
                conectar.Open();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return conectar;
        }
    }
}
