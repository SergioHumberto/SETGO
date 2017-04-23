<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PayPalRestAPI.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.PayPalRestAPI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
    
        <div id="tablaNotificacion" runat="server"></div>

        <div>
            <button class="btn btn-default" runat="server" id="ccc" onserverclick="btnRegistraOtroParticipante_Click"><span class="glyphicon glyphicon-list-alt">&nbsp;</span>Registrar otro participante</button>
            <button class="btn btn-default" onclick="window.print();"><span class="glyphicon glyphicon-print">&nbsp;</span>Imprime esta confirmación</button>
        </div>

        <asp:Literal ID="litMessage" runat="server"></asp:Literal>

    </form>
</body>
</html>