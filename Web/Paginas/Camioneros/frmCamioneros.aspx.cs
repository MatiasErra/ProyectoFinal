﻿using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Camioneros
{
    public partial class frmCamioneros : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] != null || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                {
                    btnVolver.Visible = true;
                    lstCamionero.Visible = false;
                    lstCamioneroSel.Visible = true;
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

                

                System.Web.HttpContext.Current.Session["idCamioneroSel"] = null;

                CargarListBuscar();
                CargarListOrdenarPor();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreCamioneroBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreCamioneroBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreCamioneroBuscar"] = null;
                txtApellidoBuscar.Text = System.Web.HttpContext.Current.Session["apellidoCamioneroBuscar"] != null ? System.Web.HttpContext.Current.Session["apellidoCamioneroBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["apellidoCamioneroBuscar"] = null;
                txtEmailBuscar.Text = System.Web.HttpContext.Current.Session["emailCamioneroBuscar"] != null ? System.Web.HttpContext.Current.Session["emailCamioneroBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["emailCamioneroBuscar"] = null;
             
                txtCedulaBuscar.Text = System.Web.HttpContext.Current.Session["cedulaCamioneroBuscar"] != null ? System.Web.HttpContext.Current.Session["cedulaCamioneroBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["cedulaCamioneroBuscar"] = null;

                txtFchVencBuscarPasada.Text = System.Web.HttpContext.Current.Session["fchVencPasadaCamioneroBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchVencPasadaCamioneroBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchVencPasadaCamioneroBuscar"] = null;
                txtFchVencBuscarFutura.Text = System.Web.HttpContext.Current.Session["fchVencFuturaCamioneroBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchVencFuturaCamioneroBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchVencFuturaCamioneroBuscar"] = null;

                // Listas
                lstDisponibleBuscar.SelectedValue = System.Web.HttpContext.Current.Session["disponibleCamioneroBuscar"] != null ? System.Web.HttpContext.Current.Session["disponibleCamioneroBuscar"].ToString() : "Seleccionar disponibilidad";
                System.Web.HttpContext.Current.Session["disponibleCamioneroBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstCamionero"] != null ? System.Web.HttpContext.Current.Session["BuscarLstCamionero"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstCamionero"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorCamionero"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorCamionero"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorCamionero"] = null;


                comprobarBuscar();
                listarPagina();

                lblMensajes.Text = System.Web.HttpContext.Current.Session["idCamioneroMod"] != null ? "Camionero modificado con éxito." : "";
                System.Web.HttpContext.Current.Session["idCamioneroMod"] = null;
            }
        }

        #endregion

        #region Utilidad

        private bool ViajeCamionero(int idCamionero)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Viaje viaje = new Viaje();
            int costoMenor = 0;
            int costoMayor = 99999999;
            string fchMenor = "1000-01-01";
            string fchMayor = "3000-12-30";
            viaje.IdCamion = 0;
            viaje.IdCamionero = 0;
            viaje.Estado = "";
            string ordenar = "";

            List<Viaje> viajes = Web.buscarViajeFiltro(viaje, costoMenor, costoMayor, fchMenor, fchMayor, ordenar);

            int i = 0;
            foreach (Viaje unViaje in viajes)
            {
                if (unViaje.IdCamionero == idCamionero)
                {
                    i++;
                    break;
                }
            }
            if (i == 0)
            {
                return true;
            }
            else return false;
        }



        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == "" || txtFchManejo.Text == "" || txtFchNac.Text == "" || txtCedula.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool fchNotToday()
        {
            string fecha = txtFchNac.Text;
            DateTime fechaDate = Convert.ToDateTime(fecha);
            if (fechaDate < DateTime.Today)
            {
                return true;
            }
            else { return false; }
        }

        private bool fchVencNotToday()
        {
            string fecha = txtFchManejo.Text;
            DateTime fechaDate = Convert.ToDateTime(fecha);
            if (fechaDate > DateTime.Today)
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

            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            txtFchNac.Text = "";
            txtCedula.Text = "";
            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
            txtFchManejo.Text = "";

            txtNombreBuscar.Text = "";
            txtApellidoBuscar.Text = "";
            txtEmailBuscar.Text = "";

            txtCedulaBuscar.Text = "";

            txtFchVencBuscarPasada.Text = "";
            txtFchVencBuscarFutura.Text = "";
            lstDisponibleBuscar.SelectedValue = "Seleccionar disponibilidad";
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
            List<Persona> lstPer = Web.lstIdPersonas();
            foreach (Persona persona in lstPer)
            {
                if (persona.IdPersona.Equals(intGuid))
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
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtApellidoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtEmailBuscar.Visible = listBuscarPor.SelectedValue == "Email" ? true : false;
            lblFchVenc.Visible = listBuscarPor.SelectedValue == "Fecha de vencimiento" ? true : false;
            txtCedulaBuscar.Visible = listBuscarPor.SelectedValue == "Cedula" ? true : false;
            lstDisponibleBuscar.Visible = listBuscarPor.SelectedValue == "Disponibilidad" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["nombreCamioneroBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["apellidoCamioneroBuscar"] = txtApellidoBuscar.Text;
            System.Web.HttpContext.Current.Session["emailCamioneroBuscar"] = txtEmailBuscar.Text;

            System.Web.HttpContext.Current.Session["cedulaCamioneroBuscar"] = txtCedula.Text;

             System.Web.HttpContext.Current.Session["disponibleCamioneroBuscar"] = lstDisponibleBuscar.SelectedValue != "Seleccionar disponibilidad" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["fchVencPasadaCamioneroBuscar"] = txtFchVencBuscarPasada.Text != "" ? txtFchVencBuscarPasada.Text : null;
            System.Web.HttpContext.Current.Session["fchVencFuturaCamioneroBuscar"] = txtFchVencBuscarFutura.Text != "" ? txtFchVencBuscarFutura.Text : null;
            System.Web.HttpContext.Current.Session["BuscarLstCamionero"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorCamionero"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 6;
        }



        private void listarPagina()
        {
            List<Camionero> camioneros = obtenerCamioneros();
            List<Camionero> camioneroPagina = new List<Camionero>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Camionero unCamionero in camioneros)
            {
                if (camioneroPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    camioneroPagina.Add(unCamionero);
                }

                cont++;
            }

            if (camioneroPagina.Count == 0)
            {
                txtPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Camionero.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCamionero.Visible = false;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] != null || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                {

                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstCamionero.Visible = false;
                    lstCamioneroSel.Visible = true;
                    lstCamioneroSel.DataSource = null;
                    lstCamioneroSel.DataSource = camioneroPagina;
                    lstCamioneroSel.DataBind();
                }
                else
                {
                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstCamioneroSel.Visible = false;
                    lstCamionero.Visible = true;
                    lstCamionero.DataSource = null;
                    lstCamionero.DataSource = camioneroPagina;
                    lstCamionero.DataBind();
                }
            }

        }
        private void modificarPagina()
        {
            List<Camionero> admins = obtenerCamioneros();
            double pxp = PagMax();
            double count = admins.Count;
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


        private List<Camionero> obtenerCamioneros()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero camionero = new Camionero();
            camionero.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            camionero.Apellido = HttpUtility.HtmlEncode(txtApellidoBuscar.Text);
            camionero.Email = HttpUtility.HtmlEncode(txtEmailBuscar.Text);

            camionero.Cedula = HttpUtility.HtmlEncode(txtCedulaBuscar.Text);
            camionero.Disponible = lstDisponibleBuscar.SelectedValue != "Seleccionar disponibilidad" ? lstDisponibleBuscar.SelectedValue : "";
            if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Abm" || System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
            {
                camionero.Disponible = "Disponible";
            }

            string fchVencDesde = txtFchVencBuscarPasada.Text != "" ? txtFchVencBuscarPasada.Text : "1000-01-01";
            string fchVencHasta = txtFchVencBuscarFutura.Text != "" ? txtFchVencBuscarFutura.Text : "3000-12-30";
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Camionero> camioneros = Web.buscarCamioneroFiltro(camionero, fchVencDesde, fchVencHasta, ordenar);

            return camioneros;
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
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Apellido", "Apellido", dt));
            dt.Rows.Add(createRow("Email", "Email", dt));
            dt.Rows.Add(createRow("Fecha de vencimiento", "Fecha de vencimiento", dt));
            dt.Rows.Add(createRow("Cedula", "Cedula", dt));
            dt.Rows.Add(createRow("Disponibilidad", "Disponibilidad", dt));

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
            dt.Rows.Add(createRow("Nombre y Apellido", "Nombre y Apellido", dt));
            dt.Rows.Add(createRow("Email", "Email", dt));
            dt.Rows.Add(createRow("Fecha de vencimiento", "Fecha de vencimiento", dt));
            dt.Rows.Add(createRow("Cedula", "Cedula", dt));
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
                    if (DateTime.Parse(txtFchVencBuscarPasada.Text) <= DateTime.Parse(txtFchVencBuscarFutura.Text)) num++;
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
                    lblMensajes.Text = "La fecha de vencimiento menor es mayor.";
                    listBuscarPor.SelectedValue = "Fecha de vencimiento";
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
                if (fchNotToday())
                {
                    if (fchVencNotToday())
                    {
                        if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                        {
                            int id = GenerateUniqueId();
                            string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                            string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                            string email = HttpUtility.HtmlEncode(txtEmail.Text);
                            string tele = HttpUtility.HtmlEncode(txtTel.Text);
                            string txtFc = HttpUtility.HtmlEncode(txtFchNac.Text);
                            string cedula = HttpUtility.HtmlEncode(txtCedula.Text);
                            string disponible = HttpUtility.HtmlEncode(lstDisponible.SelectedValue.ToString());
                            string txtFchVenc = HttpUtility.HtmlEncode(txtFchManejo.Text);

                            int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                            Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula, disponible, txtFchVenc);
                            if (Web.altaCamionero(unCamionero, idAdmin))
                            {
                                if (System.Web.HttpContext.Current.Session["ViajeDatosMod"] != null)
                                {
                                    System.Web.HttpContext.Current.Session["Camionero"] = unCamionero.IdPersona.ToString();
                                    Response.Redirect("/Paginas/Viajes/modViaje");
                                }
                                else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Abm")
                                {
                                    System.Web.HttpContext.Current.Session["Camionero"] = unCamionero.IdPersona.ToString();
                                    Response.Redirect("/Paginas/Viajes/frmViajes");
                                }
                                else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Buscar")
                                {
                                    System.Web.HttpContext.Current.Session["camioneroViajeBuscar"] = unCamionero.IdPersona.ToString();
                                    Response.Redirect("/Paginas/Viajes/frmViajes");
                                }
                                else
                                {
                                    limpiar();
                                    lblPaginaAct.Text = "1";
                                    listarPagina();
                                    lblMensajes.Text = "Camionero dado de alta con éxito.";
                                }
                            }
                            else lblMensajes.Text = "Ya existe un Camionero con estos datos. Estos son los posibles datos repetidos (Email / Teléfono / Cedula).";
                        }
                        else lblMensajes.Text = "Falta seleccionar la disponibilidad.";
                    }
                    else lblMensajes.Text = "Seleccione una fecha de vencimiento mayor a hoy.";
                }
                else lblMensajes.Text = "Seleccione una fecha de nacimiento menor a hoy.";
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
                System.Web.HttpContext.Current.Session["Camionero"] = id;
                Response.Redirect("/Paginas/Viajes/modViaje");
            }
            else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Abm")
            {
                System.Web.HttpContext.Current.Session["Camionero"] = id;
                Response.Redirect("/Paginas/Viajes/frmViajes");
            }
            else if (System.Web.HttpContext.Current.Session["ViajeDatosFrm"] == "Buscar")
            {
                System.Web.HttpContext.Current.Session["camioneroViajeBuscar"] = id;
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
            Camionero unCamionero = Web.buscarCamionero(id);
            if (unCamionero != null)
            {
                if (ViajeCamionero(unCamionero.IdPersona))
                {
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.bajaCamionero(id, idAdmin))
                    {
                        limpiar();
                        
                        lblPaginaAct.Text = "1";
                        listarPagina();
                        lblMensajes.Text = "Se ha borrado el camionero.";
                    }
                    else lblMensajes.Text = "No se ha podido borrar el camionero.";
                }
                else lblMensajes.Text = "El camionero no se puede eliminar porque tiene un viaje.";
            }
            else lblMensajes.Text = "El camionero no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";

            guardarBuscar();

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idCamioneroSel"] = id;
            Response.Redirect("/Paginas/Camioneros/modCamionero");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion

    }
}