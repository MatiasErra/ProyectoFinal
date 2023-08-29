using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }


                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
                {
                    btnVolverFrm.Visible = true;

                    lstClienteSelect.Visible = true;
                    lstCliente.Visible = false;

                } 
                
                CargarListOrdenarPor();
                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;

                }
                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;
                }

                listarPagina();
             
            }
        }

        public void CargarListOrdenarPor()
        {
            listOrdenarPor.DataSource = null;
            listOrdenarPor.DataSource = createDataSourceOrdenarPor();
            listOrdenarPor.DataTextField = "nombre";
            listOrdenarPor.DataValueField = "id";
            listOrdenarPor.DataBind();
        }

        ICollection createDataSourceOrdenarPor()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ordenar por", "Ordenar por", dt));
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Apellido", "Apellido", dt));
            dt.Rows.Add(createRow("E-Mail", "E-Mail", dt));
            dt.Rows.Add(createRow("Teléfono", "Teléfono", dt));
            dt.Rows.Add(createRow("Fecha de Nacimiento", "Fecha de Nacimiento", dt));
            dt.Rows.Add(createRow("Usuario", "Usuario", dt));
            dt.Rows.Add(createRow("Dirección", "Dirección", dt));

            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow createRow(String Text, String Value, DataTable dt)
        {


            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }

        private int PagMax()
        {

            return 4;
        }



        private List<Cliente> obtenerClientes()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;

            string ordenar = "";


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }



            List<Cliente> clientes = Web.buscarCliFiltro(buscar, ordenar);

            return clientes;
        }


        private void listarPagina()
        {
            List<Cliente> cliente = obtenerClientes();
            List<Cliente> ClientePagina = new List<Cliente>();
            string p = lblPaginaAct.Text.ToString();

            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Cliente unCliente in cliente)
            {
                if (ClientePagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    ClientePagina.Add(unCliente);
                }

                cont++;
            }

            if (ClientePagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún administrador.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCliente.Visible = false;
                lstClienteSelect.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
                {
                    lstClienteSelect.Visible = true;
                    modificarPagina();
                    lstClienteSelect.DataSource = null;
                    lstClienteSelect.DataSource = ClientePagina;
                    lstClienteSelect.DataBind();
                }

                else
                {
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstCliente.Visible = true;
                    lstCliente.DataSource = null;
                    lstCliente.DataSource = ClientePagina;
                    lstCliente.DataBind();
                }
            }

        }


        private void modificarPagina()
        {
            List<Cliente> clientes = obtenerClientes();
            double pxp = PagMax();
            double count = clientes.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();

            lblPaginaSig.Visible = true;
            lblPaginaAct.Visible = true;
            lblPaginaAnt.Visible = true;
            if (pagAct == cantPags.ToString())
            {
                lblPaginaSig.Visible = false;
            }
            if (pagAct == "1")
            {
                lblPaginaAnt.Visible = false;
            }
            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }


        private void limpiar()
        {

            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";
            listarPagina();


        }
        private bool GranjaCli(Cliente cli)
        {
            bool resultado;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string var1 = "";
            string var2 = "";
            List<Granja> granjas = Web.buscarGranjaFiltro(var1, var2);
         
            foreach (Granja unaGranja in granjas)
            {
               
                    if (unaGranja.IdCliente.Equals(cli.IdPersona))
                    {
                        resultado = false;
                        return resultado;

                    }
                
            }
            resultado = true;
            return resultado;

        }
        protected void listOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }







        protected void btnVolverFrm_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Granjas/frmGranjas");
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
                if (GranjaCli(unCliente))
                {
                    if (Web.bajaCli(id))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el Cliente.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listarPagina();
                    }
                    else
                    {

                        lblMensajes.Text = "No se ha podido borrar el Cliente.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Hay una granja asociada a este Cliente.";
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