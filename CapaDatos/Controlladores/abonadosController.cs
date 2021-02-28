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
     public class abonadosController : ClsConexion
    {
            public  ClsAbonados abono{ get; set; }
            public DataTable dataTable { get; set; }
            public int filasAfectadas { get; set; }
            public int numError { get; set; }
            public string mensajeError { get; set; }
            public string operacion { get; set; }

            public abonadosController(ClsAbonados abono)
            {
                this.abono = abono;
                filasAfectadas = 0;
                dataTable = new DataTable();
            }

            public abonadosController()
            {
                this.abono = new ClsAbonados();
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

                    SqlCommand coneccion = new SqlCommand("stp_CPABONADOS_CRUD", conectado);
                    //coneccion.Connection = conectado;
                    coneccion.CommandType = CommandType.StoredProcedure;

                    //Parámetros

                    coneccion.Parameters.Add(Parametro("@aID", this.abono.ID));
                    coneccion.Parameters.Add(Parametro("@aIDchildRelation", this.abono.IDchildRelation));
                    coneccion.Parameters.Add(Parametro("@aDNI", this.abono.DNI));
                    coneccion.Parameters.Add(Parametro("@aNombre", this.abono.Nombre));
                    coneccion.Parameters.Add(Parametro("@aDireccion", this.abono.Direccion));
                    coneccion.Parameters.Add(Parametro("@aTelefono", this.abono.Telefono));
                    coneccion.Parameters.Add(Parametro("@aBanco", this.abono.Banco));
                    coneccion.Parameters.Add(Parametro("@@aCuentaIBAM", this.abono.CuentaIBAM));
                
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

            public String crud(string operacion, ClsAbonados abono)
            {
                this.abono = abono;
                this.operacion = operacion;
                return crud();
            }

            public String crud(string operacion)
            {
                this.operacion = operacion;
                return crud();
            }

            public String crud(ClsAbonados abono)
            {
                this.abono = abono;
                return crud();
            }

            private void construirObjeto(DataTable data)
            {
                foreach (DataRow row in data.Rows)
                {
                    this.abono = new ClsAbonados();
                    try
                    {
                    this.abono.ID = row.Field<int>("ID");
                    this.abono.IDchildRelation = row.Field<int>("IDchildRelation");
                    this.abono.DNI = row.Field<string>("DNI");
                    this.abono.Nombre = row.Field<String>("Nombre");
                    this.abono.Direccion = row.Field<String>("Direccion");
                    this.abono.Telefono = row.Field<string>("Telefono");
                    this.abono.Banco = row.Field<String>("Banco");
                    this.abono.CuentaIBAM = row.Field<string>("CuentaIBAM");
                }
                catch (Exception ex)
                    {
                        this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                    }
                }
            }
        }
 }
