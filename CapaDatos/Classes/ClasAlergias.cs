using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClasAlergias : ClsConexion
    {
        public String NombreIngrediente;
        public int IDchild;
        ///Atributos
        ///
        public ClasAlergias()
        {
            this.NombreIngrediente = "";
            this.IDchild = 0;
        }

        public ClasAlergias(String pNombreIngrediente, int IDchild)
        {
            this.NombreIngrediente = pNombreIngrediente;
            this.IDchild = IDchild;
        }

        public String MantenimientoAsistencia(ClasAlergias pClsAsistencia, String pOperacion)
        {
            String vResultado = "";
            if (this.Conectando())//Probamos si ahy coneccion...
            {
                try
                {

                    //creamos nuestra propia coneccion
                    SqlConnection conectado = new SqlConnection(this.coneccion);
                    conectado.Open();
                    SqlCommand coneccion = new SqlCommand();
                    coneccion.Connection = conectado;
                    coneccion.CommandType = CommandType.StoredProcedure;
                    coneccion.CommandText = "stp_CPALERGIAS_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aNombreIngrediente", pClsAsistencia.NombreIngrediente);
                    coneccion.Parameters.AddWithValue("@aPIDchild", pClsAsistencia.IDchild);
                    coneccion.Parameters.AddWithValue("@aOperacion", pOperacion);
                    coneccion.ExecuteNonQuery();
                    conectado.Close();
                    vResultado = "Ejecutado con exito";


                }
                catch (Exception Ex)
                {
                    //MessageBox.Show(Ex.Message);
                    vResultado = Ex.Message;

                }
            }
            return vResultado;

        }

        private DataSet dataTable = new DataSet();
        public DataSet GetListaAlergias(ClasAlergias pClsAsistencia, String pOperacion)
        {
            try
            {
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                //creamos nuestra propia coneccion
                SqlConnection conectado = new SqlConnection(this.coneccion);
                conectado.Open();
                SqlCommand coneccion = new SqlCommand();
                coneccion.Connection = conectado;
                //coneccion.CommandType = System.Data.CommandType.StoredProcedure;
                coneccion.CommandText = "stp_CPALERGIAS_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aNombreIngrediente", pClsAsistencia.NombreIngrediente);
                coneccion.Parameters.AddWithValue("@aIDchild", pClsAsistencia.IDchild);
                coneccion.Parameters.AddWithValue("@aOperacion", pOperacion);
                adapter = new SqlDataAdapter(coneccion);
                adapter.Fill(dataTable);
                conectado.Close();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Console.WriteLine(Ex.Message);
            }
            return dataTable;


        }
    }
}
