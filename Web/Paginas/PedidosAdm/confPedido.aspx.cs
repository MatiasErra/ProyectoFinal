using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.PedidosAdm;
using Web.Paginas.Productos;

namespace Web.Paginas.PedidosAdm
{
    public partial class confPedido : System.Web.UI.Page
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

            if (System.Web.HttpContext.Current.Session["PedidoCompraSel"] == null)
            {
                Response.Redirect("/Paginas/PedidosAdm/frmPedido");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());



                if (System.Web.HttpContext.Current.Session["PagActLot"] == null)
                {
                    lblPaginaActLot.Text = "1";
                }
                else
                {
                    lblPaginaActLot.Text = System.Web.HttpContext.Current.Session["PagActLot"].ToString();
                    System.Web.HttpContext.Current.Session["PagActLot"] = null;
                }



                if (System.Web.HttpContext.Current.Session["PagActLotPed"] == null)
                {
                    lblPaginaActLotPed.Text = "1";
                }
                else
                {
                    lblPaginaActLotPed.Text = System.Web.HttpContext.Current.Session["PagActLotPed"].ToString();
                    System.Web.HttpContext.Current.Session["PagActLotPed"] = null;
                }



                CargarListProducto(idPedido);

                lstPedidosProd.SelectedValue = System.Web.HttpContext.Current.Session["lstPedidosProd"] != null ? System.Web.HttpContext.Current.Session["lstPedidosProd"].ToString() : "Seleccione un pedido del cliente";

                System.Web.HttpContext.Current.Session["lstPedidosProd"] = null;


