<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MCliente.Master" AutoEventWireup="true" CodeBehind="VerPedidosCli.aspx.cs" Inherits="Web.Paginas.Catalogo.VerPedidoCli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Ver Pedidos </h2>
                    </div>

                    <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>
                    </div>






                    <div class="col-md-12 align-self-center text-center">
                        <div class="col-12">
                        </div>
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <asp:Label ID="PedSinCon" runat="server"> <h5 class="title">Pedidos sin Confirmar</h5>  </asp:Label>
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="LstPedSinCon" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">

                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>




                                                <asp:BoundField DataField="idPedido"
                                                    HeaderText="Identificador del pedido"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Estado"
                                                    HeaderText="Estado"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="fchPedido"
                                                    HeaderText="Tipo de venta"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="Costo"
                                                    HeaderText="Precio Total"
                                                    ItemStyle-CssClass="GridStl" />



                                                <asp:TemplateField HeaderText="Opciones"
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>


                                                        <asp:Button ID="btnVerPedido" CssClass="btnE btn--radius btn--green" runat="server" Text="Ver Pedido" OnClick="btnVerPedido_Click" />
                                                        <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--blue" runat="server" Text="Modificar Pedido" OnClick="btnModificar_Click" />
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

</asp:Content>
