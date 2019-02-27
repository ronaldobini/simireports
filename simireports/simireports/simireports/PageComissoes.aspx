<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageComissoes.aspx.cs" Inherits="simireports.PageComissoes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Comissões</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color:#222;">
    <a  title="Voltar ao Inicio" href=" .aspx"><img style="margin:25px;" width="100px" src="img/syss.png" /></a>
    <center>
    <h3><font color=white>Comissões</font></h3>
        <br />
        
        <div id="filtros" style="margin-bottom:100px;">
            <form runat="server" id="filtrosComissoes" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:100px; text-align:center;">Data Inicio</th>
                        <th style="width:100px; text-align:center;">Data Fim</th>
                        <th style="width:100px; text-align:center;">Representante</th>
                        <th>Pgto</th>
                    </tr>
                    <tr>
                        <td><input style="width:100px; text-align:center;" runat="server" type="text" id="datInicio" value="01/02/2019"/></td>
                        <td><input style="width:100px; text-align:center;" runat="server" type="text" id="datFim" value="28/02/2019"/></td>
                        <td><input runat="server" type="text" id="repres"/></td>
                        <td><input style="width:35px; text-align:center;" runat="server" type="text" id="sitPgto" value="T"/></td>
                        <td><input runat="server" type="submit" value="Filtrar" onserverclick="filtrarComiss_Click" /></td>
                    </tr>
                </table>
            </form>
        </div>

        <div style="color:white; margin-bottom:30px;">Total Comiss: <%=totComiss %></div>

        <div id="resultados">
            <table class="table table-striped table-dark" style = "max-width:90%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">CodRepres</th>
                    <th style="width: 5%; text-align:center;">Empresa</th>
                    <th style="width: 10%; text-align:center;">N Docum</th>
                    <th style="width: 10%; text-align:center;">Origem</th>
                    <th style="width: 10%; text-align:center;">Pedido</th>
                    <th style="width: 10%; text-align:center;">Cliente</th>
                    <th style="width: 15%; text-align:center;">Valor Bruto</th>
                    <th style="width: 10%; text-align:center;">% Comiss</th>
                    <th style="width: 10%; text-align:center;">Comissao</th>
                    <th style="width: 20%; text-align:center;">Representante</th>
                    <th style="width: 20%; text-align:center;">Emissao</th>
                    <th style="width: 20%; text-align:center;">Dat. Pgto</th>
                    <th style="width: 5%; text-align:center;">Pagamento</th>
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
                        <td style="text-align:center;"><%= codRepres %></td>
                        <td style="text-align:center;"><%= codEmpresa %></td>
                        <td style="text-align:center;"><%= numDocum %></td>
                        <td style="text-align:center;"><%= numDocumOrigem %></td>
                        <td style="text-align:center;"><%= numPedido %></td>
                        <td style="text-align:center;"><%= nomCliente %></td>
                        <td style="text-align:right;"><%= valBruto %></td>
                        <td style="text-align:right;"><%= pctComissao %></td>
                        <td style="text-align:right;"><%= comiss %></td>
                        <td style="text-align:center;"><%= nomRepres %></td>
                        <td style="text-align:center;"><%= datEmiss %></td>
                        <td style="text-align:center;"><%= datPgto %></td>
                        <td style="text-align:center;"><%= iesPgtoDocum %></td>
                    </tr>
                <%                         
                    }
                %>
            </table>
        </div>
    
</body>
</html>
