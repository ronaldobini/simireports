﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelOMsPendentes.aspx.cs" Inherits="simireports.PageOMsPendentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous" />
</head>
<body style="background-color: #222;">
    <div id="logo" style="margin-left: 20px; float: left;">
        <a title="Voltar ao Inicio" href=" Relatorios.aspx">
            <img style="width: 50px;" src="img/syss.png" /></a>
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
                        <td style="width:150px;text-align:center;">Empresa</td>
                        <%--<td style="width:150px;text-align:center;">Pedido</td>
                        <td style="width:150px;text-align:center;">Item</td>
                        <td style="width:150px;text-align:center;">CodCliente</td>
                        <td style="width:150px;text-align:center;">Cliente</td>
                        <td style="width:200px;text-align:center;">Situacao</td>--%>
                        <td style="width:200px;text-align:center;">Tipo de Entrega</td>

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
                            </td>
                            
<%--                        <td style="width:150px;"><input class="form-control" style="width:130px; text-align:center;" runat="server" type="text" id="pedido"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="item"/></td>
                        <td style="width:130px;"><input class="form-control" style="width:110px; text-align:center;" runat="server" type="text" id="codCliente"/></td>
                        <td style="width:100px;"><input class="form-control" style="width:80px; text-align:center;" runat="server" type="text" id="cliente"/></td>
                        --%>
                            <td>
                <select class="form-control" id="tipoEntrega" runat="server">
                    <option value="0">Todas</option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
                            </td>
                            <%--<td>
                <select class="form-control" id="situacao" runat="server">
                    <option value="0">Todos</option>
                    <option value="1">Atrasados</option>
                </select>
                            </td>--%>

                             </tr>
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
                        %> 
                    <tr style="background-color: #070a0e; color:white;">
                        <th style="width: 5%;">Empresa</th>
                        <th style="width: 10%;">Efetiv</th>
                        <th style="width: 10%;">Cod Cliente</th>
                        <th style="width: 20%;">Cliente</th>
                        <th style="width: 5%;">Pedido</th>
                        <th style="width: 5%;">Tipo Entrega</th>
                        <th style="width: 5%;">Transportadora</th>
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
                                    <td><%= omsp.Trans %></td>
                                </tr>
                                <tr>
                                    <td colspan ="7">
                                        <table class="table table-sm table-dark" style="background-color:#3f4142; width:100%; color:white; font-size: 12px;">
                                       
                                            <tr>
                                                <th style="width: 15%;">Cod Item</th>
                                                <th style="width: 45%;">Descricao Item</th>
                                                <th style="width: 5%;">Solic</th>
                                                <th style="width: 5%;">Cancel</th>
                                                <th style="width: 5%;">Atend</th>
                                                <th style="width: 5%;">Roma</th>
                                                <th style="width: 5%;">Lib</th>
                                                <th style="width: 5%;">Reserv</th>
                                                <th style="width: 20%;">Prazo</th>
                                            </tr><%
                                                     foreach (var item in omsp.Itens)
                                                     {
                                                         string codItem = item.CodItem;
                                                         string nomeItem = item.NomeItem;
                                                         string przEntrega = item.PrzEntrega;
                                                         string cor = "#fff";
                                                         //bool mostraOC = true;
                                                         if (item.QtdSolic == item.QtdAtend + item.QtdCancel)
                                                         {
                                                             cor = "#666";
                                                             //mostraOC = false;
                                                         }
                                          %>
                                            <tr>
                                                <td style="color:<%= cor %>;"><%= codItem %></td>
                                                <td style="color:<%= cor %>;"><%= nomeItem %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdSolic %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdCancel %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdAtend %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdRom %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdLib %></td>
                                                <td style="color:<%= cor %>;"><%= item.QtdRes %></td>
                                                <td style="color:<%= cor %>;"><%= m.configDataBanco2Human(przEntrega) %></td>
                                            </tr>
                                            <% 
                                                if (item.OCs1 != null && item.OCs1.Count > 0)
                                                {%>
                                            <tr>
                                                <td></td>
                                                <td colspan ="9">
                                                    <table class="table table-sm table-dark" style="background-color:#3f4142; width:50%; color:white; font-size: 12px;">
                                                            <tr>
                                                <th style="width: 15%;">Num OC</th>
                                                <th style="width: 45%;">Num Docum</th>
                                                <th style="width: 15%;">Empresa</th>
                                                <th style="width: 10%;">Cod Item</th>
                                                <th style="width: 10%;">Previsao Chegada</th>
                                                <th style="width: 10%;">Quantidade</th>
                                            </tr><%

                                                     foreach (var oc in item.OCs1)
                                                     {
                                                                                      %>
                                                                                        <tr>
                                                                                            <td style="color:<%= cor %>;"><%= oc.NumOc %></td>
                                                                                            <td style="color:<%= cor %>;"><%= oc.NumDocum %></td>
                                                                                            <td style="color:<%= cor %>;"><%= oc.Empresa %></td>
                                                                                            <td style="color:<%= cor %>;"><%= oc.CodItem %></td>
                                                                                            <td style="color:<%= cor %>;"><%= oc.PrevistaChegada %></td>
                                                                                            <td style="color:<%= cor %>;"><%= oc.Qtd %></td>
                                                                                        </tr>
                                                                                      <%
                                                                                          } %>
                                                        </table>
                                                </td>
                                                </tr><%


                                                             }
                                                         }%>

                                                    </table>
                                                </td>
                                                </tr>
                                            <%
                                                }
                %></table>
        </div>
</body>
</html>
