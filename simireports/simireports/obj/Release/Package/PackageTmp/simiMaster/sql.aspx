<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="simireports.simiMaster.sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-color:black; color:white;">
    <center>
    <form id="formsql" runat="server" action="#" method="post">
        <div>
            <input type="text" name="sqlExe" id="sqlExe" style="width:560px;" runat="server"/>
            <input style="background-color:black; color:white;" type="submit" value="Executar" runat="server" onserverclick="exe_Click" />
        </div>
    </form>
    <br /><br /><br />
    <font color="green"><%=result %></font>
</body>
</html>
