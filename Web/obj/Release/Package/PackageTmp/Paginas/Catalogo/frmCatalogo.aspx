﻿<%@ Page Language="C#" Title="Catálogo" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmCatalogo.aspx.cs" Inherits="Web.Paginas.frmCatalogo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="container row m-2 text-center">
            <div class="row justify-content-center">
                <div class="col-10 m-3 p-2 text-center backforContent">
                    <div class="row">

                        <div class="col-12 p-3">
                            <h2 class="title">Productos</h2>


                            <div class="row text-center">
                                <div class=" col-sm-12">
                                    <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                                    <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </div>
                            </div>




                            <div class="row text-center">
                                <div class="col-sm-12">
                                    <asp:TextBox Visible="false" ID="txtBuscar" CssClass="input--style-text-search" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                        ControlToValidate="txtBuscar"
                                        ValidationExpression="^[a-zA-Z ]*$"
                                        ErrorMessage="No es una letra valida" />



                                    <asp:DropDownList Visible="false" ID="listFiltroTipo" CssClass="input--style-lst-search" runat="server">
                                    </asp:DropDownList>

                                    <asp:DropDownList Visible="false" ID="listFiltroVen" CssClass="input--style-lst-search" runat="server">
                                    </asp:DropDownList>





                                    <div class="row justify-content-center">
                                        <div class="col-lg-6">
                                            <asp:Label Visible="false" runat="server" ID="lblPrecio">
                                                <asp:Label class="text initText" Text="Desde:" runat="server" />
                                                <asp:TextBox ID="txtPrecioMenorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                    ControlToValidate="txtPrecioMenorBuscar"
                                                    ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                    ErrorMessage="Solo numeros" />

                                                <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                                <asp:TextBox ID="txtPrecioMayorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                    ControlToValidate="txtPrecioMayorBuscar"
                                                    ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                    ErrorMessage="Solo numeros" />
                                            </asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12">

                                <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            </div>




                            <div class="col-12 my-2">
                                <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>



                            </div>

                            <div class="rowLine" />

                        </div>
                        <div class="col-md-12 align-self-center text-center">
                            <div class="row align-self-center">
                                <div class="col-md-10 col-md-offset-1">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <asp:GridView ID="lstProducto" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-condensed table-responsive table-hover  mb-2"
                                                runat="server">
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                                <RowStyle BackColor="#f5f5f5" />
                                                <Columns>
                                                    <asp:BoundField DataField="Nombre"
                                                        HeaderText="Nombre del producto"
                                                        ItemStyle-CssClass="GridStl" />

                                                    <asp:BoundField DataField="Tipo"
                                                        HeaderText="Tipo"
                                                        ItemStyle-CssClass="GridStl" />

                                                    <asp:BoundField DataField="TipoVenta"
                                                        HeaderText="Tipo de venta"
                                                        ItemStyle-CssClass="GridStl" />



                                                    <asp:BoundField DataField="Precio"
                                                        HeaderText="Precio"
                                                        ItemStyle-CssClass="GridStl" />

                                                    
                                                    <asp:BoundField DataField="CantDisp"
                                                        HeaderText="Cantidad Disponible"
                                                        ItemStyle-CssClass="GridStl" />

                                                    <asp:BoundField DataField="Imagen"
                                                        HeaderText="Imagen"
                                                        HtmlEncode="false" />


                                                    <asp:TemplateField
                                                        ItemStyle-CssClass="GridStl">
                                                        <ItemTemplate>

                                                            <asp:Button ID="btnRealizarPedido" CssClass="btnE btn--radius btn--green" runat="server" Text="Agregar al carrito" OnClick="btnRealizarPedido_Click" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblPaginas" CssClass="text pagStyle">
                                <div class="text-center">

                                    <asp:Label runat="server" ID="txtPaginas" CssClass="text pagStyle" Text="Paginas" />
                                    <div class="text-center">
                                        <asp:LinkButton ID="lblPaginaAnt" CssClass="text pagTextAct" OnClick="lblPaginaAnt_Click" runat="server"></asp:LinkButton>
                                        <asp:Label ID="lblPaginaAct" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                        <asp:LinkButton ID="lblPaginaSig" CssClass="text pagTextAct" OnClick="lblPaginaSig_Click" runat="server"></asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Label>
                        </div>


                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
