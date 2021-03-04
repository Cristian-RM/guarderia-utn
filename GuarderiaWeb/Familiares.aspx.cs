using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Familiares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IDrelacion"] = "";
                Session["op"] = "";
                validarSessino();
                cargarTabla();
            }
            else
            {
            }
            cargarTabla();
        }

        private string validarSessino()
        {
            try
            {
                int idchild = Convert.ToInt32((string)Session["IDchild"]);
                String IDchild = (String)Session["IDchild"];

                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudChild(idchild, "", "", "", "g");

                lblNino.Text = result.Rows[0].Field<string>("Nombre");

                txtMatricula.Text = IDchild;
                txtMatricula.ReadOnly = true;
                if (IDchild.Length == 0)
                {
                    Response.Redirect("Child.aspx"); return "";
                }
                return IDchild;
            }
            catch (Exception)
            {
                Response.Redirect("Child.aspx");
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
                alertModal.Visible = false;
                Session["op"] = "u";
                txtID.Text = row.Cells[1].Text;
                txtTipoRelacion.Text = row.Cells[4].Text;

                Session["IDrelacion"] = txtID.Text;
                txtTipoRelacion.ReadOnly = false;
                encerderModal(1);
            }
            catch (Exception)
            {
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Session["op"] = "i";
            txtID.Text = "";
            txtTipoRelacion.Text = "";

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
            DataTable result = Ws.crudChildRelations(parse(txtID.Text), parse(txtMatricula.Text), txtTipoRelacion.Text, op)[0];

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
            catch (Exception ex)
            {
                informar(ex.Message);
            }
        }

        protected void btnSempleadoAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Encargados.aspx");
        }

        protected void BTNABONADO_Click(object sender, EventArgs e)
        {
            Response.Redirect("Abonados.aspx");
        }
    }
}