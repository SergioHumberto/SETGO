<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaResultados.aspx.cs" Inherits="WebApplicationTemplate.Web.PublicPages.ConsultaResultados1" %>

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

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h3>Consulta de resultados</h3>

        <br />

        <div class="input-group" style="width:60%">
            <label>Carrera</label>
            <asp:DropDownList CssClass="form-control" ID="ddlCarrera" AutoPostBack="true" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="-1">-- Seleccione una Carrera --</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblErrorCarrera" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>

        <br />

        <div class="input-group" style="width:60%">
            <asp:Button ID="btnConsultarResultados" CssClass="btn btn-default" runat="server" Text="Consultar Resultados"
                    OnClick="btnConsultarResultados_Click"/>
        </div>

        <br />

        <div style="overflow-x:auto;width:100%">
           <div class="input-group" style="width:60%" >
               <asp:GridView ID="grdConsultarResultados" runat="server"
                    OnPageIndexChanging="PageIndexChanging" 
                    AllowPaging = "true"
                    CssClass="table table-bordered bs-table">
                </asp:GridView>
           </div>
        </div>

        <br />

        <div class="input-group" style="width:60%">
            <asp:Label ID="lblError" Text="" runat="server"></asp:Label>
        </div>

    </form>
</body>
</html>
