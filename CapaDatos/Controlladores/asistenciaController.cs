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
    class asistenciaController : ClsConexion
    {

        public ClsAsisntencias asis { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public asistenciaController(ClsAsisntencias asis)
        {
            this.asis = asis;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public asistenciaController()
        {
            this.asis = new ClsAsisntencias();
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

                SqlCommand coneccion = new SqlCommand("stp_CPAASISTENCIAS_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.asis.ID));
                coneccion.Parameters.Add(Parametro("@aIDchild", this.asis.IDchild));
                coneccion.Parameters.Add(Parametro("@aFechaRegistro", this.asis.FechaRegistro));
                coneccion.Parameters.Add(Parametro("@aMES", this.asis.Mes));
                coneccion.Parameters.Add(Parametro("@aHoraEntrada", this.asis.HoraEntrada));
                coneccion.Parameters.Add(Parametro("@aHoraSalida", this.asis.HoraSalida));
                coneccion.Parameters.Add(Parametro("@@aDetalles", this.asis.Detalles));
                coneccion.Parameters.Add(Parametro("@aSNCANCELADO", this.asis.SNCANCELADO));

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

        public String crud(string operacion, ClsAsisntencias asis)
        {
            this.asis = asis;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsAsisntencias asis)
        {
            this.asis = asis;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.asis = new ClsAsisntencias();
                try
                {
                    this.asis.ID = row.Field<int>("ID");
                    this.asis.IDchild = row.Field<int>("IDchild");
                    this.asis.FechaRegistro = row.Field<string>("FechaRegistro");
                    this.asis.Mes = row.Field<String>("MES");
                    this.asis.HoraEntrada = row.Field<String>("HoraEntrada");
                    this.asis.HoraSalida = row.Field<string>("HoraSalida");
                    this.asis.Detalles = row.Field<String>("Detalles");
                    this.asis.SNCANCELADO = this.asis.isactivoBYTE(row.Field<String>("SNCANCELADO"));

                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}
