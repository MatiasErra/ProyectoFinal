using Clases;
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
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

                cargarDisponible();

                if (System.Web.HttpContext.Current.Session["idCamioneroMod"] != null)
                {
                    lblMensajes.Text = "Camionero modificado con éxito.";
                    System.Web.HttpContext.Current.Session["idCamioneroMod"] = null;
                }
                System.Web.HttpContext.Current.Session["idCamioneroSel"] = null;

                CargarListFiltroTipo();
                CargarListOrdenarPor();


                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;
                }

                if (System.Web.HttpContext.Current.Session["FiltroTipo"] != null)
                {
                    listFiltroTipo.SelectedValue = System.Web.HttpContext.Current.Session["FiltroTipo"].ToString();
                    System.Web.HttpContext.Current.Session["FiltroTipo"] = null;
                }


                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                }
           
                listarPagina();
            }
        }

        #region Utilidad


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



        public void cargarDisponible()
        {
            lstDisponible.DataSource = createDataSource();
            lstDisponible.DataTextField = "nombre";
            lstDisponible.DataValueField = "id";
            lstDisponible.DataBind();
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

        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
           
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            txtFchNac.Text = "";
            txtCedula.Text = "";
            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
            listFiltroTipo.SelectedValue = "Seleccionar disponibilidad";
            listOrdenarPor.SelectedValue = "Ordenar por";
            txtFchManejo.Text = "";
            lstCamionero.SelectedIndex = -1;
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

  
        private int PagMax()
        {

            return 5;
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
                lblMensajes.Text = "No se encontro ningún Camionero.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCamionero.Visible = false;
            }
            else
            {

                lblMensajes.Text = "";
                modificarPagina();
                lstCamionero.Visible = true;
                lstCamionero.DataSource = null;
                lstCamionero.DataSource = camioneroPagina;
                lstCamionero.DataBind();
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
            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }


        private List<Camionero> obtenerCamioneros()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;
            string disp = "";
         
            string ordenar = "";
            if (listFiltroTipo.SelectedValue != "Seleccionar disponibilidad")
            {
                disp = listFiltroTipo.SelectedValue;
            }

            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }

            List<Camionero> camioneros = Web.buscarCamioneroFiltro(buscar, disp,ordenar);

 
            return camioneros;
        }
        #region Filtro

        public void CargarListFiltroTipo()
        {
            listFiltroTipo.DataSource = null;
            listFiltroTipo.DataSource = createDataSourceFiltroTipoHab();
            listFiltroTipo.DataTextField = "nombre";
            listFiltroTipo.DataValueField = "id";
            listFiltroTipo.DataBind();
        }

        ICollection createDataSourceFiltroTipoHab()
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

        #region ordenar
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
            dt.Rows.Add(createRow("E-Mail", "E-Mail", dt));
            dt.Rows.Add(createRow("Teléfono", "Teléfono", dt));
            dt.Rows.Add(createRow("Fecha de Nacimiento", "Fecha de Nacimiento", dt));
            dt.Rows.Add(createRow("Cedula", "Cedula", dt));
            dt.Rows.Add(createRow("Vencimiento de libreta", "Vencimiento de libreta", dt));
            dt.Rows.Add(createRow("Disponible", "Disponible", dt));
           
            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion



        #endregion


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }
        protected void listFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void listOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }
        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltroTipo.SelectedValue;

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltroTipo.SelectedValue;
     
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
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

                            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                            Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula, disponible, txtFchVenc);
                            if (Web.altaCamionero(unCamionero))
                            {
                                limpiar();
                                lblMensajes.Text = "Camionero dado de alta con éxito.";
                                listarPagina();
                            }
                            else
                            {
                                lblMensajes.Text = "Ya existe un Camionero con estos datos. Estos son los posibles datos repetidos (Email / Teléfono / Cedula).";

                            }
                        }
                        else
                        {
                            lblMensajes.Text = "Falta seleccionar la disponibilidad.";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Seleccione una fecha de vencimiento mayor a hoy.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Seleccione una fecha de nacimiento menor a hoy.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
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
                    if(Web.bajaCamionero(id))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el camionero.";
                        txtBuscar.Text = "";
                    listarPagina();
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido borrar el camionero.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El camionero no existe.";
                }
         
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
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
    }
}