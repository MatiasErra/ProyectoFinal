<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdmin.aspx.cs" Inherits="Web.Paginas.frmAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <p>
        Id&nbsp;
        <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
    </p>
    <p>
        Nombre&nbsp;
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </p>
    <p>
        Apellido&nbsp;
        <asp:TextBox ID="txtApell" runat="server"></asp:TextBox>
    </p>
    <p>
        Email&nbsp;
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    </p>
    <p>
        Telefono&nbsp;
        <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
    </p>
    <p>
        Fecha de nacimiento&nbsp;&nbsp;&nbsp;
    </p>
        <asp:Calendar ID="Calendar1" runat="server" SelectedDate="2023-07-11" PrevMonthText="&amp;lt; "></asp:Calendar>
    <p>
        &nbsp;</p>
        <p>
        Usuario&nbsp;
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
    </p>
    <p>
        Contraseña&nbsp;
        <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
    </p>
    <p>
        Tipo de Admin


        <asp:DropDownList ID="listTipoAdmin" runat="server" OnSelectedIndexChanged="listTipoAdmin_SelectedIndexChanged" AutoPostBack="true" Width="300">
        </asp:DropDownList>


    </p>
    <p>
        <asp:Button ID="btnAlta" runat="server" Text="Alta" OnClick="btnAlta_Click" />
&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Button" />
&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Button" />
&nbsp;
        <asp:Button ID="Button5" runat="server" Text="Button" />
    </p>
    <p>
        <asp:ListBox ID="lstAdmin" runat="server"></asp:ListBox>
    &nbsp;<asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </p>
</asp:Content>
