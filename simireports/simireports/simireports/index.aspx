<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="simireports.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SimiReports</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

</head>
<body style="background-color:#2a2d33;">



    <center>
        <a  title="Atualizar a página" href="index.aspx"><img style="margin:25px;" width="100px" src="img/syss.png" /></a>
        <br /><font color=white><%=Session["nome"] %></font><hr /><br />
        <table class="table table-dark" style = "text-align: center; width:50%; color:white; font-size: 14px;">
            <tr>
                <th style="width:33%;">Comercial</th>
                <th style="width:33%;">Fiscal</th>
                <th style="width:33%;">Administrativo</th>
            </tr>
            <tr>
                <td><a href="PageComissoes.aspx">Comissoes</a></td>
                <td><a href="PageDevolucoes.aspx">Devoluções</td>
                <td><a href="PageOMsPendentes.aspx">OMs Pendentes</a></td>
            </tr>           
            <tr>
                <td><a href="PagePedidosEfetivados.aspx">Pedidos Efetivados Logix</a></td>
                <td><a href="PagePedidosEfetivadosCRM.aspx">Pedidos Efetivados CRM</a></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3"> <a href="Logout.aspx"><h3><font color="red">Sair</font></h3></a></td>
            </tr>
        </table>
       
    </center>
    
</body>
</html>
