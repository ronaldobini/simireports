<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageDevolucoes.aspx.cs" Inherits="simireports.PageDevolucoes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Devoluções</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color:#222;">
    <a  title="Voltar ao Inicio" href=" index.aspx"><img style="margin:25px;" width="100px;" src="img/syss.png" /></a>
    <center>
    <h3><font color=white >Devoluções</font></h3>
        <br />
        
        <div id="filtros" style="margin-bottom:100px;">
            <form runat="server" id="filtrosDevolucoes" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:50px; text-align:center;">Unidade</th>
                        <th style="width:70px; text-align:center;">Nota</th>
                        <th style="width:70px; text-align:center;">NumAvisoRec</th>
                        <th style="width:100px; text-align:center;">Data Inicio</th>
                        <th style="width:100px; text-align:center;">Data Final</th>
                        <th style="width:100px; text-align:center;">CodItem</th>
                        <th style="width:50px; text-align:center;">CodOperacao</th>
                    </tr>
                    <tr>
                        <td><input style="width:50px; text-align:center;" runat="server" type="text" id="unidade"/></td>
                        <td><input style="width:70px; text-align:center;" runat="server" type="text" id="numNF"/></td>
                        <td><input style="width:70px; text-align:center;" runat="server" type="text" id="avisoRec"/></td>
                        <td><input style="width:100px; text-align:center;" runat="server" type="text" id="datIni" value="01/02/2019"/></td>
                        <td><input style="width:100px; text-align:center;" runat="server" type="text" id="datFim" value="28/02/2019"/></td>
                        <td><input style="width:100px; text-align:center;" runat="server" type="text" id="codItem"/></td>
                        <td><input style="width:50px; text-align:center;" runat="server" type="text" id="codOper" value="DEVC"/></td>
                        <td><input runat="server" type="submit" value="Filtrar" onserverclick="filtrarDevolucoes_Click" /></td>
                    </tr>
                </table>
            </form>
        </div>

        <div id="resultados">
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">Unidade</th>
                    <th style="width: 5%; text-align:center;">Num NF</th>
                    <th style="width: 5%; text-align:center;">Aviso Rec</th>
                    <th style="width: 10%; text-align:center;">Valor</th>
                    <th style="width: 10%; text-align:center;">Data Emiss</th>
                    <th style="width: 10%; text-align:center;">codItem</th>
                    <th style="width: 10%; text-align:center;">preUnit</th>
                    <th style="width: 5%; text-align:center;">codOper</th>
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
                        <td style="text-align:center;"><%= codEmpresa %></td>
                        <td style="text-align:center;"><%= numDocum %></td>
                        <td style="text-align:center;"><%= avisoRec %></td>
                        <td style="text-align:right;"><%= "R$ "+valorS %></td>
                        <td style="text-align:center;"><%= datEmiss %></td>
                        <td style="text-align:center;"><%= codItem %></td>
                        <td style="text-align:right;"><%= "R$ "+preUnitS %></td>
                        <td style="text-align:center;"><%= codOper %></td>
                    </tr>
                <%
                    }%>
            </table>
        </div>
    
</body>
</html>
