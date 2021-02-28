using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsMfacturas : ClsConexion
    {
        public int IDabonado;
        public String Mes;
        public String FechaCreacion;
        ///Atributos
        ///
        public ClsMfacturas()
        {
            this.IDabonado = 0;
            this.Mes = "";
            this.FechaCreacion = "";
        }

        public ClsMfacturas(int pIDabonado, String pMes, String pFechaCreacion)
        {
            this.IDabonado = pIDabonado;
            this.Mes = pMes;
            this.FechaCreacion = pFechaCreacion;
        }

        public String MantenimientoMfacturas(ClsMfacturas pClsFactura, String pOperacion)
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
                    coneccion.CommandText = "stp_CPMfacturas_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aIDabonado", pClsFactura.IDabonado);
                    coneccion.Parameters.AddWithValue("@aMes", pClsFactura.Mes);
                    coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsFactura.FechaCreacion);
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
        public DataSet GetListaMfactura(ClsMfacturas pClsFactura, String pOperacion)
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
                coneccion.CommandText = "stp_CPMfacturas_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aIDabonado", pClsFactura.IDabonado);
                coneccion.Parameters.AddWithValue("@aMes", pClsFactura.Mes);
                coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsFactura.FechaCreacion);
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
