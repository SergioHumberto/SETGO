<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPal.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.PayPal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form name="formPayPal" action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">
        <input type="hidden" name="cmd" value="_xclick"/>
        <input type="hidden" name="business" value="humberto1_sergio-facilitator@hotmail.com"/>
        <input type="hidden" name="item_name" value="Carrera"/>
        <input type="hidden" name="item_number" value="1"/>
        <input type="hidden" name="currency_code" value="MXN"/>
        <input type="hidden" value="1" name="no_note"/>
        <input type="hidden" value="1" name="no_shipping"/>
        <input type="hidden" name="amount" value="300"/>
        <input type="hidden" name="return" value="http://localhost:61880/WebApplicationTemplate/Pages/Home.aspx"/>
        <input type="hidden" name="cancel_return" value="http://localhost:61880/WebApplicationTemplate/Pages/TestIFrame.aspx"/>
        <input type="hidden" name="notify_url" value="http://localhost:61880/WebApplicationTemplate/Pages/Home.aspx"/>
        <%--<input type="image" src="http://www.paypal.com/es_XC/i/btn/x-click-but01.gif"
               name="submit"
               runat="server"
               id="btnPagar"  />--%>
    </form>

    <script type='text/javascript'>
        document.formPayPal.submit();
    </script>

</body>
</html>
