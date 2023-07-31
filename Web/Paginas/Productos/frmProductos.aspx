<%@ Page Language="C#" Title="Gestion de productos" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="Web.Paginas.Productos.frmProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h5>ABM Producto </h5>
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12 align-self-center">
                        <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Producto?')" OnClick="btnBaja_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />

                    </div>

                    <div class="col-12">
                        <button type="button" class="btn btn-outline-dark m-1 w-25" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Modificar/Añadir Producto
                        </button>

                    </div>

                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Producto</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="col-12">
                                        Nombre
                                        <asp:TextBox ID="txtNombre" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z0-9]*$"
                                            ErrorMessage="No es un caracter valido" />
                                    </div>

                                    <div class="col-12">
                                        Tipo
                                        <asp:TextBox ID="txtTipo" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Tipo" MaxLength="30" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtTipo"
                                            ValidationExpression="^[a-zA-Z]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>
                                    <div class="col-12">
                                        Tipo venta
                                        <asp:TextBox ID="txtTipoVenta" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Tipo venta" MaxLength="15" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtTipoVenta"
                                            ValidationExpression="^[a-zA-Z]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>
                                    <div class="col-12">
                                        Imagen
                                    </div>
                                    <div class="col-12">
                                        
                                        <asp:FileUpload ID="fileImagen" CssClass="mt-1 mb-1 w-75 m-auto" runat="server" /> 
                                        <asp:Image ID="imgImagen" runat="server" />
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                    <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Producto?')" OnClick="btnModificar_Click" />
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:ListBox ID="lstProducto" runat="server" AutoPostBack="true" CssClass="w-75 h-auto" OnSelectedIndexChanged="lstProducto_SelectedIndexChanged"></asp:ListBox>
            </div>
        </div>

    </div>

    <asp:TextBox Visible="false" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>
