<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelPedidosEfetivados.aspx.cs" Inherits="simireports.PagePedidosEfetivados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/png" href="img/syss.png">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SimiWeb - <%=Session["swver"] %></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>

   <script>
        //onload
        function onload() {

            //bloqueia campo repres
            var represChange = "<%=represChange %>";
            if (represChange != "sim") {
                var repres = document.getElementById('repres');
                repres.disabled = true;
                repres.value = "<%=Session["nome"] %>";
            }

            //first
            var first = <%=Session["firstJ"] %>;
            if (first == 1) {
                var inicio = document.getElementById('datIni');
                inicio.value = "<%=hoje %>";

                var fim = document.getElementById('datFim');
                fim.value = "<%=hoje %>";

                var repres = document.getElementById('repres');
            }
       }


       $(document).ready(function () {

           $(".mostrar").click(function () {
            $(".mostrar").hide();
            $(".espacoBotao").hide();
            $(".qtdOcultos").show(500);
            $(".ocultar").show(500);
           });

           $(".ocultar").click(function () {
            $(".qtdOcultos").hide();
            $(".ocultar").hide();
            $(".mostrar").show(500);
            $(".espacoBotao").show(500);
           });

        });

        
    </script>
</head>
<body style="background-color:#222;" onload="onload();" >
    <div id="logo" style="margin-left:20px; float:left;">
        <a  title="Voltar ao Inicio" href=" Relatorios.aspx"><img style=" height:50px;" src="img/syss.png" /></a>
       
    </div>
    <center>
    <div id="titulo" style="margin-top:40px; margin-right:70px; color:white; font-size:30px;">002 - Pedidos Logix  <a  title="Ajuda" href="help/ped_logix.pdf"><img style=" height:25px; margin-left:20px;" src="img/help.PNG" /></a></div>
    <br />
    <p><%=sqlview %></p>
       
        <div id="filtros" style="margin-bottom:40px;">
            <form runat="server" id="filtrosPedEfet" action="#" method="post">
                
                <table>
                    <tr style="color:white;">
                        <th>Unidade</th>
                        <th>CNPJ</th>
                        <th>Nome Cliente</th>
                        <th>Representante</th>
                        <th>Pedido</th>
                        <th>Cod. do Item</th>
                        <th>Fam.</th>
                        <th>Data Inicio <br /> (Min: 2016)</th>
                        <th>Data Fim</th>
                        <th>Sit.</th>
                    </tr>
                    <tr>
                        <td style="width:120px;">
                            <select class="form-control" style="width:100px;" id="unidade" runat="server">
                                <option value="">Todas</option>
                                <option value="2">02</option>
                                <option value="3">03</option>
                                <option value="4">04</option>
                                <option value="5">05</option>
                                <option value="6">06</option>
                            </select>
                        </td>
                        <td style="width:150px;"><input class="form-control" style="width:130px; text-align:center;" runat="server" type="text" id="codCliente"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="cliente"/></td>
                        <td style="width:130px;"><input class="form-control" style="width:110px; text-align:center;" runat="server" type="text" id="repres"/></td>
                        <td style="width:100px;"><input class="form-control" style="width:80px; text-align:center;" runat="server" type="text" id="numPed"/></td>
                        <td style="width:120px;"><input class="form-control" style="width:100px; text-align:center;" runat="server" type="text" id="codItem"/></td>
                        <td style="width:140px;">
                            <select class="form-control" style="width:120px;" id="familia" runat="server">
                                <option value="00">Todas</option>
                                <option value="01">SICK</option>
                                <option value="02">LS</option>
                                <option value="03">BRADY</option>
                                <option value="09">BELDEN</option>
                                <option value="34">GREENPROCESS</option>
                                <option value="50">SERVICOS</option>
                                <option value="99">DIVERSOS04</option>
                            </select>
                        </td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datIni"/></td>
                        <td style="width:140px;"><input class="form-control" style="width:120px; text-align:center;" runat="server" type="text" id="datFim"/></td>
                        <td style="width:120px;">
                            <select class="form-control" style="width:120px;" id="openclose" runat="server">
                                <option value="0">Efetivados</option>
                                <option value="-1">Todos</option>
                                <option value="1">Abertos</option>
                                <option value="2">Fechados</option>
                                <option value="4">Reprovados</option>
                                <option value="5">Cancelados</option>
                                <option value="3">Atrasados</option>
                            </select>
                        </td>
                    </tr>
                </table>
                <br>
                <input class="btn btn-primary btn-xs" style="background-color:#126DBD" runat="server" type="submit" value="Filtrar" onserverclick="filtrarPedEfet_Click" />
                <input class="btn btn-primary btn-xs" style="background-color:#0a5e03; border-color:#0a5e03;" runat="server" type="submit" value="Exportar" onserverclick="export_Click" />

            </form>
        </div>

        <div id="resultados">
            
            <font color=white>Mostrando <%=pedsEfets.Count%> resultados, de <%=postDatInicio %> a <%=postDatFim %> <br />
                Total Efetivado: R$ <%=totGeralS %> | Total Atendido: R$ <%=totGeralAS %> | Total Pendente: R$ <%=totGeralPS %> </font><br/>
            <table class="table table-hover table-dark" style = "max-width:95%; color:white; font-size: 12px;">
                
                
                <% 
                    Decimal totPed = 0.0m;
                    string totPedS = "";
                    Decimal totAtend = 0.0m;
                    string totAtendS = "";
                    Decimal totPend = 0.0m;
                    string totPendS = "";

                    foreach (var pedEfet in pedsEfets) {

                     %> 
                        <thead style="background-color: #070a0e; color:white;">
                            <tr>
                                <th scope="col" style="width: 5%; text-align:center;">Data</th>
                                <th scope="col" style="width: 5%; text-align:center;">Unidade</th>
                                <th scope="col"style="width: 10%; text-align:center;">Pedido</th>
                                <th scope="col" style="width: 5%; text-align:center;">CNPJ</th>
                                <th scope="col" style="width: 10%; text-align:center;">Cliente</th>
                                <th scope="col" style="width: 10%; text-align:center;">Representante</th>
                                <th scope="col" style="width: 10%; text-align:center;">Ped. Cliente</th>
                                <th scope="col" style="width: 10%; text-align:center;">Pgto</th>
                            </tr>
                        </thead>
                     <%
                        string codEmpresa = pedEfet.CodEmpresa;
                        DateTime dat = pedEfet.Dat;
                        string codCliente = pedEfet.CodCliente;
                        String numPed = pedEfet.NumPed;
                        string cliente = pedEfet.Cliente;
                        string repres = pedEfet.Repres;
                        string pedCli = pedEfet.PedCli;
                        string finalidade = pedEfet.Finalidade;                         
                        string condPgto = pedEfet.CondPgto;
                     %> 
                        <tr>
                            <td style="text-align:center;"><b><%= dat %></b></td>
                            <td style="text-align:center;"><b><%= codEmpresa %></b></td>
                            <td style="text-align:center;"><b><%= numPed %></b></td>
                            <td style="text-align:center;"><b><%= codCliente %></b></td>
                            <td style="text-align:center;"><b><%= cliente %></b></td>
                            <td style="text-align:center;"><b><%= repres %></b></td>
                            <td style="text-align:center;"><b><%= pedCli %></b></td>
                            <td style="text-align:center;"><b><%= condPgto %></b></td>
                        </tr>
                        <tr>
                            <td colspan ="8">
                                <table class="table table-sm table-dark" style="background-color:#3f4142; width:100%; color:white; font-size: 12px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 10%;">Cod. do Item</th>
                                            <th style="width: 35%;">Desc. Item</th>
                                            <th style="width: 5%; text-align:center;"">Solic</th>
                                            <th  style="width: 5%; text-align:center;">Cancel</th>
                                            <th style="width: 5%; text-align:center;"">Atend</th>
                                            <th class="espacoBotao" colspan="3" style="width: 5%; text-align:center;"><button style="background-color:#222; color:white; font-size:10px;" class="mostrar">></button></th>
                                            <th class="qtdOcultos" style="width: 5%; text-align:center;display:none;">Rom</th>
                                            <th class="qtdOcultos" style="width: 5%; text-align:center;display:none;">Lib</th>
                                            <th class="qtdOcultos" style="width: 5%; text-align:center;display:none;">Res</th>
                                            <th style="text-align:right;width: 10%;"><button style="background-color:#222;color:white;font-size:10px;display:none;" class="ocultar"><</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Preço Unit (R$)</th>
                                            <th style="text-align:right;width: 10%;">Valor Total (R$)</th>
                                            <th style="text-align:right;width: 20%;">Prazo</th>
                                        </tr>
                                    </thead>
                                    <%
                                        totPed = 0.0m;
                                        totAtend = 0.0m;
                                        totPedS = m.formatarDecimal(totPed);
                                        totAtendS = m.formatarDecimal(totPed);
                                        foreach (var item in pedEfet.Itens)
                                        {
                                            string codItem = item.CodItem;

                                            string nomeItem = item.NomeItem;
                                            string przEntrega = item.PrzEntrega;

                                            Decimal preUnit = Decimal.Round(item.PrecoUnit,2);
                                            String preUnitS = m.formatarDecimal(preUnit);

                                            totPed += preUnit*(item.QtdSolic-item.QtdCancel);
                                            totAtend += preUnit*item.QtdAtend;

                                            totPedS = m.formatarDecimal(totPed);
                                            totAtendS = m.formatarDecimal(totAtend);

                                            totPend = totPed - totAtend;
                                            totPendS =  m.formatarDecimal(totPend);


                                            string cor = "red";
                                            if(item.QtdSolic == (item.QtdAtend + item.QtdCancel))
                                            {
                                                cor = "#fff";
                                            }

                                            string link = "";
                                            string link2 = "";
                                            if (item.QtdAtend > 0m)
                                            {
                                                link = "<a href='RelFaturamento.aspx?getPedido="+numPed+"' style='color:"+cor+";'>";
                                                link2 = "</a>";
                                            }
                                            else
                                            {
                                                link = "";
                                                link2 = "";
                                            }


                                    %>
                                            <tr>
                                                <td style="text-align:center;color:<%= cor %>;"><%= codItem %></td>
                                                <td style="text-align:center;"><%= nomeItem %></td>
                                                <td style="text-align:center;"><%= item.QtdSolic %></td>
                                                <td style="text-align:center;"><%= item.QtdCancel %></td>
                                                <td style="text-align:center;color:<%= cor %>;">
                                                    <%=link %><%= item.QtdAtend %><%=link2 %>
                                                </td>
                                                <td class="espacoBotao" colspan="3"></td>
                                                <td class="qtdOcultos" style="text-align:center;display:none;"><%= item.QtdRom %></td>
                                                <td class="qtdOcultos" style="text-align:center;display:none;"><%= item.QtdLib %></td>
                                                <td class="qtdOcultos" style="text-align:center;display:none;"><%= item.QtdRes %></td>
                                                <td style="text-align:right;"><%= preUnitS %></td>
                                                <td style="text-align:right;"><%= preUnit*item.QtdSolic %></td>
                                                <td style="text-align:right;"><%= m.configDataBanco2Human(przEntrega) %></td>
                                            </tr>
                                                
                                    <% } %>
                                    <tr>
                                        <td colspan="6" style="background-color: #070a0e; color:white;"><b>Total Pedido: R$ <%= totPedS %></td>
                                        <td colspan="3" style="background-color: #070a0e; color:white;"><b>Total Atendido: R$ <%= totAtendS %></td>
                                        <td colspan="3" style="background-color: #070a0e; color:white;"><b>Total Pendente: R$ <%= totPendS %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                                <%
                                    } 
                                %>
            </table>
        </div>
    
</body>
</html>
