using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Productos
{
    public partial class frmProductos : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                CargarListTipo();
                CargarListTipoVenta();
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    btnVolver.Visible = true;
                    lstProducto.Visible = false;
                    lstProductoSelect.Visible = true;

                }
                if (System.Web.HttpContext.Current.Session["ProductoMod"] != null)

                {
                    lblMensajes.Text = "Productos Modificado";
                }

                System.Web.HttpContext.Current.Session["idProductoMod"] = null;


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

   

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || listTipo.SelectedValue == "Seleccione un tipo de producto" || listTipoVenta.SelectedValue == "Seleccione un tipo de venta" || !fileImagen.HasFile)
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

            txtNombre.Text = "";
            fileImagen.Attributes.Clear();
            lstProducto.SelectedIndex = -1;
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
                lstProductoSelect.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)

                {
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstProductoSelect.Visible = true;
                    lstProductoSelect.DataSource = null;
                    lstProductoSelect.DataSource = productosPagina;
                    lstProductoSelect.DataBind();
                }
                else
                {
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstProducto.Visible = true;
                    lstProducto.DataSource = null;
                    lstProducto.DataSource = productosPagina;
                    lstProducto.DataBind();
                }
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

        #region Tipo

        public void CargarListTipo()
        {
            listTipo.DataSource = null;
            listTipo.DataSource = createDataSourceTipo();
            listTipo.DataTextField = "nombre";
            listTipo.DataValueField = "id";
            listTipo.DataBind();
        }

        ICollection createDataSourceTipo()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un tipo de producto", "Seleccione un tipo de producto", dt));
            dt.Rows.Add(createRow("Fruta", "Fruta", dt));
            dt.Rows.Add(createRow("Verdura", "Verdura", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Tipo Venta

        public void CargarListTipoVenta()
        {
            listTipoVenta.DataSource = null;
            listTipoVenta.DataSource = createDataSourceTipoVenta();
            listTipoVenta.DataTextField = "nombre";
            listTipoVenta.DataValueField = "id";
            listTipoVenta.DataBind();
        }

        ICollection createDataSourceTipoVenta()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un tipo de venta", "Seleccione un tipo de venta", dt));
            dt.Rows.Add(createRow("Kilos", "Kilos", dt));
            dt.Rows.Add(createRow("Unidades", "Unidades", dt));

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
            List<Producto> lstProductos = Web.buscarProductoFiltro(string.Empty, string.Empty, string.Empty, string.Empty);
            foreach (Producto producto in lstProductos)
            {
                if (producto.IdProducto.Equals(intGuid))
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

        private bool detectarImagen()
        {
            if (fileImagen.PostedFile.ContentLength < 2100000)
            {
                string archivo = System.IO.Path.GetExtension(fileImagen.FileName);
                string[] extenciones = { ".jpg", ".jpeg", ".png" };
                if (extenciones.Contains(archivo.ToLower()))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (detectarImagen())
                {
                    int id = GenerateUniqueId();
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string tipo = HttpUtility.HtmlEncode(listTipo.SelectedValue);
                    string tipoVenta = HttpUtility.HtmlEncode(listTipoVenta.SelectedValue);
                    byte[] fileBytes = fileImagen.FileBytes;
                    string imagen = Convert.ToBase64String(fileBytes);

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Producto unProducto = new Producto(id, nombre, tipo, tipoVenta, imagen);
                    if (Web.altaProducto(unProducto))
                    {
                        if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                        {
                            System.Web.HttpContext.Current.Session["idProductoSel"] = unProducto.IdProducto.ToString();
                            Response.Redirect("/Paginas/Lotes/frmAltaLotes");
                        }
                        else
                        {
                            limpiar();
                            listarPagina();
                            lblMensajes.Text = "Producto dado de alta con éxito.";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Ya existe un Producto con estos datos. Estos son los posibles datos repetidos (Nombre).";
                    }
                }
                else
                {
                    lblMensajes.Text = "El archivo debe ser una imagen o es muy grande.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        private bool loteExistente(int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string b = "";
            string d = "";
      
            List<Lote> lotes = Web.buscarFiltrarLotes(b,d);
            foreach (Lote unLote in lotes)
            {
                if (unLote.IdProducto == idProducto)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto unProducto = Web.buscarProducto(int.Parse(selectedrow.Cells[0].Text));
            if (unProducto != null)
            {
                if (!loteExistente(unProducto.IdProducto))
                {
                    if (Web.bajaProducto(unProducto.IdProducto))
                    {
                        limpiar();
                        listarPagina();
                        lblMensajes.Text = "Se ha borrado el producto.";
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido borrar el producto.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Hay un lote asociado a este producto.";
                }

            }
            else
            {
                lblMensajes.Text = "El producto no existe.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idProducto = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));


            System.Web.HttpContext.Current.Session["idProductoMod"] = idProducto;
            Response.Redirect("/Paginas/Productos/modProducto");
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            System.Web.HttpContext.Current.Session["idProductoSel"] = id;

            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

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



    }
}