using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Facturacion : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = "";

                cargarTabla();
                cargarIngredientes();
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
        }

        public void cargarIngredientes()
        {
            string m = "";
            try
            {
                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudAbonado(-1, -1, "", "", "", "", "", "", "l")[0];
                txtIngrediente.DataSource = result;
                txtIngrediente.DataTextField = "Nombre";
                txtIngrediente.DataValueField = "ID";
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
            try
            {
                GridViewRow row = tbl.Rows[tbl.SelectedIndex];
                Session["op"] = "u";
                TxtID.Text = row.Cells[1].Text;
                informar(row.Cells[1].Text);
                String m = sqlExecute("lb").Rows.Count.ToString();
                informar(m);
                //txtIDmatricula.Text = row.Cells[3].Text;
                tblConsumos.DataSource = sqlExecute("lb");
                tblConsumos.DataBind();

                tblAsistencias.DataSource = sqlExecute("g");
                tblAsistencias.DataBind();
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
            txtMes.Text = "";
            txtIngrediente.Enabled = true;
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

            return sqlExecute(op);
        }

        private DataTable sqlExecute(string op)
        {
            GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
            DataTable result = Ws.crudmfactura((parse(TxtID.Text)), (parse(txtIngrediente.SelectedValue)), txtMes.Text, ""
            , op)[0];
            Ws.Close();
            return result;
        }

        private byte parseByte(String num)
        {
            try
            {
                return Convert.ToByte(num);
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
        }
    }
}
