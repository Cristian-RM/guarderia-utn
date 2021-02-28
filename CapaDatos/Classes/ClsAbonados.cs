using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsAbonados : ClsConexion
    {
        public int IDchildRelation;
        public String DNI;
        public String Nombre;
        public String Direccion;
        public String Telefono;
        public String Banco;
        public String CuentaIBAM;
        ///Atributos
        ///
        public ClsAbonados()
        {
            this.IDchildRelation = 0;
            this.DNI = "";
            this.Nombre = "";
            this.Direccion = "";
            this.Telefono = "";
            this.Banco = "";
            this.CuentaIBAM = "";
        }

        public ClsAbonados(int pIDchildRelation, string pDNI, String pNombre, String pDireccion, String pTelefono, String pBanco, String pCuentaIBAM)
        {
            this.IDchildRelation = pIDchildRelation;
            this.DNI = pDNI;
            this.Nombre = pNombre;
            this.Direccion = pDireccion;
            this.Telefono = pTelefono;
            this.Banco = pBanco;
            this.CuentaIBAM = pCuentaIBAM;
        }

        public String MantenimientoAbonados(ClsAbonados pClsAbonado, String pOperacion)
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
                    coneccion.CommandText = "stp_CPABONADOS_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aIDchildRelation", pClsAbonado.IDchildRelation);
                    coneccion.Parameters.AddWithValue("@aDNI", pClsAbonado.DNI);
                    coneccion.Parameters.AddWithValue("@aNombre", pClsAbonado.Nombre);
                    coneccion.Parameters.AddWithValue("@aDireccion", pClsAbonado.Direccion);
                    coneccion.Parameters.AddWithValue("@aTelefono", pClsAbonado.Telefono);
                    coneccion.Parameters.AddWithValue("@aBanco", pClsAbonado.Banco);
                    coneccion.Parameters.AddWithValue("@aCuentaIBAM", pClsAbonado.CuentaIBAM);
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
        public DataSet GetListaAbonados(ClsAbonados pClsAbonado, String pOperacion)
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
                coneccion.CommandText = "stp_CPABONADOS_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aIDchildRelation", pClsAbonado.IDchildRelation);
                coneccion.Parameters.AddWithValue("@aDNI", pClsAbonado.DNI);
                coneccion.Parameters.AddWithValue("@aNombre", pClsAbonado.Nombre);
                coneccion.Parameters.AddWithValue("@aDireccion", pClsAbonado.Direccion);
                coneccion.Parameters.AddWithValue("@aTelefono", pClsAbonado.Telefono);
                coneccion.Parameters.AddWithValue("@aBanco", pClsAbonado.Banco);
                coneccion.Parameters.AddWithValue("@aCuentaIBAM", pClsAbonado.CuentaIBAM);
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
