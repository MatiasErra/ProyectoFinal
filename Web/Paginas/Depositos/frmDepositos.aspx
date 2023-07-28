<%@ Page Title="Gestion de depositos" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmDepositos.aspx.cs" Inherits="Web.Paginas.Depositos.frmDepositos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h5>ABM Deposito </h5>
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12 align-self-center">
                        <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Deposito?')" OnClick="btnBaja_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />

                    </div>

                    <div class="col-12">
                        <button type="button" class="btn btn-outline-dark m-1 w-25" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Modificar/Añadir Deposito
                        </button>

                    </div>

                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Deposito</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="col-12">
                                        Capacidad
                                        <asp:TextBox ID="txtCapacidad" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Capacidad en toneladas" MaxLength="20" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCapacidad"
                                            ValidationExpression="^[0-9]*$"
                                            ErrorMessage="No es un caracter valido" />
                                    </div>

                                    <div class="col-12">
                                        Ubicacion
                                        <asp:TextBox ID="txtUbicacion" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Ubicacion" MaxLength="50" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtUbicacion"
                                            ValidationExpression="^[a-zA-Z0-9 ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="col-12">
                                        Temperatura
                                        <asp:TextBox ID="txtTemperatura" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" MaxLength="3" placeholder="Temperatura en °C" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtTemperatura"
                                            ValidationExpression="^[0-9]*$"
                                            ErrorMessage="No es un numero valido" />
                                    </div>

                                    <div class="col-12">
                                        Condiciones
                                        <asp:TextBox ID="txtCondiciones" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Condiciones" MaxLength="50" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCondiciones"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                    <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Deposito?')" OnClick="btnModificar_Click" />
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12" >
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                  
                    </div>
                                        <div class="col-12" >
                    
                        <asp:ListBox ID="lstDeposito" runat="server"  CssClass="w-50 h-auto" AutoPostBack="true" OnSelectedIndexChanged="lstDeposito_SelectedIndexChanged"></asp:ListBox>
                    </div> 

             
                </div>
                
            </div>
        </div>

    </div>

    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>