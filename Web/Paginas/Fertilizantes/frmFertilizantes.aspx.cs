using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;
using Web.Paginas.Productos;

namespace Web.Paginas.Fertilizantes
{
    public partial class frmFertilizante : System.Web.UI.Page
    {

        #region Load
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



                CargarImpacto();
                if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                {
                    btnVolverFerti.Visible = true;
                    lstFert.Visible = false;
                    lstFertSel.Visible = true;
                }

                System.Web.HttpContext.Current.Session["idFert"] = null;



                CargarListBuscar();
                CargarListOrdenarPor();

                // Buscador
                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"] = null;
                txtTipoBuscar.Text = System.Web.HttpContext.Current.Session["tipoFertilizanteBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoFertilizanteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["tipoFertilizanteBuscar"] = null;
                txtPhMenorBuscar.Text = System.Web.HttpContext.Current.Session["phMenorFertilizanteBuscar"] != null ? System.Web.HttpContext.Current.Session["phMenorFertilizanteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["phMenorFertilizanteBuscar"] = null;
                txtPhMayorBuscar.Text = System.Web.HttpContext.Current.Session["phMayorFertilizanteBuscar"] != null ? System.Web.HttpContext.Current.Session["phMayorFertilizanteBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["phMayorFertilizanteBuscar"] = null;

                // Listas
                lstImpactoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["impactoFertilizanteBuscar"] != null ? System.Web.HttpContext.Current.Session["impactoFertilizanteBuscar"].ToString() : "Seleccionar tipo de impacto";
                System.Web.HttpContext.Current.Session["impactoFertilizanteBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstFertilizante"] != null ? System.Web.HttpContext.Current.Session["BuscarLstFertilizante"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstFertilizante"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorFertilizante"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorFertilizante"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorFertilizante"] = null;
                comprobarBuscar();
                listarPagina();

                if (System.Web.HttpContext.Current.Session["FertiMod"] != null)
                {
                    lblMensajes.Text = "Fertilizante modificado";
                    System.Web.HttpContext.Current.Session["FertiMod"] = null;
                }
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
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
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
            Fertilizante ferti = new Fertilizante();
            Fertilizante fertilizante = new Fertilizante(0, "", "", 0, "");

            List<Fertilizante> lstFer = Web.buscarFertilizanteFiltro(ferti, 0, 15, "", 0, 0, "30/12/3000");
            foreach (Fertilizante fertilizantes in lstFer)
            {
                if (fertilizantes.IdFertilizante.Equals(intGuid))
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

        private void comprobarBuscar()
        {
            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre" ? true : false;
            txtTipoBuscar.Visible = listBuscarPor.SelectedValue == "Tipo" ? true : false;
            lblPh.Visible = listBuscarPor.SelectedValue == "PH" ? true : false;
            lstImpactoBuscar.Visible = listBuscarPor.SelectedValue == "Impacto" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["nombreFertilizanteBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["tipoFertilizanteBuscar"] = txtTipoBuscar.Text;
            System.Web.HttpContext.Current.Session["phMenorFertilizanteBuscar"] = txtPhMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["phMayorFertilizanteBuscar"] = txtPhMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["impactoFertilizanteBuscar"] = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["BuscarLstFertilizante"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorFertilizante"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 4;
        }

        private List<Fertilizante> obtenerFertilizantes()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Fertilizante fertilizante = new Fertilizante();
            fertilizante.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            fertilizante.Tipo = HttpUtility.HtmlEncode(txtTipoBuscar.Text);
            fertilizante.Impacto = lstImpactoBuscar.SelectedValue != "Seleccionar tipo de impacto" ? lstImpactoBuscar.SelectedValue : "";
            double phMenor = txtPhMenorBuscar.Text == "" ? 0 : double.Parse(txtPhMenorBuscar.Text);
            double phMayor = txtPhMayorBuscar.Text == "" ? 15 : double.Parse(txtPhMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";
            List<Fertilizante> fertilizantes = Web.buscarFertilizanteFiltro(fertilizante, phMenor, phMayor, ordenar, 0, 0, "30/12/3000");

            if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
            {
                string nombreGranja = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();
                string nombreProducto = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();
                Lote lote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
                List<Fertilizante> mostrar = new List<Fertilizante>();
                List<Lote_Ferti> fertisEnLote = Web.FertisEnLote(lote.IdGranja, lote.IdProducto, lote.FchProduccion, "", "");
                foreach (Fertilizante ferti in fertilizantes)
                {
                    int cont = 0;
                    foreach (Lote_Ferti loteF in fertisEnLote)
                    {
                        if (loteF.IdFertilizante.Equals(ferti.IdFertilizante))
                        {
                            cont++;
                        }
                    }
                    if (cont == 0)
                    {
                        mostrar.Add(ferti);
                    }
                }
                return mostrar;
            }
            else
            {
                return fertilizantes;
            }
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

                lblPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún fertilizante.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstFert.Visible = false;
                lstFertSel.Visible = false;
            }
            else
            {

                if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                {

                    lblPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina(fertilizantes);
                    lstFertSel.Visible = true;
                    lstFertSel.DataSource = null;
                    lstFertSel.DataSource = fertilizantesPagina;
                    lstFertSel.DataBind();

                }
                else
                {
                    lblPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina(fertilizantes);
                    lstFert.Visible = true;
                    lstFert.DataSource = null;
                    lstFert.DataSource = fertilizantesPagina;
                    lstFert.DataBind();
                }
            }
        }

        private void modificarPagina(List<Fertilizante> fertilizantes)
        {

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
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

            }


            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }

        #endregion

        #region DropDownBoxes

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

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string id = (HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));

            System.Web.HttpContext.Current.Session["idFertilizanteSel"] = id;

            Response.Redirect("/Paginas/Lotes/frmLotesFertis");

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                if (double.Parse(txtPhMenorBuscar.Text) <= double.Parse(txtPhMayorBuscar.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {
                lblPaginaAct.Text = "1";
                listarPagina();
            }
            else
            {
                lblMensajes.Text = "El pH menor es mayor.";
                listBuscarPor.SelectedValue = "PH";
                comprobarBuscar();
            }
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comprobarBuscar();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }


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

                        int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                        Fertilizante fertilizante = new Fertilizante(id, nombre, tipo, pH, impacto);
                        if (Web.altaFerti(fertilizante, idAdmin))
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
                                lblPaginaAct.Text = "1";
                                listarPagina();
                            }
                        }
                        else lblMensajes.Text = "Ya existe un Fertilizante con estos datos. Estos son los posibles datos repetidos (Nombre).";
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
            Fertilizante fertilizante = Web.buscarFerti(id);
            if (fertilizante != null)
            {
                int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                if (Web.bajaFerti(id, idAdmin))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha eliminado el Fertilizante.";
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else lblMensajes.Text = "No se ha podido eliminar el Fertilizante.";
            }
            else lblMensajes.Text = "El Fertilizante no existe.";
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";
            guardarBuscar();

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
            listarPagina();
        }



        #endregion

    }
}