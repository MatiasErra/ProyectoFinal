<%@ Page Language="C#" AutoEventWireup="true" Title="Producto" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmVerProducto.aspx.cs" Inherits="Web.Paginas.frmVerProducto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-7 m-3 p-2 text-center backforContent">
                <div class="row">



                    <div class="col-md-12 pb-2 mb-2">
                        <div class="rowLine">
                            <div>
                                <asp:Label CssClass="h1" ID="nombreProducto" Text="" runat="server" />
                            </div>

                            <div>
                                <asp:Label ID="tipoProducto" CssClass="" Text="" runat="server" />
                            </div>

                        </div>

                        <asp:Image CssClass="imgPro" ID="imgProducto" runat="server" />

                    </div>


                    <div class=" col-md-12 mb-2">
                        <div class="w-50 styleBox">
                            <div class="m-2">
                                <h3>Realizar pedido</h3>



                                <div>
                                    <asp:Label ID="tipoVentaProducto" Text="" CssClass=" text centerText" runat="server" />
                                </div>

                                <div>
                                    <asp:TextBox ID="txtCantidad" runat="server" placeholder="Cantidad" CssClass="input--style-tex  text centerText" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" CssClass="text centerText"
                                        ControlToValidate="txtCantidad"
                                        ValidationExpression="^[0-9]+$"
                                        ErrorMessage="No es un caracter valido" />

                                </div>
                                <div>
                                    <asp:Label ID="lblcantActual" Text="" CssClass=" text centerText" runat="server" />
                                </div>

                                    <div>
                                    <asp:Label ID="lblcantRess" Text="" CssClass=" text centerText" runat="server" />
                                </div>


                                <div>
                                    <asp:Button ID="btnRealizarPedido" CssClass="btnE btn--radius btn--green my-2" runat="server" Text="Realizar producto" OnClick="btnRealizarPedido_Click" />
                                </div>

                                                                <div>

                                    <asp:Label ID="lblMensajes" CssClass="text centerText" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>

</asp:Content>
