<%@ Page Language="C#" Title="Gestión de productos" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="Web.Paginas.Productos.frmProductos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 backforContent">
                <div class="row">

                    <div class="col-12 p-3">
                        <h2 class="title">Gestión de Productos </h2>


                        <div class="row text-center">
                            <div class=" col-sm-12">
                                <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                                <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                            </div>
                        </div>

                        <div class="row text-center">
                            <div class="col-sm-12">
                                <asp:TextBox Visible="false" ID="txtNombreBuscar" CssClass="input--style-text-search" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtNombreBuscar"
                                    ValidationExpression="^[a-zA-Z ]*$"
                                    ErrorMessage="No es una letra valida" />

                                <asp:DropDownList Visible="false" ID="lstTipoBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>

                                <asp:DropDownList Visible="false" ID="lstTipoVentaBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>

                                <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" runat="server" ID="lblPrecio">
                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtPrecioMenorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtPrecioMenorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                ErrorMessage="Solo numeros" />

                                            <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                            <asp:TextBox ID="txtPrecioMayorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtPrecioMayorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                ErrorMessage="Solo numeros" />
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-12">
                            <asp:Button ID="btnVolver" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Visible="false" Text="Volver" OnClick="btnVolver_Click" />
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                                Añadir Producto
                            </button>

                        </div>



                        <div class="col-12 my-2">
                            <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                        </div>
                        <div class="rowLine" />
                    </div>

                    <!-- Modal Nuevo producto -->
                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Producto</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="input-group">
                                        <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]+$"
                                            ErrorMessage="No es un carácter válido" />
                                    </div>

                                    <div class="input-group">
                                        <asp:DropDownList ID="listTipo" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                                    </div>

                                    <div class="input-group">
                                        <asp:DropDownList ID="listTipoVenta" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                                    </div>

                                    <div class="input-group">
                                        <asp:TextBox ID="txtPrecio" CssClass="input--style-tex" runat="server" placeholder="Precio" MaxLength="6" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtPrecio"
                                            ValidationExpression="^[0-9]+$"
                                            ErrorMessage="No es un carácter válido" />
                                    </div>

                                    <div class="row">
                                        <asp:Label ID="lblimg" class="text initText" Text="Cambiar Imagen" runat="server" />
                                        <asp:FileUpload ID="fileImagen" CssClass="m-1 input--style-lst initText" runat="server" />
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                        <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>




                    <div class="col-md-12 align-self-center text-center">
                        <div class="row align-self-center">
                            <div class="col-md-11 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstProducto" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>

                                                <asp:BoundField DataField="IdProducto"
                                                    HeaderText="Id del Producto"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Nombre"
                                                    HeaderText="Nombre"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Tipo"
                                                    HeaderText="Tipo"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="TipoVenta"
                                                    HeaderText="Tipo de venta"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Imagen"
                                                    HeaderText="Imagen"
                                                    HtmlEncode="false" />

                                                <asp:BoundField DataField="Precio"
                                                    HeaderText="Precio" />

                                                <asp:BoundField DataField="CantTotal"
                                                    HeaderText="Cantidad Total"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="CantRes"
                                                    HeaderText="Cantidad Reservada"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="CantDisp"
                                                    HeaderText="Cantidad Disponible"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField HeaderText="Opciones del administrador"
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Lote?')" OnClick="btnBaja_Click" />
                                                        <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>




                                        <asp:GridView ID="lstProductoSelect" Width="100%" SelectedIndex="1" AutoGenerateColumns="false" Visible="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>

                                                <asp:BoundField DataField="IdProducto"
                                                    HeaderText="Id del Producto"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Nombre"
                                                    HeaderText="Nombre"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Tipo"
                                                    HeaderText="Tipo"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="TipoVenta"
                                                    HeaderText="Tipo de venta"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Imagen"
                                                    HeaderText="Imagen"
                                                    HtmlEncode="false" />

                                                <asp:BoundField DataField="Precio"
                                                    HeaderText="Precio"
                                                    HtmlEncode="false" />

                                                <asp:BoundField DataField="CantTotal"
                                                    HeaderText="CantTotal"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="CantDisp"
                                                    HeaderText="Cantidad Disponible"
                                                    ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField HeaderText="Opciones del administrador"
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar" OnClick="btnSelected_Click" />
                                                        <asp:Button ID="Button1" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Lote?')" OnClick="btnBaja_Click" />
                                                        <asp:Button ID="Button2" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <asp:Label runat="server" ID="lblPaginas" CssClass="text pagStyle">
                            <div class="text-center">

                                <asp:Label runat="server" ID="txtPaginas" CssClass="text pagStyle" Text="Paginas" />
                                <div class="text-center">
                                    <asp:LinkButton ID="lblPaginaAnt" CssClass="text pagTextAct" OnClick="lblPaginaAnt_Click" runat="server"></asp:LinkButton>
                                    <asp:Label ID="lblPaginaAct" CssClass="text pagText" runat="server" Text=""></asp:Label>
                                    <asp:LinkButton ID="lblPaginaSig" CssClass="text pagTextAct" OnClick="lblPaginaSig_Click" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox Visible="false" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>

