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
using Web.Paginas.Productos;

namespace Web.Paginas.Lotes
{
    public partial class frmLotesFertis : System.Web.UI.Page
    {

        #region Load
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (System.Web.HttpContext.Current.Session["nombreGranjaSel"] == null
            || System.Web.HttpContext.Current.Session["nombreProductoSel"] == null
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
                string nombreGranja = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();
                string nombreProducto = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                string[] lote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);

                if (System.Web.HttpContext.Current.Session["loteDatosMod"] != null)
                {
                    btnAtrasMod.Visible = true;
                }
                else
                {
                    btnAtrasFrm.Visible = true;
                }

                if (System.Web.HttpContext.Current.Session["loteFertiDatos"] != null)
                {
                    cargarDatos();
                }
                CargarLote(nombreGranja, nombreProducto, fchProduccion);
                CargarListFertilizante(int.Parse(lote[0]), int.Parse(lote[2]), fchProduccion);
                CargarLotesFertis(int.Parse(lote[0]), int.Parse(lote[2]), fchProduccion);





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

        #region Fertilizante

        public void CargarListFertilizante(int idGranja, int idProducto, string fchProduccion)
        {
            listFertilizante.DataSource = null;
            listFertilizante.DataSource = createDataSourceFertilizante(idGranja, idProducto, fchProduccion);
            listFertilizante.DataTextField = "nombre";
            listFertilizante.DataValueField = "id";
            listFertilizante.DataBind();
        }

        ICollection createDataSourceFertilizante(int idGranja, int idProducto, string fchProduccion)
        {
            listFertilizante.Visible = true;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Fertilizante> fertilizantes = new List<Fertilizante>();
            List<string[]> mostrar = new List<string[]>();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            if (txtBuscarFertilizante.Text == "")
            {
                dt.Rows.Add(createRow("Seleccione un Fertilizante", "Seleccione un Fertilizante", dt));
                fertilizantes = Web.lstFerti();
            }
            else
            {
                string value = txtBuscarFertilizante.Text.ToLower();
                fertilizantes = Web.buscarVarFerti(value);

            }
            if (fertilizantes.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Fertilizante.";
                listFertilizante.Visible = false;
            }
            else
            {
                listFertilizante.Visible = true;
                List<string[]> filtro = Web.FertisEnLote(idGranja, idProducto, fchProduccion);
                foreach (Fertilizante unFerti in fertilizantes)
                {
                    int cont = 0;
                    foreach (string[] fertL in filtro)
                    {
                        if (int.Parse(fertL[0]).Equals(unFerti.IdFertilizante))
                        {
                            cont++;
                        }
                    }
                    if (cont == 0)
                    {
                        string[] fert = new string[5];
                        fert[0] = unFerti.IdFertilizante.ToString();
                        fert[1] = unFerti.Nombre;
                        fert[2] = unFerti.Tipo;
                        fert[3] = unFerti.PH.ToString();
                        fert[4] = unFerti.Impacto;
                        mostrar.Add(fert);
                    }
                }

                if (mostrar.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningún Fertilizante sin añadir.";
                    listFertilizante.Visible = false;
                }
                else
                {
                    cargarFertilizantes(mostrar, dt);
                }
            }
                DataView dv = new DataView(dt);
                return dv;
           
        }

        private void cargarFertilizantes(List<string[]> fertilizantes, DataTable dt)
        {
            foreach (string[] unFertilizante in fertilizantes)
            {
                dt.Rows.Add(createRow(unFertilizante[1] + " " + unFertilizante[2], unFertilizante[0], dt));
            }
        }

        protected void btnAltaFertilizante_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");
        }

        protected void btnBuscarFertilizante_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idGranja = int.Parse(lote[0]);
            int idProducto = int.Parse(lote[2]);
            string fchProduccion = lote[4];

            CargarListFertilizante(idGranja, idProducto, fchProduccion);
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


            System.Web.HttpContext.Current.Session["loteFertiDatos"] = "Si";

