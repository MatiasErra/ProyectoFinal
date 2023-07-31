using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Granjass
{
    public partial class frmGranjas : System.Web.UI.Page
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
            lstGranja.DataSource = null;
            lstGranja.DataSource = Web.listGranjas();
            lstGranja.DataBind();
            lstCliente.DataSource = null;
            lstCliente.DataSource = Web.lstCli();
            lstCliente.DataBind();
        }


        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtUbicacion.Text == "" || txtIdCliente.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool faltaIdGranja()
        {
            if (lstGranja.SelectedIndex == -1)
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
            

            txtNombre.Text = "";
            txtUbicacion.Text = "";
            txtIdCliente.Text = "";
            lstGranja.SelectedIndex = -1;
            lstCliente.SelectedIndex = -1;
        }


        private void cargarGranja(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja granja = Web.buscarGranja(id);
            txtId.Text = granja.IdGranja.ToString();
            txtNombre.Text = granja.Nombre;
            txtUbicacion.Text = granja.Ubicacion;
            txtIdCliente.Text = granja.IdCliente.ToString();

        }

        protected void lstGranja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdGranja())
            {
                string linea = this.lstGranja.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                cargarGranja(id);
                lstGranja.SelectedIndex = -1;
            }
            else
            {
                lblMensajes.Text = "Debe seleccionar una granja de la lista.";
            }
        }

        protected void lstCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCliente.SelectedIndex != -1)
            {
                string linea = this.lstCliente.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                txtIdCliente.Text = partes[0];
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
            List<Granja> lstGranjas = Web.listIdGranjas();
            foreach (Granja granja in lstGranjas)
            {
                if (granja.IdGranja.Equals(intGuid))
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
            List<Granja> granjas = Web.buscarVarGranjas(value);
            lstGranja.DataSource = null;



            if (granjas.Count > 0)
            {
                lstGranja.Visible = true;
                lblMensajes.Text = "";
                lstGranja.DataSource = granjas;
                lstGranja.DataBind();
            }
            else
            {
                lstGranja.Visible = false;
                lblMensajes.Text = "No se encontro ninguna granja.";
            }
        }

        /*private void buscarCliente()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
            List<Cliente> clientes = Web.buscarVarCli(value);
            lstCliente.DataSource = null;

            if (clientes.Count > 0)
            {
                lstCliente.Visible = true;
                lstCliente.DataSource = clientes;
                lstCliente.DataBind();
            }
            else
            {
                lstCliente.Visible = false;
            }
        }*/

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            //buscarCliente();
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = GenerateUniqueId();
                string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                int idCliente = int.Parse(HttpUtility.HtmlEncode(txtIdCliente.Text));

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Granja unaGranja = new Granja(id, nombre, ubicacion, idCliente);
                if (Web.altaGranja(unaGranja))
                {
                    limpiar();
                    lblMensajes.Text = "Granja dada de alta con exito.";
                    listar();

                }
                else
                {
                    limpiar();
                    lblMensajes.Text = "No se pudo dar de alta la granja.";

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

                //if existe producen
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Granja unaGranja = Web.buscarGranja(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if (unaGranja != null)
                {
                    if (Web.bajaGranja(int.Parse(txtId.Text)))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha borrado la granja.";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar la granja.";
                    }
                }
                else
                {
                    lblMensajes.Text = "La granja no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Seleccione una granja para eliminar. ";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (!txtId.Text.Equals(""))
                {
                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                    int idCliente = int.Parse(HttpUtility.HtmlEncode(txtIdCliente.Text));

                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Granja unaGraja = new Granja(id, nombre, ubicacion, idCliente);
                    if (Web.modGranja(unaGraja))
                    {
                        limpiar();
                        lblMensajes.Text = "Granja modificada con exito.";
                        listar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo modificar la granja";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe seleccionar una granja.";
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