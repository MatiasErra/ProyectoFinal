<%@ Page Title="Listado y eliminacion de camioneros" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmListarCamioneros.aspx.cs" Inherits="Web.Paginas.Camioneros.frmListarCamioneros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


 


    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">


                  
                    <div class="col-12">

                        <h5>Listar o eliminar un Camionero</h5>
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                    </div>
                    <div class="col-12">

                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar"  OnClick="btnBuscar_Click" />

                    </div>
                    <div class="col-12 align-self-center">

                        <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Camionero?')" OnClick="btnBaja_Click" />
                    </div>
                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:ListBox ID="lstCamionero" runat="server" AutoPostBack="true" CssClass="w-auto h-auto" OnSelectedIndexChanged="lstCamionero_SelectedIndexChanged"></asp:ListBox>
            </div>
        </div>
    </div>
    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>
