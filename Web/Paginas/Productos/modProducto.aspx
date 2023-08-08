<%@ Page Language="C#" Title="Modificar producto" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modProducto.aspx.cs" Inherits="Web.Paginas.Productos.modProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="card mt-1 mb-1 w-50 m-auto  ">
        <div class="card-header text-center" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modificar Producto</h1>
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

            <div class="col-12 mt-1 mb-4 input-group">
                <asp:DropDownList ID="listTipo" runat="server" CssClass="input--style-2"></asp:DropDownList>
            </div>

            <div class="col-12 mt-1 mb-4 input-group">
                <asp:DropDownList ID="listTipoVenta" runat="server" CssClass="input--style-2"></asp:DropDownList>
            </div>

            <div>
                <p class="ms-1" style="color: #666; font-size: 16px; font-weight: 500;">
                    Imagen
                </p>
                <asp:Image CssClass="pb-2 input-group" ID="imgImagen" Width="150" runat="server" />
            </div>

            <div>
                <p class="ms-1 mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                Cambiar imagen
            </p>

                 <asp:FileUpload ID="fileImagen" CssClass="m-1 input--style-2 input-group" runat="server" />
            </div>

           



        </div>

    <div class="col-12 text-center">
        <asp:Label ID="lblMensajes" runat="server"></asp:Label>

    </div>
    <div class="col-12 text-center">
        <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este producto?')" />
        <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
    </div>


    </div>

</asp:Content>
