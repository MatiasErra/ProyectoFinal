<%@ Page Language="C#" Title="Gestion de productos" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="Web.Paginas.Productos.frmProductos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">

                    <div class="col-12  text-center">
                        <h2 class="title">ABM Productos </h2>
                    </div>

                    <div class="col-12 text-center">
                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(event.keyCode<91 || event.keyCode==189);"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12 text-center">
                        <asp:Button ID="btnVolver" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Visible="false" Text="Volver" OnClick="btnVolver_Click" />
                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Añadir Producto
                        </button>
                    </div>

                    <!-- Modal Nuevo producto -->
                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Producto</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="input-group">
                                        <asp:TextBox ID="txtNombre" CssClass="input--style-2" runat="server" placeholder="Nombre" MaxLength="30" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]+$"
                                            ErrorMessage="No es un caracter valido" />
                                    </div>

                                    <div class="col-12 mb-5 input-group">
                                        <asp:DropDownList ID="listTipo" runat="server" CssClass="input--style-2"></asp:DropDownList>
                                    </div>

                                    <div class="col-12 mb-5 input-group">
                                        <asp:DropDownList ID="listTipoVenta" runat="server" CssClass="input--style-2"></asp:DropDownList>
                                    </div>

                                    <div>
                                        <p class="ms-1 mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                                            Imagen
                                        </p>

                                        <asp:FileUpload ID="fileImagen" CssClass="ms-1 input--style-2" runat="server" />
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 text-center">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="col-md-12 align-self-center text-center">
                    <div class="row align-self-center">
                        <div class="col-md-10 col-md-offset-1">
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


                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>

                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Lote?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox Visible="false" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>

