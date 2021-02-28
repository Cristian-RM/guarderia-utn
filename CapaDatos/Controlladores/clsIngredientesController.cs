﻿using CapaDatos.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Controlladores
{
    public class clsIngredientesController : ClsConexion

    {
        public ClsChilds child { get; set; }
        public DataTable dataTable { get; set; }
        public int filasAfectadas { get; set; }
        public int numError { get; set; }
        public string mensajeError { get; set; }
        public string operacion { get; set; }

        public clsIngredientesController(ClsChilds child)
        {
            this.child = child;
            filasAfectadas = 0;
            dataTable = new DataTable();
        }

        public clsIngredientesController()
        {
            this.child = new ClsChilds();
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

                SqlCommand coneccion = new SqlCommand("stp_CPChilds_CRUD", conectado);
                //coneccion.Connection = conectado;
                coneccion.CommandType = CommandType.StoredProcedure;

                //Parámetros

                coneccion.Parameters.Add(Parametro("@aIDmatricula", this.child.IDmatricula));
                coneccion.Parameters.Add(Parametro("@aNombre", this.child.Nombre));
                coneccion.Parameters.Add(Parametro("@aFechaRegistro", this.child.FechaRegistro));
                coneccion.Parameters.Add(Parametro("@aFechaNacimiento", this.child.FEchaNacimiento));

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

        public String crud(string operacion, ClsChilds child)
        {
            this.child = child;
            this.operacion = operacion;
            return crud();
        }

        public String crud(string operacion)
        {
            this.operacion = operacion;
            return crud();
        }

        public String crud(ClsChilds child)
        {
            this.child = child;
            return crud();
        }

        private void construirObjeto(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                this.child = new ClsChilds();
                try
                {
                    this.child.IDmatricula = row.Field<int>("IDmatricula");
                    this.child.Nombre = row.Field<string>("Nombre");
                    this.child.FechaRegistro = row.Field<string>("FechaRegistro");
                    this.child.FEchaNacimiento = row.Field<string>("FechaNacimiento");
                }
                catch (Exception ex)
                {
                    this.mensajeError += "  (No se pudo construir el objeto local por " + ex.Message + ")";
                }
            }
        }
    }
}