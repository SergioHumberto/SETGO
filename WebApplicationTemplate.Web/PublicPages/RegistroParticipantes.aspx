<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroParticipantes.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.RegistroParticipantes" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function removeWhitespaces(txtbox) {            
          //Get the value from textbox
            var txtbox = document.getElementById(txtbox);
          //Remove all white spaces from textbox using the regex
            txtbox.value = txtbox.value.replace(/\s/g, "");            
        }
    </script>
</head>
<body>
    <form runat="server">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
        
        

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="divHtmlRender" runat="server" >
    </div>

    <asp:UpdatePanel ID="upCusError" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="has-error">
                <asp:CustomValidator ID="cusError" runat="server" Display="Dynamic" ForeColor="Red" ></asp:CustomValidator>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div  style="width:60%;display:none;" class="row">
        <div class="col-md-6">
            <div class="input-group">
                <label class="">Tipo de registro</label>
                <asp:DropDownList ID="ddlTipoRegistro" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoRegistro_SelectedIndexChanged" runat="server">
                    <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                    <asp:ListItem Text="Equipo" Value="Equipo"></asp:ListItem>
                </asp:DropDownList>
             </div>
         </div>
        <div class="col-md-6" id="divTipoEquipo" runat="server" visible="false">
            <div class="input-group">
                <label class="">Cantidad de participantes</label>
                <asp:DropDownList ID="ddlTipoEquipo" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEquipo_SelectedIndexChanged" CssClass="form-control" runat="server" >
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <br />

    <asp:PlaceHolder ID="phInformacionPersonal" runat="server">

    <div id="divNumParticipante" runat="server" visible="false" style="width:60%" class="row">
        <div class="col-md-6">
            <div  class="form-group" style="width:60%" >
                <span class="">Información del participante </span>
                <asp:Label runat="server" ID="lblNumParticipante"></asp:Label>
            </div>
        </div>
    </div>        

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblApellidoPaterno" visible="false" runat="server">Apellido paterno *</label>
                <asp:TextBox ID="txtApellidoPaterno" Visible="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Enabled="false" ID="reqApellidoPaterno" ControlToValidate="txtApellidoPaterno" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere apellido paterno" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblApellidoMaterno" visible="false" runat="server">Apellido materno</label>
                <asp:TextBox ID="txtApellidoMaterno" Visible="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Enabled="false" ID="reqApellidoMaterno" ControlToValidate="txtApellidoMaterno" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere apellido materno" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblEtiquetaNombre" visible="false" runat="server" />
                <asp:TextBox ID="txtNombres" MaxLength="50"  Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator Enabled="false" ID="reqNombres" ControlToValidate="txtNombres" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Se requiere nombre" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>


    <div style="width:60%" class="row">
        <div class="col-md-6">
            <UserControls:DatePicker ID="datePickerEdad" Visible="false" Text="Fecha de nacimiento*" IsRequired="true" SetFocusOnError="true" ErrorMessage="Debe seleccionar una fecha" runat="server" />
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblSocio" visible="false" runat="server">Socio</label>
                <asp:TextBox ID="txtSocio" MaxLength="50" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblInvitado" visible="false" runat="server">Invitado</label>
                <asp:TextBox ID="txtInvitado" MaxLength="2" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblNoAccion" visible="false" runat="server">No. de Acción</label>
                <asp:TextBox ID="txtNoAccion" Visible="false" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator Enabled="false" ID="revTxtNoAccion" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtNoAccion" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblEmail" visible="false" runat="server">Email *</label>
                <asp:TextBox ID="txtEmail" Visible="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                <asp:RequiredFieldValidator Enabled="false" ID="reqEmail" ControlToValidate="txtEmail" SetFocusOnError="true" 
                    ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere un correo electronico" runat="server"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator Enabled="false" ID="revEmail" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico invalido"
                    ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblTelefonoPersonal" visible="false" runat="server">Telefono personal</label>
                <asp:TextBox ID="txtTelefonoPersonal" Visible="false" CssClass="form-control" MaxLength="12" runat="server" onkeyup="removeWhitespaces('txtTelefonoPersonal')"></asp:TextBox>
                
                <asp:RequiredFieldValidator Enabled="false" ID="reqTelefonoPersonal" ControlToValidate="txtTelefonoPersonal" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere telefono" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="revtxtTelefonoPersonal" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtTelefonoPersonal" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblTelefonoEmergencia" visible="false" runat="server">Telefono de emergencia</label>
                <asp:TextBox ID="txtTelefonoEmergencia" Visible="false" CssClass="form-control" MaxLength="12" runat="server" onkeyup="removeWhitespaces('txtTelefonoEmergencia')"></asp:TextBox>

                <asp:RequiredFieldValidator Enabled="false" ID="reqTelefonoEmergencia" ControlToValidate="txtTelefonoEmergencia" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere telefono de emergencia" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="revtxtTelefonoEmergencia" Enabled="false" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtTelefonoEmergencia" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label id="lblDomicilio" visible="false" runat="server">Domicilio</label>
                <asp:TextBox ID="txtDomicilio" Visible="false" MaxLength="255" CssClass="form-control" runat="server"></asp:TextBox>

                <asp:RequiredFieldValidator Enabled="false" ID="reqDomicilio" ControlToValidate="txtDomicilio" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere domicilio" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>


    </asp:PlaceHolder>
    
    <asp:Button ID="btnGuardarParticipante" CssClass="btn btn-default" Text="Guardar y continuar" OnClick="btnGuardarParticipante_Click" runat="server" Visible="false" />

    <asp:PlaceHolder runat="server" ID="phRamaCategoriaRuta">

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
            <label id="lblRama" visible="false" runat="server">Rama:</label>
                <asp:RadioButtonList ID="rblRamas" Visible="false" runat="server">
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="reqRama" ControlToValidate="rblRamas" Enabled="false" SetFocusOnError="true" 
                    ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere una rama" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upCategoria" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div style="width:60%" class="row">
                <div class="col-md-6">
                    <label id="lblCategoria" visible="false" runat="server">Categoria:</label>
                    <asp:RadioButtonList ID="rblCategoria" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="rblCategoria_SelectedIndexChanged" runat="server">
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqCategoria" Enabled="false" ControlToValidate="rblCategoria" SetFocusOnError="true" 
                            ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere una categoria" runat="server"></asp:RequiredFieldValidator>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblPoliticas" runat="server">
            </asp:Label>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <asp:CheckBox ID="chkAcepto" Visible="false" Text="Acepto" runat="server"/>
            <br />
            <asp:CustomValidator Enabled="false" ID="cusChkAcepto" runat="server" 
                ErrorMessage="Se requiere aceptar las condiciones" Display="Dynamic" ClientValidationFunction="ValidateCheckBox"
                 ForeColor="Red" SetFocusOnError="true" ></asp:CustomValidator>        
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="upTotal" Visible="false" UpdateMode="Always">
        <ContentTemplate>
            <div style="width:60%" class="row">
                <div class="col-md-12">
                    <div style="float:right">
                        <h4>Mex$<span id="lblTotal" runat="server">0.00</span></h4>
                        <h5 style="text-align:center; margin:0;" >TOTAL</h5>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    </asp:PlaceHolder>

    
    <asp:Button ID="btnEnviar" CssClass="btn btn-default" CausesValidation="true" Text="Enviar" OnClick="btnEnviar_Click" runat="server" />

    <br />

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

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

    <script type = "text/javascript">
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
