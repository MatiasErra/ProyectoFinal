using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;

namespace Web.Paginas
{
    public partial class frmCliente : System.Web.UI.Page


    {
        #region Utilidad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listar();

                Calendar1.SelectedDate = DateTime.Today;
            }
        }


        private void listar()
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstCli.DataSource = null;
            lstCli.DataSource = Web.lstCli();
            lstCli.DataBind();

        }

        protected void lstCli_Init(object sender, EventArgs e)
        {
            listar();
        }




        private void limpiar()
        {

            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            Calendar1.SelectedDate = DateTime.Today;
            txtUser.Text = "";
            txtPass.Text = "";
            txtDirr.Text = "";
            lstCli.SelectedIndex = -1;


        }
        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtDirr.Text == "")


            {
                return true;
            }
            else { return false; }

        }

        private bool faltaIdCli()
        {
            if (lstCli.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        protected void lstCli_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdCli())
            {
                string linea = this.lstCli.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                this.cargarCli(id);
                this.lstCli.SelectedIndex = -1;
            }
            else
            {
                this.lblMensajes.Text = "Debe seleccionar un admin de la lista.";


            }
        }




        private void cargarCli(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Cliente cliente = Web.buscarCli(id);

            txtId.Text = cliente.IdPersona.ToString();
            txtNombre.Text = cliente.Nombre.ToString();
            txtApell.Text = cliente.Apellido.ToString();
            txtEmail.Text = cliente.Email.ToString();
            txtTel.Text = cliente.Telefono.ToString();
            txtUser.Text = cliente.User.ToString();
            Calendar1.SelectedDate = Convert.ToDateTime(cliente.FchNacimiento);
            txtPass.Text = cliente.Contrasena.ToString();
            txtDirr.Text = cliente.Direccion.ToString();
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



        #endregion


        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (txtPass.Text != "")
                {
                    int id = GenerateUniqueId();
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string user = HttpUtility.HtmlEncode(txtUser.Text);
                    string pass = HttpUtility.HtmlEncode(txtPass.Text);
                    string dirr = HttpUtility.HtmlEncode(txtDirr.Text);

                    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");



                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Cliente unCli = new Cliente(id, nombre, apellido, email, tele, txtFc, user, hashedPassword, dirr);
                    if (Web.altaCli(unCli))
                    {
                        lblMensajes.Text = "Cliente dado de alta con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta el Cliente";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Falta la Contraseña";
                }

            }
            else
            {
                lblMensajes.Text = "Faltan Datos";
            }
        }



        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Cliente unCli = Web.buscarCli(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unCli != null)
                {


                    int Id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));

                    if (Web.bajaCli(Id))
                    {
                        lblMensajes.Text = "Cliente eliminado con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo eliminar el Cliente";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Error. El Cliente no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan selecionar un Cliente de la lista";

            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (txtPass.Text == "")
                {
                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string txtDir = HttpUtility.HtmlDecode(txtDirr.Text);
                    string user = HttpUtility.HtmlEncode(txtUser.Text);



                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Cliente unCli = new Cliente();
                    unCli.IdPersona = id;
                    unCli.Nombre = nombre;
                    unCli.Apellido = apellido;
                    unCli.Email = email;
                    unCli.Telefono = tele;
                    unCli.FchNacimiento = txtFc;
                    unCli.Direccion = txtDir;
                    unCli.User = user;

                    if (Web.modificarCli(unCli))
                    {
                        lblMensajes.Text = "Administrador modificado con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo modificar el Administrador.";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "El administrador no puede cambiar la contraseña del usuario";
                }

            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }



        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            limpiar();
        }






    }
}