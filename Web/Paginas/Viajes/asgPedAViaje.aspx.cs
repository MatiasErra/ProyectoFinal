using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Camioneros;
using Web.Paginas.PedidosAdm;
using Web.Paginas.Productos;

namespace Web.Paginas.Viajes
{
    public partial class asgPedAViaje : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
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
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }

            if (System.Web.HttpContext.Current.Session["ViajesSelected"] == null)
            {
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }




        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["PagActLotPed"] == null)
                {
                    lblPaginaActLotPed.Text = "1";
                }
                else
                {
                    lblPaginaActLotPed.Text = System.Web.HttpContext.Current.Session["PagActLotPed"].ToString();
                    System.Web.HttpContext.Current.Session["PagActLotPed"] = null;
                }

                if (System.Web.HttpContext.Current.Session["PagActLotPedVia"] == null)
                {
                    lblPaginaActViaPed.Text = "1";
                }
                else
                {
                    lblPaginaActViaPed.Text = System.Web.HttpContext.Current.Session["PagActLotPedVia"].ToString();
                    System.Web.HttpContext.Current.Session["PagActLotPedVia"] = null;
                }


                CargarListPedidos();

                lstPedidosCon.SelectedValue = System.Web.HttpContext.Current.Session["PedidoSelect"] != null ? System.Web.HttpContext.Current.Session["PedidoSelect"].ToString() : "Seleccione un pedido";
                System.Web.HttpContext.Current.Session["PedidoSelect"] = null;

                listBuscarPedLote();
            }
        }

        #region Pedidos

        public void CargarListPedidos()
        {
            lstPedidosCon.DataSource = null;
            lstPedidosCon.DataSource = createDataSourcePedido();
            lstPedidosCon.DataTextField = "nombre";
            lstPedidosCon.DataValueField = "id";
            lstPedidosCon.DataBind();
        }


        ICollection createDataSourcePedido()
        {


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            List<Pedido> pedidos = obtenerPedidos();
            if (pedidos.Count > 0)
            {
                dt.Rows.Add(createRow("Seleccione un pedido", "Seleccione un pedido", dt));
                cargarPedidos(pedidos, dt);

            }
            else
            {
                dt.Rows.Add(createRow(" ", " ", dt));
                lblMensajes.Text = "No se encontraron pedidos para asignar";
            }
            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarPedidos(List<Pedido> pedidos, DataTable dt)
        {
            foreach (Pedido unPedido in pedidos)
            {
                string[] unPedfch = unPedido.FechaPedido.ToString().Split(' ');
                dt.Rows.Add(createRow(unPedido.IdPedido + " " + unPedfch[0].ToString() + " " + unPedido.NombreCli + " " + unPedido.InformacionEnvio, unPedido.IdPedido.ToString(), dt));
            }
        }


        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        private List<Pedido> obtenerPedidos()
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Pedido> lstPedidoRes = new List<Pedido>();

            List<Pedido> lstPedido = Web.BuscarPedidoFiltro("", "Confirmado", "", 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "Fecha del pedido");


            int idViaje = 0;

            foreach (Pedido unPedido in lstPedido)
            {
                int i = 0;
                List<string[]> LotPed = Web.buscarPedidoLote(unPedido.IdPedido);

                List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(unPedido.IdPedido, idViaje);

                foreach (string[] unLotPed in LotPed)
                {
                    foreach (Viaje_Lot_Ped unViaje_Lot_P in viajeLotPed)
                    {
                        string a = unLotPed[1].ToString();
                        string b = unLotPed[3].ToString();
                        string c = unLotPed[5].ToString();

                        Viaje unViaje = Web.buscarViaje(unViaje_Lot_P.IdViaje);

                        if (unViaje_Lot_P.IdProducto.ToString().Equals(unLotPed[1].ToString())  
                            && unViaje_Lot_P.IdGranja.ToString().Equals(unLotPed[3].ToString())
                            && unViaje_Lot_P.FchProduccion.ToString().Equals(unLotPed[5].ToString())
                            && !unViaje.Estado.ToString().Equals("Pendiente")
                            )
                        {
                            i++;
                        }
                    }
                }
                if(LotPed.Count > i)
                {
                    lstPedidoRes.Add(unPedido);
                }

            }


            return lstPedidoRes;
        }

        private void GuardarDatos()
        {

            System.Web.HttpContext.Current.Session["PedidoSelect"] = lstPedidosCon.SelectedValue != "Seleccione un pedido" ? lstPedidosCon.SelectedValue : null;


        }

        #endregion

        #region Pedidos Lotes Asg


        private void listBuscarPedLote()
        {
            if (obtenerPedidos().Count> 0)
            {
                if (lstPedidosCon.SelectedValue != "Seleccione un pedido")
                {

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    int idViaje = int.Parse(System.Web.HttpContext.Current.Session["ViajesSelected"].ToString());
                    string str = lstPedidosCon.SelectedValue.ToString();
                    int idPedido = int.Parse(lstPedidosCon.SelectedValue.ToString());

                    lstPedidoLote.Visible = true;
                    lblH5LotePed.Visible = true;

             Viaje unViaje = Web.buscarViaje(idViaje);

                    if (unViaje.Estado =="Pendiente") 
                    {
                        btnConfirmarViaje.Visible = true;
                    }
                    else
                    {
                        btnConfirmarViaje.Visible = false;
                    }

                    listarPedidosLote(idPedido);
                    listarViajePedidosLote(idPedido, idViaje);


                }
                else
                {


                    lblH5LotePed.Visible = false;
                    lstPedidoLote.Visible = false;
                    lblPaginaAntLotPed.Visible = false;
                    lblPaginaActLotPed.Visible = false;
                    lblPaginaSigLotPed.Visible = false;
                    txtPaginaLotPed.Visible = false;

                    h5ConfViaje.Visible = false;
                    lstViajePed.Visible = false;
                    lblPaginaAntViaPed.Visible = false;
                    lblPaginaActViaPed.Visible = false;
                    lblPaginaSigViaPed.Visible = false;
                    txtPaginaViaPed.Visible = false;

                    btnConfirmarViaje.Visible = false;


                }

            }
            else
            {


                lblH5LotePed.Visible = false;
                lstPedidoLote.Visible = false;
                lblPaginaAntLotPed.Visible = false;
                lblPaginaActLotPed.Visible = false;
                lblPaginaSigLotPed.Visible = false;
                txtPaginaLotPed.Visible = false;

                h5ConfViaje.Visible = false;
                lstViajePed.Visible = false;
                lblPaginaAntViaPed.Visible = false;
                lblPaginaActViaPed.Visible = false;
                lblPaginaSigViaPed.Visible = false;
                txtPaginaViaPed.Visible = false;

                btnConfirmarViaje.Visible = false;
                lstPedidosCon.Visible = false;

            }

        }

        private int PagMaxLotePed()
        {

            return 4;
        }

        private List<string[]> ObtenerPedidoLote(int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<string[]> Lote_PedidosRes = new List<string[]>();
            List<string[]> Lote_Pedidos = Web.buscarPedidoLote(idPedido);

            foreach (string[] unLote_pedido in Lote_Pedidos)
            {
                string[] cant = unLote_pedido[9].ToString().Split(' ');
                int cantInt = int.Parse(cant[0].ToString());

                Producto unProd = Web.buscarProducto(int.Parse(unLote_pedido[1].ToString()));
                string StrcantViaje = unLote_pedido[12].ToString() != "0" ? unLote_pedido[12].ToString() : "0" + " " + unProd.TipoVenta.ToString();
                string[] ArrycantViaje = StrcantViaje.ToString().Split(' ');
                int cantViaje = int.Parse(ArrycantViaje[0].ToString());


                if (cantInt > cantViaje)
                {
                    string[] Lote_Pedido = new string[6];

                    Lote_Pedido[0] = unLote_pedido[0].ToString();
                    Lote_Pedido[1] = unLote_pedido[4].ToString();
                    Lote_Pedido[2] = unLote_pedido[2].ToString();
                    Lote_Pedido[3] = unLote_pedido[5].ToString();
                    Lote_Pedido[4] = unLote_pedido[9].ToString();


                    Lote_Pedido[5] = unLote_pedido[12].ToString();

                    Lote_PedidosRes.Add(Lote_Pedido);
                }
            }


            return Lote_PedidosRes;

        }

        private void listarPedidosLote(int idPedido)
        {

            List<string[]> productos = ObtenerPedidoLote(idPedido);
            List<string[]> productosPagina = new List<string[]>();
            string p = lblPaginaActLotPed.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

            foreach (string[] unProducto in productos)
            {
                if (productosPagina.Count == PagMaxLotePed())
                {
                    break;
                }
                if (cont >= ((pagina * PagMaxLotePed()) - PagMaxLotePed()))
                {
                    productosPagina.Add(unProducto);
                }

                cont++;
            }

            if (productosPagina.Count == 0)
            {
                lblH5LotePed.Visible = false;

                txtPaginaLotPed.Visible = false;
                lstPedidoLote.Visible = false;
                lblPaginaActLotPed.Visible = false;


            }
            else
            {
                lblH5LotePed.Visible = true;



                txtPaginaLotPed.Visible = true;
                modificarPaginaLotePed(idPedido);
                lstPedidoLote.Visible = true;
                lstPedidoLote.Visible = true;
                lstPedidoLote.DataSource = null;
                lstPedidoLote.DataSource = ObtenerLotePedidoDatos(productosPagina);
                lstPedidoLote.DataBind();

            }


        }

        private void modificarPaginaLotePed(int idPedido)
        {
            List<string[]> productos = ObtenerPedidoLote(idPedido);
            double pxp = PagMaxLotePed();
            double count = productos.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaActLotPed.Text.ToString();

            lblPaginaActLotPed.Visible = true;
            lblPaginaSigLotPed.Visible = true;
            lblPaginaAntLotPed.Visible = true;




            if (pagAct == cantPags.ToString())
            {
                lblPaginaSigLotPed.Visible = false;

            }
            if (pagAct == "1")
            {

                lblPaginaAntLotPed.Visible = false;

            }

            if (pagAct == cantPags.ToString() && pagAct == "1")
            {

                txtPaginaLotPed.Visible = false;
                lblPaginaActLotPed.Visible = false;

            }
            lblPaginaAntLotPed.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaActLotPed.Text = pagAct.ToString();
            lblPaginaSigLotPed.Text = (int.Parse(pagAct) + 1).ToString();
        }

        public DataTable ObtenerLotePedidoDatos(List<string[]> pedidos_lote)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {
                 new DataColumn("idPedido", typeof(string)),
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("CantidadViaje", typeof(string))
                       });
            foreach (string[] unLote_pedido in pedidos_lote)
            {

                DataRow dr = dt.NewRow();
                dr["idPedido"] = unLote_pedido[0].ToString();
                dr["NombreGranja"] = unLote_pedido[1].ToString();
                dr["NombreProducto"] = unLote_pedido[2].ToString();
                dr["FchProduccion"] = unLote_pedido[3].ToString();
                dr["Cantidad"] = unLote_pedido[4].ToString();
                string C = unLote_pedido[5].ToString();
                dr["CantidadViaje"] = unLote_pedido[5].ToString();
                dt.Rows.Add(dr);

            }
            return dt;


        }


        #endregion

        #region viajes 
        private void listarViajePedidosLote(int idPedido, int idViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            List<Viaje_Lot_Ped> viajeLotPedPag = new List<Viaje_Lot_Ped>();
            string p = lblPaginaActViaPed.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;

            foreach (Viaje_Lot_Ped unViajeLotPed in viajeLotPed)
            {
                if (viajeLotPedPag.Count == PagMaxLotePed())
                {
                    break;
                }
                if (cont >= ((pagina * PagMaxLotePed()) - PagMaxLotePed()))
                {
                    viajeLotPedPag.Add(unViajeLotPed);
                }

                cont++;
            }

            if (viajeLotPedPag.Count == 0)
            {

                h5ConfViaje.Visible = false;
                txtPaginaViaPed.Visible = false;
                lstViajePed.Visible = false;
                lblPaginaActViaPed.Visible = false;
                lblPaginaAntViaPed.Visible = false;
                lblPaginaSigViaPed.Visible = false;
           


            }
            else
            {
                h5ConfViaje.Visible = true;
            

                txtPaginaViaPed.Visible = true;
                modPaginaViajeLotePed(idPedido, idViaje);
                lstViajePed.Visible = true;
                lstViajePed.DataSource = null;
                lstViajePed.DataSource = ObtenerViajeLotePedidoDatos(viajeLotPedPag);
                lstViajePed.DataBind();

            }


        }

        private void modPaginaViajeLotePed(int idPedido, int idViaje)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            double pxp = PagMaxLotePed();
            double count = viajeLotPed.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaActViaPed.Text.ToString();

            lblPaginaActViaPed.Visible = true;
            lblPaginaSigViaPed.Visible = true;
            lblPaginaAntViaPed.Visible = true;







            if (pagAct == cantPags.ToString())
            {
                lblPaginaSigViaPed.Visible = false;

            }
            if (pagAct == "1")
            {

                lblPaginaAntViaPed.Visible = false;

            }

            if (pagAct == cantPags.ToString() && pagAct == "1")
            {

                txtPaginaViaPed.Visible = false;
                lblPaginaActViaPed.Visible = false;

            }
            lblPaginaAntViaPed.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaActViaPed.Text = pagAct.ToString();
            lblPaginaSigViaPed.Text = (int.Parse(pagAct) + 1).ToString();
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

        #region botones

        protected void lstPedidosCon_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblPaginaActLotPed.Text = "1";

            GuardarDatos();

            listBuscarPedLote();

        }

        protected void btnConfirmarViaje_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int idViaje = int.Parse(System.Web.HttpContext.Current.Session["ViajesSelected"].ToString());
            int idPedido = int.Parse(lstPedidosCon.SelectedValue.ToString());
            Viaje viaje = Web.buscarViaje(idViaje);
            string fchViaje = viaje.Fecha.ToString();
            
            DateTime fechaConvertida = DateTime.ParseExact(fchViaje, "d/M/yyyy", null);
    
            string fchViajeuwu = fechaConvertida.ToString("yyyy-MM-dd");
            string estado = "Confirmado";

            Viaje unViaje = new Viaje(viaje.IdViaje, viaje.Costo, fchViajeuwu, viaje.IdCamion, viaje.IdCamionero, estado);
            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);

            int fallo = 0;
            if (viajeLotPed.Count > 0)
            {
                int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                if (Web.modViaje(unViaje, idAdmin))
                {
                    List<Viaje_Lot_Ped> lstViajePed = Web.buscarViajePedLote(0, idViaje );
                    foreach (Viaje_Lot_Ped unPeiajePed in lstViajePed)
                    {

                        if (Web.modPedViajeEst(unPeiajePed.IdPedido, estado, idAdmin))
                        {
                        
                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo confirmar el viaje";
                            fallo++;
                            break;
                        }
                    }
                    if (fallo == 0)
                    {
                        System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje confirmado";
                        Response.Redirect("/Paginas/Viajes/frmViajes");
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo confirmar el viaje";
                    }
                }
                else
                {
                    lblMensajes.Text = "No se pudo confirmar el viaje";
                }
            }
            else
            {
                lblMensajes.Text = "No puede confirmar un viaje sin asignar lotes";
            }
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
                listBuscarPedLote();
                lblMensajes.Text = "Lote eliminado del viaje";
            }
            else
            {
                lblMensajes.Text = "No se pudo elLote eliminado del viaje";
            }

        }

        protected void btnAsignarAlViaje_Click(object sender, EventArgs e)
        {
            int idViaje = int.Parse(System.Web.HttpContext.Current.Session["ViajesSelected"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            GridViewRow row = (GridViewRow)btnConstultar.NamingContainer;
            TextBox txtCant = (TextBox)row.FindControl("txtCantidad");

            int idPedido = int.Parse(selectedrow.Cells[0].Text);
            string nombreGranja = selectedrow.Cells[1].Text;
            string nombreProducto = selectedrow.Cells[2].Text;
            string fchProduccion = selectedrow.Cells[3].Text;
            string[] CantTotal = selectedrow.Cells[4].Text.Split(' ');
            int intCantTotal = int.Parse(CantTotal[0].ToString());
            string[] CantViaje = selectedrow.Cells[5].Text.Split(' ');
            int intCantViaje = int.Parse(CantViaje[0].ToString());

            int CantViajeAct = 0;
            int cantAdd = int.Parse(txtCant.Text.ToString());

            Viaje_Lot_Ped viaje_Lot_Ped = new Viaje_Lot_Ped();
            Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            Producto unProd = Web.buscarProducto(unLote.IdProducto);




            viaje_Lot_Ped.IdViaje = idViaje;
            viaje_Lot_Ped.IdPedido = idPedido;
            viaje_Lot_Ped.IdGranja = unLote.IdGranja;
            viaje_Lot_Ped.IdProducto = unLote.IdProducto;
            viaje_Lot_Ped.FchProduccion = fchProduccion;



            viaje_Lot_Ped.Cant = cantAdd.ToString() + " " + unProd.TipoVenta;

            int resultado = intCantViaje + cantAdd;

            if (intCantTotal >= cantAdd)
            {
                if (intCantTotal >= resultado)
                {
                    CantViajeAct = CantViajeAct + cantAdd;
                    string stCantViajeAct = CantViajeAct.ToString() + " " + unProd.TipoVenta;
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.altaViajePedido_Lote(viaje_Lot_Ped, stCantViajeAct, idAdmin))
                    {
                        listBuscarPedLote();
                        lblMensajes.Text = "Se ingreso el pedido lote al viaje";
             

                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo ingresar el pedido lote al viaje";
                    }

                }
                else
                { lblMensajes.Text = "No se puede ingresar una cantidad mayor total al viaje de la que se tiene registrado en el pedido lote"; }

            }
            else
            {
                lblMensajes.Text = "No se puede ingresar una cantidad mayor al viaje de la que se tiene registrado en el pedido lote";
            }


        }
        protected void lblPaginaAntLotPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLotPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPed"] = (pagina - 1).ToString();
            GuardarDatos();
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSigLotPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLotPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPed"] = (pagina + 1).ToString();
            GuardarDatos();
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void txtPaginaAntViaPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActViaPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPedVia"] = (pagina - 1).ToString();
            GuardarDatos();
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void txtPaginaSegViaPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActViaPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPedVia"] = (pagina + 1).ToString();
            GuardarDatos();
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        #endregion

    }
}