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
    public partial class frmViajes : System.Web.UI.Page
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajesSelected"] = null;
            System.Web.HttpContext.Current.Session["PedidoCompraSel"] = null;
            System.Web.HttpContext.Current.Session["idViajeSelMod"] = null;
            if (!IsPostBack)
            {
                CargarListCamionero();
                CargarListCamioneroBuscar();
                CargarListCamiones();
                CargarListCamionesBuscar();
                CargarListEstado();
                CargarListBuscar();
                CargarListOrdenarPor();


                if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] != null)
                {
                    cargarDatos();
                }
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }



                // Buscador
                txtCostoMenorBuscar.Text = System.Web.HttpContext.Current.Session["costoMenorViajeBuscar"] != null ? System.Web.HttpContext.Current.Session["costoMenorViajeBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["costoMenorViajeBuscar"] = null;
                txtCostoMayorBuscar.Text = System.Web.HttpContext.Current.Session["costoMayorViajeBuscar"] != null ? System.Web.HttpContext.Current.Session["costoMayorViajeBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["costoMayorViajeBuscar"] = null;
                txtFchMenor.Text = System.Web.HttpContext.Current.Session["fchMenorViajeBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchMenorViajeBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchMenorViajeBuscar"] = null;
                txtFchMayor.Text = System.Web.HttpContext.Current.Session["fchMayorViajeBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchMayorViajeBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchMayorViajeBuscar"] = null;

                // Listas
                lstCamionBuscar.SelectedValue = System.Web.HttpContext.Current.Session["camionViajeBuscar"] != null ? System.Web.HttpContext.Current.Session["camionViajeBuscar"].ToString() : "Seleccionar un Camion";
                System.Web.HttpContext.Current.Session["camionViajeBuscar"] = null;
                lstCamioneroBuscar.SelectedValue = System.Web.HttpContext.Current.Session["camioneroViajeBuscar"] != null ? System.Web.HttpContext.Current.Session["camioneroViajeBuscar"].ToString() : "Seleccionar un Camionero";
                System.Web.HttpContext.Current.Session["camioneroViajeBuscar"] = null;
                lstEstadoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["estadoViajeBuscar"] != null ? System.Web.HttpContext.Current.Session["estadoViajeBuscar"].ToString() : "Seleccionar un Estado";
                System.Web.HttpContext.Current.Session["estadoViajeBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstViaje"] != null ? System.Web.HttpContext.Current.Session["BuscarLstViaje"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstViaje"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorViaje"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorViaje"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorViaje"] = null;
                comprobarBuscar();
                listarPagina();


                lblMensajes.Text = System.Web.HttpContext.Current.Session["pedidoMensaje"] != null ? System.Web.HttpContext.Current.Session["pedidoMensaje"].ToString() : "";
                System.Web.HttpContext.Current.Session["pedidoMensaje"] = null;
            }
        }

        #endregion

        #region Utilidad

        private bool faltanDatos()
        {
            if (txtCosto.Text == "" || txtFch.Text == "" || listCamion.SelectedValue == "Seleccione un Camion" || listCamionero.SelectedValue == "Seleccione un Camionero")
            {
                return true;
            }
            return false;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";

            txtCostoMenorBuscar.Text = "";
            txtCostoMayorBuscar.Text = "";
            txtFchMenor.Text = "";
            txtFchMayor.Text = "";
            lstCamionBuscar.SelectedValue = "Seleccione un Camion";
            lstCamioneroBuscar.SelectedValue = "Seleccione un Camionero";
            lstEstadoBuscar.SelectedValue = "Seleccione un Estado";

            txtCosto.Text = "";
            txtFch.Text = "";
            listCamion.SelectedValue = "Seleccione un Camion";
            listCamionero.SelectedValue = "Seleccione un Camionero";
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        static int GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            int intGuid = guid.GetHashCode();
            int i = 0;

            while (intGuid < 0)
            {
                return GenerateUniqueId();
            }

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Viaje viaje = new Viaje(0, 0, "", 0, 0, "");
            List<Viaje> lstViajes = Web.buscarViajeFiltro(viaje, 0, 99999, "1000-01-01", "3000-12-30", "");
            foreach (Viaje unViaje in lstViajes)
            {
                if (unViaje.IdViaje.Equals(intGuid))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                return intGuid;
            }
            else return GenerateUniqueId();
        }

        private bool comprobarViajesLote(int id)
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int idPedido = 0;
            int idViaje = 0;
            List<Viaje_Lot_Ped> viajeLotPed = Web.buscarViajePedLote(idPedido, idViaje);
            foreach (Viaje_Lot_Ped unLoteVia in viajeLotPed)
            {
                if (unLoteVia.IdViaje.Equals(id))
                {
                    return false;
                }
            }
            return true;

        }

        private void comprobarBuscar()
        {
            lblCosto.Visible = listBuscarPor.SelectedValue == "Costo" ? true : false;
            lblFch.Visible = listBuscarPor.SelectedValue == "Fecha" ? true : false;
            lstCamionBuscar.Visible = listBuscarPor.SelectedValue == "Camion" ? true : false;
            btnBuscarCamionBuscar.Visible = listBuscarPor.SelectedValue == "Camion" ? true : false;
            lstCamioneroBuscar.Visible = listBuscarPor.SelectedValue == "Camionero" ? true : false;
            btnBuscarCamioneroBuscar.Visible = listBuscarPor.SelectedValue == "Camionero" ? true : false;
            lstEstadoBuscar.Visible = listBuscarPor.SelectedValue == "Estado" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["costoMenorViajeBuscar"] = txtCostoMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["costoMayorViajeBuscar"] = txtCostoMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["fchMenorViajeBuscar"] = txtFchMenor.Text != "" ? txtFchMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchMayorViajeBuscar"] = txtFchMayor.Text != "" ? txtFchMayor.Text : null;
            System.Web.HttpContext.Current.Session["camionViajeBuscar"] = lstCamionBuscar.SelectedValue != "Seleccione un Camion" ? lstCamionBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["camioneroViajeBuscar"] = lstCamioneroBuscar.SelectedValue != "Seleccione un Camionero" ? lstCamioneroBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["estadoViajeBuscar"] = lstEstadoBuscar.SelectedValue != "Seleccione un Estado" ? lstEstadoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["BuscarLstViaje"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorViaje"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 6;
        }



        private List<Viaje> obtenerViajes()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Viaje viaje = new Viaje();
            int costoMenor = txtCostoMenorBuscar.Text == "" ? 0 : int.Parse(txtCostoMenorBuscar.Text);
            int costoMayor = txtCostoMayorBuscar.Text == "" ? 99999999 : int.Parse(txtCostoMayorBuscar.Text);
            string fchMenor = txtFchMenor.Text == "" ? "1000-01-01" : txtFchMenor.Text;
            string fchMayor = txtFchMayor.Text == "" ? "3000-12-30" : txtFchMayor.Text;
            viaje.IdCamion = lstCamionBuscar.SelectedValue != "Seleccione un Camion" ? int.Parse(lstCamionBuscar.SelectedValue) : 0;
            viaje.IdCamionero = lstCamioneroBuscar.SelectedValue != "Seleccione un Camionero" ? int.Parse(lstCamioneroBuscar.SelectedValue) : 0;
            viaje.Estado = lstEstadoBuscar.SelectedValue != "Seleccione un Estado" ? lstEstadoBuscar.SelectedValue : "";
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Viaje> viajes = Web.buscarViajeFiltro(viaje, costoMenor, costoMayor, fchMenor, fchMayor, ordenar);

            return viajes;
        }


        private void listarPagina()
        {
            List<Viaje> viajes = obtenerViajes();
            List<Viaje> viajesPagina = new List<Viaje>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Viaje unViaje in viajes)
            {
                if (viajesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    viajesPagina.Add(unViaje);
                }

                cont++;
            }

            if (viajesPagina.Count == 0)
            {
                txtPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Viaje.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstViaje.Visible = false;

            }
            else
            {

                txtPaginas.Visible = true;
                lblMensajes.Text = "";
                modificarPagina();
                lstViaje.Visible = true;
                lstViaje.DataSource = null;
                lstViaje.DataSource = ObtenerDatos(viajesPagina);
                lstViaje.DataBind();

            }
        }

        private void modificarPagina()
        {
            List<Viaje> viajes = obtenerViajes();
            double pxp = PagMax();
            double count = viajes.Count;
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


        public DataTable ObtenerDatos(List<Viaje> viajes)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("IdViaje", typeof(int)),
                new DataColumn("Costo", typeof(string)),
                new DataColumn("Fecha", typeof(string)),
                new DataColumn("idCamion", typeof(string)),
                new DataColumn("MarcaCamion", typeof(string)),
                new DataColumn("ModeloCamion", typeof(string)),
                new DataColumn("idCamionero", typeof(string)),
                new DataColumn("NombreCamionero", typeof(string)),
                new DataColumn("Estado", typeof(string)),


            });

            foreach (Viaje unViaje in viajes)
            {

                DataRow dr = dt.NewRow();
                dr["IdViaje"] = unViaje.IdViaje.ToString();
                dr["Costo"] = unViaje.Costo.ToString() + " $";
                dr["Fecha"] = unViaje.Fecha.ToString();
                dr["idCamion"] = unViaje.IdCamion.ToString();
                dr["MarcaCamion"] = unViaje.MarcaCamion.ToString();
                dr["ModeloCamion"] = unViaje.ModeloCamion.ToString();
                dr["idCamionero"] = unViaje.IdCamionero.ToString();
                dr["NombreCamionero"] = unViaje.NombreCamionero.ToString();
                dr["Estado"] = unViaje.Estado.ToString();

                dt.Rows.Add(dr);



            }

            return dt;
        }

        #endregion

        #region Guardar y cargar datos
        private void guardarDatos()
        {

            System.Web.HttpContext.Current.Session["Costo"] = txtCosto.Text;
            System.Web.HttpContext.Current.Session["Fecha"] = txtFch.Text != "" ? txtFch.Text : null;
            System.Web.HttpContext.Current.Session["Camion"] = listCamion.SelectedValue != "Seleccione un Camion" ? listCamion.SelectedValue : null;
            System.Web.HttpContext.Current.Session["Camionero"] = listCamionero.SelectedValue != "Seleccione un Camionero" ? listCamionero.SelectedValue : null;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["ViajeDatosFrm"] = null;

            txtCosto.Text = System.Web.HttpContext.Current.Session["Costo"] != null ? System.Web.HttpContext.Current.Session["Costo"].ToString() : "";
            System.Web.HttpContext.Current.Session["Costo"] = null;
            txtFch.Text = System.Web.HttpContext.Current.Session["Fecha"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["Fecha"].ToString()).ToString("yyyy-MM-dd") : "";
            System.Web.HttpContext.Current.Session["Fecha"] = null;
            listCamion.SelectedValue = System.Web.HttpContext.Current.Session["Camion"] != null ? System.Web.HttpContext.Current.Session["Camion"].ToString() : "Seleccione un Camion";
            System.Web.HttpContext.Current.Session["Camion"] = null;
            listCamionero.SelectedValue = System.Web.HttpContext.Current.Session["Camionero"] != null ? System.Web.HttpContext.Current.Session["Camionero"].ToString() : "Seleccione un Camionero";
            System.Web.HttpContext.Current.Session["Camionero"] = null;
        }

        #endregion


        #region DropDownBoxes

        #region Ordenar

        public void CargarListOrdenarPor()
        {
            listOrdenarPor.DataSource = null;
            listOrdenarPor.DataSource = createDataSourceOrdenarPor();
            listOrdenarPor.DataTextField = "nombre";
            listOrdenarPor.DataValueField = "id";
            listOrdenarPor.DataBind();
        }

        ICollection createDataSourceOrdenarPor()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ordenar por", "Ordenar por", dt));
            dt.Rows.Add(createRow("Costo", "Costo", dt));
            dt.Rows.Add(createRow("Fecha", "Fecha", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));

            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

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
            dt.Rows.Add(createRow("Costo", "Costo", dt));
            dt.Rows.Add(createRow("Fecha", "Fecha", dt));
            dt.Rows.Add(createRow("Camion", "Camion", dt));
            dt.Rows.Add(createRow("Camionero", "Camionero", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));
            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Camion

        public void CargarListCamiones()
        {
            listCamion.DataSource = null;
            listCamion.DataSource = createDataSourceCamion();
            listCamion.DataTextField = "nombre";
            listCamion.DataValueField = "id";
            listCamion.DataBind();

            lstCamionBuscar.DataSource = null;
            lstCamionBuscar.DataSource = createDataSourceCamion();
            lstCamionBuscar.DataTextField = "nombre";
            lstCamionBuscar.DataValueField = "id";
            lstCamionBuscar.DataBind();

        }

        ICollection createDataSourceCamion()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion cam = new Camion(0, "", "", 0, "");
            List<Camion> camiones = Web.buscarFiltroCam(cam, 0, 99999, "");
            List<Camion> cargar = new List<Camion>();

            foreach (Camion unCam in camiones)
            {
                if (unCam.Disponible == "Disponible")
                {
                    cargar.Add(unCam);
                }
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

        #region Camion Buscar

        public void CargarListCamionesBuscar()
        {
            lstCamionBuscar.DataSource = null;
            lstCamionBuscar.DataSource = createDataSourceCamionBuscar();
            lstCamionBuscar.DataTextField = "nombre";
            lstCamionBuscar.DataValueField = "id";
            lstCamionBuscar.DataBind();

        }

        ICollection createDataSourceCamionBuscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion cam = new Camion(0, "", "", 0, "");
            List<Camion> camiones = Web.buscarFiltroCam(cam, 0, 99999, "");

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Camion", "Seleccione un Camion", dt));

            cargarCamionesBuscar(camiones, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarCamionesBuscar(List<Camion> camiones, DataTable dt)
        {
            foreach (Camion unCamion in camiones)
            {
                dt.Rows.Add(createRow(unCamion.Marca + " " + unCamion.Modelo + " " + unCamion.Carga + " Kg", unCamion.IdCamion.ToString(), dt));
            }
        }

        #endregion

        #region Camionero

        public void CargarListCamionero()
        {
            listCamionero.DataSource = null;
            listCamionero.DataSource = createDataSourceCamionero();
            listCamionero.DataTextField = "nombre";
            listCamionero.DataValueField = "id";
            listCamionero.DataBind();
        }

        ICollection createDataSourceCamionero()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero cam = new Camionero(0, "", "", "", "", "", "", "", "");
            List<Camionero> camioneros = Web.buscarCamioneroFiltro(cam, "1000-01-01", "3000-12-30", "");
            List<Camionero> cargar = new List<Camionero>();

            foreach (Camionero unCam in camioneros)
            {
                if (unCam.Disponible == "Disponible")
                {
                    cargar.Add(unCam);
                }
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

        #region Camionero Buscar

        public void CargarListCamioneroBuscar()
        {
            lstCamioneroBuscar.DataSource = null;
            lstCamioneroBuscar.DataSource = createDataSourceCamioneroBuscar();
            lstCamioneroBuscar.DataTextField = "nombre";
            lstCamioneroBuscar.DataValueField = "id";
            lstCamioneroBuscar.DataBind();
        }

        ICollection createDataSourceCamioneroBuscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero cam = new Camionero(0, "", "", "", "", "", "", "", "");
            List<Camionero> camioneros = Web.buscarCamioneroFiltro(cam, "1000-01-01", "3000-12-30", "");


            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Camionero", "Seleccione un Camionero", dt));

            cargarCamionerosBuscar(camioneros, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarCamionerosBuscar(List<Camionero> camioneros, DataTable dt)
        {
            foreach (Camionero unCamionero in camioneros)
            {
                dt.Rows.Add(createRow(unCamionero.Nombre + " " + unCamionero.Apellido, unCamionero.IdPersona.ToString(), dt));
            }
        }

        #endregion

        #region Estado

        public void CargarListEstado()
        {
            lstEstadoBuscar.DataSource = null;
            lstEstadoBuscar.DataSource = createDataSourceEstado();
            lstEstadoBuscar.DataTextField = "nombre";
            lstEstadoBuscar.DataValueField = "id";
            lstEstadoBuscar.DataBind();
        }

        ICollection createDataSourceEstado()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Estado", "Seleccione un Estado", dt));
            dt.Rows.Add(createRow("Pendiente", "Pendiente", dt));
            dt.Rows.Add(createRow("En viaje", "En viaje", dt));
            dt.Rows.Add(createRow("Finalizado", "Finalizado", dt));

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

        #region Botones

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                if (DateTime.Parse(txtFchMenor.Text) <= DateTime.Parse(txtFchMayor.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {
                try
                {
                    if (int.Parse(txtCostoMenorBuscar.Text) <= int.Parse(txtCostoMayorBuscar.Text)) num++;
                }
                catch
                {
                    num++;
                }
                if (num == 2)
                {
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else
                {
                    lblMensajes.Text = "El costo menor es mayor.";
                    listBuscarPor.SelectedValue = "Costo";
                    comprobarBuscar();
                }
            }
            else
            {
                lblMensajes.Text = "La fecha menor es mayor.";
                listBuscarPor.SelectedValue = "Fecha";
                comprobarBuscar();
            }
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comprobarBuscar();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnBuscarCamion_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosFrm"] = "Abm";
            guardarDatos();
            guardarBuscar();
            Response.Redirect("/Paginas/Camiones/frmCamiones");
        }

        protected void btnBuscarCamionBuscar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosFrm"] = "Buscar";
            guardarDatos();
            guardarBuscar();
            Response.Redirect("/Paginas/Camiones/frmCamiones");
        }

        protected void btnBuscarCamionero_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosFrm"] = "Abm";
            guardarDatos();
            guardarBuscar();
            Response.Redirect("/Paginas/Camioneros/frmCamioneros");
        }

        protected void btnBuscarCamioneroBuscar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ViajeDatosFrm"] = "Buscar";
            guardarDatos();
            guardarBuscar();
            Response.Redirect("/Paginas/Camioneros/frmCamioneros");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (int.Parse(txtCosto.Text) != 0)
                {
                    if (Convert.ToDateTime(txtFch.Text) >= DateTime.Today)
                    {
                        int id = GenerateUniqueId();
                        int costo = int.Parse(HttpUtility.HtmlEncode(txtCosto.Text));
                        string fecha = HttpUtility.HtmlEncode(txtFch.Text);
                        int idCamion = int.Parse(HttpUtility.HtmlEncode(listCamion.SelectedValue));
                        int idCamionero = int.Parse(HttpUtility.HtmlEncode(listCamionero.SelectedValue));

                        int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

                        Viaje unViaje = new Viaje(id, costo, fecha, idCamion, idCamionero, "Pendiente");
                        if (Web.altaViaje(unViaje, idAdmin))
                        {



                            limpiar();
                            lblPaginaAct.Text = "1";
                            listarPagina();
                            lblMensajes.Text = "Viaje dado de alta con éxito.";

                        }
                        else lblMensajes.Text = "Ocurrio un error al ingresar el Viaje.";
                    }
                    else lblMensajes.Text = "La fecha del viaje no puede ser menor a hoy;";
                }
                else lblMensajes.Text = "El costo no puede ser cero.";
            }
            else lblMensajes.Text = "Faltan datos.";
        }

        protected void btnVerLotes_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            System.Web.HttpContext.Current.Session["ViajesSelected"] = id;
            //string estadoVia = HttpUtility.HtmlEncode(selectedrow.Cells[6].Text);
            //System.Web.HttpContext.Current.Session["estadoVia"] = estadoVia;


            Response.Redirect("/Paginas/Viajes/verLoteDelViaje");
        }


        protected void btnAsignarPaqu_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            System.Web.HttpContext.Current.Session["ViajesSelected"] = id;
            string fecha = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);
            DateTime fechaDate = DateTime.ParseExact(fecha, "d/M/yyyy", null);
            string estadoVia = HttpUtility.HtmlEncode(selectedrow.Cells[8].Text);

            string[] Datehoy = DateTime.Now.ToString().Split(' ');
            DateTime fechaHoy = DateTime.ParseExact(Datehoy[0].ToString(), "d/M/yyyy", null);
            string[] fechaHoyArry = fechaHoy.ToString().Split(' ');

            if (estadoVia == "Pendiente"
                || estadoVia == "Confirmado")
            {
                if (fechaDate >= fechaHoy)
                {
                    Response.Redirect("/Paginas/Viajes/asgPedAViaje");
                }
                else
                {
                    lblMensajes.Text = "La fecha del viaje debe ser igual o menor a la fecha de hoy " + fechaHoyArry[0].ToString();
                }
            }
            else
            {
                lblMensajes.Text = "El pedido tiene que estar en Pendiente o Confirmado para poder asignarle un Lote";
            }


        }
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Viaje unViaje = Web.buscarViaje(id);
            if (unViaje != null)
            {
                if (comprobarViajesLote(id))
                {
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.bajaViaje(id, idAdmin))
                    {
                        if (unViaje.Estado == "En viaje")
                        {
                            Camion unCamion = Web.buscarCam(unViaje.IdCamion);
                            unCamion.Disponible = "Disponible";
                            Camionero unCamionero = Web.buscarCamionero(unViaje.IdCamionero);
                            unCamionero.Disponible = "Disponible";
                            if (Web.modCam(unCamion, idAdmin))
                            {
                                if (Web.modCamionero(unCamionero, idAdmin))
                                {
                                    limpiar();
                                    lblPaginaAct.Text = "1";
                                    listarPagina();

                                    CargarListCamiones();
                                    CargarListCamionero();
                                    lblMensajes.Text = "Se ha borrado el viaje y el camión y camionero ahora estan disponibles.";
                                }
                            }
                        }
                        else
                        {
                            limpiar();
                            lblPaginaAct.Text = "1";
                            listarPagina();
                            lblMensajes.Text = "Se ha borrado el viaje.";
                        }
                    }
                    else lblMensajes.Text = "No se ha podido borrar el viaje.";
                }
                else lblMensajes.Text = "No se ha podido eliminar este viaje porque está asociada a un pedido.";
            }
            else lblMensajes.Text = "El viaje no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";
            guardarBuscar();

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idViajeSelMod"] = id;
            Response.Redirect("/Paginas/Viajes/modViaje");


        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion
    }
}