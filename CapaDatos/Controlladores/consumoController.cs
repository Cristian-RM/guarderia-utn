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
    public class consumoController : ClsConexion
    {
        public ClsConsumos consu { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public consumoController(ClsConsumos consu)
        {
            this.consu = consu;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public consumoController()
        {
            this.consu = new ClsConsumos();
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

                SqlCommand coneccion = new SqlCommand("stp_CPConsumos_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros
                coneccion.Parameters.Add(Parametro("@aID", this.consu.ID));
                coneccion.Parameters.Add(Parametro("@aIDchild", this.consu.IDchild));
                coneccion.Parameters.Add(Parametro("@aIDmenu", this.consu.Idmenu));
                coneccion.Parameters.Add(Parametro("@aFechaConsumo", this.consu.FechaConsumo));
                coneccion.Parameters.Add(Parametro("@aSnCancelado", this.consu.SnCancelado));

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

        public String crud(string operacion, ClsConsumos consu)
        {
            this.consu = consu;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsConsumos consu)
        {
            this.consu = consu;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.consu = new ClsConsumos();
                try
                {
                    this.consu.ID = row.Field<int>("ID");
                    this.consu.IDchild = row.Field<int>("IDchild");
                    this.consu.Idmenu = row.Field<int>("Idmenu");
                    this.consu.FechaConsumo = row.Field<string>("FechaConsumo");
                    this.consu.SnCancelado = this.consu.isactivoBYTE(row.Field<String>("SNCANCELADO"));
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}