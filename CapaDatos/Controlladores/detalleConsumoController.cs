using CapaDatos.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos.Controlladores
{
    public class detalleConsumoController : ClsConexion
    {
        public ClsDetalleConsumos deConsu { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public detalleConsumoController(ClsDetalleConsumos deConsu)
        {
            this.deConsu = deConsu;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public detalleConsumoController()
        {
            this.deConsu = new ClsDetalleConsumos();
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

                SqlCommand coneccion = new SqlCommand("stp_CPDetalleConsumos_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.deConsu.ID));
                coneccion.Parameters.Add(Parametro("@aIDfactura", this.deConsu.IDfactura));
                coneccion.Parameters.Add(Parametro("@aIDconsumo", this.deConsu.IDconsumo));
                coneccion.Parameters.Add(Parametro("@aFechaCreacion", this.deConsu.FechaCreacion));

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

        public String crud(string operacion, ClsDetalleConsumos deConsu)
        {
            this.deConsu = deConsu;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsDetalleConsumos deConsu)
        {
            this.deConsu = deConsu;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.deConsu = new ClsDetalleConsumos();
                try
                {
                    this.deConsu.ID = row.Field<int>("ID");
                    this.deConsu.IDfactura = row.Field<int>("IDfactura");
                    this.deConsu.IDconsumo = row.Field<int>("IDconsumo");
                    this.deConsu.FechaCreacion = row.Field<String>("FechaCreacion");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}