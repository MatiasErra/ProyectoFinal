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
                else
                {
                    Response.Redirect("/Paginas/Nav/frmInicio");
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }

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


                if (System.Web.HttpContext.Current.Session["GranjaDatosMod"] != null)
                {
                    cargarDatos();
                }
            }
        }

        #region Guardar y cargar datos
        private void guardarDatos()
        {


            System.Web.HttpContext.Current.Session["Ubicacion"] = txtUbicacion.Text;
            System.Web.HttpContext.Current.Session["Nombre"] = txtNombre.Text;

        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["GranjaDatosMod"] = null;



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
            Cliente cli = new Cliente(0, "", "", "", "", "", "", "", "");
            clientes = Web.buscarCliFiltro(cli, "1000-01-01", "3000-12-30", "");


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
        protected void btnBuscarDueño_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["GranjaDatosMod"] = "Si";
            guardarDatos();
            Response.Redirect("/Paginas/Clientes/frmListarClientes");
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