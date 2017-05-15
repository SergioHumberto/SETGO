<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="BuscaCarrera.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.BuscaCarrera" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <div class="form-horizontal">
        <div class="form-group">
            <h2 class="col-md-12">Carreras</h2>
        </div>
        <div class="col-md-12 alert alert-danger" runat="server" id="lblError" visible="false"></div>
        <div class="form-group">
            <div class="col-md-12 text-right">
                <asp:LinkButton runat="server" ID="lnkLimpiar" CssClass="btn-link" OnClick="lnkLimpiar_Click" Text="Limpiar Filtros"></asp:LinkButton>
            </div>
            <label class="col-md-1 control-label" for="txtNombreCarrera">Nombre</label>
            <div class="col-md-11">
                <asp:TextBox CssClass="form-control" runat="server" ID="txtNombreCarrera"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-1 control-label" for="txtDesde">Desde</label>
            <div class="col-md-3">
                <div class="input-group">
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtDesde"></asp:TextBox>
                    <span class="input-group-addon btn btn-default glyphicon glyphicon-calendar" id="imgCalDesde"></span>
                </div>
                <aspx:CalendarExtender runat="server" TargetControlID="txtDesde" PopupButtonID="imgCalDesde" />
                <aspx:MaskedEditExtender ID="mskCalDesde" runat="server" TargetControlID="txtDesde" MaskType="Date" Mask="99/99/9999" />
            </div>
            <label class="col-md-1 control-label" for="txtHasta">Hasta</label>
            <div class="col-md-3">
                <div class="input-group">
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtHasta"></asp:TextBox>
                    <span class="input-group-addon btn btn-default glyphicon glyphicon-calendar" id="imgCalHasta"></span>
                </div>
                <aspx:CalendarExtender runat="server" TargetControlID="txtHasta" PopupButtonID="imgCalHasta" />
                <aspx:MaskedEditExtender ID="mskCalHasta" runat="server" TargetControlID="txtHasta" MaskType="Date" Mask="99/99/9999" />
            </div>
            <div class="col-md-3">
                <asp:Button runat="server" CssClass="btn btn-default" Text="Filtrar resultados" ID="btnFiltrar" OnClick="btnFiltrar_Click" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12 text-right">
                <asp:LinkButton runat="server" ID="lnkShowInactive" CssClass="btn-link" OnClick="lnkShowInactive_Click"></asp:LinkButton>
            </div>
            <div class="col-md-12">
                <asp:GridView runat="server" ID="grdCarreras" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" OnRowCommand="grdCarreras_RowCommand" OnRowDataBound="grdCarreras_RowDataBound" OnSelectedIndexChanged="grdCarreras_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCarrera" runat="server" Text='<%#Bind("IdCarrera") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="50%" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MMM/yy}" ItemStyle-Width="16%" />
                        <asp:TemplateField ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnActivarDesactivar" runat="server" CssClass="btn-link" CommandName="ActivarDesactivar"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Link" ShowSelectButton="true" ControlStyle-CssClass="btn-link" SelectText="Editar" ItemStyle-Width="12%" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-offset-5">
                <asp:Button runat="server" ID="btnNuevaCarrera" CssClass="btn btn-default" Text="Nueva Carrera" OnClick="btnNuevaCarrera_Click" />
            </div>
        </div>
    </div>
</asp:Content>
