using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsMenus : ClsConexion
    {
        public String Nombre;
        public decimal Precio;
        ///Atributos
        ///
        public ClsMenus()
        {
            this.Nombre = "";
            this.Precio = 0;
        }

        public ClsMenus( String pNombre, decimal pPrecio)
        {
            this.Nombre = pNombre;
            this.Precio = pPrecio;
        }

        public String MantenimientoMenus(ClsMenus pClsMenu, String pOperacion)
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
                    coneccion.CommandText = "stp_CPMenus_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aNombre", pClsMenu.Nombre);
                    coneccion.Parameters.AddWithValue("@aPrecio", pClsMenu.Precio);
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
        public DataSet GetListaMenu(ClsMenus pClsMenu, String pOperacion)
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
                coneccion.CommandText = "stp_CPMenus_CRUD";
                coneccion.Parameters.AddWithValue("@aNombre", pClsMenu.Nombre);
                coneccion.Parameters.AddWithValue("@aDireccion", pClsMenu.Precio);
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

