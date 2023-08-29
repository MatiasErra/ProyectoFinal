using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmCatalogo : System.Web.UI.Page
    {

        #region Load
        protected void Page_PreInit(object sender, EventArgs e)
        {
       

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

                CargarListFiltroTipo();
                CargarListFiltroTipoVen();
                CargarListOrdenarPor();
                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;
                }

                if (System.Web.HttpContext.Current.Session["FiltroTipo"] != null)
                {
                    listFiltroTipo.SelectedValue = System.Web.HttpContext.Current.Session["FiltroTipo"].ToString();
                    System.Web.HttpContext.Current.Session["FiltroTipo"] = null;
                }

                if (System.Web.HttpContext.Current.Session["FiltroTipoVen"] != null)
                {
                    listFiltroVen.SelectedValue = System.Web.HttpContext.Current.Session["FiltroTipoVen"].ToString();
                    System.Web.HttpContext.Current.Session["FiltroTipoVen"] = null;
                }

                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;
                }


       

                listarPagina();

            }

        }
        #endregion

        #region Utilidad


        private void limpiar()
        {
            listFiltroVen.SelectedValue = "Filtrar por tipo de venta";
            listFiltroTipo.SelectedValue = "Filtrar por tipo";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";
            listarPagina();
        }


            private int PagMax()
        {
            //Devuelve la cantidad de productos por pagina
            return 4;
        }

        private List<Producto> obtenerProductos()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;
            string tipo = "";
            string tipoVen = "";
            string ordenar = "";
            if (listFiltroTipo.SelectedValue != "Filtrar por tipo")
            {
                tipo = listFiltroTipo.SelectedValue;
            }


            if (listFiltroVen.SelectedValue != "Filtrar por tipo de venta")
            {
                tipoVen = listFiltroVen.SelectedValue;
            }


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }

            List<Producto> productos = Web.buscarProductoFiltro(buscar, tipo, tipoVen, ordenar);
            foreach (Producto unProducto in productos)
            {
                string Imagen = "data:image/jpeg;base64,";
                Imagen += unProducto.Imagen;
                Imagen = $"<img style=\"max-width:100px\" src=\"{Imagen}\">";
                unProducto.Imagen = Imagen;
            }

            return productos;
        }

        private void listarPagina()
        {
            List<Producto> productos = obtenerProductos();
            List<Producto> productosPagina = new List<Producto>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Producto unProducto in productos)
            {
                if (productosPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    productosPagina.Add(unProducto);
                }

                cont++;
            }

            if (productosPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún producto.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstProducto.Visible = false;
            }
            else
            {
                lblMensajes.Text = "";
                modificarPagina();
                lstProducto.Visible= true;
                lstProducto.DataSource = null;
                lstProducto.DataSource = productosPagina;
                lstProducto.DataBind();
            }


        }

        private void modificarPagina()
        {
            List<Producto> productos = obtenerProductos();
            double pxp = PagMax();
            double count = productos.Count;
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

        #region DropDownBoxes

        #region Filtro

        public void CargarListFiltroTipo()
        {
            listFiltroTipo.DataSource = null;
            listFiltroTipo.DataSource = createDataSourceFiltroTipo();
            listFiltroTipo.DataTextField = "nombre";
            listFiltroTipo.DataValueField = "id";
            listFiltroTipo.DataBind();
        }

        ICollection createDataSourceFiltroTipo()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Filtrar por tipo", "Filtrar por tipo", dt));
            dt.Rows.Add(createRow("Fruta", "Fruta", dt));
            dt.Rows.Add(createRow("Verdura", "Verdura", dt));



            DataView dv = new DataView(dt);
            return dv;
        }

        public void CargarListFiltroTipoVen()
        {
            listFiltroVen.DataSource = null;
            listFiltroVen.DataSource = createDataSourceFiltroTipoVen();
            listFiltroVen.DataTextField = "nombre";
            listFiltroVen.DataValueField = "id";
            listFiltroVen.DataBind();
        }


        ICollection createDataSourceFiltroTipoVen()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Filtrar por tipo de venta", "Filtrar por tipo de venta", dt));
            dt.Rows.Add(createRow("Kilos", "Kilos", dt));
            dt.Rows.Add(createRow("Unidades", "Unidades", dt));

            DataView dv = new DataView(dt);
            return dv;
        }
        #endregion

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
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));
            dt.Rows.Add(createRow("Tipo de venta", "Tipo de venta", dt));


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

        protected void listFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void listOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        #endregion

        #region Botones

        protected void btnVerProducto_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            List<Producto> productos = obtenerProductos();
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombre = selectedrow.Cells[0].Text;
            foreach (Producto unProducto in productos)
            {
                if (unProducto.Nombre.Equals(nombre))
                {
                    producto = unProducto;
                }
            }
            System.Web.HttpContext.Current.Session["catalogoProducto"] = producto.IdProducto;
            Response.Redirect("/Paginas/Catalogo/frmVerProducto");
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
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltroTipo.SelectedValue;
            System.Web.HttpContext.Current.Session["FiltroTipoVen"] = listFiltroVen.SelectedValue;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltroTipo.SelectedValue;
            System.Web.HttpContext.Current.Session["FiltroTipoVen"] = listFiltroVen.SelectedValue;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion


    }
}