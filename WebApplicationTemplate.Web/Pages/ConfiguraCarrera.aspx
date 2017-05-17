<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="ConfiguraCarrera.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.ModificaCarrera" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnCopy").click(function () {
                var id = "#" + "<%= txtURL.ClientID %>";
                try {
                    $(id).select();
                    document.execCommand("copy");
                }
                catch (e) {
                    alert('Copy operation failed');
                }
            });
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <div class="form-horizontal" style="max-width: none !important; width: 880px;">
        <div class="form-group">
            <h2 class="col-md-12">Configura Carrera</h2>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Generales
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updGenerales">
                    <ContentTemplate>
                        <div class="col-md-12 alert alert-success" runat="server" id="lblSuccessGenerales" visible="false"></div>
                        <div class="col-md-12 alert alert-danger" runat="server" id="lblError" visible="false"></div>
                        <div class="form-group">
                            <label class="col-md-1 col-md-offset-10 col-xs-12" for="lblEstatus">Estatus:</label>
                            <asp:Label CssClass="col-md-1 col-xs-12" runat="server" ID="lblEstatus"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtURL">URL</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtURL" TextMode="Url" ReadOnly="true"></asp:TextBox>
                                <div class="text-right">
                                    <a class="btn btn-link" id="btnCopy">Copiar URL</a>
                                    <span>|</span>
                                    <asp:HyperLink runat="server" Target="_blank" ID="lnkVistaPrevia" CssClass="btn btn-link" Text="Vista Previa"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtNombreCarrera">Nombre</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtNombreCarrera"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqNombre" ControlToValidate="txtNombreCarrera" SetFocusOnError="true"
                                    CssClass="MessageError" runat="server" Display="Dynamic" ErrorMessage="Se requiere el nombre de la carrera" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtFecha">Fecha</label>
                            <div class="col-md-3">
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtFecha"></asp:TextBox>
                                    <span class="input-group-addon btn btn-default glyphicon glyphicon-calendar" id="imgCalDesde"></span>
                                </div>
                                <aspx:CalendarExtender runat="server" TargetControlID="txtFecha" PopupButtonID="imgCalDesde" />
                                <aspx:MaskedEditExtender ID="mskCalFecha" runat="server" TargetControlID="txtFecha" MaskType="Date" Mask="99/99/9999" />
                                <asp:RequiredFieldValidator ID="reqFecha" ControlToValidate="txtFecha" SetFocusOnError="true"
                                    CssClass="MessageError" runat="server" Display="Dynamic" ErrorMessage="Se requiere la fecha" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="ckeEncabezado">Encabezado</label>
                            <div class="col-md-10">
                                <CKEditor:CKEditorControl ID="ckeEncabezado" BasePath="~/Controls/CKEditor/" runat="server"></CKEditor:CKEditorControl>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtTerminosCondic">Términos y Condiciones</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtTerminosCondic" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtPaypalEmail">Paypal Email</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPaypalEmail"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rexPaypalEmail" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico PayPal inválido"
                                    ControlToValidate="txtPaypalEmail" Display="Dynamic" CssClass="MessageError" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtCC">CC</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtCC"></asp:TextBox>
                                <span class="help-block">Usa coma "," para separar cada correo electrónico, e.g. correo1@mail.com,correo2@mail.com</span>
                                <asp:RegularExpressionValidator ID="rexCC" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico para copia es inválido"
                                    ControlToValidate="txtCC" Display="Dynamic" CssClass="MessageError" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)(\,\s*([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+))*$" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtBCC">BCC</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtBCC"></asp:TextBox>
                                <span class="help-block">Usa coma "," para separar cada correo electrónico, e.g. correo1@mail.com,correo2@mail.com</span>
                                <asp:RegularExpressionValidator ID="rexBCC" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico para copia oculta es inválido"
                                    ControlToValidate="txtBCC" Display="Dynamic" CssClass="MessageError" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)(\,\s*([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+))*$" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtURLRegistro">URL Registro</label>
                            <div class="col-md-10">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtURLRegistro"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rexURLRegistro" runat="server" SetFocusOnError="true" ErrorMessage="La URL de registro es inválida"
                                    ControlToValidate="txtURLRegistro" Display="Dynamic" CssClass="MessageError"
                                    ValidationExpression="^(http|https)\://[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,3}|:[0-9]{2,})/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*$" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtFolioInicial">Folio Inicial</label>
                            <div class="col-md-3">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtFolioInicial"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFolioInicial" ControlToValidate="txtFolioInicial" SetFocusOnError="true"
                                    CssClass="MessageError" runat="server" Display="Dynamic" ErrorMessage="Se requiere el folio inicial" />
                            </div>
                        </div>
                        <div class="col-md-offset-5">
                            <asp:Button runat="server" ID="btnGuardarGenerales" CssClass="btn btn-default" Text="Guardar" CausesValidation="true" OnClick="btnGuardarGenerales_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Campos
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updpnlCampos">
                    <ContentTemplate>
                        <div class="col-md-12 alert alert-danger" runat="server" id="lblErrorMessagesCampos" visible="false"></div>
                        <asp:GridView runat="server" ID="grdCampos" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" OnRowDataBound="grdCampos_RowDataBound" OnDataBinding="grdCampos_DataBinding" OnRowEditing="grdCampos_RowEditing" OnRowCancelingEdit="grdCampos_RowCancelingEdit" OnRowUpdating="grdCampos_RowUpdating" OnRowDeleting="grdCampos_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Campo" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdControlXCarrera" Value='<%#Bind("IdControlXCarrera") %>' />
                                        <asp:HiddenField runat="server" ID="hdnIdControl" Value='<%#Bind("IdControl") %>' />
                                        <asp:Label runat="server" ID="lblControl" Text='<%#Bind("IdControlASP")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdControlXCarrera" Value='<%#Bind("IdControlXCarrera") %>' />
                                        <asp:HiddenField runat="server" ID="hdnIdControl" Value='<%#Bind("IdControl") %>' />
                                        <asp:DropDownList ID="ddlControl" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Etiqueta" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEtiqueta" Text='<%#Bind("Etiqueta") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtEtiqueta" Text='<%#Bind("Etiqueta") %>' CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req." ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkRequerido" Enabled="false" Checked='<%#Bind("Requerido") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkRequerido" Checked='<%#Bind("Requerido") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error Requerido" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblErrorRequerido" Text='<%#Bind("EtiquetaRequerido") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtErrorRequerido" Text='<%#Bind("EtiquetaRequerido") %>' CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Validar" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkValidar" Enabled="false" Checked='<%#Bind("RegularExpression") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkValidar" Checked='<%#Bind("RegularExpression") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expresion Reg." ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblExpReg" Text='<%#Bind("ValidationExpression") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtExpReg" Text='<%#Bind("ValidationExpression") %>' CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error Exp.Reg." ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblErrorExpReg" Text='<%#Bind("RegularErrorMessage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtErrorExpReg" Text='<%#Bind("RegularErrorMessage") %>' CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Resources/Images/icon-edit.png" CancelImageUrl="~/Resources/Images/icon-cancel.png" UpdateImageUrl="~/Resources/Images/icon-apply.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Resources/Images/icon-delete.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                            </Columns>
                        </asp:GridView>
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnAddNewRow" CssClass="btn btn-default" Text="Agregar nuevo" OnClick="btnAddNewRow_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Ramas
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updRamas">
                    <ContentTemplate>
                        <div class="col-md-12 alert alert-danger" runat="server" id="lblErrorRamas" visible="false"></div>
                        <div class="text-right">
                            <asp:LinkButton CssClass="" runat="server" ID="lnkShowInactiveRamas" OnClick="lnkShowInactiveRamas_Click"></asp:LinkButton>
                        </div>
                        <asp:GridView runat="server" ID="grdRamas" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" OnRowEditing="grdRamas_RowEditing" OnRowCancelingEdit="grdRamas_RowCancelingEdit" OnRowUpdating="grdRamas_RowUpdating" OnRowDeleting="grdRamas_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdRama" Value='<%#Bind("IdRama") %>' />
                                        <asp:Label runat="server" ID="lblNombre" Text='<%#Bind("Nombre")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdRama" Value='<%#Bind("IdRama") %>' />
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Text='<%#Bind("Nombre")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activo" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Enabled="false" Checked='<%#Bind("Activo") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Checked='<%#Bind("Activo") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Resources/Images/icon-edit.png" CancelImageUrl="~/Resources/Images/icon-cancel.png" UpdateImageUrl="~/Resources/Images/icon-apply.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Resources/Images/icon-delete.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                            </Columns>
                        </asp:GridView>
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnAgregarRama" CssClass="btn btn-default" Text="Agregar nueva Rama" OnClick="btnAgregarRama_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Categorías
            </div>
            <div class="panel-body">
                <div class="panel-body">
                    <asp:UpdatePanel runat="server" ID="updCategoria">
                        <ContentTemplate>
                            <div class="col-md-12 alert alert-danger" runat="server" id="lblErrorCategoria" visible="false"></div>
                            <div class="text-right">
                                <asp:LinkButton CssClass="" runat="server" ID="lnkShowInactiveCategoria" OnClick="lnkShowInactiveCategoria_Click"></asp:LinkButton>
                            </div>
                            <asp:GridView runat="server" ID="grdCategorias" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" OnRowEditing="grdCategorias_RowEditing" OnRowCancelingEdit="grdCategorias_RowCancelingEdit" OnRowUpdating="grdCategorias_RowUpdating" OnRowDeleting="grdCategorias_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombre" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnIdCategoria" Value='<%#Bind("IdCategoria") %>' />
                                            <asp:Label runat="server" ID="lblNombre" Text='<%#Bind("Nombre")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnIdCategoria" Value='<%#Bind("IdCategoria") %>' />
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Text='<%#Bind("Nombre")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
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
                                    <asp:TemplateField HeaderText="Activo" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                        <ItemTemplate>
                                            <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Enabled="false" Checked='<%#Bind("Activo") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Checked='<%#Bind("Activo") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Resources/Images/icon-edit.png" CancelImageUrl="~/Resources/Images/icon-cancel.png" UpdateImageUrl="~/Resources/Images/icon-apply.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Resources/Images/icon-delete.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                                </Columns>
                            </asp:GridView>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Button runat="server" ID="btnAgregarCategoria" CssClass="btn btn-default" Text="Agregar nueva Categoria" OnClick="btnAgregarCategoria_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Rutas
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updRutas">
                    <ContentTemplate>
                        <div class="col-md-12 alert alert-danger" runat="server" id="lblErrorRutas" visible="false"></div>
                        <div class="text-right">
                            <asp:LinkButton runat="server" ID="lnkShowInactiveRutas" OnClick="lnkShowInactiveRutas_Click"></asp:LinkButton>
                        </div>
                        <asp:GridView runat="server" ID="grdRutas" AutoGenerateColumns="false" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" OnRowDataBound="grdRutas_RowDataBound" OnDataBinding="grdRutas_DataBinding" OnRowEditing="grdRutas_RowEditing" OnRowCancelingEdit="grdRutas_RowCancelingEdit" OnRowUpdating="grdRutas_RowUpdating" OnRowDeleting="grdRutas_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Categoria" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdRuta" Value='<%#Bind("IdRuta") %>' />
                                        <asp:HiddenField runat="server" ID="hdnIdCategoria" Value='<%#Bind("IdCategoria") %>' />
                                        <asp:Label runat="server" ID="lblCategoria" Text='<%#Bind("NombreCategoria")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnIdRuta" Value='<%#Bind("IdRuta") %>' />
                                        <asp:HiddenField runat="server" ID="hdnIdCategoria" Value='<%#Bind("IdCategoria") %>' />
                                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNombre" Text='<%#Bind("Nombre")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Text='<%#Bind("Nombre")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distancia Km" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistanciaKM" runat="server" Text='<%#Bind("DistanciaKM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDistanciaKM" runat="server" Text='<%#Bind("DistanciaKM") %>' class="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activo" ControlStyle-Font-Size="8pt" HeaderStyle-Font-Size="9pt" ItemStyle-Font-Size="8pt">
                                    <ItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Enabled="false" Checked='<%#Bind("Activo") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox class="checkbox-inline" runat="server" ID="chkActivo" Checked='<%#Bind("Activo") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Resources/Images/icon-edit.png" CancelImageUrl="~/Resources/Images/icon-cancel.png" UpdateImageUrl="~/Resources/Images/icon-apply.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Resources/Images/icon-delete.png" ItemStyle-Width="5%" ControlStyle-BorderStyle="None" />
                            </Columns>
                        </asp:GridView>
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnAregarRuta" CssClass="btn btn-default" Text="Agregar nueva Ruta" OnClick="btnAregarRuta_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Niveles
            </div>
            <div class="panel-body">
                <%--AGREGAR Niveles AQUI--%>
            </div>
        </div>
    </div>
</asp:Content>
