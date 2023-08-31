using Clases;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Lotes
{
    public partial class frmLotesPestis : System.Web.UI.Page
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

                if (System.Web.HttpContext.Current.Session["lotePestiDatos"] != null)
                {
                    cargarDatos();
                }
                CargarLote(nombreGranja, nombreProducto, fchProduccion);
                CargarListPesticida(lote.IdGranja, lote.IdProducto, fchProduccion);


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

        #region DropDownBoxes

        #region Pesticida

        public void CargarListPesticida(int idGranja, int idProducto, string fchProduccion)
        {
            listPesticida.DataSource = null;
            listPesticida.DataSource = createDataSourcePesticida(idGranja, idProducto, fchProduccion);
            listPesticida.DataTextField = "nombre";
            listPesticida.DataValueField = "id";
            listPesticida.DataBind();
        }

        ICollection createDataSourcePesticida(int idGranja, int idProducto, string fchProduccion)
        {
            listPesticida.Visible = true;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Pesticida> pesticidas = new List<Pesticida>();
            List<string[]> mostrar = new List<string[]>();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            if (txtBuscarPesticida.Text == "")
            {
                dt.Rows.Add(createRow("Seleccione un Pesticida", "Seleccione un Pesticida", dt));
                string value = "";
                string impact = "";
                string ordenar = "";

                pesticidas = Web.buscarPesticidaFiltro(value, impact, ordenar);
            }
            else
            {
                string value = txtBuscarPesticida.Text.ToLower();
                string impact = "";
                string ordenar = "";

                pesticidas = Web.buscarPesticidaFiltro(value, impact, ordenar);

            }
            if (pesticidas.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Pesticida.";
                listPesticida.Visible = false;
            }
            else
            {
                listPesticida.Visible = true;
                string b = "";
                string o = "";
                List<Lote_Pesti> filtro = Web.PestisEnLote(idGranja, idProducto, fchProduccion,b,o);
                foreach (Pesticida unPesti in pesticidas)
                {
                    int cont = 0;
                    foreach (Lote_Pesti pestL in filtro)
                    {
                        if (pestL.IdProducto.Equals(unPesti.IdPesticida))
                        {
                            cont++;
                        }
                    }
                    if (cont == 0)
                    {
                        string[] pest = new string[5];
                        pest[0] = unPesti.IdPesticida.ToString();
                        pest[1] = unPesti.Nombre;
                        pest[2] = unPesti.Tipo;
                        pest[3] = unPesti.PH.ToString();
                        pest[4] = unPesti.Impacto;
                        mostrar.Add(pest);
                    }
                }

                if (mostrar.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningún Pesticida sin añadir.";
                    listPesticida.Visible = false;
                }
                else
                {
                    listPesticida.Visible = true;
                    cargarPesticidas(mostrar, dt);
                }
            }
            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarPesticidas(List<string[]> pesticidas, DataTable dt)
        {
            foreach (string[] unPesticida in pesticidas)
            {
                dt.Rows.Add(createRow(unPesticida[1] + " " + unPesticida[2], unPesticida[0], dt));
            }
        }


        ICollection createDataSourcePesticidaSel(Pesticida pest)
        {
            listPesticidaSel.Visible = true;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow(pest.Nombre.ToString() + " " + pest.Tipo.ToString(), pest.IdPesticida.ToString(), dt));
            DataView dv = new DataView(dt);
            return dv;
        }

        protected void btnAltaPesticida_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Pesticidas/frmPesticidas");
        }

        protected void btnBuscarPesticida_Click(object sender, EventArgs e)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;

            CargarListPesticida(idGranja, idProducto, fchProduccion);
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

        private int PagMax()
        {

            return 2;
        }



        private List<Lote_Pesti> obtenerLote_Pesti(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            string buscar = txtBuscar.Text;

            string ordenar = "";


            if (listOrdenarPor.SelectedValue != "Ordenar por")
            {
                ordenar = listOrdenarPor.SelectedValue;
            }



            List<Lote_Pesti> Lote_Pesti = Web.PestisEnLote(idGranja, idProducto, fchProduccion, buscar, ordenar);

            return Lote_Pesti;
        }


        private void listarPagina(int idGranja, int idProducto, string fchProduccion)
        {
            List<Lote_Pesti> lotes_pest = obtenerLote_Pesti(idGranja, idProducto, fchProduccion);
            List<Lote_Pesti> Lotes_pestPagina = new List<Lote_Pesti>();

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Lote_Pesti unLote in lotes_pest)
            {
                if (Lotes_pestPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    Lotes_pestPagina.Add(unLote);
                }

                cont++;
            }

            if (Lotes_pestPagina.Count == 0)
            {
                lblMensajes.Text = "No se encontro ningún Pesticida en el lote.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstLotPestiSel.Visible = false;


                txtBuscar.Visible = false;
                btnBuscar.Visible = false;
                listOrdenarPor.Visible = false;
                btnLimpiar.Visible = false;

            }
            else
            {
                lblMensajes.Text = "";
                txtBuscar.Visible = true;
                btnBuscar.Visible = true;
                listOrdenarPor.Visible = true;
                btnLimpiar.Visible = true;


                modificarPagina(idGranja, idProducto, fchProduccion);
                lstLotPestiSel.Visible = true;
                lstLotPestiSel.DataSource = null;
                lstLotPestiSel.DataSource = ObtenerDatos(Lotes_pestPagina);
                lstLotPestiSel.DataBind();
            }


        }

        private void modificarPagina(int idGranja, int idProducto, string fchProduccion)
        {
            List<Lote_Pesti> lotes = obtenerLote_Pesti(idGranja, idProducto, fchProduccion);
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
            lblPaginaAnt.Text = (int.Parse(pagAct) - 1).ToString();
            lblPaginaAct.Text = pagAct.ToString();
            lblPaginaSig.Text = (int.Parse(pagAct) + 1).ToString();
        }





        public DataTable ObtenerDatos(List<Lote_Pesti> pesticidas)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
            new DataColumn("IdPesticida", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Tipo", typeof(string)),
             new DataColumn("Cantidad", typeof(string)),
            new DataColumn("PH", typeof(string))});
            foreach (Lote_Pesti str in pesticidas)
            {

                DataRow dr = dt.NewRow();
                dr["IdPesticida"] = str.IdPesticida;
                dr["Nombre"] = str.NombrePesti;
                dr["Tipo"] = str.TipoPesti;
                dr["PH"] = str.PHPesti;
                dr["Cantidad"] = str.Cantidad;

                dt.Rows.Add(dr);
            }
            return dt;


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
        private void limpiar()
        {
            lblMensajes.Text = "";
            txtBuscar.Text = "";
            listOrdenarPor.SelectedValue = "Ordenar por";
            lblPaginaAct.Text = "1";


        }


        protected void btnSelectPesticida_Click(object sender, EventArgs e)
        {
            if (listPesticida.SelectedValue != "Seleccione un Pesticida")
            {
                if (txtCantidadPesti.Text != "" && int.Parse(txtCantidadPesti.Text) > 0)
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                    int idPesticida = int.Parse(HttpUtility.HtmlEncode(listPesticida.SelectedValue));
                    int idGranja = lote.IdGranja;
                    int idProducto = lote.IdProducto;
                    string fchProduccion = lote.FchProduccion;

                    string cantidad = HttpUtility.HtmlEncode(txtCantidadPesti.Text);
                    Lote_Pesti loteP = new Lote_Pesti(idPesticida, idGranja, idProducto, fchProduccion, cantidad);
                    if (Web.altaLotePesti(loteP))
                    {
                        CargarListPesticida(idGranja, idProducto, fchProduccion);
                        listarPagina(idGranja, idProducto, fchProduccion);
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
                    lblMensajes.Text = "Debe ingresar una cantidad o la cantidad debe ser mayor a cero.";
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
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idPesticida = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;

            if (Web.bajaLotePesti(idPesticida, idGranja, idProducto, fchProduccion))
            {
                limpiar();
                listarPagina(idGranja, idProducto, fchProduccion);
                CargarListPesticida(idGranja, idProducto, fchProduccion);
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
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

            int idPesticida = int.Parse(HttpUtility.HtmlEncode(selectedrow.Cells[0].Text));
            int idGranja = lote.IdGranja;
            int idProducto = lote.IdProducto;
            string fchProduccion = lote.FchProduccion;

            System.Web.HttpContext.Current.Session["idPestiSel"] = idPesticida;

            Lote_Pesti loteP = Web.buscarLotePesti(idPesticida, idGranja, idProducto, fchProduccion);
            Pesticida pesti = Web.buscarPesti(idPesticida);

            txtCantidadPesti.Text = loteP.Cantidad;


            txtBuscar.Visible = false;
            btnBuscar.Visible = false;
            listOrdenarPor.Visible = false;
            btnLimpiar.Visible = false;
            lstLotPestiSel.Visible = false;
            lblPaginaAct.Text = "1";
            lblPaginaAct.Visible = false;

            listPesticida.Visible = false;
            listPesticida.Enabled = false;
            listPesticidaSel.DataSource = null;
            listPesticidaSel.Enabled = false;
            listPesticidaSel.DataSource = createDataSourcePesticidaSel(pesti);
            listPesticidaSel.DataTextField = "nombre";
            listPesticidaSel.DataValueField = "id";
            listPesticidaSel.DataBind();
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
            if (txtCantidadPesti.Text != "" && int.Parse(txtCantidadPesti.Text) > 0)
            {
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote lote = Web.buscarLote(txtGranja.Text, txtProducto.Text, txtFechProd.Text);

                int idPesticida = (int)System.Web.HttpContext.Current.Session["idPestiSel"];
                int idGranja = lote.IdGranja;
                int idProducto = lote.IdProducto;
                string fchProduccion = lote.FchProduccion;
                string cantidad = HttpUtility.HtmlEncode(txtCantidadPesti.Text);

                Lote_Pesti loteP = new Lote_Pesti(idPesticida, idGranja, idProducto, fchProduccion, cantidad);
                if (Web.modLotePesti(loteP))
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
            System.Web.HttpContext.Current.Session["idPestiSel"] = null;
            txtCantidadPesti.Text = "";
            listPesticidaSel.Visible = false;
            listPesticida.Visible = true;
            listPesticida.Enabled = true;



            txtBuscar.Visible = true;
            btnBuscar.Visible = true;
            listOrdenarPor.Visible = true;
            btnLimpiar.Visible = true;
            lstLotPestiSel.Visible = true;
            lblPaginaAct.Text = "1";
            lblPaginaAct.Visible = true;





            btnSelect.Visible = true;
            btnAltaPesticida.Visible = true;
            btnBuscarPesticida.Visible = true;
            btnBuscarPesticida.Enabled = true;
            txtBuscarPesticida.Visible = true;
            btnCancelar.Visible = false;
            btnModificarCantidadPestiLote.Visible = false;

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