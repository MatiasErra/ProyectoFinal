using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Granjas
{
    public partial class modGranja : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idGranjaSelMod"] == null)
            {
                Response.Redirect("/Paginas/Granjas/frmGranjas");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idGranjaSelMod"];
                txtId.Text = id.ToString();
                cargarGranja(id);
                CargarListDueño();
            }
        }


        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idGranjaSelMod"] = null;
        }

        private void cargarGranja(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja granja = Web.buscarGranja(id);
            txtId.Text = granja.IdGranja.ToString();
            txtNombre.Text = granja.Nombre;
            txtUbicacion.Text = granja.Ubicacion;
            listDueño.SelectedValue = granja.IdCliente.ToString();
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
            txtNombre.Text = "";
            txtUbicacion.Text = "";
            txtBuscarDueño.Text = "";

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
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            if (txtBuscarDueño.Text == "")
            {
                clientes = Web.lstCli();
                dt.Rows.Add(createRow("Seleccione un Dueño", "Seleccione un Dueño", dt));
            }
            else
            {
                string value = txtBuscarDueño.Text.ToLower();
                clientes = Web.buscarVarCli(value);
               
            }
            if (clientes.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningun Cliente.";
            }
            else
            {
                cargarDueños(clientes, dt);

            }

     
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
        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            CargarListDueño();
        
            }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Granjas/frmGranjas");


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!faltanDatos())
                {
                    if (!txtId.Text.Equals(""))
                    {
                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                        string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                        string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                        int idCliente = int.Parse(HttpUtility.HtmlEncode(listDueño.SelectedValue));

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Granja unaGraja = new Granja(id, nombre, ubicacion, idCliente);
                        if (Web.modGranja(unaGraja))
                        {
                            limpiar();
                            lblMensajes.Text = "Granja modificada con exito.";
                            System.Web.HttpContext.Current.Session["GranjaMod"] = "si";
                            limpiarIdSession();
                            Response.Redirect("/Paginas/Granjas/frmGranjas");
                        }
                        else
                        {
                            lblMensajes.Text = "Ya existe una Granja con estos datos. Estos son los posibles datos repetidos (Ubicación).";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Debe seleccionar una granja.";
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