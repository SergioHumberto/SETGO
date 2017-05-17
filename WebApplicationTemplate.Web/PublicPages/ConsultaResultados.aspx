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

        <div class="input-group" style="width:60%">
            <asp:Table ID="tbRegistros" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblMostrar" runat="server" Text="Mostrar"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlNumRegistros" AutoPostBack="true" runat="server" AppendDataBoundItems="true" 
                            CssClass="dropdown" OnSelectedIndexChanged="ddlNumRegistros_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="lblRegistros" runat="server" Text="Registros"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <br />
        
        <div style="overflow-x:auto;width:100%">
           <div class="input-group" style="width:60%" >
               <asp:GridView ID="grdConsultarResultados" runat="server"
                    OnPageIndexChanging="PageIndexChanging" 
                    AllowPaging = "true"
                    CssClass="table table-bordered bs-table"
                   PageSize="10"
                   PagerSettings-Mode="NextPrevious"
                   PagerSettings-PreviousPageText="Anterior"
                   PagerSettings-NextPageText="Siguiente"
                   PagerStyle-HorizontalAlign="Right"
                   HeaderStyle-BackColor="#d9edf7"
                   >
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
