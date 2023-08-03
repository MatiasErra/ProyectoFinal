<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="RegCliente.aspx.cs" Inherits="Web.Paginas.Clientes.RegCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
             <h2 class="title">Registrarse </h2>
        </div>
        <div class="card-body">


            <div class="input-group">

                <asp:TextBox ID="txtNombre" CssClass="input--style-2" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtNombre"
                    ValidationExpression="^[a-zA-Z ]*$"
                    ErrorMessage="No es una letra valida" />
            </div>

            <div class="input-group">

                <asp:TextBox ID="txtApell" CssClass="input--style-2" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
            </div>
            <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                ControlToValidate="txtApell"
                ValidationExpression="^[a-zA-Z ]*$"
                ErrorMessage="No es una letra valida" />

            <div class="input-group">

                <asp:TextBox ID="txtEmail" CssClass="input--style-2" runat="server" placeholder="Email" onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^\S+@\S+$"
                    ErrorMessage="No es un Email valido" />



            </div>
            <div class="input-group">

                <asp:TextBox ID="txtTel" CssClass="input--style-2" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>

                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2"
                    ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="Debe ser un numero de 9 caracteres." />


            </div>

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
        <div class="input-group">

            <asp:TextBox ID="txtDir" runat="server" CssClass="input--style-2" placeholder="Dirección" MaxLength="40" onkeydown="return(!(event.keyCode>=90));"> </asp:TextBox>
            <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                ControlToValidate="txtDir"
                ValidationExpression="^[a-zA-Z 0-9]*$"
                ErrorMessage="No es una letra valida" />
        </div>

        <div class="input-group">
            Fecha de nacimiento<br />
            <asp:TextBox ID="txtFchNac" runat="server" CssClass="input--style-2 js-datepicker px-0 py-2" placeholder="Telefono" TextMode="Date"></asp:TextBox>
        </div>





        <div class="col-12">
            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
        </div>


        <div class="col-12">
            <asp:Button ID="btnAlta" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Registrarse" OnClick="btnAlta_Click" />
        </div>


    </div>
    </div>
 


</asp:Content>
