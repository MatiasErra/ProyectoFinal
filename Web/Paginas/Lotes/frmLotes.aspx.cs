using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;
using Web.Paginas.Productos;

namespace Web.Paginas.Lotes
{
    public partial class frmLotes : System.Web.UI.Page
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
                listar();
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    cargarDatos();


                }
                System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
                System.Web.HttpContext.Current.Session["idProductoSel"] = null;
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
                listProductoUpdate();
            }
        }
        #endregion

        #region Utilidad

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstLote.DataSource = null;
            lstLote.DataSource = Web.listLotes();
            lstLote.DataBind();

            limpiar();
        }

        private bool faltanDatos()
        {
            if (listGranja.SelectedValue == "Seleccione una Granja" || listProducto.SelectedValue == "Seleccione un Producto"
                || txtFchProduccion.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == ""
                || listDeposito.SelectedValue == "Seleccione un Deposito"
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtBuscar.Text = "";
            txtBuscarGranja.Text = "";
            txtBuscarProducto.Text = "";
            txtBuscarDeposito.Text = "";


            txtFchProduccion.Text = "";
            txtCantidad.Text = "";
            txtPrecio.Text = "";

            lstLote.SelectedIndex = -1;
            CargarListGranja();
            CargarListProducto();
            CargarListDeposito();

        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text.ToLower();
            List<Lote> lotes = Web.buscarVarLotes(value);
            lstLote.DataSource = null;

            if (txtBuscar.Text != "")
            {
                if (lotes.Count > 0)
                {
                    lstLote.Visible = true;
                    lblMensajes.Text = "";
                    lstLote.DataSource = lotes;
                    lstLote.DataBind();
                }
                else
                {
                    lstLote.Visible = false;
                    lblMensajes.Text = "No se encontró ningún lote.";
                }
            }
            else
            {
                lblMensajes.Text = "Debe ingresar algún dato en el buscador para buscar.";
            }

        }

        private bool fchNotToday()
        {
            DateTime fecha = Convert.ToDateTime(txtFchProduccion.Text);
            if (fecha <= DateTime.Today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void listProductoUpdate()
        {
            if (listProducto.SelectedValue != "Seleccione un Producto")
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                int idProducto = int.Parse(HttpUtility.HtmlEncode(listProducto.SelectedValue));
                Producto unProducto = Web.buscarProducto(idProducto);
                lblCantidad.Text = "Cantidad (" + unProducto.TipoVenta + ")";
            }
            else
            {
                lblCantidad.Text = "Cantidad";
            }
        }

        #endregion

        #region DropDownBoxes

        #region Granja

        public void CargarListGranja()
        {
            listGranja.DataSource = null;
            listGranja.DataSource = createDataSourceGranja();
            listGranja.DataTextField = "nombre";
            listGranja.DataValueField = "id";
            listGranja.DataBind();
        }

        ICollection createDataSourceGranja()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Granja> granjas = new List<Granja>();
            if (txtBuscarGranja.Text == "")
            {
                granjas = Web.listGranjas();
            }
            else
            {
                string value = txtBuscarGranja.Text.ToLower();
                granjas = Web.buscarVarGranjas(value);
                if (granjas.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ninguna Granja.";
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione una Granja", "Seleccione una Granja", dt));

            cargarGranjas(granjas, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarGranjas(List<Granja> granjas, DataTable dt)
        {
            foreach (Granja unaGranja in granjas)
            {
                dt.Rows.Add(createRow(unaGranja.Nombre + " " + unaGranja.Ubicacion, unaGranja.IdGranja.ToString(), dt));
            }
        }

        #endregion

        #region Producto

        public void CargarListProducto()
        {
            listProducto.DataSource = null;
            listProducto.DataSource = createDataSourceProducto();
            listProducto.DataTextField = "nombre";
            listProducto.DataValueField = "id";
            listProducto.DataBind();
        }

        ICollection createDataSourceProducto()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Producto> productos = new List<Producto>();
            if (txtBuscarProducto.Text == "")
            {
                productos = Web.listProductos();
            }
            else
            {
                string value = txtBuscarProducto.Text.ToLower();
                productos = Web.buscarVarProductos(value);
                if (productos.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningun Producto.";
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Producto", "Seleccione un Producto", dt));

            cargarProductos(productos, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarProductos(List<Producto> productos, DataTable dt)
        {
            foreach (Producto unProducto in productos)
            {
                dt.Rows.Add(createRow(unProducto.Nombre + " " + unProducto.Tipo, unProducto.IdProducto.ToString(), dt));
            }
        }

        #endregion

        #region Deposito

        public void CargarListDeposito()
        {
            listDeposito.DataSource = null;
            listDeposito.DataSource = createDataSourceDeposito();
            listDeposito.DataTextField = "nombre";
            listDeposito.DataValueField = "id";
            listDeposito.DataBind();
        }

        ICollection createDataSourceDeposito()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Deposito> depositos = new List<Deposito>();
            if (txtBuscarDeposito.Text == "")
            {
                depositos = Web.listDeps();
            }
            else
            {
                string value = txtBuscarDeposito.Text.ToLower();
                depositos = Web.buscarVarDeps(value);
                if (depositos.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningun Deposito.";
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Deposito", "Seleccione un Deposito", dt));

            cargarDepositos(depositos, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarDepositos(List<Deposito> depositos, DataTable dt)
        {
            foreach (Deposito unDeposito in depositos)
            {
                dt.Rows.Add(createRow(unDeposito.Ubicacion + " " + unDeposito.Capacidad + " " + unDeposito.Temperatura + " C°", unDeposito.IdDeposito.ToString(), dt));
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

        #region Guardar y cargar datos

        private void guardarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatos"] = "Si";

            System.Web.HttpContext.Current.Session["idGranjaSel"] = listGranja.SelectedValue;


            System.Web.HttpContext.Current.Session["idProductoSel"] = listProducto.SelectedValue;

            if (txtFchProduccion.Text != "")
            {
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = txtFchProduccion.Text;
            }
            System.Web.HttpContext.Current.Session["cantidadSel"] = txtCantidad.Text;
            System.Web.HttpContext.Current.Session["precioSel"] = txtPrecio.Text;
            System.Web.HttpContext.Current.Session["idDepositoSel"] = listDeposito.SelectedValue;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatos"] = null;

            if (System.Web.HttpContext.Current.Session["idGranjaSel"] == null)
            {
                listGranja.SelectedValue = "Seleccione una Granja";

            }
            else
            {
                listGranja.SelectedValue = System.Web.HttpContext.Current.Session["idGranjaSel"].ToString();
            }

            if (System.Web.HttpContext.Current.Session["idProductoSel"] == null)
            {
                listProducto.SelectedValue = "Seleccione un Producto";

            }
            else
            {
                listProducto.SelectedValue = System.Web.HttpContext.Current.Session["idProductoSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["fchProduccionSel"] != null)
            {
                txtFchProduccion.Text = DateTime.Parse(System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString()).ToString("yyyy-MM-dd");
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
            }

            txtCantidad.Text = System.Web.HttpContext.Current.Session["cantidadSel"].ToString();
            System.Web.HttpContext.Current.Session["cantidadSel"] = null;
            txtPrecio.Text = System.Web.HttpContext.Current.Session["precioSel"].ToString();
            System.Web.HttpContext.Current.Session["precioSel"] = null;




            if (System.Web.HttpContext.Current.Session["idDepositoSel"] == null)
            {
                listDeposito.SelectedValue = "Seleccione un Deposito";
                System.Web.HttpContext.Current.Session["idDepositoSel"] = null;
            }
            else
            {
                listDeposito.SelectedValue = System.Web.HttpContext.Current.Session["idDepositoSel"].ToString();
                System.Web.HttpContext.Current.Session["idDepositoSel"] = null;
            }


        }

        #endregion

        #region Botones

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBuscarGranja_Click(object sender, EventArgs e)
        {
            CargarListGranja();
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            CargarListProducto();
        }

        protected void btnBuscarDeposito_Click(object sender, EventArgs e)
        {
            CargarListDeposito();
        }

        protected void btnAltaGranja_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Granjas/frmGranjas");
        }

        protected void btnAltaProducto_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Productos/frmProductos");
        }

        protected void btnAltaDeposito_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Depositos/frmDepositos");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (fchNotToday())
                {
                    int idGranja = int.Parse(HttpUtility.HtmlEncode(listGranja.SelectedValue));
                    int idProducto = int.Parse(HttpUtility.HtmlEncode(listProducto.SelectedValue));
                    string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                    int cantidad = int.Parse(HttpUtility.HtmlEncode(txtCantidad.Text));
                    double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                    int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));


                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Lote unLote = new Lote(idGranja, idProducto, fchProduccion, cantidad, precio, idDeposito);

                    if (Web.altaLote(unLote))
                    {
                        listar();
                        listProductoUpdate();
                        lblMensajes.Text = "Lote dado de alta con éxito.";
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta el lote.";

                    }
                }
                else
                {
                    lblMensajes.Text = "La fecha de producción debe ser igual o menor a la de hoy.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[1].Text));
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote unLote = Web.buscarLote(idGranja, idProducto, fchProduccion);
            if (unLote != null)
            {
                //if existe pedidos_producen

                if (Web.bajaLote(idGranja, idProducto, fchProduccion))
                {
                    listar();
                    lblMensajes.Text = "Se ha borrado el lote.";
                }
                else
                {
                    lblMensajes.Text = "No se ha podido borrar el lote.";
                }
            }
            else
            {
                lblMensajes.Text = "El lote no existe.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[1].Text));
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);


            System.Web.HttpContext.Current.Session["idGranjaSel"] = idGranja;
            System.Web.HttpContext.Current.Session["idProductoSel"] = idProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/modLote");
        }

        protected void btnVerPestis_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[1].Text));
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["idGranjaSel"] = idGranja;
            System.Web.HttpContext.Current.Session["idProductoSel"] = idProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesPestis");
        }

        protected void listProductoUpdate(object sender, EventArgs e)
        {
            listProductoUpdate();
        }

        protected void btnVerFertis_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[1].Text));
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["idGranjaSel"] = idGranja;
            System.Web.HttpContext.Current.Session["idProductoSel"] = idProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesFertis");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion

    }
}