using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Productos;

namespace Web.Paginas.Pedidos
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] == null)
            {
                Response.Redirect("/Paginas/PedidosCli/VerPedidosCli");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["ClienteIniciado"] == null)
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
                        else if (admin.TipoDeAdmin == "Administrador de pedidos")
                        {
                            this.MasterPageFile = "~/Master/APedidos.Master";
                        }
                    }
                    else
                    {
                        Response.Redirect("/Paginas/Nav/frmInicio");
                    }
                }
                else
                {
                    this.MasterPageFile = "~/Master/MCliente.Master";

                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();



                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }



                listarProductos(idPedido);


                if (System.Web.HttpContext.Current.Session["Estado"].ToString() == "Confirmado" && System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
                {
                    btnModPedido.Visible = true;
                }
                else
                {
                    btnModPedido.Visible = false;
                }

                if (System.Web.HttpContext.Current.Session["EstadoViajeSel"].ToString() == "Asignado")
                {
                    btnVerViaje.Visible = true;
                }
                else
                {
                    btnVerViaje.Visible = false;
                }

                if (System.Web.HttpContext.Current.Session["Estado"].ToString() == "En viaje" && System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
                {
                    btnFinalizarPedido.Visible = true;
                }
                else
                {
                    btnFinalizarPedido.Visible = false;
                }

            }
        }




        private int PagMax()
        {
            //Devuelve la cantidad de productos por pagina
            return 4;
        }

        private List<string[]> obtenerProductos(int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            
            List<string[]> productos = new List<string[]>();

            List<Pedido> lstPedido = Web.BuscarPedidoFiltro("", "", "", 0, 9999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
            if (lstPedido.Count > 0)
            {
                string Estado = "";
                foreach (Pedido unPed in lstPedido)
                {
                    if (unPed.IdPedido == idPedido)
                    {
                        Estado = unPed.Estado;
                        break;

                    }
                }

                if (Estado.ToString() == "Sin confirmar" || Estado.ToString() == "Sin finalizar")
                {
                    productos = Web.buscarPedidoProd(idPedido);
                    System.Web.HttpContext.Current.Session["Estado"] = Estado.ToString();
                }
                else
                {
                    productos = Web.buscarPedidoLote(idPedido);
                    System.Web.HttpContext.Current.Session["Estado"] = Estado.ToString();
                }
            }
            return productos;
        }

        private void listarProductos(int idPedido)
        {

            List<string[]> productos = obtenerProductos(idPedido);
            string Estado = System.Web.HttpContext.Current.Session["Estado"].ToString();
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
                lblMensajes.Text = "No se encontro ningún producto en este pedido.";


                txtPaginas.Visible = false;
                lstProducto.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lblPaginaAnt.Visible = false;

            }
            else
            {
                if (Estado.ToString() == "Sin confirmar" || Estado.ToString() == "Sin finalizar")
                {
                    lblMensajes.Text = "";
                    txtPaginas.Visible = true;
                    modificarPagina(idPedido);
                    lstProductoLote.Visible = false;
                    lstProducto.Visible = true;
                    lstProducto.DataSource = null;
                    lstProducto.DataSource = ObtenerProductos(productosPagina);
                    lstProducto.DataBind();
                }
                else
                {

                    lblMensajes.Text = "";
                    txtPaginas.Visible = true;
                    modificarPagina(idPedido);
                    
                    lstProducto.Visible = false;
                    lstProductoLote.Visible = true;
                    lstProductoLote.DataSource = null;
                    lstProductoLote.DataSource = ObtenerProductosLote(productosPagina);
                    lstProductoLote.DataBind();
                }
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









        public DataTable ObtenerProductos(List<string[]> pedidos)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {




                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                new DataColumn("Imagen", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
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


                string[] arrayCant = ped[6].ToString().Split(' ');

                double PrecioSubTotal = double.Parse(ped[4].ToString()) * double.Parse(arrayCant[0].ToString());

                dr["PecioTotal"] = PrecioSubTotal.ToString() + " $";
                dt.Rows.Add(dr);





            }
            return dt;


        }




        public DataTable ObtenerProductosLote(List<string[]> pedidos)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] {



                 new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                new DataColumn("Imagen", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
            new DataColumn("PecioTotal", typeof(string))
            });



            foreach (string[] ped in pedidos)
            {
                DataRow dr = dt.NewRow();

                dr["NombreGranja"] = ped[4].ToString();
                dr["NombreProducto"] = ped[2].ToString();
                dr["FchProduccion"] = ped[5].ToString();

                dr["Tipo"] = ped[6].ToString();
                dr["Precio"] = ped[7].ToString() + " $";
                string Imagen = "data:image/jpeg;base64,";
                Imagen += ped[8].ToString();
                Imagen = $"<img style=\"max-width:100px\" src=\"{Imagen}\">";
                dr["Imagen"] = Imagen;



                dr["Cantidad"] = ped[9].ToString();


                string[] arrayCant = ped[9].ToString().Split(' ');

                double PrecioSubTotal = double.Parse(ped[7].ToString()) * double.Parse(arrayCant[0].ToString());

                dr["PecioTotal"] = PrecioSubTotal.ToString() + " $";

                dt.Rows.Add(dr);

            }
            return dt;


        }



        #region botones 


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

        protected void btnVerViaje_Click(object sender, EventArgs e)
        {

            Response.Redirect("/Paginas/Viajes/verLoteDelViaje");

        }

        protected void btnModPedido_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            string estado = System.Web.HttpContext.Current.Session["Estado"].ToString();

            if (estado == "Confirmado")
            {
                if (Web.cambiarEstadoPed(idPedido, "Sin confirmar", 0))
                {
                    lblMensajes.Text = "Estado modificado a Sin finalizar";
                    System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Estado modificado a Sin finalizar";

                    Response.Redirect("/Paginas/PedidosAdm/frmPedido");
                }
                else
                {
                    lblMensajes.Text = "No se pudo modificar el estado";
                }
            }
            else
            {
                lblMensajes.Text = "El pedido debe estar sin confirmar para poder confirmarlo";
            }




        }


        protected void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string estado = System.Web.HttpContext.Current.Session["Estado"].ToString();

            if (estado == "En viaje")
            {
                List<Viaje_Lot_Ped> viajesPed = Web.buscarViajePedLote(idPedido, 0);
                int cont = 0;
                foreach (Viaje_Lot_Ped unVLP in viajesPed)
                {
                    Viaje unViaje = Web.buscarViaje(unVLP.IdViaje);
                    if (unViaje.Estado != "Finalizado") cont++;
                }
                if (cont == 0)
                {
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.cambiarEstadoPed(idPedido, "Finalizado", idAdmin)) lblMensajes.Text = "El pedido ha sido finalizado.";
                }
                else lblMensajes.Text = "No todos los viajes de este pedido han finalizado.";
            }
            else lblMensajes.Text = "El pedido debe estar en viaje para poder finalizarlo.";
        }



        #endregion


    }

}
