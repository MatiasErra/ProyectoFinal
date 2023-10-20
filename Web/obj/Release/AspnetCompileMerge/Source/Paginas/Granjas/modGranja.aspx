<%@ Page Language="C#" Title="Modificar granja" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="modGranja.aspx.cs" Inherits="Web.Paginas.Granjas.modGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Granja </h2>
                </div>


                <div class="input-group">
                    <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
                </div>


                <div class="input-group">
                    <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtNombre"
                        ValidationExpression="^[a-zA-Z ]+$"
                        ErrorMessage="No es un caracter valido" />
                </div>

                <div class="input-group">
                    <asp:TextBox ID="txtUbicacion" CssClass="input--style-tex" runat="server" placeholder="Ubicacion" MaxLength="50" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtUbicacion"
                        ValidationExpression="^[a-zA-Z0-9 ]+$"
                        ErrorMessage="No es una letra valida" />
                </div>



                <div class="row">
                    <div class="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listDueño" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>

                    </div>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarDueño" runat="server" Text="Buscar Dueño" OnClick="btnBuscarDueño_Click" />
                    </div>
                </div>



                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                <div class="row rowLine">
                    </div>

                <div class="col-12">
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar esta Granja?')" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>
</asp:Content>
