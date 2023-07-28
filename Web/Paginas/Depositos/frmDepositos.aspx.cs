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
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstDeposito.DataSource = null;
            lstDeposito.DataSource = Web.listDeps();
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

        protected void lstDeposito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdDep())
            {

                string linea = this.lstDeposito.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                cargarDep(id);
                
            }
            else
            {
                lblMensajes.Text = "Debe seleccionar un deposito de la lista.";
            }
                
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
                    lblMensajes.Text = "No se encontro ningun deposito.";
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
                    limpiar();
                    lblMensajes.Text = "Deposito dado de alta con exito.";
                    listar();

                }
                else
                {
                    limpiar();
                    lblMensajes.Text = "No se pudo dar de alta el deposito.";

                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }



        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (!txtId.Text.Equals(""))
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = Web.buscarDeps(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unDeposito != null)
                {
                    if (Web.bajaDeps(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado el deposito.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar el deposito.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El deposito no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione un deposito para eliminar. ";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (!txtId.Text.Equals(""))
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
                        listar();
                        lblMensajes.Text = "Deposito modificado con exito.";
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo modificar el deposito";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe seleccionar un deposito.";
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