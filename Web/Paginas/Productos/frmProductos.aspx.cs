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
                listar();
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



            }
        }

        private void listar()
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Producto> productos = Web.listProductos();
            foreach (Producto unProducto in productos)
            {
                string Imagen = "data:image/jpeg;base64,";
                Imagen += unProducto.Imagen;
                Imagen = $"<img style=\"max-width:100px\" src=\"{Imagen}\">";
                unProducto.Imagen = Imagen;
            }
            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                lstProductoSelect.Visible = true;
                lstProductoSelect.DataSource = null;
                lstProductoSelect.DataSource = productos;
                lstProductoSelect.DataBind();
            }
            else
            {
                lstProducto.Visible = true;
                lstProducto.DataSource = null;
                lstProducto.DataSource = productos;
                lstProducto.DataBind();

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

            listar();
        }

        #region DropDownBoxes

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

        #endregion

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text.ToLower();
            List<Producto> productos = Web.buscarVarProductos(value);
            lstProducto.DataSource = null;
            foreach (Producto unProducto in productos)
            {
                string Imagen = "data:image/jpeg;base64,";
                Imagen += unProducto.Imagen;
                Imagen = $"<img style=\"max-width:50px\" src=\"{Imagen}\">";
                unProducto.Imagen = Imagen;
            }
            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)

            {
                if (txtBuscar.Text != "")
                {
                    if (productos.Count > 0)
                    {
                        lstProductoSelect.Visible = true;
                        lblMensajes.Text = "";
                        lstProductoSelect.DataSource = productos;
                        lstProductoSelect.DataBind();
                    }
                    else
                    {
                        lstProductoSelect.Visible = false;
                        lblMensajes.Text = "No se encontró ningún producto.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe ingresar algún dato en el buscador para buscar.";
                }
            }
            else
            {

                if (txtBuscar.Text != "")
                {
                    if (productos.Count > 0)
                    {
                        lstProducto.Visible = true;
                        lblMensajes.Text = "";
                        lstProducto.DataSource = productos;
                        lstProducto.DataBind();
                    }
                    else
                    {
                        lstProducto.Visible = false;
                        lblMensajes.Text = "No se encontró ningún producto.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe ingresar algún dato en el buscador para buscar.";
                }
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
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
            List<Producto> lstProductos = Web.listProductos();
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
                            listar();
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
            List<string[]> lotes = Web.listLotes();
            foreach (string[] unLote in lotes)
            {
                if (int.Parse(unLote[0].ToString()) == idProducto)
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
                        listar();
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }




    }
}