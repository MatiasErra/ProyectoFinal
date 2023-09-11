using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmVerProducto : System.Web.UI.Page
    {

        #region Load

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["catalogoProducto"] == null)
            {
                Response.Redirect("/Paginas/frmCatalogo");
            }

            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null)
            {
                this.MasterPageFile = "~/Master/MCliente.Master";

            }
            else
            {
                Response.Redirect("/Paginas/frmCatalogo");
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idProducto = (int)System.Web.HttpContext.Current.Session["catalogoProducto"];
                cargarProducto(idProducto);
                CargarCant();

            }
        }

        private void cargarProducto(int idProducto)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto producto = Web.buscarProducto(idProducto);
            imgProducto.ImageUrl = string.Format("data:image/png;base64," + producto.Imagen);
            nombreProducto.Text = producto.Nombre;
            tipoProducto.Text = producto.Tipo;
            tipoVentaProducto.Text = "Cantidad (" + producto.TipoVenta + "):";
            btnRealizarPedido.OnClientClick = "return confirm('¿Desea realizar un pedido de " + producto.Nombre + "?')";
            lblMensajes.Text = "";
        }

        private void CargarCant()
        {
            int idProd = int.Parse(System.Web.HttpContext.Current.Session["catalogoProducto"].ToString());
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            Producto pro = Web.buscarProducto(idProd);

            lblcantActual.Text = "La cantidad actual es de " + pro.CantTotal;
            lblcantRess.Text = "La cantidad reservada actual es de " + pro.CantRes;
        }



        public bool mayorCant()
        {
            string textCantAct = lblcantActual.Text.ToString();
            string[] textArry = textCantAct.Split(' ');
            string p = textArry[0].ToString();


            int cantActual = int.Parse(textArry[5].ToString());
            int cant = int.Parse(txtCantidad.Text.ToString());
            if (cantActual > cant)
            {
                return true;
            }

            else { return false; }

        }



        public bool mayorRess()
        {
            string textCantAct = lblcantActual.Text.ToString();
            string[] textArry = textCantAct.Split(' ');
            string p = textArry[0].ToString();
            int cantActual = int.Parse(textArry[5].ToString());

            string textRess = lblcantRess.Text.ToString();
            string[] textArry2 = textRess.Split(' ');
            int cantRess = int.Parse(textArry2[6].ToString());

            int cant = int.Parse(txtCantidad.Text.ToString());
            cantRess += cant;


            if (cantActual >= cantRess)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        static int GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            int intGuid = guid.GetHashCode();
            int i = 0;

            while (intGuid < 0)
            {
                return GenerateUniqueId();
            }

            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
            List<Pedido> lstPed = Web.listPedido();
            foreach (Pedido Pedido in lstPed)
            {
                if (Pedido.IdPedido.Equals(intGuid))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                return intGuid;
            }
            else
                return GenerateUniqueId();

        }

        private string cantResActu(int idProducto, string cantidadAdd)
        {
            ControladoraWeb Web = ControladoraWeb.obtenerInstancia();

            int cant = 0;
            string resultado = "";
            List<Pedido_Prod> pedido_Prod = Web.listPedidoCli_Prod(idProducto);
            Producto producto = Web.buscarProducto(idProducto);

            foreach (Pedido_Prod pedidos in pedido_Prod)
            {

                if (pedidos.IdProducto.Equals(producto.IdProducto))
                {
                    string textCant = pedidos.Cantidad;
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


        #endregion

        protected void btnRealizarPedido_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "")
            {
                if (mayorCant())
                {
                    if (mayorRess())
                    {
                        int idProducto = (int)System.Web.HttpContext.Current.Session["catalogoProducto"];
                        int idCliente = int.Parse(System.Web.HttpContext.Current.Session["ClienteIniciado"].ToString());
                        int idPedido = 0;
                        int idPedidoReg =0;

                        ControladoraWeb Web = ControladoraWeb.obtenerInstancia();
                      Pedido_Prod pedido_Prodbus = Web.buscarProductoCli(idProducto, idCliente);

                        if (System.Web.HttpContext.Current.Session["PedidoCompra"] == null)
                        {
                            if (pedido_Prodbus.IdPedido != 0)
                            {
                                idPedidoReg = 0;

                         
                            }
                            else
                            {

                                Pedido pedido = new Pedido();

                                idPedido = GenerateUniqueId();

                                pedido.IdPedido = idPedido;
                                pedido.IdCliente = idCliente;

                                idPedidoReg = 1;
                                if (Web.altaPedido(pedido))
                                {
                                    System.Web.HttpContext.Current.Session["PedidoCompra"] = idPedido;
                                }
                                else
                                {
                                    idPedido = 0;
                                }
                            }
                        }
                        else
                        {
                            idPedidoReg = 1;
                            idPedido = int.Parse(System.Web.HttpContext.Current.Session["PedidoCompra"].ToString());
                        }

                        if (idPedidoReg != 0)
                        {
                            if (idPedido != 0)
                            {
                            
                                Pedido_Prod pedido_prod = new Pedido_Prod();
                                Producto pre = Web.buscarProducto(idProducto);

                                string cantidadMost = txtCantidad.Text.ToString() + " " + pre.TipoVenta.ToString();

                                pedido_prod.IdProducto = idProducto;
                                pedido_prod.IdPedido = idPedido;
                                pedido_prod.Cantidad = cantidadMost; 
                                string CantRes = cantResActu(idProducto, txtCantidad.Text.ToString());

                                string[] string1 = pedido_prod.Cantidad.Split(' ');



                                double cantidadDouble = double.Parse(string1[0]);

                                double Precio = pre.Precio * cantidadDouble;



                                if (Web.altaPedido_Prod(pedido_prod, CantRes, Precio))
                                {
                                    lblMensajes.Text = "Pedido realizado";
                                    CargarCant();
                                }
                                else
                                {
                                    lblMensajes.Text = "Este producto ya se encuentra en el pedido";
                                }
                            }
                            else
                            {
                                lblMensajes.Text = "No se pudo realizar el pedido";
                            }
                        }
                        else
                        {
                            lblMensajes.Text = "Ya existe un pedido con este producto registrado";
                        }
                    }
                    else
                    {
                        lblMensajes.Text = "La cantidad ingresada, superaria la cantidad reservada de este producto en el sistema.";
                  
                    }
                }
                else
                {
                    lblMensajes.Text = "Debe ingresar una cantidad menor a la cantidad actual";
                }

            }
            else
            {
                lblMensajes.Text = "Debe ingresar una cantidad para poder realizar un pedido.";
            }
        }
    }
}