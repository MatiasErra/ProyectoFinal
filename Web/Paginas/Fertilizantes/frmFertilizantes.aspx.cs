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
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }


             
                CargarImpacto();
                if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                {
                    btnVolverFerti.Visible = true;
                }
                if(System.Web.HttpContext.Current.Session["FertiMod"] != null)
                {
                    lblMensajes.Text = "Fertilizante modificado";
                    System.Web.HttpContext.Current.Session["FertiMod"] = null;
                }
                System.Web.HttpContext.Current.Session["idFert"] = null;



                CargarListFiltroTipo();
                CargarListOrdenarPor();

                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;
                }

                if (System.Web.HttpContext.Current.Session["FiltroTipo"] != null)
                {
                    listFiltro.SelectedValue = System.Web.HttpContext.Current.Session["FiltroTipo"].ToString();
                    System.Web.HttpContext.Current.Session["FiltroTipo"] = null;
                }

                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;
                }
                listarPagina();

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
            listOrdenarPor.SelectedValue = "Ordenar por";
            listFiltro.SelectedValue = "Seleccionar tipo de impacto";
            lstFert.SelectedIndex = -1;
            lblPaginaAct.Text = "1";
            listarPagina();
        }
 

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtTipo.Text == "" || txtPH.Text == "")
            {
                return true;
            }
            else { return false; }
        }


        #region filtro
        public void CargarListFiltroTipo()
        {
            listFiltro.DataSource = null;
            listFiltro.DataSource = createDataSourceFiltroTipo();
            listFiltro.DataTextField = "nombre";
            listFiltro.DataValueField = "id";
            listFiltro.DataBind();
        }

        ICollection createDataSourceFiltroTipo()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccionar tipo de impacto", "Seleccionar tipo de impacto", dt));
            dt.Rows.Add(createRow("Alto", "Alto", dt));
            dt.Rows.Add(createRow("Medio", "Medio", dt));
            dt.Rows.Add(createRow("Bajo", "Bajo", dt));




            DataView dv = new DataView(dt);
            return dv;
        }


        #endregion

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
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));
            dt.Rows.Add(createRow("PH", "PH", dt));
            dt.Rows.Add(createRow("Impacto", "Impacto", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion
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
            string b = "";
            string d = "";
            string o = "";
            List<Fertilizante> lstFer = Web.buscarFertilizanteFiltro(b,d,o);
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

        private int PagMax()
        {

            return 2;
        }

        private List<Fertilizante> obtenerFertilizantes()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;
            string impact = "";
            string ordenar = "";
            if (listFiltro.SelectedValue != "Seleccionar tipo de impacto")
            {
                impact = listFiltro.SelectedValue;
            }

            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }

            List<Fertilizante> fertilizantes = Web.buscarFertilizanteFiltro(buscar, impact, ordenar);

            return fertilizantes;
        }

        private void listarPagina()
        {
            List<Fertilizante> fertilizantes = obtenerFertilizantes();
            List<Fertilizante> fertilizantesPagina = new List<Fertilizante>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Fertilizante unFertilizante in fertilizantes)
            {
                if (fertilizantesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    fertilizantesPagina.Add(unFertilizante);
                }

                cont++;
            }

            if (fertilizantesPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún fertilizante.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstFert.Visible = false;
            }
            else
            {
               
                lblMensajes.Text = "";
                modificarPagina();
                lstFert.Visible = true;
                lstFert.DataSource = null;
                lstFert.DataSource = fertilizantesPagina;
                lstFert.DataBind();
            }


        }

        private void modificarPagina()
        {
            List<Fertilizante> fertilizantes = obtenerFertilizantes();
            double pxp = PagMax();
            double count = fertilizantes.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();
            lblPaginaAct.Visible = true;
            lblPaginaSig.Visible = true;
            lblPaginaAnt.Visible = true;
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

        #endregion



        protected void btnVolverFerti_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmLotesFertis");
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

                        double pH = double.Parse(HttpUtility.HtmlEncode(txtPH.Text));
                        string impacto = HttpUtility.HtmlEncode(lstImpacto.SelectedValue.ToString());





                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Fertilizante fertilizante = new Fertilizante(id, nombre, tipo, pH, impacto);
                        if (Web.altaFerti(fertilizante))
                        {
                            if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                            {
                                System.Web.HttpContext.Current.Session["idFertilizanteSel"] = fertilizante.IdFertilizante.ToString();
                                Response.Redirect("/Paginas/Lotes/frmLotesFertis");
                            }
                            else
                            {
                                limpiar();
                                lblMensajes.Text = "Fertilizante dado de alta con éxito.";
                                listarPagina();
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
                    listarPagina();
                }
                else
                {

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
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltro.SelectedValue;

            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;
            System.Web.HttpContext.Current.Session["FiltroTipo"] = listFiltro.SelectedValue;

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

    }
}