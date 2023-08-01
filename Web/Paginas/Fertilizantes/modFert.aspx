<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modFert.aspx.cs" Inherits="Web.Paginas.Fertilizantes.modFert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modificar Deposito</h1>
        </div>
        <div class="card-body">


                <div class="input-group">
                <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-2" placeholder="" runat="server" Enabled="False"></asp:TextBox>
            </div>


            <div class="input-group">

                <asp:TextBox ID="txtNombre" CssClass="input--style-2" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtNombre"
                    ValidationExpression="^[a-zA-Z ]*$"
                    ErrorMessage="No es una letra valida" />
            </div>

            <div class="input-group">

                <asp:TextBox ID="txtTipo" CssClass="input--style-2" runat="server" placeholder="Tipo" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
            </div>
            <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                ControlToValidate="txtTipo"
                ValidationExpression="^[a-zA-Z ]*$"
                ErrorMessage="No es una letra valida" />



            <div class="input-group">

                <asp:TextBox ID="txtPH" CssClass="input--style-2" MaxLength="2" runat="server" placeholder="PH" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>



            </div>
            <div class="col-12 mt-1 mb-1">

                <asp:DropDownList ID="lstImpacto" runat="server" CssClass="input--style-2"></asp:DropDownList>
            </div>

            <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>


            <div class="col-12">
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este Administrador?')" />
                <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>


        </div>
</asp:Content>
