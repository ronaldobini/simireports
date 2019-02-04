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
        
        <div id="filtros" style="margin-bottom:100px;">
            <form runat="server" id="filtrosComissoes" action="#" method="post">
                <input runat="server" type="text" id="datInicio" placeholder="Data Ini" value="01/01/2019"/>
                <input runat="server" type="text" id="datFim" placeholder="Data Fim" value="31/01/2019"/>
                <input runat="server" type="text" id="repres" placeholder="Repres"/>
                <input runat="server" type="text" id="sitPgto" placeholder="Pgto" value="T"/>
                
                <input runat="server" type="submit" value="Filtrar" onserverclick="filtrarComiss_Click" />
            </form>
        </div>

        <div style="color:white; margin-bottom:30px;">Total Comiss: <%=totComiss %></div>

        <div id="resultados">
            <table class="table table-striped table-dark" style = "max-width:90%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%;">CodRepres</th>
                    <th style="width: 5%;">Empresa</th>
                    <th style="width: 10%;">N Documento</th>
                    <th style="width: 10%;">Origem</th>
                    <th style="width: 10%;">Pedido</th>
                    <th style="width: 10%;">Cliente</th>
                    <th style="width: 20%;">Valor Bruto</th>
                    <th style="width: 5%;">% Comissao</th>
                    <th style="width: 20%;">Comissao</th>
                    <th style="width: 20%;">Representante</th>
                    <th style="width: 20%;">Emissao</th>
                    <th style="width: 20%;">Dat. Pgto</th>
                    <th style="width: 5%;">Pagamento</th>
                </tr>
                
                <% 
                    string uCodRepres = "";
                    string uNomRepres = "";
                    Decimal totRepres = 0.0m;

                    foreach (var comissao in comissoes) {
                        string codEmpresa = comissao.CodEmpresa;
                        string numDocum = comissao.NumDocum;
                        string numDocumOrigem = comissao.NumDocumOrigem;
                        string numPedido = comissao.NumPedido;
                        string nomCliente = comissao.NomCliente;
                        Decimal valBruto = Decimal.Round(comissao.ValBruto,2);
                        Decimal pctComissao = Decimal.Round(comissao.PctComissao,2);
                        Decimal comiss = Decimal.Round(comissao.Comiss,2);
                        string nomRepres = comissao.NomRepres;
                        DateTime datEmiss = comissao.DatEmiss;
                        DateTime datPgto = comissao.DatPgto;
                        char iesPgtoDocum = comissao.IesPgtoDocum;
                        string codRepres = comissao.CodRepres;

                        totRepres = totRepres + comiss;
                        if(uCodRepres != "" && uCodRepres != codRepres)
                        {
                            %>
                                <tr><td colspan="4" style="background-color:black; color:white;"><b><% = uNomRepres %>(<% = uCodRepres %>) Total Comiss: <% = totRepres %></td><td colspan="6"></td><td colspan="4"></td></tr>
                            <%
                            totRepres = 0.0M;
                        }
                        uCodRepres = codRepres;
                        uNomRepres = nomRepres;
                        
                %> 
                    <tr>
                        <td><%= codRepres %></td>
                        <td><%= codEmpresa %></td>
                        <td><%= numDocum %></td>
                        <td><%= numDocumOrigem %></td>
                        <td><%= numPedido %></td>
                        <td><%= nomCliente %></td>
                        <td><%= valBruto %></td>
                        <td><%= pctComissao %></td>
                        <td><%= comiss %></td>
                        <td><%= nomRepres %></td>
                        <td><%= datEmiss %></td>
                        <td><%= datPgto %></td>
                        <td><%= iesPgtoDocum %></td>
                    </tr>
                <%                         
                    }
                %>
            </table>
        </div>
    
</body>
</html>
