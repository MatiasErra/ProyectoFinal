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
            if (System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
            {
                int id = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin admin = Web.buscarAdm(id);

                if (admin.TipoDeAdmin == "Administrador global")
                {
                    this.MasterPageFile = "~/Master/AGlobal.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de productos")
                {
                    this.MasterPageFile = "~/Master/AProductos.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }
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


                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null || System.Web.HttpContext.Current.Session["GranjaDatosMod"] != null)
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
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstCliente"] != null ? System.Web.HttpContext.Current.Session["BuscarLstCliente"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstCliente"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorCliente"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorCliente"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorCliente"] = null;
                comprobarBuscar();
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
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
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

        private void comprobarBuscar()
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtApellidoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtEmailBuscar.Visible = listBuscarPor.SelectedValue == "Email" ? true : false;
            txtTelBuscar.Visible = listBuscarPor.SelectedValue == "Telefono" ? true : false;
            lblFchNac.Visible = listBuscarPor.SelectedValue == "Fecha de nacimiento" ? true : false;
            txtUsuarioBuscar.Visible = listBuscarPor.SelectedValue == "Usuario" ? true : false;
            txtDireccionBuscar.Visible = listBuscarPor.SelectedValue == "Direccion" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["nombreClienteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["apellidoClienteBuscar"] = txtApellidoBuscar.Text;
            System.Web.HttpContext.Current.Session["emailClienteBuscar"] = txtEmailBuscar.Text;
            System.Web.HttpContext.Current.Session["telClienteBuscar"] = txtTelBuscar.Text;
            System.Web.HttpContext.Current.Session["usuarioClienteBuscar"] = txtUsuarioBuscar.Text;
            System.Web.HttpContext.Current.Session["direccionClienteBuscar"] = txtDireccionBuscar.Text;
            System.Web.HttpContext.Current.Session["fchPasadaClienteBuscar"] = txtFchNacBuscarPasada.Text != "" ? txtFchNacBuscarPasada.Text : null;
            System.Web.HttpContext.Current.Session["fchFuturaClienteBuscar"] = txtFchNacBuscarFutura.Text != "" ? txtFchNacBuscarFutura.Text : null;
            System.Web.HttpContext.Current.Session["BuscarLstCliente"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorCliente"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
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
            string fchDesde = txtFchNacBuscarPasada.Text != "" ? txtFchNacBuscarPasada.Text : "1000-01-01";
            string fchHasta = txtFchNacBuscarFutura.Text != "" ? txtFchNacBuscarFutura.Text : "3000-12-30";
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
                lblPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún cliente.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCliente.Visible = false;
                lstClienteSelect.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null || System.Web.HttpContext.Current.Session["GranjaDatosMod"] != null)
                {
                    lblPaginas.Visible = true;
                    lstClienteSelect.Visible = true;
                    modificarPagina();
                    lstClienteSelect.DataSource = null;
                    lstClienteSelect.DataSource = ClientePagina;
                    lstClienteSelect.DataBind();
                }
                else
                {
                    lblPaginas.Visible = true;
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
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

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
            dt.Rows.Add(createRow("Fecha de Nacimiento", "Fecha de Nacimiento", dt));

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
            int num = 0;
            try
            {
                if (DateTime.Parse(txtFchNacBuscarPasada.Text) <= DateTime.Parse(txtFchNacBuscarFutura.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {

                lblPaginaAct.Text = "1";
                listarPagina();
            }
            else
            {
                lblMensajes.Text = "La fecha de nacimiento menor es mayor.";
                listBuscarPor.SelectedValue = "Fecha de nacimiento";
                comprobarBuscar();
            }
        }

        


        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comprobarBuscar();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void btnVolverFrm_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
            {
                Response.Redirect("/Paginas/Granjas/frmGranjas");
            }
            else if (System.Web.HttpContext.Current.Session["GranjaDatosMod"] != null)
            {
                Response.Redirect("/Paginas/Granjas/modGranja");
            }
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
                        lblPaginaAct.Text = "1";
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


            if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
            {
                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"].ToString() == "Abm")
                {
                    System.Web.HttpContext.Current.Session["DuenoSelected"] = id;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] = id;
                }

                Response.Redirect("/Paginas/Granjas/frmGranjas");
            }
            else if (System.Web.HttpContext.Current.Session["GranjaDatosMod"] != null)
            {
                System.Web.HttpContext.Current.Session["DuenoSelected"] = id;
                Response.Redirect("/Paginas/Granjas/modGranja");
            }
        }

        #endregion

    }
}