                listBuscarLote();









            }
        }

        #region Pedido de Cliente

        public void CargarListProducto(int idPedido)
        {
            lstPedidosProd.DataSource = null;
            lstPedidosProd.DataSource = createDataSourceProducto(idPedido);
            lstPedidosProd.DataTextField = "nombre";
            lstPedidosProd.DataValueField = "id";
            lstPedidosProd.DataBind();
        }

        ICollection createDataSourceProducto(int idPedido)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            List<string[]> productos = Web.buscarPedidoProd(idPedido);
            dt.Rows.Add(createRow("Seleccione un pedido del cliente", "Seleccione un pedido del cliente", dt));

            cargarProductos(productos, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarProductos(List<string[]> productos, DataTable dt)
        {
            foreach (string[] unProducto in productos)
            {
                dt.Rows.Add(createRow(unProducto[2].ToString() + " " + unProducto[6].ToString(), unProducto[1].ToString(), dt));
            }
        }


        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        #endregion


        private void listBuscarLote()
        {

            if (lstPedidosProd.SelectedValue != "Seleccione un pedido del cliente")
            {


                int idProducto = int.Parse(lstPedidosProd.SelectedValue.ToString());

                lstPedidoLote.Visible = true;


                lstLote.Visible = true;
                lblCantAdm.Visible = true;
                txtCantAdm.Visible = true;

                txtCantCli.Visible = true;

                lblCantCli.Visible = true;

                listarLotes();
                txtCantCli.Text = cargarCant();
                txtCantAdm.Text = CalCantAdm();
                listarPedidosLote(idProducto);



            }
            else
            {
                lblCantAdm.Visible = false;
                txtCantAdm.Visible = false;


                txtCantCli.Visible = false;
                lblCantCli.Visible = false;
                btnConfPed.Visible = false;

                h5ConfPedido.Visible = false;
                lblH5Lote.Visible = false;
                lstPedidoLote.Visible = false;
                lblPaginaAntLotPed.Visible = false;
                lblPaginaActLotPed.Visible = false;
                lblPaginaSigLotPed.Visible = false;
                txtPaginaLotPed.Visible = false;


                lblPaginaAntLot.Visible = false;
                lblPaginaActLot.Visible = false;
                lblPaginaSigLot.Visible = false;
                txtPaginasLot.Visible = false;
                lstLote.Visible = false;

            }

        }

        private string cargarCant()
        {
            int idProducto = int.Parse(System.Web.HttpContext.Current.Session["ProductoSelecId"].ToString());
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string cantPedido = "";

            List<string[]> productos = Web.buscarPedidoProd(idPedido);
            foreach (string[] prod in productos)
            {
                if (int.Parse(prod[1].ToString()) == idProducto
                    && int.Parse(prod[0].ToString()) == idPedido)
                {
                    cantPedido = prod[6].ToString();
                }
            }
            return cantPedido;
        }

        private string CalCantAdm()
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<string[]> pedidoCantAdm = Web.buscarPedidoLote(idPedido);

            int intResultado = 0;
            int idProducto = int.Parse(lstPedidosProd.SelectedValue.ToString());

            foreach (string[] pedidoAdm in pedidoCantAdm)
            {

                if (pedidoAdm[1].ToString().Equals(idProducto.ToString()))
                {
                    string[] pedidoArry = pedidoAdm[9].ToString().Split(' ');
                    int CantAdmint = int.Parse(pedidoArry[0].ToString());
                    intResultado += CantAdmint;
                }


            }

            Producto producto = Web.buscarProducto(idProducto);
            string resultado = intResultado.ToString() + " " + producto.TipoVenta.ToString();

            return resultado;

        }


        #region Listar lotes


        private int PagMax()
        {
            return 1;
        }


        private List<Lote> obtenerLote()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int idProducto = int.Parse(System.Web.HttpContext.Current.Session["ProductoSelecId"].ToString());
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());

            List<Lote> resultado = new List<Lote>();
            Lote lote = new Lote();
            lote.IdGranja = 0;
            lote.IdProducto = idProducto;

            lote.IdDeposito = 0;
            double precioMenor = 0;
            double precioMayor = 99999999;

            string fchProduccionMenor = "1000-01-01";
            string fchProduccionMayor = "3000-12-30";
            string fchCaducidadMenor = "1000-01-01";
            string fchCaducidadMayor = "3000-12-30";
            string ordenar = "Fecha de caducidad";

            List<Lote> lotes = Web.buscarFiltrarLotes(lote, precioMenor, precioMayor, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);
            List<string[]> Lote_Pedidos = Web.buscarPedidoLote(idPedido);

            int i = 0;

            foreach (Lote unLote in lotes)
            {
                int unique = 0;
                if (idProducto.ToString().Equals(unLote.IdProducto.ToString()))
                {


                    foreach (string[] unLote_pedido in Lote_Pedidos)
                    {
                        string exgran = unLote_pedido[3].ToString();
                        string exfch = unLote_pedido[5].ToString();
                        string ex = unLote_pedido[1].ToString();
                        string exPedido = unLote_pedido[0].ToString();

                        if (unLote.IdProducto.ToString().Equals(unLote_pedido[1].ToString()) &&
                            unLote.IdGranja.ToString().Equals(unLote_pedido[3].ToString()) &&
                              unLote.FchProduccion.ToString().Equals(unLote_pedido[5].ToString()))


                        {
                            unique++;
                        }



                    }
                    if (unique == 0)
                    {
                        resultado.Add(unLote);
                    }
                }
            }

            return resultado;
        }


        private void listarLotes()
        {
            List<Lote> lotes = obtenerLote();
            List<Lote> LotesPagina = new List<Lote>();

            string p = lblPaginaActLot.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Lote unLote in lotes)
            {
                if (LotesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    LotesPagina.Add(unLote);
                }

                cont++;
            }

            if (LotesPagina.Count == 0)
            {

                lblPaginaAntLot.Visible = false;
                lblPaginaActLot.Visible = false;
                lblPaginaSigLot.Visible = false;
                txtPaginasLot.Visible = false;
             
                txtPaginasLot.Text = "";
                lblH5Lote.Visible = false;




                lstLote.Visible = false;
            }
            else
            {
                lblH5Lote.Visible = true;
                txtPaginasLot.Text = "Paginas";
           
                modificarPaginaLote();
                lstLote.Visible = true;
                lstLote.DataSource = null;
                lstLote.DataSource = ObtenerDatos(LotesPagina);
                txtPaginasLot.Visible = true;
                lstLote.DataBind();
            }


        }

        private void modificarPaginaLote()
        {
            List<Lote> lotes = obtenerLote();
            double pxp = PagMax();
            double count = lotes.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaActLot.Text.ToString();
            txtPaginasLot.Text = "Paginas";
            lblPaginaSigLot.Visible = true;
            lblPaginaAntLot.Visible = true;
            lblPaginaActLot.Visible = true;
            if (pagAct == cantPags.ToString())
            {
                lblPaginaSigLot.Visible = false;
            }
            if (pagAct == "1")
            {
                lblPaginaAntLot.Visible = false;

            }

            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginasLot.Text = "";
                lblPaginaActLot.Visible = false;

            }


            lblPaginaAntLot.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaActLot.Text = pagAct.ToString();
            lblPaginaSigLot.Text = (int.Parse(pagAct) + 1).ToString();
        }



        public DataTable ObtenerDatos(List<Lote> lotes)
        {
            DataTable dt = new DataTable();



            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("FchCaducidad", typeof(string)),
                new DataColumn("Cantidad", typeof(string))

           });

            foreach (Lote unLote in lotes)
            {
                DataRow dr = dt.NewRow();
                dr["NombreGranja"] = unLote.NombreGranja.ToString();
                dr["NombreProducto"] = unLote.NombreProducto.ToString();
                dr["FchProduccion"] = unLote.FchProduccion.ToString();
                dr["FchCaducidad"] = unLote.FchCaducidad.ToString();
                dr["Cantidad"] = unLote.Cantidad.ToString();




                dt.Rows.Add(dr);
            }
            return dt;
        }


        #endregion

        private void GuardarDatos()
        {
            System.Web.HttpContext.Current.Session["lstPedidosProd"] = lstPedidosProd.SelectedValue != "Seleccione un pedido del cliente" ? lstPedidosProd.SelectedValue : null;
        }

        #region Pedido Lote

        private int PagMaxLotePed()
        {

            return 3;
        }

        private List<string[]> ObtenerPedidoLote(int idPedido, int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<string[]> Lote_PedidosRes = new List<string[]>();
            List<string[]> Lote_Pedidos = Web.buscarPedidoLote(idPedido);

            foreach (string[] unLote_pedido in Lote_Pedidos)
            {
                if (unLote_pedido[1].ToString().Equals(idProducto.ToString()))
                {
                    string[] Lote_Pedido = new string[6];
                    Lote_Pedido[0] = unLote_pedido[4].ToString();
                    Lote_Pedido[1] = unLote_pedido[2].ToString();
                    Lote_Pedido[2] = unLote_pedido[5].ToString();
                    Lote_Pedido[3] = unLote_pedido[9].ToString();
                    Lote_Pedido[4] = unLote_pedido[3].ToString();
                    Lote_Pedido[5] = unLote_pedido[5].ToString();

                    Lote_PedidosRes.Add(Lote_Pedido);
                }
            }
            return Lote_PedidosRes;

        }

        private void listarPedidosLote(int idProducto)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            List<string[]> productos = ObtenerPedidoLote(idPedido, idProducto);
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
                h5ConfPedido.Visible = false;
       
                txtPaginaLotPed.Visible = false;
                lstPedidoLote.Visible = false;
                lblPaginaActLotPed.Visible = false;
                btnConfPed.Visible = false;

            }
            else
            {
                h5ConfPedido.Visible = true;
                btnConfPed.Visible = true;
                btnConfPed.Visible = true;


                modificarPaginaLotePed(idPedido, idProducto);
                lstPedidoLote.Visible = true;
                lstPedidoLote.Visible = true;
                lstPedidoLote.DataSource = null;
                lstPedidoLote.DataSource = ObtenerLotePedidoDatos(productosPagina);
                lstPedidoLote.DataBind();

            }


        }

        private void modificarPaginaLotePed(int idPedido, int idProducto)
        {
            List<string[]> productos = ObtenerPedidoLote(idPedido, idProducto);
            double pxp = PagMaxLotePed();
            double count = productos.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaActLotPed.Text.ToString();

            txtPaginaLotPed.Visible = true;
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
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("Cantidad", typeof(string))
                       });
            foreach (string[] unLote_pedido in pedidos_lote)
            {

                DataRow dr = dt.NewRow();
                dr["NombreGranja"] = unLote_pedido[0].ToString();
                dr["NombreProducto"] = unLote_pedido[1].ToString();
                dr["FchProduccion"] = unLote_pedido[2].ToString();
                dr["Cantidad"] = unLote_pedido[3].ToString();
                dt.Rows.Add(dr);

            }
            return dt;


        }

        #endregion

        private bool CompCantIng(int idPedido)
        {
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            List<string[]> lstPediCli = web.buscarPedidoProd(idPedido);
            List<string[]> lstPediAdm = web.buscarPedidoLote(idPedido);
            int i = 0;
            foreach (string[] unPedi in lstPediCli)
            {
                int cantCli = 0;
                int cantAdm = 0;
                string[] cantStr = unPedi[6].ToString().Split(' ');
                cantCli += int.Parse(cantStr[0].ToString());
                foreach (string[] unPediAdm in lstPediAdm)
                {
                    if (unPediAdm[1].ToString().Equals(unPedi[1].ToString()))
                    {
                        string[] cantStrAdm = unPediAdm[9].ToString().Split(' ');
                        cantAdm += int.Parse(cantStrAdm[0].ToString());
                    }

                }
                if (cantAdm == cantCli)
                {
                    i++;
                }



            }
            if (i == lstPediCli.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #region Botones

        #region lotes
        protected void lblPaginaAntLot_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLot.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLot"] = (pagina - 1).ToString();
            GuardarDatos();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSigLot_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLot.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLot"] = (pagina + 1).ToString();
            GuardarDatos();



            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
        #endregion


        #region Pedido lotes
        protected void lblPaginaAntLotPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLotPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPed"] = (pagina - 1).ToString();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSigLotPed_Click(object sender, EventArgs e)
        {
            string p = lblPaginaActLotPed.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagActLotPed"] = (pagina + 1).ToString();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
        #endregion



        protected void lstPedidosProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaActLot.Text = "1";
            lblPaginaActLotPed.Text = "1";
            GuardarDatos();
            System.Web.HttpContext.Current.Session["ProductoSelecId"] = lstPedidosProd.SelectedValue != "Seleccione un pedido del cliente" ? lstPedidosProd.SelectedValue : null;

            listBuscarLote();

        }

        protected void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia(); 

            if (CompCantIng(idPedido))
            {
                string estado = "Confirmado";
                Web.cambiarEstadoPed(idPedido, estado);
                
                lblMensajes.Text = "Pedido confirmado";

                System.Web.HttpContext.Current.Session["pedidoConf"] = "si";
                Response.Redirect("/Paginas/PedidosAdm/frmPedido");
              
            }
            else
            {
                lblMensajes.Text = "Falta añadir más cantidad de producto de los lotes en el pedido para igualar la cantidad solicitada por el usuario";
            }

        }
        protected void btnEliminarLotePed_Click(object sender, EventArgs e)
        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            int idProducto = int.Parse(System.Web.HttpContext.Current.Session["ProductoSelecId"].ToString());
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = selectedrow.Cells[0].Text;
            string nombreProducto = selectedrow.Cells[1].Text;
            string fchProduccion = selectedrow.Cells[2].Text;
            string[] cantIngr = selectedrow.Cells[3].Text.Split(' ');
            int cantIntIngr = int.Parse(cantIngr[0].ToString());

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);

            string[] unLoteCant = unLote.Cantidad.Split(' ');

            int resultado = int.Parse(unLoteCant[0].ToString()) + cantIntIngr;

            string cantLote = resultado.ToString() + " " + unLoteCant[1].ToString();




            Producto prod = Web.buscarProducto(idProducto);
            string[] cantDisp = prod.CantTotal.ToString().Split(' ');
            int intCantDisp = int.Parse(cantDisp[0].ToString());
            int intCantTotal = intCantDisp + cantIntIngr;


            string CantDisp = intCantTotal.ToString() + " " + cantDisp[1].ToString();

            string[] cantRess = prod.CantRes.ToString().Split(' ');
            int intCantRess = int.Parse(cantRess[0].ToString());
            int intCantRessTotal = intCantRess + cantIntIngr;

            string CantRess = intCantRessTotal.ToString() + " " + cantRess[1].ToString();






            if (Web.bajaLotesPedido(idPedido, unLote.IdGranja, unLote.IdProducto, fchProduccion, cantLote, CantDisp, CantRess))
            {
                lblPaginaActLot.Text = "1";
                lblPaginaActLotPed.Text = "1";
                listBuscarLote();
                lblMensajes.Text = "Lote dado de baja de pedido";

            }
            else
            {
                lblMensajes.Text = "No se pudo dar de baja el lote de pedido";
            }
        }

   

        protected void btnModificarCantidad_Click(object sender, EventArgs e)

        {
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            int idProducto = int.Parse(System.Web.HttpContext.Current.Session["ProductoSelecId"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            TextBox txtCant = (TextBox)row.FindControl("txtCantidad");

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = selectedrow.Cells[0].Text;
            string nombreProducto = selectedrow.Cells[1].Text;
            string fchProduccion = selectedrow.Cells[2].Text;


            string CantidadIng = selectedrow.Cells[3].Text.ToString();
            string[] CantArrayIng = CantidadIng.Split(' ');
            int CantidadintIng = int.Parse(CantArrayIng[0]);


            Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            string[] CantidadLote = unLote.Cantidad.ToString().Split(' ');
            int CantidadLoteint = int.Parse(CantidadLote[0].ToString());

            int CantSinPedLote = CantidadintIng + CantidadLoteint;

            string cantPedido = txtCantCli.Text;
            string[] cantPedidoarry = cantPedido.Split(' ');
            int intCantPed = int.Parse(cantPedidoarry[0].ToString());

            if (!txtCant.Text.Equals(""))
            {
                int txtCantint = int.Parse(txtCant.Text);

                string[] txtCantAdmArry = txtCantAdm.Text.Split(' ');
                int intCantAdm = int.Parse(txtCantAdmArry[0].ToString());
                int intCantAdmAct = intCantAdm - CantidadintIng;
                int resultado = intCantAdmAct + txtCantint;

                if (CantSinPedLote >= txtCantint)
                {
                    if (intCantPed >= resultado)
                    {

                        Lote_Pedido lote_Pedido = new Lote_Pedido();
                        lote_Pedido.IdGranja = unLote.IdGranja;
                        lote_Pedido.IdProducto = unLote.IdProducto;
                        lote_Pedido.FchProduccion = unLote.FchProduccion;
                        lote_Pedido.IdPedido = idPedido;
                        lote_Pedido.Cant = txtCantint.ToString() + " " + txtCantAdmArry[1].ToString();

                        Producto prod = Web.buscarProducto(idProducto);
                        string[] cantDisp = prod.CantTotal.ToString().Split(' ');
                        int intCantDisp = int.Parse(cantDisp[0].ToString());
                        int intCantDispTotal = intCantDisp + CantidadintIng;
                        int intCantTotal = intCantDispTotal - txtCantint;

                        string CantDisp = intCantTotal.ToString() + " " + cantDisp[1].ToString();

                        string[] cantRess = prod.CantRes.ToString().Split(' ');
                        int intCantRess = int.Parse(cantRess[0].ToString());
                        int intCantRessTotal = intCantRess + CantidadintIng;
                        int intCantRessAct = intCantRessTotal - txtCantint;
                        string CantRess = intCantRessAct.ToString() + " " + cantRess[1].ToString();

                        string[] unloteCantarry = unLote.Cantidad.Split(' ');
                        int unLoteCantint = int.Parse(unloteCantarry[0].ToString());
                        int unLoteCantintAct = unLoteCantint + CantidadintIng;


                        int CantTotal = unLoteCantintAct - txtCantint;
                        string CantLote = CantTotal.ToString() + " " + unloteCantarry[1].ToString();
                        if (Web.modCantPedidoLote(lote_Pedido, CantLote, CantDisp, CantRess))
                        {
                            listBuscarLote();
                            lblMensajes.Text = "Cantidad modificada del pedido con exito";





                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo modificar la cantidad pedido en el lote";
                        }

                    }
                    else
                    {
                        lblMensajes.Text = "No se puede ingresar una cantidad mayor a la que el cliente solicito";
                    }
                }
                else
                {
                    lblMensajes.Text = "No se puede ingresar una cantidad mayor a la cantidad de lote en el sistema";
                }
            }
        }

        protected void btnAgregarCantidad_Click(object sender, EventArgs e)
        {
            int idProducto = int.Parse(System.Web.HttpContext.Current.Session["ProductoSelecId"].ToString());
            int idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompraSel"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            TextBox txtCant = (TextBox)row.FindControl("txtCantidad");

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = selectedrow.Cells[0].Text;
            string nombreProducto = selectedrow.Cells[1].Text;
            string fchProduccion = selectedrow.Cells[2].Text;

            string Cantidad = selectedrow.Cells[4].Text.ToString();
            string[] CantArray = Cantidad.Split(' ');
            int Cantidadint = int.Parse(CantArray[0]);

            string cantPedido = txtCantCli.Text;


            string[] cantPedidoarry = cantPedido.Split(' ');
            int intCantPed = int.Parse(cantPedidoarry[0].ToString());
            if (!txtCant.Text.Equals(""))
            {
                int txtCantint = int.Parse(txtCant.Text);
                string[] txtCantAdmArry = txtCantAdm.Text.Split(' ');
                int intCantAdm = int.Parse(txtCantAdmArry[0].ToString());
                int resultado = intCantAdm + txtCantint;

                if (Cantidadint >= txtCantint)
                {
                    if (intCantPed >= resultado)
                    {
                        Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
                        Lote_Pedido lote_Pedido = new Lote_Pedido();
                        lote_Pedido.IdGranja = unLote.IdGranja;
                        lote_Pedido.IdProducto = unLote.IdProducto;
                        lote_Pedido.FchProduccion = unLote.FchProduccion;
                        lote_Pedido.IdPedido = idPedido;
                        lote_Pedido.Cant = txtCantint.ToString() + " " + CantArray[1].ToString();

                        Producto prod = Web.buscarProducto(idProducto);
                        string[] cantDisp = prod.CantTotal.ToString().Split(' ');
                        int intCantDisp = int.Parse(cantDisp[0].ToString());

                        int intCantTotal = intCantDisp - txtCantint;

                        string CantDisp = intCantTotal.ToString() + " " + cantDisp[1].ToString();

                        string[] cantRess = prod.CantRes.ToString().Split(' ');
                        int intCantRess = int.Parse(cantRess[0].ToString());

                        int intCantRessAct = intCantRess - txtCantint;
                        string CantRess = intCantRessAct.ToString() + " " + cantRess[1].ToString();

                        string[] unloteCantarry = unLote.Cantidad.Split(' ');
                        int unLoteCantint = int.Parse(unloteCantarry[0].ToString());



                        int CantTotal = unLoteCantint - txtCantint;
                        string CantLote = CantTotal.ToString() + " " + unloteCantarry[1].ToString();




                        if (Web.altaPedido_Lote(lote_Pedido, CantLote, CantDisp, CantRess))
                        {
                            listBuscarLote();
                            lblMensajes.Text = "Lote ingresado a pedido con exito";





                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo ingresar el lote a pedido";
                        }

                    }
                    else
                    {
                        lblMensajes.Text = "No se puede ingresar una cantidad mayor a la que el cliente solicito";
                    }
                }
                else
                {
                    lblMensajes.Text = "No se puede ingresar una cantidad mayor a la cantidad de lote en el sistema";
                }
            }
        }

    }




    #endregion
}
