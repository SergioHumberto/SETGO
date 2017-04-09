<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PrivatePage.Master" CodeBehind="TestIFrame.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.TestIFrame" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="Content">

    <iframe width="600" height="1600" style="width:100%;border:none;height:1600px !important" allowtransparency="true" runat="server" frameborder="0" scrolling="no" src="RegistroParticipantes.aspx">

    </iframe>

</asp:Content>
