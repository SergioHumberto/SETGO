<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaResultados.aspx.cs" Inherits="WebApplicationTemplate.Web.PublicPages.ConsultaResultados1" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>

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

        <asp:Label ID="lblErrorCarrera" ForeColor="Red" runat="server"></asp:Label>

        <obout:Grid id="grdConsultarResultados" runat="server"  AutoGenerateColumns="false" CallbackMode="true" Serialize="true" 
			 FolderStyle="styles/premiere_blue" AllowColumnResizing="true" EnableRecordHover="true"
			AllowAddingRecords="true" Language="es" AllowDataAccessOnServer="true" AllowFiltering="true" ShowLoadingMessage="true">
            <Columns>
                <obout:Column DataField="Nombres" Wrap="true" HeaderText="Nombres" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Paterno" Wrap="true" HeaderText="Paterno" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Materno" HeaderText="Materno" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Sexo" HeaderText="Sexo" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="T_Chip" HeaderText="Tiempo chip" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Lug_Rama" HeaderText="Lugar rama" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Vel" HeaderText="Vel" Align="left" runat="server" Width="100"/>
                <obout:Column DataField="Lug_Gral" Align="left" Wrap="true" Width="120" HeaderStyle-Wrap="true" HeaderText="Lugar general" runat="server"/>
                <obout:Column>
                    <TemplateSettings TemplateId="templateCertificado" HeaderTemplateId="templateHeaderCertificado" />
                </obout:Column>

                <%-- otros campos --%>
                <%--<obout:Column DataField="Categoria" HeaderText="Categoria" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Numero" HeaderText="Numero" Wrap="true" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Folio" HeaderText="Folio" Width="90" runat="server"/>--%>
                <%--<obout:Column DataField="Procedencia" HeaderText="Procedencia" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Equipo" HeaderText="Equipo" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Telefono" HeaderText="Telefono" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="T_Oficial" HeaderText="T_Oficial" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Lug_Cat" HeaderText="Lug_Cat" Width="100" runat="server"/>--%>
                <%--<obout:Column DataField="Rama" HeaderText="Rama" Width="100" runat="server"/>--%>
            </Columns>
            <Templates>
                <obout:GridTemplate runat="server" ID="templateHeaderCertificado">
                    <Template>
                        Certificado
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="templateCertificado">
                    <Template>
                        <asp:LinkButton runat="server" ID="lnkBtnImprimirCertificado" OnClick="lnkBtnImprimirCertificado_Click" Text="Imprime"></asp:LinkButton>
                    </Template>
                </obout:GridTemplate>
            </Templates>
		</obout:Grid>		

        <div class="input-group" style="width:60%">
            <asp:Label ID="lblError" Text="" runat="server"></asp:Label>
        </div>

    </form>
</body>
</html>
