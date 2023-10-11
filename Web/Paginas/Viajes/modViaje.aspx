<%@ Page Language="C#" AutoEventWireup="true" Title="Modificar viaje" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="modViaje.aspx.cs" Inherits="Web.Paginas.Viajes.modViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-lg-7 col-sm-11 m-3 p-3 text-center backforContent">
                <div class="row rowLine">
                    <h2 class="title">Modificar Granja </h2>
                </div>


                <div class="input-group">
                    <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-tex" placeholder="" runat="server" Enabled="False"></asp:TextBox>
                </div>


                <div class="input-group">
                    <asp:TextBox ID="txtCosto" CssClass="input--style-tex" runat="server" placeholder="Costo" MaxLength="6" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                        ControlToValidate="txtCosto"
                        ValidationExpression="^[0-9]+$"
                        ErrorMessage="No es un carácter válido" />
                </div>

                <div class="input-group">
                    <asp:Label class="text initText" Text=" Fecha del viaje" runat="server" />
                    <asp:TextBox ID="txtFch" runat="server" CssClass=" input--style-tex js-datepicker" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>



                <div class="row">
                    <asp:Label runat="server" ID="lblCamion" CssClass="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listCamion" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>

                    </asp:Label>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamion" runat="server" Text="Buscar Camion" OnClick="btnBuscarCamion_Click" />
                    </div>
                </div>

                <div class="row">
                    <asp:Label runat="server" ID="lblCamionero" CssClass="col-xl-9 col-lg-12">
                        <asp:DropDownList ID="listCamionero" CssClass="input--style-lst" runat="server">
                        </asp:DropDownList>

                    </asp:Label>
                    <div class="col-xl-3 col-lg-12">
                        <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarCamionero" runat="server" Text="Buscar Camionero" OnClick="btnBuscarCamionero_Click" />
                    </div>
                </div>

                <div class="input-group">

                    <asp:DropDownList ID="listEstado" runat="server" CssClass="input--style-lst"></asp:DropDownList>
                </div>



                <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                </div>

                <div class="row rowLine">
                </div>

                <div class="col-12">
                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar esta Granja?')" />
                    <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                </div>


            </div>
        </div>
    </div>
</asp:Content>
