<%@ Page Title="Baja de camioneros" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmBajaCamionero.aspx.cs" Inherits="Web.Paginas.Camioneros.frmBajaCamionero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="CamList">
        <div class="d-flex">
            <asp:TextBox CssClass="form-control" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
            <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark ms-1" runat="server" Text="Baja" OnClick="btnBaja_Click" />
        </div>
        <div>
            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
        </div>
        <div>
            <asp:ListBox ID="lstCamionero" runat="server" Height="600px" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged"></asp:ListBox>
        </div>

    </div>

</asp:Content>
