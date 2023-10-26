<%@ Page Title="Iniciar sesion" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="iniciarSesion.aspx.cs" Inherits="Web.Paginas.Clientes.iniciarSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Iniciar Sesión</h2>
                </div>


                <div class="input-group">
                    <asp:TextBox ID="txtUser" runat="server" CssClass="input--style-tex" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtUser"
                        ValidationExpression="[a-zA-Z0-9]*$"
                        ErrorMessage="No es una letra valida" />

                </div>



                <div class="input-group">
                    <asp:TextBox ID="txtPass" runat="server" CssClass="input--style-tex" TextMode="Password" MaxLength="40" placeholder="Contraseña" onkeydown="return(event.keyCode!=32);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtPass"
                        ValidationExpression="[a-zA-Z0-9-_.,]*$"
                        ErrorMessage="No es una letra valida" />
                </div>



               <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>
                    </div>

                <div class="col-12">
                    <asp:Button ID="btnIniciar" CssClass="btnE btn--radius btn--blue mt-1 mb-1" runat="server" Text="Iniciar Sesión" OnClick="btnIniciar_Click" />
                    <asp:Button ID="btnRegist" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Registrarse" OnClick="btnRegist_Click" />
                </div>


            </div>
        </div>
    </div>


</asp:Content>
