<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationTemplate.Web.Default"
	MasterPageFile="~/Masters/PublicPage.Master" %>

<asp:Content ID="content" ContentPlaceHolderID="Content" runat="server">
	<p>
		<asp:Label ID="lblPassword" runat="server" Text="<%$ Resources:Global,Username %>"
			AssociatedControlID="txtUsername" />:
		<asp:TextBox ID="txtUsername" runat="server" />
	</p>
	<p>
		<asp:Label ID="lblUsername" runat="server" Text="<%$ Resources:Global,Password %>"
			AssociatedControlID="txtPassword" />:
		<asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
	</p>
	<p>
		<asp:Button ID="btnSignIn" Text="Sign In" OnClick="btnSignIn_Click" CssClass="button"
			runat="server" />
		<asp:Label ID="lblError" runat="server" CssClass="MessageError" />
	</p>
</asp:Content>
