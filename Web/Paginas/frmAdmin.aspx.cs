using Clases;
using System;
using System.Collections;
using System.Data;

namespace Web.Paginas
{
    public partial class frmAdmin : System.Web.UI.Page
    {


        private void listar()
        {

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstAdmin.DataSource = null;
            lstAdmin.DataSource = Web.lstAdmin();
            lstAdmin.DataBind();
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
            Calendar1.SelectedDate = DateTime.Now;
            txtUser.Text = "";
            txtPass.Text = "";
            listTipoAdmin.SelectedValue = "Seleccionar tipo de Admin";

           // txtTipoAdm.Text = "";

        }
        private bool faltanDatos()
        {
            if (txtId.Text == "" || txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtPass.Text == "" || listTipoAdmin.SelectedValue.ToString() == "Seleccionar tipo de Admin")

 
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
          

     

            listTipoAdmin.DataSource = CreateDataSource();
            listTipoAdmin.DataTextField = "nombre";
            listTipoAdmin.DataValueField = "id";
            listTipoAdmin.DataBind();

        }

        ICollection CreateDataSource()
        {

          
            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(CreateRow("Seleccionar tipo de Admin", "Seleccionar tipo de Admin", dt));
            dt.Rows.Add(CreateRow("Administrador global", "Administrador global", dt));
            dt.Rows.Add(CreateRow("Administrador de productos", "Administrador de productos", dt));
            dt.Rows.Add(CreateRow("Administrador de pedidos", "Administrador de pedidos", dt));
            dt.Rows.Add(CreateRow("Administrador de flota", "Administrador de flota", dt));
        
     
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

        
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }



        protected void listTipoAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            listTipoAdmin.SelectedIndexChanged -= listTipoAdmin_SelectedIndexChanged;

        }


        private int traerAdm()
        {
            if(!faltaIdAdm())
            { 
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                string AdmSt = this.lstAdmin.SelectedItem.ToString();
                string[] AdmArr = AdmSt.Split(' ');
                int Id = Convert.ToInt16(AdmArr[0]);
                Admin rep = Web.buscarAdm(Id);
                if (rep != null)
                {
                    return Id;

                }
                else
                {
                    return 0;

                }
            }

            else
            {
                return 0;

            }

        }

        private void cargarAdm(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin lstadm = Web.buscarAdm(id);

            txtNombre.Text = lstadm.Nombre.ToString();
            txtApell.Text = lstadm.Apellido.ToString();
            txtEmail.Text = lstadm.Email.ToString();
            txtTel.Text = lstadm.Telefono.ToString();
            txtUser.Text = lstadm.User.ToString();
            Calendar1.SelectedDate = Convert.ToDateTime( lstadm.FchNacimiento);
            txtPass.Text = lstadm.Contrasena.ToString();
            listTipoAdmin.SelectedValue = lstadm.TipoDeAdmin.ToString();
        }



        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int id = Convert.ToInt32(txtId.Text);
                string nombre = txtNombre.Text;
                string apellido = txtApell.Text;
                string email = txtEmail.Text;
                string tele = txtTel.Text;
                string txtFc = Calendar1.SelectedDate.ToShortDateString();


                string tipoAdm = listTipoAdmin.SelectedValue.ToString();
                string user = txtUser.Text;
                string pass = txtPass.Text;



                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin unAdmin = new Admin(id,nombre, apellido, email, tele, txtFc, pass, user, tipoAdm);
                if (Web.altaAdmin(unAdmin))
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
                lblMensajes.Text = "Faltan Datos";
            }
        }


    
        protected void btnBaja_Click(object sender, EventArgs e)
         {
            if (!faltaIdAdm())
            {

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                int Id = traerAdm();

                if (Web.bajaAdmin(Id))
                {
                    lblMensajes.Text = "Cliente eliminado con exito.";
                    listar();
                    limpiar();
                }
                else
                {
                    lblMensajes.Text = "No se pudo eliminar el Cliente";
                    limpiar();
                }
            }
            else
            {
                lblMensajes.Text = "Faltan selecionar un Cliente de la lista";

            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (this.lstAdmin.SelectedIndex > -1)
            {
                string linea = this.lstAdmin.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                short id = Convert.ToInt16(partes[0]);
                this.cargarAdm(id);

            }
            else
            {
                this.lblMensajes.Text = "Debe seleccionar un admin de la lista.";


            }
        }








        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTipoAdm();
            }
            }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void lstAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstAdmin.SelectedIndex > -1)
            {
                string linea = this.lstAdmin.SelectedItem.ToString();
                string[] partes = linea.Split(' ');
                short id = Convert.ToInt16(partes[0]);
                this.cargarAdm(id);

            }
            else
            {
                this.lblMensajes.Text = "Debe seleccionar un admin de la lista.";


            }
        }
    }
}
