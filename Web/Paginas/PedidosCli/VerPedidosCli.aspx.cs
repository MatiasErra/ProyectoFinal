using Clases;
using System;
using System.Collections;
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
                System.Web.HttpContext.Current.Session["ViajesSelected"] = null;
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
                CargarListBuscar();
   
                CargarListOrdenar();
                CargarEstadoBuscar();
                CargarViajeBuscar();



                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLst"] != null ? System.Web.HttpContext.Current.Session["BuscarLst"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLst"] = null;
                listBuscarVisibilidad();

                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

       

                lstEstados.SelectedValue = System.Web.HttpContext.Current.Session["EstadoPed"] != null ? System.Web.HttpContext.Current.Session["EstadoPed"].ToString() : "";
                System.Web.HttpContext.Current.Session["EstadoPed"] = null;

                lstViaje.SelectedValue = System.Web.HttpContext.Current.Session["EstadoViaje"] != null ? System.Web.HttpContext.Current.Session["EstadoViaje"].ToString() : "";
                System.Web.HttpContext.Current.Session["EstadoViaje"] = null;

                txtCostoMenorBuscar.Text = System.Web.HttpContext.Current.Session["CostoMin"] != null ? System.Web.HttpContext.Current.Session["CostoMin"].ToString() : "";
                System.Web.HttpContext.Current.Session["CostoMin"] = null;

                txtCostoMayorBuscar.Text = System.Web.HttpContext.Current.Session["CostoMax"] != null ? System.Web.HttpContext.Current.Session["CostoMax"].ToString() : "";
                System.Web.HttpContext.Current.Session["CostoMax"] = null;

                txtFchPedidoMenor.Text = System.Web.HttpContext.Current.Session["fchPedidoMenorCliBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchPedidoMenorCliBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchPedidoMenorCliBuscar"] = null;
                txtFchPedidoMayor.Text = System.Web.HttpContext.Current.Session["fchPedidoMayorCliBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchPedidoMayorCliBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchPedidoMayorCliBuscar"] = null;
                txtFchEntregaMenor.Text = System.Web.HttpContext.Current.Session["fchEntregaMenorCliBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchEntregaMenorCliBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchEntregaMenorCliBuscar"] = null;
                txtFchEntregaMayor.Text = System.Web.HttpContext.Current.Session["fchEntregaMayorCliBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchEntregaMayorCliBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchEntregaMayorCliBuscar"] = null;


                System.Web.HttpContext.Current.Session["PedidoCompraSel"] = null;


                listarPagina();

            }






        }
        #region Buscador

  
 


        public void CargarListBuscar()
        {
            listBuscarPor.DataSource = null;
            listBuscarPor.DataSource = createDataSourceBuscar();
            listBuscarPor.DataTextField = "nombre";
            listBuscarPor.DataValueField = "id";
            listBuscarPor.DataBind();
        }

        ICollection createDataSourceBuscar()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Buscar por", "Buscar por", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));
            dt.Rows.Add(createRow("Viaje", "Viaje", dt));
            dt.Rows.Add(createRow("Costo", "Costo", dt));
            dt.Rows.Add(createRow("Fecha pedido", "Fecha pedido", dt));
            dt.Rows.Add(createRow("Fecha entrega", "Fecha entrega", dt));


            DataView dv = new DataView(dt);
            return dv;
        }



        public void CargarEstadoBuscar()
        {
            lstEstados.DataSource = null;
            lstEstados.DataSource = createDataSourceEstado();
            lstEstados.DataTextField = "nombre";
            lstEstados.DataValueField = "id";
            lstEstados.DataBind();
        }

        ICollection createDataSourceEstado()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            dt.Rows.Add(createRow("Seleccionar estado", "Seleccionar estado", dt));
            dt.Rows.Add(createRow("Sin finalizar", "Sin finalizar", dt));
            dt.Rows.Add(createRow("Sin confirmar", "Sin confirmar", dt));
            dt.Rows.Add(createRow("Confirmado", "Confirmado", dt));
            dt.Rows.Add(createRow("En viaje", "En viaje", dt));
            dt.Rows.Add(createRow("Finalizado", "Finalizado", dt));

            DataView dv = new DataView(dt);
            return dv;
        }


        public void CargarViajeBuscar()
        {
            lstViaje.DataSource = null;
            lstViaje.DataSource = createDataSourceViaje();
            lstViaje.DataTextField = "nombre";
            lstViaje.DataValueField = "id";
            lstViaje.DataBind();
        }

        ICollection createDataSourceViaje()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            dt.Rows.Add(createRow("Seleccionar estado del viaje", "Seleccionar estado del viaje", dt));
            dt.Rows.Add(createRow("Sin asignar", "Sin asignar", dt));
            dt.Rows.Add(createRow("Asignado", "Asignado", dt));


            DataView dv = new DataView(dt);
            return dv;
        }



        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        #endregion

        #region ordenar 



        public void CargarListOrdenar()
        {
            listOrdenarPor.DataSource = null;
            listOrdenarPor.DataSource = createDataSourceOrdenar();
            listOrdenarPor.DataTextField = "nombre";
            listOrdenarPor.DataValueField = "id";
            listOrdenarPor.DataBind();
        }

        ICollection createDataSourceOrdenar()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ordenar por", "Ordenar por", dt));
            dt.Rows.Add(createRow("Cliente", "Cliente", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));
            dt.Rows.Add(createRow("Viaje", "Viaje", dt));
            dt.Rows.Add(createRow("Costo", "Costo", dt));
            dt.Rows.Add(createRow("Fecha del pedido", "Fecha del pedido", dt));

            DataView dv = new DataView(dt);
            return dv;
        }





        #endregion


        #region listar


        private void limpiar()
        {
            lblMensajes.Text = "";

            lstEstados.SelectedValue = "Seleccionar estado";
            listOrdenarPor.SelectedValue = "Ordenar por";
            listBuscarPor.SelectedValue = "Buscar por";
            lstViaje.SelectedValue = "Seleccionar estado del viaje";
            txtFchEntregaMenor.Text = "";
            txtFchPedidoMayor.Text = "";
            txtFchEntregaMenor.Text = "";
            txtFchEntregaMayor.Text = "";

            txtCostoMenorBuscar.Text = "";
            txtCostoMayorBuscar.Text = "";
            lblPaginaAct.Text = "1";
            listBuscarVisibilidad();
            listarPagina();

        }




        private int PagMax()
        {

            return 4;
        }



        private void listarPagina()
        {
            List<Pedido> pedidos = obtenerPedidos();
            List<Pedido> pedidosPagina = new List<Pedido>();


            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Pedido unPedido in pedidos)
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

                txtPaginas.Visible = false;
                lstPedido.Visible = false;
                lblPaginaSig.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaAnt.Visible = false;
                lblMensajes.Text = "No se encontró ningún pedido";

            }
            else
            {
                txtPaginas.Visible = true;
                lblMensajes.Text = "";

                modificarPagina();

                lstPedido.Visible = true;
                lstPedido.DataSource = ObtenerDatos(pedidosPagina);
                lstPedido.DataBind();

            }



        }


        private void modificarPagina()
        {
            List<Pedido> pedidos = obtenerPedidos();
            double pxp = PagMax();
            double count = pedidos.Count;
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
            int idCliente = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());

            ControladoraWeb web = ControladoraWeb.obtenerInstancia();

            Cliente unCli = web.buscarCli(idCliente);

            string Estado = lstEstados.SelectedValue != "Seleccionar estado" ? lstEstados.SelectedValue : "";
            string Viaje = lstViaje.SelectedValue != "Seleccionar estado del viaje" ? lstViaje.SelectedValue : "";
            int CostoMin = txtCostoMenorBuscar.Text == "" ? 0 : int.Parse(txtCostoMenorBuscar.Text);
            int CostoMayor = txtCostoMayorBuscar.Text == "" ? 99999999 : int.Parse(txtCostoMayorBuscar.Text);
            string fchPedidoMenor = txtFchPedidoMenor.Text == "" ? "1000-01-01" : txtFchPedidoMenor.Text;
            string fchPedidoMayor = txtFchPedidoMayor.Text == "" ? "3000-12-30" : txtFchPedidoMayor.Text;
            string fchEntregaMenor = txtFchEntregaMenor.Text == "" ? "1000-01-01" : txtFchEntregaMenor.Text;
            string fchEntregaMayor = txtFchEntregaMayor.Text == "" ? "3000-12-30" : txtFchEntregaMayor.Text;
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            

            List<Pedido> lstPedido = web.BuscarPedidoFiltro(unCli.User, Estado, Viaje, CostoMin, CostoMayor, fchPedidoMenor, fchPedidoMayor, fchEntregaMenor, fchEntregaMayor, ordenar);

            return lstPedido;
        }






        public DataTable ObtenerDatos(List<Pedido> pedido)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("idPedido", typeof(int)),
                new DataColumn("idCliente", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Estado", typeof(string)),
                new DataColumn("Viaje", typeof(string)),
                                new DataColumn("Costo", typeof(double)),
                new DataColumn("fchPedido", typeof(string)),
    
                new DataColumn("fchEntre", typeof(string)),

                new DataColumn("InfoEnv", typeof(string)),
            });

            foreach (Pedido unPed in pedido)
            {

                DataRow dr = dt.NewRow();
                dr["idPedido"] = unPed.IdPedido.ToString();
                dr["idCliente"] = unPed.IdCliente.ToString();
                dr["Nombre"] = unPed.NombreCli.ToString();
                dr["Estado"] = unPed.Estado.ToString();
                dr["Viaje"] = unPed.Viaje.ToString();
                dr["Costo"] = unPed.Costo.ToString();

                string[] FechPedido = unPed.FechaPedido.ToString().Split(' ');
                dr["fchPedido"] = FechPedido[0];


                dr["fchEntre"] = unPed.FechaEntre != "" ? unPed.FechaEntre.ToString().Split(' ')[0] : "Sin asignar";



                dr["InfoEnv"] = unPed.InformacionEnvio.ToString();
                dt.Rows.Add(dr);



            }

            return dt;
        }

        #endregion

        private void GuardarDatos()
        {
            System.Web.HttpContext.Current.Session["BuscarLst"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : "";

            System.Web.HttpContext.Current.Session["EstadoPed"] = lstEstados.SelectedValue != "Seleccionar estado" ? lstEstados.SelectedValue : "";
            System.Web.HttpContext.Current.Session["EstadoViaje"] = lstViaje.SelectedValue != "Seleccionar estado del viaje" ? lstViaje.SelectedValue : "";

            System.Web.HttpContext.Current.Session["fchPedidoMenorCliBuscar"] = txtFchPedidoMenor.Text != "" ? txtFchPedidoMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchPedidoMayorCliBuscar"] = txtFchPedidoMayor.Text != "" ? txtFchPedidoMayor.Text : null;
            System.Web.HttpContext.Current.Session["fchEntregaMenorCliBuscar"] = txtFchEntregaMenor.Text != "" ? txtFchEntregaMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchEntregaMayorCliBuscar"] = txtFchEntregaMayor.Text != "" ? txtFchEntregaMayor.Text : null;

            System.Web.HttpContext.Current.Session["CostoMin"] = txtCostoMenorBuscar.Text.ToString();
            System.Web.HttpContext.Current.Session["CostoMax"] = txtCostoMayorBuscar.Text.ToString();



            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;



        }



        #region botones 
        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBuscarVisibilidad();
        }

        private void listBuscarVisibilidad()
        {

            lstEstados.Visible = listBuscarPor.SelectedValue == "Estado" ? true : false;
            lstViaje.Visible = listBuscarPor.SelectedValue == "Viaje" ? true : false;
            lblFchPedido.Visible = listBuscarPor.SelectedValue == "Fecha pedido" ? true : false;
            lblFchEntrega.Visible = listBuscarPor.SelectedValue == "Fecha entrega" ? true : false;
            lblCostoMenorBuscar.Visible = listBuscarPor.SelectedValue == "Costo" ? true : false;
            txtCostoMenorBuscar.Visible = listBuscarPor.SelectedValue == "Costo" ? true : false;
            lblCostoMayorBuscar.Visible = listBuscarPor.SelectedValue == "Costo" ? true : false;
            txtCostoMayorBuscar.Visible = listBuscarPor.SelectedValue == "Costo" ? true : false;



        }


        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            GuardarDatos();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();


            GuardarDatos();

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
            System.Web.HttpContext.Current.Session["EstadoViajeSel"] = selectedrow.Cells[4].Text;

            Response.Redirect("/Paginas/PedidosCli/VerProductosPedido");

        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);
            string estadoPedido = selectedrow.Cells[3].Text;
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();


            int idCliente = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
            List<Pedido> lstPedio = web.listPedidoCli(idCliente);
            int i = 0;
            string estado = "Sin finalizar";

            if (estadoPedido == "Sin confirmar")
            {
                foreach (Pedido pedido in lstPedio)
                {
                    if (pedido.Estado.Equals("Sin finalizar"))
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
            else lblMensajes.Text = "El pedido debe estar sin confirmar.";

            


        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            int num = 0;
            try
            {
                if (int.Parse(txtCostoMenorBuscar.Text) <= int.Parse(txtCostoMayorBuscar.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {
                lblPaginaAct.Text = "1";
                listarPagina();
            }
            else
            {
                lblMensajes.Text = "El costo menor (desde) es mayor que el costo mayor (hasta).";

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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion

    }
}