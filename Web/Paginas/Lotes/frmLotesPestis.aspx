<%@ Page Language="C#" Title="Gestion de Pesticidas en Lotes" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmLotesPestis.aspx.cs" Inherits="Web.Paginas.Lotes.frmLotesPestis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="card mt-1 mb-1 w-75 m-auto  text-center">
        <div class="card-header" style="background-color: white">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Gestionar Pesticidas en Lotes</h1>
        </div>
        <div class="card-body">



            <div class="row">

                <div class="col-4">
                    <p cssclass="centerText input--style-2">Id Granja</p>

                </div>

                <div class="col-4">
                    <p cssclass="centerText input--style-2">Id Producto</p>

                </div>

                <div class="col-4">
                    <p cssclass="centerText input--style-2">Fecha de producción</p>

                </div>

            </div>


            <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5">

                <div class="col-4">
                    <asp:Label ID="txtGranja" CssClass="centerText input--style-2" runat="server"></asp:Label>

                </div>
                <div class=" col-4">
                    <asp:Label ID="txtProducto" CssClass="centerText input--style-2" runat="server"></asp:Label>

                </div>
                <div class=" col-4">
                    <asp:Label ID="txtFechProd" CssClass="centerText input--style-2" runat="server"></asp:Label>

                </div>


            </div>



            <div class="row">
                <div class="col-9">
                    <asp:TextBox CssClass="form-control mt-2 mb-2" ID="txtBuscarPesticida" runat="server" placeholder="Buscar pesticida" MaxLength="100" onkeydown="return(!(event.keyCode>=91));"></asp:TextBox>

                </div>
                <div class="col-3">
                    <asp:Button CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" ID="btnBuscarPesticida" runat="server" Text="Buscar" OnClick="btnBuscarPesticida_Click" />
                </div>
            </div>
            <div class="row">

                <div class="col-9">
                    <asp:DropDownList ID="listPesticida" CssClass="input--style-2" runat="server">
                    </asp:DropDownList>
                </div>


                <div class="col-3">
                    <asp:Button ID="btnModificarCantidadPestiLote" CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" Visible="false" runat="server" Text="Modificar cantidad" OnClick="btnModificarCantidadPestiLote_Click" />
                    <asp:Button ID="btnAltaPesticida" class="btnE btn--radius btn--blue float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir Pesticida" OnClick="btnAltaPesticida_Click" />
                </div>
            </div>
            <div class="row" style="margin-bottom: 32px; border-bottom: 1px solid #e5e5e5">
                <div class=" col-9">
                    <asp:TextBox ID="txtCantidadPesti" CssClass="input--style-2" runat="server" placeholder="Cantidad de pesticida" MaxLength="10" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"></asp:TextBox>
                </div>
                <div class="col-3">
                    <asp:Button ID="btnCancelar" CssClass="btnE btn--radius btn--red float-end mt-2 mb-2 align-self-center btn--srch" Visible="false" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnSelect" CssClass="btnE btn--radius btn--green float-end mt-2 mb-2 align-self-center btn--srch" runat="server" Text="Añadir al Lote" OnClick="btnSelectPesticida_Click" />
                </div>
            </div>



            <div class="col-md-12 align-self-center">
                <div class="row align-self-center">
                    <div class="col-md-10 col-md-offset-1">
                        <div class="form-group">
                            <div class="table-responsive">
                                <asp:GridView ID="lstLotPestiSel" Width="100%" SelectedIndex="1" AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-condensed table-responsive table-hover"
                                    runat="server">
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Large" ForeColor="White" />
                                    <RowStyle BackColor="#f5f5f5" />
                                    <Columns>

                                        <asp:BoundField DataField="IdPesticida"
                                            HeaderText="Id del Pesticida"
                                            ItemStyle-CssClass="GridStl" />

                                        <asp:BoundField DataField="Nombre"
                                            HeaderText="Nombre"
                                            ItemStyle-CssClass="GridStl" />

                                        <asp:BoundField DataField="Tipo"
                                            HeaderText="Tipo"
                                            ItemStyle-CssClass="GridStl" />

                                        <asp:BoundField DataField="Cantidad"
                                            HeaderText="Cantidad"
                                            ItemStyle-CssClass="GridStl" />



                                        <asp:TemplateField HeaderText="Opciones del administrador"
                                            ItemStyle-CssClass="GridStl">
                                            <ItemTemplate>
                                                <asp:Button ID="btnBajaPesti" CssClass="btnE btn--radius btn--red" runat="server" Text="Eliminar" OnClientClick="return confirm('¿Desea eliminar este Pesticida del Lote?')" OnClick="btnBajaPesti_Click" />
                                                <asp:Button ID="btnModificarCantidad" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar cantidad" OnClick="btnModificarCantidad_Click" />
                                                <asp:Button ID="btnModificarPesti" CssClass="btnE btn--radius btn--yellow" runat="server" Text="Modificar pesticida" OnClick="btnModificarPesti_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

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
                <asp:Button ID="btnAtrasFrm" CssClass="btnE btn--radius btn--gray mt-1 mb-1" Visible="false" runat="server" Text="Volver" OnClick="btnAtrasFrm_Click" />
                <asp:Button ID="btnAtrasMod" CssClass="btnE btn--radius btn--gray mt-1 mb-1" Visible="false" runat="server" Text="Volver" OnClick="btnAtrasMod_Click" />
            </div>

            <asp:TextBox ID="txtIdFertilizante" Enabled="false" Visible="false" CssClass="input--style-2" runat="server"></asp:TextBox>
        </div>
    </div>
</asp:Content>
