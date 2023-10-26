using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Viajes
{
    public partial class verLoteDelPedio : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null)
            {
                this.MasterPageFile = "~/Master/MCliente.Master";

                if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] == null)
                {
                    Response.Redirect("/Paginas/PedidosCli/VerPedidosCli");
                }


            }
            else
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

                    else if (admin.TipoDeAdmin == "Administrador de pedidos")
                    {
                        this.MasterPageFile = "~/Master/APedidos.Master";
                    }
                    else
                    {
                        Response.Redirect("/Paginas/Nav/frmInicio");
                    }


                    if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] == null
                        &&
                        System.Web.HttpContext.Current.Session["ViajesSelected"] == null
                        )
                    {
                        Response.Redirect("/Paginas/PedidosAdm/frmPedido");
                    }

                }
                else
                {

                    Response.Redirect("/Paginas/Nav/frmInicio");
                }

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


                 if (System.Web.HttpContext.Current.Session["ViajesSelected"] != null)
                {
                    int idViaje = int.Parse(System.Web.HttpContext.Current.Session["ViajesSelected"].ToString());
                    listarLotes(idViaje);
                }
                if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] != null)
                {
                    int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
                    listarViajesPed(idPedido);
                }







            }
        }

        #region Listar Lotes

        private int PagMax()
        {

            return 8;
        }

        private List<Viaje_Lot_Ped> ObtenerLotesViaje(int idPedido, int idViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            

            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            return viajeLotPed;
        }


        private void listarLotes(int idViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int idPedido = 0;
            List<Viaje_Lot_Ped> viajeLotPed = ObtenerLotesViaje(idPedido,idViaje);
            List<Viaje_Lot_Ped> viajeLotPedPag = new List<Viaje_Lot_Ped>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            Viaje unViaje = Web.buscarViaje(idViaje);
            foreach (Viaje_Lot_Ped unLote in viajeLotPed)
            {
                if (viajeLotPedPag.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    viajeLotPedPag.Add(unLote);
                }

                cont++;
            }

            if (viajeLotPedPag.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Lote en este Viaje.";

                lblPaginaAct.Visible = false;
                txtPaginas.Text = "";
                lstViajePed.Visible = false;
                lstViajePedElim.Visible = false;
            }
            else
            {
                if (unViaje.Estado == "Confirmado" || unViaje.Estado == "Finalizado")
                {
                    lblMensajes.Text = "";
                    modificarPagina(idViaje);
                    txtPaginas.Text = "Paginas";
                    lstViajePedElim.Visible = false;
                    lstViajePedElim.Visible = true;
                    lstViajePedElim.DataSource = null;
                    lstViajePedElim.DataSource = ObtenerViajeLotePedidoDatos(viajeLotPedPag);
                    lstViajePedElim.DataBind();
                }
                else
                {
                    lblMensajes.Text = "";
                    modificarPagina(idViaje);
                    txtPaginas.Text = "Paginas";
                    lstViajePed.Visible = false;
                    lstViajePed.Visible = true;
                    lstViajePed.DataSource = null;
                    lstViajePed.DataSource = ObtenerViajeLotePedidoDatos(viajeLotPedPag);
                    lstViajePed.DataBind();
                }


            }


        }

        private void modificarPagina(int idViaje)
        {
            int idPedido = 0;
            List<Viaje_Lot_Ped> viajeLotPed = ObtenerLotesViaje(idPedido, idViaje);
            double pxp = PagMax();
            double count = viajeLotPed.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();
            lblPaginaAct.Visible = true;
            lblPaginaSig.Visible = true;
            lblPaginaAnt.Visible = true;
            txtPaginas.Visible = true;
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


        public DataTable ObtenerViajeLotePedidoDatos(List<Viaje_Lot_Ped> Viapedidos_lote)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {
                      new DataColumn("idPedido", typeof(string)),
                 new DataColumn("idViaje", typeof(string)),
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("CantidadAsg", typeof(string)),

                       });
            foreach (Viaje_Lot_Ped unViajeLote_pedido in Viapedidos_lote)
            {


                DataRow dr = dt.NewRow();
                dr["idPedido"] = unViajeLote_pedido.IdPedido.ToString();
                dr["idViaje"] = unViajeLote_pedido.IdViaje.ToString();
                dr["NombreGranja"] = unViajeLote_pedido.NombreGranja.ToString();
                dr["NombreProducto"] = unViajeLote_pedido.NombreProducto.ToString();


                string[] fch = unViajeLote_pedido.FchProduccion.ToString().Split(' ');
                dr["FchProduccion"] = fch[0].ToString();
                dr["CantidadAsg"] = unViajeLote_pedido.Cant.ToString();


                dt.Rows.Add(dr);

            }
            return dt;


        }

        #endregion

        #region Listar Viajes

 

        private void listarViajesPed(int idPedido)
        {
            int idViaje = 0;

            List<Viaje_Lot_Ped> viajes = ObtenerLotesViaje(idPedido, idViaje);
            List<Viaje_Lot_Ped> viajesPagina = new List<Viaje_Lot_Ped>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

            foreach (Viaje_Lot_Ped unLote in viajes)
            {
                if (viajesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    viajesPagina.Add(unLote);
                }

                cont++;
            }

            if (viajesPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Viaje en este pedido.";

                lblPaginaAct.Visible = false;
                txtPaginas.Text = "";
                lstViaje.Visible = false;

            }
            else
            {

                lblMensajes.Text = "";
                modificarPaginaViaje(idPedido);
                txtPaginas.Text = "Paginas";
                lstViaje.Visible = false;
                lstViaje.Visible = true;
                lstViaje.DataSource = null;
                lstViaje.DataSource = ObtenerViajeDatos(viajesPagina);
                lstViaje.DataBind();


            }


        }

        private void modificarPaginaViaje(int idPedido)
        {
            int idViaje = 0;
            List<Viaje_Lot_Ped> viajeLotPed = ObtenerLotesViaje(idPedido, idViaje);
            double pxp = PagMax();
            double count = viajeLotPed.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();
            lblPaginaAct.Visible = true;
            lblPaginaSig.Visible = true;
            lblPaginaAnt.Visible = true;
            txtPaginas.Visible = true;
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


        public DataTable ObtenerViajeDatos(List<Viaje_Lot_Ped> lstViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {

                 new DataColumn("idViaje", typeof(string)),
                new DataColumn("fchViaje", typeof(string)),
                new DataColumn("Estado", typeof(string)),
              new DataColumn("NombreProducto", typeof(string)),
                 new DataColumn("CantidadAsg", typeof(string)),
    
                       });
          
            foreach (Viaje_Lot_Ped unLotViaje in lstViaje)
            {
                Viaje unViaje = Web.buscarViaje(unLotViaje.IdViaje);

                DataRow dr = dt.NewRow();
                dr["idViaje"] = unViaje.IdViaje.ToString();
                dr["fchViaje"] = unViaje.Fecha.ToString();
                dr["Estado"] = unViaje.Estado.ToString();
                dr["NombreProducto"] = unLotViaje.NombreProducto.ToString();
                dr["CantidadAsg"] = unLotViaje.Cant.ToString();


                dt.Rows.Add(dr);

            }
            return dt;


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

        protected void btnBorrarViaPedLot_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            GridViewRow row = (GridViewRow)btnConstultar.NamingContainer;




            int idPedido = int.Parse(selectedrow.Cells[0].Text);
            int idViaje = int.Parse(selectedrow.Cells[1].Text);
            string nombreGranja = selectedrow.Cells[2].Text;
            string nombreProducto = selectedrow.Cells[3].Text;
            string fchProduccion = selectedrow.Cells[4].Text;
            string[] CantAsg = selectedrow.Cells[5].Text.Split(' ');
            int intCantAsg = int.Parse(CantAsg[0].ToString());

            int CantAct = 0;

            Viaje_Lot_Ped viaje_Lot_Ped = new Viaje_Lot_Ped();
            Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);

            viaje_Lot_Ped.IdViaje = idViaje;
            viaje_Lot_Ped.IdPedido = idPedido;
            viaje_Lot_Ped.IdGranja = unLote.IdGranja;
            viaje_Lot_Ped.IdProducto = unLote.IdProducto;
            viaje_Lot_Ped.FchProduccion = fchProduccion;

            List<string[]> lstLotePedio = Web.buscarPedidoLote(idPedido);
            Producto unProd = Web.buscarProducto(unLote.IdProducto);
            foreach (string[] unLotePedido in lstLotePedio)
            {
                if (unLotePedido[0].ToString().Equals(idPedido.ToString())
                    && unLotePedido[1].ToString().Equals(unLote.IdProducto.ToString())
                    && unLotePedido[3].ToString().Equals(unLote.IdGranja.ToString())
                    && unLotePedido[5].ToString().Equals(unLote.FchProduccion.ToString())
                    )
                {
                    string[] CantActArr = unLotePedido[12].ToString().Split(' ');
                    CantAct = int.Parse(CantActArr[0].ToString());

                }
            }
            int CantTotal = CantAct - intCantAsg;

            string strCantTotal = CantTotal.ToString() + " " + unProd.TipoVenta.ToString();

            int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
            if (Web.bajaViajePedido_Lote(viaje_Lot_Ped, strCantTotal, idAdmin))
            {
                listarLotes(idViaje);
                lblMensajes.Text = "Lote eliminado del viaje";
            }
            else
            {
                lblMensajes.Text = "No se pudo elLote eliminado del viaje";
            }

        }



        #endregion
    }
}