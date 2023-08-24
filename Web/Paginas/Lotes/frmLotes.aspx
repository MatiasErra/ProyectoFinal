<%@ Page Title="Gestion de lotes" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmLotes.aspx.cs" Inherits="Web.Paginas.Lotes.frmLotes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">

                    <div class="col-12">
                        <h2 class="title">ABM Lotes </h2>
                    </div>

              
                 
                     

       

                    <div class="col-12">
                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(event.keyCode<91 || event.keyCode==189);"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    
             


                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        <asp:Button ID="btnAltaLot" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Añadir Lote" OnClick="btnAltaLot_Click" />
                    </div>
                </div>

                              <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                      <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtBuscar"
                            ValidationExpression="^[a-zA-Z0-9 ]+$"
                            ErrorMessage="No es un carácter válido" />

                </div>

                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
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

                                            <asp:BoundField DataField="Cantidad"
                                                HeaderText="Cantidad de  producción"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Precio"
                                                HeaderText="Precio"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="UbicacionDeposito"
                                                HeaderText="Depósito"
                                                ItemStyle-CssClass="GridStl" />




                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVerPestis" CssClass="btnE btn--radius btn--blue" runat="server" Text="Pesticidas" OnClick="btnVerPestis_Click" />
                                                    <asp:Button ID="btnVerFertis" CssClass="btnE btn--radius btn--blue" runat="server" Text="Fertilizantes" OnClick="btnVerFertis_Click" />
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Lote?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
