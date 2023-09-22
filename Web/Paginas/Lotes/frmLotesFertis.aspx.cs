using Clases;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
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
                if (System.Web.HttpContext.Current.Session["PagAct"] == null)
                {
                    lblPaginaAct.Text = "1";
                }
                else
                {
                    lblPaginaAct.Text = System.Web.HttpContext.Current.Session["PagAct"].ToString();
                    System.Web.HttpContext.Current.Session["PagAct"] = null;
                }


                string nombreGranja = System.Web.HttpContext.Current.Session["nombreGranjaSel"].ToString();
                string nombreProducto = System.Web.HttpContext.Current.Session["nombreProductoSel"].ToString();
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote lote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);

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
                CargarListFertilizante(lote.IdGranja, lote.IdProducto, fchProduccion);
               CargarListOrdenarPor();

                if (System.Web.HttpContext.Current.Session["Buscar"] != null)
                {
                    txtBuscar.Text = System.Web.HttpContext.Current.Session["Buscar"].ToString();
                    System.Web.HttpContext.Current.Session["Buscar"] = null;
                }


                if (System.Web.HttpContext.Current.Session["OrdenarPor"] != null)
                {
                    listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPor"].ToString();
                    System.Web.HttpContext.Current.Session["OrdenarPor"] = null;
                }

                listarPagina(lote.IdGranja, lote.IdProducto, fchProduccion);





            }

            if (txtGranja.Text == "" ||
                txtProducto.Text == "" ||
                txtFechProd.Text == "")
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");

            }
        }




        #endregion

        #region Utilidad

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

        #region Cargar lista fertilizantes

        private int PagMax()
        {

            return 2;
        }



        private List<Lote_Ferti> obtenerLote_Fert(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;

            string ordenar = "";


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }



            List<Lote_Ferti> Lote_Fert = Web.FertisEnLote(idGranja, idProducto, fchProduccion, buscar, ordenar);

            return Lote_Fert;
        }


        private void listarPagina(int idGranja, int idProducto, string fchProduccion)
        {
            List<Lote_Ferti> lotes_fert = obtenerLote_Fert(idGranja, idProducto, fchProduccion);
            List<Lote_Ferti> Lotes_fertPagina = new List<Lote_Ferti>();

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Lote_Ferti unLote in lotes_fert)
            {
                if (Lotes_fertPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    Lotes_fertPagina.Add(unLote);
                }

                cont++;
            }

            if (Lotes_fertPagina.Count == 0)
            {
                lblPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Fertilizante en el lote.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstLotFertSel.Visible = false;

                txtBuscar.Visible = false;
                btnBuscar.Visible = false;
                listOrdenarPor.Visible = false;
                btnLimpiar.Visible = false;

            }
            else
            {
                lblPaginas.Visible = true;
                lblMensajes.Text = "";
                txtBuscar.Visible = true;
                btnBuscar.Visible = true;
                listOrdenarPor.Visible = true;
                btnLimpiar.Visible = true;


                modificarPagina(idGranja, idProducto, fchProduccion);
                lstLotFertSel.Visible = true;
                lstLotFertSel.DataSource = null;
                lstLotFertSel.DataSource = ObtenerDatos(Lotes_fertPagina);
                lstLotFertSel.DataBind();
            }


        }

        private void modificarPagina(int idGranja, int idProducto, string fchProduccion)
        {
            List<Lote_Ferti> lotes = obtenerLote_Fert(idGranja, idProducto, fchProduccion);
            double pxp = PagMax();
            double count = lotes.Count;
            double pags = count / pxp;
            double cantPags = Math.Ceiling(pags);

            string pagAct = lblPaginaAct.Text.ToString();

            lblPaginaSig.Visible = true;
            lblPaginaAnt.Visible = true;
            lblPaginaAct.Visible = true;
            if (pagAct == cantPags.ToString())
            {
                lblPaginaSig.Visible = false;
            }
            if (pagAct == "1")
            {
                lblPaginaAnt.Visible = false;
            }
            if (pagAct == cantPags.ToString() && pagAct == "1")
            {
                txtPaginas.Visible = false;
                lblPaginaAct.Visible = false;

            }


            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }




        public DataTable ObtenerDatos(List<Lote_Ferti> fertilizantes)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("IdFertilizante", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
                new DataColumn("PH", typeof(string)),
             new DataColumn("Cantidad", typeof(string))});

            foreach (Lote_Ferti str in fertilizantes)
            {

                DataRow dr = dt.NewRow();
                dr["IdFertilizante"] = str.IdFertilizante;
                dr["Nombre"] = str.NombreFert;
                dr["Tipo"] = str.TipoFert;
                dr["PH"] = str.PHFert;
                dr["Cantidad"] = str.Cantidad;

                dt.Rows.Add(dr);
            }
            return dt;


        }


        private void limpiar()
        {
            lblMensajes.Text = "";
            txtBuscar.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";


        }

        #endregion

        #region Guardar y cargar datos

        private void guardarDatos()
        {


            System.Web.HttpContext.Current.Session["loteFertiDatos"] = "Si";

            System.Web.HttpContext.Current.Session["idFerCantSel"] = txtCantidadFerti.Text;

            System.Web.HttpContext.Current.Session["idFertilizanteSel"] = listFertilizante.SelectedValue != "Seleccione un Fertilizante" ? listFertilizante.SelectedValue : null;
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

        #region DropDownBoxes

        #region Fertilizante



        ICollection createDataSourceFertilizanteSel(Fertilizante fert)
        {
            listFertilizanteSel.Visible = true;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Fertilizante", "Seleccione un Fertilizante", dt));

            dt.Rows.Add(createRow(fert.Nombre.ToString() + " " + fert.Tipo.ToString(), fert.IdFertilizante.ToString(), dt));
            DataView dv = new DataView(dt);
            return dv;
        }




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

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            Fertilizante fert = new Fertilizante(0, "", "", 0, "");

            // Pesticidas fuera del lote
            List<Fertilizante> fertilizantes = Web.buscarFertilizanteFiltro(fert, 0, 15, "", idGranja, idProducto, fchProduccion);

            List<Fertilizante> mostrar = new List<Fertilizante>();
            List<Lote_Ferti> fertisEnLote = Web.FertisEnLote(idGranja, idProducto, fchProduccion, "", "");
            foreach (Fertilizante ferti in fertilizantes)
            {
                int cont = 0;
                foreach (Lote_Ferti loteF in fertisEnLote)
                {
                    if (loteF.IdFertilizante.Equals(ferti.IdFertilizante))
                    {
                        cont++;
                    }
                }
                if (cont == 0)
                {
                    mostrar.Add(ferti);
                }
            }

            if (mostrar.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Fertilizante para añadír.";
                listFertilizante.Visible = false;
            }
            else
            {
                dt.Rows.Add(createRow("Seleccione un Fertilizante", "Seleccione un Fertilizante", dt));
                listFertilizante.Visible = true;
                cargarFertilizantes(mostrar, dt);
            }
            DataView dv = new DataView(dt);
            return dv;

        }

        private void cargarFertilizantes(List<Fertilizante> fertilizantes, DataTable dt)
        {
            foreach (Fertilizante unFertilizante in fertilizantes)
            {
                dt.Rows.Add(createRow(unFertilizante.Nombre + " " + unFertilizante.Tipo, unFertilizante.IdFertilizante.ToString(), dt));
            }
        }



        #endregion

        #region Ordenar

        public void CargarListOrdenarPor()
        {
            listOrdenarPor.DataSource = null;
            listOrdenarPor.DataSource = createDataSourceOrdenarPor();
            listOrdenarPor.DataTextField = "nombre";
            listOrdenarPor.DataValueField = "id";
            listOrdenarPor.DataBind();
        }

        ICollection createDataSourceOrdenarPor()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ordenar por", "Ordenar por", dt));
            dt.Rows.Add(createRow("Nombre", "Nombre", dt));
            dt.Rows.Add(createRow("Tipo", "Cantidad", dt));
            dt.Rows.Add(createRow("pH", "pH", dt));
            dt.Rows.Add(createRow("Cantidad", "Cantidad", dt));


            DataView dv = new DataView(dt);
            return dv;
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

        #endregion

        #region Botones

        protected void btnBuscarFertilizante_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");

        }

        protected void btnSelectFertilizante_Click(object sender, EventArgs e)
        {
            if (listFertilizante.SelectedValue != "Seleccione un Fertilizante")
            {
                if (txtCantidadFerti.Text != "" && int.Parse(txtCantidadFerti.Text) > 0)
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                    int idFertilizante = int.Parse(HttpUtility.HtmlEncode(listFertilizante.SelectedValue));
                    int idGranja = lote.IdGranja;
                    int idProducto = lote.IdProducto;
                    string fchProduccion = lote.FchProduccion;

                    string cantidad = HttpUtility.HtmlEncode(txtCantidadFerti.Text);
                    Lote_Ferti loteF = new Lote_Ferti(idFertilizante, idGranja, idProducto, fchProduccion, cantidad);
                    if (Web.altaLoteFerti(loteF))
                    {
                        CargarListFertilizante(idGranja, idProducto, fchProduccion);
                        listarPagina(idGranja, idProducto, fchProduccion);
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
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idFertilizante = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;

            if (Web.bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion))
            {
                limpiar();
                listarPagina(idGranja, idProducto, fchProduccion);
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
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idFertilizante = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;
            System.Web.HttpContext.Current.Session["idFertiSel"] = idFertilizante;

            Lote_Ferti loteF = Web.buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
            Fertilizante fer = Web.buscarFerti(idFertilizante);

            txtCantidadFerti.Text = loteF.Cantidad;

            listFertilizante.SelectedValue = "Seleccione un Fertilizante";
            listFertilizante.Enabled = false;
            listFertilizante.Visible = false;


            listFertilizanteSel.DataSource = null;
            listFertilizanteSel.Enabled = false;
            listFertilizanteSel.DataSource = createDataSourceFertilizanteSel(fer);
            listFertilizanteSel.DataTextField = "nombre";
            listFertilizanteSel.DataValueField = "id";
            listFertilizanteSel.DataBind();



            txtBuscar.Visible = false;
            btnBuscar.Visible = false;
            listOrdenarPor.Visible = false;
            btnLimpiar.Visible = false;
            lstLotFertSel.Visible = false;
            lblPaginaAct.Text = "1";
            lblPaginaAct.Visible = false;




            btnSelect.Visible = false;

            btnCancelar.Visible = true;
            btnModificarCantidadFertiLote.Visible = true;
            btnBuscarFertilizante.Visible = false;
            btnBuscarFertilizante.Enabled = false;

            btnAtrasFrm.Visible = false;
            btnAtrasMod.Visible = false;

            lblMensajes.Text = "Modifique la cantidad y presione el botón \"Modificar cantidad\".";
        }

        protected void btnModificarCantidadFertiLote_Click(object sender, EventArgs e)
        {
            if (txtCantidadFerti.Text != "" && int.Parse(txtCantidadFerti.Text) > 0)
            {

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                int idFertilizante = (int)System.Web.HttpContext.Current.Session["idFertiSel"];
                int idGranja = lote.IdGranja;
                int idProducto = lote.IdProducto;
                string fchProduccion = lote.FchProduccion;
                string cantidad = HttpUtility.HtmlEncode(txtCantidadFerti.Text);

                Lote_Ferti loteF = new Lote_Ferti(idFertilizante, idGranja, idProducto, fchProduccion, cantidad);
                if (Web.modLoteFerti(loteF))
                {
                    cancelar();
                    limpiar();
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
            listFertilizante.Visible = true;
            listFertilizanteSel.Visible = false;



            txtBuscar.Visible = true;
            btnBuscar.Visible = true;
            listOrdenarPor.Visible = true;
            btnLimpiar.Visible = true;
            lstLotFertSel.Visible = true;
            lblPaginaAct.Text = "1";
            lblPaginaAct.Visible = true;



            btnSelect.Visible = true;
    
            btnBuscarFertilizante.Visible = true;
            btnBuscarFertilizante.Enabled = true;
       
            btnCancelar.Visible = false;
            btnModificarCantidadFertiLote.Visible = false;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;

            listarPagina(idGranja, idProducto, fchProduccion);


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


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);
            listarPagina(lote.IdGranja, lote.IdProducto, lote.FchProduccion);
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;


            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();
            System.Web.HttpContext.Current.Session["Buscar"] = txtBuscar.Text;


            System.Web.HttpContext.Current.Session["OrdenarPor"] = listOrdenarPor.SelectedValue;
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);
            listarPagina(lote.IdGranja, lote.IdProducto, lote.FchProduccion);
        }


        protected void listFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);
            listarPagina(lote.IdGranja, lote.IdProducto, lote.FchProduccion);
        }

        protected void listOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPaginaAct.Text = "1";
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);
            listarPagina(lote.IdGranja, lote.IdProducto, lote.FchProduccion);
        }


        #endregion

    }
}