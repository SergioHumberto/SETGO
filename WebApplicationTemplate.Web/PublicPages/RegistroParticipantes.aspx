<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroParticipantes.aspx.cs" 
    Inherits="WebApplicationTemplate.Web.Pages.RegistroParticipantes" EnableEventValidation="true" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

        <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

        <!-- Latest compiled and minified JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <style>
            .AlertaRequerido {
                color: red;
            }

            .LetraEtiqueta {
                font-size: 14px;
            }

            #disablingDiv
            {
                /* Do not display it on entry */
                display: block; 
 
                /* Display it on the layer with index 1001.
                   Make sure this is the highest z-index value
                   used by layers on that page */
                z-index:1001;
     
                /* make it cover the whole screen */
                position: absolute; 
                top: 0%; 
                left: 0%; 
                width: 100%; 
                height: 100%; 
 
                /* make it white but fully transparent */
                background-color: white; 
                opacity:.00; 
                filter: alpha(opacity=00);
            }

        </style>

    <script type="text/javascript">
        function removeWhitespaces(txtbox) {
            //Get the value from textbox
            var txtbox = document.getElementById(txtbox);
            //Remove all white spaces from textbox using the regex
            txtbox.value = txtbox.value.replace(/\D/g, "");
        }

        $(document).ready(function () {
            $("#disablingDiv").hide();
            console.log("hide");
        });

    </script>
