using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Depositos
{
    public partial class frmDepositos : System.Web.UI.Page
    {
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

                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {
                    btnVolver.Visible = true;
                    lstDeposito.Visible = false;
                    lstDepositoSelect.Visible = true;
                }
                if (System.Web.HttpContext.Current.Session["idDepMod"] != null)
                {
                    lblMensajes.Text = "Deposito Modificado";
                    System.Web.HttpContext.Current.Session["idDepMod"] = null;
                }
                System.Web.HttpContext.Current.Session["idDep"] = null;

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

        //private void listar()
        //{
        //    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
        //    List<Deposito> lst = Web.listDeps();
        //    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
        //    {

        //        lstDepositoSelect.Visible=true;
        //        lstDepositoSelect.DataSource =lst;
        //        lstDepositoSelect.DataBind();
        //    }
        //    else
        //    {

        //        lstDeposito.Visible = true;
        //        lstDeposito.DataSource = lst;
        //        lstDeposito.DataBind();
        //    }
        //}


        private int PagMax()
        {

            return 2;
        }



        private void listarPagina()
        {
            List<Deposito> depositos = obtenerDepositos();
            List<Deposito> depositoPagina = new List<Deposito>();
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Deposito unDeposito in depositos)
            {
                if (depositoPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    depositoPagina.Add(unDeposito);
                }

                cont++;
            }

            if (depositoPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Depósito.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstDeposito.Visible = false;
                lstDepositoSelect.Visible = false;
            }
            else
            {

                if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                {

                    lblMensajes.Text = "";
                    modificarPagina();
                    lstDepositoSelect.Visible = true;
                    lstDepositoSelect.DataSource = null;
                    lstDepositoSelect.DataSource = depositoPagina;
                    lstDepositoSelect.DataBind();
                }
                else
                {
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstDeposito.Visible = true;
                    lstDeposito.DataSource = null;
                    lstDeposito.DataSource = depositoPagina;
                    lstDeposito.DataBind();
                }
            }

        }
        private void modificarPagina()
        {
            List<Deposito> deposito = obtenerDepositos();
            double pxp = PagMax();
            double count = deposito.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();

            lblPaginaSig.Visible = true;
            lblPaginaAct.Visible = true;
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


        private List<Deposito> obtenerDepositos()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;

            string ordenar = "";


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }

            List<Deposito> depositos = Web.buscarDepositoFiltro(buscar, ordenar);

            return depositos;
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
            dt.Rows.Add(createRow("Capacidad", "Capacidad", dt));
            dt.Rows.Add(createRow("Ubicación", "Ubicación", dt));
            dt.Rows.Add(createRow("Temperatura", "Temperatura", dt));
            dt.Rows.Add(createRow("Condiciones", "Condiciones", dt));

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


        private bool faltanDatos()
        {
            if (txtCapacidad.Text == "" || txtCondiciones.Text == "" || txtTemperatura.Text == "" || txtUbicacion.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void limpiar()
        {
            lblMensajes.Text = "";
            txtId.Text = "";
            txtBuscar.Text = "";

            txtCapacidad.Text = "";
            txtCondiciones.Text = "";
            txtTemperatura.Text = "";
            txtUbicacion.Text = "";
            lstDeposito.SelectedIndex = -1;
            lblMensajes.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";
            listarPagina();
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
           
            List<Deposito> lstDep = Web.buscarDepositoFiltro(b,d);
            foreach (Deposito deposito in lstDep)
            {
                if (deposito.IdDeposito.Equals(intGuid))
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



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
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


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = GenerateUniqueId();
                string capacidad = HttpUtility.HtmlEncode(txtCapacidad.Text);
                string ubicacion = HttpUtility.HtmlEncode(txtUbicacion.Text);
                short temperatura = short.Parse(HttpUtility.HtmlEncode(txtTemperatura.Text));
                string condiciones = HttpUtility.HtmlEncode(txtCondiciones.Text);

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = new Deposito(id, capacidad, ubicacion, temperatura, condiciones);
                if (Web.altaDeps(unDeposito))
                {
                    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                    {
                        System.Web.HttpContext.Current.Session["idDepositoSel"] = unDeposito.IdDeposito.ToString();
                        Response.Redirect("/Paginas/Lotes/frmLotes");
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "Depósito dado de alta con éxito.";
                        listarPagina();

                    }

                }
                else
                {

                    lblMensajes.Text = "Ya existe un Depósito con estos datos. Estos son los posibles datos repetidos (Ubicación).";

                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idDepositoSel"] = id;

            Response.Redirect("/Paginas/Lotes/frmAltaLotes");



        }


        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Deposito unDeposito = Web.buscarDeps(id);
            if (unDeposito != null)
            {
                if (Web.bajaDeps(id))
                {
                    limpiar();
                    lblMensajes.Text = "Se ha eliminado el Depósito.";
                    listarPagina();
                }
                else
                {
                    lblMensajes.Text = "No se ha podido eliminar el Depósito.";
                }

            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idDep"] = id;
            Response.Redirect("/Paginas/Depositos/modDep");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}