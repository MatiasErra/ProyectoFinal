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

                return @"Server=DESKTOP-F29CTN7\SQLEXPRESS;Database=Proyecto; User=sa; Password=6122;";
            }
        }

        public static SqlConnection Conectar()
        {
            SqlConnection conectar = new SqlConnection(CadenadaDeConexion);
            try
            {
                conectar.Open();
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return conectar;
        }
    }
}
