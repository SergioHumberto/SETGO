<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentProcess.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.PaymentProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <div>
        <label class="h3">¡Gracias por su pago!</label>

        <br />

        <label class="">El estatus de su pago es: </label> <asp:Label CssClass="" ID="lblStatus" runat="server"></asp:Label>

        <br />

        <asp:Button CssClass="btn btn-default" Text="Cerrar" OnClientClick="Window.Close();" runat="server" />

    </div>

        <div id="tablaNotificacion" runat="server">

        </div>

    </form>
</body>
</html>
