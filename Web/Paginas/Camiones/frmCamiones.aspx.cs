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
                limpiar();
                listar();

                if (System.Web.HttpContext.Current.Session["idCamionMod"] != null)
                {
                    lblMensajes.Text = "Camión Modificado";
                    System.Web.HttpContext.Current.Session["idCamionMod"] = null;
                }

                System.Web.HttpContext.Current.Session["idCamionSel"] = null;
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstCamiones.Visible = true;
            lstCamiones.DataSource = null;
            lstCamiones.DataSource = Web.lstCam();
            lstCamiones.DataBind();
            cargarDisponible();
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

            listar();
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
            List<Camion> lstCam = Web.lstCam();
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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;

            List<Camion> camiones = new List<Camion>();
            camiones = Web.buscarVarCam(value);
            lstCamiones.DataSource = null;

            if (txtBuscar.Text != "")
            {

                if (camiones.Count > 0)
                {
                    lstCamiones.Visible = true;
                    lblMensajes.Text = "";
                    lstCamiones.DataSource = camiones;
                    lstCamiones.DataBind();
                }
                else
                {
                    lstCamiones.Visible = false;
                    lblMensajes.Text = "No se encontro ningun Camión.";
                }
            }
            else
            {
                lblMensajes.Text = "Debe poner algun dato en el buscador.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
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
                        listar();
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
                    listar();
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
