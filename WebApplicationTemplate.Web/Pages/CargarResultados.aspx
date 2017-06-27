<%@ Page Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="CargarResultados.aspx.cs" Inherits="WebApplicationTemplate.Web.PublicPages.ConsultaResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnCopy").click(function () {
                var id = "#" + "<%= txtURL.ClientID %>";
                try {
                    $(id).select();
                    document.execCommand("copy");
                }
                catch (e) {
                    alert('Copy operation failed');
                }
            });
        });
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h3>Carga de resultados</h3>

    <br />

    <div class="input-group" style="width: 60%">
        <asp:UpdatePanel ID="upCarrera" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblCarrera" runat="server" Text="Carrera"></asp:Label>
                <asp:DropDownList CssClass="form-control" ID="ddlCarrera" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged">
                    <asp:ListItem Value="-1">-- Seleccione una Carrera --</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblErrorCarrera" runat="server" Text="" ForeColor="Red"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />

    <div class="input-group" style="width: 60%">
        <asp:UpdatePanel ID="upCategoria" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblCategoria" runat="server" Text="Categoría" Visible="false"></asp:Label>
                <asp:DropDownList CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                    Visible="false" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                    <asp:ListItem Value="-1">-- Seleccione una Categoría --</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="lblErrorCategoria" runat="server" Text="" ForeColor="Red"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />

    <div class="input-group" style="width: 60%">
        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="false" />

        <asp:Button ID="btnUpload" runat="server" Text="Cargar Resultados"
            OnClick="btnUpload_Click" Style="display: none" />
    </div>

    <br />

    <div class="input-group" style="width: 60%">
        <asp:Button ID="btnConsultarResultados" CssClass="btn btn-default" runat="server" Text="Consultar Resultados"
            OnClick="btnConsultarResultados_Click" />
    </div>
    <br />
    <div class="input-group" style="width: 100%">
        <asp:UpdatePanel ID="upURL" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <label class="control-label" for="txtURL">URL para visualizar los resultados</label>
                <div class="clearfix"></div>
                <div>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtURL" TextMode="Url" ReadOnly="true"></asp:TextBox>
                    <div class="text-right">
                        <a class="btn btn-link" id="btnCopy">Copiar URL</a>
                        <span>|</span>
                        <asp:HyperLink runat="server" Target="_blank" ID="lnkVistaPrevia" CssClass="btn btn-link" Text="Vista Previa"></asp:HyperLink>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />

    <asp:UpdatePanel ID="upGrdResultados" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="overflow-x: auto; width: 100%">
                <div class="input-group" style="width: 60%">
                    <asp:GridView ID="grdResultados" runat="server"
                        OnPageIndexChanging="PageIndexChanging" AllowPaging="true"
                        CssClass="table table-bordered bs-table"
                        PagerSettings-Mode="NextPrevious"
                        PagerSettings-PreviousPageText="Anterior"
                        PagerSettings-NextPageText="Siguiente">
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <asp:UpdatePanel ID="updLstConfigCarrera" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="col-md-12 alert alert-success" runat="server" id="lblSuccessConfig" visible="false"></div>
            <div class="input-group" style="width: 100%" runat="server" id="divConfig">
                <asp:Label runat="server" CssClass="control-label" ID="lblConfiguracion" Text="Marque los campos que desea que se muestren en Resultados." AssociatedControlID="chklstCampos"></asp:Label>
                <div>
                    <asp:CheckBoxList ID="chklstCampos" runat="server" RepeatColumns="4" Width="100%" Font-Size="Small" Font-Bold="false">
                    </asp:CheckBoxList>
                    <br />
                </div>
                <div class="input-group" style="width: 60%">
                    <label class="control-label" for="upldImgCertf">Imagen de fondo para el certificado</label>
                    <asp:FileUpload ID="upldImgCertf" runat="server" AllowMultiple="false"/>
                    <asp:Button ID="btnUpldImgCertf" runat="server" Text="Cargar Imagen"
                        OnClick="btnUpldImgCertf_Click" Style="display: none" />
                </div>
                <br />
                <div class="form-group" style="width: 100%">
                    <label class="control-label" for="upldImgCertf">Imagen guardada como</label>
                    <asp:TextBox CssClass="form-control col-md-12" runat="server" ID="txtImgFileName" ReadOnly="true" />
                    <div class="text-right">
                        <asp:HyperLink runat="server" Target="_blank" ID="lnkVerImagen" CssClass="btn btn-link" Text="Ver Imagen"></asp:HyperLink>
                    </div>
                </div>
                <br /><br />
                <div class="form-group" style="width: 50%">
                    <label class="control-label" for="upldImgCertf">Formato de Certificado</label>
                    <asp:DropDownList runat="server" ID="ddlFormatoCert" CssClass="form-control"></asp:DropDownList>
                </div>
                <br /><br />
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnSubmit_Click" />
            </div>
        </ContentTemplate>
        <Triggers>            
            <asp:PostBackTrigger ControlID="btnUpldImgCertf" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />            
        </Triggers>
    </asp:UpdatePanel>

    <br />

    <div class="input-group" style="width: 100%">
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>

    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }

        function UploadImageCertf(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpldImgCertf.ClientID %>").click();
            }
        }
    </script>

</asp:Content>
