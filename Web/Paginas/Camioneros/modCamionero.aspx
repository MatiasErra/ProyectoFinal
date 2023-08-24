<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modCamionero.aspx.cs" Inherits="Web.Paginas.Camioneros.modCamionero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                        <h2 class="title">Modificar Camionero</h2>
                    </div>

                    <div class="input-group">
                        <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
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
                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtApell"
                            ValidationExpression="^[a-zA-Z ]*$"
                            ErrorMessage="No es una letra valida" />
                    </div>

                    <div class="input-group">

                        <asp:TextBox ID="txtEmail" CssClass="input--style-tex" runat="server" placeholder="Email"></asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtEmail"
                            ValidationExpression="^\S+@\S+$"
                            ErrorMessage="No es un Email valido" />
                    </div>

                    <div class="input-group">

                        <asp:TextBox ID="txtTel" CssClass="input--style-tex" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2" class="text initText"
                            ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="El teléfono debe tener 9 caracteres." />
                    </div>


                    <div class="input-group">

                        <asp:TextBox ID="txtCedula" CssClass="input--style-tex" MaxLength="8" runat="server" placeholder="Cedula" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCedula" ID="RegularExpressionValidator1" class="text initText"
                            ValidationExpression="^[\s\S]{8,}$" runat="server" ErrorMessage="La cedula debe tener 8 caracteres." />
                    </div>

                    <div class="input-group">

                        <asp:DropDownList ID="lstDisponible" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>
                    </div>



                    <div class="input-group">
                        <asp:Label class="text initText" Text=" Fecha de nacimiento" runat="server" />
                        <asp:TextBox ID="txtFchNac" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                    </div>



                    <div class="input-group">
                        <asp:Label class="text initText" Text=" Fecha de vencimiento de carnet de manejo" runat="server" />
                        <asp:TextBox ID="txtFchManejo" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                    </div>


                    <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnModificar" class="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Camionero?')" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                    </div>
                </div>



            </div>

        </div>




</asp:Content>
