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
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }

                System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
                System.Web.HttpContext.Current.Session["idProductoSel"] = null;
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
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
                CargarListOrdenarPor();

                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;
                }


                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;
                }
                listarPagina();


            }
        }
        #endregion

        #region Utilidad

        //private void listar()
        //{
        //    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
        //    List<string[]> lotes = Web.listLotes();
        //    lstLote.DataSource = null;
        //    lstLote.DataSource = ObtenerDatos(lotes);
        //    lstLote.DataBind();

        //    limpiar();
        //}

        private int PagMax()
        {

            return 2;
        }



        private List<string[]> obtenerLote()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;

            string ordenar = "";


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }



            List<string[]> Lote = Web.buscarFiltrarLotes(buscar, ordenar);

            return Lote;
        }


        private void listarPagina()
        {
            List<string[]> lotes = obtenerLote();
            List<string[]> LotesPagina = new List<string[]>();

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (string[] unLote in lotes)
            {
                if (LotesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    LotesPagina.Add(unLote);
                }

                cont++;
            }

            if (LotesPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Lote.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstLote.Visible = false;
            }
            else
            {
                lblMensajes.Text = "";
                modificarPagina();
                lstLote.Visible = true;
                lstLote.DataSource = null;
                lstLote.DataSource = ObtenerDatos(LotesPagina);
                lstLote.DataBind();
            }


        }

        private void modificarPagina()
        {
            List<string[]> lotes = obtenerLote();
            double pxp = PagMax();
            double count = lotes.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();

            lblPaginaSig.Visible = true;
            lblPaginaAnt.Visible = true;
            lblPaginaAct.Visible = true;
            if (pagAct == cantPags.ToString())
            {
                lblPaginaSig.Visible = false;
            }
            if (pagAct == "1")
            {
                lblPaginaAnt.Visible = false;
            }
            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
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

        #region Ordenar

        public void CargarListOrdenarPor()
        {
            listOrdenarPor.DataSource = null;
            listOrdenarPor.DataSource = createDataSourceOrdenarPor();
            listOrdenarPor.DataTextField = "nombre";
            listOrdenarPor.DataValueField = "id";
            listOrdenarPor.DataBind();
        }

        ICollection createDataSourceOrdenarPor()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ordenar por", "Ordenar por", dt));
            dt.Rows.Add(createRow("Granja", "Granja", dt));
            dt.Rows.Add(createRow("Producto", "Producto", dt));
            dt.Rows.Add(createRow("Fecha de producción", "Fecha de producción", dt));
            dt.Rows.Add(createRow("Cantidad de  producción", "Cantidad de  producción", dt));
            dt.Rows.Add(createRow("Precio", "Precio", dt));
            dt.Rows.Add(createRow("Depósito", "Depósito", dt));


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

        #endregion


        private void limpiar()
        {
            lblMensajes.Text = "";
            txtBuscar.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";

            lstLote.SelectedIndex = -1;
          

        }

      
        #endregion


        #region Botones

    

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
                    listarPagina();
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

      

        protected void btnAltaLot_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
      

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
        

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }


        protected void listFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        protected void listOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            listarPagina();
        }





        #endregion

    }
}