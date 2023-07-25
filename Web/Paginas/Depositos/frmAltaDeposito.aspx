<%@ Page Title="Ingreso y modificacion de depositos" MasterPageFile="~/Master/AGlobal.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmAltaDeposito.aspx.cs" Inherits="Web.Paginas.Depositos.frmAltaDeposito" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                     <h5>Agregar o modificar un Camionero</h5>  

                        Capacidad
                        <asp:TextBox ID="txtCapacidad" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Capacidad" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Ubicacion
                        <asp:TextBox ID="txtUbicacion" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Ubicacion" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Temperatura
                        <asp:TextBox ID="txtTemperatura" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Temperatura" onkeydown="return(!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Condiciones
                        <asp:TextBox ID="txtCondiciones" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Condiciones" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnAlta" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnModificar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Modificar"  OnClientClick="return confirm('¿Desea modificar este Deposito?')" OnClick="btnModificar_Click" />
                    </div>
                    <div class="col-12">
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                    </div>
                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>
                    <asp:ListBox ID="lstDeposito" runat="server" CssClass="w-auto h-auto m-auto" AutoPostBack="true" OnSelectedIndexChanged="lstDeposito_SelectedIndexChanged"></asp:ListBox>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtId" runat="server" Visible="false" Enabled="False"></asp:TextBox>
</asp:Content>
