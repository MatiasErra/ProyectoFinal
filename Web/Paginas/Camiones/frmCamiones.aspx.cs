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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
              

                if (System.Web.HttpContext.Current.Session["idCamionMod"] != null)
                {
                    lblMensajes.Text = "Camión Modificado";
                    System.Web.HttpContext.Current.Session["idCamionMod"] = null;
                }

                System.Web.HttpContext.Current.Session["idCamionSel"] = null;



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

   
        private bool faltanDatos()
        {
            if (txtModelo.Text == "" || txtMarca.Text == "" || txtCarga.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
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
            txtModelo.Text = "";
            txtBuscar.Text = "";
            txtMarca.Text = "";
            txtCarga.Text = "";

            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
            listFiltroTipo.SelectedValue = "Seleccionar disponibilidad";
            listOrdenarPor.SelectedValue = "Ordenar por";
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
            string b = "";
            string d = "";
            string o = "";
            List<Camion> lstCam = Web.buscarFiltroCam(b, d,o);
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

        private int PagMax()
        {

            return 5;
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
                lblMensajes.Text = "No se encontro ningún Camión.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstCamiones.Visible = false;
            }
            else
            {

                lblMensajes.Text = "";
                modificarPagina();
                lstCamiones.Visible = true;
                lstCamiones.DataSource = null;
                lstCamiones.DataSource = camionesPagina;
                lstCamiones.DataBind();
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
            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }


        private List<Camion> obtenerCamiones()
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

            List<Camion> camiones = Web.buscarFiltroCam(buscar, disp, ordenar);


            return camiones;
        }







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
                        limpiar();
                        lblMensajes.Text = "Camión dado de alta con éxito.";
                        listarPagina();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta el Camión.";

                    }

                }
                else
                {
                    lblMensajes.Text = "Falta seleccionar la disponibilidad.";
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
            Camion camion = Web.buscarCam(id);
            if (camion != null)
            {
                if (Web.bajaCam(id))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha borrado el Camión.";
                    listarPagina();
                }
                else
                {

                    lblMensajes.Text = "No se ha podido borrar el Camión.";
                }
            }
            else
            {
                lblMensajes.Text = "El Camión no existe.";
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
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
    }



}
