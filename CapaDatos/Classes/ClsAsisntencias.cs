using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Classes
{
    class ClsAsisntencias : ClsConexion
    {
        public int IDchild;
        public String FechaRegistro;
        public String Mes;
        public String HoraEntrada;
        public String HoraSalida;
        public String Detalles;
        public String SNCANCELADO;
        ///Atributos
        ///
        public ClsAsisntencias()
        {
            this.IDchild = 0;
            this.FechaRegistro = "";
            this.Mes = "";
            this.HoraEntrada = "";
            this.HoraSalida = "";
            this.Detalles = "";
            this.SNCANCELADO = "";
        }

        public ClsAsisntencias(int pIDchild, string pFechaRegistro, String pMes, String pHoraEntrada, String pHoraSalida, String Detalles, String pSNCANCELADO)
        {
            this.IDchild = pIDchild;
            this.FechaRegistro = pFechaRegistro;
            this.Mes = pMes;
            this.HoraEntrada = pHoraEntrada;
            this.HoraSalida = pHoraSalida;
            this.Detalles = Detalles;
            this.SNCANCELADO = pSNCANCELADO;
        }

        public String MantenimientoAsistencia(ClsAsisntencias pClsAsistencia, String pOperacion)
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
                    coneccion.Parameters.AddWithValue("@aIDchild", pClsAsistencia.IDchild);
                    coneccion.Parameters.AddWithValue("@aFechaRegistro", pClsAsistencia.FechaRegistro);
                    coneccion.Parameters.AddWithValue("@aMES", pClsAsistencia.Mes);
                    coneccion.Parameters.AddWithValue("@aHoraEntrada", pClsAsistencia.HoraEntrada);
                    coneccion.Parameters.AddWithValue("@aHoraSalida", pClsAsistencia.HoraSalida);
                    coneccion.Parameters.AddWithValue("@aDetalles", pClsAsistencia.Detalles);
                    coneccion.Parameters.AddWithValue("@aSNCANCELADO", pClsAsistencia.SNCANCELADO);
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
        public DataSet GetListaAsistencia(ClsAsisntencias pClsAsistencia, String pOperacion)
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
                coneccion.Parameters.AddWithValue("@aIDchild", pClsAsistencia.IDchild);
                coneccion.Parameters.AddWithValue("@aFechaRegistro", pClsAsistencia.FechaRegistro);
                coneccion.Parameters.AddWithValue("@aMES", pClsAsistencia.Mes);
                coneccion.Parameters.AddWithValue("@aHoraEntrada", pClsAsistencia.HoraEntrada);
                coneccion.Parameters.AddWithValue("@aHoraSalida", pClsAsistencia.HoraSalida);
                coneccion.Parameters.AddWithValue("@aDetalles", pClsAsistencia.Detalles);
                coneccion.Parameters.AddWithValue("@aSNCANCELADO", pClsAsistencia.SNCANCELADO);
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
