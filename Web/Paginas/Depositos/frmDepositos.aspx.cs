using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Depositos
{
    public partial class frmDepositos : System.Web.UI.Page
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
                CargarListBuscar();

                // Buscador
                txtCondicionesBuscar.Text = System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] = null;
                txtUbicacionBuscar.Text = System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] = null;
                txtTemperaturaMenorBuscar.Text = System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] = null;
                txtTemperaturaMayorBuscar.Text = System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] = null;
                txtCapacidadMenorBuscar.Text = System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] = null;
                txtCapacidadMayorBuscar.Text = System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] != null ? System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] = null;

                // Listas
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"] != null ? System.Web.HttpContext.Current.Session["OrdenarPor"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPor"] = null;

                listarPagina();


            }
        }

        #endregion

        #region Utilidad

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

            txtCondicionesBuscar.Text = "";
            txtUbicacionBuscar.Text = "";
            txtTemperaturaMayorBuscar.Text = "";
            txtTemperaturaMenorBuscar.Text = "";
            txtCapacidadMayorBuscar.Text = "";
            txtCapacidadMenorBuscar.Text = "";

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

            Deposito dep = new Deposito();
            List<Deposito> lstDep = Web.buscarDepositoFiltro(dep, 0, 0, -1, -1, "");
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

        #region Paginas

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
            Deposito deposito = new Deposito();
            deposito.Condiciones = HttpUtility.HtmlEncode(txtCondicionesBuscar.Text);
            deposito.Ubicacion = HttpUtility.HtmlEncode(txtUbicacionBuscar.Text);   
            int capacidadMenor = txtCapacidadMenorBuscar.Text == "" ? 0 : int.Parse(txtCapacidadMenorBuscar.Text);
            int capacidadMayor = txtCapacidadMayorBuscar.Text == "" ? 0 : int.Parse(txtCapacidadMayorBuscar.Text);
            int temperaturaMenor = txtTemperaturaMenorBuscar.Text == "" ? -1 : int.Parse(txtTemperaturaMenorBuscar.Text);
            int temperaturaMayor = txtTemperaturaMayorBuscar.Text == "" ? -1 : int.Parse(txtTemperaturaMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Deposito> depositos = Web.buscarDepositoFiltro(deposito,capacidadMenor, capacidadMayor, temperaturaMenor, temperaturaMayor, ordenar);

            return depositos;
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
            dt.Rows.Add(createRow("Capacidad", "Capacidad", dt));
            dt.Rows.Add(createRow("Ubicación", "Ubicación", dt));
            dt.Rows.Add(createRow("Temperatura", "Temperatura", dt));
            dt.Rows.Add(createRow("Condiciones", "Condiciones", dt));

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
            dt.Rows.Add(createRow("Capacidad", "Capacidad", dt));
            dt.Rows.Add(createRow("Ubicación", "Ubicación", dt));
            dt.Rows.Add(createRow("Temperatura", "Temperatura", dt));
            dt.Rows.Add(createRow("Condiciones", "Condiciones", dt));

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
            if (comprobarNumeros())
            {
                if(txtTemperaturaMenorBuscar.Text != "" && txtCapacidadMenorBuscar.Text != "")
                {
                    if ((int.Parse(txtTemperaturaMenorBuscar.Text) <= int.Parse(txtTemperaturaMayorBuscar.Text)) && (int.Parse(txtCapacidadMenorBuscar.Text) <= int.Parse(txtCapacidadMayorBuscar.Text)))
                    {
                        lblPaginaAct.Text = "1";
                        listarPagina();
                    }
                    else lblMensajes.Text = "La capacidad o temperatura menor es mayor.";
                }
                else if (txtTemperaturaMenorBuscar.Text != "")
                {
                    if (int.Parse(txtTemperaturaMenorBuscar.Text) <= int.Parse(txtTemperaturaMayorBuscar.Text))
                    {
                        lblPaginaAct.Text = "1";
                        listarPagina();
                    }
                    else lblMensajes.Text = "La temperatura menor es mayor.";
                }
                else if (txtCapacidadMenorBuscar.Text != "")
                {
                    if (int.Parse(txtCapacidadMenorBuscar.Text) <= int.Parse(txtCapacidadMayorBuscar.Text))
                    {
                        lblPaginaAct.Text = "1";
                        listarPagina();
                    }
                    else lblMensajes.Text = "La capacidad menor es mayor.";
                }
                else
                {
                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
            }
            else lblMensajes.Text = "La capacidad o temperatura esta incompleta.";
        }

        private bool comprobarNumeros()
        {
            int num = 0;
            num = txtCapacidadMayorBuscar.Text == "" && txtCapacidadMenorBuscar.Text == "" ? num + 1 : num + 0;
            num = txtCapacidadMayorBuscar.Text != "" && txtCapacidadMenorBuscar.Text != "" ? num + 1 : num + 0;
            num = txtTemperaturaMayorBuscar.Text == "" && txtTemperaturaMenorBuscar.Text == "" ? num + 1 : num + 0;
            num = txtTemperaturaMayorBuscar.Text != "" && txtTemperaturaMenorBuscar.Text != "" ? num + 1 : num + 0;

            if (num < 2) return false;
            return true;
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUbicacionBuscar.Visible = listBuscarPor.SelectedValue == "Ubicación" ? true : false;
            txtCondicionesBuscar.Visible = listBuscarPor.SelectedValue == "Condiciones" ? true : false;
            lblCapacidadMayorBuscar.Visible = listBuscarPor.SelectedValue == "Capacidad" ? true : false;
            lblCapacidadMenorBuscar.Visible = listBuscarPor.SelectedValue == "Capacidad" ? true : false;
            txtCapacidadMayorBuscar.Visible = listBuscarPor.SelectedValue == "Capacidad" ? true : false;
            txtCapacidadMenorBuscar.Visible = listBuscarPor.SelectedValue == "Capacidad" ? true : false;
            lblTemperaturaMayorBuscar.Visible = listBuscarPor.SelectedValue == "Temperatura" ? true : false;
            lblTemperaturaMenorBuscar.Visible = listBuscarPor.SelectedValue == "Temperatura" ? true : false;
            txtTemperaturaMayorBuscar.Visible = listBuscarPor.SelectedValue == "Temperatura" ? true : false;
            txtTemperaturaMenorBuscar.Visible = listBuscarPor.SelectedValue == "Temperatura" ? true : false;
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] = txtCondicionesBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] = txtCapacidadMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] = txtCapacidadMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] = txtTemperaturaMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] = txtTemperaturaMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] = txtCondicionesBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] = txtCapacidadMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] = txtCapacidadMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] = txtTemperaturaMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] = txtTemperaturaMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

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
                else lblMensajes.Text = "Ya existe un Depósito con estos datos. Estos son los posibles datos repetidos (Ubicación).";
            }
            else lblMensajes.Text = "Faltan datos.";
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
                else lblMensajes.Text = "No se ha podido eliminar el Depósito.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";
            System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] = txtCondicionesBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] = txtCapacidadMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] = txtCapacidadMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] = txtTemperaturaMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] = txtTemperaturaMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;

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

        #endregion
 
    }
}