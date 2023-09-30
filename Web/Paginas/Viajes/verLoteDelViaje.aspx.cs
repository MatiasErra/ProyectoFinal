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
                if(System.Web.HttpContext.Current.Session["PedidoCompraSel"] != null)
                {
                    int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
                    listarViajes(idPedido);
                }
                






            }
        }

        #region Listar Lotes

        private int PagMax()
        {

            return 4;
        }

        private List<Viaje_Lot_Ped> ObtenerLotesViaje(int idViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int idPedido = 0;

            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            return viajeLotPed;
        }


        private void listarLotes(int idViaje)
        {


            List<Viaje_Lot_Ped> viajeLotPed = ObtenerLotesViaje(idViaje);
            List<Viaje_Lot_Ped> viajeLotPedPag = new List<Viaje_Lot_Ped>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

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

        private void modificarPagina(int idViaje)
        {
            List<Viaje_Lot_Ped> viajeLotPed = ObtenerLotesViaje(idViaje);
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

        private List<Viaje> ObtenerViajes(int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Viaje viaje = new Viaje();
            int costoMenor = 0 ;
            int costoMayor =  99999999;
            string fchMenor = "1000-01-01";
            string fchMayor = "3000-12-30";
            viaje.IdCamion =  0;
            viaje.IdCamionero =  0;
            viaje.Estado =  "";
            string ordenar = "";

            List<Viaje> viajes = Web.buscarViajeFiltro(viaje, costoMenor, costoMayor, fchMenor, fchMayor, ordenar);

            List<Viaje> lstViajeRes = new List<Viaje> ();
            int idViaje = 0;

            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            int i = 0;

            foreach (Viaje unViaje in viajes)
            {
                i = 0;
                foreach (Viaje_Lot_Ped viaje_Lot_Ped in viajeLotPed)
                {
                    if (viaje_Lot_Ped.IdViaje.ToString().Equals(unViaje.IdViaje.ToString()))
                        {
                        i++;
                    }
                }

                if(i >0)
                {
                    lstViajeRes.Add(unViaje);
                }
            }

            return lstViajeRes;
        }


        private void listarViajes(int idPedido)
        {


            List<Viaje> viajes = ObtenerViajes(idPedido);
            List<Viaje> viajesPagina = new List<Viaje>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

            foreach (Viaje unLote in viajes)
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
            List<Viaje> viajeLotPed = ObtenerViajes(idPedido);
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


        public DataTable ObtenerViajeDatos(List<Viaje> lstViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] {

                 new DataColumn("idViaje", typeof(string)),
                new DataColumn("fchViaje", typeof(string)),
                new DataColumn("Estado", typeof(string))

                       });
            foreach (Viaje unViaje in lstViaje)
            {


                DataRow dr = dt.NewRow();
                dr["idViaje"] = unViaje.IdViaje.ToString();
                dr["fchViaje"] = unViaje.Fecha.ToString();
                dr["Estado"] = unViaje.Estado.ToString();
         


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




        #endregion
    }
}