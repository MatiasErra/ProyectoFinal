﻿using Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Paginas.Granjas;

namespace Web.Paginas.Productos
{
    public partial class modProducto : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["idProductoSel"] == null)
            {
                Response.Redirect("/Paginas/Productos/frmProductos");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idProducto = (int)System.Web.HttpContext.Current.Session["idProductoSel"];

                CargarListTipo();
                CargarListTipoVenta();
                cargarProducto(idProducto);

            }
        }


        private void limpiarIdSession()
        {
            System.Web.HttpContext.Current.Session["idGranjaSel"] = null;
            System.Web.HttpContext.Current.Session["idProductoSel"] = null;
            System.Web.HttpContext.Current.Session["fchProduccionSel"] = null;
        }

        private void cargarProducto(int id)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto producto = Web.buscarProducto(id);
            txtId.Text = producto.IdProducto.ToString();
            txtNombre.Text = producto.Nombre;
            listTipo.SelectedValue = producto.Tipo;
            listTipoVenta.SelectedValue = producto.TipoVenta;
            imgImagen.ImageUrl = string.Format("data:image/png;base64," + producto.Imagen);
        }



        private bool faltanDatos()
        {
            if (txtNombre.Text == "" || listTipo.SelectedValue == "Seleccione un tipo de producto" || listTipoVenta.SelectedValue == "Seleccione un tipo de venta")
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

            txtNombre.Text = "";
            fileImagen.Attributes.Clear();
            CargarListTipo();
            CargarListTipoVenta(); ;
        }

        #region DropDownBoxes

        #region Tipo

        public void CargarListTipo()
        {
            listTipo.DataSource = null;
            listTipo.DataSource = createDataSourceTipo();
            listTipo.DataTextField = "nombre";
            listTipo.DataValueField = "id";
            listTipo.DataBind();
        }

        ICollection createDataSourceTipo()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un tipo de producto", "Seleccione un tipo de producto", dt));
            dt.Rows.Add(createRow("Fruta", "Fruta", dt));
            dt.Rows.Add(createRow("Verdura", "Verdura", dt));


            DataView dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Tipo Venta

        public void CargarListTipoVenta()
        {
            listTipoVenta.DataSource = null;
            listTipoVenta.DataSource = createDataSourceTipoVenta();
            listTipoVenta.DataTextField = "nombre";
            listTipoVenta.DataValueField = "id";
            listTipoVenta.DataBind();
        }

        ICollection createDataSourceTipoVenta()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("nombre", typeof(String)));
            dt.Columns.Add(new DataColumn("id", typeof(String)));

            dt.Rows.Add(createRow("Seleccione un tipo de venta", "Seleccione un tipo de venta", dt));
            dt.Rows.Add(createRow("Kilo", "Kilo", dt));
            dt.Rows.Add(createRow("Unidad", "Unidad", dt));

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

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            limpiar();
            limpiarIdSession();
            Response.Redirect("/Paginas/Productos/frmProductos");
        }

        private bool detectarImagen()
        {
            if (fileImagen.HasFile)
            {
                if (fileImagen.PostedFile.ContentLength < 2100000)
                {
                    string archivo = System.IO.Path.GetExtension(fileImagen.FileName);
                    string[] extenciones = { ".jpg", ".jpeg", ".png" };
                    if (extenciones.Contains(archivo.ToLower()))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!faltanDatos())
            {
                if (detectarImagen())
                {
                    ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                    int idProducto = int.Parse(HttpUtility.HtmlEncode(txtId.Text));
                    string nombre = HttpUtility.HtmlEncode(txtNombre.Text);
                    string tipo = HttpUtility.HtmlEncode(listTipo.SelectedValue);
                    string tipoVenta = HttpUtility.HtmlEncode(listTipoVenta.SelectedValue);
                    string imagen = "";
                    if (fileImagen.HasFile)
                    {
                        byte[] fileBytes = fileImagen.FileBytes;
                        imagen = Convert.ToBase64String(fileBytes);
                    }
                    else
                    {
                        Producto producto = Web.buscarProducto(idProducto);
                        imagen = producto.Imagen;
                    }



                    Producto unProducto = new Producto(idProducto, nombre, tipo, tipoVenta, imagen);
                    if (Web.modProducto(unProducto))
                    {
                        limpiar();
                        lblMensajes.Text = "Producto modificado con éxito.";

                        limpiarIdSession();
                        Response.Redirect("/Paginas/Productos/frmProductos");
                    }
                    else
                    {
                        lblMensajes.Text = "Ya existe un Producto con estos datos. Estos son los posibles datos repetidos (Nombre).";
                    }

                }
                else
                {
                    lblMensajes.Text = "El archivo debe ser una imagen o es muy grande.";
                }
            }
            else
            {
                lblMensajes.Text = "Faltan datos.";
            }
        }
    }
}