<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDeposito.aspx.cs" Inherits="Web.Paginas.frmDeposito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        Id&nbsp;
        <asp:TextBox ID="txtId" runat="server" Enabled="False"></asp:TextBox>
    </p>
    <p>
        Capacidad&nbsp;
        <asp:TextBox ID="txtCapacidad" runat="server" placeholder="Capacidad" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"    ></asp:TextBox>
    </p>
    <p>
        Ubicacion&nbsp;
        <asp:TextBox ID="txtUbicacion" runat="server" placeholder="Ubicacion" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
    </p>
    <p>
        Temperatura&nbsp;
        <asp:TextBox ID="txtTemperatura" runat="server" placeholder="Temperatura" onkeydown = "return(!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
    </p>
    <p>
        Condiciones&nbsp;
        <asp:TextBox ID="txtCondiciones" runat="server" placeholder="Condiciones" onkeydown = "return(!(event.keyCode>=91));"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnAlta" runat="server" Text="Alta" OnClick="btnAlta_Click" />
&nbsp;
        <asp:Button ID="btnBaja" runat="server" Text="Baja" OnClick="btnBaja_Click" />
&nbsp;
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
&nbsp;
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    </p>
    <p>
        <asp:ListBox ID="lstDeposito" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstDeposito_SelectedIndexChanged" Height="221px" Width="780px"></asp:ListBox>
    </p>
    <p>
    &nbsp;<asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </p>
</asp:Content>
