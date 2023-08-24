<%@ Page Language="C#" Title="Gestion de granjas" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmGranjas.aspx.cs" Inherits="Web.Paginas.Granjass.frmGranjas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center backforContent">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">ABM Granjas </h2>
                    </div>
                    <div class="col-12">
                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0" ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100"  onkeydown="return(( event.keyCode<91 &&  event.keyCode>64  ) || (event.keyCode>=48) && (event.keyCode<=57)   || event.keyCode==32 || event.keyCode==8);"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>
                      <div class="col-12 ">
                         <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtBuscar"
                            ValidationExpression="^[a-zA-Z0-9 ]+$"
                            ErrorMessage="No es un carácter válido" />
                           </div>


                    <div class="col-12">

                        <asp:Button ID="btnVolver" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Visible="false" Text="Volver" OnClick="btnVolver_Click" />
                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />



                        <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Añadir Granja
                        </button>

                    </div>


                    <!-- Modal Nuevo granja -->
                    
                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nueva Granja</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <div class="input-group">
                                        <asp:TextBox ID="txtNombre" CssClass="input--style-tex" runat="server" placeholder="Nombre" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-Z ]+$"
                                            ErrorMessage="No es un caracter valido" />
                                    </div>

                                    <div class="input-group">
                                        <asp:TextBox ID="txtUbicacion" CssClass="input--style-tex" runat="server" placeholder="Ubicacion" MaxLength="40" onkeydown="return(!(event.keyCode>=91) || (event.keyCode!=32));"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                                            ControlToValidate="txtUbicacion"
                                            ValidationExpression="^[a-zA-Z0-9 ]+$"
                                            ErrorMessage="No es una letra valida" />
                                    </div>


                                    <div class="row">
                                        <div class="col-xl-7 col-lg-12">
                                           <asp:DropDownList ID="listDueño" CssClass="input--style-lst" runat="server">
                                        </asp:DropDownList>

                                        </div>
                                        <div class="col-xl-5 col-lg-12">
                                            <asp:Button CssClass="btnE btn--radius btn--green  align-self-center btn--srch" ID="btnBuscarDueño" runat="server" Text="Buscar Dueño" OnClick="btnBuscarDueño_Click" />
                                        </div>
                                    </div>


                              




                                    <div class="modal-footer">
                                        <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />
                                      
                                        <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                        <div class="col-12 my-2">
                    <asp:Label CssClass="text centerText " ID="lblMensajes" runat="server"></asp:Label>

                      <asp:RegularExpressionValidator Display="Dynamic" runat="server" class="text initText"
                            ControlToValidate="txtBuscar"
                            ValidationExpression="^[a-zA-Z0-9 ]+$"
                            ErrorMessage="No es un carácter válido" />

                </div>

                </div>
                <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstGranja" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdGranja"
                                                HeaderText="Id de Granja"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Ubicacion"
                                                HeaderText="Ubicacion"
                                                ItemStyle-CssClass="GridStl" />
                                           
                                            <asp:BoundField DataField="NomDue"
                                                HeaderText="Nombre del dueño"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField
                                                HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>


                                                    
                                                    
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar esta Granja?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>




                                    <asp:GridView ID="lstGranjaSelect" Width="100%" SelectedIndex="1" AutoGenerateColumns="false" Visible ="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdGranja"
                                                HeaderText="Id de Granja"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Ubicacion"
                                                HeaderText="Ubicacion"
                                                ItemStyle-CssClass="GridStl" />
                                           
                                            <asp:BoundField DataField="NomDue"
                                                HeaderText="Nombre del dueño"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:TemplateField
                                                HeaderText="Opciones del administrador"
                                                ItemStyle-CssClass="GridStl">
                                                <ItemTemplate>


                                                    
                                                     <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--blue" runat="server" Text="Seleccionar"  OnClick="btnSelected_Click" />
                                                    <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar esta Granja?')" OnClick="btnBaja_Click" />
                                                    <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar" OnClick="btnModificar_Click" />

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


