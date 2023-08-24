<%@ Page Language="C#" Title="Modificar producto" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modProducto.aspx.cs" Inherits="Web.Paginas.Productos.modProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-sm-11 m-3 p-3 text-center backforContent">
                
                    <h2 class="title">Modificar Producto </h2>
           <div class="row rowLine">     </div>






                <div class="input-group">
                    <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <div class="input-group">
                    <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtNombre"
                        ValidationExpression="^[a-zA-Z ]+$"
                        ErrorMessage="No es un caracter valido" />
                </div>

                <div class="input-group">
                    <asp:DropDownList ID="listTipo" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                </div>

                <div class="input-group">
                    <asp:DropDownList ID="listTipoVenta" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                </div>

                <div class="input-group">
                    <asp:Label ID="lblimg" class="text initText" Text="Imagen" runat="server" />
                   
                </div>
                 <div class ="row">
                    <asp:Image CssClass="pb-2 input-group" ID="imgImagen" Width="150" runat="server" />

                    </div>

                 <div class ="row">
                 <asp:Label ID="lblNewImg" class="text initText" Text="Cambiar Imagen" runat="server" />
                    <asp:FileUpload ID="fileImagen" CssClass="m-1 input--style-lst text initText" runat="server" />
                </div>


                <div class="rowLine"></div>


          

             <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>



            <div class="col-12 text-center">
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este producto?')" />
                <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>


        </div>
    </div>
     </div>
</asp:Content>
