using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Camioneros
{
    public partial class modCamionero : System.Web.UI.Page
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

            if (System.Web.HttpContext.Current.Session["idCamioneroSel"] == null)
            {
                Response.Redirect("/Paginas/Camioneros/frmCamioneros");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idCamioneroSel"];
                txtId.Text = id.ToString();
                cargarCam(id);
                cargarDisponible();
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
       
            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            txtFchNac.Text = "";
            txtCedula.Text = "";
            lstDisponible.SelectedValue = "Seleccionar disponibilidad";
            txtFchManejo.Text = "";
            lblMensajes.Text = "";

         
        }

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idCamioneroSel"] = null;
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!faltanDatos())
                {
                    if (!txtId.Text.Equals(""))
                    {
                        if (fchNotToday())
                        {
                            if (fchVencNotToday())
                            {
                                if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                                {
                                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
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
                                    if (Web.modCamionero(unCamionero, idAdmin))
                                    {
                                        limpiar();
                                        lblMensajes.Text = "Camionero modificado con éxito.";
                                        limpiarIdSession();
                                        System.Web.HttpContext.Current.Session["idCamioneroMod"] = "si";

                                        Response.Redirect("/Paginas/Camioneros/frmCamioneros");

                                    }
                                    else
                                    {

                                        lblMensajes.Text = "No se pudo modificar el camionero.";
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
                        lblMensajes.Text = "Debe seleccionar un camionero.";
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
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Camioneros/frmCamioneros");
        }
    }
}