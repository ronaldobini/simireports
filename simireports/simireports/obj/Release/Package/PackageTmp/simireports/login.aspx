<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="simireports.simireports.WebForm1" %>


<br /><br /><br /><br />
    <form runat="server" name="login" action="#" method="post">

        <input type="text"  autocomplete="off" runat="server" id="login" />
        <input type="submit" id="logar" value="Logar" runat="server" onserverclick="logar_Click"/>

    </form>

    
    Resposta: <%=wLogin%>


