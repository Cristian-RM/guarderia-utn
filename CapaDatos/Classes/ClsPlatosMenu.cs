using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    internal class ClsPlatosMenu : Entidad
    {
        public String NombrePlato;
        public int IDmenu;

        ///Atributos
        ///
        public ClsPlatosMenu()
        {
            this.NombrePlato = "";
            this.IDmenu = 0;
        }

        public ClsPlatosMenu(String pNombrePlato, int pIDmenu)
        {
            this.NombrePlato = pNombrePlato;
            this.IDmenu = pIDmenu;
        }

        public String MantenimientoPlatoMenu(ClsPlatosMenu pClsPlatoMenu, String pOperacion)
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
                    coneccion.CommandText = "stp_CPPLATOSDEMENU_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                    coneccion.CommandTimeout = 10;
                    coneccion.Parameters.AddWithValue("@aNombrePlato", pClsPlatoMenu.NombrePlato);
                    coneccion.Parameters.AddWithValue("@aIDmenu", pClsPlatoMenu.IDmenu);
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

        public DataSet GetListaMenu(ClsPlatosMenu pClsPlatoMenu, String pOperacion)
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
                coneccion.CommandText = "stp_CPPLATOSDEMENU_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aNombrePlato", pClsPlatoMenu.NombrePlato);
                coneccion.Parameters.AddWithValue("@aIDmenu", pClsPlatoMenu.IDmenu);
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