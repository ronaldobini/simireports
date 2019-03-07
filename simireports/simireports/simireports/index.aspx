<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="simireports.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SimiReports</title>
</head>
<body bgcolor="#2a2d33">
    <center>
        <a  title="Atualizar a página" href="index.aspx"><img style="margin:25px;" width="100px" src="img/syss.png" /></a>
        <br /><br /><br /><br />
        <a href="PageComissoes.aspx"><h3><font color="white">Comissoes</font></h3></a>
        <a href="PageDevolucoes.aspx"><h3><font color="white">Devoluções</font></h3></a>
        <!--<a href="Estoque.aspx">Estoque</a><br />-->
        <a href="PageOMsPendentes.aspx"><h3><font color="white">OMs Pendentes</font></h3></a>
        <a href="PagePedidosEfetivados.aspx"><h3><font color="white">Pedidos Efetivados</font></h3></a>
    </center>
    <%=Session["nome"] %>
</body>
</html>
