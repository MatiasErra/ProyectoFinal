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

        #region Load

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
                CargarListBuscar();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreClienteBuscar"] = null;
                txtApellidoBuscar.Text = System.Web.HttpContext.Current.Session["apellidoClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["apellidoClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["apellidoClienteBuscar"] = null;
                txtEmailBuscar.Text = System.Web.HttpContext.Current.Session["emailClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["emailClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["emailClienteBuscar"] = null;
                txtTelBuscar.Text = System.Web.HttpContext.Current.Session["telClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["telClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["telClienteBuscar"] = null;
                txtUsuarioBuscar.Text = System.Web.HttpContext.Current.Session["usuarioClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["usuarioClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["usuarioClienteBuscar"] = null;
                txtUsuarioBuscar.Text = System.Web.HttpContext.Current.Session["direccionClienteBuscar"] != null ? System.Web.HttpContext.Current.Session["direccionClienteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["direccionClienteBuscar"] = null;
                txtFchNacBuscarPasada.Text = System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"] = null;
                txtFchNacBuscarFutura.Text = System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"] = null;

                // Listas
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                listarPagina();

            }
        }

        #endregion

        #region Utilidad

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";

            txtNombreBuscar.Text = "";
            txtApellidoBuscar.Text = "";
            txtEmailBuscar.Text = "";
            txtTelBuscar.Text = "";
            txtUsuarioBuscar.Text = "";
            txtFchNacBuscarPasada.Text = "";
            txtFchNacBuscarFutura.Text = "";
            txtDireccionBuscar.Text = "";

            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        private bool GranjaCli(Cliente cli)
        {
            bool resultado;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja gra = new Granja(0, "", "", 0);
            List<Granja> granjas = Web.buscarGranjaFiltro(gra, "");

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

        #region Paginas

        private int PagMax()
        {
            return 2;
        }

        private List<Cliente> obtenerClientes()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Cliente cliente = new Cliente();
            cliente.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            cliente.Apellido = HttpUtility.HtmlEncode(txtApellidoBuscar.Text);
            cliente.Email = HttpUtility.HtmlEncode(txtEmailBuscar.Text);
            cliente.Telefono = HttpUtility.HtmlEncode(txtTelBuscar.Text);
            cliente.User = HttpUtility.HtmlEncode(txtUsuarioBuscar.Text);
            cliente.Direccion = HttpUtility.HtmlEncode(txtDireccionBuscar.Text);
            string fchDesde = txtFchNacBuscarPasada.Text != "" ? txtFchNacBuscarPasada.Text : "";
            string fchHasta = txtFchNacBuscarFutura.Text != "" ? txtFchNacBuscarFutura.Text : "";
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Cliente> clientes = Web.buscarCliFiltro(cliente, fchDesde, fchHasta, ordenar);

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

        #endregion

        #region DropDownBoxes

        #region Ordenar

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

        #endregion

        #region Buscador

        public void CargarListBuscar()
        {
            listBuscarPor.DataSource = null;
            listBuscarPor.DataSource = createDataSourceBuscar();
            listBuscarPor.DataTextField = "nombre";
            listBuscarPor.DataValueField = "id";
            listBuscarPor.DataBind();
        }

        ICollection createDataSourceBuscar()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Buscar por", "Buscar por", dt));
            dt.Rows.Add(createRow("Nombre y Apellido", "Nombre y Apellido", dt));
            dt.Rows.Add(createRow("Email", "Email", dt));
            dt.Rows.Add(createRow("Telefono", "Telefono", dt));
            dt.Rows.Add(createRow("Fecha de nacimiento", "Fecha de nacimiento", dt));
            dt.Rows.Add(createRow("Usuario", "Usuario", dt));
            dt.Rows.Add(createRow("Direccion", "Direccion", dt));

            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        #endregion

        #endregion

        #region Botones

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (comprobarFechas() == 1)
            {
                lblPaginaAct.Text = "1";
                listarPagina();
            }
            else if (comprobarFechas() == 2)
            {
                if (Convert.ToDateTime(txtFchNacBuscarPasada.Text) <= Convert.ToDateTime(txtFchNacBuscarFutura.Text))
                {
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else lblMensajes.Text = "La fecha futura es menor que la fecha pasada.";
            }
            else lblMensajes.Text = "La fecha pasada o futura esta vacía.";
        }

        private int comprobarFechas()
        {
            if (txtFchNacBuscarFutura.Text == "" && txtFchNacBuscarPasada.Text == "") return 1;
            else if (txtFchNacBuscarFutura.Text != "" && txtFchNacBuscarPasada.Text != "") return 2;
            return 0;
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtApellidoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtEmailBuscar.Visible = listBuscarPor.SelectedValue == "Email" ? true : false;
            txtTelBuscar.Visible = listBuscarPor.SelectedValue == "Telefono" ? true : false;
            txtFchNacBuscarPasada.Visible = listBuscarPor.SelectedValue == "Fecha de nacimiento" ? true : false;
            lblFchNacBuscarPasada.Visible = listBuscarPor.SelectedValue == "Fecha de nacimiento" ? true : false;
            txtFchNacBuscarFutura.Visible = listBuscarPor.SelectedValue == "Fecha de nacimiento" ? true : false;
            lblFchNacBuscarFutura.Visible = listBuscarPor.SelectedValue == "Fecha de nacimiento" ? true : false;
            txtUsuarioBuscar.Visible = listBuscarPor.SelectedValue == "Usuario" ? true : false;
            txtDireccionBuscar.Visible = listBuscarPor.SelectedValue == "Direccion" ? true : false;
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            System.Web.HttpContext.Current.Session["nombreClienteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["apellidoClienteBuscar"] = txtApellidoBuscar.Text;
            System.Web.HttpContext.Current.Session["emailClienteBuscar"] = txtEmailBuscar.Text;
            System.Web.HttpContext.Current.Session["telClienteBuscar"] = txtTelBuscar.Text;
            System.Web.HttpContext.Current.Session["usuarioClienteBuscar"] = txtUsuarioBuscar.Text;
            System.Web.HttpContext.Current.Session["direccionClienteBuscar"] = txtDireccionBuscar.Text;
            System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"] = txtFchNacBuscarPasada.Text != "" ? txtFchNacBuscarPasada.Text : null;
            System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"] = txtFchNacBuscarFutura.Text != "" ? txtFchNacBuscarFutura.Text : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            System.Web.HttpContext.Current.Session["nombreClienteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["apellidoClienteBuscar"] = txtApellidoBuscar.Text;
            System.Web.HttpContext.Current.Session["emailClienteBuscar"] = txtEmailBuscar.Text;
            System.Web.HttpContext.Current.Session["telClienteBuscar"] = txtTelBuscar.Text;
            System.Web.HttpContext.Current.Session["usuarioClienteBuscar"] = txtUsuarioBuscar.Text;
            System.Web.HttpContext.Current.Session["direccionClienteBuscar"] = txtDireccionBuscar.Text;
            System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"] = txtFchNacBuscarPasada.Text != "" ? txtFchNacBuscarPasada.Text : null;
            System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"] = txtFchNacBuscarFutura.Text != "" ? txtFchNacBuscarFutura.Text : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

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

                        listarPagina();
                    }
                    else lblMensajes.Text = "No se ha podido borrar el Cliente.";
                }
                else lblMensajes.Text = "Hay una granja asociada a este Cliente.";
            }
            else lblMensajes.Text = "El Cliente no existe.";
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            if(System.Web.HttpContext.Current.Session["GranjaDatosFrm"].ToString() == "Abm")
            {
                System.Web.HttpContext.Current.Session["DuenoSelected"] = id;
            }
            else
            {
                System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] = id;
            }

            Response.Redirect("/Paginas/Granjas/frmGranjas");

        }

        #endregion

    }
}