using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Camiones
{
    public partial class modCamiones : System.Web.UI.Page
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
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
                }
                else
                {
                    Response.Redirect("/Paginas/Nav/frmInicio");
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }

            if (System.Web.HttpContext.Current.Session["idCamionSel"] == null)
            {
                Response.Redirect("/Paginas/Camiones/frmCamiones");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idCamionSel"];
                txtId.Text = id.ToString();
                cargarCamion(id);
                cargarDisponible();
            }
        }

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idCamionSel"] = null;
        }




        private void cargarCamion(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Camion cam = Web.buscarCam(id);

            txtId.Text = cam.IdCamion.ToString();
            txtModelo.Text = cam.Modelo.ToString();
            txtMarca.Text = cam.Marca.ToString();

            txtCarga.Text = cam.Carga.ToString();
            lstDisponible.SelectedValue = cam.Disponible.ToString();


        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtModelo.Text = "";
            
            txtMarca.Text = "";
            txtCarga.Text = "";

            lstDisponible.SelectedValue = "Seleccionar disponibilidad";

        
        }


        private bool faltanDatos()
        {
            if (txtModelo.Text == "" || txtMarca.Text == "" || txtCarga.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void cargarDisponible()
        {
            lstDisponible.DataSource = createDataSource();
            lstDisponible.DataTextField = "nombre";
            lstDisponible.DataValueField = "id";
            lstDisponible.DataBind();
        }

        ICollection createDataSource()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(createRow("Seleccionar disponibilidad", "Seleccionar disponibilidad", dt));
            dt.Rows.Add(createRow("Disponible", "Disponible", dt));
            dt.Rows.Add(createRow("No disponible", "No disponible", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }




        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!faltanDatos())
                {
                    if (lstDisponible.SelectedValue.ToString() != "Seleccionar disponibilidad")
                    {
                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                        string marca = HttpUtility.HtmlEncode(txtMarca.Text);
                        string modelo = HttpUtility.HtmlEncode(txtModelo.Text);
                        double carga = double.Parse(HttpUtility.HtmlEncode(txtCarga.Text));
                        string disponible = HttpUtility.HtmlEncode(lstDisponible.SelectedValue.ToString());



                        int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Camion camion = new Camion(id, marca, modelo, carga, disponible);
                        if (Web.modCam(camion, idAdmin))
                        {
                            limpiar();
                            lblMensajes.Text = "Camión modificado con éxito.";
                            limpiarIdSession();
                            System.Web.HttpContext.Current.Session["idCamionMod"] = "si";

                            Response.Redirect("/Paginas/Camiones/frmCamiones");




                        }
                        else
                        {

                            lblMensajes.Text = "No se pudo modificar el Camión";

                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Seleccione una disponibilidad.";
                    }

                }
                else
                {
                    lblMensajes.Text = "Faltan Datos.";
                }
            }
            else
            {
                lblMensajes.Text = "Hay algún caracter no válido o faltante en el formulario";

            }



        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Camiones/frmCamiones");

        }
    }
}