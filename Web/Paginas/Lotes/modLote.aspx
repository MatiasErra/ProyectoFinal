<%@ Page Language="C#" Title="Modificar lote" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modLote.aspx.cs" Inherits="Web.Paginas.Lotes.modLote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Lote </h2>

                </div>

                <div class="row">

                    <div class="col-xl-4  col-lg-12">
                        <asp:Label CssClass="text centerText " Text="Nombre de la Granja" runat="server"></asp:Label>
                        <div class=" col-lg-12">
                            <asp:Label ID="txtIdGranja" Visible="false" CssClass="text centerText " runat="server"></asp:Label>
                            <asp:Label ID="txtNomGranja" CssClass="text centerText " runat="server"></asp:Label>
                        </div>

                    </div>

                    <div class="col-xl-4 col-lg-12">
                        <asp:Label CssClass="text centerText " Text="Nombre del Producto" runat="server"></asp:Label>
                        <div class=" col-lg-12">
                            <asp:Label ID="txtIdProducto" Visible="false" CssClass="text centerText " runat="server"></asp:Label>
                            <asp:Label ID="txtNomProd" CssClass="text centerText " runat="server"></asp:Label>
                        </div>

                    </div>

                    <div class="col-xl-4 col-lg-12">
                        <asp:Label CssClass="text centerText " Text="Fecha de producción" runat="server"></asp:Label>
                        <div class="col-lg-12">
                            <asp:Label ID="txtFchProduccion" CssClass="text centerText" runat="server"></asp:Label>

                        </div>

                    </div>

                </div>





                <div class="rowLine">
                </div>


                <div class="input-group">
                    <asp:Label class="text initText" Text=" Fecha de caducidad" runat="server" />
                    <asp:TextBox ID="txtFchCaducidad" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="input-group">
                    <asp:Label ID="lblCantidad" class="text initText" Text="Cantidad" runat="server" />
                    <asp:TextBox ID="txtCantidad" CssClass="input--style-tex" runat="server" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtCantidad"
                        ValidationExpression="^[0-9]+$"
                        ErrorMessage="No es un carácter válido" />
                </div>

                <div class="input-group">
                    <asp:Label class="text initText" Text="Precio" runat="server" />
                    <asp:TextBox ID="txtPrecio" CssClass="input--style-tex" runat="server" placeholder="Stock" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtPrecio"
                        ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                        ErrorMessage="No es un carácter válido">
                    </asp:RegularExpressionValidator>
                </div>



                <div class="row">
                    <div class="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listDeposito" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscarDeposito" runat="server" Text="Buscar" OnClick="btnBuscarDeposito_Click" />
                    </div>
                </div>



                <div class="rowLine"></div>



                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                <div class="col-12">
                    <asp:Button ID="btnVerPestis" CssClass="btnE btn--radius btn--blue mt-1 mb-1" runat="server" Text="Ver Pesticidas del Lote" OnClick="btnVerPestis_Click" />
                    <asp:Button ID="btnVerFertis" CssClass="btnE btn--radius btn--blue mt-1 mb-1" runat="server" Text="Ver Fertilizantes del Lote" OnClick="btnVerFertis_Click" />
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este lote?')" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>
