<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagePedidosEfetivados.aspx.cs" Inherits="simireports.PagePedidosEfetivados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Pedidos Efetivados</title>
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

                <% Session["firstJ"] = 0; %>
                <% Session["first"] = 1; %>
            }
        }

        //reset
        function resetFirst(){
            <% Session["firstJ"] = 1; %>
        }
    </script>
</head>
<body style="background-color:#222;">
    <a  title="Voltar ao Inicio" href=" index.aspx" onclick="resetFirst();"><img style="margin:25px;" width="50px;" src="img/syss.png" /></a>
    <center>
    <h3><font color=white >Pedidos Efetivados</font></h3>
        <br />
        <p><%=sqlview %></p>
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosPedEfet" action="#" method="post">
                <table>
                    <tr style="color:white;">
                        <th>Unidade</th>
                        <th>CodCliente</th>
                        <th>Cliente</th>
                        <th>Representante</th>
                        <th>NumPed</th>
                        <th>CodItem</th>
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
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codCliente"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="cliente"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="repres"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="numPed"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codItem"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="datIni"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="datFim"/></td>
                    </tr>
                </table>
                <br>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarPedEfet_Click" />
            </form>
        </div>

        <div id="resultados">
            <font color=white>Mostrando <%=pedsEfets.Count%> resultados, de <%=postDatInicio %> a <%=postDatFim %></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                
                
                <% 
                    Decimal totPrePed = 0.0m;
                    string totPrePedS = "";
                    foreach (var pedEfet in pedsEfets) {

                     %> <tr>
                            <th style="width: 5%; text-align:center;">Unidade</th>
                            <th style="width: 5%; text-align:center;">CodCliente</th>
                            <th style="width: 10%; text-align:center;">Cliente</th>
                            <th style="width: 10%; text-align:center;">Representante</th>
                            <th style="width: 10%; text-align:center;">numPed</th>
                            <th style="width: 5%; text-align:center;">Data</th>
                        </tr>
                     <%
                        string codEmpresa = pedEfet.CodEmpresa;
                        DateTime dat = pedEfet.Dat;
                        string codCliente = pedEfet.CodCliente;
                        String numPed = pedEfet.NumPed;
                        string cliente = pedEfet.Cliente;
                        string repres = pedEfet.Repres;
                     %> 
                        <tr>
                            <td style="text-align:center;"><%= codEmpresa %></td>
                            <td style="text-align:center;"><%= codCliente %></td>
                            <td style="text-align:center;"><%= cliente %></td>
                            <td style="text-align:center;"><%= repres %></td>
                            <td style="text-align:right;"><%= numPed %></td>
                            <td style="text-align:center;"><%= dat %></td>
                        </tr>
                                <tr>
                                    <td colspan ="6"><table class="table table-striped table-dark" style="background-color:#3f4142; width:100%; color:white; font-size: 12px;">
                                        <tbody>
                                            <tr>
                                                <th style="width: 15%;">Cod Item</th>
                                                <th style="width: 10%;">Solic</th>
                                                <th style="width: 10%;">Cancel</th>
                                                <th style="width: 10%;">Atend</th>
                                                <th style="width: 45%;">Descricao Item</th>
                                                <th style="width: 45%;">Preco Unit</th>
                                                <th style="width: 20%;">Prazo</th>
                                            </tr><%
                                                     totPrePed = 0.0m;
                                                     totPrePedS = m.formatarDecimal(totPrePed);
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

                                                         totPrePed += preUnit*qtdSolicD;
                                                         totPrePedS = m.formatarDecimal(totPrePed);
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
                                            <tr><td colspan="4" style="background-color:black; color:white;"><b>Total Pre Pedido: R$ <%= totPrePedS %></td><td colspan="6"></td><td colspan="4"></td></tr>
                                        </tbody></table></td>
                                    </tr><%
                                    }
                                %>
            </table>
        </div>
    
</body>
</html>
