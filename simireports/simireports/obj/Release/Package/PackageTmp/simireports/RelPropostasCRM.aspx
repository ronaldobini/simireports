<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelPropostasCRM.aspx.cs" Inherits="simireports.PagePedidosEfetivadosCRM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                repres.value = "<%=Session["nome"] %>";
            }

            //first
            var first = <%=Session["firstJ"] %>;
            if (first == 1) {
                var inicio = document.getElementById('datIni');
                inicio.value = "<%=hoje %>";

                var fim = document.getElementById('datFim');
                fim.value = "<%=hoje %>";

                var repres = document.getElementById('repres');
            }
        }


    </script>
</head>
<body style="background-color: #222;" onload="onload();">
    <div id="logo" style="margin-left: 20px; float: left;">
        <a title="Voltar ao Inicio" href=" Relatorios.aspx">
            <img style="width: 50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">003 - Propostas CRM</div>
    <br />
    <p><%//=sqlview %></p>
       
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosPedEfet" action="#" method="post">
                
                <table>
                    <tr style="color:white;">
                        <th>Unidade</th>
                        <th>CNPJ</th>
                        <th>Nome Cliente</th>
                        <th>Representante</th>
                        <th>Cod. Prop.</th>
                        <th>Cod. do Item</th>
                        <th>Data Inicio</th>
                        <th>Data Fim</th>
                        <th>Class</th>

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
                        <td style="width:150px;"><input class="form-control" style="width:130px; text-align:center;" runat="server" type="text" id="codCliente"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="cliente"/></td>
                        <td style="width:130px;"><input class="form-control" style="width:110px; text-align:center;" runat="server" type="text" id="repres"/></td>
                        <td style="width:100px;"><input class="form-control" style="width:80px; text-align:center;" runat="server" type="text" id="numPed"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codItem"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datIni"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"/></td>
                        <td style="width:120px;">
                            <select class="form-control" style="width:100px;" id="classprop" runat="server">
                                <option value="1">Todas</option>
                                <option value="2">AAC</option>
                                <option value="3">AMC</option>
                                <option value="4">ABC</option>                                
                                <option value="5">Fechadas</option>
                            </select>
                        </td>
                    </tr>
                </table>
                <br>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarPedEfet_Click" />
            </form>
        </div>

        <div id="resultados">
            
            <font color=white>Mostrando <%=pedsEfets.Count%> resultados, de <%=m.configDataBanco2Human(postDatInicio) %> a <%=m.configDataBanco2Human(postDatFim) %> - Total R$ <%=totGeralS %></font><br/>
                    
        <font color=white>Carregamento: <%= demora.Seconds%>.<%= demora.Milliseconds%>s</font><br/>
            <table class="table table-hover table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                
                
                <% 
                    Decimal totPed = 0.0m;
                    string totPedS = "";
                    string totAtendS = "";

                    foreach (var pedEfet in pedsEfets)
                    {

                     %> 
                        <thead style="background-color: #070a0e; color:white;">
                            <tr>
                                <th scope="col" style="width: 5%; text-align:center;">Data</th>
                                <th scope="col" style="width: 5%; text-align:center;">Unidade</th>
                                <th scope="col"style="width: 10%; text-align:center;">Proposta</th>
                                <th scope="col" style="width: 5%; text-align:center;">CNPJ</th>
                                <th scope="col" style="width: 10%; text-align:center;">Cliente</th>
                                <th scope="col" style="width: 10%; text-align:center;">Representante</th>
                                <th scope="col" style="width: 10%; text-align:center;">Class.</th>
                            </tr>
                        </thead>
                     <%
                         string codEmpresa = pedEfet.CodEmpresa;
                         DateTime dat = pedEfet.Dat;
                         string codCliente = pedEfet.CodCliente;
                         String numPed = pedEfet.NumPed;
                         string cliente = pedEfet.Cliente;
                         string repres = pedEfet.Repres;
                         string clprop = pedEfet.Clprop;

                     %> 
                        <tr>
                            <td style="text-align:center;"><b><%= dat %></b></td>
                            <td style="text-align:center;"><b><%= codEmpresa %></b></td>
                            <td style="text-align:center;"><b><%= cliente %></b></td>
                            <td style="text-align:center;"><b><%= codCliente %></b></td>
                            <td style="text-align:center;"><b><%= numPed %></b></td>
                            <td style="text-align:center;"><b><%= repres %></b></td>
                            <td style="text-align:center;"><b><%= clprop %></b></td>
                        </tr>
                        <tr>
                            <td colspan ="8">
                                <table class="table table-sm table-dark" style="background-color:#3f4142; width:100%; color:white; font-size: 12px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 10%;">Cod. do Item</th>
                                            <th style="text-align:left;width: 30%;">Desc. Item</th>
                                            <th  style="text-align:center;width: 5%;">Qtd</th>
                                            <th style="text-align:right;width: 10%;">Preço Unit (R$)</th>
                                            <th style="text-align:right;width: 10%;">Total (R$)</th>
                                            <th style="text-align:center;width: 10%;">Desconto (%)</th>
                                            <th style="text-align:center;width: 10%;">Comissão (%)</th>
                                            <th style="text-align:center;width: 15%;">Prazo</th>
                                            <%--<th style="text-align:center;width: 30%;">Prazo</th>--%>
                                        </tr>
                                    </thead>
                                    <%
                                        totPed = 0.0m;
                                        totPedS = m.formatarDecimal(totPed);
                                        totAtendS = m.formatarDecimal(totPed);
                                        foreach (var item in pedEfet.Itens)
                                        {
                                            string codItem = item.CodItem;
                                            int qtdSolic = item.QtdSolic;

                                            string nomeItem = item.NomeItem;
                                            string przEntrega = item.PrzEntrega;

                                            Decimal preUnit = Decimal.Round(item.PrecoUnit,2);
                                            String preUnitS = m.formatarDecimal(preUnit);

                                            Decimal totItem = preUnit * qtdSolic;
                                            String totItemS = m.formatarDecimal(totItem);

                                            Decimal desconto = item.Desconto;
                                            Decimal comiss = item.Comiss;
                                            int pedLogix = item.PedLogix;

                                            totPed = totPed + totItem;
                                            totPedS = m.formatarDecimal(totPed);

                                    %>
                                            <tr>
                                                <td><%= codItem %></td>
                                                <td style="text-align:left;"><%= nomeItem %></td>
                                                <td style="text-align:center;"><%= qtdSolic %></td>
                                                <td style="text-align:right;"><%= preUnitS %></td>
                                                <td style="text-align:right;"><%= totItemS %></td>
                                                <td style="text-align:center;"><%= Math.Round(desconto*100,0) %></td>
                                                <td style="text-align:center;"><%= comiss %></td>
                                                <td style="text-align:center;"><%= przEntrega %></td>
                                                <%--<td><%= przEntrega %></td>--%>
                                            </tr>
                                                
                                    <% } %>
                                    <tr>
                                        <td colspan="8" style="background-color: #070a0e; color:white;"><b>Total Proposta: R$ <%= totPedS %></td>
                                        <%--<td colspan="2" style="background-color: #070a0e; color:white;"><div style="float:left;"><b>Total Atendido: R$ <%= totAtendS %></div>
                                            <div style="float:right;">Total Cancelado: R$ <%= totCancelS %></div></td>--%>
                                        <%--<td colspan="2" style="background-color: #070a0e; color:white;"><b>Total Cancelado: R$ <%= totCancelS %></td>--%>
                                        <%--<td colspan="2" style="background-color: #070a0e; color:white;text-align:right;"><b>Total Pendente: R$ <%= totPendS %></td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                                <%
                                    }
                                    totGeralS = m.formatarDecimal(totGeral);
                                    
                                %>
            </table>
        </div>
        <%            /*dtFim = DateTime.Now;
                      demora = dtFim - dtInicio;*/ %>
        <font color=white>Carregamento: <%= demora.Seconds%>.<%= demora.Milliseconds%>s</font><br/>
</body>
</html>



<%=errosql %>
<br /><br />
<%=sqlview %>
