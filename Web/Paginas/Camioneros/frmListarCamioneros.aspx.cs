using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;

namespace Web.Paginas.Camioneros
{
    public partial class frmListarCamioneros : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/AGlobal.Master";
        }
        
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
            lstCamionero.DataSource = null;
            lstCamionero.DataSource = Web.listCamionero();

            lstCamionero.DataBind();
        }

        protected void lstCamionero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Camionero> camioneros = Web.listCamionero();
            lstCamionero.DataSource = null;
            string value = txtBuscar.Text;
            /*string value = selectBuscar.SelectedValue;
            if(value == "nombre")
            {
                foreach(Camionero unCamionero in camioneros)
                {
                    if(unCamionero.Nombre == txtBuscar.Text)
                    {
                        lstCamionero.DataSource = unCamionero;
                    }
                }
            }*/
            List<Camionero> camioneroslst = new List<Camionero>();
            foreach (Camionero unCamionero in camioneros)
            {
                
                if(unCamionero.Nombre == value || unCamionero.Apellido == value || unCamionero.Email == value || unCamionero.Telefono == value || unCamionero.Cedula == value)
                {
                    camioneroslst.Add(unCamionero);
                }
            }
            lstCamionero.DataSource = camioneroslst;
            lstCamionero.DataBind();
        }
    }
}