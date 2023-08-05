<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modCamiones.aspx.cs" Inherits="Web.Paginas.Camiones.modCamiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
          <h2 class="title">Modificar Camión</h2>
        </div>
        <div class="card-body">

            <div class="input-group">
                <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-2" placeholder="" runat="server" Enabled="False"></asp:TextBox>
            </div>

                <div class="input-group">

                                        <asp:TextBox ID="txtMarca" CssClass="input--style-2" runat="server" placeholder="Marca" MaxLength="30" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtMarca"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtModelo" CssClass="input--style-2" runat="server" placeholder="Modelo" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtModelo"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es un letra valido" />
                                    </div>

                                    <div class="input-group">

                                        <asp:TextBox ID="txtCarga" CssClass="input--style-2" runat="server" placeholder="Carga" MaxLength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtCarga"
                                            ValidationExpression="^[0-9]*$"
                                            ErrorMessage="No es un numero valido" />
                                    </div>
                                    <div class="input-group">

                                        <asp:DropDownList ID="lstDisponible" CssClass="input--style-2" runat="server">
                                        </asp:DropDownList>
                                    </div>
            
                <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>

            <div class="col-12">
                <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Camión?')" OnClick="btnModificar_Click" />
               <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>

            </div>
        </div>
</asp:Content>
