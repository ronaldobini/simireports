<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelDevolucoes.aspx.cs" Inherits="simireports.PageDevolucoes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Devoluções</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color:#222;">
   <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">004 - Devoluções</div>
    <br />
        
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosDevolucoes" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th>Unidade</th>
                        <th>Nota</th>
                        <th>NumAvisoRec</th>
                        <th>CodItem</th>
                        <th>Data Inicio</th>
                        <th>Data Final</th>
                        <th>CodOperacao</th>
                    </tr>
                    <tr>
                        <td style="width:120px;">
                            <select class="form-control" style="width:100px;" id="unidade" runat="server">
                                <option value="">Todas</option>
                                <option value="2">02</option>
                                <option value="3">03</option>
                                <option value="4">04</option>
                                <option value="5">05</option>
                                <option value="6">06</option>
                            </select>
                        </td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="numNF"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="avisoRec"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codItem"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datIni"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codOper" value="DEVC"/></td>
                    </tr>
                </table>
                <br>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarDevolucoes_Click" />
            </form>
        </div>

        <div id="resultados">
            <font color=white>Mostrando <%=devolucoes.Count%> resultados, de <%=dataPesqIni%> a <%=dataPesqFim%></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 10%; text-align:center;">Data Emiss</th>
                    <th style="width: 5%; text-align:center;">Unidade</th>
                    <th style="width: 5%; text-align:center;">Num NF</th>
                    <th style="width: 5%; text-align:center;">Aviso Rec</th>
                    <th style="width: 10%; text-align:center;">Cod. Item</th>
                    <th style="width: 10%; text-align:center;">Pre Unit</th>
                    <th style="width: 10%; text-align:center;">Valor</th>
                </tr>
                
                <% 
                    foreach (var devolucao in devolucoes) {
                        string codEmpresa = devolucao.Empresa;
                        string numDocum = devolucao.NumDocum;
                        string avisoRec = devolucao.AvisoRec;
                        Decimal valor = Decimal.Round(devolucao.Valor,2);
                        String valorS = m.formatarDecimal(valor);
                        DateTime datEmiss = devolucao.DataEmis;
                        string codItem = devolucao.CodItem;
                        Decimal preUnit = Decimal.Round(devolucao.PreUnit,2);
                        String preUnitS = m.formatarDecimal(preUnit);
                        string codOper = devolucao.CodOper;
                %> 
                    <tr>
                        <td style="text-align:center;"><%= datEmiss %></td>
                        <td style="text-align:center;"><%= codEmpresa %></td>
                        <td style="text-align:center;"><%= numDocum %></td>
                        <td style="text-align:center;"><%= avisoRec %></td>
                        <td style="text-align:center;"><%= codItem %></td>
                        <td style="text-align:right;"><%= "R$ "+preUnitS %></td>
                        <td style="text-align:right;"><%= "R$ "+valorS %></td>
                    </tr>
                <%
                    }%>
            </table>
        </div>
    
</body>
</html>
