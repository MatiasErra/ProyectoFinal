<%@ Page Title="Modificar admin" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modAdmin.aspx.cs" Inherits="Web.Paginas.Admins.modAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Administrador </h2>
                </div>

                <div class="input-group">
                    <asp:TextBox ID="txtId"  CssClass="input--style-tex" ReadOnly="false" placeholder="Id" runat="server" Enabled="False"></asp:TextBox>
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



                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                   <div class ="rowLine"></div>


                <div class="col-12">
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este Administrador?')" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>
















</asp:Content>
