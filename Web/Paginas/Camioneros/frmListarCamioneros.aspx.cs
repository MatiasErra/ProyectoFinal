using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;

namespace Web.Paginas.Camioneros
{
    public partial class frmListarCamioneros : System.Web.UI.Page
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

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
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
                string id = partes[0];
                txtId.Text= id;
            }
        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Camionero> camioneros = Web.listCamionero();
            lstCamionero.DataSource = null;
            string value = txtBuscar.Text;
            List<Camionero> camioneroslst = new List<Camionero>();
            if (value == "")
            {
                camioneroslst = camioneros;
            }
            else
            {
                foreach (Camionero unCamionero in camioneros)
                {

                    if (unCamionero.Nombre == value || unCamionero.Apellido == value || unCamionero.Email == value || unCamionero.Telefono == value || unCamionero.Cedula == value)
                    {
                        camioneroslst.Add(unCamionero);
                    }
                }
            }
            if (camioneroslst.Count > 0)
            {
                lstCamionero.Visible = true;
                lblMensajes.Text = "";
                lstCamionero.DataSource = camioneroslst;
                lstCamionero.DataBind();
            }
            else
            {
                lstCamionero.Visible = false;
                lblMensajes.Text = "No se encontro ningun camionero";
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
                Camionero unCamionero = Web.buscarCamionero(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unCamionero != null)
                {
                    if (Web.bajaCamionero(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el Camionero.";
                        listar();
                    }
                    else
                    {
                        limpiar();
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