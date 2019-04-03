<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="simireports.simireports.WebForm1" %>

<html lang="en">
	<head>
	    <link rel="icon" type="image/png" href="img/syss.png">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	    <title>SimiReports</title>

	    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
		<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
		<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
    </head>

  	<body style="background-color: #2a2d33;">

  		<!-- LOGO TOP -->
		<div style="text-align: center; height: 110px; background-color:white; padding-top: 10px;">
			 <a href="index.php"><img src = "img/logoSimi.png" width="250"></a>
		</div>

	    <div class="container" style="margin-top:70px; width:25%;">

	     <form runat="server" class="form-signin" method="post" action="#">
            <div class="form-signin-heading" style="color:white; font-size:18px;"><b>Login</b></div>
	        <input runat="server" type="text" name="login" id="login" class="form-control" placeholder="Login" required="" style="font-size: 15px;">
	        <div class="form-signin-heading" style="color:white;font-size:18px; margin-top:20px;"><b>Senha</b></div>
	        <input runat="server" type="password" name="pass" id="senha" class="form-control" placeholder="Senha" required="" style="font-size: 15px;">
	        <br>
               <center> 
                   <input runat="server" style="width:100px;" class="btn btn-lg btn-primary btn-block" type="submit" value="Entrar" onserverclick="Logar_Click" /><br />
                   <font color="red"><%=erro %></font>
               </center>
	        
	     </form>


             <% //VERIFICA SESSAO E REDIRECIONA

                if(Session["key"]!= null) { 
                    int key = (int) Session["key"];
                    if (key >= 1)
                    {
             %>
                    <center><h3 style="color:white;"><%=Session["Nome"] %> entrando...</h3></p></center>
                    <meta http-equiv="refresh" content="0; URL=index.aspx" />
             <%     
                    }

                }//--------------------
             %>
	     	
	    </div> <!-- /container -->
         
		 <center>
			 <font color="green" style = "margin-bottom: -1px;">
				 	Desenvolvido por Similar Tecnologia e Automação Ltda - 2019
			 </font>
			 <br />
	 	</center>

   	</body>
</html>