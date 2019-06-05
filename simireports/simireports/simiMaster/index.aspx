<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="simireports.simiMaster.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/designIndex.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <title>% SimiMaster %</title>

     <script>
        //onload
       $(document).ready(function () {
           $("#menuMaster").hide();
           $("#secret").click(function () {
               $("#menuMaster").show(1000);
           });

           

        });

        
    </script>


</head>


<body style="background-color:black;">
    <center>
            
        <div id="menuMaster">                
                <table>
                    <tr>
                        <td style="width:200px"><a href="pedidos.aspx">Alterar Repres</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Pedido Advanced Info</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Alterar Produto</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Alterar Pedidos</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Armazem TI</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Emails</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Inventario</a></td>
                    
                    </tr>
                    <tr>
                        <td style=""><a href="pedidos.aspx">Senhas</a></td>
                    
                    </tr>
                     <tr>
                        <td style=""><a href="sql.aspx">SQL</a></td>
                    
                    </tr>
                
                </table>
        </div>

        <div id="secret">
            
        </div>
</body>
</html>
