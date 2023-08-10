using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Fertilizantes
{
    public partial class modFert : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idFert"] == null)
            {
                Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idFert"];
                txtId.Text = id.ToString();
                cargarFert(id);
                CargarImpacto();
            }
        }

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idFert"] = null;
        }




        private void cargarFert(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Fertilizante fer = Web.buscarFerti(id);

            txtId.Text = fer.IdFertilizante.ToString();
            txtNombre.Text = fer.Nombre.ToString();
            txtTipo.Text = fer.Tipo.ToString();

            txtPH.Text = fer.PH.ToString();
            lstImpacto.SelectedValue = fer.Impacto.ToString();


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
            short ph = short.Parse(txtPH.Text.ToString());
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
            if (!faltanDatos())
            {
                if (phValid())
                {


                    if (lstImpacto.SelectedValue.ToString() != "Seleccionar tipo de impacto")
                    {

                        int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                        string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                        string tipo = HttpUtility.HtmlEncode(txtTipo.Text);

                        short pH = short.Parse(HttpUtility.HtmlEncode(txtPH.Text));
                        string impacto = HttpUtility.HtmlEncode(lstImpacto.SelectedValue.ToString());





                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Fertilizante fertilizante = new Fertilizante(id, nombre, tipo, pH, impacto);
                        if (Web.modFerti(fertilizante))
                        {
                            limpiar();
                            lblMensajes.Text = "Fertilizante modificado con éxito.";
                            limpiarIdSession();
                            if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                            {
                                Response.Redirect("/Paginas/Lotes/frmLotesFertis");
                            }
                            else
                            {
                                Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");
                            }





                        }
                        else
                        {

                            lblMensajes.Text = "Ya existe un Fertilizante con estos datos. Estos son los posibles datos repetidos(Nombre).";

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

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotesFertis");
            }
            else
            {
                Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");
            }

        }
    }
}