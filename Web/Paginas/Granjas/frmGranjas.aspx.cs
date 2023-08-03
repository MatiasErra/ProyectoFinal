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
            if (!IsPostBack)
            {
                listar();
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstGranja.DataSource = null;
            lstGranja.DataSource = Web.listGranjas();
            lstGranja.DataBind();
            limpiar();
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
            txtBuscarDueño.Text = "";
            lstGranja.SelectedIndex = -1;
            CargarListDueño();
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
            if (txtBuscarDueño.Text == "")
            {
                clientes = Web.lstCli();
            }
            else
            {
                string value = txtBuscarDueño.Text.ToLower();
                clientes = Web.buscarVarCli(value);
                if (clientes.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningun Cliente.";
                }
            }


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
            List<Granja> lstGranjas = Web.listIdGranjas();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            CargarListDueño();
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
                    listar();
                    lblMensajes.Text = "Granja dada de alta con exito.";

                }
                else
                {
                    lblMensajes.Text = "No se pudo dar de alta la granja.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        private bool comprobarProducen(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Produce> producen = Web.listProducen();
            foreach(Produce unProduce in producen)
            {
                if (unProduce.IdGranja.Equals(id))
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
            if (!comprobarProducen(id))
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Granja unaGranja = Web.buscarGranja(id);
                if (unaGranja != null)
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
                    lblMensajes.Text = "La granja no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Esta granja esta asociada a un produce.";
            }          
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idGranjaSel"] = id;
            Response.Redirect("/Paginas/Granjas/modGranja");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

    }
}