<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelComissoesEspec.aspx.cs" Inherits="simireports.simireports.RelComissoesEspec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

     <title>SimiWeb - <%=Session["swver"] %></title>
</head>
<body style="background-color:#222;">
     <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" width:50px;" src="img/syss.png" /></a>
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">006 - Comissões Especiais</div>
    <br />

   <%-- <div id="filtros" style="margin-bottom:40px;">
        <form id="form1" runat="server">
            <div>
                <form runat="server" id="filtrosComissoesEspec" action="#" method="post">
                    <table>
                        <tr style="color:white;">
                        
                            <th style="width:100px;">Data Inicio</th>
                            <th style="width:120px;">Data Fim</th>
                        </tr>
                        <tr>                       
                            <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datInicio" autocomplete="off"/></td>
                            <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"  autocomplete="off"/></td>
                        </tr>
                    </table>
                    <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarComissEspec_Click" />
                </form>
            </div>
        </form>
    </div>--%>


    <div id="resultados">
        <br /><br /><br />

        <table class="table table-striped table-dark" style = "max-width:25%; color:white; font-size: 12px;">
            <tr>
                <th style="width:80px; text-align:left;">Repres.</th>
                <th style="width:300px;text-align:right;">Vendin/8 + Similar/8 + Assist. (R$)</th>
                <th style="width:100px;text-align:right;">Total (R$)</th>

            </tr>
            <% if ((string)Session["nome"] == "Karolline" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Karol</td>
                <td style="text-align:right;"> <%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissKarolS %> </td>
                <td style="text-align:right;"><%=(Double.Parse(comissKarolS) + Double.Parse(comissVendin8) + Double.Parse(comissSimilar8)) %></td>

            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Dayane" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Dayane</td>
                <td style="text-align:right;"><%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissDayaneS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissDayaneS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>

            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Luana" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Luana</td>
                <td style="text-align:right;"><%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissLuanaS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissLuanaS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>

            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Danielli" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Danielli</td>
                <td style="text-align:right;"><%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissDanielliS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissDanielliS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>
            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Rafaella" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Rafaella</td>
                <td style="text-align:right;"><%=comissVendin8 %>  + <%=comissSimilar8 %> + <%=comissRafaS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissRafaS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>

            </tr>
             <%} %>
            <% if ((string)Session["nome"] == "Rosilaine" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Rosilaine</td>
                <td style="text-align:right;"> <%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissLaineS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissLaineS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>
            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Luciana" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Luciana</td>
                <td style="text-align:right;"><%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissLucianaS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissLucianaS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>
            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Priscila" || (int)Session["key"] >= 7 || (string)Session["nome"] == "Vanessa") { %>
            <tr>
                <td style="text-align:left;">Priscila</td>
                <td style="text-align:right;"><%=comissVendin8 %> + <%=comissSimilar8 %> + <%=comissPriscilaS %></td>
                <td style="text-align:right;"><%=(Double.Parse(comissPriscilaS)+Double.Parse(comissVendin8)+Double.Parse(comissSimilar8)) %></td>
            </tr>
            <%} %>

        </table>


        <br /><br />

         <table class="table table-striped table-dark" style = "max-width:20%; color:white; font-size: 12px;">
            <tr>
                <th style="width:80px; text-align:left;">Repres.</th>
                <th style="width:100px;text-align:right;">Total (R$)</th>

            </tr>
            <% if ((string)Session["nome"] == "Vanessa" || (int)Session["key"] >= 7) { %>
            <tr>
                <td style="text-align:left;">Vanessa</td>
                <td style="text-align:right;"><%=comissVanessaS %></td>
            </tr>
            <%} %>
            <% if ((string)Session["nome"] == "Fabiano" || (int)Session["key"] >= 7) { %>
            <tr>
                <td style="text-align:left;">Fabiano</td>
                <td style="text-align:right;"><%=comissFabianoS %></td>
            </tr>
            <%} %>

        
        <%--<font color="green"><%=sqlview %></font>--%>

    </div>
</body>
</html>