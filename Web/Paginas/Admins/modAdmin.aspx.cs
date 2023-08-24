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
    public partial class modAdmin : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idAdminSel"] == null)
            {
                Response.Redirect("/Paginas/Admins/frmAdmins");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = (int)System.Web.HttpContext.Current.Session["idAdminSel"];
                txtId.Text = id.ToString();
                cargarAdm(id);
                CargarTipo();
            }
        }


        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idAdminSel"] = null;
        }

        private void cargarAdm(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Admin lstadm = Web.buscarAdm(id);

            txtId.Text = lstadm.IdPersona.ToString();
            txtNombre.Text = lstadm.Nombre.ToString();
            txtApell.Text = lstadm.Apellido.ToString();



            txtFchNac.Text = DateTime.Parse(lstadm.FchNacimiento).ToString("yyyy-MM-dd");


            listTipoAdmin.SelectedValue = lstadm.TipoDeAdmin.ToString();
            lstEstado.SelectedValue = lstadm.Estado.ToString();
        }



        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || txtApell.Text == ""
             || txtFchNac.Text == "")


            {
                return true;
            }
            else { return false; }


        }

        private void limpiar()
        {

            txtId.Text = "";


            txtNombre.Text = "";
            txtApell.Text = "";

            txtFchNac.Text = "";
            lblMensajes.Text = "";
            listTipoAdmin.SelectedValue = "Seleccionar tipo de Admin";


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


        public void CargarTipo()
        {
            listTipoAdmin.DataSource = createDataSource();
            listTipoAdmin.DataTextField = "nombre";
            listTipoAdmin.DataValueField = "id";
            listTipoAdmin.DataBind();

            lstEstado.DataSource = createDataSourceEstado();
            lstEstado.DataTextField = "nombre";
            lstEstado.DataValueField = "id";
            lstEstado.DataBind();

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

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Admins/frmAdmins");


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {


                if (!faltanDatos())
                {
                    if (!txtId.Text.Equals(""))
                    {

                        if (listTipoAdmin.SelectedValue.ToString() != "Seleccionar tipo de Admin")
                        {
                            if (lstEstado.SelectedValue.ToString() != "Seleccionar estado de Admin")
                            {
                                if (fchNotToday())
                                {
                                    int id = Convert.ToInt32(HttpUtility.HtmlEncode(txtId.Text.ToString()));
                                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                                    string apellido = HttpUtility.HtmlEncode(txtApell.Text);


                                    string txtFc = HttpUtility.HtmlEncode(txtFchNac.Text);
                                    string tipoAdm = HttpUtility.HtmlEncode(listTipoAdmin.SelectedValue.ToString());
                                    string estado = HttpUtility.HtmlEncode(lstEstado.SelectedValue.ToString());



                                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                                    Admin unAdmin = new Admin();
                                    unAdmin.IdPersona = id;
                                    unAdmin.Nombre = nombre;
                                    unAdmin.Apellido = apellido;


                                    unAdmin.FchNacimiento = txtFc;
                                    unAdmin.TipoDeAdmin = tipoAdm;
                                    unAdmin.Estado = estado;

                                    if (Web.modificarAdm(unAdmin))
                                    {
                                        limpiar();

                                        lblMensajes.Text = "Admin modificado con éxito.";
                                        limpiarIdSession();
                                        System.Web.HttpContext.Current.Session["idAdminMod"] = "si";

                                        Response.Redirect("/Paginas/Admins/frmAdmins");
                                    }
                                    else
                                    {
                                        lblMensajes.Text = "No se pudo modificar el Administrador.";

                                    }
                                }
                                else
                                {
                                    lblMensajes.Text = "Seleccionar una fecha menor a hoy.";
                                }
                            }
                            else
                            {
                                lblMensajes.Text = "Falta seleccionar el estado de admin.";
                            }
                        }
                        else
                        {
                            lblMensajes.Text = "Falta selecionar el tipo de Admin.";

                        }
                    }
                    else
                    {
                        lblMensajes.Text = "Debe seleccionar un Admin.";
                    }


                }

                else
                {
                    lblMensajes.Text = "Faltan datos.";
                }
            }

            else
            {
                lblMensajes.Text = "Hay algún caracter no válido o faltante en el formulario";

            }
        }



    }

}

