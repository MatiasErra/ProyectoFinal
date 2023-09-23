using Clases;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Clientes
{
    public partial class frmModificarCuenta : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {



            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] == null)
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }
            else
            {
                this.MasterPageFile = "~/Master/MCliente.Master";
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        #endregion

        #region Utilidad

        private void cargarDatos()
        {
            int Idusuario = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
          Cliente cliente = Web.buscarCli(Idusuario);
            txtId.Text = cliente.IdPersona.ToString();
            txtNombre.Text = cliente.Nombre;
            txtApell.Text = cliente.Apellido;
            txtEmail.Text = cliente.Email;
            txtTel.Text = cliente.Telefono;
            txtUser.Text = cliente.User;
            txtDir.Text = cliente.Direccion;
            txtFchNac.Text = DateTime.Parse(cliente.FchNacimiento).ToString("yyyy-MM-dd");
        }

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtDir.Text == "" || txtFchNac.Text == "")


            {
                return true;
            }
            else { return false; }

        }

        private bool cambioContraseña()
        {
            if (txtPassActual.Text == "" || txtPassNueva.Text == "")
            {
                return false;
            }
            return true;
        }

        private bool mismaContraseña(string pass)
        {
            int Idusuario = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Cliente cliente = Web.buscarCli(Idusuario);
            string actual = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");
            if (actual == cliente.Contrasena)
            {
                return true;
            }
            return false;
        }

        private int combrobarCambioContraseña()
        {
            int num = 0;
            if (cambioContraseña())
            {
                num = 1;
                if (mismaContraseña(HttpUtility.HtmlEncode(txtPassActual.Text)))
                {
                    num = 2;
                }
            }
            return num;
        }

        #endregion

        #region Botones

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int num = combrobarCambioContraseña();
                if(num == 0 || num == 2)
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    int id = int.Parse(HttpUtility.HtmlEncode(txtId.Text));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(txtFchNac.Text);
                    string user = HttpUtility.HtmlEncode(txtUser.Text);
                    string pass = "";
                    if(num == 2)
                    {
                        pass = HttpUtility.HtmlEncode(txtPassNueva.Text);
                        pass = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");
                    }

                    string dirr = HttpUtility.HtmlEncode(txtDir.Text);

                    Cliente unCli = new Cliente(id, nombre, apellido, email, tele, txtFc, user, pass, dirr);
                    if (Web.modificarCli(unCli))
                    {
                        lblMensajes.Text = "Se modificó su cuenta con éxito.";
                    }
                    else
                    {
                        lblMensajes.Text = "Ocurrió un error al modificar su cuenta.";
                    }
                }
                else
                {
                    lblMensajes.Text = "La contraseña actual no es igual a la contraseña registrada.";
                }
                

            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
}

protected void btnAtras_Click(object sender, EventArgs e)
{
    Response.Redirect("/Paginas/Nav/frmInicio");
}

protected void btnCerrarSesion_Click(object sender, EventArgs e)
{
    Session.Remove("ClienteIniciado");
    Response.Redirect("/Paginas/Nav/frmInicio");
}

        #endregion


    }
}