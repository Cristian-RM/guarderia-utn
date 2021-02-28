using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsDetalleConsumos : ClsConexion
    {
        public int IDfactura;
        public int IDconsumo;
        public String FechaCreacion;
        ///Atributos
        ///
        public ClsDetalleConsumos()
        {
            this.IDfactura = 0;
            this.IDconsumo = 0;
            this.FechaCreacion = "";
        }

        public ClsDetalleConsumos(int pIDfactura, int pIDconsumo, String pFechaCreacion)
        {
            this.IDfactura = pIDfactura;
            this.IDconsumo = pIDconsumo;
            this.FechaCreacion = pFechaCreacion;
        }

        public String MantenimientoDetalleConsumo(ClsDetalleConsumos pClsDeConsumo, String pOperacion)
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
                    coneccion.CommandText = "stp_CPDetalleConsumos_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aIDfactura", pClsDeConsumo.IDfactura);
                    coneccion.Parameters.AddWithValue("@aIDconsumo", pClsDeConsumo.IDconsumo);
                    coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsDeConsumo.FechaCreacion);
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
        public DataSet GetListaDetalleCosumo(ClsDetalleConsumos pClsDeConsumo, String pOperacion)
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
                coneccion.CommandType = CommandType.StoredProcedure;
                coneccion.CommandText = "stp_CPDetalleConsumos_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aIDfactura", pClsDeConsumo.IDfactura);
                coneccion.Parameters.AddWithValue("@aIDconsumo", pClsDeConsumo.IDconsumo);
                coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsDeConsumo.FechaCreacion);
                coneccion.Parameters.AddWithValue("@aOperacion", pOperacion);
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
