<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true"
	CodeBehind="Demo.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.Demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<h1>
		Header 1
	</h1>
	<h2>
		Header 2
	</h2>
	<h3>
		Header 3
	</h3>
	<p>
		<span>Plain text.</span> <a href="#">Link</a>
	</p>
	<p>
		<label>
			Label</label>
	</p>
	<p>
		<span class="Message">Message</span>
	</p>
	<p>
		<span class="MessageError">Error Message</span>
	</p>
	<p>
		<span class="MessageWarning">Warning Message</span>
	</p>
	<p>
		<input type="text" value="Input Text" />
		<input type="password" value="Input Password" />
	</p>
	<p>
		<input type="text" value="Required Input Text" class="RequiredField" />
		<input type="password" value="Required Input Password" class="RequiredField" />
		<span class="WarningRequiredField">&nbsp;</span>
	</p>
	<p>
		<input type="text" value="Disabled Input Text" disabled="disabled" />
		<input type="password" value="Disabled Input Password" disabled="disabled" />
	</p>
	<p>
		<textarea rows="3" cols="20">Text Area</textarea>
	</p>
	<p>
		<textarea rows="3" cols="20" class="RequiredField">Required Text Area</textarea>
	</p>
	<p>
		<textarea rows="3" cols="20" disabled="disabled">Disabled Text Area</textarea>
	</p>
	<p>
		<select>
			<option>Select</option>
		</select>
		<select class="RequiredField">
			<option>Required Select</option>
		</select>
		<select disabled="disabled">
			<option>Disabled Select</option>
		</select>
	</p>
	<p>
		<input type="button" value="Input Button" />
		<input type="submit" value="Input Submit" />
		<button>
			Element Button</button>
	</p>
	<p>
		<input type="button" value="Disabled Input Button" disabled="disabled" />
		<input type="submit" value="Disabled Input Submit" disabled="disabled" />
		<button disabled="disabled">
			Disabled Element Button</button>
	</p>
	<p>
		<asp:Button ID="Button1" runat="server" CssClass='<%$ Resources: Global, CssButtonSearch %>' />
		<asp:Button ID="Button2" runat="server" CssClass='<%$ Resources: Global, CssButtonClearFields %>' />
		<asp:Button ID="Button3" runat="server" CssClass='<%$ Resources: Global, CssButtonNew %>' />
		<asp:Button ID="Button4" runat="server" CssClass='<%$ Resources: Global, CssButtonPrint %>' />
		<asp:Button ID="Button5" runat="server" CssClass='<%$ Resources: Global, CssButtonClose %>' />
		<asp:Button ID="Button6" runat="server" CssClass="ButtonImage" />
		<asp:Button ID="Button7" runat="server" CssClass="ButtonCalendar" />
	</p>
	<div class="MenuOption">
		<a href="#">Menu Option 1</a> <a href="#">Menu Option 2</a> <a href="#">Menu Option
			3</a>
	</div>
	<p>
		<table class="TableData">
			<tbody>
				<tr class="TableHeader">
					<td>
						Column 1
					</td>
					<td>
						Column 2
					</td>
					<td>
						Column 3
					</td>
				</tr>
				<tr class="TableRow">
					<td>
						Normal Row 1
					</td>
					<td>
						Normal Row 1
					</td>
					<td>
						Normal Row 1
					</td>
				</tr>
				<tr class="TableRowAlternate">
					<td>
						Alternate Row 2
					</td>
					<td>
						Alternate Row 2
					</td>
					<td>
						Alternate Row 2
					</td>
				</tr>
				<tr class="TableRowSelected">
					<td>
						Selected Row 2
					</td>
					<td>
						Selected Row 2
					</td>
					<td>
						Selected Row 2
					</td>
				</tr>
				<tr class="TableFooter">
					<td>
						&nbsp;
					</td>
					<td>
						&nbsp;
					</td>
					<td>
						&nbsp;
					</td>
				</tr>
			</tbody>
		</table>
	</p>
</asp:Content>
