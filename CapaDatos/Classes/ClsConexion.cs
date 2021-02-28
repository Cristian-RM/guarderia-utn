using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
  public  class ClsConexion
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
    }
}
