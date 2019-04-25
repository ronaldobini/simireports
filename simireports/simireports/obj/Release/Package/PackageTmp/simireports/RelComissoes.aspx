<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelComissoes.aspx.cs" Inherits="simireports.PageComissoes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

    <script>
        //onload
        function onload() {

            //bloqueia campo repres
            var represChange = "<%=represChange %>";
            if (represChange != "sim") {
                var repres = document.getElementById('repres');
                repres.disabled = true;
                repres.value = "<%=postRepres %>";
            }

            //first
            var first = <%=Session["firstJ"] %>;
            if (first == "1") {
                var inicio = document.getElementById('datInicio');
                inicio.value = "<%=mesPassado %>";

                var fim = document.getElementById('datFim');
                fim.value = "<%=hoje %>";

                var repres = document.getElementById('repres');
                repres.value = "<%=postRepres %>";
            }
        }

       
    </script>

</head>
<body style="background-color:#222;" onload="onload();">
     <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">001 - Comissões</div>
    <br />

        <p><%=sqlview %></p>
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosComissoes" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th style="width:100px;">Unidade</th>
                        <th style="width:100px;">Cliente</th>
<%--                        <th style="width:100px; text-align:center;">% Comiss</th>
                        <th style="width:100px; text-align:center;">Valor</th>--%>
                        <th style="width:130px;">Representante </th>
                        <th style="width:100px;">Data Inicio</th>
                        <th style="width:120px;">Data Fim</th>
                        <th style="width:100px;">Situação</th>
                        <th style="width:100px;text-align:center;">Detalhes</th>
                    </tr>
                    <tr>
                        <td style="width:120px;">
                            <select class="form-control" style="width:100px;" id="unidade" runat="server">
                                <option value="">Todas</option>
                                <option value="2">02</option>
                                <option value="4">04</option>
                            </select>
                        </td>
                        <td style="width:150px;"><input class="form-control" style="width:130px;" runat="server" type="text" id="cliente" autocomplete="off"/></td>
<%--                        <td><input runat="server" type="text" id="pctComiss"/></td>
                        <td><input runat="server" type="text" id="valor"/></td>--%>
                        <td style="width:120px;"><input class="form-control" style="width:110px;" runat="server" type="text" id="repres"  autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datInicio" autocomplete="off"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"  autocomplete="off"/></td>
                        <td style="width:100px;">
                            <select class="form-control" style="width:100px;" id="sitPgto" runat="server">                                
                                <option value="T">Pago</option>
                                <option value="A">Aberto</option>
                            </select>
                        </td>
                        <td style="text-align:center"><input type="checkbox" id="detalhes" value="Detalhes" runat="server"> </td>
                    </tr>
                </table>
                <br />
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarComiss_Click" />
            </form>
        </div>

        <div style="color:white; margin-bottom:30px;">Total Comiss: R$ <%=totComissS %></div>

        <div id="resultados">
            <font color=white>Mostrando <%=comissoes.Count%> resultados, de <%=postDatInicio%> a <%=postDatFim%></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">CodRepres</th>
                    <th style="width: 5%; text-align:center;">Unidade</th>
                    <th style="width: 15%; text-align:center;">N Docum</th>
                    <th style="width: 5%; text-align:center;">Origem</th>
                    <th style="width: 5%; text-align:center;">Pedido</th>
                    <th style="width: 20%; text-align:center;">Cliente</th>
                    <th style="width: 10%; text-align:center;">Valor Bruto</th>
                    <th style="width: 10%; text-align:center;">% Comiss</th>
                    <th style="width: 5%; text-align:center;">Comissao</th>
                    <th style="width: 10%; text-align:center;">Representante</th>
                    <th style="width: 20%; text-align:center;">Emissao</th>
                    <th style="width: 20%; text-align:center;">Dat. Pgto</th>
                    <th style="width: 5%; text-align:center;">Pgto</th>
                </tr>
                
                <% 
                    string uCodRepres = "";
                    string uNomRepres = "";
                    Decimal totRepres = 0.0m;
                    string totRepresS = "";
                    

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
