using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;
using Web.Paginas.Productos;

namespace Web.Paginas.Lotes
{
    public partial class frmLotes : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listar();
                
                System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
                System.Web.HttpContext.Current.Session["idProductoSel"] = null;
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
                if (System.Web.HttpContext.Current.Session["LoteAlta"] != null)
                {
                    lblMensajes.Text = "Lote Añadido con éxito";
                    System.Web.HttpContext.Current.Session["LoteAlta"] = null;
                }

                if (System.Web.HttpContext.Current.Session["LoteAlta"] != null)
                {
                    lblMensajes.Text = "Lote Añadido con éxito";
                    System.Web.HttpContext.Current.Session["LoteAlta"] = null;
                }
                if (System.Web.HttpContext.Current.Session["LoteMod"] != null)
                {
                    lblMensajes.Text = "Lote Modificado";
                    System.Web.HttpContext.Current.Session["LoteMod"] = null;
                }
               

            }
        }
        #endregion

        #region Utilidad

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<string[]> lotes = Web.listLotes();
            lstLote.DataSource = null;
            lstLote.DataSource = ObtenerDatos(lotes);
            lstLote.DataBind();

            limpiar();
        }

        public DataTable ObtenerDatos(List<string[]> lotes)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("Precio", typeof(double)),
                new DataColumn("UbicacionDeposito", typeof(string))});

            foreach (string[] unLote in lotes)
            {
                DataRow dr = dt.NewRow();
                dr["NombreGranja"] = unLote[1];
                dr["NombreProducto"] = unLote[3];
                dr["FchProduccion"] = unLote[4];
                dr["Cantidad"] = unLote[5];
                dr["Precio"] = double.Parse(unLote[6]);
                dr["UbicacionDeposito"] = unLote[8];

                dt.Rows.Add(dr);
            }
            return dt;
        }



        private void limpiar()
        {
            lblMensajes.Text = "";
            txtBuscar.Text = "";
        

            lstLote.SelectedIndex = -1;
          

        }

        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text.ToLower();
            List<string[]> lotes = Web.buscarVarLotes(value);
            lstLote.DataSource = null;

            if (txtBuscar.Text != "")
            {
                if (lotes.Count > 0)
                {
                    lstLote.Visible = true;
                    lblMensajes.Text = "";
                    lstLote.DataSource = ObtenerDatos(lotes);
                    lstLote.DataBind();
                }
                else
                {
                    lstLote.Visible = false;
                    lblMensajes.Text = "No se encontró ningún lote.";
                }
            }
            else
            {
                lblMensajes.Text = "Debe ingresar algún dato en el buscador para buscar.";
            }

        }
        #endregion


        #region Botones

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

      

        protected void btnBaja_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            if (unLote != null)
            {
        
                if (Web.bajaLote(nombreGranja, nombreProducto, fchProduccion))
                {
                    listar();
                    lblMensajes.Text = "Se ha borrado el lote.";
                }
                else
                {
                    lblMensajes.Text = "No se ha podido borrar el lote.";
                }
            }
            else
            {
                lblMensajes.Text = "El lote no existe.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/modLote");
        }

        protected void btnVerPestis_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesPestis");
        }

  
        protected void btnVerFertis_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesFertis");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            listar();
        }

        protected void btnAltaLot_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

        }

        #endregion

    }
}