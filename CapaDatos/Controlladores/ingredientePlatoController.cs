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
    public class ingredientePlatoController : ClsConexion
    {
        public ClsIngredientePlato ingredientePlato { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public ingredientePlatoController(ClsIngredientePlato ingredientePlato)
        {
            this.ingredientePlato = ingredientePlato;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public ingredientePlatoController()
        {
            this.ingredientePlato = new ClsIngredientePlato();
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

                SqlCommand coneccion = new SqlCommand("stp_CPINGREDIENTESDEPLATO_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.ingredientePlato.ID));
                coneccion.Parameters.Add(Parametro("@aNombrePlato", this.ingredientePlato.NombrePlato));
                coneccion.Parameters.Add(Parametro("@aNombreIngrediente", this.ingredientePlato.nombreIngrediente));

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

        public String crud(string operacion, ClsIngredientePlato ingredientePlato)
        {
            this.ingredientePlato = ingredientePlato;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsIngredientePlato ingredientePlato)
        {
            this.ingredientePlato = ingredientePlato;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.ingredientePlato = new ClsIngredientePlato();
                try
                {
                    this.ingredientePlato.ID = row.Field<int>("ID");
                    this.ingredientePlato.NombrePlato = row.Field<string>("NombrePlato");
                    this.ingredientePlato.nombreIngrediente = row.Field<string>("NombreIngrediente");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}