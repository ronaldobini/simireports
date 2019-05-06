<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relatorios.aspx.cs" Inherits="simireports.simireports.Relatorios" %>

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
          ['Ano', 'Vendas'],
          ['Jan',  950],
          ['Fev',  800],
          ['Mar',  1200],
          ['Abr',  0]
        ]);

        var options = {
          title: '',
          backgroundColor: '#2a2d33',
          legend: {position: 'top', textStyle: {color: 'white', fontSize: 16}},
          hAxis: {title: 'Minhas Vendas',  titleTextStyle: {color: 'white'}, textStyle: {color: 'white', fontSize: 11}},
          vAxis: {minValue: 0, titleTextStyle: {color: 'white'}, textStyle: {color: 'white', fontSize: 11}}
        };

        var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
        chart.draw(data, options);
      }
    </script>
</head>
<body  style="background-color:#2a2d33;">

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

    <div id="conteudo" style="margin-top:50px;">
        <center>

            <!-- GRAFICO 
            <div id="chart_div" style="width: 65%; height: 300px; color:white;"></div>
            <hr /><br />
            /GRAFICO -->
            <font color="red"><%=Session["erro"] %></font>
            <table class="table table-dark" style = "text-align: left; width:50%; color:white; font-size: 14px;">
                <tr>
                    <th style="width:33%;">Comercial</th>
                    <th style="width:33%;">Fiscal</th>
                    <th style="width:33%;">Administrativo</th>
                </tr>
                <tr>
                    <td><a style="color:#399bfc;" href="RelComissoes.aspx">001 - Comissões</a></td>
                    <td><a style="color:#399bfc;" href="RelDevolucoes.aspx">004 - Devoluções</td>
                    <td><a style="color:#399bfc;" href="RelOMsPendentes.aspx">005 - OMs Pendentes</a></td>
                </tr>           
                <tr>
                    <td><a style="color:#399bfc;" href="RelPedidosEfetivados.aspx">002 - Pedidos Efetivados Logix </a></td>
                    <td><a style="color:#399bfc;" href="PageSenhas.aspx">Ultra Senhas Secretas</a></td>
                    <td></td>
                </tr>
                <tr>
                    <td><a style="color:#399bfc;" href="RelPedidosEfetivadosCRM.aspx">003 - Pedidos Efetivados CRM</a></td>
                    <td></td>
                    <td></td>
                </tr>
                 <tr>
                    <td><a style="color:#399bfc;" href="RelComissoesEspec.aspx">006 - Comissões Especiais</a></td>
                    <td></td>
                    <td><!--<a href="BotEmailsPedCRM.aspx">Email CRM</a>--></td>
                </tr>
             </table>
        </center>
    </div>
</body>
</html>


<% Session["erro"] = " "; %>