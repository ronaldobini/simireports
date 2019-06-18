<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelResumoPedido.aspx.cs" Inherits="simireports.simireports.RelResumoPedido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body style="background-color:#222;" onload="onload();" >
    <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" height:50px;" src="img/syss.png" /></a>
       
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-bottom:25px; margin-right:70px; color:white; font-size:30px;">010 - Resumo do Pedido </div>
    <br />
    <div id="filtros" style="margin-bottom:40px;">
        <form id="resumo" runat="server" action="#" method="post">
        
            <table>
                 <tr style="color:white;">
                        <th>Num. Ped. Logix</th>
                        <th></th>
                        <th>Cod. Ped. CRM</th>
                    </tr>
                <tr>
                    <td><input  class="form-control" style="width:130px; text-align:center;" runat="server" type="text" id="pedLogix"/></td>
                    <td style="color:white; width:50px; text-align:center;"> ou </td>
                    <td><input class="form-control" style="width:130px; text-align:center;" runat="server" type="text" id="pedCRM"/></td>

                </tr>
                <tr><td colspan="3">-</td></tr>
                <tr><td colspan="3" style="text-align:center;">
                    <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Buscar" onserverclick="buscar_Click" />
                </td></tr>
            </table>
      
        </form>
    </div>

       <font color=white><%=resultado1 %> </font><br/>
       <font color=white><%=resultado2 %> </font><br/>

        <br /><br /><br /><br />
        <font color=silver><%=resultado3 %> </font><br/>
            
    
        <table class="table table-hover table-dark" style = "max-width:95%; color:white; font-size: 12px;">
        </table>


</body>
</html>
