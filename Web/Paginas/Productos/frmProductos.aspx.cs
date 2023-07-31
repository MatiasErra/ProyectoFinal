using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
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
                limpiar();
                listar();
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstProducto.DataSource = null;
            lstProducto.DataSource = Web.listProductos();
            lstProducto.DataBind();
        }

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtTipo.Text == "" || txtTipoVenta.Text == "" || !fileImagen.HasFile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool faltaIdProducto()
        {
            if (lstProducto.SelectedIndex == -1)
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
            txtId.Text = "";
            txtBuscar.Text = "";
            txtNombre.Text = "";
            txtTipo.Text = "";
            txtTipoVenta.Text = "";
            fileImagen.Attributes.Clear();
            imgImagen.ImageUrl = "";
            lstProducto.SelectedIndex = -1;
        }


        private void cargarProducto(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto producto = Web.buscarProducto(id);
            txtId.Text = producto.IdProducto.ToString();
            txtNombre.Text = producto.Nombre;
            txtTipo.Text = producto.Tipo;
            txtTipoVenta.Text = producto.TipoVenta;
            imgImagen.ImageUrl = string.Format("data:image/png;base64,"+ producto.Imagen);
        }

        protected void lstProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdProducto())
            {
                string linea = this.lstProducto.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                cargarProducto(id);
                lstProducto.SelectedIndex = -1;
            }
            else
            {
                lblMensajes.Text = "Debe seleccionar un producto de la lista.";
            }
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
            List<Producto> lstProductos = Web.listIdProductos();
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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
            List<Producto> productos = Web.buscarVarProductos(value);
            lstProducto.DataSource = null;



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
                lblMensajes.Text = "No se encontro ningun producto.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private bool detectarImagen()
        {
            if(fileImagen.PostedFile.ContentLength < 2100000)
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

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (detectarImagen())
                {
                    int id = GenerateUniqueId();
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string tipo = HttpUtility.HtmlEncode(txtTipo.Text);
                    string tipoVenta = HttpUtility.HtmlEncode(txtTipoVenta.Text);
                    string imagen = "";

                    if (fileImagen.HasFile)
                    {
                        byte[] fileBytes = fileImagen.FileBytes;
                        imagen = Convert.ToBase64String(fileBytes);
                    }

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Producto unProducto = new Producto(id, nombre, tipo, tipoVenta, imagen);
                    if (Web.altaProducto(unProducto))
                    {
                        limpiar();
                        lblMensajes.Text = "Producto dado de alta con exito.";
                        listar();

                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se pudo dar de alta la producto.";

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



        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (!txtId.Text.Equals(""))
            {
                //if existe producen
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Producto unProducto = Web.buscarProducto(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unProducto != null)
                {
                    if (Web.bajaProducto(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el producto.";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar el producto.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El producto no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un producto para eliminar. ";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (!txtId.Text.Equals(""))
                {
                    if (detectarImagen())
                    {
                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                        string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                        string tipo = HttpUtility.HtmlEncode(txtTipo.Text);
                        string tipoVenta = HttpUtility.HtmlEncode(txtTipoVenta.Text);
                        string imagen = "";

                        if (fileImagen.HasFile)
                        {
                            byte[] fileBytes = fileImagen.FileBytes;
                            imagen = Convert.ToBase64String(fileBytes);
                        }

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Producto unProducto = new Producto(id, nombre, tipo, tipoVenta, imagen);
                        if (Web.modProducto(unProducto))
                        {
                            limpiar();
                            lblMensajes.Text = "Producto modificado con exito.";
                            listar();
                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo modificar el producto.";
                            limpiar();
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "El archivo debe ser una imagen.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe seleccionar un producto.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}