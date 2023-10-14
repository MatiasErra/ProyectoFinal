using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Viajes
{
    public partial class modViaje : System.Web.UI.Page
    {

        #region Load
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

            if (System.Web.HttpContext.Current.Session["idViajeSelMod"] == null)
            {
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idViajeSelMod"];
                txtId.Text = id.ToString();

                CargarListCamionero(id);
                CargarListCamiones(id);
                CargarListEstado(id);


                cargarViaje(id);
                if (System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                {
                    cargarDatos();
                }
                else
                {
                    cargarViaje(id);
                }

            }
        }

        #endregion


        #region Utilidad



        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = null;
        }

        private void cargarViaje(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Viaje viaje = Web.buscarViaje(id);
            txtId.Text = viaje.IdViaje.ToString();
            txtCosto.Text = viaje.Costo.ToString();
            txtFch.Text = DateTime.Parse(viaje.Fecha).ToString("yyyy-MM-dd");
            listCamion.SelectedValue = viaje.IdCamion.ToString();
            listCamionero.SelectedValue = viaje.IdCamionero.ToString();
            listEstado.SelectedValue = viaje.Estado;
            if (viaje.Estado != "Pendiente" && viaje.Estado != "Confirmado")
            {
                txtFch.Enabled = false;
            }
        }



        private bool faltanDatos()
        {
            if (txtCosto.Text == "" || txtFch.Text == "" || listCamion.SelectedValue == "Seleccione un Camion" || listCamionero.SelectedValue == "Seleccione un Camionero" || listEstado.SelectedValue == "Seleccione un Estado")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtCosto.Text = "";
            txtFch.Text = "";
            listCamion.SelectedValue = "Seleccione un Camion";
            listCamionero.SelectedValue = "Seleccione un Camionero";
            listEstado.SelectedValue = "Seleccione un Estado";
        }

        #region Guardar y cargar datos
        private void guardarDatos()
        {
            System.Web.HttpContext.Current.Session["Costo"] = txtCosto.Text;
            System.Web.HttpContext.Current.Session["Fecha"] = txtFch.Text != "" ? txtFch.Text : null;
            System.Web.HttpContext.Current.Session["Camion"] = listCamion.SelectedValue != "Seleccione un Camion" ? listCamion.SelectedValue : null;
            System.Web.HttpContext.Current.Session["Camionero"] = listCamionero.SelectedValue != "Seleccione un Camionero" ? listCamionero.SelectedValue : null;
            System.Web.HttpContext.Current.Session["Estado"] = listEstado.SelectedValue != "Seleccione un Estado" ? listEstado.SelectedValue : null;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["ViajeDatosMod"] = null;

            txtCosto.Text = System.Web.HttpContext.Current.Session["Costo"] != null ? System.Web.HttpContext.Current.Session["Costo"].ToString() : "";
            System.Web.HttpContext.Current.Session["Costo"] = null;
            txtFch.Text = System.Web.HttpContext.Current.Session["Fecha"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["Fecha"].ToString()).ToString("yyyy-MM-dd") : "";
            System.Web.HttpContext.Current.Session["Fecha"] = null;
            listCamion.SelectedValue = System.Web.HttpContext.Current.Session["Camion"] != null ? System.Web.HttpContext.Current.Session["Camion"].ToString() : "Seleccione un Camion";
            System.Web.HttpContext.Current.Session["Camion"] = null;
            listCamionero.SelectedValue = System.Web.HttpContext.Current.Session["Camionero"] != null ? System.Web.HttpContext.Current.Session["Camionero"].ToString() : "Seleccione un Camionero";
            System.Web.HttpContext.Current.Session["Camionero"] = null;
            listEstado.SelectedValue = System.Web.HttpContext.Current.Session["Estado"] != null ? System.Web.HttpContext.Current.Session["Estado"].ToString() : "Seleccione un Estado";
            System.Web.HttpContext.Current.Session["Estado"] = null;
        }


        #endregion

        #region DropDownBoxes

        #region Camion

        public void CargarListCamiones(int id)
        {
            listCamion.DataSource = null;
            listCamion.DataSource = createDataSourceCamion(id);
            listCamion.DataTextField = "nombre";
            listCamion.DataValueField = "id";
            listCamion.DataBind();
        }

        ICollection createDataSourceCamion(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion cam = new Camion(0, "", "", 0, "");
            List<Camion> camiones = Web.buscarFiltroCam(cam, 0, 99999, "");
            List<Camion> cargar = new List<Camion>();

            Viaje viaje = Web.buscarViaje(id);

            cargar.Add(Web.buscarCam(viaje.IdCamion));


            if (viaje.Estado == "Pendiente" || viaje.Estado == "Confirmado")
            {
                foreach (Camion unCam in camiones)
                {
                    if (unCam.Disponible == "Disponible")
                    {
                        cargar.Add(unCam);
                    }
                }
            }
            else
            {
                cargar = camiones;
                btnBuscarCamion.Visible = false;
                lblCamion.CssClass = "col-12";
                listCamion.Enabled = false;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Camion", "Seleccione un Camion", dt));

            cargarCamiones(cargar, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarCamiones(List<Camion> camiones, DataTable dt)
        {
            foreach (Camion unCamion in camiones)
            {
                dt.Rows.Add(createRow(unCamion.Marca + " " + unCamion.Modelo + " " + unCamion.Carga + " Kg", unCamion.IdCamion.ToString(), dt));
            }
        }

        #endregion

        #region Camiones

        public void CargarListCamionero(int id)
        {
            listCamionero.DataSource = null;
            listCamionero.DataSource = createDataSourceCamionero(id);
            listCamionero.DataTextField = "nombre";
            listCamionero.DataValueField = "id";
            listCamionero.DataBind();
        }

        ICollection createDataSourceCamionero(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero cam = new Camionero(0, "", "", "", "", "", "", "", "");
            List<Camionero> camioneros = Web.buscarCamioneroFiltro(cam, "1000-01-01", "3000-12-30", "");
            List<Camionero> cargar = new List<Camionero>();

            Viaje viaje = Web.buscarViaje(id);

            cargar.Add(Web.buscarCamionero(viaje.IdCamionero));

            if (viaje.Estado == "Pendiente" || viaje.Estado == "Confirmado")
            {
                foreach (Camionero unCam in camioneros)
                {
                    if (unCam.Disponible == "Disponible")
                    {
                        cargar.Add(unCam);
                    }
                }
            }
            else
            {
                cargar = camioneros;
                btnBuscarCamionero.Visible = false;
                lblCamionero.CssClass = "col-12";
                listCamionero.Enabled = false;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Camionero", "Seleccione un Camionero", dt));

            cargarCamioneros(cargar, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarCamioneros(List<Camionero> camioneros, DataTable dt)
        {
            foreach (Camionero unCamionero in camioneros)
            {
                dt.Rows.Add(createRow(unCamionero.Nombre + " " + unCamionero.Apellido, unCamionero.IdPersona.ToString(), dt));
            }
        }

        #endregion

        #region Estado

        public void CargarListEstado(int id)
        {
            listEstado.DataSource = null;
            listEstado.DataSource = createDataSourceEstado(id);
            listEstado.DataTextField = "nombre";
            listEstado.DataValueField = "id";
            listEstado.DataBind();
        }

        ICollection createDataSourceEstado(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            Viaje viaje = Web.buscarViaje(id);

            dt.Rows.Add(createRow("Seleccione un Estado", "Seleccione un Estado", dt));
            if (viaje.Estado == "Pendiente")
            {
                listEstado.Enabled = false;
                dt.Rows.Add(createRow("Pendiente", "Pendiente", dt));
            }
            else if (viaje.Estado == "Confirmado")
            {
                dt.Rows.Add(createRow("Confirmado", "Confirmado", dt));
                dt.Rows.Add(createRow("En viaje", "En viaje", dt));
            }
            else if (viaje.Estado == "En viaje")
            {
                dt.Rows.Add(createRow("Confirmado", "Confirmado", dt));
                dt.Rows.Add(createRow("En viaje", "En viaje", dt));
                dt.Rows.Add(createRow("Finalizado", "Finalizado", dt));
            }
            else
            {
                listEstado.Enabled = false;
                dt.Rows.Add(createRow("Pendiente", "Pendiente", dt));
                dt.Rows.Add(createRow("En viaje", "En viaje", dt));
                dt.Rows.Add(createRow("Finalizado", "Finalizado", dt));
            }



            DataView dv = new DataView(dt);
            return dv;

        }


        #endregion


        DataRow createRow(String Text, String Value, DataTable dt)
        {


            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }

        #endregion

        #endregion


        protected void btnBuscarCamion_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosMod"] = "Si";
            guardarDatos();
            Response.Redirect("/Paginas/Camiones/frmCamiones");
        }

        protected void btnBuscarCamionero_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosMod"] = "Si";
            guardarDatos();
            Response.Redirect("/Paginas/Camioneros/frmCamioneros");
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Viajes/frmViajes");
        }



        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!faltanDatos())
                {
                    if (int.Parse(txtCosto.Text) != 0)
                    {
                        if (Convert.ToDateTime(txtFch.Text) >= DateTime.Today)
                        {
                            if (!txtId.Text.Equals(""))
                            {
                                int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                                int costo = int.Parse(HttpUtility.HtmlEncode(txtCosto.Text));
                                string fecha = HttpUtility.HtmlEncode(txtFch.Text);
                                int idCamion = int.Parse(HttpUtility.HtmlEncode(listCamion.SelectedValue));
                                int idCamionero = int.Parse(HttpUtility.HtmlEncode(listCamionero.SelectedValue));
                                string estado = HttpUtility.HtmlEncode(listEstado.SelectedValue);

                                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

                                Viaje viaje = Web.buscarViaje(id);
                                int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];


                                int cont = 0;
                                if (estado == "En viaje")
                                {
                                    if (Convert.ToDateTime(fecha) == DateTime.Today) cont++;
                                }
                                else cont++;

                                if (cont == 1)
                                {
                                    if ((viaje.Estado == "Confirmado" && estado == "En viaje") || (viaje.Estado == "En viaje" && estado == "Confirmado"))
                                    {
                                        Viaje via = new Viaje(0, 0, "", 0, 0, "");
                                        List<Viaje> viajes = Web.buscarViajeFiltro(via, 0, 99999999, "1000-01-01", "3000-12-30", "");

                                        int camionNum = 0;
                                        int camioneroNum = 0;
                                        foreach (Viaje viajeCam in viajes)
                                        {
                                            if (viajeCam.IdViaje != id && viajeCam.Estado == "En viaje")
                                            {
                                                if (viajeCam.IdCamion == idCamion)
                                                {
                                                    camionNum++;
                                                }
                                                if (viajeCam.IdCamionero == idCamionero)
                                                {
                                                    camioneroNum++;
                                                }
                                            }
                                        }

                                        if (camionNum > 0)
                                        {
                                            lblMensajes.Text = "El camión de este viaje esta en un viaje ahora.";
                                        }
                                        else if (camioneroNum > 0)
                                        {
                                            lblMensajes.Text = "El camionero de este viaje esta en un viaje ahora.";
                                        }
                                        else
                                        {

                                            Viaje unViaje = new Viaje(id, costo, fecha, idCamion, idCamionero, estado);
                                            if (Web.modViaje(unViaje, idAdmin))
                                            {
                                                int contador = 0;
                                                if (unViaje.Estado == "En viaje")
                                                {
                                                    List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(0, unViaje.IdViaje);
                                                    List<Pedido> pedidos = Web.BuscarPedidoFiltro("", "", "", 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");

                                                    foreach (Viaje_Lot_Ped viaLotPed in viajeLotPed)
                                                    {
                                                        foreach (Pedido unPedido in pedidos)
                                                        {
                                                            if (viaLotPed.IdPedido.Equals(unPedido.IdPedido))
                                                            {
                                                                if (unPedido.Estado == "Confirmado")
                                                                {
                                                                    if (Web.cambiarEstadoPed(unPedido.IdPedido, "En viaje", idAdmin))
                                                                    {
                                                                        System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito y pedido puesto en viaje.";
                                                                        contador++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if(contador == 0)
                                                    {
                                                        System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito.";
                                                    }
                                                }
                                                else if (viaje.Estado == "En viaje" && unViaje.Estado == "Confirmado")
                                                {
                                                    List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(0, unViaje.IdViaje);
                                                    List<Pedido> pedidos = Web.BuscarPedidoFiltro("", "", "", 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
                                                    
                                                    foreach (Viaje_Lot_Ped viaLotPed in viajeLotPed)
                                                    {
                                                        
                                                        foreach (Pedido unPedido in pedidos)
                                                        {
                                                            if (viaLotPed.IdPedido.Equals(unPedido.IdPedido))
                                                            {
                                                                if (unPedido.Estado == "En viaje")
                                                                {
                                                                    if (Web.cambiarEstadoPed(unPedido.IdPedido, "Confirmado", idAdmin))
                                                                    {
                                                                        System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito y pedido puesto en confirmado.";
                                                                        contador++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (contador == 0)
                                                    {
                                                        System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito.";
                                                    }
                                                }
                                                else System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito.";

                                                limpiarIdSession();
                                                Response.Redirect("/Paginas/Viajes/frmViajes");
                                            }
                                            else
                                            {
                                                lblMensajes.Text = "Ocurrio un error al modificar el viaje.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Viaje unViaje = new Viaje(id, costo, fecha, idCamion, idCamionero, estado);
                                        if (Web.modViaje(unViaje, idAdmin))
                                        {
                                            limpiar();
                                            System.Web.HttpContext.Current.Session["pedidoMensaje"] = "Viaje modificado con exito.";

                                            limpiarIdSession();
                                            Response.Redirect("/Paginas/Viajes/frmViajes");
                                        }
                                        else
                                        {
                                            lblMensajes.Text = "Ocurrio un error al modificar el viaje.";
                                        }
                                    }
                                }
                                else
                                {
                                    lblMensajes.Text = "La fecha del viaje debe ser el dia de hoy.";
                                }
                            }
                            else
                            {
                                lblMensajes.Text = "Debe seleccionar un viaje.";
                            }
                        }
                        else
                        {
                            lblMensajes.Text = "La fecha del viaje no puede ser menor a hoy;";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "El costo no puede ser cero.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Faltan datos.";
                }
            }

            else
            {
                lblMensajes.Text = "Hay algún caracter no válido o faltante en el formulario";

            }

        }
    }
}