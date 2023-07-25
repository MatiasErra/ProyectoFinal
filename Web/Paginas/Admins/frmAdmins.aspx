<%@ Page Title="Listado y eliminacion de admins" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmAdmins.aspx.cs" Inherits="Web.Paginas.Admins.frmListarAdmins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h5>ABM Administrador </h5>
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                       <div class="col-12 align-self-center">
                        <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Admin?')" OnClick="btnBaja_Click" />
                    </div>

                        <div class="col-12">
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />

                    </div>

                    <div class="col-12">
                        <button type="button" class="btn btn-outline-dark m-1 w-25" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Modificar/Añadir Admin
                        </button>

                    </div>

                    <!-- Modal Nuevo admin -->
                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Admin</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="col-12">
                                        Nombre
                        <asp:TextBox ID="txtNombre" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="col-12">
                                        Apellido
                        <asp:TextBox ID="txtApell" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Apellido"  MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                    </div>
                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                        ControlToValidate="txtApell"
                                        ValidationExpression="^[a-zA-Z ]*$"
                                        ErrorMessage="No es una letra valida" />
                               
                                <div class="col-12">
                                    Email
                        <asp:TextBox ID="txtEmail" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server"  placeholder="Email"  onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                        ControlToValidate="txtEmail"
                                        ValidationExpression="^\S+@\S+$"
                                        ErrorMessage="No es un Email valido" />



                                </div>
                                <div class="col-12">
                                    Telefono
                        <asp:TextBox ID="txtTel" CssClass="form-control mt-1 mb-1 w-75 m-auto" MaxLength="9" runat="server" placeholder="Telefono"  onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>

                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2"
                                        ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="Debe ser un numero de 9 caracteres." />


                                </div>
                                <div class="col-12 mt-1 mb-1">
                                    Fecha de nacimiento<br />
                                    <asp:TextBox ID="txtFchNac" runat="server" CssClass="form-control mt-1 mb-1 w-75 m-auto" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    Usuario
                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control mt-1 mb-1 w-75 m-auto" placeholder="Nombre de Usuario" MaxLength="40"  onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                        ControlToValidate="txtUser"
                                        ValidationExpression="[a-zA-Z0-9]*$"
                                        ErrorMessage="No es una letra valida" />

                                </div>
                                <div class="col-12">
                                    Contraseña
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control mt-1 mb-1 w-75 m-auto" TextMode="Password" MaxLength="40" placeholder="Contraseña"   onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    Tipo de Admin

                        <asp:DropDownList ID="listTipoAdmin" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server">
                        </asp:DropDownList>
                                </div>

                            </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                      <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Admin?')" OnClick="btnModificar_Click" />
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                               
                                </div>
                            </div>
                        </div>
                     </div>
                 


                 

                  
                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:ListBox ID="lstAdmin" runat="server" AutoPostBack="true" CssClass="w-auto h-auto" OnSelectedIndexChanged="lstAdmin_SelectedIndexChanged"></asp:ListBox>
            </div>
         </div>

    </div>
  
    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>
