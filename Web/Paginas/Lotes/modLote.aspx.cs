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
            if (System.Web.HttpContext.Current.Session["AdminIniciado"] != null)
            {
                int id = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Admin admin = Web.buscarAdm(id);

                if (admin.TipoDeAdmin == "Administrador global")
                {
                    this.MasterPageFile = "~/Master/AGlobal.Master";
                }
                else if (admin.TipoDeAdmin == "Administrador de productos")
                {
                    this.MasterPageFile = "~/Master/AProductos.Master";
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

                txtNomGranja.Text = nombreGranja;
                txtNomProd.Text = nombreProducto;
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

        private bool fchCadMayorPro()
        {
            DateTime caducidad = Convert.ToDateTime(txtFchCaducidad.Text);
            DateTime produccion = Convert.ToDateTime(txtFchProduccion.Text);

            if (caducidad > produccion)
            {
                return true;
            }
            return false;
        }

        private string CantTotalProd(int idGranja, int idProducto, string fchProduccion, string cantidadAdd)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = new Lote(0,0,"","","",0,0);
            int cant = 0;
            string resultado = "";
            List<Lote> lotes = Web.buscarFiltrarLotes(lote, 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
            Producto producto = Web.buscarProducto(idProducto);

            foreach (Lote unlotes in lotes)
            {

                if (unlotes.IdProducto.Equals(producto.IdProducto)
                    && !unlotes.IdGranja.Equals(idGranja)
                     && !unlotes.FchProduccion.Equals(fchProduccion))
                {
                    string textCant = unlotes.Cantidad;
                    string[] str = textCant.Split(' ');
                    textCant = str[0];
                    cant += int.Parse(textCant);



                }

            }

            int cantidad = int.Parse(cantidadAdd);
            int total = cant + cantidad;
            resultado = total.ToString() + " " + producto.TipoVenta.ToString();
            return resultado;
        }



        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);

            txtFchProduccion.Text = DateTime.Parse(lote.FchProduccion).ToString("dd/MM/yyyy");
            txtFchCaducidad.Text = DateTime.Parse(lote.FchCaducidad).ToString("yyyy-MM-dd");
            string cantidad = lote.Cantidad.ToString();
            string[] cant = cantidad.Split(' ');
            string count = cant[0].ToString();

            txtCantidad.Text = count;
            txtPrecio.Text = lote.Precio.ToString();
            listDeposito.SelectedValue = lote.IdDeposito.ToString();
        }

        private bool faltanDatos()
        {
            if (txtFchCaducidad.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "" || listDeposito.SelectedValue == "Seleccione un Deposito")
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
            Deposito dep = new Deposito(0, "", "", 0, "");

            depositos = Web.buscarDepositoFiltro(dep, 0, 99999999, 0, 999, "");

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Deposito", "Seleccione un Deposito", dt));
            cargarDepositos(depositos, dt);
            dv = new DataView(dt);




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

            if (txtFchCaducidad.Text != "")
            {
                System.Web.HttpContext.Current.Session["fchCaducidadSel"] = txtFchCaducidad.Text;
            }

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = txtNomGranja.Text;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = txtNomProd.Text;
            System.Web.HttpContext.Current.Session["cantidadSel"] = txtCantidad.Text;
            System.Web.HttpContext.Current.Session["precioSel"] = txtPrecio.Text;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = txtFchProduccion.Text;
            System.Web.HttpContext.Current.Session["idDepositoSel"] = listDeposito.SelectedValue;


        }

        private void cargarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatosMod"] = null;



            if (System.Web.HttpContext.Current.Session["nombreGranjaSel"] != null)
            {
                txtNomGranja.Text = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["nombreProductoSel"] != null)
            {
                txtNomProd.Text = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();

            }

            if (System.Web.HttpContext.Current.Session["fchProduccionSel"] != null)
            {
                txtFchProduccion.Text = DateTime.Parse(System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString()).ToString("dd/MM/yyyy");

            }

            if (System.Web.HttpContext.Current.Session["fchCaducidadSel"] != null)
            {
                txtFchCaducidad.Text = DateTime.Parse(System.Web.HttpContext.Current.Session["fchCaducidadSel"].ToString()).ToString("yyyy-MM-dd");
                System.Web.HttpContext.Current.Session["fchCaducidadSel"] = null;
            }

            txtCantidad.Text = System.Web.HttpContext.Current.Session["cantidadSel"].ToString();
            System.Web.HttpContext.Current.Session["cantidadSel"] = null;

            txtPrecio.Text = System.Web.HttpContext.Current.Session["precioSel"].ToString();
            System.Web.HttpContext.Current.Session["precioSel"] = null;

            if (System.Web.HttpContext.Current.Session["idDepositoSel"] == null)
            {
                listDeposito.SelectedValue = "Seleccione un Deposito";
              
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
            System.Web.HttpContext.Current.Session["LoteDatosMod"] = "Si";
            guardarDatos();
            Response.Redirect("/Paginas/Depositos/frmDepositos");


        }



        protected void btnAltaDeposito_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Depositos/frmDepositos");
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
                if (fchCadMayorPro())
                {
                    if (int.Parse(txtCantidad.Text) > 0)
                    {
                        if (double.Parse(txtPrecio.Text) > 0)
                        {
                            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                            string fchProduccion = HttpUtility.HtmlEncode(txtFchProduccion.Text);
                            Lote lote = Web.buscarLote(txtNomGranja.Text, txtNomProd.Text, fchProduccion);

                            int idGranja = int.Parse(lote.IdGranja.ToString());
                            int idProducto = int.Parse(lote.IdProducto.ToString());
                         
                            Producto producto = Web.buscarProducto(idProducto);
                            string fchCaducidad = HttpUtility.HtmlEncode(txtFchCaducidad.Text);
                            string cantidad = HttpUtility.HtmlEncode(txtCantidad.Text) + " " + producto.TipoVenta.ToString();
                            double precio = double.Parse(HttpUtility.HtmlEncode(txtPrecio.Text));
                            int idDeposito = int.Parse(HttpUtility.HtmlEncode(listDeposito.SelectedValue));
                            string CantTotal = CantTotalProd(idGranja, idProducto, fchProduccion, txtCantidad.Text);

                            int idAdmin = (int)System.Web.HttpContext.Current.Session["AdminIniciado"];

                            Lote unLote = new Lote(idGranja, idProducto, fchProduccion, fchCaducidad, cantidad, precio, idDeposito);

                            if (Web.modLote(unLote, CantTotal, idAdmin))
                            {
                                limpiar();
                                lblMensajes.Text = "Lote modificado con éxito.";
                                limpiarIdSession();
                                System.Web.HttpContext.Current.Session["LoteMod"] = "si";
                                Response.Redirect("/Paginas/Lotes/frmLotes");

                            }
                            else lblMensajes.Text = "No se modifico el lote";
                        }
                        else lblMensajes.Text = "El precio debe ser mayor a cero.";
                    }
                    else lblMensajes.Text = "La fecha de caducidad debe ser mayor a la de producción.";
                }
                else lblMensajes.Text = "La cantidad debe ser mayor a cero.";
            }
            else lblMensajes.Text = "Faltan datos.";
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