<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PrivatePage.Master" AutoEventWireup="true" CodeBehind="ReporteRegistrados.aspx.cs" Inherits="WebApplicationTemplate.Web.Pages.ReporteRegistrados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
                    "sScrollX": "90%",
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


    <h3>Reporte de participantes registrados</h3>

    <br />

    <div class="input-group" style="width:60%">
        <label>Carrera</label>
        <asp:DropDownList CssClass="form-control" OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged" ID="ddlCarrera" AutoPostBack="true" runat="server">
        </asp:DropDownList>
    </div>
    

    <br />

    <asp:Button ID="btnGenerar" Text="Generar reporte" CssClass="btn btn-default" OnClick="btnGenerar_Click" runat="server" />

    <br />
    <br />
    <br />

   <div class="input-group">
        <div style="width: 27%;">
            <asp:Repeater ID="repeater" runat="server">
                <HeaderTemplate>
                    <table id="tbl" cellpadding="1" cellspacing="0"
                        border="0" class="display">
                        <thead>
                            <tr>
                                <th class="Titulo">Folio</th>
                                <th class="Titulo">Nombre</th>
                                <th class="Titulo">Paterno</th>
                                <th class="Titulo">Materno</th>
                                <th class="Titulo">Fecha nacimiento</th>
                                <th class="Titulo">Domicilio</th>
                                <th class="Titulo">Invitado</th>
                                <th class="Titulo">Numero accion</th>
                                <th class="Titulo">Telefono</th>
                                <th class="Titulo">Email</th>
                                <th class="Titulo">Telefono emergencia</th>
                                <th class="Titulo">Rama</th>
                                <th class="Titulo">Categoria</th>
                                <th class="Titulo">Ruta</th>
                                <th class="Titulo">TransactionID</th>
                                <th class="Titulo">Status Paypal</th>
                                <th class="Titulo">Generic01</th>
                                <th class="Titulo">Generic02</th>
                                <th class="Titulo">Generic03</th>
                                <th class="Titulo">Generic04</th>
                                <th class="Titulo">Generic05</th>
                                <th class="Titulo">Generic06</th>
                                <th class="Titulo">Generic07</th>
                                <th class="Titulo">Generic08</th>
                                <th class="Titulo">Generic09</th>
                                <th class="Titulo">Generic10</th>
                                <th class="Titulo">Clasificaciones</th>
                                <th class="Titulo">Codigo pago</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Folio") %></td>
                        <td><%# Eval("Nombre") %></td>
                        <td><%# Eval("ApellidoPaterno") %></td>
                        <td><%# Eval("ApellidoMaterno") %></td>
                        <td><%# Eval("FechaNacimiento") %></td>
                        <td><%# Eval("Domicilio") %></td>
                        <td><%# Eval("Invitado") %></td>
                        <td><%# Eval("NumeroAccion") %></td>
                        <td><%# Eval("Telefono") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("TelefonoEmergencia") %></td>
                        <td><%# Eval("Rama") %></td>
                        <td><%# Eval("Categoria") %></td>
                        <td><%# Eval("Ruta") %></td>
                        <td><%# Eval("TransactionNumber") %></td>
                        <td><%# Eval("StatusPaypal") %></td>
                        <td><%# Eval("Generic01") %></td>
                        <td><%# Eval("Generic02") %></td>
                        <td><%# Eval("Generic03") %></td>
                        <td><%# Eval("Generic04") %></td>
                        <td><%# Eval("Generic05") %></td>
                        <td><%# Eval("Generic06") %></td>
                        <td><%# Eval("Generic07") %></td>
                        <td><%# Eval("Generic08") %></td>
                        <td><%# Eval("Generic09") %></td>
                        <td><%# Eval("Generic10") %></td>
                        <td><%# Eval("Clasificaciones") %></td>
                        <td><%# Eval("CodigoPago") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
