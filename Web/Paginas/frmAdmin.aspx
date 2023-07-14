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
        <asp:Calendar ID="Calendar1" runat="server" SelectedDate= "2023-07-14" ></asp:Calendar>
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
        <asp:Button ID="btnBaja" runat="server" Text="Baja" OnClick="btnBaja_Click" />
&nbsp;
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" />
&nbsp;
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    &nbsp;<asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" OnClick="btnSeleccionar_Click" />
    </p>
    <p>
<<<<<<< HEAD
        <asp:ListBox ID="lstAdmin" runat="server" OnInit="lstAdmin_Init" OnSelectedIndexChanged="lstAdmin_SelectedIndexChanged"></asp:ListBox>
=======
        <asp:ListBox ID="lstAdmin" runat="server"></asp:ListBox>
    </p>
    <p>
>>>>>>> c26bc18d9d525507cb46910ad5c5d4457465ebf4
    &nbsp;<asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </p>
</asp:Content>
