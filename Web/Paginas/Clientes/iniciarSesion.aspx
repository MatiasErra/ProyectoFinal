<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="iniciarSesion.aspx.cs" Inherits="Web.Paginas.Clientes.iniciarSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h2 class="title">Iniciar Sesión</h2>
        </div>
        <div class="card-body">


            <div class="input-group">
                <asp:TextBox ID="txtUser" runat="server" CssClass="input--style-2" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtUser"
                    ValidationExpression="[a-zA-Z0-9]*$"
                    ErrorMessage="No es una letra valida" />

            </div>


            <div class="input-group">
                <asp:TextBox ID="txtPass" runat="server" CssClass="input--style-2" TextMode="Password" MaxLength="40" placeholder="Contraseña" onkeydown="event.keyCode!=32);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtPass"
                    ValidationExpression="[a-zA-Z0-9-_.,]*$"
                    ErrorMessage="No es una letra valida" />
            </div>

            

        <div class="col-12">
            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
        </div>
            
        <div class="col-12">
            <asp:Button ID="btnIniciar" CssClass="btnE btn--radius btn--blue mt-1 mb-1" runat="server" Text="Iniciar Sesión" OnClick="btnIniciar_Click" />
            <asp:Button ID="btnRegist" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Registrarse" OnClick="btnRegist_Click" />
        </div>


    </div>
    </div>


</asp:Content>
