<%@ Page Title="Modificar pesticida" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modPest.aspx.cs" Inherits="Web.Paginas.Pesticidas.modPest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Pesticida </h2>
                </div>



                <div class="input-group">
                    <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
                </div>


                <div class="input-group">

                    <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || event.keyCode==32 || event.keyCode==8 );"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtNombre"
                        ValidationExpression="^[a-zA-Z ]*$"
                        ErrorMessage="No es una letra valida" />
                </div>

                <div class="input-group">

                    <asp:TextBox ID="txtTipo" CssClass="input--style-tex" runat="server" placeholder="Tipo" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || event.keyCode==32 || event.keyCode==8 );"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtTipo"
                        ValidationExpression="^[a-zA-Z ]*$"
                        ErrorMessage="No es una letra valida" />

                </div>




                <div class="input-group">

                    <asp:TextBox ID="txtPH" CssClass="input--style-tex" MaxLength="4" runat="server" placeholder="PH" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==190 || event.keyCode==8);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtPH"
                        ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
                        ErrorMessage="Solo numeros">
                    </asp:RegularExpressionValidator>




                </div>
                <div class="input-group">

                    <asp:DropDownList ID="lstImpacto" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                </div>
                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>


                <div class="col-12">
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este Administrador?')" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>
</asp:Content>
