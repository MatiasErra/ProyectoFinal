using Clases;
using Controladoras;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmCamionero : System.Web.UI.Page
    {
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
            lstCamionero.Items.Clear();
            List<Camionero> listaCamioneros = Web.listarCamioneros();

            foreach (Camionero unCamionero in listaCamioneros)
            {
                lstCamionero.Items.Add(unCamionero.ToString());
            }
            lstCamionero.DataBind();
        }

        private void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            Calendar1.SelectedDate = DateTime.Now;
            txtCedula.Text = "";
            lstCamionero.SelectedIndex = -1;
        }

        private bool faltanDatos()
        {
            if (txtId.Text == "" || txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == "" || txtCedula.Text == "")
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = Convert.ToInt32(txtId.Text);
                string nombre = txtNombre.Text;
                string apellido = txtApell.Text;
                string email = txtEmail.Text;
                string tele = txtTel.Text;
                string txtFc = Calendar1.SelectedDate.ToShortDateString();
                string cedula = txtCedula.Text;


                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula);
                if (Web.altaCamionero(unCamionero))
                {
                    lblMensajes.Text = "Camionero dado de alta con exito.";
                    listar();
                    limpiar();
                }
                else
                {
                    lblMensajes.Text = "No se pudo dar de alta el Camionero";
                    limpiar();
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos";
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camionero unCamionero = Web.buscarCamionero(int.Parse(txtId.Text));
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = Convert.ToInt32(txtId.Text);
                string nombre = txtNombre.Text;
                string apellido = txtApell.Text;
                string email = txtEmail.Text;
                string tele = txtTel.Text;
                string txtFc = Calendar1.SelectedDate.ToLongDateString();
                string cedula = txtCedula.Text;

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Camionero unCamionero = new Camionero(id, nombre, apellido, email, tele, txtFc, cedula);
                if (Web.modificarCamionero(unCamionero))
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
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void lstCamionero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstCamionero.SelectedIndex > -1)
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                string linea = this.lstCamionero.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = int.Parse(partes[0].ToString());
                Camionero camionero = Web.buscarCamionero(id);

                txtId.Text = camionero.IdPersona.ToString();
                txtNombre.Text = camionero.Nombre;
                txtApell.Text = camionero.Apellido;
                txtEmail.Text = camionero.Email;
                txtTel.Text = camionero.Telefono;
                Calendar1.SelectedDate = DateTime.Parse(camionero.FchNacimiento);
                txtCedula.Text = camionero.Cedula;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}