using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Lotes
{
    public partial class modLote : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idGranjaSel"] == null
                || System.Web.HttpContext.Current.Session["idProductoSel"] == null
                || System.Web.HttpContext.Current.Session["fchProduccionSel"] == null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
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

                if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
                {
                    cargarDatos();
                }
                else
                {
                    cargarLote(idGranja, idProducto, fchProduccion);
                }

            }
        }

        #endregion

        #region Utilidad

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["idProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(idGranja, idProducto, fchProduccion);
            txtIdGranja.Text = lote.IdGranja.ToString();
            txtIdProducto.Text = lote.IdProducto.ToString();
            txtFchProduccion.Text = DateTime.Parse(lote.FchProduccion).ToString("yyyy-MM-dd");
            txtCantidad.Text = lote.Cantidad.ToString();
            txtPrecio.Text = lote.Precio.ToString();
            listDeposito.SelectedValue = lote.IdDeposito.ToString();
        }

        private bool faltanDatos()
        {
            if (txtCantidad.Text == "" || txtPrecio.Text == "" || listDeposito.SelectedValue == "Seleccione un Deposito")
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

            txtCantidad.Text = "";
            txtPrecio.Text = "";
            CargarListDeposito();
        }

        #endregion

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
                dt.Rows.Add(createRow(unDeposito.Ubicacion + " " + unDeposito.Capacidad + " " + unDeposito.Temperatura + " C°", unDeposito.IdDeposito.ToString(), dt));
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

        #region Guardar y cargar datos

        private void guardarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatosMod"] = "Si";

            System.Web.HttpContext.Current.Session["cantidadSel"] = txtCantidad.Text;
            System.Web.HttpContext.Current.Session["precioSel"] = txtPrecio.Text;
            System.Web.HttpContext.Current.Session["idDepositoSel"] = listDeposito.SelectedValue;


        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatosMod"] = null;



            if (System.Web.HttpContext.Current.Session["idGranjaSel"] != null)
            {
                txtIdGranja.Text = System.Web.HttpContext.Current.Session["idGranjaSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["idProductoSel"] != null)
            {
                txtIdProducto.Text = System.Web.HttpContext.Current.Session["idProductoSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["fchProduccionSel"] != null)
            {
                txtFchProduccion.Text = DateTime.Parse(System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString()).ToString("yyyy-MM-dd");

            }

            txtCantidad.Text = System.Web.HttpContext.Current.Session["cantidadSel"].ToString();
            System.Web.HttpContext.Current.Session["cantidadSel"] = null;

            txtPrecio.Text = System.Web.HttpContext.Current.Session["precioSel"].ToString();
            System.Web.HttpContext.Current.Session["precioSel"] = null;

            if (System.Web.HttpContext.Current.Session["idDepositoSel"] == null)
            {
                listDeposito.SelectedValue = "Seleccione un Deposito";
                System.Web.HttpContext.Current.Session["idDepositoSel"] = null;
            }
            else
            {
                listDeposito.SelectedValue = System.Web.HttpContext.Current.Session["idDepositoSel"].ToString();
                System.Web.HttpContext.Current.Session["idDepositoSel"] = null;
            }
        }


        #endregion

        #region Botones

        protected void btnBuscarDeposito_Click(object sender, EventArgs e)
        {
            CargarListDeposito();
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Lotes/frmLotes");
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                int idGranja = int.Parse(HttpUtility.HtmlEncode(txtIdGranja.Text));
                int idProducto = int.Parse(HttpUtility.HtmlEncode(txtIdProducto.Text));
                string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                int cantidad = int.Parse(HttpUtility.HtmlEncode(txtCantidad.Text));
                double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote unLote = new Lote(idGranja, idProducto, fchProduccion, cantidad, precio, idDeposito);

                if (Web.modLote(unLote))
                {
                    limpiar();
                    lblMensajes.Text = "Lote modificado con éxito.";

                }
                limpiarIdSession();
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        protected void btnVerPestis_Click(object sender, EventArgs e)
        {
            guardarDatos();
            System.Web.HttpContext.Current.Session["loteDatosMod"] = "Si";
            Response.Redirect("/Paginas/Lotes/frmLotesPestis");
        }

        protected void btnVerFertis_Click(object sender, EventArgs e)
        {
            guardarDatos();
            System.Web.HttpContext.Current.Session["loteDatosMod"] = "Si";
            Response.Redirect("/Paginas/Lotes/frmLotesFertis");
        }

        #endregion

    }
}