<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerControl.ascx.cs" Inherits="WebApplicationTemplate.Web.Controls.UserControls.DatePickerControl" %>

<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

<script>
 $.datepicker.regional['es'] = {
 closeText: 'Cerrar',
 prevText: '< Ant',
 nextText: 'Sig >',
 currentText: 'Hoy',
 monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
 monthNamesShort: ['Ene','Feb','Mar','Abr', 'May','Jun','Jul','Ago','Sep', 'Oct','Nov','Dic'],
 dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
 dayNamesShort: ['Dom','Lun','Mar','Mié','Juv','Vie','Sáb'],
 dayNamesMin: ['Do','Lu','Ma','Mi','Ju','Vi','Sá'],
 weekHeader: 'Sm',
 dateFormat: 'dd/mm/yy',
 firstDay: 1,
 isRTL: false,
 showMonthAfterYear: false,
 yearSuffix: ''
 };
 $.datepicker.setDefaults($.datepicker.regional['es']);

</script>

<label><%= Text %></label>


<asp:TextBox runat="server" CssClass="form-control" maxlength="10" ID="datepicker" 
    placeholder="DD-MM-YYYY"  />
<asp:RequiredFieldValidator ID="reqValidator" ControlToValidate="datepicker" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>



<script type="text/javascript">

    <%--$(document).ready(function() {
    
    if( '<%= IsRequired %>' == 'False' )
    {
        ValidatorEnabled($("<%= reqValidator.ClientID %>"), false);
    }
    
    });
    --%>

</script>


<script type="text/javascript">
    $(function () {

        $('#<%= datepicker.ClientID  %>').datepicker({
            dateFormat: "dd-mm-yy",
            regional: "es" 
        });
    });

  </script>