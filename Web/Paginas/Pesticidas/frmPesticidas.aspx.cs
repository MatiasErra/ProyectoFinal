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
    public partial class frmPesticida : System.Web.UI.Page
    {

        #region Load

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

                if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
                {
                    btnVolverPesti.Visible = true;
                }
                if(System.Web.HttpContext.Current.Session["PestiMod"] != null)
                {
                    lblMensajes.Text = "Pesticida modificado";
                    System.Web.HttpContext.Current.Session["PestiMod"] = null;
                }
                System.Web.HttpContext.Current.Session["idPest"] = null;

                CargarListBuscar();
                CargarListOrdenarPor();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombrePesticidaBuscar"] != null ? System.Web.HttpContext.Current.Session["nombrePesticidaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombrePesticidaBuscar"] = null;
                txtTipoBuscar.Text = System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"] = null;
                txtPhMenorBuscar.Text = System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"] != null ? System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"] = null;
                txtPhMayorBuscar.Text = System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"] != null ? System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"] = null;

                // Listas
                lstImpactoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"] != null ? System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"].ToString() : "Seleccionar tipo de impacto";
                System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                listarPagina();


            }
        }

        #endregion

        #region Utilidad

        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtNombre.Text = "";
            txtTipo.Text = "";

            txtNombreBuscar.Text = "";
            txtTipoBuscar.Text = "";
            txtPhMenorBuscar.Text = "";
            txtPhMayorBuscar.Text = "";
            lstImpactoBuscar.SelectedValue = "Seleccionar tipo de impacto";

            txtPH.Text = "";
            lstImpacto.SelectedValue = "Seleccionar tipo de impacto";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lstPest.SelectedIndex = -1;
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

            Pesticida pesti = new Pesticida();
            List<Pesticida> lstPest = Web.buscarPesticidaFiltro(pesti, -1, -1, "");
            foreach (Pesticida pesticida in lstPest)
            {
                if (pesticida.IdPesticida.Equals(intGuid))
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

        #region Paginas

        private int PagMax()
        {

            return 2;
        }

        private List<Pesticida> obtenerPesticidas()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Pesticida pesticida = new Pesticida();
            pesticida.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            pesticida.Tipo = HttpUtility.HtmlEncode(txtTipoBuscar.Text);
            pesticida.Impacto = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : "";
            double phMenor = txtPhMenorBuscar.Text == "" ? -1 : double.Parse(txtPhMenorBuscar.Text);
            double phMayor = txtPhMayorBuscar.Text == "" ? -1 : double.Parse(txtPhMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";
            List<Pesticida> pesticidas = Web.buscarPesticidaFiltro(pesticida, phMenor, phMayor, ordenar);

            return pesticidas;
        }

        private void listarPagina()
        {
            List<Pesticida> pesticidas = obtenerPesticidas();
            List<Pesticida> pesticidasPagina = new List<Pesticida>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Pesticida unPesticida in pesticidas)
            {
                if (pesticidasPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    pesticidasPagina.Add(unPesticida);
                }

                cont++;
            }

            if (pesticidasPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún pesticida.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstPest.Visible = false;
            }
            else
            {
                lblMensajes.Text = "";
                modificarPagina();
                lstPest.Visible = true;
                lstPest.DataSource = null;
                lstPest.DataSource = pesticidasPagina;
                lstPest.DataBind();
            }
        }

        private void modificarPagina()
        {
            List<Pesticida> pesticidas = obtenerPesticidas();
            double pxp = PagMax();
            double count = pesticidas.Count;
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

        #endregion

        #region DropDownBoxes

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

        #region Impacto

        public void CargarImpacto()
        {
            lstImpacto.DataSource = createDataSource();
            lstImpacto.DataTextField = "nombre";
            lstImpacto.DataValueField = "id";
            lstImpacto.DataBind();

            lstImpactoBuscar.DataSource = createDataSource();
            lstImpactoBuscar.DataTextField = "nombre";
            lstImpactoBuscar.DataValueField = "id";
            lstImpactoBuscar.DataBind();
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

        #endregion

        #region Buscador

        public void CargarListBuscar()
        {
            listBuscarPor.DataSource = null;
            listBuscarPor.DataSource = createDataSourceBuscar();
            listBuscarPor.DataTextField = "nombre";
            listBuscarPor.DataValueField = "id";
            listBuscarPor.DataBind();
        }

        ICollection createDataSourceBuscar()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Buscar por", "Buscar por", dt));
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));
            dt.Rows.Add(createRow("PH", "PH", dt));
            dt.Rows.Add(createRow("Impacto", "Impacto", dt));
            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        DataRow createRow(String Text, String Value, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;
        }

        #endregion

        #endregion

        #region Botones

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtPhMenorBuscar.Text == "" && txtPhMayorBuscar.Text == "")
            {
                lblPaginaAct.Text = "1";
                listarPagina();
            }
            else if (txtPhMenorBuscar.Text != "" && txtPhMayorBuscar.Text != "")
            {
                if (double.Parse(txtPhMenorBuscar.Text) <= double.Parse(txtPhMayorBuscar.Text))
                {
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else lblMensajes.Text = "El pH menor es mayor";
            }
            else lblMensajes.Text = "El pH menor o mayor esta vacía.";
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre" ? true : false;
            txtTipoBuscar.Visible = listBuscarPor.SelectedValue == "Tipo" ? true : false;
            lblPhMenorBuscar.Visible = listBuscarPor.SelectedValue == "PH" ? true : false;
            txtPhMenorBuscar.Visible = listBuscarPor.SelectedValue == "PH" ? true : false;
            lblPhMayorBuscar.Visible = listBuscarPor.SelectedValue == "PH" ? true : false;
            txtPhMayorBuscar.Visible = listBuscarPor.SelectedValue == "PH" ? true : false;
            lstImpactoBuscar.Visible = listBuscarPor.SelectedValue == "Impacto" ? true : false;
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            System.Web.HttpContext.Current.Session["nombrePesticidaBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"] = txtTipoBuscar.Text;
            System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"] = txtPhMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"] = txtPhMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"] = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"] = txtTipoBuscar.Text;
            System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"] = txtPhMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"] = txtPhMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"] = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }


        protected void btnVolverPesti_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmLotesPestis");
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
                        Pesticida pesticida = new Pesticida(id, nombre, tipo, pH, impacto);
                        if (Web.altaPesti(pesticida))
                        {
                            if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
                            {
                                System.Web.HttpContext.Current.Session["idPesticidaSel"] = pesticida.IdPesticida.ToString();
                                Response.Redirect("/Paginas/Lotes/frmLotesPestis");
                            }
                            else
                            {
                                limpiar();
                                lblMensajes.Text = "Pesticida dado de alta con éxito.";
                                listarPagina();
                            }

                        }
                        else lblMensajes.Text = "Ya existe un Pesticida con estos datos. Estos son los posibles datos repetidos (Nombre).";
                    }
                    else lblMensajes.Text = "Falta seleccionar el tipo de impacto.";
                }
                else lblMensajes.Text = "El PH debe estar entre 0-14.";
            }
            else lblMensajes.Text = "Faltan Datos.";
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Pesticida pesticida = Web.buscarPesti(id);
            if (pesticida != null)
            {
                if (Web.bajaPesti(id))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha eliminado el Pesticida.";
                    listarPagina();
                }
                else
                {
                    limpiar();
                    lblMensajes.Text = "No se ha podido eliminar el Pesticida.";
                }
            }
            else lblMensajes.Text = "El Pesticida no existe.";
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";

            System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["tipoPesticidaBuscar"] = txtTipoBuscar.Text;
            System.Web.HttpContext.Current.Session["phMenorPesticidaBuscar"] = txtPhMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["phMayorPesticidaBuscar"] = txtPhMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["impactoPesticidaBuscar"] = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idPest"] = id;
            Response.Redirect("/Paginas/Pesticidas/modPest");


        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }


        #endregion

    }
}