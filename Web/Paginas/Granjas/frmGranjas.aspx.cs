using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Granjass
{
    public partial class frmGranjas : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = null;
            if (!IsPostBack)
            {

                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    btnVolver.Visible = true;
                    lstGranja.Visible = false;
                    lstGranjaSelect.Visible = true;
                }
                if (System.Web.HttpContext.Current.Session["GranjaMod"] != null)
                {
                    lblMensajes.Text = "Granja Modificada";
                    System.Web.HttpContext.Current.Session["GranjaMod"] = null;
                }
                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
                {
                    cargarDatos();
                }
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }
                CargarListBuscar();
                CargarListDueño();
                CargarListOrdenarPor();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreGranjaBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreGranjaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreGranjaBuscar"] = null;
                txtUbicacionBuscar.Text = System.Web.HttpContext.Current.Session["ubicacionGranjaBuscar"] != null ? System.Web.HttpContext.Current.Session["ubicacionGranjaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["ubicacionGranjaBuscar"] = null;

                // Listas
                lstDueñoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] != null ? System.Web.HttpContext.Current.Session["duenoGranjaBuscar"].ToString() : "Seleccionar tipo de impacto";
                System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                listarPagina();

            }
        }

        #endregion

        #region Utilidad

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtUbicacion.Text == "" || listDueño.SelectedValue == "Seleccione un Dueño")
            {
                return true;
            }
            return false;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";

            txtNombreBuscar.Text = "";
            txtUbicacionBuscar.Text = "";
            lstDueñoBuscar.SelectedValue = "Seleccione un Dueño";

            txtNombre.Text = "";
            txtUbicacion.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lstGranja.SelectedIndex = -1;
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        public DataTable ObtenerGranjasDataTable(List<Granja> granjas)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {

                new DataColumn("IdGranja", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Ubicacion", typeof(string)),

                new DataColumn("NomDue", typeof(string))});

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            foreach (Granja gran in granjas)
            {
                Cliente cli = Web.buscarCli(gran.IdCliente);

                DataRow dr = dt.NewRow();
                dr["IdGranja"] = gran.IdGranja.ToString();
                dr["Nombre"] = gran.Nombre.ToString();
                dr["Ubicacion"] = gran.Ubicacion.ToString();
                dr["NomDue"] = cli.Nombre.ToString();
                dt.Rows.Add(dr);
            }

            return dt;
        }

        static int GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            int intGuid = guid.GetHashCode();
            int i = 0;

            while (intGuid < 0)
            {
                return GenerateUniqueId();
            }

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja gra = new Granja(0, "", "", 0);
            List<Granja> lstGranjas = Web.buscarGranjaFiltro(gra, "");
            foreach (Granja granja in lstGranjas)
            {
                if (granja.IdGranja.Equals(intGuid))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                return intGuid;
            }
            else return GenerateUniqueId();
        }

        private bool comprobarProducen(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = "";
            string ord = "";
            List<Lote> lotes = Web.buscarFiltrarLotes(buscar, ord);
            foreach (Lote unLote in lotes)
            {
                if (unLote.IdGranja.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        #region Paginas

        private int PagMax()
        {
            return 2;
        }



        private List<Granja> obtenerGranjas()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja granja = new Granja();
            granja.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            granja.Ubicacion = HttpUtility.HtmlEncode(txtUbicacionBuscar.Text);
            granja.IdCliente = lstDueñoBuscar.SelectedValue != "Seleccione un Dueño" ? int.Parse(lstDueñoBuscar.SelectedValue) : 0;
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Granja> granjas = Web.buscarGranjaFiltro(granja, ordenar);

            return granjas;
        }


        private void listarPagina()
        {
            List<Granja> granjas = obtenerGranjas();
            List<Granja> granjasPagina = new List<Granja>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Granja unaGranja in granjas)
            {
                if (granjasPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    granjasPagina.Add(unaGranja);
                }

                cont++;
            }

            if (granjasPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningúna granja.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstGranja.Visible = false;
                lstGranjaSelect.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    lstGranjaSelect.Visible = true;
                    modificarPagina();
                    lstGranjaSelect.DataSource = null;
                    lstGranjaSelect.DataSource = ObtenerDatos(granjasPagina);
                    lstGranjaSelect.DataBind();
                }

                else
                {
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstGranja.Visible = true;
                    lstGranja.DataSource = null;
                    lstGranja.DataSource = ObtenerDatos(granjasPagina);
                    lstGranja.DataBind();
                }
            }
        }

        public DataTable ObtenerDatos(List<Granja> granjas)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("IdGranja", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Ubicacion", typeof(string)),
                new DataColumn("NomDue", typeof(string))});

            foreach (Granja unaGranja in granjas)
            {
                DataRow dr = dt.NewRow();
                dr["IdGranja"] = unaGranja.IdGranja;
                dr["Nombre"] = unaGranja.Nombre;
                dr["Ubicacion"] = unaGranja.Ubicacion;
                dr["NomDue"] = unaGranja.NombreCliente;

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void modificarPagina()
        {
            List<Granja> granjas = obtenerGranjas();
            double pxp = PagMax();
            double count = granjas.Count;
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

        #region Guardar y cargar datos

        private void guardarDatos()
        {


            System.Web.HttpContext.Current.Session["Ubicacion"] = txtUbicacion.Text;
            System.Web.HttpContext.Current.Session["Nombre"] = txtNombre.Text;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["GranjaDatosFrm"] = null;



            txtUbicacion.Text = System.Web.HttpContext.Current.Session["Ubicacion"].ToString();
            System.Web.HttpContext.Current.Session["Ubicacion"] = null;

            txtNombre.Text = System.Web.HttpContext.Current.Session["Nombre"].ToString();
            System.Web.HttpContext.Current.Session["Nombre"] = null;

            if (System.Web.HttpContext.Current.Session["DuenoSelected"] != null)
            {
                listDueño.SelectedValue = System.Web.HttpContext.Current.Session["DuenoSelected"].ToString();
                System.Web.HttpContext.Current.Session["DuenoSelected"] = null;
            }





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
            dt.Rows.Add(createRow("Ubicación", "Ubicación", dt));
            dt.Rows.Add(createRow("Nombre del dueño", "Nombre del dueño", dt));

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
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Ubicación", "Ubicación", dt));
            dt.Rows.Add(createRow("Nombre del dueño", "Nombre del dueño", dt));
            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Dueño

        public void CargarListDueño()
        {
            listDueño.DataSource = null;
            listDueño.DataSource = createDataSource();
            listDueño.DataTextField = "nombre";
            listDueño.DataValueField = "id";
            listDueño.DataBind();

            lstDueñoBuscar.DataSource = null;
            lstDueñoBuscar.DataSource = createDataSource();
            lstDueñoBuscar.DataTextField = "nombre";
            lstDueñoBuscar.DataValueField = "id";
            lstDueñoBuscar.DataBind();

        }

        ICollection createDataSource()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Cliente> clientes = new List<Cliente>();
            Cliente cli = new Cliente(0, "", "", "", "", "", "", "", "");
            clientes = Web.buscarCliFiltro(cli, "", "", "");


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Dueño", "Seleccione un Dueño", dt));

            cargarDueños(clientes, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarDueños(List<Cliente> clientes, DataTable dt)
        {
            foreach (Cliente unCliente in clientes)
            {
                dt.Rows.Add(createRow(unCliente.IdPersona + " " + unCliente.Nombre + " " + unCliente.Apellido, unCliente.IdPersona.ToString(), dt));
            }
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
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre" ? true : false;
            txtUbicacionBuscar.Visible = listBuscarPor.SelectedValue == "Ubicación" ? true : false;
            lstDueñoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre del dueño" ? true : false;
            btnBuscarDueñoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre del dueño" ? true : false;
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            System.Web.HttpContext.Current.Session["nombreGranjaBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["ubicacionGranjaBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] = lstDueñoBuscar.SelectedValue != "Seleccione un Dueño" ? lstDueñoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            System.Web.HttpContext.Current.Session["nombreGranjaBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["ubicacionGranjaBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["duenoGranjaBuscar"] = lstDueñoBuscar.SelectedValue != "Seleccione un Dueño" ? lstDueñoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["GranjaDatosFrm"] = "Abm";
            guardarDatos();
            Response.Redirect("/Paginas/Clientes/frmListarClientes");
        }

        protected void btnBuscarDueñoBuscar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["GranjaDatosFrm"] = "Buscar";
            guardarDatos();
            Response.Redirect("/Paginas/Clientes/frmListarClientes");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = GenerateUniqueId();
                string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                int idCliente = int.Parse(HttpUtility.HtmlEncode(listDueño.SelectedValue));

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Granja unaGranja = new Granja(id, nombre, ubicacion, idCliente);
                if (Web.altaGranja(unaGranja))
                {
                    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                    {
                        System.Web.HttpContext.Current.Session["idGranjaSel"] = unaGranja.IdGranja.ToString();
                        Response.Redirect("/Paginas/Lotes/frmAltaLotes");
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "Granja dada de alta con éxito.";
                    }
                }
                else lblMensajes.Text = "Ya existe una Granja con estos datos. Estos son los posibles datos repetidos (Ubicación).";
            }
            else lblMensajes.Text = "Faltan datos.";
        }


        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            System.Web.HttpContext.Current.Session["idGranjaSel"] = id;

            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja unaGranja = Web.buscarGranja(id);
            if (unaGranja != null)
            {
                if (!comprobarProducen(id))
                {
                    if (Web.bajaGranja(id))
                    {
                        limpiar();
                        listarPagina();
                        lblMensajes.Text = "Se ha borrado la granja.";
                    }
                    else lblMensajes.Text = "No se ha podido borrar la granja.";
                }
                else lblMensajes.Text = "Esta granja esta asociada a un lote.";
            }
            else lblMensajes.Text = "La granja no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = id;
            Response.Redirect("/Paginas/Granjas/modGranja");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion

    }
}