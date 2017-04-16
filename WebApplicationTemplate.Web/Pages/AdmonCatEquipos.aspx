<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="AdmonCatEquipos.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <h2>Catálogo de Equipos</h2>
            </div>
        </div>
        <div class="alert alert-danger" runat="server" id="lblError" visible="false"></div>
        <asp:GridView runat="server" ID="grdEquipos" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCancelingEdit="grdEquipos_RowCancelingEdit" OnRowDeleting="grdEquipos_RowDeleting" OnRowEditing="grdEquipos_RowEditing" OnRowUpdating="grdEquipos_RowUpdating" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="IdTipoEquipo" HeaderText="Id" ReadOnly="true" ItemStyle-Width="10%"/>
                <asp:BoundField DataField="IdCategoria" HeaderText="Categoría" ItemStyle-Width="20%" />
                <asp:BoundField DataField="CantidadParticipantes" HeaderText="Cant. Participantes" ItemStyle-Width="20%"/>
                <asp:BoundField DataField="Precio" HeaderText="Precio" ItemStyle-Width="20%"/>
                <asp:CommandField ShowEditButton="true" ItemStyle-Width="20%"/>
                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="10%"/>
            </Columns>
        </asp:GridView>
        <div class="form-group">
            <div class="col-md-12">
                <asp:Button runat="server" ID="btnAddNewRow" CssClass="btn btn-default" Text="Agregar nuevo" OnClick="btnAddNewRow_Click" />
            </div>
        </div>
    </div>
</asp:Content>
