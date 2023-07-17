<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCamionero.aspx.cs" Inherits="Web.Paginas.frmCamionero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        Id&nbsp;
        <asp:TextBox ID="txtId" runat="server" Enabled="False"></asp:TextBox>
    </p>
    <p>
        Nombre&nbsp;
        <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"    ></asp:TextBox>
    </p>
    <p>
        Apellido&nbsp;
        <asp:TextBox ID="txtApell" runat="server" placeholder="Apellido" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
    </p>
    <p>
        Email&nbsp;
        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
    </p>
    <p>
        Telefono&nbsp;
        <asp:TextBox ID="txtTel" runat="server" placeholder="Telefono" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
    </p>
    <p>
        Fecha de nacimiento&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        <asp:Calendar ID="Calendar1" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
    </p>
    <p>
        Cedula:&nbsp;
        <asp:TextBox ID="txtCedula" runat="server" placeholder="Cedula" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
    </p>
        <p>
            Disponible&nbsp;


        <asp:DropDownList ID="lstDisponible" runat="server" AutoPostBack="true" Width="300">
        </asp:DropDownList>


    </p>
        <p>
            Fecha de vencimiento carnet de manejo</p>
        <asp:Calendar ID="CalendarManejo" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
        <p>
            &nbsp;</p>
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
        <asp:ListBox ID="lstCamionero" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged" Height="221px" Width="780px"></asp:ListBox>
    </p>
    <p>
    &nbsp;<asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </p>
</asp:Content>

