<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelFaturamento.aspx.cs" Inherits="simireports.RelFaturamento" %>

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
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">000 - Faturamento</div>
    <br />

        <p><%=sqlview %></p>
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosComissoes" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:100px;">Data</th>
                        <th style="width:100px;">Empresa</th>
                        <th style="width:100px;">Nota</th>
                        <th style="width:130px;">Item</th>
                        <th style="width:100px;">Nome Item</th>
                        <th style="width:100px;">CPF Cliente</th>
                        <th style="width:100px;">Nome Cliente</th>
                        <th style="width:120px;">Natureza</th>
                        <th style="width:100px;">Pedido</th>
                        <th style="width:100px;">Pedido Cliente</th>
                        <th style="width:100px;">Transportadora</th>
                    </tr>
                    <tr>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datInicio" autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:130px;" runat="server" type="text" id="empresa" autocomplete="off"/></td>
                        <td style="width:150px;"><input class="form-control" style="width:130px;" runat="server" type="text" id="nota" autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="codItem"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="descItem"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="cliente"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="nomCli"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="natureza"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="pedido"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="pedCli"  autocomplete="off"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="trans"  autocomplete="off"/></td>
                    </tr>
                </table>
                <br />
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarFat_Click" />
            </form>
        </div>

        <div style="color:white; margin-bottom:30px;">Total Faturamentos: R$ <%=totFat %></div>

        <div id="resultados">
            <font color=white>Mostrando <%=fats.Count%> resultados, de <%=postDatInicio%> a <%=postDatFim%></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width:100px;">Data</th>
                        <th style="width:100px;">Empresa</th>
                        <th style="width:100px;">Nota</th>
                        <th style="width:130px;">Item</th>
                        <th style="width:100px;">Nome Item</th>
                        <th style="width:100px;">Valor Unit</th>
                        <th style="width:100px;">Quantidade</th>
                        <th style="width:100px;">Valor Total</th>
                        <th style="width:120px;">Natureza</th>
                        <th style="width:100px;">Pedido</th>
                        <th style="width:100px;">Pedido Cliente</th>
                        <th style="width:100px;">Transportadora</th>
                </tr>
                
                <% 
                    string uCodRepres = "";
                    string uNomRepres = "";
                    Decimal totRepres = 0.0m;
                    string totRepresS = "";
                    

                    foreach (var fat in fats) {

                        

                        if(uCodRepres != "" && uCodRepres != codRepres)
                        {
                            totRepresS = m.formatarDecimal(totRepres);
                            %>
                                <tr><td colspan="4" style="background-color:black; color:white;"><b><% = uNomRepres %>(<% = uCodRepres %>) Total Comiss: R$ <% = totRepresS %></td><td colspan="6"></td><td colspan="4"></td></tr>
                            <%
                                    totRepres = 0.0m;
                                }
                                totRepres = totRepres + comiss;

                                uCodRepres = codRepres;
                                uNomRepres = nomRepres;
                                //valBruto = 100000000.00M;
                                string valBrutoS = m.formatarDecimal(valBruto);

                                string comissaoS = comiss.ToString();
                                comissaoS = m.pontoPorVirgula(comissaoS);

                                if(postDetalhes == 1) { 
                %> 
                    <tr>
                        <td style="text-align:center;"><%= codRepres %></td>
                        <td style="text-align:center;"><%= codEmpresa %></td>
                        <td style="text-align:center;"><%= numDocum %></td>
                        <td style="text-align:center;"><%= numDocumOrigem %></td>
                        <td style="text-align:center;"><%= numPedido %></td>
                        <td style="text-align:center;"><%= nomCliente %></td>
                        <td style="text-align:right;"><%= "R$ "+valBrutoS %></td>
                        <td style="text-align:center;"><%= pctComissao %></td>
                        <td style="text-align:right;"><%= "R$ "+comissaoS %></td>
                        <td style="text-align:center;"><%= nomRepres %></td>
                        <td style="text-align:center;"><%= datEmiss %></td>
                        <td style="text-align:center;"><%= datPgto %></td>
                        <td style="text-align:center;"><%= iesPgtoDocum %></td>
                    </tr>
                <%
                               }
                    }
                totRepresS = m.formatarDecimal(totRepres);
                %>
                <tr><td colspan="4" style="background-color:black; color:white;"><b><% = uNomRepres %>(<% = uCodRepres %>) Total Comiss: R$ <% = totRepresS %></td><td colspan="6"></td><td colspan="4"></td></tr>
            </table>
        </div>
    
</body>
</html>
