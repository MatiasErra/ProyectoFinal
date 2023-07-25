using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Depositos
{
    public partial class frmListarDepositos : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiar();
                listar();
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstDeposito.DataSource = null;
            lstDeposito.DataSource = Web.listDeps();

            lstDeposito.DataBind();
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
        }


        protected void lstDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDeposito.SelectedIndex != -1)
            {
                string linea = this.lstDeposito.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                string id = partes[0];
                txtId.Text = id;
            }
        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Deposito> depositos = Web.listDeps();
            lstDeposito.DataSource = null;
            string value = txtBuscar.Text;
            List<Deposito> depositolst = new List<Deposito>();
            if (value == "")
            {
                depositolst = depositos;
            }
            else
            {
                foreach (Deposito unDeposito in depositos)
                {

                    if (unDeposito.Condiciones == value || unDeposito.Capacidad == value || unDeposito.Temperatura.ToString() == value)
                    {
                        depositolst.Add(unDeposito);
                    }
                }
            }
            if (depositolst.Count > 0)
            {
                lstDeposito.Visible = true;
                lblMensajes.Text = "";
                lstDeposito.DataSource = depositolst;
                lstDeposito.DataBind();
            }
            else
            {
                lstDeposito.Visible = false;
                lblMensajes.Text = "No se encontro ningun deposito";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = Web.buscarDeps(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unDeposito != null)
                {
                    if (Web.bajaDeps(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el Deposito.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar el Deposito.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El Deposito no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un Deposito para eliminar. ";
            }
        }
    }
}