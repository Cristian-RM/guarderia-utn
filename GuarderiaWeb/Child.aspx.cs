using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Child : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IDchild"] = "";
                Session["op"] = "";
                cargarTabla();
            }
            else
            {
            }
        }

        public void informar(String m)
        {
            AlertFooter.Visible = true;
            informacion.Text = m;
            informacion.DataBind();
            informarModal(m);
        }

        public void informarModal(String m)
        {
            alertModal.Visible = true;
            lbLAlertModal.Text = m;
            informacion.DataBind();
        }

        public void cargarTabla()
        {
            string m = "";
            try
            {
                DataTable r = sqlExecute("l");

                tbl.DataSource = r;
                tbl.DataBind();
            }
            catch (Exception es)
            {
                m = es.Message;
                informar(m);
            }
        }

        //public void cargarEmpresas()
        //{
        //    string m = "";
        //    try
        //    {
        //        clsEmpresaController empresaC = new clsEmpresaController();
        //        empresaC.crud(empresaC.list); //listar
        //        lstCODCIA.DataSource = empresaC.dataTable;
        //        lstCODCIA.DataTextField = "NOMBRE_CIA";
        //        lstCODCIA.DataValueField = "CODCIA";
        //        lstCODCIA.DataBind();
        //        m = empresaC.mensajeError;
        //    }
        //    catch (Exception es)
        //    {
        //        m = es.Message;
        //        informar(m);
        //    }
        //}

        protected void tblEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = tbl.Rows[tbl.SelectedIndex];
                Session["op"] = "u";
                txtIDmatricula.Text = row.Cells[1].Text;
                txtNombre.Text = row.Cells[2].Text;
                txtFechaRegistro.Text = row.Cells[3].Text;
                txtFechaNacimiento.Text = row.Cells[4].Text;
                txtFechaRegistro.ReadOnly = true;
                Session["IDchild"] = row.Cells[1].Text;
                //btnSempleadoAgregar.Visible = true;
                btnEliminar.Visible = true;
                alertModal.Visible = false;
                btnSempleadoAgregar.Visible = true;
                encerderModal(1);
            }
            catch (Exception)
            {
                cargarTabla();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Session["op"] = "i";

            txtNombre.Text = "";
            txtFechaNacimiento.Text = "";
            txtIDmatricula.Text = "";
            txtFechaRegistro.Text = "";
            txtFechaRegistro.ReadOnly = true;

            alertModal.Visible = false;
            btnEliminar.Visible = false;
            btnSempleadoAgregar.Visible = false;
            tbl.SelectedIndex = -1;
            encerderModal(1);
        }

        public void encerderModal(int op)
        {
            if (op == 1)
            {
                string encenderModal = "$('#staticBackdrop').modal('show')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", encenderModal, true);
            }
            else if (op == 1)
            {
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                sqlExecute();
                tbl.SelectedIndex = -1;
                btnEliminar.Visible = true;
            }
            catch (Exception)
            {
                encerderModal(1);
            }
            cargarTabla();
        }

        private DataTable sqlExecute()
        {
            String op = (string)Session["op"];

            return sqlExecute(op);
        }

        private DataTable sqlExecute(string op)
        {
            GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
            DataTable result = Ws.crudChild(parse(txtIDmatricula.Text), txtNombre.Text, txtFechaRegistro.Text, txtFechaNacimiento.Text, op);
            Ws.Close();
            return result;
        }

        private int numErr(DataTable infotable)
        {
            try
            {
                //DataRow r = infotable.Rows[infotable.Rows.Count - 1];
                informar(infotable.TableName);
                //return Convert.ToInt32(r.Field<int>("numeroDeError"));
            }
            catch (Exception i)
            {
                informar(i.Message);
            }
            return 0;
        }

        private string mensajeERROR(DataTable infotable)
        {
            try
            {
                DataRow r = infotable.Rows[0];
                return (r.Field<string>("mensajeDeError"));
            }
            catch (Exception i)
            {
                informar(i.Message);
            }
            return "";
        }

        private int parse(String num)
        {
            try
            {
                return Convert.ToInt32(num);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                sqlExecute("d");
                cargarTabla();
            }
            catch (Exception)
            {
                cargarTabla();
            }
        }

        protected void btnSempleadoAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Familiares.aspx");
        }

        protected void BTNABONADO_Click(object sender, EventArgs e)
        {
            Response.Redirect("Alergias.aspx");
        }
    }
}