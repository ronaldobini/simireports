<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagePedidosEfetivadosCRM.aspx.cs" Inherits="simireports.PagePedidosEfetivadosCRM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Pedidos Efetivados CRM</title>
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
            if (first == 1) {
                var inicio = document.getElementById('datIni');
                inicio.value = "<%=ontem %>";

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
        <a  title="Voltar ao Inicio" href=" index.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">Pedidos Efetivados CRM</div>
    <br />
    <p><%=sqlview %></p>
       
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosPedEfet" action="#" method="post">
                
                <table>
                    <tr style="color:white;">
                        <th>Unidade</th>
                        <th>CNPJ</th>
                        <th>Nome Cliente</th>
                        <th>Representante</th>
                        <th>Pedido</th>
                        <th>Cod. do Item</th>
                        <th>Data Inicio</th>
                        <th>Data Fim</th>
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
                    </tr>
                </table>
                <br>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarPedEfet_Click" />
            </form>
        </div>

        <div id="resultados">
            
            <font color=white>Mostrando <%=pedsEfets.Count%> resultados, de <%=postDatInicio %> a <%=postDatFim %> - Total R$ <%=totGeralS %></font><br/>
            <table class="table table-hover table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                
                
                <% 
                    Decimal totPed = 0.0m;
                    string totPedS = "";
                    foreach (var pedEfet in pedsEfets) {

                     %> 
                        <thead style="background-color: #070a0e; color:white;">
                            <tr>
                                <th scope="col" style="width: 5%; text-align:center;">Data</th>
                                <th scope="col" style="width: 5%; text-align:center;">Unidade</th>
                                <th scope="col"style="width: 10%; text-align:center;">Pedido</th>
                                <th scope="col" style="width: 5%; text-align:center;">CNPJ</th>
                                <th scope="col" style="width: 10%; text-align:center;">Cliente</th>
                                <th scope="col" style="width: 10%; text-align:center;">Representante</th>
                            </tr>
                        </thead>
                     <%
                        string codEmpresa = pedEfet.CodEmpresa;
                        DateTime dat = pedEfet.Dat;
                        string codCliente = pedEfet.CodCliente;
                        String numPed = pedEfet.NumPed;
                        string cliente = pedEfet.Cliente;
                        string repres = pedEfet.Repres;
                     %> 
                        <tr>
                            <td style="text-align:center;"><b><%= dat %></b></td>
                            <td style="text-align:center;"><b><%= codEmpresa %></b></td>
                            <td style="text-align:center;"><b><%= numPed %></b></td>
                            <td style="text-align:center;"><b><%= codCliente %></b></td>
                            <td style="text-align:center;"><b><%= cliente %></b></td>
                            <td style="text-align:center;"><b><%= repres %></b></td>
                        </tr>
                        <tr>
                            <td colspan ="6">
                                <table class="table table-sm table-dark" style="background-color:#3f4142; width:100%; color:white; font-size: 12px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 10%;">Cod. do Item</th>
                                            <th style="width: 5%;">Solic</th>
                                            <th style="width: 5%;">Cancel</th>
                                            <th style="width: 5%;">Atend</th>
                                            <th style="width: 55%;">Desc. Item</th>
                                            <th style="width: 10%;">Preço Unit</th>
                                            <th style="width: 20%;">Prazo</th>
                                        </tr>
                                    </thead>
                                    <%
                                    totPed = 0.0m;
                                        totPedS = m.formatarDecimal(totPed);
                                        foreach (var item in pedEfet.Itens)
                                        {
                                            string codItem = item.CodItem;
                                            string qtdSolic = item.QtdSolic;
                                            qtdSolic = m.pontoPorVirgula(qtdSolic);
                                            Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic),0);

                                            string qtdCancel = item.QtdCancel;
                                            string qtdAtend = item.QtdAtend;
                                            string nomeItem = item.NomeItem;
                                            string przEntrega = item.PrzEntrega;

                                            Decimal preUnit = Decimal.Round(item.PrecoUnit,2);
                                            String preUnitS = m.formatarDecimal(preUnit);

                                            totPed += preUnit*qtdSolicD;
                                            totPedS = m.formatarDecimal(totPed);   
                                    %>
                                            <tr>
                                                <td><%= codItem %></td>
                                                <td><%= qtdSolic %></td>
                                                <td><%= qtdCancel %></td>
                                                <td><%= qtdAtend %></td>
                                                <td><%= nomeItem %></td>
                                                <td><%= "R$"+preUnitS %></td>
                                                <td><%= przEntrega %></td>
                                            </tr>
                                                
                                    <% } %>
                                    <tr>
                                        <td colspan="7" style="background-color: #070a0e; color:white;"><b>Total Pedido: R$ <%= totPedS %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                                <%
                                    } totGeralS = m.formatarDecimal(totGeral);
                                %>
            </table>
        </div>
    
</body>
</html>