            System.Web.HttpContext.Current.Session["idFerCantSel"] = txtCantidadFerti.Text;
        }

        private void cargarDatos()
        {






            System.Web.HttpContext.Current.Session["loteFertiDatos"] = null;
            if (System.Web.HttpContext.Current.Session["idFertilizanteSel"] != null)
            {
                listFertilizante.SelectedValue = System.Web.HttpContext.Current.Session["idFertilizanteSel"].ToString();
                System.Web.HttpContext.Current.Session["idFertilizanteSel"] = null;
            }
            txtCantidadFerti.Text = System.Web.HttpContext.Current.Session["idFerCantSel"].ToString();
        }


        #endregion

        #region Cargar lista fertilizantes

        private void CargarLotesFertis(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            List<string[]> fertilizantes = Web.FertisEnLote(idGranja, idProducto, fchProduccion);

            lstLotFertSel.DataSource = null;
            lstLotFertSel.DataSource = ObtenerDatos(fertilizantes);
            lstLotFertSel.DataBind();
        }

        public DataTable ObtenerDatos(List<string[]> fertilizantes)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("IdFertilizante", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
             new DataColumn("Cantidad", typeof(string))});

            foreach (string[] str in fertilizantes)
            {
                int id = int.Parse(str[0].ToString());
                DataRow dr = dt.NewRow();
                dr["IdFertilizante"] = id;
                dr["Nombre"] = str[1].ToString();
                dr["Tipo"] = str[2].ToString();
                dr["Cantidad"] = str[3].ToString();

                dt.Rows.Add(dr);
            }
            return dt;


        }
        #endregion

        #region Lote

        private void CargarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            txtGranja.Text = nombreGranja.ToString();
            txtProducto.Text = nombreProducto.ToString();
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

        protected void btnSelectFertilizante_Click(object sender, EventArgs e)
        {
            if (listFertilizante.SelectedValue != "Seleccione un Fertilizante")
            {
                if (txtCantidadFerti.Text != "" && int.Parse(txtCantidadFerti.Text) > 0)
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                    int idFertilizante = int.Parse(HttpUtility.HtmlEncode(listFertilizante.SelectedValue));
                    int idGranja = int.Parse(lote[0]);
                    int idProducto = int.Parse(lote[2]);
                    string fchProduccion = lote[4];

                    string cantidad = HttpUtility.HtmlEncode(txtCantidadFerti.Text);
                    Lote_Ferti loteF = new Lote_Ferti(idFertilizante, idGranja, idProducto, fchProduccion, cantidad);
                    if (Web.altaLoteFerti(loteF))
                    {
                        CargarListFertilizante(idGranja, idProducto, fchProduccion);
                        CargarLotesFertis(idGranja, idProducto, fchProduccion);
                        txtCantidadFerti.Text = "";
                        lblMensajes.Text = "Fertilizante dado de alta en el Lote con éxito.";
                    }
                    else
                    {
                        lblMensajes.Text = "Este Fertilizante ya existe en este Lote.";
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe ingresar una cantidad o debe ser mayor a cero.";
                }

            }
            else
            {
                lblMensajes.Text = "Debe seleccionar un Fertilizante para añadir al Lote.";
            }
        }

        protected void btnBajaFerti_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idFertilizante = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = int.Parse(lote[0]);
            int idProducto = int.Parse(lote[2]);
            string fchProduccion = lote[4];

            if (Web.bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion))
            {
                CargarLotesFertis(idGranja, idProducto, fchProduccion);
                CargarListFertilizante(idGranja, idProducto, fchProduccion);
                lblMensajes.Text = "Se eliminó el Fertilizante del Lote.";
            }
            else
            {
                lblMensajes.Text = "Error al eliminar Fertilizante del Lote.";
            }

        }

        protected void btnModificarFerti_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            int idFertilizante = int.Parse(selectedrow.Cells[0].Text);

            guardarDatos();
            System.Web.HttpContext.Current.Session["idFert"] = idFertilizante;
            Response.Redirect("/Paginas/Fertilizantes/modFert");
        }

        protected void btnModificarCantidad_Click(object sender, EventArgs e)
        {
            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idFertilizante = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = int.Parse(lote[0]);
            int idProducto = int.Parse(lote[2]);
            string fchProduccion = lote[4];
            System.Web.HttpContext.Current.Session["idFertiSel"] = idFertilizante;

            Lote_Ferti loteF = Web.buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
            Fertilizante fer = Web.buscarFerti(idFertilizante);

            txtCantidadFerti.Text = loteF.Cantidad;

            listFertilizante.SelectedValue = "Seleccione un Fertilizante";
            listFertilizante.Enabled = false;
            listFertilizante.SelectedValue = fer.IdFertilizante.ToString();
            btnSelect.Visible = false;
            btnAltaFertilizante.Visible = false;
            btnCancelar.Visible = true;
            btnModificarCantidadFertiLote.Visible = true;
            btnBuscarFertilizante.Visible = false;
            btnBuscarFertilizante.Enabled = false;
            txtBuscarFertilizante.Visible = false;
            btnAtrasFrm.Visible = false;
            btnAtrasMod.Visible = false;

            lblMensajes.Text = "Modifique la cantidad y presione el botón \"Modificar cantidad\".";
        }

        protected void btnModificarCantidadFertiLote_Click(object sender, EventArgs e)
        {
            if (txtCantidadFerti.Text != "" && int.Parse(txtCantidadFerti.Text) > 0)
            {

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                int idFertilizante = (int)System.Web.HttpContext.Current.Session["idFertiSel"];
                int idGranja = int.Parse(lote[0]);
                int idProducto = int.Parse(lote[2]);
                string fchProduccion = lote[4];
                string cantidad = HttpUtility.HtmlEncode(txtCantidadFerti.Text);

                Lote_Ferti loteF = new Lote_Ferti(idFertilizante, idGranja, idProducto, fchProduccion, cantidad);
                if (Web.modLoteFerti(loteF))
                {
                    cancelar();
                    lblMensajes.Text = "Se modifico la cantidad con éxito.";
                }
            }
            else
            {
                lblMensajes.Text = "La cantidad esta vacía o debe ser mayor a cero.";
            }
        }

        private void cancelar()
        {
            System.Web.HttpContext.Current.Session["idFertiSel"] = null;
            txtCantidadFerti.Text = "";
            listFertilizante.Enabled = true;
            btnSelect.Visible = true;
            btnAltaFertilizante.Visible = true;
            btnBuscarFertilizante.Visible = true;
            btnBuscarFertilizante.Enabled = true;
            txtBuscarFertilizante.Visible = true;
            btnCancelar.Visible = false;
            btnModificarCantidadFertiLote.Visible = false;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string[] lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idGranja = int.Parse(lote[0]);
            int idProducto = int.Parse(lote[2]);
            string fchProduccion = lote[4];

            CargarLotesFertis(idGranja, idProducto, fchProduccion);


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