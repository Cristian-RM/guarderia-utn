using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsDetalleAsistencia : ClsConexion
    {
        public int IDfactura;
        public int IDasistencia;
        public String FechaCreacion;
        ///Atributos
        ///
        public ClsDetalleAsistencia()
        {
            this.IDfactura = 0;
            this.IDasistencia = 0;
            this.FechaCreacion = "";
        }

        public ClsDetalleAsistencia(int pIDfactura, int pIDasistencia, String pFechaCreacion)
        {
            this.IDfactura = pIDfactura;
            this.IDasistencia = pIDasistencia;
            this.FechaCreacion = pFechaCreacion;
        }

        public String MantenimientoDeAsistencia(ClsDetalleAsistencia pClsDeAsiste, String pOperacion)
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
                    coneccion.CommandText = "stp_CPDetalleAsistencia_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aIDfactura", pClsDeAsiste.IDfactura);
                    coneccion.Parameters.AddWithValue("@aIDasistencia", pClsDeAsiste.IDasistencia);
                    coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsDeAsiste.FechaCreacion);
                    coneccion.Parameters.AddWithValue("@aFechaCreacion", pOperacion);
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
        public DataSet GetListaDeAsistencia(ClsDetalleAsistencia pClsDeAsiste, String pOperacion)
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
                coneccion.CommandText = "stp_CPDetalleAsistencia_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aIDfactura", pClsDeAsiste.IDfactura);
                coneccion.Parameters.AddWithValue("@aIDasistencia", pClsDeAsiste.IDasistencia);
                coneccion.Parameters.AddWithValue("@aFechaCreacion", pClsDeAsiste.FechaCreacion);
                coneccion.Parameters.AddWithValue("@aFechaCreacion", pOperacion);
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
