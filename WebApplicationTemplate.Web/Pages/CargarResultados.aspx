<%@ Page Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="CargarResultados.aspx.cs" Inherits="WebApplicationTemplate.Web.PublicPages.ConsultaResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h3>Carga de resultados</h3>

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
        <asp:FileUpload ID="FileUpload1" CssClass="btn btn-defaul btn-file" runat="server" />
    </div>

    <br />

    <div class="input-group" style="width:60%">
        <asp:Button ID="btnUpload" CssClass="btn btn-default" runat="server" Text="Cargar Resultados"
                OnClick="btnUpload_Click" />
        &nbsp &nbsp
        <asp:Button ID="btnConsultarResultados" CssClass="btn btn-default" runat="server" Text="Consultar Resultados"
                OnClick="btnConsultarResultados_Click"/>
    </div>

    <br />

    <div style="overflow-x:auto;width:100%">
       <div class="input-group" style="width:60%" >
            <asp:GridView ID="GridView1" runat="server"
                    OnPageIndexChanging = "PageIndexChanging" AllowPaging = "true"
                    CssClass="table table-bordered bs-table">
            </asp:GridView>
        </div>
    </div>

    <br />

    <div class="input-group" style="width:60%">
        <fieldset class="form-group">
            <h3>
                <asp:Label runat="server" ID="lblConfiguracion" Text="Marque los campos que desea que se muestren en Resultados."></asp:Label>
            </h3>
            <div class="checkbox checkbox-primary">
                <asp:CheckBoxList ID="chklstCampos" runat="server" CssClass="styled">
                    <asp:ListItem Text="Número" Value="Numero" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Paterno" Value="Paterno" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Materno" Value="Materno" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Nombres" Value="Nombres" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Folio" Value="Folio" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Sexo" Value="Sexo" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Categoría" Value="Categoria" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Procedencia" Value="Procedencia" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Equipo" Value="Equipo" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Telefono" Value="Telefono" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="T_Chip" Value="T_Chip" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="T_Oficial" Value="T_Oficial" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Lug_Cat" Value="Lug_Cat" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Rama" Value="Rama" Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
                <br /><br />
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btnSubmit_Click" />
            </div>
        </fieldset>
        </div>

    <br />

    <div class="input-group" style="width:60%">
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>

</asp:Content>