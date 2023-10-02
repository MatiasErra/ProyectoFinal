<%@ Page Title="Confirmar pedido" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="confPedido.aspx.cs" Inherits="Web.Paginas.PedidosAdm.confPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-10 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Confirmar Pedidos </h2>
                    </div>








                    <h5 class="title mb-2">Pedido del Cliente </h5>


                    <div class="row text-center mb-3">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="lstPedidosProd" CssClass="input--style-lst-search" OnSelectedIndexChanged="lstPedidosProd_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>

                        </div>

                    </div>

                    <div class="rowLine">
                    </div>

                        <asp:Label ID="lblH5Lote" runat="server"> 
                       <h5 class="title">Lotes</h5> 
                            </asp:Label>
                    <div class="col-md-12 align-self-center text-center">
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstLote" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
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

                                                <asp:BoundField DataField="FchCaducidad"
                                                    HeaderText="Fecha de caducidad"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Cantidad"
                                                    HeaderText="Cantidad del lote"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" CssClass="input--style-tex-grid   text centerText" runat="server" placeholder="Cantidad" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAgregarCantidad" CssClass="btnE btn--radius btn--green" runat="server" Text="Agregar cantidad" OnClick="btnAgregarCantidad_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Label runat="server" CssClass="text pagStyle ">
                            <div class="text-center">

                                <asp:Label runat="server" CssClass="text pagStyle" ID="txtPaginasLot" Text="Paginas" />
                                <div class="text-center">
                                    <asp:LinkButton ID="lblPaginaAntLot" CssClass="text pagTextAct" OnClick="lblPaginaAntLot_Click" runat="server"></asp:LinkButton>
                                    <asp:Label ID="lblPaginaActLot" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                    <asp:LinkButton ID="lblPaginaSigLot" CssClass="text pagTextAct" OnClick="lblPaginaSigLot_Click" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Label>



                    </div>




                    <div class="row mb-2">
                        <div class="col-xl-4 col-lg-12">
                            <asp:Label ID="lblCantCli" CssClass="text centerText " Text="Cantidad solicitada por el Cliente" runat="server"></asp:Label>
                            <div class="col-lg-12">
                                <asp:Label ID="txtCantCli" CssClass="text centerText" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-xl-4 col-lg-12">
                            <asp:Label ID="lblCantAdm" CssClass="text centerText " Text="Cantidad ingresada por el Admin" runat="server"></asp:Label>
                            <div class ="col-lg-12">
                                    <asp:Label ID="txtCantAdm" CssClass="text centerText" runat="server"></asp:Label>
                                </div>
                        </div>
                    </div>

       


               



                    <asp:Label ID="h5ConfPedido" runat="server"> 
                        <div class="rowLine">
                    </div>
                    
                    <h5 class="title">Confirmar pedido</h5> 



                    <div class="rowLine">
                    </div>

                    </asp:Label>


                        <asp:Label CssClass="text centerText my-2" ID="lblMensajes" runat="server"></asp:Label>



                    <div class="col-md-12 align-self-center text-center">
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstPedidoLote" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
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

                                                <asp:BoundField DataField="Cantidad"
                                                    HeaderText="Cantidad ingresada"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" CssClass="input--style-tex-grid   text centerText" runat="server" placeholder="Cantidad" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnModificarCantidad" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar cantidad" OnClick="btnModificarCantidad_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminarLotePed" CssClass="btnE btn--radius btn--red" runat="server" Text="Eliminar del pedido" OnClick="btnEliminarLotePed_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Label runat="server" CssClass="text pagStyle ">
                            <div class="text-center">

                                <asp:Label runat="server" CssClass="text pagStyle" ID="txtPaginaLotPed" Text="Paginas" />
                                <div class="text-center">
                                    <asp:LinkButton ID="lblPaginaAntLotPed" CssClass="text pagTextAct" OnClick="lblPaginaAntLotPed_Click" runat="server"></asp:LinkButton>
                                    <asp:Label ID="lblPaginaActLotPed" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                    <asp:LinkButton ID="lblPaginaSigLotPed" CssClass="text pagTextAct" OnClick="lblPaginaSigLotPed_Click" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Label>
                    </div>



                    <div class="col-12">

                            <asp:Button ID="btnConfPed" CssClass="btnE btn--radius btn--blue my-2" runat="server" Text="Confirmar Pedido" OnClick="btnConfirmarPedido_Click" />
                       </div>






                </div>
            </div>
        </div>
    </div>


</asp:Content>
