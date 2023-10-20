using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmInicio : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["AdminIniciado"] = 168792794; // admin global 1 
            //System.Web.HttpContext.Current.Session["AdminIniciado"] = 206676677; // admin global 2 
            //System.Web.HttpContext.Current.Session["ClienteIniciado"] = 1481022565;

            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null)
            {
                this.MasterPageFile = "~/Master/MCliente.Master";

            }
            else if (System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
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
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
                }
            }
            else
            {
                this.MasterPageFile = "~/Master/Default.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(System.Web.HttpContext.Current.Session["ClienteIniciado"] != null || System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
                {
                    // Catalogo
                    Imagen.ImageUrl = "~/Imagenes/image3.png";
                }
                else
                {
                    // Iniciar sesion
                    Imagen.ImageUrl = "~/Imagenes/image2.png";
                }
            }
        }

        /*private List<Producto> obtenerProductos()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<Producto> productos = Web.buscarProductoCatFiltro("", "", "", "");
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

            List<Producto> lstProductos = LstObtenerProductosSinPed(productos);

            foreach(Producto unProducto in lstProductos)
            {
                if(productosPagina.Count < 5)
                {
                    productosPagina.Add(unProducto);
                }
            }

            lstProducto.Visible = true;
            lstProducto.DataSource = null;
            lstProducto.DataSource = ObtenerDatos(productosPagina);
            lstProducto.DataBind();


        }



        public List<Producto> LstObtenerProductosSinPed(List<Producto> productos)
        {
            List<Producto> lstProductos = new List<Producto>();
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();


            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null)
            {

                int idClinete = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
                List<Pedido> lstPedido = Web.listPedidoCli(idClinete);
                int i = 0;

                foreach (Producto prod in productos)
                {
                    i = 0;
                    int idProducto = prod.IdProducto;


                    List<Pedido_Prod> lstPedidoProd = Web.listPedidoCli_Prod(idProducto);

                    foreach (Pedido pedido in lstPedido)
                    {
                        List<string[]> lstPedidLote = Web.buscarPedidoLote(pedido.IdPedido);
                        if (lstPedidLote.Count > 0)
                        {
                            foreach (string[] unPedidoLote in lstPedidLote)
                            {
                                if (unPedidoLote[0].ToString().Equals(pedido.IdPedido.ToString())
                                 && unPedidoLote[1].ToString().Equals(idProducto.ToString())
                                && pedido.Estado.ToString().Equals("Sin confirmar") ||
                                    pedido.Estado.ToString().Equals("Sin finalizar")
                                    )

                                {
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            foreach (Pedido_Prod pedido_Prod in lstPedidoProd)
                            {

                                if (pedido_Prod.IdProducto == idProducto
                                    && pedido_Prod.IdPedido == pedido.IdPedido
                                    && (pedido.Estado.ToString().Equals("Sin confirmar") ||
                                    pedido.Estado.ToString().Equals("Sin finalizar"))
                                    )
                                {
                                    i++;
                                }
                            }
                        }
                    }

                    if (i == 0)
                    {
                        Producto producto = new Producto();
                        producto.IdProducto = prod.IdProducto;
                        producto.Nombre = prod.Nombre;
                        producto.Tipo = prod.Tipo;
                        producto.TipoVenta = prod.TipoVenta;
                        producto.Imagen = prod.Imagen;
                        producto.Precio = prod.Precio;
                        lstProductos.Add(producto);
                    }


                }


            }
            else
            {
                lstProductos = productos;
            }
            return lstProductos;


        }

        public DataTable ObtenerDatos(List<Producto> Productos)
        {
            DataTable dt = new DataTable();


            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                new DataColumn("TipoVenta", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Imagen", typeof(string))

           });

            foreach (Producto unProducto in Productos)
            {
                DataRow dr = dt.NewRow();
                dr["Nombre"] = unProducto.Nombre.ToString();
                dr["Tipo"] = unProducto.Tipo.ToString();
                dr["TipoVenta"] = unProducto.TipoVenta.ToString();
                dr["Precio"] = unProducto.Precio.ToString() + " $";
                dr["Imagen"] = unProducto.Imagen.ToString();




                dt.Rows.Add(dr);
            }
            return dt;
        }*/

        protected void btnVerCat_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Catalogo/frmCatalogo");
        }

        protected void LinkImagen_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null || System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
            {
                // Catalogo
                Response.Redirect("/Paginas/Catalogo/frmCatalogo");
            }
            else
            {
                // Iniciar sesion
                Response.Redirect("/Paginas/Clientes/iniciarSesion");
            }
        }
    }
}