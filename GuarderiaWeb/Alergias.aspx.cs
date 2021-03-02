using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Alergias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = "";
                validarSessino();
                cargarTabla();
                cargarIngredientes();
            }
            else
            {
            }
            cargarTabla();
        }

        private int validarSessino()
        {
            try
            {
                int child = Convert.ToInt32((String)Session["IDchild"]);

                ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudChild(child, "", "", "", "g");

                lblbebe.Text = result.Rows[0].Field<string>("Nombre");
                txtIDmatricula.Text = Convert.ToString(child);
                txtIDmatricula.ReadOnly = true;

                if (child == -1)

                {
                    Response.Redirect("Child.aspx"); return child;
                }
                return child;
            }
            catch (Exception)
            {
                Response.Redirect("Child.aspx");
                return -1;
            }
        }

        public void informar(String m)
        {
            AlertFooter.Visible = true;
            informacion.Text = m;
            informacion.DataBind();
        }

        public void cargarIngredientes()
        {
            string m = "";
            try
            {
                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudIngredientes("", "l")[0];
                txtIngrediente.DataSource = result;
                txtIngrediente.DataTextField = "Nombre";
                txtIngrediente.DataValueField = "Nombre";
                txtIngrediente.DataBind();
            }
            catch (Exception es)
            {
                m = es.Message;
                informar(m);
            }
        }

        public void cargarTabla()
        {
            string m = "";
            try
            {
                DataTable r = sqlExecute("lb");

                tbl.DataSource = r;
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
            try
            {
                GridViewRow row = tbl.Rows[tbl.SelectedIndex];
                Session["op"] = "u";
                TxtID.Text = row.Cells[1].Text;
                txtIDmatricula.Text = row.Cells[3].Text;

                ListItem item = txtIngrediente.Items.FindByValue(row.Cells[2].Text);
                txtIngrediente.SelectedIndex = txtIngrediente.Items.IndexOf(item);
                txtIngrediente.Enabled = false;

                btnEliminar.Visible = true;
                tbl.SelectedIndex = -1;

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
            TxtID.Text = "";
            txtIngrediente.Enabled = true;
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
            try
            {
                sqlExecute();
                tbl.SelectedIndex = -1;
                btnEliminar.Visible = true;
                cargarTabla();
            }
            catch (Exception)
            {
                encerderModal(1);
            }
        }

        private DataTable sqlExecute()
        {
            String op = (string)Session["op"];

            return sqlExecute(op);
        }

        private DataTable sqlExecute(string op)
        {
            GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
            DataTable result = Ws.crudAlergias((parse(TxtID.Text)), (parse(txtIDmatricula.Text)), txtIngrediente.SelectedValue, op)[0];
            Ws.Close();
            return result;
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
        }
    }
}