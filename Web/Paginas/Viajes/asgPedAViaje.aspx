<%@ Page Title="Asignar viaje a pedido" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="asgPedAViaje.aspx.cs" Inherits="Web.Paginas.Viajes.asgPedAViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-10 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Confirmar Viaje</h2>
                    </div>


                    <h5 class="title mb-2">Pedido </h5>

                    <div class="row text-center mb-3">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="lstPedidosCon" CssClass="input--style-lst-search" OnSelectedIndexChanged="lstPedidosCon_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>

                        </div>

                    </div>


                    <div class="rowLine">
                    </div>


                    <asp:Label ID="lblH5LotePed" runat="server"> 
                       <h5 class="title">Lotes asignados al pedido</h5> 
                    </asp:Label>





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

                                                <asp:BoundField DataField="idPedido"
                                                    HeaderText="Identificador de pedido"
                                                    ItemStyle-CssClass="GridStl" />


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

                                                <asp:BoundField DataField="CantidadViaje"
                                                    HeaderText="Cantidad asignada a un viaje"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" CssClass="input--style-tex-grid   text centerText" runat="server" placeholder="Cantidad" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                            ControlToValidate="txtCantidad"
                                                            ValidationExpression="^[0-9]*$"
                                                            ErrorMessage="No es un numero valido" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAsignarAlViaje" CssClass="btnE btn--radius btn--blue" runat="server" Text="Asignar al viaje" OnClick="btnAsignarAlViaje_Click" />
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






                    <asp:Label ID="h5ConfViaje" runat="server"> 
                        <div class="rowLine">
                    </div>
                    
                    <h5 class="title">Pedidos asignados al Viaje</h5> 



                    <div class="rowLine">
                    </div>

                    </asp:Label>


                    <asp:Label CssClass="text centerText my-2" ID="lblMensajes" runat="server"></asp:Label>




                    <div class="col-md-12 align-self-center text-center">
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstViajePed" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>
                                                <asp:BoundField DataField="idPedido"
                                                    HeaderText="Identificador de pedido"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="idViaje"
                                                    HeaderText="Identificador de viaje"
                                                    ItemStyle-CssClass="GridStl" />



                                                <asp:BoundField DataField="NombreGranja"
                                                    HeaderText="Granja"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="NombreProducto"
                                                    HeaderText="Producto"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="FchProduccion"
                                                    HeaderText="Fecha de producción"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="CantidadAsg"
                                                    HeaderText="Cantidad ingresada"
                                                    ItemStyle-CssClass="GridStl" />



                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnBorrarViaPedLot" CssClass="btnE btn--radius btn--red" runat="server" Text="Borrar del viaje" OnClick="btnBorrarViaPedLot_Click" />
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

                                <asp:Label runat="server" CssClass="text pagStyle" ID="txtPaginaViaPed" Text="Paginas" />
                                <div class="text-center">
                                    <asp:LinkButton ID="lblPaginaAntViaPed" CssClass="text pagTextAct" OnClick="txtPaginaAntViaPed_Click" runat="server"></asp:LinkButton>
                                    <asp:Label ID="lblPaginaActViaPed" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                    <asp:LinkButton ID="lblPaginaSigViaPed" CssClass="text pagTextAct" OnClick="txtPaginaSegViaPed_Click" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Label>
                    </div>


                    <div class="col-12">
                        <asp:Button ID="btnAsignarViajePedido" CssClass="btnE btn--radius btn--blue my-2" runat="server" Text="Confirmar viaje" OnClick="btnAsignarViajePedido_Click" />
                        <asp:Button ID="btnConfirmarViaje" CssClass="btnE btn--radius btn--blue my-2" runat="server" Text="Confirmar Viaje" OnClick="btnConfirmarViaje_Click" />
                    </div>



                </div>
            </div>
        </div>
    </div>



</asp:Content>
