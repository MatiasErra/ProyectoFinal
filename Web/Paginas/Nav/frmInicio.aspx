﻿<%@ Page Language="C#" Title="Inicio" MasterPageFile="~/Master/AGlobal.Master" AutoEventWireup="true" CodeBehind="frmInicio.aspx.cs" Inherits="Web.Paginas.frmInicio" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="carouselExampleInterval" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-inner rounded">
            <div class="carousel-item active" data-bs-interval="10000">
                <asp:Image ImageUrl="~/Imagenes/image1.png" CssClass="d-block w-100" runat="server" />
            </div>
            <div class="carousel-item" data-bs-interval="3000">
                <asp:LinkButton ID="LinkImagen" OnClick="LinkImagen_Click" runat="server">
                    <asp:Image ID="Imagen" ImageUrl="~/Imagenes/image2.png" CssClass="d-block w-100" runat="server" />
                </asp:LinkButton>
                
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleInterval" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleInterval" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-12 m-3 p-2 backforContent">
                <div class="row">
                    <div class="col-12 p-3">
                        <p class="fw-bold fs-2">Frutas y Verduras Frescas S.A.</p>
                        <p class="lh-base">Somos una distribuidora mayorista de productos agrícolas en <a href="https://goo.gl/maps/MyxWX4RbERWUTX4Z9" target="_blank">Montevideo, Uruguay</a>, que se dedica a proveer a comercios mayoristas, supermercados, restaurantes y hoteles en la ciudad y sus alrededores.</p>
                        <p class="lh-base">Frutas y Verduras Frescas S.A. es una empresa dedicada a la distribución y venta de frutas y verduras frescas desde el año 2005.</p>
                        <hr />
                    </div>

                    <div class="col-12 p-3">
                        <p class="fw-bold fs-2">¿Que ofrecemos?</p>
                        <p class="lh-base">
                            Frutas y Verduras Frescas S.A. permite a los comercios mayoristas, supermercados, restaurantes y hoteles la compra de frutas y verduras mediante un 
                                <a href="/Paginas/Catalogo/frmCatalogo">Catalogo</a>
                            en el cual pueden seleccionar los productos que deseen para luego realizar el pedido.
                        </p>
                        <p class="lh-base">
                            Al ser confirmado el pedido, este sera enviado a su ubicacion en un tiempo estimado el cual usted podra controlar en todo momento ademas de el estado de su paquete.
                        </p>
                        <hr />
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
