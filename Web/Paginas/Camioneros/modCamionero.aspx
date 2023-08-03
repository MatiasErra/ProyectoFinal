<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modCamionero.aspx.cs" Inherits="Web.Paginas.Camioneros.modCamionero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
          <h2 class="title">Modificar Camionero</h2>
        </div>
        <div class="card-body">

            <div class="input-group">
                <asp:TextBox ID="txtId" ReadOnly="false" CssClass="input--style-2" placeholder="" runat="server" Enabled="False"></asp:TextBox>
            </div>

            <div class="input-group">
                                    
                                        <asp:TextBox ID="txtNombre" CssClass="input--style-2" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="input-group">
                                        
                                        <asp:TextBox ID="txtApell" CssClass="input--style-2" runat="server" placeholder="Apellido" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtApell"
                                            ValidationExpression="^[a-zA-Z ]*$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>

                                    <div class="input-group">
                                     
                                        <asp:TextBox ID="txtEmail" CssClass="input--style-2" runat="server" placeholder="Email"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtEmail"
                                            ValidationExpression="^\S+@\S+$"
                                            ErrorMessage="No es un Email valido" />
                                    </div>

                                    <div class="input-group">
                                        
                                        <asp:TextBox ID="txtTel" CssClass="input--style-2" MaxLength="9" runat="server" placeholder="Telefono" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtTel" ID="RegularExpressionValidator2"
                                            ValidationExpression="^[\s\S]{9,}$" runat="server" ErrorMessage="Debe ser un numero de 9 caracteres." />
                                    </div>

                                   
                                    <div class="input-group">
                                     
                                        <asp:TextBox ID="txtCedula" CssClass="input--style-2" MaxLength="8" runat="server" placeholder="Cedula" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtCedula" ID="RegularExpressionValidator1"
                                            ValidationExpression="^[\s\S]{8,}$" runat="server" ErrorMessage="La cedula debe tener 8 caracteres." />
                                    </div>

                                    <div class="input-group">
                                      
                                        <asp:DropDownList ID="lstDisponible" CssClass="input--style-2" runat="server">
                                        </asp:DropDownList>
                                    </div>


                                      <div class="input-group">
                                        Fecha de nacimiento<br />
                                        <asp:TextBox ID="txtFchNac" runat="server" CssClass="input--style-2 js-datepicker px-0 py-2" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                    </div>



                                    <div class="input-group">
                                        Fecha de vencimiento carnet de manejo<br />
                                        <asp:TextBox ID="txtFchManejo" runat="server" CssClass="input--style-2 js-datepicker px-0 py-2" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                    </div>


                <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>

            <div class="col-12">
                <asp:Button ID="btnModificar" class="btn btn-primary" runat="server" Text="Modificar" OnClientClick="return confirm('¿Desea modificar este Camionero?')" OnClick="btnModificar_Click" />
               <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>
                                </div>
  


                    </div>
    

</asp:Content>
