<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageOMsPendentes.aspx.cs" Inherits="simireports.PageOMsPendentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Relatorio de OMs Pendentes</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous"/>
</head>
<body style="background-color: #222;">
    <center><br />
    <h3><font color=white>OMs Pendentes</font></h3>
        <br />
        
        <div id="filtros" style="margin-bottom:100px;">
            <form runat="server" id="filtrosOMs" action="#" method="post">
                <input runat="server" type="text" id="empresa" placeholder="Empresa" value="0">
                <input runat="server" type="text" id="tipoEntrega" placeholder="1, 2, 3, 4"/>
                
                <input runat="server" type="submit" value="Filtrar" onserverclick="filtrarOMs_Click" />
            </form>
        </div>

        <div id="resultados">
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
