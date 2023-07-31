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
                listar();

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


        public void CargarTipoAdm()
        {
            listTipoAdmin.DataSource = createDataSource();
            listTipoAdmin.DataTextField = "nombre";
            listTipoAdmin.DataValueField = "id";
            listTipoAdmin.DataBind();

        }



        ICollection createDataSource()
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

        private void cargarAdm(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin lstadm = Web.buscarAdm(id);

            txtId.Text = lstadm.IdPersona.ToString();
            txtNombre.Text = lstadm.Nombre.ToString();
            txtApell.Text = lstadm.Apellido.ToString();
            txtEmail.Text = lstadm.Email.ToString();
            txtTel.Text = lstadm.Telefono.ToString();
            txtUser.Text = lstadm.User.ToString();

            txtFchNac.Text = DateTime.Parse(lstadm.FchNacimiento).ToString("yyyy-MM-dd");
    
            txtPass.Text = lstadm.Contrasena.ToString();
            listTipoAdmin.SelectedValue = lstadm.TipoDeAdmin.ToString();
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

                            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");



                            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                            Admin unAdmin = new Admin(id, nombre, apellido, email, tele, txtFc, user, hashedPassword, tipoAdm);
                            if (Web.altaAdmin(unAdmin))
                            {
                                limpiar();
                                lblMensajes.Text = "Admin dado de alta con exito.";
                                listar();

                            }
                            else
                            {
                                limpiar();
                                lblMensajes.Text = "No se pudo dar de alta el Admin.";

                            }
                        }
                        else
                        {
                            lblMensajes.Text = "Seleccionar una fecha menor a hoy.";
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
                        lblMensajes.Text = "Se ha borrado el Admin.";
                        txtId.Text = "";
                        txtBuscar.Text = "";
                        listar();
                    }
                    else
                    {
                        limpiar();
                        lblMensajes.Text = "No se ha podido borrar el Admin.";
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
            Response.Redirect("/Paginas/Admins/modAdmin");




            //if (!faltanDatos())
            //{
            //    if (!txtId.Text.Equals(""))
            //    {
            //        if (txtPass.Text == "")
            //        {
            //            if (listTipoAdmin.SelectedValue.ToString() != "Seleccionar tipo de Admin")
            //            {
            //                if (fchNotToday())
            //                {
            //                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
            //                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
            //                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
            //                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
            //                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
            //                    string txtFc = HttpUtility.HtmlEncode(txtFchNac.Text);
            //                    string tipoAdm = HttpUtility.HtmlEncode(listTipoAdmin.SelectedValue.ToString());
            //                    string user = HttpUtility.HtmlEncode(txtUser.Text);



            //                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            //                    Admin unAdmin = new Admin();
            //                    unAdmin.IdPersona = id;
            //                    unAdmin.Nombre = nombre;
            //                    unAdmin.Apellido = apellido;
            //                    unAdmin.Email = email;
            //                    unAdmin.Telefono = tele;
            //                    unAdmin.FchNacimiento = txtFc;
            //                    unAdmin.TipoDeAdmin = tipoAdm;
            //                    unAdmin.User = user;

            //                    if (Web.modificarAdm(unAdmin))
            //                    {
            //                        lblMensajes.Text = "Admin modificado con exito.";
            //                        listar();
            //                        limpiar();
            //                    }
            //                    else
            //                    {
            //                        lblMensajes.Text = "No se pudo modificar el Administrador.";
            //                        limpiar();
            //                    }
            //                }
            //                else
            //                {
            //                    lblMensajes.Text = "Seleccionar una fecha menor a hoy.";
            //                }
            //            }
            //            else
            //            {
            //                lblMensajes.Text = "Falta selecionar un tipo de Admin.";

            //            }
            //        }
            //        else
            //        {
            //            lblMensajes.Text = "El administrador no puede cambiar la contraseña del Admin.";
            //        }
            //    }
            //    else
            //    {
            //        lblMensajes.Text = "Debe seleccionar un Admin.";
            //    }

            //}
            //else
            //{
            //    lblMensajes.Text = "Faltan datos.";
            //}
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }


    }
}