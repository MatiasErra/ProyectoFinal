using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas;
using Web.Paginas.Catalogo;

namespace Web
{
    public partial class MCliente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            int iduser = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
            Cliente cli = Web.buscarCli(iduser);
            returNombre(cli.User);
    
        }

        public string nombre;
        public void returNombre(string value)
        {
            nombre = value;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Paginas/Nav/frmInicio");
        }






    }
}