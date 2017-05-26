<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaResultados.aspx.cs" Inherits="WebApplicationTemplate.Web.PublicPages.ConsultaResultados1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

        <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

        <!-- Latest compiled and minified JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

        <link rel="stylesheet" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" />
        <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>

        <style type="text/css">
            .Titulo {
                background-color: #d9edf7;
                font-weight: 700;
                vertical-align: middle;
            }

            .Titulo:hover {
                background-color: #31B0E6;
            }
        </style>

        <script type="text/javascript">

            $(function () {
                bindDataTable(); // bind data table on first page load
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable); // bind data table on every UpdatePanel refresh
            });

            function bindDataTable(){
                $('#tbl').dataTable({
                    "sScrollY": "90%",
                    //'sPaginationType': 'full_numbers',
                    //'iDisplayLength': 5,
                    "bPaginate": true,
                    "bProcessing": true,
                    "bServerSide": false,
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ registros por página",
                        "zeroRecords": "No se encontraron registros coincidentes",
                        "info": "Mostrando página _PAGE_ de _PAGES_",
                        "infoEmpty": "No hay registros disponibles",
                        "infoFiltered": "(filtrados de _MAX_ registros totales)",
                        "search": "Búsqueda:",
                        "paginate": {
                            "previous": "Anterior",
                            "next": "Siguiente"
                        }
                    }
                });
            }

        </script>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="upDdlCarreraDdlCategoria" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCarrera" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="input-group">
                                <label class="input-group-addon">Carrera</label>
                                <asp:DropDownList ID="ddlCarrera" AutoPostBack="true"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>
                <br />

                <asp:PlaceHolder ID="phCategoria" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="input-group">
                                <label class="input-group-addon">Categoria</label>
                                <asp:DropDownList ID="ddlCategoria" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <br />

                <asp:Label ID="lblErrorCarrera" ForeColor="Red" runat="server"></asp:Label>

                <br />

                <div class="input-group">
                    <div class="col-md-12">
                        <asp:Repeater ID="repeater" runat="server">
                            <HeaderTemplate>
                                <table id="tbl" cellpadding="1" cellspacing="0"
                                    border="0" class="display">
                                    <thead>
                                        <tr>
                                            <th class="Titulo">Nombres</th>
                                            <th class="Titulo">Paterno</th>
                                            <th class="Titulo">Materno</th>
                                            <th class="Titulo">Sexo</th>
                                            <th class="Titulo">Tiempo chip</th>
                                            <th class="Titulo">Lugar rama</th>
                                            <th class="Titulo">Vel</th>
                                            <th class="Titulo">Lugar general</th>
                                            <th class="Titulo">Imprime</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombres") %></td>
                                    <td><%# Eval("Paterno") %></td>
                                    <td><%# Eval("Materno") %></td>
                                    <td><%# Eval("Sexo") %></td>
                                    <td><%# Eval("T_Chip") %></td>
                                    <td><%# Eval("Lug_Rama") %></td>
                                    <td><%# Eval("Vel") %></td>
                                    <td><%# Eval("Lug_Gral") %></td>
                                    <td><a target="_blank" href="<%= URLRedirectImprimirCertificado %>?IdCarrera=<%# Eval("IdCarrera") %>&IdResultado=<%# Eval("IdResultado") %>">Imprime </a></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                        </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
