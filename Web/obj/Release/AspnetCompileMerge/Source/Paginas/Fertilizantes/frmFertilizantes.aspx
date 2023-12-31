﻿<%@ Page Title="Gestión de Fertilizantes" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmFertilizantes.aspx.cs" Inherits="Web.Paginas.Fertilizantes.frmFertilizante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12 p-3">
                        <h2 class="title">Gestión de Fertilizantes </h2>


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

                                <asp:TextBox Visible="false" ID="txtTipoBuscar" CssClass="input--style-text-search" runat="server" placeholder="Tipo" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text centerText"
                                    ControlToValidate="txtTipoBuscar"
                                    ValidationExpression="^[a-zA-Z ]*$" />

                                <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" runat="server" ID="lblPh">
                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtPhMenorBuscar" CssClass="input--style-text-search" runat="server" placeholder="pH" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==190 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtPhMenorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
                                                ErrorMessage="Solo numeros" />

                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtPhMayorBuscar" CssClass="input--style-text-search" runat="server" placeholder="pH" MaxLength="4" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==190 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtPhMenorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
                                                ErrorMessage="Solo numeros" />
                                        </asp:Label>
                                    </div>
                                </div>

                                <asp:DropDownList Visible="false" ID="lstImpactoBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-12">
                            <asp:Button ID="btnVolverFerti" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Visible="false" Text="Volver" OnClick="btnVolverFerti_Click" />
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                                Añadir Fertilizante
                            </button>

                        </div>



                        <div class="col-12 my-2">
                            <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                        </div>
                        <div class="rowLine" />
                    </div>


                    <!-- Modal -->

                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Fertilizante</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>

                                <div class="modal-body">
                                    <div class="input-group">

                                        <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || event.keyCode==32 || event.keyCode==8 );"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtTipo" CssClass="input--style-tex" runat="server" placeholder="Tipo" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || event.keyCode==32 || event.keyCode==8 );"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtTipo"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />



                                    </div>



                                    <div class="input-group">

                                        <asp:TextBox ID="txtPH" CssClass="input--style-tex" MaxLength="4" runat="server" placeholder="PH" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==190 || event.keyCode==8);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtPH"
                                            ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" 
                                            ErrorMessage="Solo numeros">
                                        </asp:RegularExpressionValidator>


                                    </div>
                                    <div class="input-group">

                                        <asp:DropDownList ID="lstImpacto" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                                    </div>



                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                    <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>

                                </div>
                            </div>
                        </div>

                    </div>








                    <div class="col-md-12 align-self-center">
                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstFert" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>

                                                <asp:BoundField DataField="IdFertilizante"
                                                    HeaderText="Identificador del Fertilizante"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Nombre"
                                                    HeaderText="Nombre"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Tipo"
                                                    HeaderText="Tipo" ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="pH"
                                                    HeaderText="PH" ItemStyle-CssClass="GridStl" />


                                                <asp:BoundField DataField="Impacto"
                                                    HeaderText="Impacto" ItemStyle-CssClass="GridStl" />

                                                <asp:TemplateField HeaderText="Opciones del administrador"
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>




                                                        <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Fertilizante?')" OnClick="btnBaja_Click" />
                                                        <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                            </Columns>
                                        </asp:GridView>
                                    </div>










                                    <asp:GridView ID="lstFertSel" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdFertilizante"
                                                HeaderText="Identificador del Fertilizante"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Tipo"
                                                HeaderText="Tipo" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="pH"
                                                HeaderText="PH" ItemStyle-CssClass="GridStl" />


                                            <asp:BoundField DataField="Impacto"
                                                HeaderText="Impacto" ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>



                                                    <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar" OnClick="btnSelected_Click" />
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Fertilizante?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>
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
    </div>
    </div>

    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>


















</asp:Content>
