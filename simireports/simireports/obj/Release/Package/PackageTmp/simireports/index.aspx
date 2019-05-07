<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="simireports.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <link rel="stylesheet" href="css/design.css" />
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
      google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawChart);

      function drawChart() {

        var data = google.visualization.arrayToDataTable([
          ['Task', 'Hours per Day'],
          ['Minhas',     11],
          ['Em Atraso',      2],
          ['Pendentes',  2],
          ['Em Andamento', 2],
          ['Concluidas',    7]
        ]);

        var options = {
            title: 'Solicitações',
            backgroundColor: '#2a2d33',
            legendTextStyle: { color: '#fff' },
            titleTextStyle: { color: '#fff'}
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
    </script>
</head>
<body style="background-color:#2a2d33;">
    
    <div id="topo">
        <div class="center">
		    <table>
			    <tr>
				    <td width="52px">
					    <a  title="Início" href="index.aspx"><img style = "height: 52px; margin-top:2px;" src="img/syss.png" /></a>
				    </td>
				    <td>
					    <nav id="rolling-nav">
						    <ul>
							    <li>
								    <form id="relatorios" action="Relatorios.aspx" method="post">
									    <a href="javascript:;" onclick="document.getElementById('relatorios').submit();" data-clone="Reports">Relatórios</a>
									    <input type="hidden" name="key" value="1">
								    </form>
							    </li>
							    <li>
								    <form id="upload" action="construcao.aspx" method="post">
									    <a href="javascript:;" onclick="document.getElementById('upload').submit();" data-clone="Requests">Solicitações</a>
									    <input type="hidden" name="key" value="1">
								    </form>
							    </li>							
							    <li>
								    <form id="servicos" action="construcao.aspx" method="post">
									    <a href="javascript:;" onclick="document.getElementById('servicos').submit();" data-clone="IT"> TI</a>
									    <input type="hidden" name="key" value="1">
								    </form>
							    </li>
							    <li>
								    <form id="adm" action="construcao.aspx" method="post">
									    <a href="javascript:;" onclick="document.getElementById('adm').submit();" data-clone="Management">Gerência</a>
									    <input type="hidden" name="key" value="1">
								    </form>
							    </li>
							    <li style="color:red;">
								    <form id="sair" action="Logout.aspx" method="post">
									    <a href="javascript:;" onclick="document.getElementById('sair').submit();" data-clone="Quit">Sair</a>
									    <input type="hidden" name="key" value="1">
									    <input type="hidden" name="sair" value="1">
								    </form>
							    </li>
						    </ul>
					    </nav>
				    </td>
			    </tr>
		    </table>
        </div>
	</div>


    

           
    <center>    
    
    <br /><font color="silver">Bem vindo <%=Session["nome"] %> 
        
        <%--<% if ((int)Session["key"] >= 5) {
                %> <a href="PageSenhas.aspx"> <img src = "img/key.png" style="width:30px;"/></a> <%
            }%>--%>

          </font><font color=white></font><hr />

    

    <img src = "img/simiweb1.png" style="width:65%;"/>

    <!-- GRAFICO 
    <div id="piechart" style="width: 65%; height: 250px; color:white;"></div>
    <hr /><br />
     /GRAFICO -->


    
       
    </center>
    
</body>
</html>
