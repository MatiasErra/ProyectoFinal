using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;
using Web.Paginas.Productos;

namespace Web.Paginas.Lotes
{
    public partial class frmLotes : System.Web.UI.Page
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
                else if (admin.TipoDeAdmin == "Administrador de pedidos")
                {
                    this.MasterPageFile = "~/Master/APedidos.Master";
                }
            }
            else
            {
                Response.Redirect("/Paginas/Nav/frmInicio");
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

                System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
                System.Web.HttpContext.Current.Session["idProductoSel"] = null;
                System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
                if (System.Web.HttpContext.Current.Session["LoteAlta"] != null)
                {
                    lblMensajes.Text = "Lote Añadido con éxito";
                    System.Web.HttpContext.Current.Session["LoteAlta"] = null;
                }

                if (System.Web.HttpContext.Current.Session["LoteMod"] != null)
                {
                    lblMensajes.Text = "Lote Modificado";
                    System.Web.HttpContext.Current.Session["LoteMod"] = null;
                }


                CargarListGranja();
                CargarListProducto();
                CargarListDeposito();
                CargarListBuscar();
                CargarListOrdenarPor();

                // Buscador
                txtFchProdMenor.Text = System.Web.HttpContext.Current.Session["fchProdLoteMenorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchProdLoteMenorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchProdLoteMenorBuscar"] = null;
                txtFchProdMayor.Text = System.Web.HttpContext.Current.Session["fchProdLoteMayorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchProdLoteMayorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchProdLoteMayorBuscar"] = null;
                txtFchCadMenor.Text = System.Web.HttpContext.Current.Session["fchCadLoteMenorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchCadLoteMenorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchCadLoteMenorBuscar"] = null;
                txtFchCadMayor.Text = System.Web.HttpContext.Current.Session["fchCadLoteMayorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["fchCadLoteMayorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["fchCadLoteMayorBuscar"] = null;

                txtPrecioMenor.Text = System.Web.HttpContext.Current.Session["precioLoteMenorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["precioLoteMenorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["precioLoteMenorBuscar"] = null;
                txtPrecioMayor.Text = System.Web.HttpContext.Current.Session["precioLoteMayorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["precioLoteMayorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["precioLoteMayorBuscar"] = null;
                txtCantMenor.Text = System.Web.HttpContext.Current.Session["cantLoteMenorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["cantLoteMenorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["cantLoteMenorBuscar"] = null;
                txtCantMayor.Text = System.Web.HttpContext.Current.Session["cantLoteMayorBuscar"] != null ? DateTime.Parse(System.Web.HttpContext.Current.Session["cantLoteMayorBuscar"].ToString()).ToString("yyyy-MM-dd") : "";
                System.Web.HttpContext.Current.Session["cantLoteMayorBuscar"] = null;

                // Listas
                lstGranjaBuscar.SelectedValue = System.Web.HttpContext.Current.Session["granjaLoteBuscar"] != null ? System.Web.HttpContext.Current.Session["granjaLoteBuscar"].ToString() : "Seleccione una Granja";
                System.Web.HttpContext.Current.Session["granjaLoteBuscar"] = null;
                lstProductoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["productoLoteBuscar"] != null ? System.Web.HttpContext.Current.Session["productoLoteBuscar"].ToString() : "Seleccione un Producto";
                System.Web.HttpContext.Current.Session["productoLoteBuscar"] = null;
                lstDepositoBuscar.SelectedValue = System.Web.HttpContext.Current.Session["depositoLoteBuscar"] != null ? System.Web.HttpContext.Current.Session["depositoLoteBuscar"].ToString() : "Seleccione un Deposito";
                System.Web.HttpContext.Current.Session["depositoLoteBuscar"] = null;
                listBuscarPor.SelectedValue = System.Web.HttpContext.Current.Session["BuscarLstLote"] != null ? System.Web.HttpContext.Current.Session["BuscarLstLote"].ToString() : "Buscar por";
                System.Web.HttpContext.Current.Session["BuscarLstLote"] = null;
                listOrdenarPor.SelectedValue = System.Web.HttpContext.Current.Session["OrdenarPorLote"] != null ? System.Web.HttpContext.Current.Session["OrdenarPorLote"].ToString() : "Ordernar por";
                System.Web.HttpContext.Current.Session["OrdenarPorLote"] = null;


                comprobarBuscar();
                listarPagina();


            }
        }
        #endregion

        #region Utilidad

        public DataTable ObtenerDatos(List<Lote> lotes)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] {
                new DataColumn("NombreGranja", typeof(string)),
                new DataColumn("NombreProducto", typeof(string)),
                new DataColumn("FchProduccion", typeof(string)),
                new DataColumn("FchCaducidad", typeof(string)),
                new DataColumn("Cantidad", typeof(string)),
                new DataColumn("Precio", typeof(string)),
                new DataColumn("UbicacionDeposito", typeof(string))});

            foreach (Lote unLote in lotes)
            {
                DataRow dr = dt.NewRow();
                dr["NombreGranja"] = unLote.NombreGranja.ToString();
                dr["NombreProducto"] = unLote.NombreProducto.ToString();
                dr["FchProduccion"] = unLote.FchProduccion.ToString();
                dr["FchCaducidad"] = unLote.FchCaducidad.ToString();
                dr["Cantidad"] = unLote.Cantidad.ToString();
                dr["Precio"] = unLote.Precio.ToString() + " $";
                dr["UbicacionDeposito"] = unLote.UbicacionDeps.ToString();

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private string CantTotalProd(string nomProducto, string cantidadAdd)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            int cant = 0;
            Lote lote = new Lote(0, 0, "", "", "", 0, 0);
            List<Lote> lotes = Web.buscarFiltrarLotes(lote, 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");




            foreach (Lote unlotes in lotes)
            {

                if (unlotes.NombreProducto.Equals(nomProducto))
                {
                    string textCant1 = unlotes.Cantidad.ToString();
                    string[] str = textCant1.Split(' ');
                    textCant1 = str[0];
                    cant += int.Parse(textCant1);



                }

            }
            string[] textCant2 = cantidadAdd.Split(' ');


            int cantidad = int.Parse(textCant2[0].ToString());
            int total = cant - cantidad;
            string resultado = total.ToString() + " " + textCant2[1];
            return resultado;
        }

        private void limpiar()
        {
            lblMensajes.Text = "";
            lblPaginaAct.Text = "1";

            txtCantMenor.Text = "";
            txtCantMayor.Text = "";
            txtPrecioMenor.Text = "";
            txtPrecioMayor.Text = "";
            txtFchCadMenor.Text = "";
            txtFchCadMayor.Text = "";
            txtFchProdMenor.Text = "";
            txtFchProdMayor.Text = "";
            lstGranjaBuscar.SelectedValue = "Seleccione una Granja";
            lstProductoBuscar.SelectedValue = "Seleccione un Producto";
            lstDepositoBuscar.SelectedValue = "Seleccione un Deposito";
            listBuscarPor.SelectedValue = "Buscar por";
            listOrdenarPor.SelectedValue = "Ordenar por";
            comprobarBuscar();
            lblPaginaAct.Text = "1";
            listarPagina();
        }

        private void guardarBuscar()
        {
            System.Web.HttpContext.Current.Session["fchProdLoteMenorBuscar"] = txtFchProdMenor.Text != "" ? txtFchProdMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchProdLoteMayorBuscar"] = txtFchProdMayor.Text != "" ? txtFchProdMayor.Text : null;
            System.Web.HttpContext.Current.Session["fchCadLoteMenorBuscar"] = txtFchCadMenor.Text != "" ? txtFchCadMenor.Text : null;
            System.Web.HttpContext.Current.Session["fchCadLoteMayorBuscar"] = txtFchCadMayor.Text != "" ? txtFchCadMayor.Text : null;

            System.Web.HttpContext.Current.Session["precioLoteMenorBuscar"] = txtPrecioMenor.Text != "" ? txtPrecioMenor.Text : null;
            System.Web.HttpContext.Current.Session["precioLoteMayorBuscar"] = txtPrecioMayor.Text != "" ? txtPrecioMayor.Text : null;
            System.Web.HttpContext.Current.Session["cantLoteMenorBuscar"] = txtCantMenor.Text != "" ? txtCantMenor.Text : null;
            System.Web.HttpContext.Current.Session["cantLoteMayorBuscar"] = txtCantMayor.Text != "" ? txtCantMayor.Text : null;

            System.Web.HttpContext.Current.Session["granjaLoteBuscar"] = lstGranjaBuscar.SelectedValue != "Seleccione una Granja" ? lstGranjaBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["productoLoteBuscar"] = lstProductoBuscar.SelectedValue != "Seleccione un Producto" ? lstProductoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["depositoLoteBuscar"] = lstDepositoBuscar.SelectedValue != "Seleccione un Deposito" ? lstDepositoBuscar.SelectedValue : null;
            System.Web.HttpContext.Current.Session["BuscarLstLote"] = listBuscarPor.SelectedValue != "Buscar por" ? listBuscarPor.SelectedValue : null;
            System.Web.HttpContext.Current.Session["OrdenarPorLote"] = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : null;
        }

        private void comprobarBuscar()
        {
            lstGranjaBuscar.Visible = listBuscarPor.SelectedValue == "Granja" ? true : false;
            lstProductoBuscar.Visible = listBuscarPor.SelectedValue == "Producto" ? true : false;
            lstDepositoBuscar.Visible = listBuscarPor.SelectedValue == "Deposito" ? true : false;
            lblFchProd.Visible = listBuscarPor.SelectedValue == "Fecha de producción" ? true : false;
            lblFchCad.Visible = listBuscarPor.SelectedValue == "Fecha de caducidad" ? true : false;
            lblCant.Visible = listBuscarPor.SelectedValue == "Cantidad" ? true : false;
            lblPrecio.Visible = listBuscarPor.SelectedValue == "Precio" ? true : false;

        }

        #region Paginas

        private int PagMax()
        {
            return 4;
        }


        private List<Lote> obtenerLote()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote lote = new Lote();
            lote.IdGranja = lstGranjaBuscar.SelectedValue != "Seleccione una Granja" ? int.Parse(lstGranjaBuscar.SelectedValue) : 0;
            lote.IdProducto = lstProductoBuscar.SelectedValue != "Seleccione un Producto" ? int.Parse(lstProductoBuscar.SelectedValue) : 0;
            lote.IdDeposito = lstDepositoBuscar.SelectedValue != "Seleccione un Deposito" ? int.Parse(lstDepositoBuscar.SelectedValue) : 0;
            double precioMenor = txtPrecioMenor.Text == "" ? 0 : double.Parse(txtPrecioMenor.Text);
            double precioMayor = txtPrecioMayor.Text == "" ? 99999999 : double.Parse(txtPrecioMayor.Text);
            long cantidadMenor = txtCantMenor.Text == "" ? 0 : long.Parse(txtCantMenor.Text);
            long cantidadMayor = txtCantMayor.Text == "" ? 9999999999 : long.Parse(txtCantMayor.Text);
            string fchProduccionMenor = txtFchProdMenor.Text == "" ? "1000-01-01" : txtFchProdMenor.Text;
            string fchProduccionMayor = txtFchProdMayor.Text == "" ? "3000-12-30" : txtFchProdMayor.Text;
            string fchCaducidadMenor = txtFchCadMenor.Text == "" ? "1000-01-01" : txtFchCadMenor.Text;
            string fchCaducidadMayor = txtFchCadMayor.Text == "" ? "3000-12-30" : txtFchCadMayor.Text;
            string ordenar = listOrdenarPor.SelectedValue != "Ordenar por" ? listOrdenarPor.SelectedValue : "";

            List<Lote> lotes = Web.buscarFiltrarLotes(lote, precioMenor, precioMayor, fchProduccionMenor, fchProduccionMayor, fchCaducidadMenor, fchCaducidadMayor, ordenar);

            List<Lote> mostrar = new List<Lote>();

            foreach (Lote unLote in lotes)
            {
                int cantLote = int.Parse(unLote.Cantidad.Split(' ')[0]);
                if (cantLote >= cantidadMenor && cantLote <= cantidadMayor)
                {
                    mostrar.Add(unLote);
                }
            }

            return mostrar;
        }
        private void listarPagina()
        {
            List<Lote> lotes = obtenerLote();
            List<Lote> LotesPagina = new List<Lote>();

            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            int cont = 0;
            foreach (Lote unLote in lotes)
            {
                if (LotesPagina.Count == PagMax())
                {
                    break;
                }
                if (cont >= ((pagina * PagMax()) - PagMax()))
                {
                    LotesPagina.Add(unLote);
                }

                cont++;
            }

            if (LotesPagina.Count == 0)
            {
                lblPaginas.Visible = false;
                lblMensajes.Text = "No se encontro ningún Lote.";

                lblPaginaAnt.Visible = false;
                lblPaginaAct.Visible = false;
                lblPaginaSig.Visible = false;
                lstLote.Visible = false;
            }
            else
            {
                lblPaginas.Visible = true;
                lblMensajes.Text = "";
                modificarPagina();
                lstLote.Visible = true;
                lstLote.DataSource = null;
                lstLote.DataSource = ObtenerDatos(LotesPagina);
                lstLote.DataBind();
            }


        }

        private void modificarPagina()
        {
            List<Lote> lotes = obtenerLote();
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

        #endregion

        #region DropDownBoxes

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
            dt.Rows.Add(createRow("Granja", "Granja", dt));
            dt.Rows.Add(createRow("Producto", "Producto", dt));
            dt.Rows.Add(createRow("Fecha de producción", "Fecha de producción", dt));
            dt.Rows.Add(createRow("Fecha de caducidad", "Fecha de caducidad", dt));
            dt.Rows.Add(createRow("Cantidad de producción", "Cantidad de producción", dt));
            dt.Rows.Add(createRow("Precio", "Precio", dt));
            dt.Rows.Add(createRow("Depósito", "Depósito", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Granja

        public void CargarListGranja()
        {
            lstGranjaBuscar.DataSource = null;
            lstGranjaBuscar.DataSource = createDataSourceGranja();
            lstGranjaBuscar.DataTextField = "nombre";
            lstGranjaBuscar.DataValueField = "id";
            lstGranjaBuscar.DataBind();
        }

        ICollection createDataSourceGranja()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Granja gra = new Granja(0, "", "", 0);
            List<Granja> granjas = Web.buscarGranjaFiltro(gra, "");
            DataTable dt = new DataTable();



            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));


            dt.Rows.Add(createRow("Seleccione una Granja", "Seleccione una Granja", dt));
            cargarGranjas(granjas, dt);


            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarGranjas(List<Granja> granjas, DataTable dt)
        {
            foreach (Granja unaGranja in granjas)
            {
                dt.Rows.Add(createRow(unaGranja.Nombre + " " + unaGranja.Ubicacion, unaGranja.IdGranja.ToString(), dt));
            }
        }

        #endregion

        #region Producto



        public void CargarListProducto()
        {
            lstProductoBuscar.DataSource = null;
            lstProductoBuscar.DataSource = createDataSourceProducto();
            lstProductoBuscar.DataTextField = "nombre";
            lstProductoBuscar.DataValueField = "id";
            lstProductoBuscar.DataBind();
        }

        ICollection createDataSourceProducto()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Producto> productos = new List<Producto>();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            Producto pro = new Producto(); 
            pro.Nombre = "";
            pro.Tipo = "";
            pro.TipoVenta = "";
           int precioMenor = 0;
            int precioMayor = 999999;
          productos = Web.buscarProductoFiltro(pro, precioMenor, precioMayor, "");
            dt.Rows.Add(createRow("Seleccione un Producto", "Seleccione un Producto", dt));

            cargarProductos(productos, dt);

            DataView dv = new DataView(dt);
            return dv;
        }

        private void cargarProductos(List<Producto> productos, DataTable dt)
        {
            foreach (Producto unProducto in productos)
            {
                dt.Rows.Add(createRow(unProducto.Nombre + " " + unProducto.Tipo, unProducto.IdProducto.ToString(), dt));
            }
        }

        #endregion

        #region Deposito

        public void CargarListDeposito()
        {
            lstDepositoBuscar.DataSource = null;
            lstDepositoBuscar.DataSource = createDataSourceDeposito();
            lstDepositoBuscar.DataTextField = "nombre";
            lstDepositoBuscar.DataValueField = "id";
            lstDepositoBuscar.DataBind();
        }

        ICollection createDataSourceDeposito()
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Deposito> depositos = new List<Deposito>();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            Deposito dep = new Deposito(0, "", "", 0, "");
            depositos = Web.buscarDepositoFiltro(dep, 0, 0, -1, -1, "");
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

        #region Buscador

        public void CargarListBuscar()
        {
            listBuscarPor.DataSource = null;
            listBuscarPor.DataSource = createDataSourceBuscar();
            listBuscarPor.DataTextField = "nombre";
            listBuscarPor.DataValueField = "id";
            listBuscarPor.DataBind();
        }

        ICollection createDataSourceBuscar()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Buscar por", "Buscar por", dt));
            dt.Rows.Add(createRow("Granja", "Granja", dt));
            dt.Rows.Add(createRow("Producto", "Producto", dt));
            dt.Rows.Add(createRow("Fecha de producción", "Fecha de producción", dt));
            dt.Rows.Add(createRow("Fecha de caducidad", "Fecha de caducidad", dt));
            dt.Rows.Add(createRow("Precio", "Precio", dt));
            dt.Rows.Add(createRow("Cantidad", "Cantidad", dt));
            dt.Rows.Add(createRow("Deposito", "Deposito", dt));

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                if (DateTime.Parse(txtFchProdMenor.Text) <= DateTime.Parse(txtFchProdMayor.Text)) num++;
            }
            catch
            {
                num++;
            }

            if (num == 1)
            {
                try
                {
                    if (DateTime.Parse(txtFchCadMenor.Text) <= DateTime.Parse(txtFchCadMayor.Text)) num++;
                }
                catch
                {
                    num++;
                }
                if (num == 2)
                {
                    try
                    {
                        if (double.Parse(txtPrecioMenor.Text) <= double.Parse(txtPrecioMayor.Text)) num++;
                    }
                    catch
                    {
                        num++;
                    }
                    if (num == 3)
                    {
                        try
                        {
                            if (long.Parse(txtCantMenor.Text) <= long.Parse(txtCantMayor.Text)) num++;
                        }
                        catch
                        {
                            num++;
                        }
                        if (num == 4)
                        {
                            lblPaginaAct.Text = "1";
                            listarPagina();
                        }
                        else
                        {
                            lblMensajes.Text = "La cantidad menor es mayor.";
                            listBuscarPor.SelectedValue = "Cantidad";
                            comprobarBuscar();
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "El precio menor es mayor.";
                        listBuscarPor.SelectedValue = "Precio";
                        comprobarBuscar();
                    }
                }
                else
                {
                    lblMensajes.Text = "La fecha de caducidad menor es mayor.";
                    listBuscarPor.SelectedValue = "Fecha de caducidad";
                    comprobarBuscar();
                }
            }
            else
            {
                lblMensajes.Text = "La fecha de producción menor es mayor.";
                listBuscarPor.SelectedValue = "Fecha de producción";
                comprobarBuscar();
            }
        }

        protected void listBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comprobarBuscar();
        }

        protected void lblPaginaAnt_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina - 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void lblPaginaSig_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            int pagina = int.Parse(p);
            System.Web.HttpContext.Current.Session["PagAct"] = (pagina + 1).ToString();

            guardarBuscar();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);
            string cantidad = HttpUtility.HtmlEncode(selectedrow.Cells[4].Text);

            string CantTotal = CantTotalProd(nombreProducto, cantidad);

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Lote unLote = Web.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            if (unLote != null)
            {

                if (Web.bajaLote(nombreGranja, nombreProducto, fchProduccion, CantTotal))
                {
                    limpiar();
                    lblPaginaAct.Text = "1";
                    listarPagina();
                    lblMensajes.Text = "Se ha borrado el lote.";
                }
                else
                {
                    lblMensajes.Text = "No se ha podido borrar el lote.";
                }
            }
            else
            {
                lblMensajes.Text = "El lote no existe.";
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["PagAct"] = "1";

            guardarBuscar();

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/modLote");
        }

        protected void btnVerPestis_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            System.Web.HttpContext.Current.Session["PagAct"] = p;

            guardarBuscar();

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesPestis");
        }


        protected void btnVerFertis_Click(object sender, EventArgs e)
        {
            string p = lblPaginaAct.Text.ToString();
            System.Web.HttpContext.Current.Session["PagAct"] = p;

            guardarBuscar();

            Button btnConstultar = (Button)sender;
            GridViewRow selectedrow = (GridViewRow)btnConstultar.NamingContainer;
            string nombreGranja = HttpUtility.HtmlEncode(selectedrow.Cells[0].Text);
            string nombreProducto = HttpUtility.HtmlEncode(selectedrow.Cells[1].Text);
            string fchProduccion = HttpUtility.HtmlEncode(selectedrow.Cells[2].Text);

            System.Web.HttpContext.Current.Session["nombreGranjaSel"] = nombreGranja;
            System.Web.HttpContext.Current.Session["nombreProductoSel"] = nombreProducto;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = fchProduccion;

            Response.Redirect("/Paginas/Lotes/frmLotesFertis");
        }



        protected void btnAltaLot_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Lotes/frmAltaLotes");

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            listarPagina();
        }



        #endregion

    }
}