<%@ Page Language="C#" Title="Modificar lote" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="modLote.aspx.cs" Inherits="Web.Paginas.Lotes.modLote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modificar Lote</h1>
        </div>
        <div class="card-body">
            <div class="input-group">
                <p class="mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                    Id granja
                </p>
                <asp:TextBox ID="txtIdGranja" Enabled="false" CssClass="input--style-2" runat="server"></asp:TextBox>
            </div>

            <div class="input-group">
                <p class="mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                    Id producto
                </p>
                <asp:TextBox ID="txtIdProducto" Enabled="false" CssClass="input--style-2" runat="server"></asp:TextBox>
            </div>

            <div class="input-group">
                <p class="mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                    Fecha de producción
                </p>

                <asp:TextBox ID="txtFchProduccion" Enabled="false" runat="server" CssClass="input--style-2 js-datepicker px-0 py-2" TextMode="Date"></asp:TextBox>
            </div>

            <div class="input-group">
                <p class="mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                    Cantidad
                </p>
                <asp:TextBox ID="txtCantidad" CssClass="input--style-2" runat="server" placeholder="Cantidad" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==8);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtCantidad"
                    ValidationExpression="^[0-9]+$"
                    ErrorMessage="No es un caracter valido" />
            </div>

            <div class="input-group">
                <p class="mt-2" style="color: #666; font-size: 16px; font-weight: 500;">
                    Precio
                </p>
                <asp:TextBox ID="txtPrecio" CssClass="input--style-2" runat="server" placeholder="Stock" MaxLength="10" onkeydown="return(((event.keyCode>=48) && (event.keyCode<=57)) || event.keyCode==188 || event.keyCode==8);"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" runat="server"
                    ControlToValidate="txtPrecio"
                    ValidationExpression="([0-9])[0-9]*[,]?[0-9]*"
                    ErrorMessage="Solo numeros">
                </asp:RegularExpressionValidator>
            </div>

            <div class="row">
                <div class="col-7">
                    <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarDeposito" runat="server" placeholder="Buscar deposito" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                </div>
                <div class="col-5">
                    <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarDeposito" runat="server" Text="Buscar" OnClick="btnBuscarDeposito_Click" />
                </div>
            </div>
            <div class="input-group">
                <asp:DropDownList ID="listDeposito" CssClass="input--style-2" runat="server">
                </asp:DropDownList>
            </div>

    <div class="row">
                                        <div class="col-7">
                                            <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarFertilizante" runat="server" placeholder="Buscar fertilizante" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                                        </div>
                                        <div class="col-5">
                                            <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarFertilizante" runat="server" Text="Buscar" OnClick="btnBuscarFertilizante_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                       
                                        <div class="col-8">
                                            <asp:DropDownList ID="listFertilizante" CssClass="input--style-2" runat="server">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="col-4">
                                            <asp:Button ID="btnAltaFertilizante" class="btnE btn--radius btn--blue float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir Fertilizante" OnClick="btnAltaFertilizante_Click" />
                                        </div>
                                              </div>


                                      <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5";>

                                           <div class=" col-7" >
                                        <asp:TextBox ID="txtCantidadFerti" CssClass="input--style-2" runat="server" placeholder="Cantidad de fertilizante" MaxLength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>

                                               </div>
                                               
                                               <div class= "col-5">
                                        <asp:Button ID="btnSelect" Cssclass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir"  OnClick="btnSelectFertilizante_Click" />
                                        </div>


                                          </div>



               <div class="col-md-12 align-self-center">
                    <div class="row align-self-center">
                        <div class="col-md-11 col-md-offset-1">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="lstFertSel" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        runat="server">
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Large" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                        <Columns>

                                            <asp:BoundField DataField="IdFertilizante"
                                                HeaderText="Id de Fertilizante"
                                                ItemStyle-CssClass="GridStl" />

                                            <asp:BoundField DataField="Nombre"
                                                HeaderText="Nombre"
                                                ItemStyle-CssClass="GridStl" />




                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>















            <div class="col-12">
                <asp:Label ID="lblMensajes" runat="server"></asp:Label>

            </div>
            <div class="col-12">
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este lote?')" />
                <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>

    <asp:TextBox ID="txtIdFertilizante" Enabled="false" Visible="false" CssClass="input--style-2" runat="server"></asp:TextBox>
        </div>
    </div>
</asp:Content>
