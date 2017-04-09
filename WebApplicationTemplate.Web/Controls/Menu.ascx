<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="WebApplicationTemplate.Web.Controls.Menu" %>
<%@ Import Namespace="WebApplicationTemplate.Web.Tools" %>
<table class="MainMenu">
	<tr>
		<td class="MainMenuLeft">
			&nbsp;
		</td>
		<td class="MainMenuCenter">
			<a href="<%= Urls.Home() %>">Home</a> 
			<a href="<%= Urls.Demo() %>">Demo</a>
            <a href="<%= Urls.RegistroParticipantes() %>">Registro de participantes</a>
            <a href="<%= Urls.TestIFrame() %>">Test iframe</a>
			<a href="<%= Urls.SignOut() %>">Sign out</a>
		</td>
		<td class="MainMenuRight">
			&nbsp;
		</td>
	</tr>
</table>
