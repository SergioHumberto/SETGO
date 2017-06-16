<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="WebApplicationTemplate.Web.Controls.Menu" %>
<%@ Import Namespace="WebApplicationTemplate.Web.Tools" %>

<nav class="navbar navbar-default" role="navigation">
    <!-- Logo -->
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse"
            data-target=".navbar-ex1-collapse">
            <span class="sr-only">Desplegar navegación</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a href="<%= Urls.Home() %>">
            <img class="navbar-brand" src="http://setgo.mx/wp-content/uploads/2016/05/Logo-trans2x-1.png" />
        </a>
    </div>

    <!-- Items -->
    <div class="collapse navbar-collapse navbar-ex1-collapse">
        <ul class="nav navbar-nav">
            <%--<li class="active"><a href="#">Enlace #1</a></li>--%>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Administración<b class="caret"></b>
                </a>
                <ul class="dropdown-menu">                    
                    <li><a href="<%= Urls.BuscaCarrera() %>">Carreras</a></li>
                    <li><a href="<%= Urls.Usuarios() %>">Usuarios</a></li>
                    <%--<li class="divider"></li>--%>                    
                </ul>
            </li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Registro<b class="caret"></b>
                </a>
                <ul class="dropdown-menu">
                    <li><a href="<%= Urls.RegistroParticipantes() %>">Registro (Sin iFrame)</a></li>        
                    <li><a href="<%= Urls.TestIFrame() %>">Registro (Con iFrame)</a></li>                                 
                    <%--<li class="divider"></li>--%>                    
                </ul>
            </li>
            <li><a href="<%= Urls.ReporteRegistrados() %>">Reporte Registrados</a></li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Resultados<b class="caret"></b>
                </a>
                <ul class="dropdown-menu">
                    <li><a href="<%= Urls.CargarResultados() %>">Cargar</a></li>   
                    <li><a href="<%= Urls.ConsultaResultadosIFrame()%>">Consulta</a></li>                                 
                    <%--<li class="divider"></li>--%>                    
                </ul>
            </li>             
            <li><a href="<%= Urls.SignOut() %>">Salir</a></li>
        </ul>
    </div>
</nav>
