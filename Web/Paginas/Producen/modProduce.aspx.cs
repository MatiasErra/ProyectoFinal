using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;

namespace Web.Paginas.Producen
{
    public partial class modProduce : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idGranjaSel"] == null)
            {
                Response.Redirect("/Paginas/Producen/frmProducen");
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idGranja = (int)System.Web.HttpContext.Current.Session["idGranjaSel"];
                int idProducto = (int)System.Web.HttpContext.Current.Session["idProductoSel"];
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();

                CargarListDeposito();
                cargarProduce(idGranja, idProducto, fchProduccion);

            }
        }


        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["idProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarProduce(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Produce produce = Web.buscarProduce(idGranja, idProducto, fchProduccion);
            txtIdGranja.Text = produce.IdGranja.ToString();
            txtIdProducto.Text = produce.IdProducto.ToString();
            txtFchProduccion.Text = DateTime.Parse(produce.FchProduccion).ToString("yyyy-MM-dd");
            txtStock.Text = produce.Stock.ToString();
            txtPrecio.Text = produce.Precio.ToString();
            listDeposito.SelectedValue = produce.IdDeposito.ToString(); ;
        }



        private bool faltanDatos()
        {
            if (txtStock.Text == "" || txtPrecio.Text == "" || listDeposito.SelectedValue == "Seleccione un Deposito")
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
            txtIdGranja.Text = "";
            txtIdProducto.Text = "";
            txtBuscarDeposito.Text = "";

            txtStock.Text = "";
            txtPrecio.Text = "";
            CargarListDeposito();
        }

        #region DropDownBoxes

        #region Deposito

        public void CargarListDeposito()
        {
            listDeposito.DataSource = null;
            listDeposito.DataSource = createDataSourceDeposito();
            listDeposito.DataTextField = "nombre";
            listDeposito.DataValueField = "id";
            listDeposito.DataBind();
        }

        ICollection createDataSourceDeposito()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Deposito> depositos = new List<Deposito>();
            if (txtBuscarDeposito.Text == "")
            {
                depositos = Web.listDeps();
            }
            else
            {
                string value = txtBuscarDeposito.Text.ToLower();
                depositos = Web.buscarVarDeps(value);
                if (depositos.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningun Deposito.";
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Deposito", "Seleccione un Deposito", dt));

            cargarDepositos(depositos, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarDepositos(List<Deposito> depositos, DataTable dt)
        {
            foreach (Deposito unDeposito in depositos)
            {
                dt.Rows.Add(createRow(unDeposito.IdDeposito + " " + unDeposito.Capacidad + " " + unDeposito.Temperatura + "C°", unDeposito.IdDeposito.ToString(), dt));
            }
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



        protected void btnBuscarDeposito_Click(object sender, EventArgs e)
        {
            CargarListDeposito();
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Producen/frmProducen");


        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int idGranja = int.Parse(HttpUtility.HtmlEncode(txtIdGranja.Text));
                int idProducto = int.Parse(HttpUtility.HtmlEncode(txtIdProducto.Text));
                string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                int stock = int.Parse(HttpUtility.HtmlEncode(txtStock.Text));
                double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Produce unProduce = new Produce(idGranja, idProducto, fchProduccion, stock, precio, idDeposito);
                if (Web.modProduce(unProduce))
                {
                    limpiar();
                    lblMensajes.Text = "Produce modificado con exito.";

                    limpiarIdSession();
                    Response.Redirect("/Paginas/Producen/frmProducen");
                }
                else
                {
                    lblMensajes.Text = "No se pudo modificar el produce.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }
    }
}