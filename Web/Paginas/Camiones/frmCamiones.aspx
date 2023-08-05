<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmCamiones.aspx.cs" Inherits="Web.Paginas.Camiones.frmCamiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container row m-2 text-center">
        <div class="row justify-content-center">
            <div class="col-12 m-3 p-2 text-center" style="border-radius: 20px; background-color: #f2f0f0">
                <div class="row">
                    <div class="col-12">
                        <h2 class="title">ABM Camiones </h2>
                    </div>

                    <div class="col-12">


                        <asp:TextBox CssClass="d-inline form-control  w-75 m-2 border-0  " ID="txtBuscar" runat="server" placeholder="Buscar" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>
                        <asp:Button CssClass="btnE btn--radius btn--green align-self-center btn--srch" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </div>

                    <div class="col-12">

                        <asp:Button ID="btnLimpiar" Class="btnE btn--radius btn--blue align-self-center btn--lst" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />



                        <button type="button" class="btnE btn--radius btn--blue align-self-center btn--lst" data-bs-toggle="modal" data-bs-target="#altaModal">
                            Añadir Camiones
                        </button>

                    </div>

                    <div class="modal fade" id="altaModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog modal-none">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5" id="exampleModalLabel">Nuevo Deposito</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">


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

                                </div>



                                <div class="modal-footer">
                                    <asp:Button ID="btnAlta" class="btnE btn--radius btn--green" runat="server" Text="Alta" OnClick="btnAlta_Click" />

                                    <button type="button" class="btnE btn--radius btn--gray" data-bs-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMensajes" runat="server"></asp:Label>

                    </div>

                    <div class="col-md-12 align-self-center">



                        <div class="row align-self-center">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView ID="lstCamiones" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            runat="server">
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                            <RowStyle BackColor="#f5f5f5" />
                                            <Columns>

                                                <asp:BoundField DataField="IdCamion"
                                                    HeaderText="Id de Camion"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Marca"
                                                    HeaderText="Marca"
                                                    ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Modelo"
                                                    HeaderText="Modelo" ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Carga"
                                                    HeaderText="Carga" ItemStyle-CssClass="GridStl" />

                                                <asp:BoundField DataField="Disponible"
                                                    HeaderText="Disponible" ItemStyle-CssClass="GridStl" />


                                                <asp:TemplateField HeaderText="Opciones del administrador"
                                                    ItemStyle-CssClass="GridStl">
                                                    <ItemTemplate>




                                                        <asp:Button ID="btnBaja" CssClass="btnE btn--radius btn--red" runat="server" Text="Baja" OnClientClick="return confirm('¿Desea eliminar este Camión?')" OnClick="btnBaja_Click" />
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

    </div>


</asp:Content>
