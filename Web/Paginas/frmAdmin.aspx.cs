using System;

using Clases;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Paginas
{
    public partial class frmAdmin : System.Web.UI.Page
    {


        private void listar()
        {

            //   ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstAdmin.DataSource = null;
            //  lstAdmin.DataSource = Web.lstAdmin();
            lstAdmin.DataBind();
        }

        private void limpiar()
        {
            
            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            Calendar1.SelectedDate = DateTime.Now;
            txtUser.Text = "";
            txtPass.Text = "";
            txtTipoAdm.Text = "";

        }
        private bool faltanDatos()
        {
            if (txtId.Text == "" || txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtPass.Text == "" || txtTipoAdm.Text =="")
            {
                return true;
            }
            else { return false; }


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
              

                string tAdm = txtTipoAdm.Text;
                string user = txtUser.Text;
                string pass = txtPass.Text;



                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin unAdmin = new Admin(id,nombre, apellido, email, tele, txtFc, pass, user, tAdm);
                if (Web.altaAdmin(unAdmin))
                {
                    lblMensajes.Text = "Admin dado de alta con exito.";
                    listar();
                    limpiar();
                }
                else
                {
                    lblMensajes.Text = "No se pudo dar de alta el Administrador";
                    limpiar();
                }
            }
            else
            {
                lblMensajes.Text = "Faltan Datos";
            }
        }






        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }


        

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

    
    }
}
