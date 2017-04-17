<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroParticipantes.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.RegistroParticipantes" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                <label>Nombres *</label>
                <asp:TextBox ID="txtNombres" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqNombres" ControlToValidate="txtNombres" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Se requiere nombre" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Apellido paterno *</label>
                <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqApellidoPaterno" ControlToValidate="txtApellidoPaterno" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere apellido paterno" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Apellido materno</label>
                <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Edad *</label>
                <asp:TextBox ID="txtEdad" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTxtEdad" ControlToValidate="txtEdad" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere indicar edad" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtEdad" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtEdad" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Domicilio</label>
                <asp:TextBox ID="txtDomicilio" MaxLength="255" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Socio</label>
                <asp:TextBox ID="txtSocio" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Invitado</label>
                <asp:TextBox ID="txtInvitado" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>No. de Acción</label>
                <asp:TextBox ID="txtNoAccion" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revTxtNoAccion" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtNoAccion" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Telefono personal</label>
                <asp:TextBox ID="txtTelefonoPersonal" CssClass="form-control" MaxLength="12" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtTelefonoPersonal" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtTelefonoPersonal" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Email *</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" SetFocusOnError="true" 
                    ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere un correo electronico" runat="server"></asp:RequiredFieldValidator>
                
                <asp:RegularExpressionValidator ID="revEmail" runat="server" SetFocusOnError="true" ErrorMessage="Correo electronico invalido"
                    ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Telefono de emergencia</label>
                <asp:TextBox ID="txtTelefonoEmergencia" CssClass="form-control" MaxLength="12" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtTelefonoEmergencia" runat="server" SetFocusOnError="true" 
                    ErrorMessage="Debe ser un valor numerico"
                    ControlToValidate="txtTelefonoEmergencia" Display="Dynamic" ForeColor="Red" ValidationExpression="\d+" />
            </div>
        </div>
    </div>
    </asp:PlaceHolder>
    
    <asp:Button ID="btnGuardarParticipante" CssClass="btn btn-default" Text="Guardar y continuar" OnClick="btnGuardarParticipante_Click" runat="server" Visible="false" />

    <asp:PlaceHolder runat="server" ID="phRamaCategoriaRuta">

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
            <label>Rama:</label>
                <asp:RadioButtonList ID="rblRamas" runat="server">
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="reqRama" ControlToValidate="rblRamas" SetFocusOnError="true" 
                    ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere una rama" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upCategoria" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div style="width:60%" class="row">
                <div class="col-md-6">
                    <label>Categoria:</label>
                    <asp:RadioButtonList ID="rblCategoria" AutoPostBack="true" OnSelectedIndexChanged="rblCategoria_SelectedIndexChanged" runat="server">
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqCategoria" ControlToValidate="rblCategoria" SetFocusOnError="true" 
                            ForeColor="Red" Display="Dynamic" ErrorMessage="Se requiere una categoria" runat="server"></asp:RequiredFieldValidator>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblPoliticas" runat="server">
                En pleno uso de mis facultades, declaro estar sano y apto para participar en el evento, reconozco los riesgos inherentes a la práctica deportiva, por lo que voluntariamente y con conocimiento pleno de esto, acepto y asumo la responsabilidad de mi integridad física, y libero de toda responsabilidad al Club de Golg La Loma S.A. DE C.V. y al comité organizador. *
            </asp:Label>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <asp:CheckBox ID="chkAcepto" Text="Acepto" runat="server"/>
            <br />
            <asp:CustomValidator ID="cusChkAcepto" runat="server" 
                ErrorMessage="Se requiere aceptar las condiciones" Display="Dynamic" ClientValidationFunction="ValidateCheckBox"
                 ForeColor="Red" SetFocusOnError="true" ></asp:CustomValidator>        
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="upTotal" UpdateMode="Always">
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


        //$(document).ready(function () {

        //    $("#ddlTipoRegistro").change(function () {
        //        if (this.value == "Equipo")
        //        {
        //            $("#divTipoEquipo").show();
        //        }
        //        else if (this.value == "Individual")
        //        {
        //            $("#divTipoEquipo").hide();
        //        }
        //    });

        //    $("#ddlTipoEquipo").change(function () {
        //        // alert('hola');
        //    });

        //});

    </script>
    </form>
</body>
</html>
