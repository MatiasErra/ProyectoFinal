using Clases;
using Controladoras;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmCamionero : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listar();
                
                Calendar1.SelectedDate = DateTime.Today;
                CalendarManejo.SelectedDate = DateTime.Today;
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstCamionero.DataSource = null;
            lstCamionero.DataSource = Web.listCamionero();

            lstCamionero.DataBind();
            cargarDisponible();

        }

        private void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            Calendar1.SelectedDate = DateTime.Today;
            txtCedula.Text = "";
            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
            CalendarManejo.SelectedDate = DateTime.Today;
            lstCamionero.SelectedIndex = -1;
        }

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == "" 
                || txtCedula.Text == "" )
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        private bool faltaIdCam()
        {
            if (lstCamionero.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        protected void lstCamionero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdCam())
            {

                string linea = this.lstCamionero.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                cargarCam(id);
                lstCamionero.SelectedIndex = -1;
            }
            else
                lblMensajes.Text = "Debe seleccionar un camionero de la lista";


        }

        private void cargarCam(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero camionero = Web.buscarCamionero(id);
            txtId.Text = camionero.IdPersona.ToString();
            txtNombre.Text = camionero.Nombre;
            txtApell.Text = camionero.Apellido;
            txtEmail.Text = camionero.Email;
            txtTel.Text = camionero.Telefono;
            Calendar1.SelectedDate = Convert.ToDateTime(camionero.FchNacimiento);
            txtCedula.Text = camionero.Cedula;
            CalendarManejo.SelectedDate = Convert.ToDateTime(camionero.FchManejo);
            lstDisponible.SelectedValue = camionero.Disponible.ToString();
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


        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
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


        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                {
                    int id = GenerateUniqueId();
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string cedula = HttpUtility.HtmlEncode(txtCedula.Text);
                    string disponible = HttpUtility.HtmlEncode(lstDisponible.SelectedValue.ToString());
                    string txtFchManejo = HttpUtility.HtmlEncode(CalendarManejo.SelectedDate.ToShortDateString());

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula, disponible, txtFchManejo);
                    if (Web.altaCamionero(unCamionero))
                    {
                        lblMensajes.Text = "Camionero dado de alta con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta el Camionero.";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Falta Seleccionar la disponibilidad";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos";
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Camionero unCamionero = Web.buscarCamionero(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unCamionero != null)
                {
                    if (Web.bajaCamionero(int.Parse(txtId.Text)))
                    {
                        lblMensajes.Text = "Se ha borrado el Camionero.";
                        limpiar();
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "Error. No se pubo borrar el Camionero.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Error. El Camionero no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan selecionar un Caminero de la lista";

            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                {
                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string cedula = HttpUtility.HtmlEncode(txtCedula.Text);
                    string disponible = HttpUtility.HtmlEncode(lstDisponible.SelectedValue.ToString());
                    string txtFchManejo = HttpUtility.HtmlEncode(CalendarManejo.SelectedDate.ToShortDateString());

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula, disponible, txtFchManejo);
                        if (Web.modCamionero(unCamionero))
                        {
                            lblMensajes.Text = "Camionero modificado con exito.";
                            listar();
                            limpiar();
                        }
                        else
                        {
                            lblMensajes.Text = "No se pudo modificar el Camionero";
                            limpiar();
                        }
                        }
                else
                {
                    lblMensajes.Text = "Falta Seleccionar la disponibilidad";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

     
    }
}