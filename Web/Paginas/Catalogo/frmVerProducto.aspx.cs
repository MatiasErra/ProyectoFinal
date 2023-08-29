using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmVerProducto : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["catalogoProducto"] == null)
            {
                Response.Redirect("/Paginas/frmCatalogo");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int idProducto = (int)System.Web.HttpContext.Current.Session["catalogoProducto"];
                cargarProducto(idProducto);
            }
        }

        private void cargarProducto(int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto producto = Web.buscarProducto(idProducto);
            imgProducto.ImageUrl = string.Format("data:image/png;base64," + producto.Imagen);
            nombreProducto.Text = producto.Nombre;
            tipoProducto.Text = producto.Tipo;
            tipoVentaProducto.Text = "Cantidad ("+producto.TipoVenta+"):";
            btnRealizarPedido.OnClientClick = "return confirm('¿Desea realizar un pedido de "+ producto.Nombre +"?')";
            lblMensajes.Text = "";
        }

        #endregion

        protected void btnRealizarPedido_Click(object sender, EventArgs e)
        {
            if(txtCantidad.Text != "")
            {
                //Realizar el pedido
                lblMensajes.Text = "Realizar pedido";
            }
            else
            {
                lblMensajes.Text = "Debe ingresar una cantidad para poder realizar un pedido.";
            }
        }
    }
}