<%@ Page Title="Gestion de lotes" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmLotes.aspx.cs" Inherits="Web.Paginas.Lotes.frmLotes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">

                    <div class="col-12">
                        <h2 class="title">ABM Lotes </h2>
                    </div>

                    <div class="col-12">
                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(event.keyCode<91 || event.keyCode==189);"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Añadir Lote
                        </button>
                    </div>

                    <!-- Modal Nuevo lote -->
                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Lote</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-7">
                                            <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarGranja" runat="server" placeholder="Buscar granja" MaxLength="100" onkeydown="return(event.keyCode<91 || event.keyCode==189);"></asp:TextBox>

                                        </div>
                                        <div class="col-5">
                                            <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarGranja" runat="server" Text="Buscar" OnClick="btnBuscarGranja_Click" />
                                        </div>


                                    </div>
                                    <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5;">
                                        <div class="col-8">
                                            <asp:DropDownList ID="listGranja" CssClass="input--style-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <asp:Button ID="btnAltaGranja" class="btnE btn--radius btn--blue float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir granja" OnClick="btnAltaGranja_Click" />
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-7">
                                            <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarProducto" runat="server" placeholder="Buscar producto" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                                        </div>
                                        <div class="col-5">
                                            <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarProducto" runat="server" Text="Buscar" OnClick="btnBuscarProducto_Click" />
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5;">
                                        <div class="col-8">
                                            <asp:DropDownList ID="listProducto" CssClass="input--style-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <asp:Button ID="btnAltaProducto" class="btnE btn--radius btn--blue float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir Producto" OnClick="btnAltaProducto_Click" />
                                        </div>

                                    </div>

                                    <div class="input-group">
                                        <p class="ms-1 mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                                            Fecha de producción
                                        </p>
                                        <asp:TextBox ID="txtFchProduccion" runat="server" CssClass="ms-1 input--style-2 js-datepicker px-0 py-2" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </div>

                                    <div class="input-group ms-1">
                                        <asp:TextBox ID="txtCantidad" CssClass="input--style-2" runat="server" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCantidad"
                                            ValidationExpression="^[0-9]+$"
                                            ErrorMessage="No es un caracter valido" />
                                    </div>

                                    <div class="input-group ms-1">
                                        <asp:TextBox ID="txtPrecio" CssClass="input--style-2" runat="server" placeholder="Precio" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtPrecio"
                                            ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                            ErrorMessage="Solo numeros">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                    <div class="row">
                                        <div class="col-7">
                                            <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarDeposito" runat="server" placeholder="Buscar depósito" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                                        </div>
                                        <div class="col-5">
                                            <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarDeposito" runat="server" Text="Buscar" OnClick="btnBuscarDeposito_Click" />
                                        </div>
                                    </div>



                                    <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5;">
                                        <div class="col-8">
                                            <asp:DropDownList ID="listDeposito" CssClass="input--style-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <asp:Button ID="btnAltaDeposito" class="btnE btn--radius btn--blue float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir Depósito" OnClick="btnAltaDeposito_Click" />
                                        </div>

                                    </div>

                                 




                                 
                                    

                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstLote" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdGranja"
                                                HeaderText="Id de la Granja"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="IdProducto"
                                                HeaderText="Id del Producto"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchProduccion"
                                                HeaderText="Fecha de producción"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Cantidad"
                                                HeaderText="Cantidad de producto"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Precio"
                                                HeaderText="Precio"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="IdDeposito"
                                                HeaderText="Id del Depósito"
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
