<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="ConfiguraCarrera.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.ModificaCarrera" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <div class="form-horizontal">
        <div class="form-group">
            <h2 class="col-md-12">Configura Carrera</h2>
        </div>
        <div class="col-md-12 alert alert-danger" runat="server" id="lblError" visible="false"></div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Generales
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="col-md-1 col-md-offset-10 col-xs-12" for="lblEstatus">Estatus:</label>
                    <asp:Label CssClass="col-md-1 col-xs-12" runat="server" ID="lblEstatus"></asp:Label>
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
                            ControlToValidate="txtURLRegistro" Display="Dynamic" CssClass="MessageError" ValidationExpression="(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-:]+[a-zA-Z0-9]\.[^\s]{4,}|www\.[a-zA-Z0-9][a-zA-Z0-9-:]+[a-zA-Z0-9]\.[^\s]{4,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{4,}|www\.[a-zA-Z0-9]\.[^\s]{4,})" />
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
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Campos
            </div>
            <div class="panel-body">
                <%--AGREGAR CAMPOS AQUI--%>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Ramas
            </div>
            <div class="panel-body">
                <%--AGREGAR Ramas AQUI--%>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Categorías
            </div>
            <div class="panel-body">
                <%--AGREGAR Categorías AQUI--%>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                Rutas
            </div>
            <div class="panel-body">
                <%--AGREGAR Rutas AQUI--%>
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
