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

                CargarListTipo();
                CargarListTipoVenta();
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null || System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
                {
                    btnVolver.Visible = true;
                    lstProducto.Visible = false;
                    lstProductoSelect.Visible = true;

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

                CargarListBuscar();
                CargarListOrdenarPor();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreProductoBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreProductoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreProductoBuscar"] = null;
                txtPrecioMenorBuscar.Text = System.Web.HttpContext.Current.Session["precioMenorProductoBuscar"] != null ? System.Web.HttpContext.Current.Session["precioMenorProductoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["precioMenorProductoBuscar"] = null;
                txtPrecioMayorBuscar.Text = System.Web.HttpContext.Current.Session["precioMayorProductoBuscar"] != null ? System.Web.HttpContext.Current.Session["precioMayorProductoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["precioMayorProductoBuscar"] = null;

                // Listas
                lstTipoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["tipoProductoBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoProductoBuscar"].ToString() : "Seleccione un tipo de producto";
                System.Web.HttpContext.Current.Session["tipoProductoBuscar"] = null;
                lstTipoVentaBuscar.SelectedValue = System.Web.HttpContext.Current.Session["tipoVenProductoBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoVenProductoBuscar"].ToString() : "Seleccione un tipo de venta";
                System.Web.HttpContext.Current.Session["tipoVenProductoBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstProducto"] != null ? System.Web.HttpContext.Current.Session["BuscarLstProducto"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstProducto"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorProducto"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorProducto"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorProducto"] = null;

                comprobarBuscar();
                listarPagina();

                if (System.Web.HttpContext.Current.Session["ProductoMod"] != null)
                {
                    lblMensajes.Text = "Productos Modificado";
                    System.Web.HttpContext.Current.Session["ProductoMod"] = null;
                }
            }
        }

        #endregion

        #region Utilidad

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || listTipo.SelectedValue == "Seleccione un tipo de producto" || listTipoVenta.SelectedValue == "Seleccione un tipo de venta" || !fileImagen.HasFile || txtPrecio.Text == "")
            {
                return true;
            }
            return false;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";

            txtNombre.Text = "";
            listTipo.SelectedValue = "Seleccione un tipo de producto";
            listTipoVenta.SelectedValue = "Seleccione un tipo de venta";
            fileImagen.Attributes.Clear();
            txtPrecio.Text = "";

            txtNombreBuscar.Text = "";
            lstTipoVentaBuscar.SelectedValue = "Seleccione un tipo de venta";
            lstTipoBuscar.SelectedValue = "Seleccione un tipo de producto";
            txtPrecioMenorBuscar.Text = "";
            txtPrecioMayorBuscar.Text = "";
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
            lblPaginaAct.Text = "1";
            listarPagina();
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
            Producto pro = new Producto(0, "", "", "", "", 0);
            List<Producto> lstProductos = Web.buscarProductoFiltro(pro, -1, -1, "");
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

        private bool loteExistente(int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = new Lote(0, 0, "", "", "", 0, 0);

            List<Lote> lotes = Web.buscarFiltrarLotes(lote, 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
            foreach (Lote unLote in lotes)
            {
                if (unLote.IdProducto == idProducto)
                {
                    return true;
                }
            }
            return false;
        }

        private void comprobarBuscar()
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre" ? true : false;
            lstTipoBuscar.Visible = listBuscarPor.SelectedValue == "Tipo" ? true : false;
            lstTipoVentaBuscar.Visible = listBuscarPor.SelectedValue == "Tipo venta" ? true : false;
            lblPrecio.Visible = listBuscarPor.SelectedValue == "Precio" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["nombreProductoBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["tipoProductoBuscar"] = lstTipoBuscar.SelectedValue != "Seleccione un tipo de producto" ? lstTipoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["tipoVenProductoBuscar"] = lstTipoVentaBuscar.SelectedValue != "Seleccione un tipo de venta" ? lstTipoVentaBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["precioMenorProductoBuscar"] = txtPrecioMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["precioMayorProductoBuscar"] = txtPrecioMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["BuscarLstProducto"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorProducto"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            //Devuelve la cantidad de productos por pagina
            return 6;
        }

        private List<Producto> obtenerProductos()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto producto = new Producto();
            producto.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            producto.Tipo = lstTipoBuscar.SelectedValue != "Seleccione un tipo de producto" ? lstTipoBuscar.SelectedValue : "";
            producto.TipoVenta = lstTipoVentaBuscar.SelectedValue != "Seleccione un tipo de venta" ? lstTipoVentaBuscar.SelectedValue : "";
            int precioMenor = txtPrecioMenorBuscar.Text == "" ? 0 : int.Parse(txtPrecioMenorBuscar.Text);
            int precioMayor = txtPrecioMayorBuscar.Text == "" ? 999999 : int.Parse(txtPrecioMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";
            List<Producto> productos = Web.buscarProductoFiltro(producto, precioMenor, precioMayor, ordenar);

            foreach (Producto unProducto in productos)
            {
                string Imagen = "data:image/jpeg;base64,";
                Imagen += unProducto.Imagen;
                Imagen = $"<img style=\"max-width:100px\" src=\"{Imagen}\">";
                unProducto.Imagen = Imagen;
                int cant = int.Parse(unProducto.CantTotal.Split(' ')[0]);
                int cantRes = int.Parse(unProducto.CantRes.Split(' ')[0]);
                unProducto.CantDisp = (cant - cantRes).ToString();
            }

            List<Producto> listaOrdenadaXcantDisp = productos.OrderByDescending(Producto => Producto.CantDisp.Equals("0")).ThenBy(Producto => Producto.CantDisp).ToList();
            listaOrdenadaXcantDisp.Reverse();
            List<Producto> lstResult = listOrdenarPor.SelectedValue == "Cantidad disponible" ? listaOrdenadaXcantDisp : productos;

            return lstResult;
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
                txtPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún producto.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstProducto.Visible = false;
                lstProductoSelect.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null || System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)

                {
                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstProductoSelect.Visible = true;
                    lstProductoSelect.DataSource = null;
                    lstProductoSelect.DataSource = ObtenerDatos(productosPagina);
                    lstProductoSelect.DataBind();
                }
                else
                {
                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstProducto.Visible = true;
                    lstProducto.DataSource = null;
                    lstProducto.DataSource = ObtenerDatos(productosPagina);
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
            lblPaginaAct.Visible = true;
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
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

            }

            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }



        public DataTable ObtenerDatos(List<Producto> productos)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("IdProducto", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                new DataColumn("TipoVenta", typeof(string)),
                new DataColumn("Imagen", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("CantTotal", typeof(string)),
                new DataColumn("CantRes", typeof(string)),
                new DataColumn("CantDisp", typeof(string)),


            });

            foreach (Producto unProd in productos)
            {

                DataRow dr = dt.NewRow();
                dr["IdProducto"] = unProd.IdProducto.ToString();
                dr["Nombre"] = unProd.Nombre.ToString();
                dr["Tipo"] = unProd.Tipo.ToString();
                dr["TipoVenta"] = unProd.TipoVenta.ToString();
                dr["Imagen"] = unProd.Imagen.ToString();
                dr["Precio"] = unProd.Precio.ToString() + "$";
                dr["CantTotal"] = unProd.CantTotal.ToString();
                dr["CantRes"] = unProd.CantRes.ToString();
                dr["CantDisp"] = unProd.CantDisp.ToString() + " " + unProd.TipoVenta.ToString();

                dt.Rows.Add(dr);



            }

            return dt;
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
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));
            dt.Rows.Add(createRow("Tipo de venta", "Tipo de venta", dt));
            dt.Rows.Add(createRow("Precio", "Precio", dt));
            dt.Rows.Add(createRow("Cantidad disponible", "Cantidad disponible", dt));
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

            lstTipoBuscar.DataSource = null;
            lstTipoBuscar.DataSource = createDataSourceTipo();
            lstTipoBuscar.DataTextField = "nombre";
            lstTipoBuscar.DataValueField = "id";
            lstTipoBuscar.DataBind();
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

            lstTipoVentaBuscar.DataSource = null;
            lstTipoVentaBuscar.DataSource = createDataSourceTipoVenta();
            lstTipoVentaBuscar.DataTextField = "nombre";
            lstTipoVentaBuscar.DataValueField = "id";
            lstTipoVentaBuscar.DataBind();
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
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));
            dt.Rows.Add(createRow("Tipo venta", "Tipo venta", dt));
            dt.Rows.Add(createRow("Precio", "Precio", dt));
            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text; dr[1] = Value;

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
                if (int.Parse(txtPrecioMenorBuscar.Text) <= int.Parse(txtPrecioMayorBuscar.Text)) num++;
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
                lblMensajes.Text = "El precio menor es mayor.";
                listBuscarPor.SelectedValue = "Precio";
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                Response.Redirect("/Paginas/Lotes/frmAltaLotes");
            }
            else
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (detectarImagen())
                {
                    if (int.Parse(txtPrecio.Text) > 0)
                    {
                        int id = GenerateUniqueId();
                        string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                        string tipo = HttpUtility.HtmlEncode(listTipo.SelectedValue);
                        string tipoVenta = HttpUtility.HtmlEncode(listTipoVenta.SelectedValue);
                        byte[] fileBytes = fileImagen.FileBytes;
                        string imagen = Convert.ToBase64String(fileBytes);
                        int precio = int.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));

                        int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Producto unProducto = new Producto(id, nombre, tipo, tipoVenta, imagen, precio);
                        if (Web.altaProducto(unProducto, idAdmin))
                        {
                            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                            {
                                System.Web.HttpContext.Current.Session["idProductoSel"] = unProducto.IdProducto.ToString();
                                Response.Redirect("/Paginas/Lotes/frmAltaLotes");
                            }
                            else if (System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
                            {
                                System.Web.HttpContext.Current.Session["productoLoteBuscar"] = unProducto.IdProducto.ToString();
                                Response.Redirect("/Paginas/Lotes/frmLotes");
                            }
                            else
                            {
                                limpiar();
                                lblPaginaAct.Text = "1";
                                listarPagina();
                                lblMensajes.Text = "Producto dado de alta con éxito.";
                            }
                        }
                        else lblMensajes.Text = "Ya existe un Producto con estos datos. Estos son los posibles datos repetidos (Nombre).";
                    }
                    else lblMensajes.Text = "El precio no puede ser cero.";
                }
                else lblMensajes.Text = "El archivo debe ser una imagen o es muy grande.";
            }
            else lblMensajes.Text = "Faltan datos.";
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
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.bajaProducto(unProducto.IdProducto, idAdmin))
                    {
                        limpiar();
                        lblPaginaAct.Text = "1";
                        listarPagina();
                        lblMensajes.Text = "Se ha borrado el producto.";
                    }
                    else lblMensajes.Text = "No se ha podido borrar el producto.";
                }
                else lblMensajes.Text = "No se ha podido eliminar el producto porque está asociado a un lote";
            }
            else lblMensajes.Text = "El producto no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";

            guardarBuscar();

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

            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                System.Web.HttpContext.Current.Session["idProductoSel"] = id;
                Response.Redirect("/Paginas/Lotes/frmAltaLotes");
            }
            else if (System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
            {
                System.Web.HttpContext.Current.Session["productoLoteBuscar"] = id;
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion

    }
}