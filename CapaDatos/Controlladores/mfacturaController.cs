﻿using CapaDatos.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos.Controlladores
{
    public class mfacturaController : ClsConexion
    {
        public ClsMfacturas mfactura { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public mfacturaController(ClsMfacturas mfactura)
        {
            this.mfactura = mfactura;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public mfacturaController()
        {
            this.mfactura = new ClsMfacturas();
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

                SqlCommand coneccion = new SqlCommand("stp_CPMfacturas_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aID", this.mfactura.ID));
                coneccion.Parameters.Add(Parametro("@aIDabonado", this.mfactura.IDabonado));
                coneccion.Parameters.Add(Parametro("@aMes", this.mfactura.Mes));
                coneccion.Parameters.Add(Parametro("@aFechaCreacion", this.mfactura.FechaCreacion));

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

        public String crud(string operacion, ClsMfacturas mfactura)
        {
            this.mfactura = mfactura;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsMfacturas mfactura)
        {
            this.mfactura = mfactura;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.mfactura = new ClsMfacturas();
                try
                {
                    this.mfactura.ID = row.Field<int>("ID");
                    this.mfactura.IDabonado = row.Field<int>("IDabonado");
                    this.mfactura.Mes = row.Field<string>("Mes");
                    this.mfactura.FechaCreacion = row.Field<String>("FechaCreacion");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}