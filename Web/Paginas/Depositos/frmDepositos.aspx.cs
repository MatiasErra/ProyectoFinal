using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Depositos
{
    public partial class frmDepositos : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {
                limpiar();
                listar();
            }
        }

        private void listar()
        {  ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Deposito> lst = Web.listDeps();
            lstDeposito.DataSource = lst;
            lstDeposito.DataBind();

        }


        private bool faltanDatos()
        {
            if (txtCapacidad.Text == "" || txtCondiciones.Text == "" || txtTemperatura.Text == "" || txtUbicacion.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";

            txtCapacidad.Text = "";
            txtCondiciones.Text = "";
            txtTemperatura.Text = "";
            txtUbicacion.Text = "";
            lstDeposito.SelectedIndex = -1;
            lblMensajes.Text = "";
            listar();
        }


        private void cargarDep(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Deposito deposito = Web.buscarDeps(id);
            txtId.Text = deposito.IdDeposito.ToString();
            txtCapacidad.Text = deposito.Capacidad;
            txtCondiciones.Text = deposito.Condiciones;
            txtTemperatura.Text = deposito.Temperatura.ToString();
            txtUbicacion.Text = deposito.Ubicacion;
        }

        protected void Select_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);
     
                cargarDep(id);

        

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
            List<Deposito> lstDep = Web.listIdDeps();
            foreach (Deposito deposito in lstDep)
            {
                if (deposito.IdDeposito.Equals(intGuid))
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

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
            List<Deposito> depositos = new List<Deposito>();
            depositos = Web.buscarVarDeps(value);
            lstDeposito.DataSource = null;

            if (txtBuscar.Text != "")
            {

                if (depositos.Count > 0)
                {
                    lstDeposito.Visible = true;
                    lblMensajes.Text = "";
                    lstDeposito.DataSource = depositos;
                    lstDeposito.DataBind();
                }
                else
                {
                    lstDeposito.Visible = false;
                    lblMensajes.Text = "No se encontro ningun Depósito.";
                }
            }
            else
            {
                lblMensajes.Text = "Debe poner algun dato en el buscador.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = GenerateUniqueId();
                string capacidad = HttpUtility.HtmlEncode(txtCapacidad.Text);
                string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                short temperatura = short.Parse(HttpUtility.HtmlEncode(txtTemperatura.Text));
                string condiciones = HttpUtility.HtmlEncode(txtCondiciones.Text);

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = new Deposito(id, capacidad, ubicacion, temperatura, condiciones);
                if (Web.altaDeps(unDeposito))
                {
                    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                    {
                        System.Web.HttpContext.Current.Session["idDepositoSel"] = unDeposito.IdDeposito.ToString();
                        Response.Redirect("/Paginas/Lotes/frmLotes");
                    }
                    else
                    {
                         limpiar();
                    lblMensajes.Text = "Depósito dado de alta con éxito.";
                    listar();

                    }
                   
                }
                else
                {
                   
                    lblMensajes.Text = "Ya existe un Depósito con estos datos. Estos son los posibles datos repetidos (Ubicación).";

                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }



        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = Web.buscarDeps(id);
                if (unDeposito != null)
                {
                    if (Web.bajaDeps(id))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha eliminado el Depósito.";
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se ha podido eliminar el Depósito.";
                    }
           
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idDep"] = id;
            Response.Redirect("/Paginas/Depositos/modDep");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}