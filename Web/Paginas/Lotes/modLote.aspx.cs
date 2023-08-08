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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idGranjaSel"] == null 
                || System.Web.HttpContext.Current.Session["idProductoSel"] ==null 
                || System.Web.HttpContext.Current.Session["fchProduccionSel"] == null)
            {
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }

        }
     static   List<Fertilizante> lstFertSelected = new List<Fertilizante>();

        public List<Fertilizante> LstFertSelected { get => lstFertSelected; set => lstFertSelected = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
         
                int idGranja = (int)System.Web.HttpContext.Current.Session["idGranjaSel"];
                int idProducto = (int)System.Web.HttpContext.Current.Session["idProductoSel"];
                string fchProduccion = System.Web.HttpContext.Current.Session["fchProduccionSel"].ToString();
                

          

                CargarListDeposito();
                CargarListFertilizante();

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


        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idFertilizanteSel"] = null;
            System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["idProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = Web.buscarLote(idGranja, idProducto, fchProduccion);
      //      Lote_Ferti loteF = Web.buscarLote_Ferti(idFertilizante, idGranja, idProducto, fchProduccion);
            txtIdGranja.Text = lote.IdGranja.ToString();
            txtIdProducto.Text = lote.IdProducto.ToString();
            txtFchProduccion.Text = DateTime.Parse(lote.FchProduccion).ToString("yyyy-MM-dd");
            txtCantidad.Text = lote.Cantidad.ToString();
            txtPrecio.Text = lote.Precio.ToString();
            listDeposito.SelectedValue = lote.IdDeposito.ToString();
      //      txtIdFertilizante.Text = loteF.IdFertilizante.ToString();
       //     txtCantidadFerti.Text = loteF.Cantidad;
        }


      

        private void guardarDatos()
        {
            System.Web.HttpContext.Current.Session["loteDatosMod"] = "Si";


            if (txtFchProduccion.Text != "")
            {
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = txtFchProduccion.Text;
            }
            System.Web.HttpContext.Current.Session["cantidadSel"] = txtCantidad.Text;
            System.Web.HttpContext.Current.Session["precioSel"] = txtPrecio.Text;
            System.Web.HttpContext.Current.Session["idDepositoSel"] = listDeposito.SelectedValue;
            System.Web.HttpContext.Current.Session["idFerCantSel"] = txtCantidadFerti.Text;

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
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
            }

            txtCantidad.Text = System.Web.HttpContext.Current.Session["cantidadSel"].ToString();
            System.Web.HttpContext.Current.Session["cantidadSel"] = null;
            txtPrecio.Text = System.Web.HttpContext.Current.Session["precioSel"].ToString();
            System.Web.HttpContext.Current.Session["precioSel"] = null;
            txtCantidadFerti.Text = System.Web.HttpContext.Current.Session["idFerCantSel"].ToString();
            System.Web.HttpContext.Current.Session[" idFerCantSel"] = null;
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



        private bool faltanDatos()
        {
            if (txtCantidad.Text == "" || txtPrecio.Text == "" || listDeposito.SelectedValue == "Seleccione un Deposito" || txtCantidadFerti.Text == "")
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
            txtCantidadFerti.Text = "";
            CargarListDeposito();
            CargarListFertilizante();
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
                dt.Rows.Add(createRow(unDeposito.Ubicacion + " " + unDeposito.Capacidad + " " + unDeposito.Temperatura + " C°", unDeposito.IdDeposito.ToString(), dt));
            }
        }

        #endregion

        #region Fertilizante

        public void CargarListFertilizante()
        {
            listFertilizante.DataSource = null;
            listFertilizante.DataSource = createDataSourceFertilizante();
            listFertilizante.DataTextField = "nombre";
            listFertilizante.DataValueField = "id";
            listFertilizante.DataBind();
        }

        ICollection createDataSourceFertilizante()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Fertilizante> fertilizantes = new List<Fertilizante>();
            if (txtBuscarFertilizante.Text == "")
            {
                fertilizantes = Web.lstFerti();
            }
            else
            {
                string value = txtBuscarFertilizante.Text.ToLower();
                fertilizantes = Web.buscarVarFerti(value);
                if (fertilizantes.Count == 0)
                {
                    lblMensajes.Text = "No se encontro ningún Fertilizante.";
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Fertilizante", "Seleccione un Fertilizante", dt));

            cargarFertilizantes(fertilizantes, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarFertilizantes(List<Fertilizante> fertilizantes, DataTable dt)
        {
            foreach (Fertilizante unFertilizante in fertilizantes)
            {
                dt.Rows.Add(createRow(unFertilizante.Nombre + " " + unFertilizante.Tipo ,unFertilizante.IdFertilizante.ToString(), dt));
            }
        }


        protected void btnBuscarFertilizante_Click(object sender, EventArgs e)
        {
            CargarListFertilizante();
        }


        protected void btnAltaFertilizante_Click(object sender, EventArgs e)
        {
            guardarDatos();
            Response.Redirect("/Paginas/Fertilizantes/frmFertilizantes");
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

              //  string cantidadFerti = HttpUtility.HtmlEncode(txtCantidadFerti.Text);

                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Lote unLote = new Lote(idGranja, idProducto, fchProduccion, cantidad, precio, idDeposito);
               
                foreach (Fertilizante fer in MlstFertSelected())
                {
                    Lote_Ferti unLoteF = new Lote_Ferti(fer.IdFertilizante, idGranja, idProducto, fchProduccion, "100");


                    if (Web.modLote(unLote, unLoteF))
                    {
                        limpiar();
                        lblMensajes.Text = "Lote modificado con éxito.";

                    }
                    else
                    {
                        lblMensajes.Text = "No se pudo modificar el lote.";
                    }
                }

                limpiarIdSession();
                Response.Redirect("/Paginas/Lotes/frmLotes");
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }

        private void listar()
        {
            lstFertSel.DataSource = null;
            lstFertSel.DataSource = MlstFertSelected();
            lstFertSel.DataBind();
          
        }

        private List<Fertilizante> MlstFertSelected()
        {
            return LstFertSelected;
        }

        public void listarFertSel()
        {
            if (listFertilizante.SelectedValue != "Seleccione un Fertilizante")
            {
                int idFertilizante = int.Parse(HttpUtility.HtmlEncode(listFertilizante.SelectedValue));
                ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                Fertilizante fert = Web.buscarFerti(idFertilizante);
                if (fert != null)
                {
                    LstFertSelected.Add(fert);
                   
                }
               
              
            }

        }

        //public DataTable listarFertSel()
        //{

        //    //DataTable dt = new DataTable();
        //    //dt.Columns.AddRange(new DataColumn[2]{
        //    //    new DataColumn("IdFertilizante", typeof(int)),
        //    //    new DataColumn("Nombre", typeof(string))
        //    //});
        //    int idFertilizante = int.Parse(HttpUtility.HtmlEncode(listFertilizante.SelectedValue));
        //    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
        //    Fertilizante fert = Web.buscarFerti(idFertilizante);
        //    if(fert != null ) 
        //    {
        //        lstFertSelected.Add(fert);
        //        foreach (Fertilizante fer in lstFertSelected)
        //        {
        //            DataRow dr = dt.NewRow();

        //            dr["IdFertilizante"] = fer.IdFertilizante;
        //            dr["Nombre"] = fer.Nombre;


        //            dt.Rows.Add(dr);
        //        }
        //    }

        //    return dt;
        //}

        protected void btnSelectFertilizante_Click(object sender, EventArgs e)
        {
            listarFertSel();
            listar();
        }
           
    }
}