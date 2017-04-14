<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPal.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.PayPal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</head>
<body>

    <h1>Espere un momento...</h1>

    <form name="formPayPal" action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">
        <input type="hidden" name="cmd" value="_xclick"/>
        <input type="hidden" name="business" value="<%= PayPalEmail %>"/>
        <input type="hidden" name="item_name" value="<%= ItemName %>"/>
        <input type="hidden" name="item_number" value="1"/>
        <input type="hidden" name="currency_code" value="MXN"/>
        <input type="hidden" name="no_note" value="1"/>
        <input type="hidden" name="no_shipping" value="1" />
        <input type="hidden" name="amount" value="<%= Amount %>"/>
        <input type="hidden" name="custom" value="<%= Custom %>" />
        <input type="hidden" name="return" value="<%= ReturnURL %>"/>
        <input type="hidden" name="cancel_return" value="<%= CancelURL %>"/>
        
        <input type="image" name="submit" src="http://www.paypal.com/es_XC/i/btn/x-click-but01.gif" />
    </form>

    <script type="text/javascript">

        $(document).ready(function () {
            document.forms["formPayPal"].submit();
        });

    </script>

</body>
</html>
