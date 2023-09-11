using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Clientes
{
    public partial class iniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(System.Web.HttpContext.Current.Session["CliReg"] != null)
            {
                lblMensajes.Text = "Se ha registrado con éxito";
            }
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Clientes/RegCliente");
        }



        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            ControladoraWeb web = new ControladoraWeb();
            string user = HttpUtility.HtmlEncode(txtUser.Text);


            string pass = HttpUtility.HtmlEncode(txtPass.Text);

            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");
            if (web.iniciarSesionCli(user, hashedPassword) > 0 && web.iniciarSesionAdm(user, hashedPassword)==0)
            {
                lblMensajes.Text = "Sesión iniciada como cliente ";
                System.Web.HttpContext.Current.Session["ClienteIniciado"] = web.iniciarSesionCli(user, hashedPassword);

                Response.Redirect("/Paginas/Nav/frmInicio");
            

            }
            if (web.iniciarSesionAdm(user, hashedPassword) > 0 && web.iniciarSesionCli(user, hashedPassword) == 0)
            {
                lblMensajes.Text = "Sesión iniciada como Administrador ";
            }
           if (web.iniciarSesionAdm(user, hashedPassword) == 0 && web.iniciarSesionCli(user, hashedPassword) == 0)
            {
                lblMensajes.Text = "No se pudo iniciar sesión";
            }

           
        }
    }
}