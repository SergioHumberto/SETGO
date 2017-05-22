<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="ConsultaResultadosIFrame.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.ConsultaResultadosIFrame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <iframe style="width:100%;border:none;height:2500px !important" allowtransparency="true" runat="server" frameborder="0" scrolling="auto"  src="../PublicPages/ConsultaResultados.aspx">
    </iframe>
</asp:Content>
