<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmPedido.aspx.cs" Inherits="Web.Paginas.PedidosADM.frmPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Administrar Pedidos </h2>
                    </div>




                    <div class="row text-center">
                        <div class=" col-sm-12">
                            <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                            <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                        <div class="row text-center">
                            <div class="col-sm-12">
                                 <asp:DropDownList Visible="false" ID="lstCliente" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>
                                 <asp:Button Visible="false" CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarDueñoBuscar" runat="server" Text="Buscar Dueño" OnClick="btnBuscarDueñoBuscar_Click" />
                           
                                <asp:DropDownList Visible="false" ID="lstEstados" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>


                                   <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" ID="lblCostoMenorBuscar" class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox Visible="false" ID="txtCostoMenorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Carga" MaxLength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtCostoMenorBuscar"
                                            ValidationExpression="^[0-9]*$"
                                            ErrorMessage="No es un numero valido" />

                                        <asp:Label Visible="false" ID="lblCostoMayorBuscar" class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox Visible="false" ID="txtCostoMayorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Carga" MaxLength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtCostoMayorBuscar"
                                            ValidationExpression="^[0-9]*$"
                                            ErrorMessage="No es un numero valido" />
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


                    <div class="col-md-12 align-self-center text-center">
                        <div class="col-12">
                        </div>
                        <div class="row align-self-center">
                            <div class="col-md-12 col-md-offset-1">

                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstPedido" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">

                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>




                                                <asp:BoundField DataField="idPedido"
                                                    HeaderText="Identificador del pedido"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="idCliente"
                                                    HeaderText="Identificador del cliente"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Nombre"
                                                    HeaderText="Nombre"
                                                    ItemStyle-CssClass="GridStl" />



                                                <asp:BoundField DataField="Estado"
                                                    HeaderText="Estado"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Costo"
                                                    HeaderText="Precio Total"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="fchPedido"
                                                    HeaderText="Fecha de pedido"
                                                    ItemStyle-CssClass="GridStl" />

                                                
                                                <asp:BoundField DataField="InfoEnv"
                                                    HeaderText="Dirección de envió"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="fchEspe"
                                                    HeaderText="Fecha de entrega esperada"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="fchEntre"
                                                    HeaderText="Fecha de entrega"
                                                    ItemStyle-CssClass="GridStl" />





                                                <asp:TemplateField
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnVerPedido" CssClass="btnE btn--radius btn--green" runat="server" Text="Ver Pedido" OnClick="btnVerPedido_Click" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField 
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>

                                                            <asp:Button ID="btnConfirmarPedido" CssClass="btnE btn--radius btn--blue" runat="server" Text="Confirmar Pedido" OnClick="btnConfirmarPedido_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField 
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnEliminar" CssClass="btnE btn--radius btn--red" runat="server" Text="Eliminar Pedido" OnClick="btnEliminar_Click" />
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

</asp:Content>
