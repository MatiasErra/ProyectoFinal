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
    public partial class frmLotesPestis : System.Web.UI.Page
    {
        #region Load
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (System.Web.HttpContext.Current.Session["idGranjaSel"] == null
            || System.Web.HttpContext.Current.Session["idProductoSel"] == null
            || System.Web.HttpContext.Current.Session["fchProduccionSel"] == null
           )

            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }


        }




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)

            {
                string Granja = System.Web.HttpContext.Current.Session["idGranjaSel"].ToString();
                string Producto = System.Web.HttpContext.Current.Session["idProductoSel"].ToString();

                int idGranja = int.Parse(Granja);
                int idProducto = int.Parse(Producto);

                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();
                if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
                {
                    btnAtrasMod.Visible = true;
                }
                else
                {
                    btnAtrasFrm.Visible = true;
                }

                if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
                {
                    cargarDatos();
                }
                CargarLote(idGranja, idProducto, fchProduccion);
                CargarListPesticida();
                CargarLotesPestis(idGranja, idProducto, fchProduccion);
            }

            if (txtGranja.Text == "" ||
                txtProducto.Text == "" ||
                txtFechProd.Text == "")
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");

            }
        }




        #endregion

        #region DropDownBoxes

        #region Pesticida

        public void CargarListPesticida()
        {
            listPesticida.DataSource = null;
            listPesticida.DataSource = createDataSourcePesticida();
            listPesticida.DataTextField = "nombre";
            listPesticida.DataValueField = "id";
            listPesticida.DataBind();
        }

        ICollection createDataSourcePesticida()
        {
            listPesticida.Visible = true;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Pesticida> pesticidas = new List<Pesticida>();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            if (txtBuscarPesticida.Text == "")
            {
                dt.Rows.Add(createRow("Seleccione un Pesticida", "Seleccione un Pesticida", dt));
                pesticidas = Web.lstPesti();
            }
            else
            {
                string value = txtBuscarPesticida.Text.ToLower();
                pesticidas = Web.buscarVarPesti(value);
                if (pesticidas.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningún Pesticida.";
                    listPesticida.Visible = false;
                }
            }



            cargarPesticidas(pesticidas, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarPesticidas(List<Pesticida> pesticidas, DataTable dt)
        {
            foreach (Pesticida unPesticida in pesticidas)
            {
                dt.Rows.Add(createRow(unPesticida.Nombre + " " + unPesticida.Tipo, unPesticida.IdPesticida.ToString(), dt));
            }
        }

        protected void btnAltaPesticida_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Pesticidas/frmPesticidas");
        }

        protected void btnBuscarPesticida_Click(object sender, EventArgs e)
        {

            CargarListPesticida();
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


            System.Web.HttpContext.Current.Session["lotePestiDatos"] = "Si";

            System.Web.HttpContext.Current.Session["idPestiCantSel"] = txtCantidadPesti.Text;
        }

        private void cargarDatos()
        {

            System.Web.HttpContext.Current.Session["lotePestiDatos"] = null;
            if (System.Web.HttpContext.Current.Session["idPesticidaSel"] != null)
            {
                listPesticida.SelectedValue = System.Web.HttpContext.Current.Session["idPesticidaSel"].ToString();
                System.Web.HttpContext.Current.Session["idPesticidaSel"] = null;
            }
            txtCantidadPesti.Text = System.Web.HttpContext.Current.Session["idPestiCantSel"].ToString();
        }


        #endregion

        #region Cargar lista pesticidas

        private void CargarLotesPestis(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<string[]> pesticidas = Web.PestisEnLote(idGranja, idProducto, fchProduccion);

            lstLotPestiSel.DataSource = null;
            lstLotPestiSel.DataSource = ObtenerDatos(pesticidas);
            lstLotPestiSel.DataBind();
        }

        public DataTable ObtenerDatos(List<string[]> pesticidas)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("IdPesticida", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
             new DataColumn("Cantidad", typeof(string))});

            foreach (string[] str in pesticidas)
            {
                int id = int.Parse(str[0].ToString());
                DataRow dr = dt.NewRow();
                dr["IdPesticida"] = id;
                dr["Nombre"] = str[1].ToString();
                dr["Tipo"] = str[2].ToString();
                dr["Cantidad"] = str[3].ToString();

                dt.Rows.Add(dr);
            }
            return dt;


        }
        #endregion

        #region Lote

        private void CargarLote(int idGranja, int idProducto, string fchProduccion)
        {
            txtGranja.Text = idGranja.ToString();
            txtProducto.Text = idProducto.ToString();

            txtFechProd.Text = fchProduccion.ToString();

        }

        private void limpiarLote()
        {
            txtGranja.Text = "";
            txtProducto.Text = "";
            txtFechProd.Text = "";

        }

        #endregion

        #region Botones

        protected void btnSelectPesticida_Click(object sender, EventArgs e)
        {
            if (listPesticida.SelectedValue != "Seleccione un Pesticida")
            {
                if (txtCantidadPesti.Text != "")
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    int idPesticida = int.Parse(HttpUtility.HtmlEncode(listPesticida.SelectedValue));
                    int idGranja = int.Parse(HttpUtility.HtmlEncode(txtGranja.Text.ToString()));
                    int idProducto = int.Parse(HttpUtility.HtmlEncode(txtProducto.Text.ToString()));
                    string fchProduccion = HttpUtility.HtmlEncode(txtFechProd.Text.ToString());

                    string cantidad = HttpUtility.HtmlEncode(txtCantidadPesti.Text);
                    Lote_Pesti loteP = new Lote_Pesti(idPesticida, idGranja, idProducto, fchProduccion, cantidad);
                    if (Web.altaLotePesti(loteP))
                    {
                        CargarListPesticida();
                        CargarLotesPestis(idGranja, idProducto, fchProduccion);
                        txtCantidadPesti.Text = "";
                        lblMensajes.Text = "Pesticida dado de alta en el Lote con éxito.";
                    }
                    else
                    {
                        lblMensajes.Text = "Este Pesticida ya existe en este Lote.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe ingresar una cantidad.";
                }

            }
            else
            {
                lblMensajes.Text = "Debe seleccionar un Pesticida para añadir al Lote.";
            }
        }

        protected void btnBajaPesti_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idPesticida = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = int.Parse(HttpUtility.HtmlEncode(txtGranja.Text.ToString()));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(txtProducto.Text.ToString()));
            string fchProduccion = HttpUtility.HtmlEncode(txtFechProd.Text.ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            if (Web.bajaLotePesti(idPesticida, idGranja, idProducto, fchProduccion))
            {
                CargarLotesPestis(idGranja, idProducto, fchProduccion);
                lblMensajes.Text = "Se eliminó el Pesticida del Lote.";
            }
            else
            {
                lblMensajes.Text = "Error al eliminar Pesticida del Lote";
            }

        }

        protected void btnModificarPesti_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idPesticida = int.Parse(selectedrow.Cells[0].Text);

            guardarDatos();
            System.Web.HttpContext.Current.Session["idPest"] = idPesticida;
            Response.Redirect("/Paginas/Pesticidas/modPest");
        }

        protected void btnModificarCantidad_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idPesticida = int.Parse(selectedrow.Cells[0].Text);
            int idGranja = int.Parse(HttpUtility.HtmlEncode(txtGranja.Text.ToString()));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(txtProducto.Text.ToString()));
            string fchProduccion = HttpUtility.HtmlEncode(txtFechProd.Text.ToString());
            System.Web.HttpContext.Current.Session["idPestiSel"] = idPesticida;

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote_Pesti loteP = Web.buscarLotePesti(idPesticida, idGranja, idProducto, fchProduccion);
            Pesticida pesti = Web.buscarPesti(idPesticida);

            txtCantidadPesti.Text = loteP.Cantidad;

            listPesticida.SelectedValue = "Seleccione un Pesticida";
            listPesticida.Enabled = false;
            listPesticida.SelectedValue = pesti.IdPesticida.ToString();
            btnSelect.Visible = false;
            btnAltaPesticida.Visible = false;
            btnCancelar.Visible = true;
            btnModificarCantidadPestiLote.Visible = true;
            btnBuscarPesticida.Visible = false;
            btnBuscarPesticida.Enabled = false;
            txtBuscarPesticida.Visible = false;
            btnAtrasFrm.Visible = false;
            btnAtrasMod.Visible = false;

            lblMensajes.Text = "Modifique la cantidad y presione el botón \"Modificar cantidad\".";
        }

        protected void btnModificarCantidadPestiLote_Click(object sender, EventArgs e)
        {
            if (txtCantidadPesti.Text != "")
            {
                int idPesticida = (int)System.Web.HttpContext.Current.Session["idPestiSel"];
                int idGranja = int.Parse(HttpUtility.HtmlEncode(txtGranja.Text.ToString()));
                int idProducto = int.Parse(HttpUtility.HtmlEncode(txtProducto.Text.ToString()));
                string fchProduccion = HttpUtility.HtmlEncode(txtFechProd.Text.ToString());
                string cantidad = HttpUtility.HtmlEncode(txtCantidadPesti.Text);
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote_Pesti loteP = new Lote_Pesti(idPesticida, idGranja, idProducto, fchProduccion, cantidad);
                if (Web.modLotePesti(loteP))
                {
                    cancelar();
                    lblMensajes.Text = "Se modifico la cantidad con éxito.";
                }
            }
            else
            {
                lblMensajes.Text = "Cantidad esta vacío.";
            }
        }

        private void cancelar()
        {
            System.Web.HttpContext.Current.Session["idPestiSel"] = null;
            txtCantidadPesti.Text = "";
            listPesticida.Enabled = true;
            btnSelect.Visible = true;
            btnAltaPesticida.Visible = true;
            btnBuscarPesticida.Visible = true;
            btnBuscarPesticida.Enabled = true;
            txtBuscarPesticida.Visible = true;
            btnCancelar.Visible = false;
            btnModificarCantidadPestiLote.Visible = false;
            int idGranja = int.Parse(HttpUtility.HtmlEncode(txtGranja.Text.ToString()));
            int idProducto = int.Parse(HttpUtility.HtmlEncode(txtProducto.Text.ToString()));
            string fchProduccion = HttpUtility.HtmlEncode(txtFechProd.Text.ToString());
            CargarLotesPestis(idGranja, idProducto, fchProduccion);

            if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
            {
                btnAtrasMod.Visible = true;
            }
            else
            {
                btnAtrasFrm.Visible = true;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar();
            lblMensajes.Text = "Modificación cancelada.";
        }

        protected void btnAtrasFrm_Click(object sender, EventArgs e)
        {
            limpiarLote();
            Response.Redirect("/Paginas/Lotes/frmLotes");
        }

        protected void btnAtrasMod_Click(object sender, EventArgs e)
        {
            limpiarLote();
            Response.Redirect("/Paginas/Lotes/modLote");
        }

        #endregion
    }
}