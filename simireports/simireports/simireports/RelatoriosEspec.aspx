<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatoriosEspec.aspx.cs" Inherits="simiweb.simireports.RelatoriosEspec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <form runat="server" id="filtrosComissoes" action="#" method="post">
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
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarComiss_Click" />
            </form>
        </div>
    </form>
</body>
</html>
