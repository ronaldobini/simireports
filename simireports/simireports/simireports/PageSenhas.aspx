<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageSenhas.aspx.cs" Inherits="simireports.PageSenhas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

    <%
        //bloqueia campo repres
        //VERIFICACAO DE SESSAO E NIVEL
        string senhaNivel = "";
        if ((int)Session["key"] <= 0)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            //VERFICA NIVEL
            if ((int)Session["key"] < 5)
            {
                Session["erro"] = "Você não tem permissão para acessar esta função.";
                Response.Redirect("Relatorios.aspx");
            }
        }
    %>
</head>
<body style="background-color: #222;" onload="onload();">
    <div id="logo" style="margin-left: 20px; float: left;">
        <a title="Voltar ao Inicio" href=" Relatorios.aspx">
            <img style="width: 50px;" src="img/syss.png" /></a>
    </div>
    <center>
        <%
            string senhaNivel = "";
            if ((int)Session["key"] >= 8)
            {
                senhaNivel = "Senha 1.0";
            }
            else if ((int)Session["key"] >= 7)
            {
                senhaNivel = "Senha 1.5";
            }
            else if ((int)Session["key"] >= 5)
            {
                senhaNivel = "Senha 2.0";
            }%>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;"><%= senhaNivel %></div>
    <br />
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosComissoes" action="#" method="post">
                <input runat="server" id="preS" type="text" maxlength="6" minlength="6"/>
                <br/><br/>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Gerar Senha" onserverclick="gerarSenha" />
            </form>
        <div style="color:White;font-size:20px">
            <br/>
            <br/>
           <%= senha %><br/>
        </div>
        </div>
</body>
</html>
