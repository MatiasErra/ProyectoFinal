using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Lotes
{
    public partial class frmAltaLotes : System.Web.UI.Page
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

                listProductoUpdate();
            }
        }
        #endregion


        #region Utilidad


        private void listar()
        {
            CargarListGranja();
            CargarListProducto();
            CargarListDeposito();
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

            



            txtFchProduccion.Text = "";
            txtCantidad.Text = "";
            txtPrecio.Text = "";


            CargarListGranja();
            CargarListProducto();
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
            List<Granja> granjas = Web.listGranjas();
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
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

       
                productos = Web.listProductos();
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
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

         
                depositos = Web.listDeps();
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


        protected void listProductoUpdate(object sender, EventArgs e)
        {
            listProductoUpdate();
        }


        protected void btnBuscarGranja_Click(object sender, EventArgs e)
        {
            CargarListGranja();
        }

        //protected void btnBuscarProducto_Click(object sender, EventArgs e)
        //{
        //    CargarListProducto();
        //}

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
                    if (int.Parse(txtCantidad.Text) > 0)
                    {

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    
                        int idGranja = int.Parse(HttpUtility.HtmlEncode(listGranja.SelectedValue));
                        int idProducto = int.Parse(HttpUtility.HtmlEncode(listProducto.SelectedValue));
                        Producto producto = Web.buscarProducto(idProducto);
                        string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                        string cantidad = HttpUtility.HtmlEncode(txtCantidad.Text) + " " + producto.TipoVenta.ToString(); ;
                        double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                        int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));


                        
                        Lote unLote = new Lote(idGranja, idProducto, fchProduccion, cantidad, precio, idDeposito);

                        if (Web.altaLote(unLote))
                        {
                            limpiar();
                            listProductoUpdate();
                            lblMensajes.Text = "Lote dado de alta con éxito.";
                            System.Web.HttpContext.Current.Session["LoteAlta"] = "si";
                            Response.Redirect("/Paginas/Lotes/frmLotes");
                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo dar de alta el lote.";

                        }
                    }
                    else
                    {
                        lblMensajes.Text = "La cantidad debe ser mayor a cero.";
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

        #endregion

    }
}