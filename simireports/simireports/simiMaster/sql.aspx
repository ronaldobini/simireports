<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="simireports.simiMaster.sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        #sqlExe:focus {
            outline: none !important;
            border:1px solid green;
            box-shadow: 0 0 10px #00c000;
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-color:black; color:white;">
    <center>
    <form id="formsql" runat="server" action="#" method="post">
        <div style="margin-top:35px;">
            <textarea name="sqlExe" id="sqlExe" style="width:850px; height:300px; background-color:black; border-color:black; color:#00c000; font-size:large;" runat="server"></textarea><br /><br />
            <input style="background-color:black; color:green; border-color:green;" type="submit" value="Executar" runat="server" onserverclick="exe_Click" />
        </div>
    </form>
    <br /><br /><br />
    <font color="green"><%=resCount %><%=result %></font>
</body>
</html>
