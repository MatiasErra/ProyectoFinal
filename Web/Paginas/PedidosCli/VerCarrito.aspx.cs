using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Catalogo;
using Web.Paginas.Productos;

namespace Web.Paginas.Pedidos
{
    public partial class VerCarrito : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] == null)
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }
            else
            {
                this.MasterPageFile = "~/Master/MCliente.Master";
            }
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

                listar();


            }


        }

        #region 

        private void listar()
        {

            listarPedSinFinalizar();

            if (System.Web.HttpContext.Current.Session["PedidoCompra"] != null)
            {
                int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompra"].ToString());
                listarProductos(idPedido);
            }
            else
            {
                lblPaginas.Visible = false;
                lstProducto.Visible = false;
            }



        }
        private void listarPedSinFinalizar()
        {
            int idCli = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());


            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            List<Pedido> lsPedidos = web.listPedidoCli(idCli);
            if (lsPedidos.Count > 0)
            {
                Pedido PedidoSinF = ObtenerDatosSinF(lsPedidos);

                if (PedidoSinF.IdPedido != 0)
                {

                    lblheaderPedido.Visible = true;
                    lblFooterPedido.Visible = true;

                    txtIdPedido.Text = PedidoSinF.IdPedido.ToString();
                    txtfchPedido.Text = PedidoSinF.FechaPedido.ToString();
                    txtprecioFin.Text = PedidoSinF.Costo.ToString() + " $";

                    btnFinalizarPedido.Visible = true;
                }
                else
                {
                    lblheaderPedido.Visible = false;
                    lblFooterPedido.Visible = false;

                    btnFinalizarPedido.Visible = false;
                    lblMensajes.Text = "Carrito vacío.";
                }

            }
            else
            {
                lblFooterPedido.Visible = false;
                lblheaderPedido.Visible = false;
                lblMensajes.Text = "Carrito vacío.";
                btnFinalizarPedido.Visible = false;
            }
        }

        public Pedido ObtenerDatosSinF(List<Pedido> pedidos)
        {

            Pedido lstPedidoSinF = new Pedido();

            foreach (Pedido unPed in pedidos)
            {
                if (unPed.Estado.Equals("Sin finalizar"))

                {
                    Pedido pedido = new Pedido();
                    pedido.IdPedido = unPed.IdPedido;
                    pedido.Estado = unPed.Estado.ToString();
                    pedido.FechaPedido = unPed.FechaPedido;
                    pedido.Costo = unPed.Costo;
                    System.Web.HttpContext.Current.Session["PedidoCompra"] = unPed.IdPedido.ToString();
                    lstPedidoSinF = pedido;
                    break;

                }
                else
                {
                    Pedido pedido = new Pedido();
                    pedido.IdPedido = 0;
                    lstPedidoSinF = pedido;
                }


            }

            return lstPedidoSinF;
        }






        private int PagMax()
        {
            //Devuelve la cantidad de productos por pagina
            return 4;
        }

        private List<string[]> obtenerProductos(int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<string[]> productos = Web.buscarPedidoProd(idPedido);

            return productos;
        }

        private void listarProductos(int idPedido)
        {
            List<string[]> productos = obtenerProductos(idPedido);
            List<string[]> productosPagina = new List<string[]>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

            foreach (string[] unProducto in productos)
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
                lblMensajes.Text = "No se encontro ningún producto a este pedido.";

                lblPaginas.Visible = false;

                lstProducto.Visible = false;

            }
            else
            {

                lblMensajes.Text = "";
                modificarPagina(idPedido);


                lstProducto.Visible = true;
                lstProducto.DataSource = null;
                lstProducto.DataSource = DtObtenerProductos(productosPagina);
                lstProducto.DataBind();

            }


        }

        private void modificarPagina(int idPedido)
        {
            List<string[]> productos = obtenerProductos(idPedido);
            double pxp = PagMax();
            double count = productos.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();
            lblPaginas.Visible = true;
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

        public DataTable DtObtenerProductos(List<string[]> pedidos)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] {
             new DataColumn("Nombre", typeof(string)),
             new DataColumn("Tipo", typeof(string)),
             new DataColumn("Imagen", typeof(string)),
             new DataColumn("Precio", typeof(string)),
             new DataColumn("Cantidad", typeof(string)),
             new DataColumn("CantidadDisp", typeof(string)),
             new DataColumn("PecioTotal", typeof(string))
            });

            foreach (string[] ped in pedidos)
            {
                DataRow dr = dt.NewRow();

                dr["Nombre"] = ped[2].ToString();
                dr["Tipo"] = ped[3].ToString();
                dr["Precio"] = ped[4].ToString() + " $";
                string Imagen = "data:image/jpeg;base64,";
                Imagen += ped[5].ToString();
                Imagen = $"<img style=\"max-width:100px\" src=\"{Imagen}\">";
                dr["Imagen"] = Imagen;
                dr["Cantidad"] = ped[6].ToString();


                string CantTotal = ped[7].ToString();
                string[] CantTotalArry = CantTotal.Split(' ');
                string CantRess = ped[8].ToString();
                string[] CantRessArry = CantRess.Split(' ');

                int numCantTotal = int.Parse(CantTotalArry[0].ToString());
                int numCantRess = int.Parse(CantRessArry[0].ToString());

                int Total = numCantTotal - numCantRess;

                dr["CantidadDisp"] = Total.ToString() + " " + CantTotalArry[1].ToString();


                string[] arrayCant = ped[6].ToString().Split(' ');

                double PrecioSubTotal = double.Parse(ped[4].ToString()) * double.Parse(arrayCant[0].ToString());

                dr["PecioTotal"] = PrecioSubTotal.ToString() + " $";


                dt.Rows.Add(dr);
            }

            return dt;
        }


        private string cantResActu(int idProducto, int idPedido, string cantidadAdd)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            int cant = 0;
            string resultado = "";
            List<Pedido_Prod> pedido_Prod = Web.listPedidoCli_Prod(idProducto);
            Producto producto = Web.buscarProducto(idProducto);


            foreach (Pedido_Prod pedidos in pedido_Prod)
            {

                if (pedidos.IdProducto.Equals(producto.IdProducto)
                    && !pedidos.IdPedido.Equals(idPedido)

                    )
                {
                    string textCant = pedidos.Cantidad;
                    string[] str = textCant.Split(' ');
                    textCant = str[0];
                    cant += int.Parse(textCant);



                }

            }

            int cantidad = int.Parse(cantidadAdd);
            int total = cant + cantidad;
            resultado = total.ToString() + " " + producto.TipoVenta.ToString();
            return resultado;
        }


        private string cantResActuElim(int idProducto, int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            int cant = 0;
            string resultado = "0";
            List<Pedido_Prod> pedido_Prod = Web.listPedidoCli_Prod(idProducto);
            Producto producto = Web.buscarProducto(idProducto);


            foreach (Pedido_Prod pedidos in pedido_Prod)
            {

                if (pedidos.IdProducto.Equals(producto.IdProducto)
                    && !pedidos.IdPedido.Equals(idPedido)

                    )
                {
                    string textCant = pedidos.Cantidad;
                    string[] str = textCant.Split(' ');
                    textCant = str[0];
                    cant += int.Parse(textCant);



                }

            }

            resultado = cant.ToString() + " " + producto.TipoVenta.ToString();
            return resultado;
        }









        private double costoActual(int idPedido, int idProducto, double precio)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            double precioNow = 0;
            double preciototal = 0;


            List<string[]> pedido_Prod = Web.buscarPedidoProd(idPedido);


            foreach (string[] pedidos in pedido_Prod)
            {

                if (pedidos[0].ToString().Equals(idPedido.ToString())
                    && !pedidos[1].ToString().Equals(idProducto.ToString()))
                {
                    string idPedid1 = pedidos[4].ToString();
                    string cant = pedidos[6].ToString();
                    string[] cantarry = cant.Split(' ');

                    precioNow = double.Parse(pedidos[4].ToString()) * double.Parse(cantarry[0].ToString());

                    preciototal += precioNow;



                }

            }

            double total = precio + preciototal;

            return total;
        }

        private double costoActualElim(int idPedido, int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            double precioNow = 0;
            double preciototal = 0;


            List<string[]> pedido_Prod = Web.buscarPedidoProd(idPedido);

            foreach (string[] pedidos in pedido_Prod)
            {

                if (pedidos[0].ToString().Equals(idPedido.ToString())
                    && !pedidos[1].ToString().Equals(idProducto.ToString()))
                {
                    string idPedid1 = pedidos[4].ToString();
                    string cant = pedidos[6].ToString();
                    string[] cantarry = cant.Split(' ');

                    precioNow = double.Parse(pedidos[4].ToString()) * double.Parse(cantarry[0].ToString());

                    preciototal += precioNow;



                }

            }



            return preciototal;
        }



        #endregion

        #region botones 


        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            ControladoraWeb web = ControladoraWeb.obtenerInstancia();

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombre = selectedrow.Cells[0].Text;

            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompra"].ToString());
            int idProducto = 0;


            List<string[]> productos = obtenerProductos(idPedido);
            foreach (string[] Unprod in productos)
            {
                if (Unprod[2].Equals(nombre))
                {
                    idProducto = int.Parse(Unprod[1].ToString());
                }

            }

            string cantRess = cantResActuElim(idProducto, idPedido);

            double Preciototal = costoActualElim(idPedido, idProducto);


            if (web.bajaPedidoProd(idPedido, idProducto, cantRess, Preciototal))
            {
                lblPaginaAct.Text = "1";


                listar();
                lblMensajes.Text = "Se elimino un producto del pedido";
            }
            else
            {
                lblMensajes.Text = "No se pudo quitar este producto del pedido";
            }

        }
        protected void btnFinalizarPedio_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompra"].ToString());
            int i = 0;
            int idProducto = 0;

            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            string estado = "Sin confirmar";

            List<string[]> pedidos_prod = web.buscarPedidoProd(idPedido);
            foreach (string[] pedidos_prods in pedidos_prod)
            {
                string cant = pedidos_prods[6].ToString();
                string[] cantarry = cant.Split(' ');

                if (cantarry[0].ToString().Equals("0"))
                {
                    i++;
                }
                idProducto = int.Parse(pedidos_prods[1].ToString());

            }
            if (idProducto != 0)
            {
                if (i == 0)
                {
                    btnBorrarPedido.Visible = false;
                    if (web.cambiarEstadoPed(idPedido, estado))
                    {
                        System.Web.HttpContext.Current.Session["PedidoCompra"] = null;

                        lblMensajes.Text = "Pedido finalizado";
                        listar();
                        Response.Redirect("/Paginas/PedidosCli/VerPedidosCli"); 
                    }
                }
                else
                {
                    lblMensajes.Text = "Hay productos sin cantidad en el pedido";
                }
            }
            else
            {
                lblMensajes.Text = "No hay productos en el pedido";

                btnBorrarPedido.Visible = true;

            }
        }
        protected void btnBorrarPedido_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(txtIdPedido.Text.ToString());
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            if (web.bajaPedido(idPedido))
            {
                lblPaginaAct.Text = "1";
                listar();
                lblMensajes.Text = "Pedido Eliminado";
                System.Web.HttpContext.Current.Session["PedidoCompra"] = null;
                btnBorrarPedido.Visible = false;

            }
        }

        protected void btnModificarCantidad_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            TextBox txtCant = (TextBox)row.FindControl("txtCantidad");

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string StockDisp = selectedrow.Cells[4].Text;

            string[] StockDispArry = StockDisp.Split(' ');
            int StockDispnum = int.Parse(StockDispArry[0].ToString());
            if (!txtCant.Text.Equals(""))
            {
                int numCant = int.Parse(txtCant.Text.ToString());
                if (StockDispnum >= numCant)
                {


                    string nombre = selectedrow.Cells[0].Text;

                    int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompra"].ToString());
                    int idProducto = 0;


                    List<string[]> productos = obtenerProductos(idPedido);
                    foreach (string[] Unprod in productos)
                    {
                        if (Unprod[2].Equals(nombre))
                        {
                            idProducto = int.Parse(Unprod[1].ToString());
                        }

                    }
                    Producto prod = Web.buscarProducto(idProducto);

                    string CantRes = cantResActu(idProducto, idPedido, txtCant.Text.ToString());

                    string cant = txtCant.Text + " " + prod.TipoVenta;

                    double precio = double.Parse(txtCant.Text) * double.Parse(prod.Precio.ToString());

                    double PrecioTotal = costoActual(idPedido, idProducto, precio);




                    if (Web.modCantPedidoCli(idPedido, idProducto, cant, CantRes, PrecioTotal))
                    {

                        listar();
                        lblMensajes.Text = "Cantidad ingresada";
                    }
                    else
                    {
                        lblMensajes.Text = "Cantidad no ingresada";
                    }


                }
                else
                {
                    lblMensajes.Text = "La cantidad ingresada no puede ser mayor a la cantidad disponible";
                }
            }
            else
            {
                lblMensajes.Text = "La cantidad ingresada no puede ser vacía";
            }
        }


        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }



        #endregion


    }
}