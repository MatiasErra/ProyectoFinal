using System;
using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using Clases;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Paginas
{
    public partial class frmAdmin : System.Web.UI.Page
    {


        private void listar()
        {

            //   ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            lstAdmin.DataSource = null;
            //  lstAdmin.DataSource = Web.lstAdmin();
            lstAdmin.DataBind();
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

           // txtTipoAdm.Text = "";

        }
        private bool faltanDatos()
        {
            if (txtId.Text == "" || txtNombre.Text == "" || txtApell.Text == "" || txtEmail.Text == "" || txtTel.Text == ""
             || txtUser.Text == "" || txtPass.Text == "" )//txtTipoAdm.Text ==""
            {
                return true;
            }
            else { return false; }


        }

        public void CargarTipoAdm()
        {
          

     

            listTipoAdmin.DataSource = CreateDataSource();
            listTipoAdmin.DataTextField = "nombre";
            listTipoAdmin.DataValueField = "id";
            listTipoAdmin.DataBind();


            //listTipoAdmin.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            //listTipoAdmin.Items.Insert(1, new ListItem("Administrador global", "1"));
            //listTipoAdmin.Items.Insert(2, new ListItem("Administrador de productos", "2"));
            //listTipoAdmin.Items.Insert(3, new ListItem("Administrador de pedidos", "3"));
            //listTipoAdmin.Items.Insert(4, new ListItem("Administrador de flota", "4"));
        }

        ICollection CreateDataSource()
        {

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(CreateRow("White", "White", dt));
            dt.Rows.Add(CreateRow("Silver", "Silver", dt));
            dt.Rows.Add(CreateRow("Dark Gray", "DarkGray", dt));
            dt.Rows.Add(CreateRow("Khaki", "Khaki", dt));
            dt.Rows.Add(CreateRow("Dark Khaki", "DarkKhaki", dt));

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }



        protected void listTipoAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            listTipoAdmin.SelectedIndexChanged -= listTipoAdmin_SelectedIndexChanged;
            
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


        

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }


    }
}
