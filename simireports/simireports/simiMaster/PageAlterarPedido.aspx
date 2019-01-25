<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageAlterarPedido.aspx.cs" Inherits="simireports.simiMaster.PageAlterarPedido" %>
<%@Html.TextBox("myTextBox", "This is value", new { @class = "form-control" })   %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Alterar Pedido</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
</head>
<body style="background-color: #222;">
    <center><br />
    <h3><font color=white>Pedido</font></h3>
        <br />
        
        <div id="filtros" style="margin-bottom:100px;">
            <form runat="server" id="filtrosPedidos" action="#" method="post">
                <input runat="server" type="text" id="pedidoP" value=""/>
                <input runat="server" type="text" id="empresa" value="2"/>
                
                <input runat="server" type="submit" value="Filtrar" onserverclick="filtrarPedido_Click" /><br>

                <% 
                    if (clicado == 1)
                    {

                    foreach (var item in pedido.Itens)
                    {
                        string codItem = item.Item.CodItem;
                        int qtdSolic = item.Qtd;
                        int seq = item.Sequencia;
                        Decimal preco = item.Item.Preco;
                        
                %> 
                     <input runat="server" type="text" id="seq" value="" />
                     <input runat="server" type="text" id="codItem" value=" <%=codItem%> "/>
                     <input runat="server" type="text" id="qtdSolic" value="<%=qtdSolic%>"/>
                     <input runat="server" type="text" id="preco" value="<%=preco%>"/>
                
                     <input runat="server" type="submit" value="Alterar" onserverclick="filtrarAltClick" /><br>
                <%  }
                        %>
            
                <%
        } %>
            </form>
        </div>

        <div id="resultados">
            <table class="table table-striped table-dark" style = "max-width:90%; color:white; font-size: 12px;">
                <tr>
                    <th style="width: 10%;">Item</th>
                    <th style="width: 10%;">codItem</th>
                    <th style="width: 10%;">qtdSolic</th>
                    <th style="width: 10%;">Preco</th>
                    <th style="width: 10%;"> - </th>
                </tr>
            </table>
        </div>
                
</body>
</html>
