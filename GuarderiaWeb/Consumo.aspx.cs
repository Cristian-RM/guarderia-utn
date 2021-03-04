using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Consumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = "";

                cargarTabla();
                cargarIngredientes();
                cargarMenu();
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
                DataTable result = Ws.crudChild(-1, "", "", "", "l");
                txtIngrediente.DataSource = result;
                txtIngrediente.DataTextField = "Nombre";
                txtIngrediente.DataValueField = "IDmatricula";
                txtIngrediente.DataBind();
            }
            catch (Exception es)
            {
                m = es.Message;
                informar(m);
            }
        }
        public void cargarMenu()
        {
            string m = "";
            try
            {
                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudMenus(-1, "", 1, "l")[0];
                txtIDmenu.DataSource = result;
                txtIDmenu.DataTextField = "Nombre";
                txtIDmenu.DataValueField = "ID";
                txtIDmenu.DataBind();
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
                //txtIDmatricula.Text = row.Cells[3].Text;

                ListItem item = txtIngrediente.Items.FindByValue(row.Cells[2].Text);
                txtIngrediente.SelectedIndex = txtIngrediente.Items.IndexOf(item);
                txtIngrediente.Enabled = false;


                item = txtIDmenu.Items.FindByValue(row.Cells[2].Text);
                txtIDmenu.SelectedIndex = txtIDmenu.Items.IndexOf(item);
                txtIDmenu.Enabled = false;

                txtFechaConsumo.Enabled = false;
                txtActivo.Enabled = false;

                txtIDmenu.Text = row.Cells[3].Text;

                txtFechaConsumo.Text = row.Cells[4].Text;

                txtActivo = null;


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

            txtFechaConsumo.Enabled = false;
            txtActivo.Enabled = false;
            TxtID.Text = "";
            txtIDmenu.Enabled = true;
            txtFechaConsumo.Text = "";
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
            DataTable result = Ws.crudConsumos((parse(TxtID.Text)), (parse(txtIngrediente.SelectedValue)), (parse(txtIDmenu.Text)), txtFechaConsumo.Text,
            parseByte(txtActivo.Text), op)[0];
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
