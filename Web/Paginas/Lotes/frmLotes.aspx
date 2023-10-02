<%@ Page Title="Gestión de lotes" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmLotes.aspx.cs" Inherits="Web.Paginas.Lotes.frmLotes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="col-12">
                    <h2 class="title">Gestión de Lotes</h2>

                    <div class="row text-center">
                        <div class=" col-sm-12">
                            <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                            <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="Button1" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                    <div class="row text-center">
                        <div class="col-sm-12">
                            <asp:DropDownList Visible="false" ID="lstGranjaBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList Visible="false" ID="lstProductoBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList Visible="false" ID="lstDepositoBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" ID="lblFchProd" runat="server">
                                        <asp:Label class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox ID="txtFchProdMenor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>


                                        <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                        <asp:TextBox ID="txtFchProdMayor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </asp:Label>

                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" ID="lblFchCad" runat="server">
                                        <asp:Label class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox ID="txtFchCadMenor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>


                                        <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                        <asp:TextBox ID="txtFchCadMayor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </asp:Label>
                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" ID="lblCant" runat="server">
                                        <asp:Label class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox ID="txtCantMenor" runat="server" CssClass="input--style-text-search" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCantMenor"
                                            ValidationExpression="^[0-9]+$"
                                            ErrorMessage="No es un caracter valido" />


                                        <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                        <asp:TextBox ID="txtCantMayor" runat="server" CssClass="input--style-text-search" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCantMayor"
                                            ValidationExpression="^[0-9]+$"
                                            ErrorMessage="No es un caracter valido" />
                                    </asp:Label>
                                </div>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" ID="lblPrecio" runat="server">
                                        <asp:Label class="text initText" Text="Desde:" runat="server" />
                                        <asp:TextBox ID="txtPrecioMenor" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtPrecioMenor"
                                            ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                            ErrorMessage="Solo numeros">
                                        </asp:RegularExpressionValidator>


                                        <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                        <asp:TextBox ID="txtPrecioMayor" CssClass="input--style-text-search" runat="server" placeholder="Precio" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtPrecioMayor"
                                            ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                            ErrorMessage="Solo numeros">
                                        </asp:RegularExpressionValidator>
                                    </asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="col-12">

                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        <asp:Button ID="btnAltaLot" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Añadir Lote" OnClick="btnAltaLot_Click" />

                    </div>



                    <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                    </div>
                    <div class="rowLine" />
                </div>

                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-12 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstLote" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="NombreGranja"
                                                HeaderText="Granja"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="NombreProducto"
                                                HeaderText="Producto"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchProduccion"
                                                HeaderText="Fecha de producción"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchCaducidad"
                                                HeaderText="Fecha de caducidad"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Cantidad"
                                                HeaderText="Cantidad de  producción"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Precio"
                                                HeaderText="Precio"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="UbicacionDeposito"
                                                HeaderText="Depósito"
                                                ItemStyle-CssClass="GridStl" />

                                            
                                            <asp:TemplateField 
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>
                                                            <asp:Button ID="btnVerPestis" CssClass="btnE btn--radius btn--blue" runat="server" Text="Pesticidas" OnClick="btnVerPestis_Click" />
                                                    <asp:Button ID="btnVerFertis" CssClass="btnE btn--radius btn--blue" runat="server" Text="Fertilizantes" OnClick="btnVerFertis_Click" />
                                                          </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField 
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
</asp:Content>
