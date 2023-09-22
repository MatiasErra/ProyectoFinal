<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmAltaLotes.aspx.cs" Inherits="Web.Paginas.Lotes.frmAltaLotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Añadir Lote </h2>

                </div>

                <div class="row ">
                    <div class="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listGranja" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button ID="btnAltaGranja" class="btnE btn--radius btn--blue align-self-center btn--srch" runat="server" Text="Buscar Granja" OnClick="btnAltaGranja_Click" />
                    </div>

                </div>
                <div class="rowLine"></div>



                <div class="row ">
                    <div class="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listProducto" AutoPostBack="True" CssClass="input--style-lst" runat="server" OnSelectedIndexChanged="listProductoUpdate">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button ID="btnAltaProducto" class="btnE btn--radius btn--blue align-self-center btn--srch" runat="server" Text="Buscar Producto" OnClick="btnAltaProducto_Click" />
                    </div>



                </div>
                <div class="rowLine"></div>

                <div class="input-group">
                    <asp:Label class="text initText" Text=" Fecha de producción" runat="server" />


                    <asp:TextBox ID="txtFchProduccion" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>

                <div class="input-group">
                    <asp:Label class="text initText" Text=" Fecha de caducidad" runat="server" />


                    <asp:TextBox ID="txtFchCaducidad" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>



                <div class="input-group">
                    <asp:Label ID="lblCantidad" class="text initText" Text="Cantidad" runat="server" />
                    <asp:TextBox ID="txtCantidad" CssClass="input--style-tex" runat="server" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtCantidad"
                        ValidationExpression="^[0-9]+$"
                        ErrorMessage="No es un caracter valido" />
                </div>

                <div class="input-group">
                    <asp:TextBox ID="txtPrecio" CssClass="input--style-tex" runat="server" placeholder="Precio" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                        ControlToValidate="txtPrecio"
                        ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                        ErrorMessage="Solo numeros">
                    </asp:RegularExpressionValidator>
                </div>



                <div class="row">
                    <div class="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listDeposito" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button ID="btnAltaDeposito" class="btnE btn--radius btn--blue align-self-center btn--srch" runat="server" Text="Buscar Depósito" OnClick="btnAltaDeposito_Click" />
                    </div>

                </div>
                <div class="rowLine"></div>

                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                <div class="col-12">
                    <asp:Button ID="btnAlta" class="btnE btn--radius btn--green align-self-center btn--srch" runat="server" Text="Alta Lote" OnClick="btnAlta_Click" />
                </div>









            </div>
        </div>

    </div>










</asp:Content>
