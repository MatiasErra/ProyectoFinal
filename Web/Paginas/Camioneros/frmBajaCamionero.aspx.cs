using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Camioneros
{
    public partial class frmBajaCamionero : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listar();
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstCamionero.DataSource = null;
            lstCamionero.DataSource = Web.listCamionero();

            lstCamionero.DataBind();
        }

        protected void lstCamionero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCamionero.SelectedIndex != -1)
            {
                string linea = this.lstCamionero.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                txtId.Text = id.ToString();
                lstCamionero.SelectedIndex = -1;
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Camionero unCamionero = Web.buscarCamionero(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unCamionero != null)
                {
                    if (Web.bajaCamionero(int.Parse(txtId.Text)))
                    {
                        lblMensajes.Text = "Se ha borrado el Camionero.";
                        txtId.Text = "";
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido borrar el Camionero.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El Camionero no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un camionero para eliminar. ";

            }
        }
    }
}