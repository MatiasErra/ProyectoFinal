<%@ Page Title="Gestion de admins" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/AGlobal.Master" CodeBehind="frmAdmins.aspx.cs" Inherits="Web.Paginas.Admins.frmListarAdmins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0;">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">ABM Administradores </h2>
                    </div>
                    <div class="col-12">
                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12">

                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />



                        <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Añadir Administrador
                        </button>

                    </div>


                    <!-- Modal Nuevo admin -->
                    <div class="modal fade" id="altaModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Admin</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

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

                                        <asp:TextBox ID="txtEmail" CssClass="input--style-2" runat="server" placeholder="Email" onkeydown="return(event.keyCode!=32);"></asp:TextBox>

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
                                      
                        <asp:TextBox ID="txtUser" runat="server" CssClass="input--style-2" placeholder="Nombre de Usuario" MaxLength="40" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"> </asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                                            ControlToValidate="txtUser"
                                            ValidationExpression="[a-zA-Z0-9]*$"
                                            ErrorMessage="No es una letra valida" />

                                    </div>
                                    <div class="input-group">
                                    
                        <asp:TextBox ID="txtPass" runat="server" CssClass="input--style-2" TextMode="Password" MaxLength="40" placeholder="Contraseña" onkeydown="return(!(event.keyCode>=91) && event.keyCode!=32);"></asp:TextBox>
                                    </div>
                                 
                                 

                               <div class="input-group">
                                   <asp:DropDownList ID="listTipoAdmin" CssClass="input--style-2" runat="server">
                                   </asp:DropDownList>
                               </div>

                                      <div class="input-group">
                                        Fecha de nacimiento<br />
                                        <asp:TextBox ID="txtFchNac" runat="server" CssClass="input--style-2 js-datepicker" placeholder="Telefono" TextMode="Date"></asp:TextBox>
                                    </div>



                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btn btn-primary" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>

                    </div>





                </div>

                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstAdmin" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdPersona"
                                                HeaderText="Id de Admin"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Apellido"
                                                HeaderText="Apellido" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Email"
                                                HeaderText="E-Mail" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Telefono"
                                                HeaderText="Telefono" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="FchNacimiento"
                                                HeaderText="Fecha de Nacimiento" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="User"
                                                HeaderText="Usuario" ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="TipoDeAdmin"
                                                HeaderText="Tipo de Admin" ItemStyle-CssClass="GridStl" />


                                            <asp:TemplateField HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>




                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Administrador?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btmModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>





            </div>
        </div>
    </div>
    

            <asp:TextBox Visible="false" CssClass="form-control m-1" ID="txtId" runat="server" Enabled="False"></asp:TextBox>
</asp:Content>
