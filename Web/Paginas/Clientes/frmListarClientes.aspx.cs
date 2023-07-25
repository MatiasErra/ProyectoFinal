using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Clientes
{
    public partial class frmListarClientes : System.Web.UI.Page
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
            lstCliente.DataSource = null;
            lstCliente.DataSource = Web.lstCli();

            lstCliente.DataBind();
        }

        protected void lstCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCliente.SelectedIndex != -1)
            {
                string linea = this.lstCliente.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                string id = partes[0];
                txtId.Text = id;
            }
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Cliente> clientes = Web.lstCli();
            lstCliente.DataSource = null;
            string value = txtBuscar.Text;
            List<Cliente> clienteslst = new List<Cliente>();
            if (value == "")
            {
                clienteslst = clientes;
            }
            else
            {
                foreach (Cliente unCliente in clientes)
                {

                    if (unCliente.Nombre == value || unCliente.Apellido == value || unCliente.Email == value || unCliente.Telefono == value || unCliente.User == value)
                    {
                        clienteslst.Add(unCliente);
                    }
                }
            }
            if (clienteslst.Count > 0)
            {
                lstCliente.Visible = true;
                lblMensajes.Text = "";
                lstCliente.DataSource = clienteslst;
                lstCliente.DataBind();
            }
            else
            {
                lstCliente.Visible = false;
                lblMensajes.Text = "No se encontro ningun Cliente.";
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
                Cliente unCliente = Web.buscarCli(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unCliente != null)
                {
                    if (Web.bajaCli(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el Cliente.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar el Cliente.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El Cliente no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un Cliente para eliminar. ";
            }
        }
    }
}