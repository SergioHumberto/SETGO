<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="AdmonCatEquipos.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <h2>Catálogo de Equipos</h2>
            </div>
        </div>
        <div class="alert alert-danger" runat="server" id="lblError" visible="false"></div>
        <div class="text-right">
            <asp:LinkButton CssClass="" runat="server" ID="lnkShowInactive" OnClick="lnkShowInactive_Click"></asp:LinkButton>
        </div>
        <asp:GridView runat="server" ID="grdEquipos" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCancelingEdit="grdEquipos_RowCancelingEdit" OnRowDeleting="grdEquipos_RowDeleting" OnRowEditing="grdEquipos_RowEditing" OnRowUpdating="grdEquipos_RowUpdating" ShowHeaderWhenEmpty="true" OnRowDataBound="grdEquipos_RowDataBound" OnDataBinding="grdEquipos_DataBinding">
            <Columns>
                <asp:BoundField DataField="IdTipoEquipo" HeaderText="Id" ReadOnly="true" ItemStyle-Width="5%" ControlStyle-CssClass="form-control" />
                <asp:TemplateField HeaderText="Categoría" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnIdCategoria" runat="server" Value='<%#Bind("IdCategoria") %>' />
                        <asp:Label ID="lblCategoria" runat="server" Text='<%#Bind("IdCategoria") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdnIdCategoria" runat="server" Value='<%#Bind("IdCategoria") %>' />
                        <asp:DropDownList ID="ddlCategoria" runat="server" class="form-control"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CantidadParticipantes" HeaderText="Cant. Participantes" ItemStyle-Width="20%" ControlStyle-CssClass="form-control" />
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Precio">
                    <ItemTemplate>
                        <div class="input-group">
                            <span>$&nbsp;</span>
                            <asp:Label ID="lblPrecio" runat="server" Text='<%#Bind("Precio") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <asp:TextBox ID="txtPrecio" runat="server" Text='<%#Bind("Precio") %>' class="form-control"></asp:TextBox>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Activo">
                    <ItemTemplate>
                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Enabled="false" Checked='<%#Bind("Activo") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Checked='<%#Bind("Activo") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" ItemStyle-Width="20%" />
                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="10%" />
            </Columns>
        </asp:GridView>
        <div class="form-group">
            <div class="col-md-12">
                <asp:Button runat="server" ID="btnAddNewRow" CssClass="btn btn-default" Text="Agregar nuevo" OnClick="btnAddNewRow_Click" />
            </div>
        </div>
    </div>
</asp:Content>
