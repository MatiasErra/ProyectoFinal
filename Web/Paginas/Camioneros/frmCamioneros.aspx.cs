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
                limpiar();
                listar();
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
            txtFchManejo.Text = "";
            lstCamionero.SelectedIndex = -1;
            listar();
        }

        private void cargarCam(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero camionero = Web.buscarCamionero(id);
            txtId.Text = camionero.IdPersona.ToString();
            txtNombre.Text = camionero.Nombre;
            txtApell.Text = camionero.Apellido;
            txtEmail.Text = camionero.Email;
            txtFchNac.Text = DateTime.Parse(camionero.FchNacimiento).ToString("yyyy-MM-dd");
            txtTel.Text = camionero.Telefono;
            txtCedula.Text = camionero.Cedula;
            txtFchManejo.Text = DateTime.Parse(camionero.FchManejo).ToString("yyyy-MM-dd");
            lstDisponible.SelectedValue = camionero.Disponible.ToString();
        }

        //protected void lstCamionero_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!faltaIdCam())
        //    {
        //        string linea = this.lstCamionero.SelectedItem.ToString();
        //        string[] partes = linea.Split(' ');
        //        int id = Convert.ToInt32(partes[0]);
        //        cargarCam(id);
        //        lstCamionero.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        lblMensajes.Text = "Debe seleccionar un camionero de la lista";
        //    }
        //}

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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
  
            List<Camionero> camioneros = new List<Camionero>();
            camioneros = Web.buscarVarCamionero(value);
            lstCamionero.DataSource = null;

            if (txtBuscar.Text != "")
            {

                if (camioneros.Count > 0)
                {
                    lstCamionero.Visible = true;
                    lblMensajes.Text = "";
                    lstCamionero.DataSource = camioneros;
                    lstCamionero.DataBind();
                }
                else
                {
                    lstCamionero.Visible = false;
                    lblMensajes.Text = "No se encontro ningun camionero.";
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
                                lblMensajes.Text = "Camionero dado de alta con exito.";
                                listar();
                            }
                            else
                            {
                                lblMensajes.Text = "No se pudo dar de alta el camionero.";

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
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        limpiar();
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