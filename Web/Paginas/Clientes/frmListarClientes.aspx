<%@ Page Title="Listado y eliminacion de clientes" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmListarClientes.aspx.cs" Inherits="Web.Paginas.Clientes.frmListarClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
               <div class="row">
                    <div class="col-12">
                         <h2 class="title">Listar o eliminar un cliente </h2>
                </div>
                    <div class="col-12">

                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                  
                        </div>


                    <div class="col-12 align-self-center">
                           <asp:Button ID="btnVolverFrm" class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Volver" visible="false"  OnClick="btnVolverFrm_Click" />
                         <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        
                    </div>
                   <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                      <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtBuscar"
                            ValidationExpression="^[a-zA-Z0-9 ]+$"
                            ErrorMessage="No es un carácter válido" />

                </div>
                </div>

               <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstCliente" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdPersona"
                                                HeaderText="Id de Cliente"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Apellido"
                                                HeaderText="Apellido" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Email"
                                                HeaderText="E-Mail" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Telefono"
                                                HeaderText="Telefono" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchNacimiento"
                                                HeaderText="Fecha de Nacimiento" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="User"
                                                HeaderText="Usuario" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Direccion"
                                                HeaderText="Dirección" ItemStyle-CssClass="GridStl" />


                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>



                                                  
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este cliente?')" OnClick="btnBaja_Click" />
                                             

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>

                                     <asp:GridView ID="lstClienteSelect" Width="100%" SelectedIndex="1" AutoGenerateColumns="false" Visible="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdPersona"
                                                HeaderText="Id de Cliente"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Apellido"
                                                HeaderText="Apellido" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Email"
                                                HeaderText="E-Mail" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Telefono"
                                                HeaderText="Telefono" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchNacimiento"
                                                HeaderText="Fecha de Nacimiento" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="User"
                                                HeaderText="Usuario" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Direccion"
                                                HeaderText="Dirección" ItemStyle-CssClass="GridStl" />


                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>



                                                     <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar"  OnClick="btnSelected_Click" />
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este cliente?')" OnClick="btnBaja_Click" />
                                             

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
    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>

