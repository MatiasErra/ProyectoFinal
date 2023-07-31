<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmFertilizante.aspx.cs" Inherits="Web.Paginas.Fertilizantes.frmFertilizante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h5>ABM Fertilizante  </h5>
                        <asp:TextBox CssClass="form-control mt-1 mb-1 w-25 m-auto" ID="txtBuscar" runat="server" placeholder="Buscar" onkeydown="return(!(event.keyCode>=91);"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button CssClass="btn btn-outline-dark m-1 w-25" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12 align-self-center">
                        <asp:Button ID="btnBaja" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Fertilizante?')" OnClick="btnBaja_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Button ID="btnLimpiar" CssClass="btn btn-outline-dark w-25 m-1 align-self-center" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />

                    </div>

                    <div class="col-12">
                        <button type="button" class="btn btn-outline-dark m-1 w-25" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Modificar/Añadir Fertilizante
                        </button>

                        <br />

  <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                    </div>

                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Admin</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="col-12">
                                        Nombre
                        <asp:TextBox ID="txtNombre" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="col-12">
                                        Tipo
                        <asp:TextBox ID="txtTipo" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server" placeholder="Tipo" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                    </div>
                                    <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                        ControlToValidate="txtTipo"
                                        ValidationExpression="^[a-zA-Z ]*$"
                                        ErrorMessage="No es una letra valida" />

                               
                                <div class="col-12">
                                    Composición química 
                        <asp:TextBox ID="txtQuimica" CssClass="form-control mt-1 mb-1 w-75 m-auto" runat="server"  placeholder="Composición química"  onkeydown="return(event.keyCode!=32);"></asp:TextBox>

                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtQuimica"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />


                                    </div>
                                    <div class="col-12">
                                        PH
                        <asp:TextBox ID="txtPH" CssClass="form-control mt-1 mb-1 w-75 m-auto" MaxLength="2" runat="server" placeholder="PH" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>



                                    </div>
                                    <div class="col-12 mt-1 mb-1">
                                        lstImpacto
                                        <asp:DropDownList ID="lstImpacto" runat="server" CssClass="form-control mt-1 mb-1 w-75 m-auto"></asp:DropDownList>
                                    </div>



                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                    <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Fertilizante?')" OnClick="btnModificar_Click" />
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                                </div>
                            </div>
                      
                          
                    </div>






                   
                </div>
                <div class="col-12 mx-auto">
                    <asp:ListBox ID="lstFert" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="lstFert_SelectedIndexChanged" CssClass="w-75 h-auto" ></asp:ListBox>
                </div>
            </div>
        </div>

    </div>
        
    </div>
    <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>


















</asp:Content>
