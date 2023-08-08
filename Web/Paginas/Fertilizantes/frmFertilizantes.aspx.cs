using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Fertilizantes
{
    public partial class frmFertilizante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiar();
                listar();
                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    btnVolver.Visible = true;
                }
            }
        }

        #region Utilidad


        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";
            txtNombre.Text = "";
            txtTipo.Text = "";

            txtPH.Text = "";
            lstImpacto.SelectedValue = "Seleccionar tipo de impacto";
            lstFert.SelectedIndex = -1;
            listar();
        }
        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstFert.DataSource = null;
            lstFert.DataSource = Web.lstFerti();
            CargarImpacto();
            lstFert.DataBind();
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
            List<Fertilizante> lstFer = Web.listIdFert();
            foreach (Fertilizante fertilizante in lstFer)
            {
                if (fertilizante.IdFertilizante.Equals(intGuid))
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
            List<Fertilizante> lstFertil = new List<Fertilizante>();
            lstFertil = Web.buscarVarFerti(value);
            lstFert.DataSource = null;

            if (txtBuscar.Text != "")
            {
                if (lstFertil.Count > 0)
                {
                    lstFert.Visible = true;
                    lblMensajes.Text = "";
                    lstFert.DataSource = lstFertil;
                    lstFert.DataBind();
                }
                else
                {
                    lstFert.Visible = false;
                    lblMensajes.Text = "No se encontro ningun admin.";

                }
            }
            else
            {
                lblMensajes.Text = "Debe poner algun dato en el buscador.";
            }
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


        #endregion


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmLotes");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (phValid())
                {
                    if (lstImpacto.SelectedValue.ToString() != "Seleccionar tipo de impacto")
                    {

                        int id = GenerateUniqueId();
                        string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                        string tipo = HttpUtility.HtmlEncode(txtTipo.Text);

                        short pH = short.Parse(HttpUtility.HtmlEncode(txtPH.Text));
                        string impacto = HttpUtility.HtmlEncode(lstImpacto.SelectedValue.ToString());





                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Fertilizante fertilizante = new Fertilizante(id, nombre, tipo, pH, impacto);
                        if (Web.altaFerti(fertilizante))
                        {
                            if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                            {
                                System.Web.HttpContext.Current.Session["idFertilizanteSel"] = fertilizante.IdFertilizante.ToString();
                                Response.Redirect("/Paginas/Lotes/frmLotes");
                            }
                            if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
                            {
                                System.Web.HttpContext.Current.Session["idFertilizanteSel"] = fertilizante.IdFertilizante.ToString();
                                Response.Redirect("/Paginas/Lotes/modLote");
                             
                            }
                            else
                            {
                                limpiar();
                                lblMensajes.Text = "Fertilizante dado de alta con éxito.";
                                listar();
                            }

                        }
                        else
                        {

                            lblMensajes.Text = "Ya existe un Fertilizante con estos datos. Estos son los posibles datos repetidos (Nombre).";

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

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Fertilizante fertilizante = Web.buscarFerti(id);
            if (fertilizante != null)
            {
                if (Web.bajaFerti(id))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha eliminado el Fertilizante.";
                    listar();
                }
                else
                {
                    limpiar();
                    lblMensajes.Text = "No se ha podido eliminar el Fertilizante.";
                }
            }
            else
            {
                lblMensajes.Text = "El Fertilizante no existe.";
            }


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idFert"] = id;
            Response.Redirect("/Paginas/Fertilizantes/modFert");


        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

    }
}