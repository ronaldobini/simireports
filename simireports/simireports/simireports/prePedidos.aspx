<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prePedidos.aspx.cs" Inherits="simireports.prePedidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <center><br />
    <h3>PRE PREDIDOS HOJE</h3>
        <br />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr><th style="width: 80px;">Ped</th><th style="width: 220px;">Tempo</th><th style="width: 150px;">Repres</th><th style="width: 190px;">Criador</th></tr>
                <% foreach (var pedido in pedidos) { %> 
                    <% 
                        string ped = pedido.CodPed;
                        DateTime tempo = pedido.Tempo;
                        string repres = pedido.Repres;
                        string criador = pedido.Criador;
                    %> 

                    <tr style="text-align:center;">
                        <td><%= ped %></td><td><%= tempo %></td><td><%= repres %></td><td><%= criador %> </td>
                    </tr>
              
                <% } %>
            </table>
        </div>
    </form>
</body>
</html>
