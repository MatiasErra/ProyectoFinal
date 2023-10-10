using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Admins
{
    public partial class frmAuditoria : System.Web.UI.Page
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

                CargarListBuscar();
                CargarListOrdenarPor();
                CargarListAdmin();
                CargarListTabla();
                CargarListTipo();

                // Buscador
                txtFchMenor.Text = System.Web.HttpContext.Current.Session["fchMenorAuditoriaBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchMenorAuditoriaBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchMenorAuditoriaBuscar"] = null;
                txtFchMayor.Text = System.Web.HttpContext.Current.Session["fchMayorAuditoriaBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchMayorAuditoriaBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchMayorAuditoriaBuscar"] = null;

                // Listas
                lstAdminBuscar.SelectedValue = System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"] != null ? System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"].ToString() : "Seleccione un Admin";
                System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"] = null;
                lstTablaBuscar.SelectedValue = System.Web.HttpContext.Current.Session["tablaAuditoriaBuscar"] != null ? System.Web.HttpContext.Current.Session["tablaAuditoriaBuscar"].ToString() : "Seleccione una Tabla";
                System.Web.HttpContext.Current.Session["tablaAuditoriaBuscar"] = null;
                lstTipoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["tipoAuditoriaBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoAuditoriaBuscar"].ToString() : "Seleccione un Tipo";
                System.Web.HttpContext.Current.Session["tipoAuditoriaBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstAuditoria"] != null ? System.Web.HttpContext.Current.Session["BuscarLstAuditoria"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstAuditoria"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorAuditoria"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorAuditoria"].ToString() : "Ordenar por";
                System.Web.HttpContext.Current.Session["OrdenarPorAuditoria"] = null;


                comprobarBuscar();
                listarPagina();
            }
            
        }

        #endregion

        #region Utilidad

        private void limpiar()
        {
            lblMensajes.Text = "";
            lblPaginaAct.Text = "1";

            txtFchMenor.Text = "";
            txtFchMayor.Text = "";
            lstAdminBuscar.SelectedValue = "Seleccione un Admin";
            lstTablaBuscar.SelectedValue = "Seleccione una Tabla";
            lstTipoBuscar.SelectedValue = "Seleccione un Tipo";
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["fchMenorAuditoriaBuscar"] = txtFchMenor.Text != "" ? txtFchMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchMayorAuditoriaBuscar"] = txtFchMayor.Text != "" ? txtFchMayor.Text : null;

            System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"] = lstAdminBuscar.SelectedValue != "Seleccione un Admin" ? lstAdminBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["tablaAuditoriaBuscar"] = lstTablaBuscar.SelectedValue != "Seleccione una Tabla" ? lstTablaBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["tipoAuditoriaBuscar"] = lstTipoBuscar.SelectedValue != "Seleccione un Tipo" ? lstTipoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["BuscarLstAuditoria"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorAuditoria"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        private void comprobarBuscar()
        {
            lstAdminBuscar.Visible = listBuscarPor.SelectedValue == "Admin" ? true : false;
            btnBuscarAdmin.Visible = listBuscarPor.SelectedValue == "Admin" ? true : false;
            lblFch.Visible = listBuscarPor.SelectedValue == "Fecha" ? true : false;
            lstTablaBuscar.Visible = listBuscarPor.SelectedValue == "Tabla" ? true : false;
            lstTipoBuscar.Visible = listBuscarPor.SelectedValue == "Tipo" ? true : false;
        }

        #region Paginas

        private int PagMax()
        {
            return 4;
        }


        private List<Auditoria> obtenerAuditoria()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Auditoria auditoria = new Auditoria();
            auditoria.IdAdmin = lstAdminBuscar.SelectedValue != "Seleccione un Admin" ? int.Parse(lstAdminBuscar.SelectedValue) : 0;
            auditoria.Tabla = lstTablaBuscar.SelectedValue != "Seleccione una Tabla" ? lstTablaBuscar.SelectedValue : "";
            auditoria.Tipo = lstTipoBuscar.SelectedValue != "Seleccione un Tipo" ? lstTipoBuscar.SelectedValue : "";
            string fchMenor = txtFchMenor.Text == "" ? "1753-01-01" : txtFchMenor.Text;
            string fchMayor = txtFchMayor.Text == "" ? "9999-12-31" : txtFchMayor.Text;
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";
            List<Auditoria> auditorias = Web.buscarFiltrarAuditoria(auditoria, fchMenor, fchMayor, ordenar);

            return auditorias;
        }
        private void listarPagina()
        {
            List<Auditoria> auditorias = obtenerAuditoria();
            List<Auditoria> auditoriasPagina = new List<Auditoria>();

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Auditoria unaAuditoria in auditorias)
            {
                if (auditoriasPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    auditoriasPagina.Add(unaAuditoria);
                }

                cont++;
            }

            if (auditoriasPagina.Count == 0)
            {
                lblPaginaSig.Visible = false;
                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                txtPaginas.Text = "";
                lblMensajes.Text = "No se encontro nade en la Auditoria.";
                lstAuditoria.Visible = false;
            }
            else
            {
                txtPaginas.Text = "Paginas";

                lblMensajes.Text = "";
                modificarPagina();
                lstAuditoria.Visible = true;
                lstAuditoria.DataSource = null;
                lstAuditoria.DataSource = auditoriasPagina;
                lstAuditoria.DataBind();
            }


        }

        private void modificarPagina()
        {
            List<Auditoria> auditorias = obtenerAuditoria();
            double pxp = PagMax();
            double count = auditorias.Count;
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
            dt.Rows.Add(createRow("Admin", "Admin", dt));
            dt.Rows.Add(createRow("Recientes", "Recientes", dt));
            dt.Rows.Add(createRow("Antiguos", "Antiguos", dt));
            dt.Rows.Add(createRow("Tabla", "Tabla", dt));
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Admins

        public void CargarListAdmin()
        {
            lstAdminBuscar.DataSource = null;
            lstAdminBuscar.DataSource = createDataSourceAdmin();
            lstAdminBuscar.DataTextField = "nombre";
            lstAdminBuscar.DataValueField = "id";
            lstAdminBuscar.DataBind();
        }

        ICollection createDataSourceAdmin()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin adm = new Admin(0, "", "", "", "", "", "", "", "", "");
            List<Admin> admins = Web.buscarAdminFiltro(adm,  "");
            DataTable dt = new DataTable();



            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            dt.Rows.Add(createRow("Seleccione un Admin", "Seleccione un Admin", dt));
            cargarAdmins(admins, dt);


            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarAdmins(List<Admin> admins, DataTable dt)
        {
            foreach (Admin unAdmin in admins)
            {
                dt.Rows.Add(createRow(unAdmin.IdPersona.ToString() + " " + unAdmin.Nombre + " " + unAdmin.Apellido, unAdmin.IdPersona.ToString(), dt));
            }
        }

        #endregion

        #region Tipo

        public void CargarListTipo()
        {
            lstTipoBuscar.DataSource = null;
            lstTipoBuscar.DataSource = createDataSourceTipo();
            lstTipoBuscar.DataTextField = "nombre";
            lstTipoBuscar.DataValueField = "id";
            lstTipoBuscar.DataBind();
        }

        ICollection createDataSourceTipo()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Tipo", "Seleccione un Tipo", dt));
            dt.Rows.Add(createRow("Alta", "Alta", dt));
            dt.Rows.Add(createRow("Baja", "Baja", dt));
            dt.Rows.Add(createRow("Modificar", "Modificar", dt));

            DataView dv = new DataView(dt);
            return dv;
        }


        #endregion

        #region Tabla

        public void CargarListTabla()
        {
            lstTablaBuscar.DataSource = null;
            lstTablaBuscar.DataSource = createDataSourceTabla();
            lstTablaBuscar.DataTextField = "nombre";
            lstTablaBuscar.DataValueField = "id";
            lstTablaBuscar.DataBind();
        }

        ICollection createDataSourceTabla()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione una Tabla", "Seleccione una Tabla", dt));
            dt.Rows.Add(createRow("Admins", "Admins", dt));
            dt.Rows.Add(createRow("Camioneros", "Camioneros", dt));
            dt.Rows.Add(createRow("Camiones", "Camiones", dt));
            dt.Rows.Add(createRow("Clientes", "Clientes", dt));
            dt.Rows.Add(createRow("Depositos", "Depositos", dt));
            dt.Rows.Add(createRow("Granjas", "Granjas", dt));
            dt.Rows.Add(createRow("Productos", "Productos", dt));
            dt.Rows.Add(createRow("Lotes", "Lotes", dt));
            dt.Rows.Add(createRow("Pesticidas", "Pesticidas", dt));
            dt.Rows.Add(createRow("Lotes Pestis", "Lotes Pestis", dt));
            dt.Rows.Add(createRow("Fertilizantes", "Fertilizantes", dt));
            dt.Rows.Add(createRow("Lotes Fertis", "Lotes Fertis", dt));
            dt.Rows.Add(createRow("Lotes Pedidos", "Lotes Pedidos", dt));
            dt.Rows.Add(createRow("Viajes Lotes Pedidos", "Viajes Lotes Pedidos", dt));
            dt.Rows.Add(createRow("Viajes", "Viajes", dt));

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
            dt.Rows.Add(createRow("Admin", "Admin", dt));
            dt.Rows.Add(createRow("Fecha", "Fecha", dt));
            dt.Rows.Add(createRow("Tabla", "Tabla", dt));
            dt.Rows.Add(createRow("Tipo", "Tipo", dt));

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
                if (DateTime.Parse(txtFchMenor.Text) <= DateTime.Parse(txtFchMayor.Text)) num++;
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
                lblMensajes.Text = "La fecha menor es mayor.";
                listBuscarPor.SelectedValue = "Fecha";
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

        protected void btnBuscarAdmin_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["AuditoriaDatosFrm"] = "Buscar";
            guardarBuscar();
            Response.Redirect("/Paginas/Admins/frmAdmins");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion

        
    }
}