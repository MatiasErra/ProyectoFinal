using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Admins
{
    public partial class frmListarAdmins : System.Web.UI.Page
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
            lstAdmin.DataSource = null;
            lstAdmin.DataSource = Web.lstAdmin();

            lstAdmin.DataBind();
        }

        protected void lstAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAdmin.SelectedIndex != -1)
            {
                string linea = this.lstAdmin.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                string id = partes[0];
                txtId.Text = id;
            }
        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Admin> admins = Web.lstAdmin();
            lstAdmin.DataSource = null;
            string value = txtBuscar.Text;
            List<Admin> adminslst = new List<Admin>();
            if (value == "")
            {
                adminslst = admins;
            }
            else
            {
                foreach (Admin unAdmin in admins)
                {
                    if (unAdmin.Nombre == value || unAdmin.Apellido == value || unAdmin.Email == value || unAdmin.Telefono == value || unAdmin.User == value)
                    {
                        adminslst.Add(unAdmin);
                    }
                }
            }
            if (adminslst.Count > 0)
            {
                lstAdmin.Visible = true;
                lblMensajes.Text = "";
                lstAdmin.DataSource = adminslst;
                lstAdmin.DataBind();
            }
            else
            {
                lstAdmin.Visible = false;
                lblMensajes.Text = "No se encontro ningun admin.";
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
                Admin unAdmin = Web.buscarAdm(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unAdmin != null)
                {
                    if (Web.bajaAdmin(int.Parse(txtId.Text)))
                    {
                        lblMensajes.Text = "Se ha borrado el Admin.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido borrar el Admin.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El Admin no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un Admin para eliminar. ";
            }
        }
    }
}