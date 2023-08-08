<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmLotes_Fert.aspx.cs" Inherits="Web.Paginas.Lotes.frmLotes_Fert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--        <div class="card mt-1 mb-1 w-50 m-auto  text-center">
        <div class="card-header" style="background-color: white">
          <h2 class="title">Modificar Administradores</h2>
        </div>
        <div class="card-body">


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
                                        <asp:Button ID="btnSelect" Cssclass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Seleccionar"  />
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
                <asp:Button ID="btnModificar" CssClass="btnE btn--radius btn--green mt-1 mb-1" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="return confirm('¿Desea modificar este Administrador?')"/>
               <asp:Button ID="btnAtras" CssClass="btnE btn--radius btn--gray mt-1 mb-1" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>
    
                          </div>
            </div>--%>

</asp:Content>
