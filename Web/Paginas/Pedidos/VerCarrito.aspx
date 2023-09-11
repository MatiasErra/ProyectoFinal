<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MCliente.Master" AutoEventWireup="true" CodeBehind="VerCarrito.aspx.cs" Inherits="Web.Paginas.Pedidos.VerCarrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-11 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Carro de compras </h2>
                    </div>

                     <asp:Label id ="lblheaderPedido" CssClass="row rowLine" runat="server"> 
                    <div class="row">
                       
                        <div class="col-xl-4  col-lg-12">
                            <asp:Label CssClass="text centerText " Text="Identificador del pedido" runat="server"></asp:Label>


                        </div>

                        <div class="col-xl-4 col-lg-12">
                            <asp:Label CssClass="text centerText " Text="Fecha de pedido" runat="server"></asp:Label>


                        </div>
                        
                    </div>

                    <div class="row">

                        <div class="col-xl-4 col-lg-12">
                            <asp:Label ID="txtIdPedido" CssClass="text centerText" runat="server"></asp:Label>

                        </div>
                        <div class=" col-xl-4 col-lg-12">
                            <asp:Label ID="txtfchPedido" CssClass="text centerText" runat="server"></asp:Label>

                        </div>
                    </div>
                </asp:Label>


                    <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>
                    </div>


                    <div class="col-md-12 align-self-center text-center mb-2" >
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstProducto" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover mb-2"
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



                                                <asp:BoundField DataField="Imagen"
                                                    HeaderText="Imagen"
                                                    HtmlEncode="false" />

                                                <asp:BoundField DataField="Precio"
                                                    HeaderText="Precio"
                                                    HtmlEncode="false" />


                                                <asp:BoundField DataField="CantidadDisp"
                                                    HeaderText="Stock Disponible"
                                                    HtmlEncode="false" />

                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" CssClass="input--style-tex-grid   text centerText" runat="server" placeholder="Cantidad" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:BoundField DataField="Cantidad"
                                                    HeaderText="Cantidad"
                                                    HtmlEncode="false" />


                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnModificarCantidad" CssClass="btnE btn--radius btn--green" runat="server" Text="Modificar cantidad" OnClick="btnModificarCantidad_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminar" CssClass="btnE btn--radius btn--red" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
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

                            <asp:Label runat="server" CssClass="text pagStyle" Text="Paginas" />
                            <div class="text-center">
                                <asp:LinkButton ID="lblPaginaAnt" CssClass="text pagTextAct" OnClick="lblPaginaAnt_Click" runat="server"></asp:LinkButton>
                                <asp:Label ID="lblPaginaAct"  CssClass="text pagText" runat="server" Text=""></asp:Label>
                                <asp:LinkButton ID="lblPaginaSig"  CssClass="text pagTextAct" OnClick="lblPaginaSig_Click" runat="server"></asp:LinkButton>
                            </div>
                        </div>
                        </asp:Label>
                    </div>


               

    <div class="row rowLine"> </div>

                     <asp:Label id ="lblFooterPedido" CssClass="mb-2" runat="server"> 
                    <div class="row">

                        <div class="col-xl-4  col-lg-12">
                            <asp:Label CssClass="text centerText " Text="Precio Final" runat="server"></asp:Label>


                        </div>
                    </div>


                    <div class="row">

                        <div class="col-xl-4 col-lg-12">
                            <asp:Label ID="txtprecioFin" CssClass="text centerText" runat="server"></asp:Label>

                        </div>
                       </div>
                         </asp:Label>


                        <div class="col-12">

                            <asp:Button ID="btnFinalizarPedido" CssClass="btnE btn--radius btn--blue my-2" runat="server" Text="Hacer Pedido" OnClick="btnFinalizarPedio_Click" />
                              <asp:Button ID="btnBorrarPedido" CssClass="btnE btn--radius btn--red my-2" Visible ="false" runat="server" Text="Borrar Pedido" OnClick="btnBorrarPedido_Click" />
                        </div>

                    </div>
    </div>
    </div>
        </div>
</asp:Content>
