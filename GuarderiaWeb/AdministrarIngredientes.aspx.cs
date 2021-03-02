using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Ingredientes_de_plato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = "";
                cargarTabla();
                lblPlato.Text = validarSessino();
                txtNombrePlato.Text = validarSessino();
                cargarIngredientes();
                txtID.ReadOnly = true;
                txtNombrePlato.ReadOnly = true;
            }
            else
            {
            }
            cargarTabla();
        }

        private String validarSessino()
        {
            try
            {
                String plato = (String)Session["plato"];
                if (plato.Length == 0)
                {
                    Response.Redirect("Plato.aspx"); return "";
                }
                return plato;
            }
            catch (Exception)
            {
                Response.Redirect("Plato.aspx");
                return "";
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

        public void cargarIngredientes()
        {
            string m = "";
            try
            {
                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudIngredientes("", "l")[0];
                txtNombreIngrediente.DataSource = result;
                txtNombreIngrediente.DataTextField = "Nombre";
                txtNombreIngrediente.DataValueField = "Nombre";
                txtNombreIngrediente.DataBind();
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
                ListItem item = txtNombreIngrediente.Items.FindByValue(row.Cells[3].Text);
                txtNombreIngrediente.SelectedIndex = txtNombreIngrediente.Items.IndexOf(item);
                txtNombreIngrediente.Enabled = false;

                alertModal.Visible = false;
                //txtNombre.Text = row.Cells[1].Text;
                //txtNombre.ReadOnly = true;
                //btnEliminar.Visible = true;
                //tbl.SelectedIndex = -1;
                //alertModal.Visible = false;

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
            alertModal.Visible = false;
            txtNombreIngrediente.Enabled = true;
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
            DataTable[] result = Ws.crudIngredientePlato(parse(txtID.Text), txtNombrePlato.Text, txtNombreIngrediente.Text, op);

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
        }
    }
}