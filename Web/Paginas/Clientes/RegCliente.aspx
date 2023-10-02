<%@ Page Title="Registrar cuenta" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="RegCliente.aspx.cs" Inherits="Web.Paginas.Clientes.RegCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Registrarse </h2>
                </div>


                <div class="input-group">

                    <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtNombre"
                        ValidationExpression="^[a-zA-Z ]*$"
                        ErrorMessage="No es una letra valida" />
                </div>

                <div class="input-group">

                    <asp:TextBox ID="txtApell" CssClass="input--style-tex" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                </div>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                    ControlToValidate="txtApell"
                    ValidationExpression="^[a-zA-Z ]*$"
                    ErrorMessage="No es una letra valida" />

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

                    <asp:TextBox ID="txtDir" runat="server" CssClass="input--style-tex" placeholder="Dirección" MaxLength="40" onkeydown="return(!(event.keyCode>=90));"> </asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtDir"
                        ValidationExpression="^[a-zA-Z 0-9]*$"
                        ErrorMessage="No es una letra valida" />
                </div>


                <div class="input-group">
                    <asp:Label class="text initText" Text="Fecha de nacimiento" runat="server" />


                    <asp:TextBox ID="txtFchNac" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>



                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                <div class="col-12">
                    <asp:Button ID="btnAlta" CssClass="btnE btn--radius btn--blue mt-1 mb-1" runat="server" Text="Registrarse" OnClick="btnAlta_Click" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>


</asp:Content>
