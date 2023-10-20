<%@ Page Language="C#" Title="Estadisticas" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmEstadisticas.aspx.cs" Inherits="Web.Paginas.Admins.frmEstadisticas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="col-12">
                    <h2 class="title">Estadisticas</h2>

                    <div class="row text-center">
                        <div class=" col-sm-12">
                            <asp:DropDownList ID="listEstadisticas" CssClass="lstOrd btn--radius  align-self-center btn--srch" Width="200" AutoPostBack="true" OnSelectedIndexChanged="listEstadisticas_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>

                    <div class="row text-center">
                        <div class="col-sm-12">

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" ID="lblGananciaMesAnio" runat="server">
                                        <asp:TextBox ID="txtAnioGananciaBuscar" CssClass="input--style-text-search" runat="server" MaxLength="4" placeholder="Año" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtAnioGananciaBuscar"
                                            ValidationExpression="^\d{4}$"
                                            ErrorMessage="El año debe tener 4 numeros" />
                                        <asp:DropDownList Visible="false" ID="lstMesGananciaBuscar" CssClass="input--style-lst-search me-3" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnBuscarGanancia" Class="btnE btn--radius btn--green align-self-center btn--lst" runat="server" Text="Buscar" OnClick="btnBuscarGanancia_Click" />
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <asp:Label Visible="false" CssClass="text centerText" ID="lblGanancias" Text="" runat="server" />
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="rowLine" />

                    <div class="col-12 my-2">
                        <asp:Label CssClass="text centerText" ID="lblMensajes" runat="server"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
