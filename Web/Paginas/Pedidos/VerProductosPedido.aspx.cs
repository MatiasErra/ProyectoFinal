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

            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] == null)
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }
            else
            {
                this.MasterPageFile = "~/Master/MCliente.Master";
                if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] == null)
                {
                    Response.Redirect("/Paginas/Pedidos/VerPedidosCli");
                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());



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
                   lblMensajes.Text = "No se encontro ningún producto en este pedido.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstProducto.Visible = false;

            }
            else
            {

                  lblMensajes.Text = "";
                modificarPagina(idPedido);
                lstProducto.Visible = true;
                lstProducto.DataSource = null;
                lstProducto.DataSource = ObtenerProductos(productosPagina);
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









        public DataTable ObtenerProductos(List<string[]> pedidos)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {




                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                 new DataColumn("Imagen", typeof(string)),
                   new DataColumn("Precio", typeof(string)),

                new DataColumn("Cantidad", typeof(string))});

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



        #endregion





    }
}