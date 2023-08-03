<%@ Page Language="C#" Title="Modificar produce" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="modProduce.aspx.cs" Inherits="Web.Paginas.Producen.modProduce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modificar Produce</h1>
        </div>
        <div class="card-body">
            <div class="input-group">
                Id granja<br />
                <asp:TextBox ID="txtIdGranja" Enabled="false" CssClass="input--style-2" runat="server"></asp:TextBox>        
            </div>

            <div class="input-group">
                Id producto<br />
                <asp:TextBox ID="txtIdProducto" Enabled="false" CssClass="input--style-2" runat="server"></asp:TextBox>        
            </div>

            <div class="input-group">
                Fecha de produccion<br />
                <asp:TextBox ID="txtFchProduccion" Enabled="false" runat="server" CssClass="input--style-2 js-datepicker px-0 py-2" TextMode="Date"></asp:TextBox>
            </div>

            <div class="input-group">
                <asp:TextBox ID="txtStock" CssClass="input--style-2" runat="server" placeholder="Stock" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtStock"
                    ValidationExpression="^[0-9]+$"
                    ErrorMessage="No es un caracter valido" />
            </div>

            <div class="input-group">
                <asp:TextBox ID="txtPrecio" CssClass="input--style-2" runat="server" placeholder="Stock" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtPrecio"
                    ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                    ErrorMessage="Solo numeros">
                </asp:RegularExpressionValidator>
            </div>

            <div class="row">
                <div class="col-7">
                    <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarDeposito" runat="server" placeholder="Buscar deposito" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                </div>
                <div class="col-5">
                    <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarDeposito" runat="server" Text="Buscar" OnClick="btnBuscarDeposito_Click" />
                </div>
            </div>
            <div>
                <asp:DropDownList ID="listDeposito" CssClass="input--style-2" runat="server">
                </asp:DropDownList>
            </div>



            <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>
            <div class="col-12">
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este produce?')" />
                <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>


        </div>
    </div>
</asp:Content>
