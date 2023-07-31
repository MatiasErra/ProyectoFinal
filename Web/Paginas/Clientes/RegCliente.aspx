<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="RegCliente.aspx.cs" Inherits="Web.Paginas.Clientes.RegCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h5>Registrarse </h5>

                    </div>


                    <div class="col-12">
                        Nombre
    <asp:TextBox ID="txtNombre" CssClass="form-control mt-1 mb-1 w-50 m-auto" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                            ControlToValidate="txtNombre"
                            ValidationExpression="^[a-zA-Z ]*$"
                            ErrorMessage="No es una letra valida" />
                    </div>

                    <div class="col-12">
                        Apellido
                        <asp:TextBox ID="txtApell" CssClass="form-control mt-1 mb-1 w-50 m-auto" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtApell"
                        ValidationExpression="^[a-zA-Z ]*$"
                        ErrorMessage="No es una letra valida" />

                    <div class="col-12">
                        Email
                        <asp:TextBox ID="txtEmail" CssClass="form-control mt-1 mb-1 w-50 m-auto" runat="server" placeholder="Email" onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                            ControlToValidate="txtEmail"
                            ValidationExpression="^\S+@\S+$"
                            ErrorMessage="No es un Email valido" />



                    </div>
                    <div class="col-12">
                        Telefono
                        <asp:TextBox ID="txtTel" CssClass="form-control mt-1 mb-1 w-50 m-auto" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>

                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2"
                            ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="Debe ser un numero de 9 caracteres." />


                    </div>
                    <div class="col-12 mt-1 mb-1">
                        Fecha de nacimiento<br />
                        <asp:TextBox ID="txtFchNac" runat="server" CssClass="form-control mt-1 mb-1 w-50 m-auto" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Usuario
                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control mt-1 mb-1 w-50 m-auto" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                            ControlToValidate="txtUser"
                            ValidationExpression="[a-zA-Z0-9]*$"
                            ErrorMessage="No es una letra valida" />

                    </div>
                    <div class="col-12">
                        Contraseña
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control mt-1 mb-1 w-50 m-auto" TextMode="Password" MaxLength="40" placeholder="Contraseña" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Dirección

                                      <asp:TextBox ID="txtDir" runat="server" CssClass="form-control mt-1 mb-1 w-50 m-auto" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=90));"> </asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                            ControlToValidate="txtDir"
                            ValidationExpression="^[a-zA-Z 0-9]*$"
                            ErrorMessage="No es una letra valida" />


                        <div class="col-12">
                            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                        </div>

                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnAlta" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                    </div>


                </div>
            </div>
        </div>

    </div>


</asp:Content>