</head>
<body>
    <form runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div id="disablingDiv"></div>

        <div id="divHtmlRender" runat="server">
        </div>

        <asp:UpdatePanel ID="upCusError" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="has-error">
                    <asp:CustomValidator ID="cusError" runat="server" Display="Dynamic" CssClass="AlertaRequerido"></asp:CustomValidator>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />

        <asp:PlaceHolder ID="phInformacionPersonal" runat="server">

            <div id="divNumParticipante" runat="server" visible="false" style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group" style="width: 60%">
                        <span class="">Información del participante </span>
                        <asp:Label runat="server" ID="lblNumParticipante"></asp:Label>
                    </div>
                </div>
            </div>

            <asp:PlaceHolder ID="phApellidoPaterno" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblApellidoPaterno" runat="server" />
                            <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" MaxLength="50" runat="server" />
                            <asp:RequiredFieldValidator ID="reqApellidoPaterno" ControlToValidate="txtApellidoPaterno" SetFocusOnError="true"
                                CssClass="AlertaRequerido" runat="server" Display="Dynamic" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phApellidoMaterno" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblApellidoMaterno" runat="server" />
                            <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" MaxLength="50" runat="server" />
                            <asp:RequiredFieldValidator ID="reqApellidoMaterno" ControlToValidate="txtApellidoMaterno" SetFocusOnError="true"
                                CssClass="AlertaRequerido" ErrorMessage="Se requiere apellido materno" runat="server" Display="Dynamic" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phNombres" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblNombres" runat="server" />
                            <asp:TextBox ID="txtNombres" MaxLength="50" CssClass="form-control" runat="server" />
                            <asp:RequiredFieldValidator ID="reqNombres" ControlToValidate="txtNombres" Display="Dynamic" SetFocusOnError="true"
                                CssClass="AlertaRequerido" ErrorMessage="Se requiere nombre" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <%--<asp:PlaceHolder ID="phDatePickerEdad" Visible="false" runat="server">
    <div style="width:60%" class="row">
        <div class="col-md-6">
            <UserControls:DatePicker ID="datePickerEdad" Text="Fecha de nacimiento*" IsRequired="true" SetFocusOnError="true" ErrorMessage="Debe seleccionar una fecha" runat="server" />
        </div>
    </div>
    </asp:PlaceHolder>--%>

            <asp:PlaceHolder ID="phFechaNacimiento" Visible="false" runat="server">
                <label id="lblFechaNacimiento" runat="server" />

                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <table>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label id="lblDia" class="LetraEtiqueta" runat="server">Dia</label>
                                        <asp:DropDownList ID="ddlDia" CssClass="form-control" runat="server" />
                                    </div>
                                </td>
                                <td style="width: 50px" />
                                <td>
                                    <div class="form-group">
                                        <label id="lblMes" class="LetraEtiqueta" runat="server">Mes</label>
                                        <asp:DropDownList ID="ddlMes" CssClass="form-control" runat="server" />
                                    </div>
                                </td>
                                <td style="width: 50px" />
                                <td>
                                    <div class="form-group">
                                        <label id="lblAnio" class="LetraEtiqueta" runat="server">Año</label>
                                        <asp:DropDownList ID="ddlAnio" CssClass="form-control" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phSocio" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblSocio" visible="false" runat="server">Socio</label>
                            <asp:TextBox ID="txtSocio" MaxLength="50" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phInvitado" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblInvitado" visible="false" runat="server">Invitado</label>
                            <asp:TextBox ID="txtInvitado" MaxLength="2" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phNoAccion" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblNoAccion" visible="false" runat="server">No. de Acción</label>
                            <asp:TextBox ID="txtNoAccion" Visible="false" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator Enabled="false" ID="revTxtNoAccion" runat="server" SetFocusOnError="true"
                                ErrorMessage="Debe ser un valor numerico"
                                ControlToValidate="txtNoAccion" Display="Dynamic" CssClass="AlertaRequerido" ValidationExpression="\d+" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phClub" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblClub" runat="server"></label>
                            <asp:TextBox ID="txtClub" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqClub" ControlToValidate="txtClub" CssClass="AlertaRequerido" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phEmail" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblEmail" runat="server">Email *</label>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" SetFocusOnError="true"
                                CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere un correo electronico" runat="server"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revEmail" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico invalido"
                                ControlToValidate="txtEmail" Display="Dynamic" CssClass="AlertaRequerido" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phTelefonoPersonal" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblTelefonoPersonal" runat="server">Telefono personal</label>
                            <asp:TextBox ID="txtTelefonoPersonal" CssClass="form-control" MaxLength="12" runat="server" onkeyup="removeWhitespaces('txtTelefonoPersonal')"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="reqTelefonoPersonal" ControlToValidate="txtTelefonoPersonal" SetFocusOnError="true"
                                CssClass="AlertaRequerido" ErrorMessage="Se requiere telefono" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revTelefonoPersonal" runat="server" SetFocusOnError="true"
                                ErrorMessage="Debe ser un valor numerico"
                                ControlToValidate="txtTelefonoPersonal" Display="Dynamic" CssClass="AlertaRequerido" ValidationExpression="\d+" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phTelefonoEmergencia" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblTelefonoEmergencia" runat="server">Telefono de emergencia</label>
                            <asp:TextBox ID="txtTelefonoEmergencia" CssClass="form-control" MaxLength="12" runat="server" onkeyup="removeWhitespaces('txtTelefonoEmergencia')"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="reqTelefonoEmergencia" ControlToValidate="txtTelefonoEmergencia" SetFocusOnError="true"
                                CssClass="AlertaRequerido" ErrorMessage="Se requiere telefono de emergencia" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="revtxtTelefonoEmergencia" runat="server" SetFocusOnError="true"
                                ErrorMessage="Debe ser un valor numerico"
                                ControlToValidate="txtTelefonoEmergencia" Display="Dynamic" CssClass="AlertaRequerido" ValidationExpression="\d+" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phDomicilio" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblDomicilio" runat="server">Domicilio</label>
                            <asp:TextBox ID="txtDomicilio" MaxLength="255" CssClass="form-control" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="reqDomicilio" ControlToValidate="txtDomicilio" SetFocusOnError="true"
                                CssClass="AlertaRequerido" ErrorMessage="Se requiere domicilio" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric01" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric01" runat="server" />
                        <asp:TextBox ID="txtGeneric01" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric01" ControlToValidate="txtGeneric01" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric01" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric01" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric02" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric02" runat="server" />
                        <asp:TextBox ID="txtGeneric02" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric02" ControlToValidate="txtGeneric02" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric02" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric02" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric03" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric03" runat="server" />
                        <asp:TextBox ID="txtGeneric03" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric03" ControlToValidate="txtGeneric03" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric03" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric03" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric04" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric04" runat="server" />
                        <asp:TextBox ID="txtGeneric04" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric04" ControlToValidate="txtGeneric04" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric04" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric04" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric05" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric05" runat="server" />
                        <asp:TextBox ID="txtGeneric05" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric05" ControlToValidate="txtGeneric05" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric05" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric05" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric06" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric06" runat="server" />
                        <asp:TextBox ID="txtGeneric06" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric06" ControlToValidate="txtGeneric06" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric06" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric06" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric07" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric07" runat="server" />
                        <asp:TextBox ID="txtGeneric07" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric07" ControlToValidate="txtGeneric07" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric07" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric07" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric08" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric08" runat="server" />
                        <asp:TextBox ID="txtGeneric08" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric08" ControlToValidate="txtGeneric08" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric08" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric08" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric09" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric09" runat="server" />
                        <asp:TextBox ID="txtGeneric09" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric09" ControlToValidate="txtGeneric09" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric09" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric09" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="phGeneric10" Visible="false" runat="server">
            <div style="width: 60%" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label id="lblGeneric10" runat="server" />
                        <asp:TextBox ID="txtGeneric10" MaxLength="255" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="reqGeneric10" ControlToValidate="txtGeneric10" SetFocusOnError="true"
                            CssClass="AlertaRequerido" Enabled="false" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator Enabled="false" ID="revGeneric10" runat="server"
                            SetFocusOnError="true" ControlToValidate="txtGeneric10" Display="Dynamic" CssClass="AlertaRequerido" />
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder runat="server" ID="phRamaCategoriaRuta">

            <asp:PlaceHolder ID="phRamas" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label id="lblRamas" runat="server">Rama:</label>
                            <asp:RadioButtonList ID="rblRamas" runat="server">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="reqRamas" ControlToValidate="rblRamas" SetFocusOnError="true"
                                CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere una rama" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phCategoria" Visible="false" runat="server">
                <asp:UpdatePanel ID="upCategoria" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <div style="width: 60%" class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label id="lblCategoria" runat="server">Categoria:</label>
                                    <asp:RadioButtonList ID="rblCategoria" AutoPostBack="true" OnSelectedIndexChanged="rblCategoria_SelectedIndexChanged" runat="server">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="reqCategoria" ControlToValidate="rblCategoria" SetFocusOnError="true"
                                        CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere una categoria" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phRuta" Visible="false" runat="server">
                <asp:UpdatePanel ID="upRuta" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <div style="width: 60%" class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label id="lblRuta" visible="false" runat="server">Ruta:</label>
                                    <asp:RadioButtonList ID="rblRuta" AutoPostBack="true" runat="server">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="reqRuta" ControlToValidate="rblRuta" SetFocusOnError="true"
                                        CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere una Ruta" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phClasificacion" Visible="false" runat="server">

                <asp:Repeater ID="rptClasificacion" OnItemDataBound="rptClasificacion_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div style="width: 60%" class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label id="lblNombreClasificacion" runat="server" />
                                    <asp:RadioButtonList ID="rblClasificacionItem" CssClass="LetraEtiqueta" runat="server" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </asp:PlaceHolder>

            <br />

                <asp:UpdatePanel ID="upTipoRegistro" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div style="width: 60%; display: block;" class="row">
                                <div class="col-md-6">
                                <div class="input-group">
                                    <label>Tipo de registro</label>
                                    <asp:RadioButtonList ID="rblTipoRegistro" AutoPostBack="true" Enabled="false" CssClass="LetraEtiqueta" OnSelectedIndexChanged="rblTipoRegistro_SelectedIndexChanged" runat="server">
                                        <asp:ListItem Text="Individual" Value="I" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Equipo" Value="E"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <asp:PlaceHolder ID="phTipoRegistro" runat="server" Visible="false">
                        <div style="width: 60%;" class="row">
                            <div class="col-md-6" id="divTipoEquipo" runat="server">
                                <div class="input-group">
                                    <label class="">Cantidad de participantes</label>
                                    <asp:DropDownList ID="ddlTipoEquipo" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEquipo_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>           

                        <br />
                        <div style="width: 60%;" class="row">
                            <div class="col-md-6" id="divNombreEquipo" runat="server">
                                <div class="input-group">

                                    <table>
                                        <tr>
                                            <td>
                                                <label class="">Nombre equipo</label>
                                                <asp:TextBox ID="txtNombreEquipo" MaxLength="255" CssClass="form-control" 
                                                    runat="server" AutoPostBack="true" OnTextChanged="txtNombreEquipo_TextChanged" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="reqNombreEquipo" ControlToValidate="txtNombreEquipo" SetFocusOnError="true"
                                                    CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere un nombre de equipo" runat="server" />
                                                <asp:CustomValidator ID="cusNombreEquipo" CssClass="AlertaRequerido" Display="Dynamic" runat="server"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        
                        <br />
                        <asp:Repeater ID="repeaterEmailParticipanteXEquipo" runat="server">
                            <HeaderTemplate>
                                <label>Correo de los participantes</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <label><%# Eval("Key") %></label>

                                            <table>
                                                <tr>
                                                    <td><asp:TextBox ID="txtEmailParticipanteXEquipo" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="reqEmailParticipanteXEquipo" ControlToValidate="txtEmailParticipanteXEquipo" SetFocusOnError="true"
                                                            CssClass="AlertaRequerido" Display="Dynamic" ErrorMessage="Se requiere un correo electronico" runat="server"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="revEmailParticipanteXEquipo" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico invalido"
                                                            ControlToValidate="txtEmailParticipanteXEquipo" Display="Dynamic" CssClass="AlertaRequerido" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />

                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        </asp:PlaceHolder>
                                  
                    </ContentTemplate>
                </asp:UpdatePanel>


            <asp:PlaceHolder ID="phPoliticas" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblPoliticas" runat="server">
                        </asp:Label>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phAcepto" Visible="false" runat="server">
                <div style="width: 60%" class="row">
                    <div class="col-md-6">
                        <asp:CheckBox ID="chkAcepto" Text="Acepto" runat="server" />
                        <br />
                        <asp:CustomValidator ID="cusAcepto" runat="server"
                            ErrorMessage="Se requiere aceptar las condiciones" Display="Dynamic" ClientValidationFunction="ValidateCheckBox"
                            CssClass="AlertaRequerido" SetFocusOnError="true"></asp:CustomValidator>
                    </div>
                </div>
            </asp:PlaceHolder>

            <br />

            <asp:UpdatePanel runat="server" ID="upFormaPago" UpdateMode="Always">
                <ContentTemplate>
                    <asp:PlaceHolder ID="phFolioOffline" Visible="false" runat="server">
                        <div style="width: 80%" class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <label id="lblFolioOffline" runat="server">Código de Pago</label>
                                    <asp:TextBox ID="txtFolioOffline" Enabled="true" MaxLength="255" CssClass="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="reqFolioOffline" ControlToValidate="txtFolioOffline" SetFocusOnError="true"
                                        CssClass="AlertaRequerido" ErrorMessage="Se requiere folio" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="upTotal" UpdateMode="Always">
                <ContentTemplate>
                    <div style="width: 60%" class="row">
                        <div class="col-md-12">
                            <div style="float: right">
                                <h4>Mex$<span id="lblTotal" runat="server">0.00</span></h4>
                                <h5 style="text-align: center; margin: 0;">TOTAL</h5>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:PlaceHolder>


        <asp:Button ID="btnEnviar" CssClass="btn btn-default" CausesValidation="true" Text="Enviar" OnClick="btnEnviar_Click" runat="server" />

        <br />

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <script type="text/javascript">

            $('#btnEnviar').click(function () {
                if (Page_ClientValidate()) {
                    this.enable = false;
                    this.value = 'Procesando espere por favor';
                }
            });

            <%--$(document).ready(function () {
            
            $("#<%= rblCategoria.ClientID %>").click(function () {
                var radioSelect = $("#<%= rblCategoria.ClientID %> input:checked")

                var registroEnEquipo = '<%= RegistroEnEquipo %>';
                if(registroEnEquipo == "False")
                {
                    // $("#lblTotal").text(radioSelect.val());

                    $.ajax({
                        type: "POST",
                        url: '<%= URLWSGetPrecioCategoria %>',
                        data: '{IdCategoria: "' + radioSelect.val() + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        failure: function(response) {
                            alert(response.d);
                        },
                        success: function (data) {
                            $("#lblTotal").text(data.d);
                            // $("#searchresultsA").html(data); // show the string that was returned, this will be the data inside the xml wrapper
                        }
                    });
                }
            });
        });--%>
        </script>

        <script type="text/javascript">
            function ValidateCheckBox(sender, args) {
                if (document.getElementById("<%= chkAcepto.ClientID %>").checked == true) {
                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }
            }

        </script>

    </form>
</body>
</html>
