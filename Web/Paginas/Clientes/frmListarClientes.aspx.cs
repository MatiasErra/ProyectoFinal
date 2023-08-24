using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

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
               

                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
                {
                    btnVolverFrm.Visible = true;

                    lstClienteSelect.Visible = true;
                    lstCliente.Visible = false;

                } 
                limpiar();
                listar();
            }
        }


        private void listar()
        {
            if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
            {
                lstClienteSelect.Visible = true;
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                lstClienteSelect.DataSource = null;
                lstClienteSelect.DataSource = Web.lstCli();
                lstClienteSelect.DataBind();
            }
            else
            {



                lstCliente.Visible = true;
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                lstCliente.DataSource = null;
                lstCliente.DataSource = Web.lstCli();

                lstCliente.DataBind();
            }
        }


        private void limpiar()
        {

            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
            listar();
         
        }

    
        

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }


        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
            string val = value.ToLower();
            List<Cliente> clienteslst = new List<Cliente>();
             clienteslst = Web.buscarVarCli(val);
            if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
            {
                lstClienteSelect.DataSource = null;
                if (txtBuscar.Text != "")
                {
                    if (clienteslst.Count > 0)
                    {
                        lstClienteSelect.Visible = true;
                        lblMensajes.Text = "";
                        lstClienteSelect.DataSource = clienteslst;
                        lstClienteSelect.DataBind();
                    }
                    else
                    {
                        lstClienteSelect.Visible = false;
                        lblMensajes.Text = "No se encontro ningun Cliente.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe poner algun dato en el buscador.";
                    listar();
                }

            }
            else
            {

                lstCliente.DataSource = null;
                if (txtBuscar.Text != "")
                {
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
                else
                {
                    lblMensajes.Text = "Debe poner algun dato en el buscador.";
                    listar();
                }
            }
        }


        protected void btnVolverFrm_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Granjas/frmGranjas");
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {


            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Cliente unCliente = Web.buscarCli(id);
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

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            System.Web.HttpContext.Current.Session["DuenoSelected"] = id;

            Response.Redirect("/Paginas/Granjas/frmGranjas");

        }
    }
}