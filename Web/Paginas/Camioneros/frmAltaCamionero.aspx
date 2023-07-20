<%@ Page Title="Ingreso de camioneros" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmAltaCamionero.aspx.cs" Inherits="Web.Paginas.Camioneros.frmAltaCamionero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="CamList">
        <div>
            Id&nbsp;
            <asp:TextBox ID="txtId" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div>
            Nombre&nbsp;
            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Nombre" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
        </div>
        <div>
            Apellido&nbsp;
            <asp:TextBox ID="txtApell" CssClass="form-control" runat="server" placeholder="Apellido" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
        </div>
        <div>
            Email&nbsp;
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Email" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
        </div>
        <div>
            Telefono&nbsp;
            <asp:TextBox ID="txtTel" CssClass="form-control" runat="server" placeholder="Telefono" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
        </div>
        <div>
            Fecha de nacimiento&nbsp;&nbsp;&nbsp;
        </div>
        <div>
            <asp:Calendar ID="Calendar1" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
        </div>
        <div>
            Cedula:&nbsp;
            <asp:TextBox ID="txtCedula" CssClass="form-control" runat="server" placeholder="Cedula" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
        </div>
        <div>
            Disponible&nbsp;
            <asp:DropDownList ID="lstDisponible" CssClass="nav-link dropdown-toggle text-dark" runat="server" AutoPostBack="true" Width="300">
            </asp:DropDownList>
        </div>
        <div>
            Fecha de vencimiento carnet de manejo
        </div>
            <asp:Calendar ID="CalendarManejo" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
        <div>
            <asp:Button ID="btnAlta" CssClass="btn btn-outline-dark w-100" runat="server" Text="Alta" OnClick="btnAlta_Click" />
            <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-100 mt-1" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
        </div>
        <div>
            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
        </div>
        <div>
            <asp:ListBox ID="lstCamionero" runat="server" Height="300px" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged"></asp:ListBox>
        </div>


    </div>

</asp:Content>
