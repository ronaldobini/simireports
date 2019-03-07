<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagePedidosEfetivados.aspx.cs" Inherits="simireports.PagePedidosEfetivados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio de Pedidos Efetivados</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color:#222;">
    <a  title="Voltar ao Inicio" href=" index.aspx"><img style="margin:25px;" width="100px;" src="img/syss.png" /></a>
    <center>
    <h3><font color=white >Pedidos Efetivados</font></h3>
        <br />
        
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
            <font color=white>Mostrando <%=pedsEfets.Count%> resultados, de <%=dataPesqIni%> a <%=dataPesqFim%></font><br/>
            <table class="table table-striped table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 5%; text-align:center;">Unidade</th>
                    <th style="width: 5%; text-align:center;">CodCliente</th>
                    <th style="width: 10%; text-align:center;">Cliente</th>
                    <th style="width: 10%; text-align:center;">Representante</th>
                    <th style="width: 10%; text-align:center;">numPed</th>
                    <th style="width: 10%; text-align:center;">QtdS</th>
                    <th style="width: 10%; text-align:center;">QtdC</th>
                    <th style="width: 10%; text-align:center;">codItem</th>
                    <th style="width: 5%; text-align:center;">preUnit</th>
                    <th style="width: 5%; text-align:center;">Data</th>
                </tr>
                
                <% 
                    foreach (var pedEfet in pedsEfets) {
                        string codEmpresa = pedEfet.CodEmpresa;
                        DateTime dat = pedEfet.Dat;
                        string codCliente = pedEfet.CodCliente;
                        String numPed = pedEfet.NumPed;
                        string qtdPecasSolic = pedEfet.QtdPecasSolic;
                        string qtdPecasCancel = pedEfet.QtdPecasCancel;
                        string codItem = pedEfet.CodItem;
                        Decimal preUnit = Decimal.Round(pedEfet.PrecoUnit,2);
                        String preUnitS = m.formatarDecimal(preUnit);
                        string cliente = pedEfet.Cliente;
                        string repres = pedEfet.Repres;
                %> 
                    <tr>
                        <td style="text-align:center;"><%= codEmpresa %></td>
                        <td style="text-align:center;"><%= codCliente %></td>
                        <td style="text-align:center;"><%= cliente %></td>
                        <td style="text-align:center;"><%= repres %></td>
                        <td style="text-align:right;"><%= numPed %></td>
                        <td style="text-align:center;"><%= qtdPecasSolic%></td>
                        <td style="text-align:center;"><%= qtdPecasCancel %></td>
                        <td style="text-align:center;"><%= codItem %></td>
                        <td style="text-align:right;"><%= "R$ "+preUnitS %></td>
                        <td style="text-align:center;"><%= dat %></td>
                    </tr>
                <%
                    }%>
            </table>
        </div>
    
</body>
</html>
