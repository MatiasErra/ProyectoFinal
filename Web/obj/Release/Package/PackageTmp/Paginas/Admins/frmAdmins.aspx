<%@ Page Title="Gestión de admins" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmAdmins.aspx.cs" Inherits="Web.Paginas.Admins.frmListarAdmins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12 p-3">
                        <h2 class="title">Gestión de Administradores </h2>


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

                            

                                <asp:TextBox Visible="false" ID="txtUsuarioBuscar" runat="server" CssClass="input--style-text-search" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtUsuarioBuscar"
                                    ValidationExpression="[a-zA-Z0-9]*$"
                                    ErrorMessage="No es una letra valida" />

                                <asp:DropDownList Visible="false" ID="lstTipoAdminBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>

                                <asp:DropDownList Visible="false" ID="lstEstadoBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>

                               

                            </div>
                        </div>


                        <div class="col-12">
                            <asp:Button ID="btnVolver" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Visible="false" Text="Volver" OnClick="btnVolver_Click" />
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                                Añadir Administrador
                            </button>

                        </div>



                        <div class="col-12 my-2">
                            <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                        </div>
                        <div class="rowLine" />
                    </div>
                    <!-- Modal Nuevo admin -->

                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Admin</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="input-group">

                                        <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtApell" CssClass="input--style-tex" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>

                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtApell"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />

                                    </div>
                                    <div class="input-group">

                                        <asp:TextBox ID="txtEmail" CssClass="input--style-tex" runat="server" placeholder="Email" onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtEmail"
                                            ValidationExpression="^\S+@\S+$"
                                            ErrorMessage="No es un Email valido" />

                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtTel" CssClass="input--style-tex" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>

                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2" class="text initText"
                                            ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="El teléfono debe de tener 9 caracteres." />


                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtUser" runat="server" CssClass="input--style-tex" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtUser"
                                            ValidationExpression="[a-zA-Z0-9]*$"
                                            ErrorMessage="No es una letra valida" />

                                    </div>
                                    <div class="input-group">

                                        <asp:TextBox ID="txtPass" runat="server" CssClass="input--style-tex" TextMode="Password" MaxLength="40" placeholder="Contraseña" onkeydown="event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtPass"
                                            ValidationExpression="[a-zA-Z0-9-_.,]*$"
                                            ErrorMessage="No es una letra valida" />

                                    </div>




                                    <div class="input-group">
                                        <asp:DropDownList ID="listTipoAdmin" CssClass="input--style-lst" runat="server">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="input-group">
                                        <asp:DropDownList ID="lstEstado" CssClass="input--style-lst" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="input-group">
                                        <asp:Label class="text initText" Text="Fecha de nacimiento" runat="server" />
                                        <asp:TextBox ID="txtFchNac" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </div>


                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                        <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>




                </div>

                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-12 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstAdmin" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Large" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdPersona"
                                                HeaderText="Identificador del Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Apellido"
                                                HeaderText="Apellido" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Email"
                                                HeaderText="E-Mail" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Telefono"
                                                HeaderText="Teléfono" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchNacimiento"
                                                HeaderText="Fecha de Nacimiento" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="User"
                                                HeaderText="Usuario" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="TipoDeAdmin"
                                                HeaderText="Tipo de Admin" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Estado"
                                                HeaderText="Estado" ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>




                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Administrador?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="lstAdminSel" Visible="false" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Large" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdPersona"
                                                HeaderText="Identificador del Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Apellido"
                                                HeaderText="Apellido" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Email"
                                                HeaderText="E-Mail" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Telefono"
                                                HeaderText="Teléfono" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchNacimiento"
                                                HeaderText="Fecha de Nacimiento" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="User"
                                                HeaderText="Usuario" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="TipoDeAdmin"
                                                HeaderText="Tipo de Admin" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Estado"
                                                HeaderText="Estado" ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>



                                                    <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar" OnClick="btnSelected_Click" />
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Administrador?')" OnClick="btnBaja_Click" />
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
