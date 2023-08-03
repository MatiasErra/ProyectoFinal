﻿using System;
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

        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Clientes/RegCliente");
        }



        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            ControladoraWeb web = new ControladoraWeb();
            string nombre = HttpUtility.HtmlEncode(txtUser.Text);


            string pass = HttpUtility.HtmlEncode(txtPass.Text);

            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");
            if (web.iniciarSesionCli(nombre, hashedPassword) > 0 && web.iniciarSesionAdm(nombre, hashedPassword)==0)
            {
                lblMensajes.Text = "Sesión iniciada como cliente ";
            }
            if (web.iniciarSesionAdm(nombre, hashedPassword) > 0 && web.iniciarSesionCli(nombre, hashedPassword) == 0)
            {
                lblMensajes.Text = "Sesión iniciada como Administrador ";
            }
           if (web.iniciarSesionAdm(nombre, hashedPassword) == 0 && web.iniciarSesionCli(nombre, hashedPassword) == 0)
            {
                lblMensajes.Text = "No se pudo iniciar sesión";
            }

           
        }
    }
}