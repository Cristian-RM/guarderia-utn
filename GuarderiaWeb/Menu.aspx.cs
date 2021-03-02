using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void tblEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = tbl.Rows[tbl.SelectedIndex];
            try
            {
                Session["op"] = "u";

                txtID.Text = row.Cells[1].Text;
                txtID.ReadOnly = true;
                txtNombre.Text = row.Cells[2].Text;
                txtPrecio.Text = row.Cells[3].Text;
                btnEliminar.Visible = true;
                tbl.SelectedIndex = -1;
                alertModal.Visible = false;

                Session["menu"] = txtID.Text;
                Session["menuNombre"] = txtNombre.Text;

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

            txtID.Text = "";
            txtPrecio.Text = "";

            txtID.ReadOnly = true;
            txtNombre.Text = "";

            alertModal.Visible = false;
            btnEliminar.Visible = false;

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
            informar(op);
            return sqlExecute(op);
        }

        private DataTable sqlExecute(string op)
        {
            GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
            DataTable result = Ws.crudMenus(parse(txtID.Text), txtNombre.Text, parseDecimal(txtPrecio.Text), op)[0];
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

        private Decimal parseDecimal(String num)
        {
            try
            {
                return Convert.ToDecimal(num);
            }
            catch (Exception)
            {
                return 0;
            }
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
            Response.Redirect("PlatosDEmenu.aspx");
        }
    }
}