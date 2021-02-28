using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsConexion : clsOperacion
    {
        //String de coneccion para nuestro SQL SERVER....
        public String coneccion = "Data Source = SQL5102.site4now.net;" +
         "Initial Catalog=DB_A6E822_dbguarderia;" +
         "User id=DB_A6E822_dbguarderia_admin;" +
         "Password=cristian20;";

        public SqlConnection cnn;
        //public String coneccion = "Data Source=DESKTOP-5S8K8TU;Initial Catalog=DbExpedientes;Integrated Security=True";
        //public String coneccion = "Server=localhost, Authentication=Windows Authentication, Database= DbExpediente";
        //public String coneccion = "Data Source=LEONY-PC;Initial Catalog=NORTHWND;Integrated Security=True";

        public Boolean Conectando()
        {
            try
            {
                cnn = new SqlConnection(this.coneccion);//Probamos que ahy coneccion.
                cnn.Close();//Cerramos la coneccion
                return true;
            }
            catch
            {
                return false;
            }
        }

        public SqlParameter Parametro(String campo, Int64 value)
        {
            SqlParameter parametro = new SqlParameter(campo, value);
            parametro.DbType = DbType.Int64;
            parametro.Direction = ParameterDirection.Input;
            return parametro;
        }
        public SqlParameter Parametro(String campo, decimal value)
        {
            SqlParameter parametro = new SqlParameter(campo, value);
            parametro.DbType = DbType.Int64;
            parametro.Direction = ParameterDirection.Input;
            return parametro;
        }


        public SqlParameter Parametro(String campo, Double value)
        {
            SqlParameter parametro = new SqlParameter(campo, value);
            parametro.DbType = DbType.Decimal;
            parametro.Direction = ParameterDirection.Input;
            return parametro;
        }

        public SqlParameter Parametro(String campo, String value)
        {
            SqlParameter parametro = new SqlParameter(campo, value);
            parametro.DbType = DbType.String;
            parametro.Direction = ParameterDirection.Input;
            return parametro;
        }
    }
}