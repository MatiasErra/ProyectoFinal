<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="verLoteDelViaje.aspx.cs" Inherits="Web.Paginas.Viajes.verLoteDelPedio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



     <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">Ver lotes del viaje </h2>
                    </div>





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
                              

                                            </Columns>
                                        </asp:GridView>
                                      

                                                <asp:GridView ID="lstViaje" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>
                                         


                                                <asp:BoundField DataField="idViaje"
                                                    HeaderText="Identificador de viaje"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="fchViaje"
                                                    HeaderText="Fecha de Viaje"
                                                    ItemStyle-CssClass="GridStl" />

                                                  <asp:BoundField DataField="Estado"
                                                HeaderText="Estado del Viaje"
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






                </div>
            </div>
        </div>
    </div>

</asp:Content>
