<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="ReporteRegistrados.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.ReporteRegistrados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

            <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
       
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h3>Reporte de participantes registrados</h3>

    <br />

    <div class="input-group" style="width:60%">
        <label>Carrera</label>
        <asp:DropDownList CssClass="form-control" ID="ddlCarrera" AutoPostBack="true" runat="server">
        </asp:DropDownList>
    </div>
    

    <br />

    <asp:Button ID="btnGenerar" Text="Generar reporte" CssClass="btn btn-default" OnClick="btnGenerar_Click" runat="server" />

    <br />

    <%--<rsweb:ReportViewer ID="ReportViewer1" Width="80%" runat="server">

    </rsweb:ReportViewer>--%>
    <rsweb:ReportViewer ID="ReportViewer2" Visible="false" runat="server" Width="80%"></rsweb:ReportViewer>

</asp:Content>
