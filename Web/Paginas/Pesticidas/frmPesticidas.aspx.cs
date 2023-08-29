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
       

            List<Pesticida> lstPest = Web.buscarPesticidaFiltro(b,d,o);
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


        private int PagMax()
        {
   
            return 2;
        }

        private List<Pesticida> obtenerPesticidas()
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

            List<Pesticida> pesticidas = Web.buscarPesticidaFiltro(buscar, impact, ordenar);
        
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
                        else
                        {

                            lblMensajes.Text = "Ya existe un Pesticida con estos datos. Estos son los posibles datos repetidos (Nombre).";

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
            else
            {
                lblMensajes.Text = "El Pesticida no existe.";
            }


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idPest"] = id;
            Response.Redirect("/Paginas/Pesticidas/modPest");


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