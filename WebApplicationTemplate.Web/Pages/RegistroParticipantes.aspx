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
    


    <h1>¡INSCRIBETE AHORA!</h1>
    <h4>1a. Carrera 5k / 10k RACE IN ONE LA LOMA GOLF 2017</h4>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Nombres *</label>
                <asp:TextBox ID="txtNombres" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqNombres" ControlToValidate="txtNombres" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Se requiere nombre" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Apellido paterno *</label>
                <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqApellidoPaterno" ControlToValidate="txtApellidoPaterno" SetFocusOnError="true" 
                    ForeColor="Red" ErrorMessage="Se requiere apellido paterno" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Apellido materno</label>
                <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Edad</label>
                <asp:TextBox ID="txtEdad" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Domicilio</label>
                <asp:TextBox ID="txtDomicilio" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Socio</label>
                <asp:TextBox ID="txtSocio" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Invitado</label>
                <asp:TextBox ID="txtInvitado" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>No. de Acción</label>
                <asp:TextBox ID="txtNoAccion" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Telefono personal</label>
                <asp:TextBox ID="txtTelefonoPersonal" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Email *</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>

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
                <asp:TextBox ID="txtTelefonoEmergencia" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <div class="form-group">
            <label>Rama:</label>
                <asp:RadioButtonList ID="rblRamas" runat="server">
                    <asp:ListItem Text="Varonil 5K" Value="V5K"></asp:ListItem>
                    <asp:ListItem Text="Varonil 10K" Value="V10K"></asp:ListItem>
                    <asp:ListItem Text="Femenil 5K" Value="F5K"></asp:ListItem>
                    <asp:ListItem Text="Femenil 10K" Value="F10K"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>


    <div style="width:60%" class="row">
        <div class="col-md-6">
            <label>Carrera:</label>
            <asp:RadioButtonList ID="rblCarrera" runat="server">
                <asp:ListItem Text="Infantil $246" Value="246"></asp:ListItem>
                <asp:ListItem Text="Adulto $296" Value="296"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <label>
                En pleno uso de mis facultades, declaro estar sano y apto para participar en el evento, reconozco los riesgos inherentes a la práctica deportiva, por lo que voluntariamente y con conocimiento pleno de esto, acepto y asumo la responsabilidad de mi integridad física, y libero de toda responsabilidad al Club de Golg La Loma S.A. DE C.V. y al comité organizador. *
            </label>
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-6">
            <asp:CheckBox ID="chkAcepto" Text="Acepto" runat="server"/>
            <br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                ErrorMessage="Se requiere aceptar las condiciones" Display="Dynamic" ClientValidationFunction="ValidateCheckBox"
                 ForeColor="Red" SetFocusOnError="true" ></asp:CustomValidator>        
        </div>
    </div>

    <div style="width:60%" class="row">
        <div class="col-md-12">
            <div style="float:right">
                <h4>Mex$<span id="lblTotal">0.00</span></h4>
                <h5 style="text-align:center; margin:0;" >TOTAL</h5>
            </div>
        </div>
    </div>

    
    <asp:Button ID="btnEnviar" CssClass="btn btn-default" CausesValidation="true" Text="Enviar" runat="server" />

    <script type="text/javascript">

        $(document).ready(function () {
            
            $("#<%= rblCarrera.ClientID %>").click(function () {
                var radioSelect = $("#<%= rblCarrera.ClientID %> input:checked")

                $("#lblTotal").text(radioSelect.val());
            });

        });
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
