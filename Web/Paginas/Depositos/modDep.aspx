<%@ Page Title="Modificar depósito" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modDep.aspx.cs" Inherits="Web.Paginas.Depositos.modDep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Deposito </h2>

                </div>
           

                <div class="input-group">
                    <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <div class="input-group">
                    <asp:TextBox ID="txtCapacidad" CssClass="input--style-tex" runat="server" placeholder="Capacidad en toneladas" MaxLength="20" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtCapacidad"
                        ValidationExpression="^[0-9]*$"
                        ErrorMessage="No es un caracter valido" />
                </div>

                <div class="input-group">

                    <asp:TextBox ID="txtUbicacion" CssClass="input--style-tex" runat="server" placeholder="Ubicacion" MaxLength="50" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtUbicacion"
                        ValidationExpression="^[a-zA-Z0-9 ]*$"
                        ErrorMessage="No es una letra valida" />
                </div>

                <div class="input-group">

                    <asp:TextBox ID="txtTemperatura" CssClass="input--style-tex" runat="server" MaxLength="3" placeholder="Temperatura en °C" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtTemperatura"
                        ValidationExpression="^[0-9]*$"
                        ErrorMessage="No es un numero valido" />
                </div>

                <div class="input-group">

                    <asp:TextBox ID="txtCondiciones" CssClass="input--style-tex" runat="server" placeholder="Condiciones" MaxLength="80" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtCondiciones"
                        ValidationExpression="^[a-zA-Z ]*$"
                        ErrorMessage="No es una letra valida" />
                </div>

                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>
                </div>

                <div class="col-12">
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Deposito?')" OnClick="btnModificar_Click" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>







</asp:Content>
