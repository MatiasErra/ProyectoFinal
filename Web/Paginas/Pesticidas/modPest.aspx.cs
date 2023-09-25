using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Pesticidas
{
    public partial class modPest : System.Web.UI.Page
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
                else
                {
                    Response.Redirect("/Paginas/Nav/frmInicio");
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
            }

            if (System.Web.HttpContext.Current.Session["idPest"] == null)
            {
                Response.Redirect("/Paginas/Pesticidas/frmPesticidas");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idPest"];
                txtId.Text = id.ToString();
                cargarPest(id);
                CargarImpacto();
            }
        }

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idPest"] = null;
        }




        private void cargarPest(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Pesticida pest = Web.buscarPesti(id);

            txtId.Text = pest.IdPesticida.ToString();
            txtNombre.Text = pest.Nombre.ToString();
            txtTipo.Text = pest.Tipo.ToString();

            txtPH.Text = pest.PH.ToString();
            lstImpacto.SelectedValue = pest.Impacto.ToString();


        }

        private void limpiar()
        {
            txtId.Text = "";

            txtNombre.Text = "";
            txtTipo.Text = "";
            lblMensajes.Text = "";
            txtPH.Text = "";
            lstImpacto.SelectedValue = "Seleccionar tipo de impacto";


        }


        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtTipo.Text == "" || txtPH.Text == "")
            {
                return true;
            }
            else { return false; }
        }



        public void CargarImpacto()
        {
            lstImpacto.DataSource = createDataSource();
            lstImpacto.DataTextField = "nombre";
            lstImpacto.DataValueField = "id";
            lstImpacto.DataBind();
        }

        ICollection createDataSource()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(createRow("Seleccionar tipo de impacto", "Seleccionar tipo de impacto", dt));
            dt.Rows.Add(createRow("Alto", "Alto", dt));
            dt.Rows.Add(createRow("Medio", "Medio", dt));
            dt.Rows.Add(createRow("Bajo", "Bajo", dt));

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



        private bool phValid()
        {
            double ph = double.Parse(txtPH.Text.ToString());
            if (ph > -1 && ph < 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!faltanDatos())
                {
                    if (phValid())
                    {


                        if (lstImpacto.SelectedValue.ToString() != "Seleccionar tipo de impacto")
                        {

                            int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                            string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                            string tipo = HttpUtility.HtmlEncode(txtTipo.Text);

                            double pH = double.Parse(HttpUtility.HtmlEncode(txtPH.Text));
                            string impacto = HttpUtility.HtmlEncode(lstImpacto.SelectedValue.ToString());





                            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                            Pesticida pesticida = new Pesticida(id, nombre, tipo, pH, impacto);
                            if (Web.modPesti(pesticida))
                            {
                                limpiar();
                                lblMensajes.Text = "Pesticida modificado con éxito.";
                                limpiarIdSession();
                                if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
                                {
                                    Response.Redirect("/Paginas/Lotes/frmLotesPestis");
                                }
                                else
                                {
                                    System.Web.HttpContext.Current.Session["PestiMod"] = "si";
                                    Response.Redirect("/Paginas/Pesticidas/frmPesticidas");
                                }


                            }
                            else
                            {

                                lblMensajes.Text = "Ya existe un Pesticida con estos datos. Estos son los posibles datos repetidos(Nombre).";

                            }

                        }
                        else
                        {
                            lblMensajes.Text = "Falta seleccionar el tipo de impacto.";
                        }

                    }
                    else
                    {
                        lblMensajes.Text = "El PH debe estar entre 0-14.";
                    }


                }
                else
                {
                    lblMensajes.Text = "Faltan Datos.";
                }
            }
            else
            {
                lblMensajes.Text = "Hay algún carácter no válido o faltante en el formulario";

            }



        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotesPestis");
            }
            else
            {
                Response.Redirect("/Paginas/Pesticidas/frmPesticidas");
            }

        }
    }
}