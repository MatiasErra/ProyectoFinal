﻿<%@ Page Title="Ingreso y modificacion de camioneros" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmAltaCamionero.aspx.cs" Inherits="Web.Paginas.Camioneros.frmAltaCamionero" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                          <h5> Agregar o modificar un Camionero </h5>

                        Nombre
                        <asp:TextBox ID="txtNombre" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Nombre" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Apellido
                        <asp:TextBox ID="txtApell" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Apellido" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Email
                        <asp:TextBox ID="txtEmail" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Email" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        Telefono
                        <asp:TextBox ID="txtTel" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Telefono" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12 mt-1 mb-1">
                        Fecha de nacimiento
                        <asp:Calendar ID="Calendar1" CssClass="m-auto" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
                    </div>
                    <div class="col-12">
                        Cedula
                        <asp:TextBox ID="txtCedula" CssClass="form-control mt-1 mb-1 w-25 m-auto" runat="server" placeholder="Cedula" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12 mt-1 mb-1">
                        Disponible
                        <asp:DropDownList ID="lstDisponible" CssClass="nav-link dropdown-toggle text-dark m-auto" runat="server" AutoPostBack="true" Width="300">
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 mt-1 mb-1">
                        Fecha de vencimiento carnet de manejo
                        <asp:Calendar ID="CalendarManejo" CssClass="m-auto" runat="server" SelectedDate="2023-07-14" PrevMonthText="&amp;lt; "></asp:Calendar>
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnAlta" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnModificar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Camionero?')" OnClick="btnModificar_Click"/>
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
                    <asp:ListBox ID="lstCamionero" runat="server" CssClass="w-auto h-auto m-auto" AutoPostBack="true" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged"></asp:ListBox>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtId" runat="server" Visible="false" Enabled="False"></asp:TextBox>
</asp:Content>