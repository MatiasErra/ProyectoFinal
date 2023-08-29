<%@ Page Language="C#" AutoEventWireup="true" Title="Producto" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmVerProducto.aspx.cs" Inherits="Web.Paginas.frmVerProducto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12 m-3 p-2" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-md-8 col-8 text-center">
                        <asp:Image CssClass="w-50" ID="imgProducto" runat="server" />
                    </div>
                    <div class="col-md-4 ps-md-3 pb-2">
                        <asp:Label CssClass="h1" ID="nombreProducto" Text="" runat="server" />
                        <br />
                        <asp:Label ID="tipoProducto" CssClass="ms-1 fs-5" Text="" runat="server" />
                        <div class="m-3" style="border-radius: 20px; background-color:white;">
                            <div class="m-2">
                                <h3>Realizar pedido:</h3>
                            <asp:Label ID="lblMensajes" Text="" runat="server" />
                            <div>
                                <asp:Label ID="tipoVentaProducto" Text="" CssClass="" runat="server" />
                                <asp:TextBox ID="txtCantidad" runat="server" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                    ControlToValidate="txtCantidad"
                                    ValidationExpression="^[0-9]+$"
                                    ErrorMessage="No es un caracter valido" />

                                <asp:Button ID="btnRealizarPedido" CssClass="btnE btn--radius btn--green mb-2" runat="server" Text="Realizar producto" OnClick="btnRealizarPedido_Click" />
                            </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
