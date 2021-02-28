using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
  public  class ClsEncargados : ClsConexion
    {
        public int IDchildRelation;
        public String DNI;
        public String Nombre;
        public String Direccion;
        public String Telefono;
        ///Atributos
        ///
        public ClsEncargados()
        {
            this.IDchildRelation = 0;
            this.DNI = "";
            this.Nombre = "";
            this.Direccion = "";
            this.Telefono = "";
        }

        public ClsEncargados(int pIDchildRelation,string pDNI, String pNombre, String pDireccion, String pTelefono)
        {
            this.IDchildRelation = pIDchildRelation;
            this.DNI = pDNI;
            this.Nombre = pNombre;
            this.Direccion = pDireccion;
            this.Telefono = pTelefono;
        }

        public String MantenimientoEncargados(ClsEncargados pClsEncargado, String pOperacion)
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
                    coneccion.CommandText = "stp_CPEncargados_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aIDchildRelation", pClsEncargado.IDchildRelation);
                    coneccion.Parameters.AddWithValue("@aDNI", pClsEncargado.DNI);
                    coneccion.Parameters.AddWithValue("@aNombre", pClsEncargado.Nombre);
                    coneccion.Parameters.AddWithValue("@aDireccion", pClsEncargado.Direccion);
                    coneccion.Parameters.AddWithValue("@aTelefono", pClsEncargado.Telefono);
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
        public DataSet GetListaEncargado(ClsEncargados pClsEncargado, String pOperacion)
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
                coneccion.CommandText = "stp_CPEncargados_CRUD";
                coneccion.Parameters.AddWithValue("@aIDchildRelation", pClsEncargado.IDchildRelation);
                coneccion.Parameters.AddWithValue("@aDNI", pClsEncargado.DNI);
                coneccion.Parameters.AddWithValue("@aNombre", pClsEncargado.Nombre);
                coneccion.Parameters.AddWithValue("@aDireccion", pClsEncargado.Direccion);
                coneccion.Parameters.AddWithValue("@aTelefono", pClsEncargado.Telefono);
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
