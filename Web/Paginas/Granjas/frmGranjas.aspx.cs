using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Granjass
{
    public partial class frmGranjas : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = null;
            if (!IsPostBack)
            {
          
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    btnVolver.Visible = true;
                    lstGranja.Visible = false;
                    lstGranjaSelect.Visible = true;
                }
                if (System.Web.HttpContext.Current.Session["GranjaMod"] != null)
                {
                    lblMensajes.Text = "Granja Modificada";
                    System.Web.HttpContext.Current.Session["GranjaMod"] = null;
                }
                if (System.Web.HttpContext.Current.Session["GranjaDatosFrm"] != null)
                {
                    cargarDatos();
                }

                listar();




            }
        }

        #region Guardar y cargar datos

        private void guardarDatos()
        {


            System.Web.HttpContext.Current.Session["GranjaDatosFrm"] = "Si";

            System.Web.HttpContext.Current.Session["Ubicacion"] = txtUbicacion.Text;
            System.Web.HttpContext.Current.Session["Nombre"] = txtNombre.Text;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["GranjaDatosFrm"] = null;



            txtUbicacion.Text = System.Web.HttpContext.Current.Session["Ubicacion"].ToString();
            System.Web.HttpContext.Current.Session["Ubicacion"] = null;

            txtNombre.Text = System.Web.HttpContext.Current.Session["Nombre"].ToString();
            System.Web.HttpContext.Current.Session["Nombre"] = null;

            if (System.Web.HttpContext.Current.Session["DuenoSelected"] != null)
            {
                listDueño.SelectedValue = System.Web.HttpContext.Current.Session["DuenoSelected"].ToString();
                System.Web.HttpContext.Current.Session["DuenoSelected"] = null;
            }





        }


        #endregion


        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Granja> granjas = Web.listGranjas();
            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                lstGranjaSelect.Visible = true;
                lstGranjaSelect.DataSource = null;
           
                lstGranjaSelect.DataSource = ObtenerGranjas(granjas);
                lstGranjaSelect.DataBind();
            
            }
            else
            {
                lstGranja.Visible = true;
                lstGranja.DataSource = null;
                lstGranja.DataSource = ObtenerGranjas(granjas);
                lstGranja.DataBind();
               
            }
            CargarListDueño();
        }



        public DataTable ObtenerGranjas(List<Granja> granjas)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {



                new DataColumn("IdGranja", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Ubicacion", typeof(string)),

                new DataColumn("NomDue", typeof(string))});

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
          
            foreach (Granja gran in granjas)
            {
                Cliente cli = Web.buscarCli(gran.IdCliente);

                DataRow dr = dt.NewRow();
                dr["IdGranja"] = gran.IdGranja.ToString();
                dr["Nombre"] = gran.Nombre.ToString();
                dr["Ubicacion"] = gran.Ubicacion.ToString();
                dr["NomDue"] = cli.Nombre.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtUbicacion.Text == "" || listDueño.SelectedValue == "Seleccione un Dueño")
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
            txtBuscar.Text = "";


            txtNombre.Text = "";
            txtUbicacion.Text = "";

            lstGranja.SelectedIndex = -1;
            listar();
        }



        public void CargarListDueño()
        {
            listDueño.DataSource = null;
            listDueño.DataSource = createDataSource();
            listDueño.DataTextField = "nombre";
            listDueño.DataValueField = "id";
            listDueño.DataBind();

        }



        ICollection createDataSource()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Cliente> clientes = new List<Cliente>();

            clientes = Web.lstCli();




            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Dueño", "Seleccione un Dueño", dt));

            cargarDueños(clientes, dt);

            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarDueños(List<Cliente> clientes, DataTable dt)
        {
            foreach (Cliente unCliente in clientes)
            {
                dt.Rows.Add(createRow(unCliente.IdPersona + " " + unCliente.Nombre + " " + unCliente.Apellido, unCliente.IdPersona.ToString(), dt));
            }
        }

        DataRow createRow(String Text, String Value, DataTable dt)
        {


            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

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
            List<Granja> lstGranjas = Web.listGranjas();
            foreach (Granja granja in lstGranjas)
            {
                if (granja.IdGranja.Equals(intGuid))
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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text.ToLower();
            List<Granja> granjas = Web.buscarVarGranjas(value);
            lstGranja.DataSource = null;
            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                if (txtBuscar.Text != "")
                {

                    if (granjas.Count > 0)
                    {
                        lstGranjaSelect.Visible = true;
                        lblMensajes.Text = "";
                        lstGranjaSelect.DataSource = ObtenerGranjas(granjas);
                        lstGranjaSelect.DataBind();
                    }
                    else
                    {
                        lstGranjaSelect.Visible = false;
                        lblMensajes.Text = "No se encontró ninguna granja.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe poner algun dato en el buscador.";
                    listar();
                }

            }
            else
            {
                if (txtBuscar.Text != "")
                {

                    if (granjas.Count > 0)
                    {
                        lstGranja.Visible = true;
                        lblMensajes.Text = "";
                        lstGranja.DataSource = granjas;
                        lstGranja.DataBind();
                    }
                    else
                    {
                        lstGranja.Visible = false;
                        lblMensajes.Text = "No se encontró ninguna granja.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe poner algun dato en el buscador.";
                    listar();
                }
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Clientes/frmListarClientes");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = GenerateUniqueId();
                string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                int idCliente = int.Parse(HttpUtility.HtmlEncode(listDueño.SelectedValue));

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Granja unaGranja = new Granja(id, nombre, ubicacion, idCliente);
                if (Web.altaGranja(unaGranja))
                {
                    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                    {
                        System.Web.HttpContext.Current.Session["idGranjaSel"] = unaGranja.IdGranja.ToString();
                        Response.Redirect("/Paginas/Lotes/frmAltaLotes");
                    }
                    else
                    {
                        listar();
                        lblMensajes.Text = "Granja dada de alta con éxito.";
                    }


                }
                else
                {
                    lblMensajes.Text = "Ya existe una Granja con estos datos. Estos son los posibles datos repetidos (Ubicación).";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }


        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            System.Web.HttpContext.Current.Session["idGranjaSel"] = id;

            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

        }

        private bool comprobarProducen(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<string[]> lotes = Web.listLotes();
            foreach (string[] unLote in lotes)
            {
                if (int.Parse(unLote[0]).Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int id = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja unaGranja = Web.buscarGranja(id);
            if (unaGranja != null)
            {
                if (!comprobarProducen(id))
                {
                    if (Web.bajaGranja(id))
                    {
                        listar();
                        lblMensajes.Text = "Se ha borrado la granja.";
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido borrar la granja.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Esta granja esta asociada a un lote.";
                }
            }
            else
            {
                lblMensajes.Text = "La granja no existe.";
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = id;
            Response.Redirect("/Paginas/Granjas/modGranja");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

    }
}