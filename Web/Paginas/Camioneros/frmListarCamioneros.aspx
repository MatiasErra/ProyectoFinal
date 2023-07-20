<%@ Page Title="Listado de camioneros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListarCamioneros.aspx.cs" Inherits="Web.Paginas.Camioneros.frmListarCamioneros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="CamList">
        <%-- <div>
            <asp:DropDownList CssClass="nav-link dropdown-toggle text-dark" ID="selectBuscar" runat="server" AutoPostBack="true" Width="600">
                <asp:ListItem Text="Buscar por"/>
                <asp:ListItem Text="Nombre" Value="nombre"/>
                <asp:ListItem Text="Apellido" Value="apellido"/>
                <asp:ListItem Text="Email" Value="email"/>
                <asp:ListItem Text="Telefono" Value="telefono"/>
                <asp:ListItem Text="Cedula" Value="cedula"/>
            </asp:DropDownList>
        </div> --%>

        <div class="d-flex">
            <asp:TextBox CssClass="form-control" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
            <asp:Button CssClass="btn btn-outline-dark ms-1" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>
        <div>
            <asp:ListBox ID="lstCamionero" runat="server" Height="600px" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged"></asp:ListBox>
        </div>

    </div>

</asp:Content>
