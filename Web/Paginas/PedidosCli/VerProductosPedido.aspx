<%@ Page Title="Ver productos del pedido" Language="C#" MasterPageFile="~/Master/MCliente.Master" AutoEventWireup="true" CodeBehind="VerProductosPedido.aspx.cs" Inherits="Web.Paginas.Pedidos.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Ver Productos </h2>
                    </div>





                    <div class="col-md-12 align-self-center text-center">
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstProducto" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
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
                                                    ItemStyle-CssClass="GridStl" />




                                                <asp:BoundField DataField="Cantidad"
                                                    HeaderText="Cantidad Solicitada"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="PecioTotal"
                                                    HeaderText="Precio total del producto"
                                                    ItemStyle-CssClass="GridStl" />

                                            </Columns>
                                        </asp:GridView>



                                        <asp:GridView ID="lstProductoLote" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>



                                                <asp:BoundField DataField="NombreGranja"
                                                    HeaderText="Granja"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="NombreProducto"
                                                    HeaderText="Producto"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="FchProduccion"
                                                    HeaderText="Fecha de producción"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="Tipo"
                                                    HeaderText="Tipo"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Imagen"
                                                    HeaderText="Imagen"
                                                    HtmlEncode="false" />


                                                <asp:BoundField DataField="Precio"
                                                    HeaderText="Precio"
                                                    ItemStyle-CssClass="GridStl" />




                                                <asp:BoundField DataField="Cantidad"
                                                    HeaderText="Cantidad Solicitada"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="PecioTotal"
                                                    HeaderText="Precio total del producto"
                                                    ItemStyle-CssClass="GridStl" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="text-center">

                            <asp:Label runat="server" ID="txtPaginas" CssClass="text pagStyle" Text="Paginas" />
                            <div class="text-center">
                                <asp:LinkButton ID="lblPaginaAnt" CssClass="text pagTextAct" OnClick="lblPaginaAnt_Click" runat="server"></asp:LinkButton>
                                <asp:Label ID="lblPaginaAct" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                <asp:LinkButton ID="lblPaginaSig" CssClass="text pagTextAct" OnClick="lblPaginaSig_Click" runat="server"></asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div>

                        <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                    </div>


                    <div class="col-12">
                        <asp:Button ID="btnModPedido" CssClass="btnE btn--radius btn--yellow my-2" runat="server" Text="Modificar Pedido a Sin confirmar" OnClick="btnModPedido_Click" />

                        <asp:Button ID="btnVerViaje" CssClass="btnE btn--radius btn--green my-2" runat="server" Text="Ver Viajes del pedido" OnClick="btnVerViaje_Click" />

                        <asp:Button ID="btnFinalizarPedido" CssClass="btnE btn--radius btn--blue my-2" runat="server" Text="Finalizar pedido" OnClick="btnFinalizarPedido_Click" />
                    </div>






                </div>
            </div>
        </div>
    </div>

</asp:Content>
