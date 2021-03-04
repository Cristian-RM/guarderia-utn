using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Plato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = ""; Session["plato"] = "";
                cargarTabla();
            }
            else
            {
            }
            cargarTabla();
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
                DataTable[] r = sqlExecute("l");

                tbl.DataSource = r[0];
                tbl.DataBind();
            }
            catch (Exception es)
            {
                m = es.Message;
                informar(m);
            }
        }

        protected void tblEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = tbl.Rows[tbl.SelectedIndex];
            try
            {
                Session["op"] = "u";

                txtNombre.Text = row.Cells[1].Text;
                txtNombre.ReadOnly = true;
                btnEliminar.Visible = true;
                tbl.SelectedIndex = -1;
                alertModal.Visible = false;
                Session["plato"] = txtNombre.Text;
                btnSempleadoAgregar.Visible = true;
                encerderModal(1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Session["op"] = "i";

            txtNombre.Text = "";
            txtNombre.ReadOnly = false;

            alertModal.Visible = false;
            btnEliminar.Visible = false;
            btnSempleadoAgregar.Visible = false;

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
            String m = "";
            try
            {
                sqlExecute();

                btnEliminar.Visible = true;
                cargarTabla();
            }
            catch (Exception ed)
            {
                encerderModal(1);
                m = m + "  " + ed.Message;
            }
            informar(m);
        }

        private DataTable[] sqlExecute()
        {
            String op = (string)Session["op"];

            return sqlExecute(op);
        }

        private DataTable[] sqlExecute(string op)
        {
            GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
            DataTable[] result = Ws.crudPlatos(txtNombre.Text, op);

            return result;
        }

        private int numErr(DataTable infotable)
        {
            try
            {
                DataRow r = infotable.Rows[infotable.Rows.Count - 1];

                return Convert.ToInt32(r.Field<int>("numeroDeError"));
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
            catch (Exception ex)
            {
                informar(ex.Message);
            }
        }

        protected void btnSempleadoAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarIngredientes.aspx");
        }
    }
}