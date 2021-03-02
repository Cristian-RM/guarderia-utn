using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuarderiaWeb
{
    public partial class Abonados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["op"] = "";
                validarSessino();
                cargarTabla();
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
                int IDrelacion = Convert.ToInt32((string)Session["IDrelacion"]);

                String IDrelacionS = Convert.ToString(IDrelacion);
                TxtIDchildRelation.Text = IDrelacionS;
                TxtIDchildRelation.ReadOnly = true;

                TxtIDchildRelation.Text = IDrelacionS;
                TxtIDchildRelation.ReadOnly = true;

                GuarderiaWeb.ServiceReference1.ServicioGuarderiaSoapClient Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                DataTable result = Ws.crudChildRelations(IDrelacion, IDrelacion, "", "g")[0];

                lblRelacion.Text = "(" + result.Rows[0].Field<string>("TipoRelacion") + ")";

                int child = result.Rows[0].Field<int>("IDchild");

                Ws = new ServiceReference1.ServicioGuarderiaSoapClient();
                result = Ws.crudChild(child, "", "", "", "g");

                lblbebe.Text = result.Rows[0].Field<string>("Nombre");

                if (IDrelacion == 0)

                {
                    Response.Redirect("Familiares.aspx"); return "";
                }
                return IDrelacionS;
            }
            catch (Exception)
            {
                Response.Redirect("Familiares.aspx");
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
            GridViewRow row = tbl.Rows[tbl.SelectedIndex];
            try
            {
                Session["op"] = "u";
                TxtID.Text = row.Cells[1].Text;
                txtDNI.Text = row.Cells[3].Text;
                txtNombre.Text = row.Cells[4].Text;
                txtDireccion.Text = row.Cells[5].Text;
                txtTelefono.Text = row.Cells[6].Text;
                txtBanco.Text = row.Cells[7].Text;
                txtCuentaIbam.Text = row.Cells[8].Text;

                TxtID.ReadOnly = true;
                TxtIDchildRelation.ReadOnly = true;
                txtDNI.ReadOnly = true;
                btnEliminar.Visible = true;
                tbl.SelectedIndex = -1;
                alertModal.Visible = false;

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
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtBanco.Text = "";
            txtCuentaIbam.Text = "";

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
            DataTable result = Ws.crudAbonado((parse(TxtID.Text)), (parse(TxtIDchildRelation.Text)), txtDNI.Text,
                txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtBanco.Text, txtCuentaIbam.Text, op)[0];

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
        }
    }
}