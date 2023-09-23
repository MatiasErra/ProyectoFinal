using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Camiones
{
    public partial class frmCamiones : System.Web.UI.Page
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
                else if (admin.TipoDeAdmin == "Administrador de productos")
                {
                    this.MasterPageFile = "~/Master/AProductos.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
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

                System.Web.HttpContext.Current.Session["idCamionSel"] = null;

                if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] != null || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                {
                    btnVolver.Visible = true;
                    lstCamiones.Visible = false;
                    lstCamionesSel.Visible = true;
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

                cargarDisponible();
                CargarListOrdenarPor();
                CargarListBuscar();

                // Buscador
                txtMarcaBuscar.Text = System.Web.HttpContext.Current.Session["marcaCamionBuscar"] != null ? System.Web.HttpContext.Current.Session["marcaCamionBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["marcaCamionBuscar"] = null;
                txtModeloBuscar.Text = System.Web.HttpContext.Current.Session["modeloCamionBuscar"] != null ? System.Web.HttpContext.Current.Session["modeloCamionBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["modeloCamionBuscar"] = null;
                txtCargaMenorBuscar.Text = System.Web.HttpContext.Current.Session["cargaMenorCamionBuscar"] != null ? System.Web.HttpContext.Current.Session["cargaMenorCamionBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["cargaMenorCamionBuscar"] = null;
                txtCargaMayorBuscar.Text = System.Web.HttpContext.Current.Session["cargaMayorCamionBuscar"] != null ? System.Web.HttpContext.Current.Session["cargaMayorCamionBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["cargaMayorCamionBuscar"] = null;

                // Listas
                lstDisponibleBuscar.SelectedValue = System.Web.HttpContext.Current.Session["disponibleCamionBuscar"] != null ? System.Web.HttpContext.Current.Session["disponibleCamionBuscar"].ToString() : "Seleccionar disponibilidad";
                System.Web.HttpContext.Current.Session["disponibleCamionBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstCamion"] != null ? System.Web.HttpContext.Current.Session["BuscarLstCamion"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstCamion"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorCamion"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorCamion"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorCamion"] = null;

                comprobarBuscar();
                listarPagina();
                lblMensajes.Text = System.Web.HttpContext.Current.Session["idCamioneroMod"] != null ? "Camión Modificado" : "";
                System.Web.HttpContext.Current.Session["idCamioneroMod"] = null;
            }
        }

        #endregion

        #region Utilidad

        private bool faltanDatos()
        {
            if (txtModelo.Text == "" || txtMarca.Text == "" || txtCarga.Text == "")
            {
                return true;
            }
            return false;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtModelo.Text = "";
            txtMarca.Text = "";
            txtCarga.Text = "";

            txtModeloBuscar.Text = "";
            txtMarcaBuscar.Text = "";
            txtCargaMayorBuscar.Text = "";
            txtCargaMenorBuscar.Text = "";
            lstDisponibleBuscar.SelectedValue = "Seleccionar disponibilidad";

            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
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
            Camion cam = new Camion(0, "", "", 0, "");
            List<Camion> lstCam = Web.buscarFiltroCam(cam, 0, 99999, "");
            foreach (Camion camion in lstCam)
            {
                if (camion.IdCamion.Equals(intGuid))
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

        private void comprobarBuscar()
        {
            txtMarcaBuscar.Visible = listBuscarPor.SelectedValue == "Marca" ? true : false;
            txtModeloBuscar.Visible = listBuscarPor.SelectedValue == "Marca" ? true : false;
            lblCarga.Visible = listBuscarPor.SelectedValue == "Carga" ? true : false;
            lstDisponibleBuscar.Visible = listBuscarPor.SelectedValue == "Disponibilidad" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["marcaCamionBuscar"] = txtMarcaBuscar.Text;
            System.Web.HttpContext.Current.Session["modeloCamionBuscar"] = txtModeloBuscar.Text;
            System.Web.HttpContext.Current.Session["cargaMenorCamionBuscar"] = txtCargaMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["cargaMayorCamionBuscar"] = txtCargaMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["disponibleCamionBuscar"] = lstDisponibleBuscar.SelectedValue != "Seleccionar disponibilidad" ? lstDisponibleBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["BuscarLst"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 2;
        }

        private void listarPagina()
        {
            List<Camion> camiones = obtenerCamiones();
            List<Camion> camionesPagina = new List<Camion>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Camion unCamion in camiones)
            {
                if (camionesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    camionesPagina.Add(unCamion);
                }

                cont++;
            }

            if (camiones.Count == 0)
            {

                lblPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Camión.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCamiones.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] != null || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                {

                    lblPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstCamiones.Visible = false;
                    lstCamionesSel.Visible = true;
                    lstCamionesSel.DataSource = null;
                    lstCamionesSel.DataSource = camionesPagina;
                    lstCamionesSel.DataBind();
                }
                else
                {
                    lblPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstCamionesSel.Visible = false;
                    lstCamiones.Visible = true;
                    lstCamiones.DataSource = null;
                    lstCamiones.DataSource = camionesPagina;
                    lstCamiones.DataBind();
                }
            }

        }
        private void modificarPagina()
        {
            List<Camion> camiones = obtenerCamiones();
            double pxp = PagMax();
            double count = camiones.Count;
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


        private List<Camion> obtenerCamiones()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion camion = new Camion();
            camion.Marca = HttpUtility.HtmlEncode(txtMarcaBuscar.Text);
            camion.Modelo = HttpUtility.HtmlEncode(txtModeloBuscar.Text);
            camion.Disponible = lstDisponibleBuscar.SelectedValue != "Seleccionar disponibilidad" ? lstDisponibleBuscar.SelectedValue : "";
            if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Abm" || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
            {
                camion.Disponible = "Disponible";
            }
            double cargaMenor = txtCargaMenorBuscar.Text == "" ? 0 : double.Parse(txtCargaMenorBuscar.Text);
            double cargaMayor = txtCargaMayorBuscar.Text == "" ? 99999 : double.Parse(txtCargaMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Camion> camiones = Web.buscarFiltroCam(camion, cargaMenor, cargaMayor, ordenar);

            return camiones;
        }

        #endregion

        #region DropDownBoxes

        #region Disponible

        public void cargarDisponible()
        {
            lstDisponible.DataSource = createDataSource();
            lstDisponible.DataTextField = "nombre";
            lstDisponible.DataValueField = "id";
            lstDisponible.DataBind();

            lstDisponibleBuscar.DataSource = createDataSource();
            lstDisponibleBuscar.DataTextField = "nombre";
            lstDisponibleBuscar.DataValueField = "id";
            lstDisponibleBuscar.DataBind();
        }

        ICollection createDataSource()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(createRow("Seleccionar disponibilidad", "Seleccionar disponibilidad", dt));
            dt.Rows.Add(createRow("Disponible", "Disponible", dt));
            dt.Rows.Add(createRow("No disponible", "No disponible", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

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
            dt.Rows.Add(createRow("Marca", "Marca", dt));
            dt.Rows.Add(createRow("Modelo", "Modelo", dt));
            dt.Rows.Add(createRow("Carga", "Carga", dt));
            dt.Rows.Add(createRow("Disponible", "Disponible", dt));
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
            dt.Rows.Add(createRow("Marca", "Marca", dt));
            dt.Rows.Add(createRow("Carga", "Carga", dt));
            dt.Rows.Add(createRow("Disponibilidad", "Disponibilidad", dt));

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
                if (int.Parse(txtCargaMenorBuscar.Text) <= int.Parse(txtCargaMayorBuscar.Text)) num++;
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
                lblMensajes.Text = "La carga menor es mayor.";
                listBuscarPor.SelectedValue = "Carga";
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

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {

                if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                {
                    int id = GenerateUniqueId();
                    string marca = HttpUtility.HtmlEncode(txtMarca.Text);
                    string modelo = HttpUtility.HtmlEncode(txtModelo.Text);
                    double carga = double.Parse(HttpUtility.HtmlEncode(txtCarga.Text));
                    string disponible = HttpUtility.HtmlEncode(lstDisponible.SelectedValue.ToString());

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Camion camion = new Camion(id, marca, modelo, carga, disponible);
                    if (Web.altaCam(camion))
                    {
                        if (System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                        {
                            System.Web.HttpContext.Current.Session["Camion"] = camion.IdCamion.ToString();
                            Response.Redirect("/Paginas/Viajes/modViaje");
                        }
                        else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Abm")
                        {
                            System.Web.HttpContext.Current.Session["Camion"] = camion.IdCamion.ToString();
                            Response.Redirect("/Paginas/Viajes/frmViajes");
                        }
                        else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Buscar")
                        {
                            System.Web.HttpContext.Current.Session["camionViajeBuscar"] = camion.IdCamion.ToString();
                            Response.Redirect("/Paginas/Viajes/frmViajes");
                        }
                        else
                        {
                            limpiar();
                            lblMensajes.Text = "Camión dado de alta con éxito.";
                            lblPaginaAct.Text = "1";
                            listarPagina();
                        }
                    }
                    else lblMensajes.Text = "No se pudo dar de alta el Camión.";
                }
                else lblMensajes.Text = "Falta seleccionar la disponibilidad.";
            }
            else lblMensajes.Text = "Faltan datos.";
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            if (System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
            {
                System.Web.HttpContext.Current.Session["Camion"] = id;
                Response.Redirect("/Paginas/Viajes/modViaje");
            }
            else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"].ToString() == "Abm")
            {
                System.Web.HttpContext.Current.Session["Camion"] = id;
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }
            else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"].ToString() == "Buscar")
            {
                System.Web.HttpContext.Current.Session["camionViajeBuscar"] = id;
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
            {
                Response.Redirect("/Paginas/Viajes/modViaje");
            }
            else
            {
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }
        }


        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion camion = Web.buscarCam(id);
            if (camion != null)
            {
                if (Web.bajaCam(id))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha borrado el Camión.";
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else lblMensajes.Text = "No se ha podido borrar el Camión.";
            }
            else lblMensajes.Text = "El Camión no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";
            guardarBuscar();

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idCamionSel"] = id;
            Response.Redirect("/Paginas/Camiones/modCamiones");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion

    }
}
