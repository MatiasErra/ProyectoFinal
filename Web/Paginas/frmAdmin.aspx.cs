using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;

namespace Web.Paginas
{
    public partial class frmAdmin : System.Web.UI.Page
    {

        #region Utilidad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listar();
                
                Calendar1.SelectedDate = DateTime.Today;
            }
        }


        private void listar()
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstAdmin.DataSource = null;
            lstAdmin.DataSource = Web.lstAdmin();
            lstAdmin.DataBind();
            CargarTipoAdm();
        }

        protected void lstAdmin_Init(object sender, EventArgs e)
        {
            listar();
        }



        private void limpiar()
        {
            
            txtId.Text = "";
            txtNombre.Text = "";
            txtApell.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            Calendar1.SelectedDate = DateTime.Today;
            txtUser.Text = "";
            txtPass.Text = "";
            listTipoAdmin.SelectedValue = "Seleccionar tipo de Admin";
            lstAdmin.SelectedIndex = -1;




        }
        private bool faltanDatos()
        {
            if ( txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtPass.Text == "" )

 
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





        //private int traerAdm()
        //{
        //    if(!faltaIdAdm())
        //    { 
        //        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
        //        string AdmSt = this.lstAdmin.SelectedItem.ToString();
        //        string[] AdmArr = AdmSt.Split(' ');
        //        int Id = Convert.ToInt32(AdmArr[0]);
        //        Admin rep = Web.buscarAdm(Id);
        //        if (rep != null)
        //        {
        //            return Id;

        //        }
        //        else
        //        {
        //            return 0;

        //        }
        //    }

        //    else
        //    {
        //        return 0;

        //    }

        //}

        protected void lstAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!faltaIdAdm())
            {
                string linea = this.lstAdmin.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                int id = Convert.ToInt32(partes[0]);
                this.cargarAdm(id);
                this.lstAdmin.SelectedIndex = -1;
            }
            else
            {
                this.lblMensajes.Text = "Debe seleccionar un admin de la lista.";


            }
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
            Calendar1.SelectedDate = Convert.ToDateTime( lstadm.FchNacimiento);
            txtPass.Text = lstadm.Contrasena.ToString();
            listTipoAdmin.SelectedValue = lstadm.TipoDeAdmin.ToString();
        }

        static int GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            int intGuid = guid.GetHashCode();
            int i = 0;

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Persona> lstPer = Web.lstIdPersonas();
          foreach (Persona persona in lstPer )
            {
                if(persona.IdPersona.Equals(intGuid))
                {
                    i ++;
                }
            }

            if (i == 0)
            {
                return intGuid;
            }
            else return GenerateUniqueId();
        }


    
        #endregion


        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (listTipoAdmin.SelectedValue.ToString() != "Seleccionar tipo de Admin")
                {
                    int id = GenerateUniqueId();
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string tipoAdm = HttpUtility.HtmlEncode(listTipoAdmin.SelectedValue.ToString());
                    string user = HttpUtility.HtmlEncode(txtUser.Text);
                    string pass = HttpUtility.HtmlEncode(txtPass.Text);

                    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "SHA1");



                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Admin unAdmin = new Admin(id, nombre, apellido, email, tele, txtFc, user, hashedPassword, tipoAdm);
                    if (Web.altaAdmin(unAdmin) == true)
                    {
                        lblMensajes.Text = "Admin dado de alta con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo dar de alta el Administrador";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Faltan seleccionar el tipo de admin";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan Datos";
            }
        }


    
        protected void btnBaja_Click(object sender, EventArgs e)
         {
            if (txtId.Text != "")
            {

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin unAdmin = Web.buscarAdm(int.Parse(HttpUtility.HtmlEncode(txtId.Text)));
                if(unAdmin != null)
                {


                    int Id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text));

                    if (Web.bajaAdmin(Id))
                    {
                        lblMensajes.Text = "Admin eliminado con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo eliminar el Admin";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Error. El Admin no existe.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan selecionar un Admin de la lista";

            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (listTipoAdmin.SelectedValue.ToString() != "Seleccionar tipo de Admin")
                {
                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);
                    string email = HttpUtility.HtmlEncode(txtEmail.Text);
                    string tele = HttpUtility.HtmlEncode(txtTel.Text);
                    string txtFc = HttpUtility.HtmlEncode(Calendar1.SelectedDate.ToShortDateString());
                    string tipoAdm = HttpUtility.HtmlEncode(listTipoAdmin.SelectedValue.ToString());
                    string user = HttpUtility.HtmlEncode(txtUser.Text);



                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Admin unAdmin = new Admin();
                    unAdmin.IdPersona = id;
                    unAdmin.Nombre = nombre;
                    unAdmin.Apellido = apellido;
                    unAdmin.Email = email;
                    unAdmin.Telefono = tele;
                    unAdmin.FchNacimiento = txtFc;
                    unAdmin.TipoDeAdmin = tipoAdm;
                    unAdmin.User = user;

                    if (Web.modificarAdm(unAdmin))
                    {
                        lblMensajes.Text = "Administrador modificado con exito.";
                        listar();
                        limpiar();
                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo modificar el Administrador.";
                        limpiar();
                    }
                }
                else
                {
                    lblMensajes.Text = "Faltan selecionar un Admin de la lista";

                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }



        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
       
            limpiar();
        }

    







       



    }
}
