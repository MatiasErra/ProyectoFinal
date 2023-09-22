<%@ Page Title="Listado y eliminacion de clientes" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmListarClientes.aspx.cs" Inherits="Web.Paginas.Clientes.frmListarClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12 p-3">
                        <h2 class="title">Gestion de Clientes</h2>


                        <div class="row text-center">
                            <div class=" col-sm-12">
                                <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                                <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                            </div>
                        </div>

                        <div class="row text-center">
                            <div class="col-sm-12">
                                <asp:TextBox Visible="false" ID="txtNombreBuscar" CssClass="input--style-text-search" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtNombreBuscar"
                                    ValidationExpression="^[a-zA-Z ]*$"
                                    ErrorMessage="No es una letra valida" />

                                <asp:TextBox Visible="false" ID="txtApellidoBuscar" CssClass="input--style-text-search" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtApellidoBuscar"
                                    ValidationExpression="^[a-zA-Z ]*$"
                                    ErrorMessage="No es una letra valida" />

                                <asp:TextBox Visible="false" ID="txtEmailBuscar" CssClass="input--style-text-search" runat="server" placeholder="Email" onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                                <asp:TextBox Visible="false" ID="txtTelBuscar" CssClass="input--style-text-search" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTelBuscar" ID="RegularExpressionValidator1" class="text centerText"
                                    ValidationExpression="^[0-9]" runat="server" ErrorMessage="El teléfono solo pueden ser numeros." />

                                <asp:TextBox Visible="false" ID="txtUsuarioBuscar" runat="server" CssClass="input--style-text-search" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtUsuarioBuscar"
                                    ValidationExpression="[a-zA-Z0-9]*$"
                                    ErrorMessage="No es una letra valida" />

                                <asp:TextBox Visible="false" ID="txtDireccionBuscar" runat="server" CssClass="input--style-text-search" placeholder="Direccion" MaxLength="40" onkeydown="return(!(event.keyCode>=90));"> </asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtDireccionBuscar"
                                    ValidationExpression="[a-zA-Z 0-9]*$"
                                    ErrorMessage="No es una letra valida" />

                                <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" runat="server" ID="lblFchNac">
                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtFchNacBuscarPasada" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>


                                            <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                            <asp:TextBox ID="txtFchNacBuscarFutura" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                        </asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>


                        <div class="col-12">
                            <asp:Button ID="btnVolverFrm" class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Volver" Visible="false" OnClick="btnVolverFrm_Click" />
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        </div>



                        <div class="col-12 my-2">
                            <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                        </div>
                        <div class="rowLine" />
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
                                                HeaderText="Identificador del Cliente"
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



                                                    <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar" OnClick="btnSelected_Click" />
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
    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>

