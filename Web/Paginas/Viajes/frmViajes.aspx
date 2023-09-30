<%@ Page Language="C#" Title="Gestion de Viajes" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmViajes.aspx.cs" Inherits="Web.Paginas.Viajes.frmViajes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12 p-3">
                        <h2 class="title">Gestion de Viajes </h2>


                        <div class="row text-center">
                            <div class=" col-sm-12">
                                <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                                <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                            </div>
                        </div>

                        <div class="row text-center">
                            <div class="col-sm-12">

                                <asp:DropDownList Visible="false" ID="lstCamionBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>
                                <asp:Button Visible="false" CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamionBuscar" runat="server" Text="Buscar Camión" OnClick="btnBuscarCamionBuscar_Click" />

                                <asp:DropDownList Visible="false" ID="lstCamioneroBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>
                                <asp:Button Visible="false" CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamioneroBuscar" runat="server" Text="Buscar Camionero" OnClick="btnBuscarCamioneroBuscar_Click" />

                                <asp:DropDownList Visible="false" ID="lstEstadoBuscar" CssClass="input--style-lst-search" runat="server">
                                </asp:DropDownList>


                                <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" runat="server" ID="lblCosto">
                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtCostoMenorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Costo" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtCostoMenorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                ErrorMessage="Solo numeros" />

                                            <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                            <asp:TextBox ID="txtCostoMayorBuscar" CssClass="input--style-text-search" runat="server" placeholder="Costo" MaxLength="6" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                                ControlToValidate="txtCostoMayorBuscar"
                                                ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                                                ErrorMessage="Solo numeros" />
                                        </asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center">
                                    <div class="col-lg-6">
                                        <asp:Label Visible="false" ID="lblFch" runat="server">
                                            <asp:Label class="text initText" Text="Desde:" runat="server" />
                                            <asp:TextBox ID="txtFchMenor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>


                                            <asp:Label class="text initText" Text="Hasta:" runat="server" />
                                            <asp:TextBox ID="txtFchMayor" runat="server" CssClass=" input--style-text-search js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                        </asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-12">
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                                Añadir Viaje
                            </button>

                        </div>



                        <div class="col-12 my-2">
                            <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                        </div>
                        <div class="rowLine" />
                    </div>


                    <!-- Modal Nuevo viaje -->

                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Viaje</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="input-group">
                                        <asp:TextBox ID="txtCosto" CssClass="input--style-tex" runat="server" placeholder="Costo" MaxLength="6" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtCosto"
                                            ValidationExpression="^[0-9]+$"
                                            ErrorMessage="No es un carácter válido" />
                                    </div>

                                    <div class="input-group">
                                        <asp:Label class="text initText" Text=" Fecha del viaje" runat="server" />
                                        <asp:TextBox ID="txtFch" runat="server" CssClass=" input--style-tex js-datepicker " placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </div>


                                    <div class="row">
                                        <div class="col-xl-7 col-lg-12">
                                            <asp:DropDownList ID="listCamion" CssClass="input--style-lst" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-xl-5 col-lg-12">
                                            <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamion" runat="server" Text="Buscar Camion" OnClick="btnBuscarCamion_Click" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-7 col-lg-12">
                                            <asp:DropDownList ID="listCamionero" CssClass="input--style-lst" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-xl-5 col-lg-12">
                                            <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamionero" runat="server" Text="Buscar Camionero" OnClick="btnBuscarCamionero_Click" />
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                        <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstViaje" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdViaje"
                                                HeaderText="Identificador del Viaje"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Costo"
                                                HeaderText="Costo"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Fecha"
                                                HeaderText="Fecha"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="MarcaCamion"
                                                HeaderText="Marca del Camión"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="ModeloCamion"
                                                HeaderText="Modelo del Camión"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="NombreCamionero"
                                                HeaderText="Nombre del Camionero"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Estado"
                                                HeaderText="Estado del Viaje"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAsignarPaqu" CssClass="btnE btn--radius btn--green" runat="server" Text="Asignar Lotes" OnClick="btnAsignarPaqu_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>



                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Viaje?')" OnClick="btnBaja_Click" />


                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>


                                                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVerLote" CssClass="btnE btn--radius btn--blue" runat="server" Text="Ver Lotes Asignados" OnClick="btnVerLotes_Click" />

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


    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>
