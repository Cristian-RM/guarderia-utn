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
    internal class detalleAsistenciaController : ClsConexion
    {
        public ClsDetalleAsistencia dasisencia { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public detalleAsistenciaController(ClsDetalleAsistencia dasisencia)
        {
            this.dasisencia = dasisencia;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public detalleAsistenciaController()
        {
            this.dasisencia = new ClsDetalleAsistencia();
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

                coneccion.Parameters.Add(Parametro("@aID", this.dasisencia.ID));
                coneccion.Parameters.Add(Parametro("@aIDfactura", this.dasisencia.IDfactura));
                coneccion.Parameters.Add(Parametro("@aIDasistencia", this.dasisencia.IDasistencia));
                coneccion.Parameters.Add(Parametro("@aFechaCreacion", this.dasisencia.FechaCreacion));

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
            this.dasisencia = dasisencia;
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
            this.dasisencia = dasisencia;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.dasisencia = new ClsDetalleAsistencia();
                try
                {
                    this.dasisencia.ID = row.Field<int>("ID");
                    this.dasisencia.IDfactura = row.Field<int>("IDchild");
                    this.dasisencia.IDasistencia = row.Field<int>("FechaRegistro");
                    this.dasisencia.FechaCreacion = row.Field<String>("MES");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}