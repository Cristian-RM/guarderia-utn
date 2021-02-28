using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    public class ClsConexion
    {
        //String de coneccion para nuestro SQL SERVER....
        public String coneccion = "Data Source = localhost;" +
         "Initial Catalog=dbGuarderia;" +
         "User id=UserExpediente;" +
         "Password=12345;";

        //public String coneccion = "Data Source=DESKTOP-5S8K8TU;Initial Catalog=DbExpedientes;Integrated Security=True";
        //public String coneccion = "Server=localhost, Authentication=Windows Authentication, Database= DbExpediente";
        //public String coneccion = "Data Source=LEONY-PC;Initial Catalog=NORTHWND;Integrated Security=True";

        public Boolean Conectando()
        {
            try
            {
                SqlConnection cnn = new SqlConnection(this.coneccion);//Probamos que ahy coneccion.
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

        public bool isconsulta(string op)
        {
            if (op == "i" || op == "u" || op == "d")
            {
                return false;
            }
            else
            if (op == "g" || op == "b" || op == "l" || op == "lb")
            {
                return true;
            }
            return false;
        }
    }
}