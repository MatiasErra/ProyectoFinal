using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Productos;

namespace Web.Paginas.PedidosADM
{
    public partial class frmPedido : System.Web.UI.Page
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
                CargarListBuscar();
                CargarUsuarioBuscar();
                CargarListOrdenar();
                CargarEstadoBuscar();
                CargarViajeBuscar();


                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLst"] != null ? System.Web.HttpContext.Current.Session["BuscarLst"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLst"] = null;
                listBuscarVisibilidad();

                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                lstCliente.SelectedValue = System.Web.HttpContext.Current.Session["CliSelected"] != null ? System.Web.HttpContext.Current.Session["CliSelected"].ToString() : "";
                System.Web.HttpContext.Current.Session["CliSelected"] = null;

                lstEstados.SelectedValue = System.Web.HttpContext.Current.Session["EstadoPed"] != null ? System.Web.HttpContext.Current.Session["EstadoPed"].ToString() : "";
                System.Web.HttpContext.Current.Session["EstadoPed"] = null;

                lstViaje.SelectedValue = System.Web.HttpContext.Current.Session["EstadoViaje"] != null ? System.Web.HttpContext.Current.Session["EstadoViaje"].ToString() : "";
                System.Web.HttpContext.Current.Session["EstadoViaje"] = null;

                txtCostoMenorBuscar.Text = System.Web.HttpContext.Current.Session["CostoMin"] != null ? System.Web.HttpContext.Current.Session["CostoMin"].ToString() : "";
                System.Web.HttpContext.Current.Session["CostoMin"] = null;

                txtCostoMayorBuscar.Text = System.Web.HttpContext.Current.Session["CostoMax"] != null ? System.Web.HttpContext.Current.Session["CostoMax"].ToString() : "";
                System.Web.HttpContext.Current.Session["CostoMax"] = null;


                System.Web.HttpContext.Current.Session["PedidoCompraSel"] = null;















                listarPagina();




                lblMensajes.Text = System.Web.HttpContext.Current.Session["pedidoMensaje"] != null ? System.Web.HttpContext.Current.Session["pedidoMensaje"].ToString() : "";
                System.Web.HttpContext.Current.Session["pedidoMensaje"] = null;

            }

        }

        #region Buscador

        public void CargarUsuarioBuscar()
        {
            lstCliente.DataSource = null;
            lstCliente.DataSource = createDataSourceBuscarCli();
            lstCliente.DataTextField = "nombre";
            lstCliente.DataValueField = "id";
            lstCliente.DataBind();
        }

        ICollection createDataSourceBuscarCli()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Cliente cliente = new Cliente();
            cliente.Nombre = "";
            cliente.Apellido = "";
            cliente.Email = "";
            cliente.Telefono = "";
            cliente.User = "";
            cliente.Direccion = "";
            string fchDesde = "1000-01-01";
            string fchHasta = "3000-12-30";
            string ordenar = "";

            List<Cliente> clientes = Web.buscarCliFiltro(cliente, fchDesde, fchHasta, ordenar);



            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccionar cliente", "Seleccionar cliente", dt));

            cargarDueños(clientes, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarDueños(List<Cliente> clientes, DataTable dt)
        {
            foreach (Cliente unCliente in clientes)
            {
                dt.Rows.Add(createRow(unCliente.IdPersona + " " + unCliente.Nombre + " " + unCliente.Apellido + " " + unCliente.User, unCliente.User.ToString(), dt));
            }
        }




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
            dt.Rows.Add(createRow("Cliente", "Cliente", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));
            dt.Rows.Add(createRow("Viaje", "Viaje", dt));
            dt.Rows.Add(createRow("Costo", "Costo", dt));


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
            lstCliente.SelectedValue = "Seleccionar cliente";
            lstEstados.SelectedValue = "Seleccionar estado";
            listOrdenarPor.SelectedValue = "Ordenar por";
            listBuscarPor.SelectedValue = "Buscar por";
            lstViaje.SelectedValue = "Seleccionar estado del viaje";



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

                lblPaginas.Visible = false;
                lstPedido.Visible = false;
                lblMensajes.Text = "No se encontró un pedido con esas características";

            }
            else
            {
                lblPaginas.Visible = true;
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

            ControladoraWeb web = ControladoraWeb.obtenerInstancia();

            string NomCli = lstCliente.SelectedValue != "Seleccionar cliente" ? lstCliente.SelectedValue : "";
            string Estado = lstEstados.SelectedValue != "Seleccionar estado" ? lstEstados.SelectedValue : "";
            string Viaje = lstViaje.SelectedValue != "Seleccionar estado del viaje" ? lstViaje.SelectedValue : "";
            int CostoMin = txtCostoMenorBuscar.Text == "" ? 0 : int.Parse(txtCostoMenorBuscar.Text);
            int CostoMayor = txtCostoMayorBuscar.Text == "" ? 99999999 : int.Parse(txtCostoMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";


            List<Pedido> lstPedido = web.BuscarPedidoFiltro(NomCli, Estado, Viaje, CostoMin, CostoMayor, ordenar);

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

              

                dr["fchEntre"] = unPed.FechaEntre != "" ? unPed.FechaEntre.ToString() : "Sin asignar";



                dr["InfoEnv"] = unPed.InformacionEnvio.ToString();
                dt.Rows.Add(dr);



            }

            return dt;
        }

        #endregion

        private void GuardarDatos()
        {
            System.Web.HttpContext.Current.Session["BuscarLst"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : "";
            System.Web.HttpContext.Current.Session["CliSelected"] = lstCliente.SelectedValue != "Seleccionar cliente" ? lstCliente.SelectedValue : "";
            System.Web.HttpContext.Current.Session["EstadoPed"] = lstEstados.SelectedValue != "Seleccionar estado" ? lstEstados.SelectedValue : "";
            System.Web.HttpContext.Current.Session["EstadoViaje"] = lstViaje.SelectedValue != "Seleccionar estado del viaje" ? lstViaje.SelectedValue : "";
            

            System.Web.HttpContext.Current.Session["CostoMin"] = txtCostoMenorBuscar.Text.ToString();
            System.Web.HttpContext.Current.Session["CostoMax"] = txtCostoMayorBuscar.Text.ToString();
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;



        }

        private bool CantActualProd(int idPedido, string Estado)
        {
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();
            List<string[]> lstPedi = new List<string[]>();
            Producto producto = new Producto();
            producto.Nombre = "";
            producto.Tipo = "";
            producto.TipoVenta = "";
            int precioMenor = 0;
            int precioMayor = 999999;
            string ordenar = "";
            List<Producto> productos = web.buscarProductoFiltro(producto, precioMenor, precioMayor, ordenar);
            string cantRess = "";


            if (Estado.ToString() == "Sin confirmar" || Estado.ToString() == "Sin finalizar")
            {
                List<string[]> lstPediLot = web.buscarPedidoLote(idPedido);
                if (lstPediLot.Count == 0)
                {
                    lstPedi = web.buscarPedidoProd(idPedido);



                    foreach (Producto unProd in productos)
                    {
                        int cant = 0;
                        int idProducto = unProd.IdProducto;

                        foreach (string[] unPedi in lstPedi)
                        {

                            if (unProd.IdProducto.ToString().Equals(unPedi[1].ToString())
                                  && unPedi[0].ToString().Equals(idPedido.ToString())
                                  )
                            {
                                string[] cantStr = unPedi[6].ToString().Split(' ');
                                cant += int.Parse(cantStr[0].ToString());
                            }

                        }
                        string[] cantRessProd = unProd.CantRes.ToString().Split(' ');
                        int resultado = int.Parse(cantRessProd[0].ToString()) - cant;

                        cantRess = resultado.ToString() + " " + unProd.TipoVenta.ToString();


                        web.bajaPedidoProd(idPedido, idProducto, cantRess, 0);






                    }
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else
            {
                string cantDisp = "";
                string cantLote = "";
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote lote = new Lote();
                lote.IdGranja = 0;
                lote.IdProducto = 0;
                lote.IdDeposito = 0;
                double precioMenorLot = 0;
                double precioMayorLot = 99999999;

                string fchProduccionMenor = "1000-01-01";
                string fchProduccionMayor = "3000-12-30";
                string fchCaducidadMenor = "1000-01-01";
                string fchCaducidadMayor = "3000-12-30";


                List<Lote> lotes = Web.buscarFiltrarLotes(lote, precioMenorLot, precioMayorLot, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);

                lstPedi = web.buscarPedidoLote(idPedido);
                foreach (Producto unProd in productos)
                {
                    int cantTotal = 0;
                    cantRess = unProd.CantRes.ToString();
                    foreach (Lote unLote in lotes)
                    {
                        int idGranja = unLote.IdGranja;
                        string FchProduccion = unLote.FchProduccion;

                        if (unProd.IdProducto == unLote.IdProducto)
                        {


                            int cant = 0;
                            int idProducto = unProd.IdProducto;

                            cantRess = unProd.CantRes.ToString();


                            foreach (string[] unPedi in lstPedi)
                            {

                                if (unProd.IdProducto.ToString().Equals(unPedi[1].ToString())
                                      && unPedi[0].ToString().Equals(idPedido.ToString())
                                      && unPedi[3].ToString().Equals(idGranja.ToString())
                                        && unPedi[5].ToString().Equals(FchProduccion.ToString())
                                      )
                                {
                                    string[] cantStr = unPedi[9].ToString().Split(' ');
                                    cantTotal += int.Parse(cantStr[0].ToString());
                                    cant = int.Parse(cantStr[0].ToString());
                                }

                            }

                            string[] cantTotaLot = unProd.CantTotal.ToString().Split(' ');
                            int resultadoDisp = int.Parse(cantTotaLot[0].ToString()) + cantTotal;

                            cantDisp = resultadoDisp.ToString() + " " + unProd.TipoVenta.ToString();

                            string[] cantLotearr = unLote.Cantidad.ToString().Split(' ');
                            int resultadoLote = int.Parse(cantLotearr[0].ToString()) + cant;

                            cantLote = resultadoLote.ToString() + " " + unProd.TipoVenta.ToString();

                            web.bajaPedidoProd(idPedido, idProducto, cantRess, 0);

                            web.bajaLotesPedido(idPedido, idGranja, idProducto, FchProduccion, cantLote, cantDisp, cantRess);

                        }




                    }



                }
                return true;
            }
        

        }




        #region Botones
        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBuscarVisibilidad();
        }

        private void listBuscarVisibilidad()
        {
            lstCliente.Visible = listBuscarPor.SelectedValue == "Cliente" ? true : false;
            lstEstados.Visible = listBuscarPor.SelectedValue == "Estado" ? true : false;
            lstViaje.Visible = listBuscarPor.SelectedValue == "Viaje" ? true : false;
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


        //protected void btnAsignarPaqu_Click(object sender, EventArgs e)
        //{
        //    Button btnConstultar = (Button)sender;
        //    GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
        //    int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
        //    System.Web.HttpContext.Current.Session["PedidoCompraSel"] = id;
        //    string estadoVia = HttpUtility.HtmlEncode(selectedrow.Cells[6].Text);

        //    if (estadoVia == "Pendiente")
        //    {
        //        Response.Redirect("/Paginas/Viajes/verLoteDelViaje");
        //    }
        //    else
        //    {
        //        lblMensajes.Text = "El pedido tiene que estar en Pendiente para poder asignarle un Lote";
        //    }
        //}


 

        protected void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);

            string estado = selectedrow.Cells[3].Text;

            if (estado == "Sin confirmar")
            {
                System.Web.HttpContext.Current.Session["PedidoCompraSel"] = idPedido;
                Response.Redirect("/Paginas/PedidosAdm/confPedido");
            }
            else
            {
                lblMensajes.Text = "El pedido debe estar sin confirmar para poder confirmarlo";
            }




        }

    

        protected void btnVerPedido_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);
            string EstadoViaje = selectedrow.Cells[4].Text;
            System.Web.HttpContext.Current.Session["PedidoCompraSel"] = idPedido;
            System.Web.HttpContext.Current.Session["EstadoViajeSel"] = EstadoViaje;
            GuardarDatos();

            Response.Redirect("/Paginas/PedidosCli/VerProductosPedido");

        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idPedido;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            idPedido = int.Parse(selectedrow.Cells[0].Text);
            string Estado = selectedrow.Cells[3].Text;
            ControladoraWeb web = ControladoraWeb.obtenerInstancia();



            if (CantActualProd(idPedido, Estado))
            {
                if (web.bajaPedido(idPedido))
                {

                    listarPagina();
                    lblMensajes.Text = "Pedido Eliminado";
                }
                else
                {
                    lblMensajes.Text = "No se pudo dar de baja el pedido";
                }
            }
            else
            {
                lblMensajes.Text = "No se pudo dar de baja el pedido";
            }


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


        protected void btnBuscarDueñoBuscar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["frmPedido"] = "Si";
            GuardarDatos();
            Response.Redirect("/Paginas/Clientes/frmListarClientes");
        }


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion


    }

}
