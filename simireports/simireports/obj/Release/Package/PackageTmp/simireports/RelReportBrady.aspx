<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelReportBrady.aspx.cs" Inherits="simireports.RelReportBrady" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

</head>
<body style="background-color:#222;" onload="onload();">
     <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">008 - Reporte Brady</div>
    <br />
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosReports" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:100px;">Destino</th>
                        <th style="width:100px;">Data Inicio</th>
                        <th style="width:120px;">Data Fim</th>
                    </tr>
                    <tr>
                        <td style="width:150px;"><input class="form-control" style="width:130px;" runat="server" type="text" id="destino" autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datInicio" autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"  autocomplete="off"/></td>
                    </tr>
                </table>
                <br />
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarReportClick" />
            </form>
        </div>

        <div style="color:white; margin-bottom:30px;">Total Reportes: R$ <%=totNotaS %></div>

        <div id="resultados">
            <font color=white>Mostrando <%=reports.Count%> resultados, de <%=postDatInicio%> a <%=postDatFim%></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">Cliente</th>
                    <th style="width: 5%; text-align:center;">CNPJ</th>
                    <th style="width: 5%; text-align:center;">Valor</th>
                    <th style="width: 5%; text-align:center;">Pedido Logix</th>
                </tr>
                
                <% 
                    foreach (var rep in reports) {
                        %>
                    <tr>
                        <td style="text-align:center;"><%= rep.Cliente %></td>
                        <td style="text-align:center;"><%= rep.CodCliente %></td>
                        <td style="text-align:center;"><%= m.formatarDecimal(rep.Valor) %></td>
                        <td style="text-align:center;"><%= rep.Pedido %></td>
                    </tr>
                <%
                    }
                %>
            </table>
        </div>
</body>
</html>

