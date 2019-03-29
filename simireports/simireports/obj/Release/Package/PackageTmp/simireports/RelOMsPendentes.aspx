<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelOMsPendentes.aspx.cs" Inherits="simireports.PageOMsPendentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Relatorio de OMs Pendentes</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous" />
</head>
<body style="background-color: #222;">
    <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">005 - OM Pendentes</div>
    <br />
        
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosOMs" action="#" method="post">
<%--                <input runat="server" select id="empresa" placeholder="Empresa (2,3,4,5,6)">
                <input runat="server" type="text" id="Text1" placeholder="Tip Entrega (1, 2, 3, 4)"/>--%>
                <h3><font color=white>
                    <table>
                        <tr>
                        <td style="width:150px;text-align:center;">
                    Empresa
                            </td>
                        <td style="width:150px;text-align:center;">
                    Tipo
                            </td>

                        </tr><tr>

                            <td>
                <select class="form-control" id="empresa" runat="server">
                    <option value="0">Todas</option>
                    <option value="2">02</option>
                    <option value="3">03</option>
                    <option value="4">04</option>
                    <option value="5">05</option>
                    <option value="6">06</option>
                </select>
                            </td><td>
                <select class="form-control" id="tipoEntrega" runat="server">
                    <option value="0">Todas</option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
                            </td></tr>
                        </table>
                            </font></h3>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarOMs_Click" />
            </form>
        </div>

        <div id="resultados">
            <font color=white>Mostrando <%=omsps.Count%> resultados</font><br/>
            <table class="table table-striped table-dark" style = "max-width:90%; color:white; font-size: 12px;">
                
                
                <% 
                    foreach (var omsp in omsps)
                    {
                        %> <tr>
                    <th style="width: 5%;">Empresa</th>
                    <th style="width: 10%;">Efetiv</th>
                    <th style="width: 10%;">Cod Cliente</th>
                    <th style="width: 20%;">Cliente</th>
                    <th style="width: 5%;">Pedido</th>
                    <th style="width: 5%;">Tipo Entrega</th>
                    </tr><%
                             string empresa = omsp.Empresa;
                             DateTime datAltSit = omsp.DatAltSit;
                             string codCliente = omsp.CodCliente;
                             string cliente = omsp.Cliente;
                             string numPed = omsp.NumPed;
                             string tipoEntrega = omsp.TipoEntrega;

                        %>
                                <tr>
                                    <td><%= empresa %></td>
                                    <td><%= datAltSit %></td>
                                    <td><%= codCliente %></td>
                                    <td><%= cliente %></td>
                                    <td><%= numPed %></td>
                                    <td><%= tipoEntrega %></td>
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
                                                <th style="width: 20%;">Prazo</th>
                                            </tr><%
                                                     foreach (var item in omsp.Itens)
                                                     {
                                                         string codItem = item.CodItem;
                                                         string qtdSolic = item.QtdSolic;
                                                         string qtdCancel = item.QtdCancel;
                                                         string qtdAtend = item.QtdAtend;
                                                         string nomeItem = item.NomeItem;
                                                         string przEntrega = item.PrzEntrega;
                                          %>
                                            <tr>
                                                <td><%= codItem %></td>
                                                <td><%= qtdSolic %></td>
                                                <td><%= qtdCancel %></td>
                                                <td><%= qtdAtend %></td>
                                                <td><%= nomeItem %></td>
                                                <td><%= przEntrega %></td>
                                            </tr>
                                          <%
                                              }%>
                                        </tbody></table></td>
                                    </tr><%
                                             }
                %>
    </table>
        </div>
</body>
</html>
