using Clases;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Admins
{
    public partial class frmListarAdmins : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if(System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
            {
                int id = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin admin = Web.buscarAdm(id);

                if(admin.TipoDeAdmin == "Administrador global")
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
                if (System.Web.HttpContext.Current.Session["AuditoriaDatosFrm"] != null)
                {
                    btnVolver.Visible = true;
                    lstAdmin.Visible = false;
                    lstAdminSel.Visible = true;
                }


                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }

                System.Web.HttpContext.Current.Session["idAdminSel"] = null;

                CargarTipos();
                CargarListBuscar();
                CargarListOrdenarPor();



                // Buscador
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstAdmin"] != null ? System.Web.HttpContext.Current.Session["BuscarLstAdmin"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstAdmin"] = null;

                comprobarBuscar();



                txtNombreBuscar.Text = System.Web.HttpContext.Current.Session["nombreAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["nombreAdminBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["nombreAdminBuscar"] = null;
                txtApellidoBuscar.Text = System.Web.HttpContext.Current.Session["apellidoAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["apellidoAdminBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["apellidoAdminBuscar"] = null;
                txtEmailBuscar.Text = System.Web.HttpContext.Current.Session["emailAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["emailAdminBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["emailAdminBuscar"] = null;
            
   
                txtUsuarioBuscar.Text = System.Web.HttpContext.Current.Session["usuarioAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["usuarioAdminBuscar"].ToString() : "";
                System.Web.HttpContext.Current.Session["usuarioAdminBuscar"] = null;
             

                // Listas
                lstTipoAdminBuscar.SelectedValue = System.Web.HttpContext.Current.Session["tipoAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["tipoAdminBuscar"].ToString() : "Seleccionar tipo de Admin";
                System.Web.HttpContext.Current.Session["tipoAdminBuscar"] = null;
                lstEstadoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["estadoAdminBuscar"] != null ? System.Web.HttpContext.Current.Session["estadoAdminBuscar"].ToString() : "Seleccionar estado de Admin";
                System.Web.HttpContext.Current.Session["estadoAdminBuscar"] = null;
              
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorAdmin"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorAdmin"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorAdmin"] = null;
              
                listarPagina();

                lblMensajes.Text = System.Web.HttpContext.Current.Session["idAdminMod"] != null ? "Administrador Modificado" : "";
                System.Web.HttpContext.Current.Session["idAdminMod"] = null;

            }
        }

        #endregion

        #region Utilidad

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtFchNac.Text == "")
            {
                return true;
            }
            return false;
        }

        private bool fchNotToday()
        {
            string fecha = txtFchNac.Text;
            DateTime fechaDate = Convert.ToDateTime(fecha);
            if (fechaDate < DateTime.Today)
            {
                return true;
            }
            return false;
        }

        private void limpiar()
        {
            txtId.Text = "";
            lblMensajes.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            txtFchNac.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            listTipoAdmin.SelectedValue = "Seleccionar tipo de Admin";
            lstEstado.SelectedValue = "Seleccionar estado de Admin";

            txtNombreBuscar.Text = "";
            txtApellidoBuscar.Text = "";
            txtEmailBuscar.Text = "";

            txtUsuarioBuscar.Text = "";
      
            lstTipoAdminBuscar.SelectedValue = "Seleccionar tipo de Admin";
            lstEstadoBuscar.SelectedValue = "Seleccionar estado de Admin";

            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        private bool ValidUser()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<string> users = Web.userRepetidoCli();
            List<string> admins = Web.userRepetidoAdm();
            int pep = 0;
            string user = txtUser.Text;
            string Luser = user.ToLower();
            for (int i = 0; i < users.Count; i++)
            {
                if (Luser.Equals(users[i].ToString().ToLower()))
                {
                    pep++;
                }
                string u = users[i].ToString();
            }

            for (int i = 0; i < admins.Count; i++)
            {
                if (Luser.Equals(admins[i].ToString().ToLower()))
                {
                    pep++;
                }
                string u = admins[i].ToString();

            }

            if (pep > 0)
            {
                return false;
            }
            return true;
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
            List<Persona> lstPer = Web.lstIdPersonas();
            foreach (Persona persona in lstPer)
            {
                if (persona.IdPersona.Equals(intGuid))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                return intGuid;
            }
            return GenerateUniqueId();
        }

        private void comprobarBuscar()
        {

            txtNombreBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtApellidoBuscar.Visible = listBuscarPor.SelectedValue == "Nombre y Apellido" ? true : false;
            txtEmailBuscar.Visible = listBuscarPor.SelectedValue == "Email" ? true : false;
    
       
            txtUsuarioBuscar.Visible = listBuscarPor.SelectedValue == "Usuario" ? true : false;
            lstTipoAdminBuscar.Visible = listBuscarPor.SelectedValue == "Tipo de Admin" ? true : false;
            lstEstadoBuscar.Visible = listBuscarPor.SelectedValue == "Estado" ? true : false;
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["BuscarLstAdmin"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["nombreAdminBuscar"] = txtNombreBuscar.Text;
            System.Web.HttpContext.Current.Session["apellidoAdminBuscar"] = txtApellidoBuscar.Text;
            System.Web.HttpContext.Current.Session["emailAdminBuscar"] = txtEmailBuscar.Text;

            System.Web.HttpContext.Current.Session["usuarioAdminBuscar"] = txtUsuarioBuscar.Text;

            System.Web.HttpContext.Current.Session["tipoAdminBuscar"] = lstTipoAdminBuscar.SelectedValue != "Seleccionar tipo de Admin" ? lstTipoAdminBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["estadoAdminBuscar"] = lstEstadoBuscar.SelectedValue != "Seleccionar estado de Admin" ? lstEstadoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorAdmin"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        #region Paginas

        private int PagMax()
        {
            return 6;
        }

        private void listarPagina()
        {
            List<Admin> admins = obtenerAdmins();
            List<Admin> adminPagina = new List<Admin>();

            string p = lblPaginaAct.Text.ToString();

            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Admin unAdmin in admins)
            {
                if (adminPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    adminPagina.Add(unAdmin);
                }

                cont++;
            }

            if (adminPagina.Count == 0)
            {
         
                lblMensajes.Text = "No se encontro ningún administrador.";

                txtPaginas.Visible = false;

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstAdminSel.Visible = false;
                lstAdmin.Visible = false;
            }
            else
            {
                txtPaginas.Visible = true;

                lblPaginas.Visible = true;
                modificarPagina();
                if (System.Web.HttpContext.Current.Session["AuditoriaDatosFrm"] != null)
                {
                    lstAdminSel.Visible = true;
                    lstAdminSel.DataSource = null;
                    lstAdminSel.DataSource = adminPagina;
                    lstAdminSel.DataBind();
                }
                else
                {
                    lstAdmin.Visible = true;
                    lstAdmin.DataSource = null;
                    lstAdmin.DataSource = adminPagina;
                    lstAdmin.DataBind();
                }
            }
        }

        private void modificarPagina()
        {
            List<Admin> admins = obtenerAdmins();
            double pxp = PagMax();
            double count = admins.Count;
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


        private List<Admin> obtenerAdmins()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin admin = new Admin();
            admin.Nombre = HttpUtility.HtmlEncode(txtNombreBuscar.Text);
            admin.Apellido = HttpUtility.HtmlEncode(txtApellidoBuscar.Text);
            admin.Email = HttpUtility.HtmlEncode(txtEmailBuscar.Text);
            admin.Telefono = "";
            admin.User = HttpUtility.HtmlEncode(txtUsuarioBuscar.Text);
            admin.TipoDeAdmin = lstTipoAdminBuscar.SelectedValue != "Seleccionar tipo de Admin" ? lstTipoAdminBuscar.SelectedValue : "";
            admin.Estado = lstEstadoBuscar.SelectedValue != "Seleccionar estado de Admin" ? lstEstadoBuscar.SelectedValue : "";

            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Admin> admins = Web.buscarAdminFiltro(admin, ordenar);

            return admins;
        }

        #endregion

        #region DropDownBoxes

        #region Tipos y Estados

        public void CargarTipos()
        {
            listTipoAdmin.DataSource = createDataSourceTip();
            listTipoAdmin.DataTextField = "nombre";
            listTipoAdmin.DataValueField = "id";
            listTipoAdmin.DataBind();

            lstEstado.DataSource = createDataSourceEstado();
            lstEstado.DataTextField = "nombre";
            lstEstado.DataValueField = "id";
            lstEstado.DataBind();

            lstTipoAdminBuscar.DataSource = createDataSourceTip();
            lstTipoAdminBuscar.DataTextField = "nombre";
            lstTipoAdminBuscar.DataValueField = "id";
            lstTipoAdminBuscar.DataBind();

            lstEstadoBuscar.DataSource = createDataSourceEstado();
            lstEstadoBuscar.DataTextField = "nombre";
            lstEstadoBuscar.DataValueField = "id";
            lstEstadoBuscar.DataBind();
        }

        ICollection createDataSourceTip()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccionar tipo de Admin", "Seleccionar tipo de Admin", dt));
            dt.Rows.Add(createRow("Administrador global", "Administrador global", dt));
            dt.Rows.Add(createRow("Administrador de productos", "Administrador de productos", dt));
            dt.Rows.Add(createRow("Administrador de pedidos", "Administrador de pedidos", dt));


            DataView dv = new DataView(dt);
            return dv;

        }

        ICollection createDataSourceEstado()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccionar estado de Admin", "Seleccionar estado de Admin", dt));
            dt.Rows.Add(createRow("Habilitado", "Habilitado", dt));
            dt.Rows.Add(createRow("No Habilitado", "No Habilitado", dt));

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
            dt.Rows.Add(createRow("Nombre y Apellido", "Nombre y Apellido", dt));
            dt.Rows.Add(createRow("Email", "Email", dt));

            dt.Rows.Add(createRow("Usuario", "Usuario", dt));
            dt.Rows.Add(createRow("Tipo de admin", "Tipo de Admin", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));

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
            dt.Rows.Add(createRow("Apellido", "Apellido", dt));
            dt.Rows.Add(createRow("Email", "Email", dt));
            dt.Rows.Add(createRow("Usuario", "Usuario", dt));
            dt.Rows.Add(createRow("Tipo de Admin", "Tipo de Admin", dt));
            dt.Rows.Add(createRow("Estado", "Estado", dt));
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
            lblPaginaAct.Text = "1";
            listarPagina();
      
                comprobarBuscar();
            
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
            Response.Redirect("/Paginas/Estadisticas/frmAuditoria");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (txtPass.Text != "")
                {
                    if (listTipoAdmin.SelectedValue.ToString() != "Seleccionar tipo de Admin")
                    {
                        if (lstEstado.SelectedValue.ToString() != "Seleccionar estado de Admin")
                        {
                            if (ValidUser())
                            {
                                if (fchNotToday())
                                {
                                    int id = GenerateUniqueId();
                                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                                    string txtFc = HttpUtility.HtmlEncode(txtFchNac.Text);
                                    string tipoAdm = HttpUtility.HtmlEncode(listTipoAdmin.SelectedValue.ToString());
                                    string user = HttpUtility.HtmlEncode(txtUser.Text);
                                    string pass = HttpUtility.HtmlEncode(txtPass.Text);
                                    string estado = HttpUtility.HtmlEncode(lstEstado.SelectedValue.ToString());

                                    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");

                                    int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                                    Admin unAdmin = new Admin(id, nombre, apellido, email, tele, txtFc, user, hashedPassword, tipoAdm, estado);
                                    if (Web.altaAdmin(unAdmin, idAdmin))
                                    {
                                        lblPaginaAct.Text = "1";
                                        if (System.Web.HttpContext.Current.Session["AuditoriaDatosFrm"] != null)
                                        {
                                            System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"] = unAdmin.IdPersona.ToString();
                                            Response.Redirect("/Paginas/Estadisticas/frmAuditoria");
                                        }
                                        else
                                        {
                                            limpiar();
                                            lblPaginaAct.Text = "1";
                                            listarPagina();
                                            lblMensajes.Text = "Administrador dado de alta con éxito.";
                                        }
                                    }
                                    else lblMensajes.Text = "Ya existe un Administrador con estos datos. Estos son los posibles datos repetidos (Email / Teléfono / Usuario).";
                                }
                                else lblMensajes.Text = "La fecha debe ser menor a hoy";
                            }
                            else lblMensajes.Text = "El nombre de usuario ya existe.";
                        }
                        else lblMensajes.Text = "Falta seleccionar el estado de Administrador.";
                    }
                    else lblMensajes.Text = "Falta seleccionar el tipo de Administrador.";
                }
                else lblMensajes.Text = "Falta la contraseña.";
            }
            else lblMensajes.Text = "Faltan Datos.";
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["adminAuditoriaBuscar"] = id;

            if (System.Web.HttpContext.Current.Session["AuditoriaDatosFrm"] != null)
            {
                Response.Redirect("/Paginas/Estadisticas/frmAuditoria");
            }
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin unAdmin = Web.buscarAdm(id);
            if (unAdmin != null)
            {
                int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                if (idAdmin != id)
                {
                    if (Web.bajaAdmin(id, idAdmin))
                    {
                        limpiar();
                      
                        lblPaginaAct.Text = "1";
                        listarPagina();
                        lblMensajes.Text = "Se ha eliminado el Administrador.";
                    }
                    else lblMensajes.Text = "No se ha podido eliminar el Administrador.";
                }
                else lblMensajes.Text = "No puedes eliminarte a ti mismo.";

                
            }
            else lblMensajes.Text = "El Administrador no existe.";
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";

            guardarBuscar();

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idAdminSel"] = id;

            Response.Redirect("/Paginas/Admins/modAdmin");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }

        #endregion

    }
}