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

namespace Web.Paginas.Admins
{
    public partial class frmListarAdmins : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.MasterPageFile = "~/Master/AGlobal.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiar();
                listar();
                CargarTipos();
            }
        }

        private void listar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstAdmin.DataSource = null;
            lstAdmin.DataSource = Web.lstAdmin();

            lstAdmin.DataBind();
        }

        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtFchNac.Text == "")


            {
                return true;
            }
            else { return false; }


        }

        private bool fchNotToday()
        {
            string fecha = txtFchNac.Text;
            DateTime fechaDate = Convert.ToDateTime(fecha);
            if (fechaDate < DateTime.Today)
            {
                return true;
            }
            else { return false; }


        }


        private bool faltaIdAdm()
        {
            if (lstAdmin.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


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

        }



        ICollection createDataSourceTip()
        {


            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(createRow("Seleccionar tipo de Admin", "Seleccionar tipo de Admin", dt));
            dt.Rows.Add(createRow("Administrador global", "Administrador global", dt));
            dt.Rows.Add(createRow("Administrador de productos", "Administrador de productos", dt));
            dt.Rows.Add(createRow("Administrador de pedidos", "Administrador de pedidos", dt));
            dt.Rows.Add(createRow("Administrador de flota", "Administrador de flota", dt));


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

            DataRow createRow(String Text, String Value, DataTable dt)
        {


            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }

        private void limpiar()
        {
           
            txtId.Text = "";
            txtBuscar.Text = "";
            lblMensajes.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            txtFchNac.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            listTipoAdmin.SelectedValue = "Seleccionar tipo de Admin";
            txtBuscar.Text = "";
            lstAdmin.SelectedIndex = -1;
            listar();

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
                if(Luser.Equals (users[i].ToString().ToLower()))
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




            if (pep>0)
            {
                return false;
            }
            else { return true; }


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
            else return GenerateUniqueId();
        }


        private void buscar()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string value = txtBuscar.Text;
            string val = value.ToLower();
            List<Admin> adminslst = new List<Admin>();
            adminslst = Web.buscarVarAdmin(value);
            lstAdmin.DataSource = null;

            if (txtBuscar.Text != "")
            {
                if (adminslst.Count > 0)
                {
                    lstAdmin.Visible = true;
                    lblMensajes.Text = "";
                    lstAdmin.DataSource = adminslst;
                    lstAdmin.DataBind();
                }
                else
                {
                    lstAdmin.Visible = false;
                    lblMensajes.Text = "No se encontro ningun admin.";
                 
                }
            } 
            else
            {
                lblMensajes.Text = "Debe poner algun dato en el buscador.";
            }
        }




        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
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



                                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                                    Admin unAdmin = new Admin(id, nombre, apellido, email, tele, txtFc, user, hashedPassword, tipoAdm, estado);
                                    if (Web.altaAdmin(unAdmin))
                                    {
                                        limpiar();
                                        lblMensajes.Text = "Admin dado de alta con éxito.";
                                        listar();

                                    }
                                    else
                                    {

                                        lblMensajes.Text = "Ya existe un admin con estos datos. Estos son los posibles datos repetidos (Email / Teléfono).";


                                    }

                                }
                                else
                                {
                                    lblMensajes.Text = "La fecha debe ser menor a hoy";
                                }
                            }
                            else
                            {
                                lblMensajes.Text = "El nombre de usuario ya existe.";
                            }
                        }
                        else
                        {
                            lblMensajes.Text = "Falta seleccionar el estado de admin.";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Falta seleccionar el tipo de admin.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Falta la contraseña.";
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
                Admin unAdmin = Web.buscarAdm(id);
                if (unAdmin != null)
                {
                    if (Web.bajaAdmin(id))
                    {
                        limpiar();
                        lblMensajes.Text = "Se ha eliminado el Admin.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                       
                        lblMensajes.Text = "No se ha podido eliminar el Admin.";
                    }
                }
                else
                {
                    lblMensajes.Text = "El Admin no existe.";
                }
          
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int id;
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            id = int.Parse(selectedrow.Cells[0].Text);

            System.Web.HttpContext.Current.Session["idAdminSel"] = id;
            

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }


    }
}