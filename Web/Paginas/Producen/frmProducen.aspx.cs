using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Producen
{
    public partial class frmProducen : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
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
            lstProduce.DataSource = null;
            lstProduce.DataSource = Web.listProducen();
            lstProduce.DataBind();
            limpiar();
        }

        private bool faltanDatos()
        {
            if (listGranja.SelectedValue == "Seleccione una Granja" || listProducto.SelectedValue == "Seleccione un Producto" || txtFchProduccion.Text == "" || txtStock.Text == "" || txtPrecio.Text == "" || listDeposito.SelectedValue == "Seleccione un Deposito")
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

            txtStock.Text = "";
            txtPrecio.Text = "";
            lstProduce.SelectedIndex = -1;
            CargarListGranja();
            CargarListProducto();
            CargarListDeposito();
        }

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
                dt.Rows.Add(createRow(unaGranja.IdGranja + " " + unaGranja.Nombre + " " + unaGranja.Ubicacion, unaGranja.IdGranja.ToString(), dt));
            }
        }

        #endregion

        #region Producto

        public void CargarListProducto(){ 
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
                dt.Rows.Add(createRow(unProducto.IdProducto + " " + unProducto.Nombre + " " + unProducto.Tipo, unProducto.IdProducto.ToString(), dt));
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
                dt.Rows.Add(createRow(unDeposito.IdDeposito + " " + unDeposito.Capacidad + " " + unDeposito.Temperatura + "C°", unDeposito.IdDeposito.ToString(), dt));
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
            List<Produce> lstProducen = Web.listIdProducen();
            foreach (Produce produce in lstProducen)
            {
                if (produce.IdGranja.Equals(intGuid))
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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text.ToLower();
            List<Produce> producen = Web.buscarVarProducen(value);
            lstProduce.DataSource = null;


            if (producen.Count > 0)
            {
                lstProduce.Visible = true;
                lblMensajes.Text = "";
                lstProduce.DataSource = producen;
                lstProduce.DataBind();
            }
            else
            {
                lstProduce.Visible = false;
                lblMensajes.Text = "No se encontro ningun produce.";
            }
        }

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

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (fchNotToday())
                {
                    int idGranja = int.Parse(HttpUtility.HtmlEncode(listGranja.SelectedValue));
                    int idProducto = int.Parse(HttpUtility.HtmlEncode(listProducto.SelectedValue));
                    string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                    int stock = int.Parse(HttpUtility.HtmlEncode(txtStock.Text));
                    double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                    int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Produce unProduce = new Produce(idGranja, idProducto, fchProduccion, stock, precio, idDeposito);
                    if (Web.altaProduce(unProduce))
                    {
                        listar();
                        lblMensajes.Text = "Produce dado de alta con exito.";
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta produce.";

                    }
                }
                else
                {
                    lblMensajes.Text = "La fecha debe ser igual o menor a la de hoy.";
                }    
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            //if existe pedidos_producen
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[1].Text));
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Produce unProduce = Web.buscarProduce(idGranja, idProducto, fchProduccion);
            if (unProduce != null)
            {
                if (Web.bajaProduce(idGranja, idProducto, fchProduccion))
                {
                    listar();
                    lblMensajes.Text = "Se ha borrado el produce.";
                }
                else
                {
                    lblMensajes.Text = "No se ha podido borrar produce.";
                }
            }
            else
            {
                lblMensajes.Text = "El produce no existe.";
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
            Response.Redirect("/Paginas/Producen/modProduce");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

    }
}