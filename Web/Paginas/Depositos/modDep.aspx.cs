using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Depositos
{
    public partial class modDep : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
            {
                int id = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin admin = Web.buscarAdm(id);

                if (admin.TipoDeAdmin == "Administrador global")
                {
                    this.MasterPageFile = "~/Master/AGlobal.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de productos")
                {
                    this.MasterPageFile = "~/Master/AProductos.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }

            if (System.Web.HttpContext.Current.Session["idDep"] == null)
            {
                Response.Redirect("/Paginas/Depositos/frmDepositos");
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idDep"];
                txtId.Text = id.ToString();
                cargarDep(id);
            }


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

            txtId.Text = "";
            lblMensajes.Text = "";
            txtCapacidad.Text = "";
            txtCondiciones.Text = "";
            txtTemperatura.Text = "";
            txtUbicacion.Text = "";

        }
        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idDep"] = null;
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Depositos/frmDepositos");


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (!txtId.Text.Equals(""))
                {
                    if (Page.IsValid)
                    {
                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                        string capacidad = HttpUtility.HtmlEncode(txtCapacidad.Text);
                        string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                        short temperatura = short.Parse(HttpUtility.HtmlEncode(txtTemperatura.Text));
                        string condiciones = HttpUtility.HtmlEncode(txtCondiciones.Text);

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Deposito unDeposito = new Deposito(id, capacidad, ubicacion, temperatura, condiciones);
                        if (Web.modDeps(unDeposito))
                        {
                            limpiar();
                            lblMensajes.Text = "Depósito modificado con éxito.";

                            limpiarIdSession();

                            System.Web.HttpContext.Current.Session["idDepMod"] = "si";
                            Response.Redirect("/Paginas/Depositos/frmDepositos");
                        }
                        else
                        {

                            lblMensajes.Text = "Ya existe un Depósito con estos datos. Estos son los posibles datos repetidos (Ubicación).";


                        }
                    }

                    else
                    {
                        lblMensajes.Text = "Hay algún caracter no válido o faltante en el formulario";

                    }

                }
                else
                {
                    lblMensajes.Text = "Debe seleccionar un Depósito.";
                
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }


    }
}