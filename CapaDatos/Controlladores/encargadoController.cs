using CapaDatos.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos.Controlladores
{
    public class encargadoController : ClsConexion
    {
        public ClsEncargados enca { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public encargadoController(ClsEncargados enca)
        {
            this.enca = enca;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public encargadoController()
        {
            this.enca = new ClsEncargados();
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

                SqlCommand coneccion = new SqlCommand("stp_CPEncargados_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.enca.ID));
                coneccion.Parameters.Add(Parametro("@aIDchildRelation", this.enca.IDchildRelation));
                coneccion.Parameters.Add(Parametro("@aDNI", this.enca.DNI));
                coneccion.Parameters.Add(Parametro("@aNombre", this.enca.Nombre));
                coneccion.Parameters.Add(Parametro("@aDireccion", this.enca.Direccion));
                coneccion.Parameters.Add(Parametro("@aTelefono", this.enca.Telefono));

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

        public String crud(string operacion, ClsEncargados enca)
        {
            this.enca = enca;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsEncargados enca)
        {
            this.enca = enca;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.enca = new ClsEncargados();
                try
                {
                    this.enca.ID = row.Field<int>("ID");
                    this.enca.IDchildRelation = row.Field<int>("IDchildRelation");
                    this.enca.DNI = row.Field<string>("DNI");
                    this.enca.Nombre = row.Field<string>("Nombre");
                    this.enca.Direccion = row.Field<string>("Direccion");
                    this.enca.Telefono = row.Field<string>("Telefono");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}