using CapaDatos.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos.Controlladores
{
    public class childRelationController : ClsConexion
    {
        public ClsChildRelations childRela { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public childRelationController(ClsChildRelations childRela)
        {
            this.childRela = childRela;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public childRelationController()
        {
            this.childRela = new ClsChildRelations();
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

                SqlCommand coneccion = new SqlCommand("stp_CPchildsRelations_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.childRela.ID));
                coneccion.Parameters.Add(Parametro("@aIDchild", this.childRela.IDchild));
                coneccion.Parameters.Add(Parametro("@aTipoRelacion", this.childRela.TipoRelacion));

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

        public String crud(string operacion, ClsChildRelations childRela)
        {
            this.childRela = childRela;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsChildRelations childRela)
        {
            this.childRela = childRela;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.childRela = new ClsChildRelations();
                try
                {
                    this.childRela.ID = row.Field<int>("ID");
                    this.childRela.IDchild = row.Field<int>("IDchild");
                    this.childRela.TipoRelacion = row.Field<string>("TipoRelacion");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}