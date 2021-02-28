using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsConsumos : ClsConexion
    {
        public int IDchild;
        public int Idmenu;
        public String FechaConsumo;
        public int SnCancelado;
        ///Atributos
        ///
        public ClsConsumos()
        {
            this.IDchild = 0;
            this.Idmenu = 0;
            this.FechaConsumo = "";
            this.SnCancelado = 0;
        }

        public ClsConsumos(int pIDchild, int pIdmenu, String pFechaConsumo, int pSnCancelado)
        {
            this.IDchild = pIDchild;
            this.Idmenu = pIdmenu;
            this.FechaConsumo = pFechaConsumo;
            this.SnCancelado = pSnCancelado;
        }

        public String MantenimientoConsumo(ClsConsumos pClsConsumo, String pOperacion)
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
                    coneccion.Parameters.AddWithValue("@aIDchil", pClsConsumo.IDchild );
                    coneccion.Parameters.AddWithValue("@aIDmenu", pClsConsumo.Idmenu);
                    coneccion.Parameters.AddWithValue("@aFechaConsumo", pClsConsumo.FechaConsumo);
                    coneccion.Parameters.AddWithValue("@aSnCancelado", pClsConsumo.SnCancelado);
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
        public DataSet GetListaConsumo(ClsConsumos pClsConsumo, String pOperacion)
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
                coneccion.CommandText = "stp_CPEncargados_CRUD"; //Nombre de Nuestro Procedimiento Almacenado
                coneccion.Parameters.AddWithValue("@aIDchil", pClsConsumo.IDchild);
                coneccion.Parameters.AddWithValue("@aIDmenu", pClsConsumo.Idmenu);
                coneccion.Parameters.AddWithValue("@aFechaConsumo", pClsConsumo.FechaConsumo);
                coneccion.Parameters.AddWithValue("@aSnCancelado", pClsConsumo.SnCancelado);
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
