using Clases;
using System;
using System.Collections.Generic;
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

        }

        private void listar()
        {
            lstCamionero.DataSource = null;
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


    }
}