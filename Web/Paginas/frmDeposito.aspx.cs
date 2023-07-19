using Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmDeposito : System.Web.UI.Page
    {
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
            lstDeposito.DataSource = null;
            lstDeposito.DataSource = Web.listDeps();

            lstDeposito.DataBind();

        }

        private void limpiar()
        {
            txtId.Text = "";
            txtCapacidad.Text = "";
            txtCondiciones.Text = "";
            txtTemperatura.Text = "";
            txtUbicacion.Text = "";
            lstDeposito.SelectedIndex = -1;
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
                    lblMensajes.Text = "Deposito dado de alta con exito.";
                    listar();
                    limpiar();
                }
                else
                {
                    lblMensajes.Text = "No se pudo dar de alta el Deposito.";
                    limpiar();
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos";
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = Web.buscarDeps(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unDeposito != null)
                {
                    if (Web.bajaDeps(int.Parse(txtId.Text)))
                    {
                        lblMensajes.Text = "Se ha borrado el Deposito.";
                        limpiar();
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "Error. No se pubo borrar el Deposito.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Error. El Deposito no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan selecionar un Deposito de la lista";

            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
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
                    lblMensajes.Text = "Deposito modificado con exito.";
                    listar();
                    limpiar();
                }
                else
                {
                    lblMensajes.Text = "No se pudo modificar el Deposito";
                    limpiar();
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

        private bool faltaIdDep()
        {
            if (lstDeposito.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void lstDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdDep())
            {

                string linea = this.lstDeposito.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                cargarDep(id);
                lstDeposito.SelectedIndex = -1;
            }
            else
                lblMensajes.Text = "Debe seleccionar un camionero de la lista";
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
    }
}