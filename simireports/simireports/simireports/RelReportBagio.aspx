<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelReportBagio.aspx.cs" Inherits="simireports.RelReportBagio" %>

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
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">009 - Reporte Baggio</div>
    <br />
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosReports" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:100px;">Chance</th>
                        <%--<th style="width:100px;">Valor menor que</th>--%>
                        <th style="width:120px;">Valor maior que</th>
                        <th style="width:100px;">Data Inicio</th>
                        <th style="width:120px;">Data Fim</th>
                    </tr>
                    <tr>
                        <td style="width:120px;">
                            <select class="form-control" style="width:100px;" id="chance" runat="server">
                                <option value="Todas">Todas</option>
                                <option value="AAC">AAC</option>
                                <option value="AMC">AMC</option>
                                <option value="ABC">ABC</option>
                            </select>
                        </td>
<%--                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="valorMenor" autocomplete="off"/></td>--%>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="valorMaior"  autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datInicio" autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"  autocomplete="off"/></td>
                   </tr>
                </table>
                <br />
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarReportClick" />
            </form>
        </div>

        <%--<div style="color:white; margin-bottom:30px;">Total Reportes: R$ <%=totNotaS %></div>--%>

        <div id="resultados">
            <font color=white>Mostrando <%=dataEm.Count%> resultados</font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">Data Em</th>
                    <th style="width: 5%; text-align:center;">CLProp</th>
                    <th style="width: 5%; text-align:center;">CodProp</th>
                    <th style="width: 5%; text-align:center;">Cliente</th>
                    <th style="width: 5%; text-align:center;">Contato</th>
                    <th style="width: 5%; text-align:center;">DPTO</th>
                    <th style="width: 5%; text-align:center;">Email</th>
                    <th style="width: %; text-align:center;">Telefone</th>
                    <th style="width: 5%; text-align:center;">CPF</th>
                    <th style="width: 5%; text-align:center;">Endereço</th>
                    <th style="width: 5%; text-align:center;">Cidade</th>
                    <th style="width: 5%; text-align:center;">Item</th>
                    <th style="width: 5%; text-align:center;">Item Reduz</th>
                    <th style="width: 5%; text-align:center;">QTD</th>
                    <th style="width: 5%; text-align:center;">Repres</th>
                </tr>
                
                <% 
                    for(int i = 0; i < dataEm.Count; ++i) {
                        %>
                    <tr>
                        <td style="text-align:center;"><%= dataEm[i] %></td>
                        <td style="text-align:center;"><%= clProp[i] %></td>
                        <td style="text-align:center;white-space:nowrap;"><%= codProp[i] %></td>
                        <td style="text-align:center;"><%= nomCliente[i] %></td>
                        <td style="text-align:center;"><%= nomContato[i] %></td>
                        <td style="text-align:center;"><%= depto[i] %></td>
                        <td style="text-align:center;white-space:nowrap;"><%= email[i] %></td>
                        <td style="text-align:center;white-space:nowrap;"><%= telefone[i] %></td>
                        <td style="text-align:center;white-space:nowrap;"><%= cpf[i] %></td>
                        <td style="text-align:center;"><%= endCliente[i] %></td>
                        <td style="text-align:center;"><%= cidade[i] %></td>
                        <td style="text-align:center;"><%= item[i] %></td>
                        <td style="text-align:center;"><%= itemReduz[i] %></td>
                        <td style="text-align:center;"><%= qtd[i] %></td>
                        <td style="text-align:center;"><%= repres[i] %></td>
                    </tr>
                <%
                    }
                %>
            </table>
        </div>
</body>
</html>

