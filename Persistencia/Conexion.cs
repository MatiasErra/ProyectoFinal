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

                return @"Server=DBfrutasYverduras.mssql.somee.com;Database=DBfrutasYverduras; User=MatiasErra_SQLLogin_1; Password=z7xcqlycvx;";

                //return @"Server=DESKTOP-PMKO4KD\SQLEXPRESS;Database=Proyecto; User=sa; Password=12;";
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
