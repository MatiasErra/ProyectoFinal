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

                if (System.Web.HttpContext.Current.Session["loteDatos"] != null || System.Web.HttpContext.Current.Session["LoteDatosMod"] != null || System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
                {
                    btnVolver.Visible = true;
                    lstDeposito.Visible = false;
                    lstDepositoSelect.Visible = true;
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
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstDeposito"] != null ? System.Web.HttpContext.Current.Session["BuscarLstDeposito"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstDeposito"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorDeposito"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorDeposito"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorDeposito"] = null;
                comprobarBuscar();
                listarPagina();

                if (System.Web.HttpContext.Current.Session["idDepMod"] != null)
                {
                    lblMensajes.Text = "Deposito Modificado";
                    System.Web.HttpContext.Current.Session["idDepMod"] = null;
                }
            }
        }

        #endregion

        #region Utilidad


        private bool DepoistoLote(int idDeposito)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote lote = new Lote();
            lote.IdGranja = 0;
            lote.IdProducto =  0;
            lote.IdDeposito = 0;
            double precioMenor = 0;
            double precioMayor = 99999999;

            string fchProduccionMenor = "1000-01-01";
            string fchProduccionMayor = "3000-12-30";
            string fchCaducidadMenor = "1000-01-01";
            string fchCaducidadMayor = "3000-12-30";
            string ordenar = "";

            List<Lote> lotes = Web.buscarFiltrarLotes(lote, precioMenor, precioMayor, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);


            int i = 0;
            foreach (Lote unLote in lotes)
            {
                if (unLote.IdDeposito == idDeposito)
                {
                    i++;
                    break;
                }
            }
            if (i == 0)
            {
                return true;
            }
            else return false;
        }

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
            lblMensajes.Text = "";
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
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
            List<Deposito> lstDep = Web.buscarDepositoFiltro(dep, 0, 99999, 0, 999, "");
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

        private void comprobarBuscar()
        {
            txtUbicacionBuscar.Visible = listBuscarPor.SelectedValue == "Ubicación" ? true : false;
            txtCondicionesBuscar.Visible = listBuscarPor.SelectedValue == "Condiciones" ? true : false;
            lblCapacidad.Visible = listBuscarPor.SelectedValue == "Capacidad" ? true : false;
            lblTemp.Visible = listBuscarPor.SelectedValue == "Temperatura" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["ubicacionDepositoBuscar"] = txtUbicacionBuscar.Text;
            System.Web.HttpContext.Current.Session["condicionesDepositoBuscar"] = txtCondicionesBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMenorDepositoBuscar"] = txtCapacidadMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["capacidadMayorDepositoBuscar"] = txtCapacidadMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMenorDepositoBuscar"] = txtTemperaturaMenorBuscar.Text;
            System.Web.HttpContext.Current.Session["temperaturaMayorDepositoBuscar"] = txtTemperaturaMayorBuscar.Text;
            System.Web.HttpContext.Current.Session["BuscarLstDeposito"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorDeposito"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 6;
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
                txtPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Depósito.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstDeposito.Visible = false;
                lstDepositoSelect.Visible = false;
            }
            else
            {

                if (System.Web.HttpContext.Current.Session["loteDatos"] != null || System.Web.HttpContext.Current.Session["LoteDatosMod"] != null || System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
                {
                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstDepositoSelect.Visible = true;
                    lstDepositoSelect.DataSource = null;
                    lstDepositoSelect.DataSource = ObtenerDatos(depositoPagina);
                    lstDepositoSelect.DataBind();
                }
                else
                {
                    txtPaginas.Visible = true;
                    lblMensajes.Text = "";
                    modificarPagina();
                    lstDeposito.Visible = true;
                    lstDeposito.DataSource = null;
                    lstDeposito.DataSource = ObtenerDatos(depositoPagina);
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
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

            }

            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }

        public DataTable ObtenerDatos(List<Deposito> depositos)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("IdDeposito", typeof(int)),
                new DataColumn("Capacidad", typeof(string)),
                new DataColumn("Ubicacion", typeof(string)),
                new DataColumn("Temperatura", typeof(string)),
                   new DataColumn("Condiciones", typeof(string)),

            });

            foreach (Deposito unDep in depositos)
            {

                DataRow dr = dt.NewRow();
                dr["IdDeposito"] = unDep.IdDeposito.ToString();
                dr["Capacidad"] = unDep.Capacidad.ToString() + " m3";
                dr["Ubicacion"] = unDep.Ubicacion.ToString();
                dr["Temperatura"] = unDep.Temperatura.ToString() + " °C";
                dr["Condiciones"] = unDep.Condiciones.ToString();

                dt.Rows.Add(dr);



            }

            return dt;
        }



        private List<Deposito> obtenerDepositos()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Deposito deposito = new Deposito();
            deposito.Condiciones = HttpUtility.HtmlEncode(txtCondicionesBuscar.Text);
            deposito.Ubicacion = HttpUtility.HtmlEncode(txtUbicacionBuscar.Text);
            int capacidadMenor = txtCapacidadMenorBuscar.Text == "" ? 0 : int.Parse(txtCapacidadMenorBuscar.Text);
            int capacidadMayor = txtCapacidadMayorBuscar.Text == "" ? 99999 : int.Parse(txtCapacidadMayorBuscar.Text);
            int temperaturaMenor = txtTemperaturaMenorBuscar.Text == "" ? 0 : int.Parse(txtTemperaturaMenorBuscar.Text);
            int temperaturaMayor = txtTemperaturaMayorBuscar.Text == "" ? 999 : int.Parse(txtTemperaturaMayorBuscar.Text);
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Deposito> depositos = Web.buscarDepositoFiltro(deposito, capacidadMenor, capacidadMayor, temperaturaMenor, temperaturaMayor, ordenar);

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
            int num = 0;
            try
            {
                if (int.Parse(txtCapacidadMenorBuscar.Text) <= int.Parse(txtCapacidadMayorBuscar.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {
                try
                {
                    if (double.Parse(txtTemperaturaMenorBuscar.Text) <= double.Parse(txtTemperaturaMayorBuscar.Text)) num++;
                }
                catch
                {
                    num++;
                }

                if (num == 2)
                {

                    lblPaginaAct.Text = "1";
                    listarPagina();
                }
                else
                {
                    lblMensajes.Text = "La temperatura menor es mayor.";
                    listBuscarPor.SelectedValue = "Temperatura";
                    comprobarBuscar();
                }
            }
            else
            {
                lblMensajes.Text = "La capacidad menor es mayor.";
                listBuscarPor.SelectedValue = "Capacidad";
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


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }
            else if (System.Web.HttpContext.Current.Session["LoteDatosMod"] != null)
            {
                Response.Redirect("/Paginas/Lotes/modLote");
            }
            else if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                Response.Redirect("/Paginas/Lotes/frmAltaLotes");
            }
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

                int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Deposito unDeposito = new Deposito(id, capacidad, ubicacion, temperatura, condiciones);
                if (Web.altaDeps(unDeposito, idAdmin))
                {
                    if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
                    {
                        System.Web.HttpContext.Current.Session["idDepositoSel"] = unDeposito.IdDeposito.ToString();
                        Response.Redirect("/Paginas/Lotes/frmLotes");
                    }
                    else if (System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
                    {
                        System.Web.HttpContext.Current.Session["depositoLoteBuscar"] = unDeposito.IdDeposito.ToString();
                        Response.Redirect("/Paginas/Lotes/frmLotes");
                    }
                    else
                    {
                        limpiar();
                       
                        lblPaginaAct.Text = "1";
                        listarPagina();
                        lblMensajes.Text = "Depósito dado de alta con éxito.";
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

            

            if (System.Web.HttpContext.Current.Session["loteDatosBuscar"] != null)
            {
                System.Web.HttpContext.Current.Session["depositoLoteBuscar"] = id;
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }
            else if (System.Web.HttpContext.Current.Session["LoteDatosMod"] != null)
            {
                System.Web.HttpContext.Current.Session["idDepositoSel"] = id;
                Response.Redirect("/Paginas/Lotes/modLote");
            }
            else if (System.Web.HttpContext.Current.Session["loteDatos"] != null)
            {
                System.Web.HttpContext.Current.Session["idDepositoSel"] = id;
                Response.Redirect("/Paginas/Lotes/frmAltaLotes");
            }
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
                if (DepoistoLote(unDeposito.IdDeposito))
                {
                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                    if (Web.bajaDeps(id, idAdmin))
                    {
                        limpiar();

                        lblPaginaAct.Text = "1";
                        listarPagina();
                        lblMensajes.Text = "Se ha eliminado el Depósito.";
                    }
                    else lblMensajes.Text = "No se ha podido eliminar el Depósito.";
                }
                else lblMensajes.Text = "No se ha podido eliminar el Depósito porque está asignado a un lote.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1"; 
            guardarBuscar();

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