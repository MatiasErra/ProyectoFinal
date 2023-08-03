<%@ Page Language="C#" Title="Modificar granja" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="modGranja.aspx.cs" Inherits="Web.Paginas.Granjas.modGranja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modificar Granja</h1>
        </div>
        <div class="card-body">

            <div class="input-group">
                <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-2" placeholder="" runat="server" Enabled="False"></asp:TextBox>
            </div>


            <div class="input-group">
                <asp:TextBox ID="txtNombre" CssClass="input--style-2" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtNombre"
                    ValidationExpression="^[a-zA-Z ]+$"
                    ErrorMessage="No es un caracter valido" />
            </div>

            <div class="input-group">
                <asp:TextBox ID="txtUbicacion" CssClass="input--style-2" runat="server" placeholder="Ubicacion" MaxLength="50" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtUbicacion"
                    ValidationExpression="^[a-zA-Z ]+$"
                    ErrorMessage="No es una letra valida" />
            </div>
            <div class="row">
                <div class="col-7">
                    <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarDueño" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                </div>
                <div class="col-5">
                    <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarDueño" runat="server" Text="Buscar" OnClick="btnBuscarDueño_Click" />
                </div>
            </div>
            <div>
                <asp:DropDownList ID="listDueño" CssClass="input--style-2" runat="server">
                </asp:DropDownList>

            </div>



            <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>
            <div class="col-12">
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar esta Granja?')" />
                <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>


        </div>
    </div>
</asp:Content>
