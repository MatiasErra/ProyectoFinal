using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas.Admins
{
    public partial class frmEstadisticas : System.Web.UI.Page
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
                else
                {
                    Response.Redirect("/Paginas/Nav/frmInicio");
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
                CargarListEstadisticas();
                CargarListMes();

                comprobarEstadisticas();
            }

        }

        #endregion

        #region Utilidad

        private void limpiar()
        {
            txtAnioGananciaBuscar.Text = "";
            lstMesGananciaBuscar.SelectedValue = "Seleccione un Mes";
            lblMensajes.Text = "";
            lblGanancias.Text = "";

        }

        private void comprobarEstadisticas()
        {
            lblGananciaMesAnio.Visible = listEstadisticas.SelectedValue == "Ganancias" ? true : false;
            lstMesGananciaBuscar.Visible = listEstadisticas.SelectedValue == "Ganancias" ? true : false;
            lblGanancias.Visible = listEstadisticas.SelectedValue == "Ganancias" ? true : false;
        }

        private void obtenerGanancia()
        {
            int anio = int.Parse(txtAnioGananciaBuscar.Text);
            int mes = lstMesGananciaBuscar.SelectedIndex;
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Pedido> pedidos = Web.BuscarPedidoFiltro("", "", "", 0, 999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
            List<Pedido> ganancia = new List<Pedido>();
            foreach (Pedido unPedido in pedidos)
            {
                if (unPedido.Estado == "Finalizado")
                {
                    string[] fecha = unPedido.FechaEntre.Split(' ')[0].Split('/');

                    if (mes == 0)
                    {
                        if (int.Parse(fecha[2]) == anio)
                        {
                            ganancia.Add(unPedido);
                        }
                    }
                    else
                    {
                        if (int.Parse(fecha[2]) == anio && int.Parse(fecha[1]) == mes)
                        {
                            ganancia.Add(unPedido);
                        }
                    }
                }
            }

            double totalPedido = 0;
            double totalLote = 0;
            double totalViaje = 0;

            foreach (Pedido unPedido in ganancia)
            {
                totalPedido += unPedido.Costo;

            }

            Lote lote = new Lote(0, 0, "", "", "", 0, 0);
            List<Lote> lotes = Web.buscarFiltrarLotes(lote, 0, 99999999, "1000-01-01", "3000-12-30", "1000-01-01", "3000-12-30", "");
            foreach (Lote unLote in lotes)
            {
                int cont = 0;
                string[] fchProd = unLote.FchProduccion.Split(' ')[0].Split('/');

                if (mes == 0)
                {
                    if (int.Parse(fchProd[2]) == anio)
                    {
                        cont++;
                    }
                }
                else
                {
                    if (int.Parse(fchProd[2]) == anio && int.Parse(fchProd[1]) == mes)
                    {
                        cont++;
                    }
                }

                if (cont == 1)
                {
                    totalLote += int.Parse(unLote.Cantidad.Split(' ')[0]) * unLote.Precio;
                }


            }
            Viaje unVia = new Viaje(0, 0, "", 0, 0, "");
            List<Viaje> viajes = Web.buscarViajeFiltro(unVia, 0, 999999, "1000-01-01", "3000-12-30", "");
            foreach (Viaje unViaje in viajes)
            {
                int cont = 0;

                string[] fchVia = unViaje.Fecha.Split(' ')[0].Split('/');

                if (mes == 0)
                {
                    if (int.Parse(fchVia[2]) == anio)
                    {
                        cont++;
                    }
                }
                else
                {
                    if (int.Parse(fchVia[2]) == anio && int.Parse(fchVia[1]) == mes)
                    {
                        cont++;
                    }
                }

                if (cont == 1)
                {
                    totalViaje += unViaje.Costo;
                }
            }


            lblGanancias.Text = "La ganancia total de esta fecha es de $" + (totalPedido - (totalViaje + totalLote));

        }

        #region DropDownBoxes

        #region Estadisticas

        public void CargarListEstadisticas()
        {
            listEstadisticas.DataSource = null;
            listEstadisticas.DataSource = createDataSourceEstadisticas();
            listEstadisticas.DataTextField = "nombre";
            listEstadisticas.DataValueField = "id";
            listEstadisticas.DataBind();
        }

        ICollection createDataSourceEstadisticas()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Ganancias", "Ganancias", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Mes

        public void CargarListMes()
        {
            lstMesGananciaBuscar.DataSource = null;
            lstMesGananciaBuscar.DataSource = createDataSourceMes();
            lstMesGananciaBuscar.DataTextField = "nombre";
            lstMesGananciaBuscar.DataValueField = "id";
            lstMesGananciaBuscar.DataBind();
        }

        ICollection createDataSourceMes()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un Mes", "Seleccione un Mes", dt));
            dt.Rows.Add(createRow("Enero", "Enero", dt));
            dt.Rows.Add(createRow("Febrero", "Febrero", dt));
            dt.Rows.Add(createRow("Marzo", "Marzo", dt));
            dt.Rows.Add(createRow("Abril", "Abril", dt));
            dt.Rows.Add(createRow("Mayo", "Mayo", dt));
            dt.Rows.Add(createRow("Junio", "Junio", dt));
            dt.Rows.Add(createRow("Julio", "Julio", dt));
            dt.Rows.Add(createRow("Agosto", "Agosto", dt));
            dt.Rows.Add(createRow("Septiembre", "Septiembre", dt));
            dt.Rows.Add(createRow("Octubre", "Octubre", dt));
            dt.Rows.Add(createRow("Noviembre", "Noviembre", dt));
            dt.Rows.Add(createRow("Diciembre", "Diciembre", dt));

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

        protected void btnBuscarGanancia_Click(object sender, EventArgs e)
        {
            lblGanancias.Text = "";
            lblMensajes.Text = "";
            try
            {
                if (int.Parse(txtAnioGananciaBuscar.Text) >= 1000 && int.Parse(txtAnioGananciaBuscar.Text) <= 9999)
                {
                    obtenerGanancia();
                }
                else
                {
                    lblMensajes.Text = "El año debe tener 4 digitos.";
                }
            }
            catch
            {
                lblMensajes.Text = "Los digitos del año es erroneo.";
            }
        }

        protected void listEstadisticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            comprobarEstadisticas();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion


    }
}