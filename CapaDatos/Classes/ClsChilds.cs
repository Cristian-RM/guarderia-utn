using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsChilds : ClsConexion
    {
        public String Nombre;
        public String FechaRegistro;
        public String FEchaNacimiento;
        ///Atributos
        ///
        public ClsChilds()
        {
            this.Nombre = "";
            this.FechaRegistro = "";
            this.FEchaNacimiento = "";

        }

        public ClsChilds( string pNombre, String pFechaRegistro, String pFEchaNacimiento)
        {
            this.Nombre = pNombre;
            this.FechaRegistro = pFechaRegistro;
            this.Nombre = pNombre;
            this.FEchaNacimiento = pFEchaNacimiento;
        }

        public String MantenimientoChilds(ClsChilds pClsChild, String pOperacion)
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
                    coneccion.CommandText = "stp_CPChilds_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aNombre", pClsChild.Nombre);
                    coneccion.Parameters.AddWithValue("@aFechaRegistro", pClsChild.FechaRegistro);
                    coneccion.Parameters.AddWithValue("@aFechaNacimiento", pClsChild.FEchaNacimiento);
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
        public DataSet GetListaChilds(ClsChilds pClsChild, String pOperacion)
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
                coneccion.CommandText = "stp_CPChilds_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aNombre", pClsChild.Nombre);
                coneccion.Parameters.AddWithValue("@aFechaRegistro", pClsChild.FechaRegistro);
                coneccion.Parameters.AddWithValue("@aFechaNacimiento", pClsChild.FEchaNacimiento);
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
