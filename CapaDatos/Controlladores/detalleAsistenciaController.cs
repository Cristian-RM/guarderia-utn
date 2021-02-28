using CapaDatos.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Controlladores
{
    public class detalleAsistenciaController : ClsConexion
    {
        public ClsDetalleAsistencia Dasisencia { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public detalleAsistenciaController(ClsDetalleAsistencia dasisencia)
        {
            this.Dasisencia = dasisencia;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public detalleAsistenciaController()
        {
            this.Dasisencia = new ClsDetalleAsistencia();
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public String crud()
        {
            try
            {
                SqlDataAdapter adapter;

                //creamos nuestra propia coneccion
                SqlConnection conectado = new SqlConnection(this.coneccion);
                conectado.Open();

                SqlCommand coneccion = new SqlCommand("stp_CPDetalleAsistencia_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.Dasisencia.ID));
                coneccion.Parameters.Add(Parametro("@aIDfactura", this.Dasisencia.IDfactura));
                coneccion.Parameters.Add(Parametro("@aIDasistencia", this.Dasisencia.IDasistencia));
                coneccion.Parameters.Add(Parametro("@aFechaCreacion", this.Dasisencia.FechaCreacion));

                coneccion.Parameters.Add(Parametro("@pOperacion", this.operacion));

                SqlParameter numError = new SqlParameter("@pnumErr", SqlDbType.Int, int.MaxValue);
                numError.Direction = ParameterDirection.Output;
                coneccion.Parameters.Add(numError);

                SqlParameter MensajeError = new SqlParameter("@pMensajeError", SqlDbType.VarChar, int.MaxValue);
                MensajeError.Direction = ParameterDirection.Output;
                coneccion.Parameters.Add(MensajeError);

                if (isconsulta(operacion))
                {
                    //////Consultar datos
                    adapter = new SqlDataAdapter(coneccion);
                    adapter.Fill(dataTable);
                    if (operacion == this.gets)
                    {
                        construirObjeto(dataTable);
                    }
                }
                else
                {
                    filasAfectadas = coneccion.ExecuteNonQuery();
                }

                conectado.Close();

                this.numError = (int)numError.Value;
                this.mensajeError = (string)MensajeError.Value;
            }
            catch (Exception Ex)
            {
                if (Conectando())
                {
                    cnn.Close();
                }
                this.mensajeError = Ex.Message;
            }
            return mensajeError;
        }

        public String crud(string operacion, ClsDetalleAsistencia dasisencia)
        {
            this.Dasisencia = dasisencia;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsDetalleAsistencia dasisencia)
        {
            this.Dasisencia = dasisencia;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.Dasisencia = new ClsDetalleAsistencia();
                try
                {
                    this.Dasisencia.ID = row.Field<int>("ID");
                    this.Dasisencia.IDfactura = row.Field<int>("IDchild");
                    this.Dasisencia.IDasistencia = row.Field<int>("FechaRegistro");
                    this.Dasisencia.FechaCreacion = row.Field<String>("MES");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}