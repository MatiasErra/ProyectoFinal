using Clases;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
            if (System.Web.HttpContext.Current.Session["nombreGranjaSel"] == null
                || System.Web.HttpContext.Current.Session["nombreProductoSel"] == null
                || System.Web.HttpContext.Current.Session["fchProduccionSel"] == null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string nombreGranja = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();
                string nombreProducto = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();

                CargarListDeposito();

                if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
                {
                    cargarDatos();
                }
                else
                {
                    cargarLote(nombreGranja, nombreProducto, fchProduccion);
                }

            }
        }

        #endregion

        #region Utilidad

        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] lote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            txtIdGranja.Text = lote[0].ToString();
            txtIdProducto.Text = lote[2].ToString();
            txtFchProduccion.Text = DateTime.Parse(lote[4]).ToString("dd/MM/yyyy");
            string cantidad = lote[5].ToString();
            string[] cant = cantidad.Split(' ');
            string count = cant[0].ToString();

            txtCantidad.Text = count;
            txtPrecio.Text = lote[6].ToString();
            listDeposito.SelectedValue = lote[7].ToString();
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
            DataView dv = new DataView();
            if (txtBuscarDeposito.Text == "")
            {
                string value = "";
                string ord = "";

                depositos = Web.buscarDepositoFiltro(value, ord);
            }
            else
            {
                string value = txtBuscarDeposito.Text.ToLower();
                string ord = "";
                depositos = Web.buscarDepositoFiltro(value, ord);

            }

            if (depositos.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningun Deposito.";
            }
            else
            {

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("nombre", typeof(String)));
                dt.Columns.Add(new DataColumn("id", typeof(String)));

                dt.Rows.Add(createRow("Seleccione un Deposito", "Seleccione un Deposito", dt));
                cargarDepositos(depositos, dt);
                dv = new DataView(dt);

            }


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



            if (System.Web.HttpContext.Current.Session["nombreGranjaSel"] != null)
            {
                txtIdGranja.Text = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["nombreProductoSel"] != null)
            {
                txtIdProducto.Text = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();

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

        protected void btnAltaDeposito_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Depositos/frmDepositos");
        }

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
                if (int.Parse(txtCantidad.Text) > 0)
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

                    int idGranja = int.Parse(HttpUtility.HtmlEncode(txtIdGranja.Text));
                    int idProducto = int.Parse(HttpUtility.HtmlEncode(txtIdProducto.Text));
                    string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                    Producto producto = Web.buscarProducto(idProducto);
                    string cantidad = HttpUtility.HtmlEncode(txtCantidad.Text) + " " + producto.TipoVenta.ToString();
                    double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                    int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));


                    Lote unLote = new Lote(idGranja, idProducto, fchProduccion, cantidad, precio, idDeposito);

                    if (Web.modLote(unLote))
                    {
                        limpiar();
                        lblMensajes.Text = "Lote modificado con éxito.";
                        limpiarIdSession();
                        System.Web.HttpContext.Current.Session["LoteMod"] = "si";
                        Response.Redirect("/Paginas/Lotes/frmLotes");

                    }
                    else
                    {
                        lblMensajes.Text = "No se modifico el lote";
                    }

                }
                else
                {
                    lblMensajes.Text = "La cantidad debe ser mayor a cero.";
                }

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