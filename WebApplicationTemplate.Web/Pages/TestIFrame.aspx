<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PrivatePage.Master" CodeBehind="TestIFrame.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.TestIFrame" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="Content">
    <script type="text/javascript" language="JavaScript">
    //window.onbeforeunload = confirmExit;
    //function confirmExit() {
    //    return "El proceso no ha terminado, ¿deseas cancelar?";
    //    }       
</script>

    <iframe width="600" style="width:100%;border:none;height:2500px !important" allowtransparency="true" runat="server" frameborder="0" scrolling="no" src="../PublicPages/RegistroParticipantes.aspx?IdCarrera=1">

    </iframe>

</asp:Content>
