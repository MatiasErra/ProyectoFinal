using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Catalogo
{
    public partial class VerPedidoCli : System.Web.UI.Page
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
                System.Web.HttpContext.Current.Session["PedidoCompraSel"] = null;
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }
                listarPagina();
               

            }






        }
        #region utilidad

        private void listarPagina()
        {
       
            listarPedSinConfirar();
        }

     
 

        private int PagMax()
        {

            return 5;
        }



        private void listarPedSinConfirar()
        {
            List<Pedido> pedidos = obtenerPedidos();
            List<Pedido> pedidosPagina = new List<Pedido>();
            List<Pedido> pedidosSinC = ObtenerLstSinC(pedidos);

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Pedido unPedido in pedidosSinC)
            {
                if (pedidosPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    pedidosPagina.Add(unPedido);
                }

                cont++;
            }

            if (pedidosPagina.Count == 0)
            {

                lblPaginas.Visible = false;
          
                PedSinCon.Visible = false;
                LstPedSinCon.Visible = false;
            }
            else
            {
                lblPaginas.Visible = true;
                lblMensajes.Text = "";
            
            
               
                if (ObtenerDatosSinC(pedidosPagina).Rows.Count > 0)
                {

                    lblPaginas.Visible = true;
                    modificarPagina();
                    PedSinCon.Visible = true;
                    LstPedSinCon.Visible = true;
                    LstPedSinCon.DataSource = ObtenerDatosSinC(pedidosPagina);
                    LstPedSinCon.DataBind();

                }
                else
                {
                    PedSinCon.Visible = false;
                    LstPedSinCon.Visible = false;
                    lblPaginas.Visible = false;

             
                }
          
            }
        }

        
        private void modificarPagina()
        {
            List<Pedido> pedidos = obtenerPedidos();
            List<Pedido> pedidosSinC = ObtenerLstSinC(pedidos);
            double pxp = PagMax();
            double count = pedidosSinC.Count;
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
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

            }

            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }


        private List<Pedido> obtenerPedidos()
        {
            int idCli = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());

            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            List<Pedido> lsPedidoSin = web.listPedidoCli(idCli);

            return lsPedidoSin;
        }



        
     

        public DataTable ObtenerDatosSinC(List<Pedido> pedido)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("idPedido", typeof(int)),
                new DataColumn("Estado", typeof(string)),
                new DataColumn("fchPedido", typeof(string)),
                new DataColumn("Costo", typeof(double)),
                 new DataColumn("InfoEnv", typeof(string)),
            });

            foreach (Pedido unPed in pedido)
            {
                if (unPed.Estado.Equals("Sin confirmar"))

                {
                    DataRow dr = dt.NewRow();
                    dr["idPedido"] = unPed.IdPedido.ToString();
                    dr["Estado"] = unPed.Estado.ToString();
                    dr["fchPedido"] = unPed.FechaPedido.ToString();
                    dr["Costo"] = unPed.Costo.ToString();
                    dr["InfoEnv"] = unPed.InformacionEnvio.ToString();
                    dt.Rows.Add(dr);
                }


            }
        
            return dt;
        }


        public List<Pedido> ObtenerLstSinC(List<Pedido> pedido)
        {

            List<Pedido> lstPed = new List<Pedido>();
      

            foreach (Pedido unPed in pedido)
            {
                if (unPed.Estado.Equals("Sin confirmar"))

                {
                    Pedido ped = new Pedido();
                    ped.IdPedido = unPed.IdPedido;
                    ped.Estado = unPed.Estado.ToString();
                    ped.FechaPedido = unPed.FechaPedido.ToString();
                    ped.Costo = unPed.Costo;
                    ped.InformacionEnvio = unPed.InformacionEnvio;
                    lstPed.Add(ped);
                }


            }

            return lstPed;
        }





        #endregion



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


        protected void btnFinalizarPedio_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            string estado = "Sin confirmar";

            if (web.cambiarEstadoPed(idPedido, estado))
            {
                System.Web.HttpContext.Current.Session["PedidoCompra"] = null;
                lblMensajes.Text = "Pedido finalizado";
                listarPagina();
            }

        }


        protected void btnVerPedido_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["PedidoCompraSel"] = idPedido;


            Response.Redirect("/Paginas/PedidosCli/VerProductosPedido");

        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();


            int idCliente = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
            List<Pedido> lstPedio = web.listPedidoCli(idCliente);
            int i = 0;
            string estado = "Sin finalizar";

            foreach (Pedido pedido in lstPedio)
            {
                if(pedido.Estado.Equals("Sin finalizar"))
                {
                    i++;
                    break;
                }
            }

            if (i == 0)
            {


                if (web.cambiarEstadoPed(idPedido, estado))
                {
                    System.Web.HttpContext.Current.Session["PedidoCompra"] = idPedido;
                    lblMensajes.Text = "Pedido modificado a sin finalizar";
                    lblPaginaAct.Text = "1";
                    listarPagina();
                    Response.Redirect("/Paginas/PedidosCli/VerCarrito");

                }
            }
            else
            {
                lblMensajes.Text = "Ya existe un pedido sin finalizar"; 
            }


        }




        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);


            System.Web.HttpContext.Current.Session["PedidoCompra"] = idPedido;
            lblMensajes.Text = "Pedido seleccionado";
        }

        #endregion

    }
}