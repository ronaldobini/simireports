<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageComissoes.aspx.cs" Inherits="simireports.PageComissoes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Comissões</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color:#222;">
    <center><br />
    <h3><font color=white>Comissões</font></h3>
        <br />
        
        <div id="filtros">
            <form id="form1" runat="server">
            </form>
        </div>

        <div id="resultados">
            <table class="table table-striped table-dark" style = "max-width:90%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 10%;">Nota Fiscal</th>
                    <th style="width: 10%;">N Pedido</th>
                    <th style="width: 10%;">Item</th>
                    <th style="width: 10%;">Nome cliente</th>
                    <th style="width: 10%;">Desc Item</th>
                    <th style="width: 20%;">Qtd Item</th>
                    <th style="width: 20%;">Preco Unit Bruto</th>
                    <th style="width: 20%;">PreTotal</th>
                    <th style="width: 20%;">% Comissao</th>
                    <th style="width: 20%;">Comissao</th>
                    <th style="width: 20%;">Nome Representante</th>
                    <th style="width: 20%;">Dat Alt Sit</th>
                    <th style="width: 20%;">Data Hora Emiss</th>
                    <th style="width: 10%;">-</th>
                </tr>
                
                <% 
                    foreach (var comissao in comissoes) {
                        int notaFiscal = comissao.NotaFiscal;
                        int numPed = comissao.NumPed;
                        string item = comissao.Item;
                        string nomCliente = comissao.NomCliente;
                        string desItem = comissao.DesItem;
                        string qtdItem = comissao.QtdItem;
                        string precoUnitBruto = comissao.PrecoUnitBruto;
                        double preTotal = comissao.PreTotal;
                        string pctComissao = comissao.PctComissao;
                        double comiss = comissao.Comiss;
                        string nomRepres = comissao.NomRepres;
                        DateTime datAltSit = comissao.DatAltSit;
                        DateTime datHorEmiss = comissao.DatHorEmiss;
                %> 

                    <tr>
                        <td><%= notaFiscal %></td>
                        <td><%= numPed %></td>
                        <td><%= item %></td>
                        <td><%= nomCliente %></td>
                        <td><%= desItem %></td>
                        <td><%= qtdItem %></td>
                        <td><%= precoUnitBruto %></td>
                        <td><%= preTotal %></td>
                        <td><%= pctComissao %></td>
                        <td><%= comiss %></td>
                        <td><%= nomRepres %></td>
                        <td><%= datAltSit %></td>
                        <td><%= datHorEmiss %></td>
                        <td></td>
                    </tr>
              
                <%  } %>
            </table>
        </div>
    
</body>
</html>
