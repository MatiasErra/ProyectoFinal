<%@ Page Language="C#" Title="Auditoria" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmAuditoria.aspx.cs" Inherits="Web.Paginas.Admins.frmAuditoria" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="col-12">
                    <h2 class="title">Auditoria</h2>

                    <div class="row text-center">
                        <div class=" col-sm-12">
                            <asp:DropDownList ID="listBuscarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listBuscarPor_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="listOrdenarPor" CssClass="lstOrd btn--radius  align-self-center btn--srch " Width="200" runat="server"></asp:DropDownList>
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="Button1" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                    <div class="row text-center">
                        <div class="col-sm-12">
                            <asp:DropDownList Visible="false" ID="lstTablaBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList Visible="false" ID="lstAdminBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>
                            <asp:Button Visible="false" CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarAdmin" runat="server" Text="Buscar Admin" OnClick="btnBuscarAdmin_Click" />

                            <asp:DropDownList Visible="false" ID="lstTipoBuscar" CssClass="input--style-lst-search" runat="server">
                            </asp:DropDownList>

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
                                    <asp:GridView ID="lstAuditoria" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>
                                            <asp:BoundField DataField="IdAuditoria"
                                                HeaderText="Identificador de la Auditoria"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="IdAdmin"
                                                HeaderText="Identificador del Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="NombreAdmin"
                                                HeaderText="Nombre del Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="ApellidoAdmin"
                                                HeaderText="Apellido del Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Fecha"
                                                HeaderText="Fecha"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Tabla"
                                                HeaderText="Tabla"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Tipo"
                                                HeaderText="Tipo"
                                                ItemStyle-CssClass="GridStl" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="text-center">

                        <asp:Label runat="server" ID="txtPaginas" CssClass="text pagStyle" Text="Paginas" />
                        <div class="text-center">
                            <asp:LinkButton ID="lblPaginaAnt" CssClass="text pagTextAct" OnClick="lblPaginaAnt_Click" runat="server"></asp:LinkButton>
                            <asp:Label ID="lblPaginaAct" CssClass="text pagText" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="lblPaginaSig" CssClass="text pagTextAct" OnClick="lblPaginaSig_Click" runat="server"></asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
