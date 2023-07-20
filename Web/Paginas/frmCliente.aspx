<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCliente.aspx.cs" Inherits="Web.Paginas.frmCliente" %>
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
        <asp:Calendar ID="Calendar1" runat="server" SelectedDate= "2023-07-14" ></asp:Calendar>
    <p>
        &nbsp;</p>
        <p>
        Usuario&nbsp;
        <asp:TextBox ID="txtUser" runat="server" placeholder="Nombre de Usuario" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
    </p>
    <p>
        Contraseña&nbsp;
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Contraseña" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"  ></asp:TextBox>
    </p>
    <p>
        Direccion
           <asp:TextBox ID="txtDirr" runat="server" placeholder="Direcciones" onkeydown = "return(!(event.keyCode>=91) && event.keyCode!=32);"  ></asp:TextBox>


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

        <asp:ListBox ID="lstCli" runat="server" OnInit="lstCli_Init" AutoPostBack="true" OnSelectedIndexChanged="lstCli_SelectedIndexChanged" Width="640px" Height="209px"></asp:ListBox>

     
    </p>
    <p>

    &nbsp;<asp:Label ID="lblMensajes" runat="server"></asp:Label>
    </p>




</asp:Content>
