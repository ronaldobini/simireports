using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using IBM.Data.Informix;
using simireports.simireports.Classes;

namespace simireports.simireports
{
    
    public partial class PageEmailsCRM : System.Web.UI.Page
    {

        public string resultado = "-";

        protected void Page_Load(object sender, EventArgs e)
        {
            Metodos m = new Metodos();
            //String hoje = DateTime.Today.ToString("d");

            String anteontem;
            String ontem;

            DayOfWeek dow = DateTime.Now.DayOfWeek;
            if (dow == DayOfWeek.Monday)
            {
                anteontem = DateTime.Today.AddDays(-3).ToString("d");
                ontem = DateTime.Today.AddDays(-3).ToString("d");
            }
            else
            {
                anteontem = DateTime.Today.AddDays(-2).ToString("d");
                ontem = DateTime.Today.AddDays(-1).ToString("d");
            }
            anteontem = m.configDataHuman2Banco(anteontem);
            ontem = m.configDataHuman2Banco(ontem);
            //Necessario por o nome dos representantes em maiuscula, toUpper nao funciona dentro do for each porque nao pode mudar variavel de iteraçao
            List<RepresEmails> represEmails = new List<RepresEmails>{
                new RepresEmails("VENDAINT,","vanessa.boumer@similar.ind.br"),
                new RepresEmails("VANESSA,","vanessa.boumer@similar.ind.br"),
                new RepresEmails("VINICIUS,","vinicius.lima@similar.ind.br"),
                new RepresEmails("FELIPE,","felipe.thomaz@similar.ind.br"),


                new RepresEmails("","ti@similar.ind.br")
            };

            foreach (var re in represEmails)
            {
                List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

                decimal totGeral = 0.0m;
                string totGeralS = "0,00";

                Decimal totPed = 0.0m;
                string totPedS = "";
                Decimal totAtend = 0.0m;
                string totAtendS = "";
                Decimal totPend = 0.0m;
                string totPendS = "";

                String corpoEmail = "";

                SqlConnection conn = new BancoAzure().abrir();
                string sql = "SELECT a.Unidade, a.DataUlt, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante," +

                               " b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.Seq, b.Comis " +

                                        " FROM PrePEDIDOS a" +

                                        " INNER JOIN PrePedidosItens b on a.CodPed = b.CodPed" +

                                        " inner join LgxPRODUTOS i on i.cod_item = b.cod_item" +

                                        " WHERE a.cod_cliente LIKE '%" + "" + "%'" +

                                        " AND a.nom_cliente LIKE '%" + "" + "%'" +

                                        " AND a.Representante LIKE '%" + re.nome + "%'" +

                                        " AND a.DataUlt >= '" + ontem + " 00:00:00'" +
                                        //" AND a.DataUlt >= '2019-03-25 00:00:00'" +

                                        " AND a.DataUlt <= '" + ontem + " 23:59:59'" +
                                        //" AND a.DataUlt <= '2019-03-26 23:59:59'" +

                                        " AND b.cod_item like '%" + "" + "%'" +

                                        " AND a.Unidade LIKE '%" + "" + "%'" +

                                        //" AND a.CLProp NOT like 'E%'"+

                                        "" +

                                        " GROUP BY a.Unidade, a.DataUlt, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante,b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.Seq, b.Comis" +
                                        " ORDER BY a.DataUlt desc, a.CodPed, b.Seq";

                //sqlview = sql; //ativa a exibicao do sql na tela

                SqlDataReader reader = new BancoAzure().consultar(sql, conn);
                List<Item> itens = new List<Item>();
                string pedAnt = "zaburska";



                string codEmpresa = "";
                DateTime dat = new DateTime();
                string codCliente = "";
                string cliente = "";
                string repres = "";
                string numPed = "";
                Item item = null;
                bool primeiro = true;
                PedidoEfetivado pedEfet = null;

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        numPed = reader.GetString(3);

                        if (primeiro)
                        {
                            pedAnt = reader.GetString(3);
                            primeiro = false;
                        }
                        else
                        {
                            if (numPed.Equals(pedAnt))
                            {
                                itens.Add(item);
                            }
                            else
                            {
                                itens.Add(item);
                                repres = repres.Substring(0,repres.IndexOf(","));
                                pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres);
                                pedsEfets.Add(pedEfet);
                                itens = new List<Item>();
                                pedAnt = numPed;
                            }
                        }

                        codEmpresa = reader.GetString(0);
                        dat = reader.GetDateTime(1);
                        codCliente = reader.GetString(2);
                        cliente = reader.GetString(4);
                        repres = reader.GetString(5);

                        Double qtdSolic = reader.GetDouble(6); 
                        Double qtdCancel = reader.GetDouble(7);
                        Double qtdAtend = reader.GetDouble(8);
                        string nomeItem = reader.GetString(9);

                        string przEntregaS = "";
                        if (!reader.IsDBNull(10))
                        {
                            przEntregaS = reader.GetString(10);
                        }

                        string codItem = reader.GetString(11);
                        Double preUnitD = reader.GetDouble(12);
                        string preUnitS = Math.Round(preUnitD, 2).ToString();
                        Decimal preUnit = Decimal.Parse(preUnitS);
                        
                        totGeral += ((Decimal)qtdSolic * preUnit);
                        totGeralS = m.formatarDecimal(totGeral);
                        Double desconto = reader.GetDouble(13);
                        item = new Item(qtdSolic.ToString(), qtdCancel.ToString(), qtdAtend.ToString(), nomeItem, przEntregaS, codItem, preUnit, Decimal.Round((((Decimal)desconto) * 100m)));

                    }

                    itens.Add(item);
                    repres = repres.Substring(0, repres.IndexOf(","));
                    pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres);
                    pedsEfets.Add(pedEfet);
                }

                else
                {
                    String erro = "null";
                    try
                    {
                        erro = new BancoAzure().consultarErros(sql,conn);
                    }
                    catch (Exception ex)
                    {
                        erro = "---" + ex;
                    }

                }
                new BancoAzure().fechar(conn);

                corpoEmail = "<body style=\"background-color:#fff; color:#000;\"><div style=\"background-color:#fff;\">Mostrando " + pedsEfets.Count + " resultados, de " + m.configDataBanco2Human(ontem) + " a " + m.configDataBanco2Human(ontem) + " -Total R$ " + totGeralS + "<br/>" +
                    "<table style = \"max-width:100%;background-color:#aaa; color:#000; font-size: 12px;\">";

                foreach (var pedE in pedsEfets)
                {
                    corpoEmail += 
                        "<thead style = \"background-color: #aaa; color:#000;\">" +
                    "<tr>" +
                        //"<td scope = \"col\"style = \"width: 5%; text-align:center;background-color: #fff;\"> Data </th>" +
                        //"<td scope = \"col\"style = \"width: 5%; text-align:center;background-color: #fff;\"> Unidade </th>" +
                        "<td scope = \"col\"style = \"width: 33%; text-align:left;color:#777;background-color: #aaa;\"> Pedido </th>" +
                        //"<td scope = \"col\"style = \"width: 5%; text-align:center;background-color: #fff;\"> CNPJ </th>" +
                        "<td scope = \"col\"style = \"width: 33%; text-align:left;color:#777;background-color: #aaa;\"> Cliente </th>" +
                        "<td scope = \"col\"style = \"width: 33%; text-align:left;color:#777;background-color: #aaa;\"> Representante </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tr>" +
                        //"<td style = \"text-align:center;\">" + pedE.Dat + "</td>" +
                        //"<td style = \"text-align:center;\">" + pedE.CodEmpresa + "</td>" +
                        "<td style = \"text-align:left;color:#000\">" + pedE.NumPed + "</td>" +
                        //"<td style = \"text-align:center;\">" + pedE.CodCliente + "</td>" +
                        "<td style = \"text-align:left;color:#000\">" + pedE.Cliente + "</td>" +
                        "<td style = \"text-align:left;color:#000\">" + pedE.Repres + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td colspan = \"3\">" +
                        "<table style=\"border: 1px solid #777;background-color:#fff; width:100%; color:#000; font-size: 12px;\">" +
                            "<tr style=\"border: 1px solid #777;\">" +
                                "<td style =\"border: 1px solid #777;text-align:left;color:#aaa;width: 20%;\" > Cod. do Item</th>" +
                                "<td style=\"border: 1px solid #777;text-align:left;color:#aaa;width: 55%;\">Desc.Item</th>" +
                                "<td style = \"border: 1px solid #777;text-align:right;color:#aaa;width: 5%;\" > Solic </th>" +
                                //"<td style=\"color:#aaa;width: 5%;\">Cancel</th>" +
                                //"<td style = \"color:#aaa;width: 5%;\" > Atend </th>" +
                                "<td style = \"border: 1px solid #777;text-align:right;color:#aaa;width: 10%;\" > Desconto</th>" +
                                "<td style = \"border: 1px solid #777;text-align:right;color:#aaa;width: 10%;\" > Preço Unit R$</th>" +
                            //"<td style=\"color:#aaa;width:10%;\">Preço Total Atend</th>" +
                            //"<td style=\"color:#aaa;width:10%;\">Preço Total Solic</th>" +
                            //"<td style = \"color:#aaa;width: 20%;\" > Prazo </th>" +
                            "</tr>";

                    totPed = 0.0m;
                    totPed = 0.0m;
                    totAtend = 0.0m;
                    totPedS = m.formatarDecimal(totPed);
                    totAtendS = m.formatarDecimal(totPed);
                    foreach (var itemV in pedE.Itens)
                    {
                        string qtdSolic = m.pontoPorVirgula(itemV.QtdSolic);
                        Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);

                        string qtdCancel = m.pontoPorVirgula(itemV.QtdCancel);
                        Decimal qtdCancelD = Decimal.Round(Decimal.Parse(qtdCancel), 0);

                        string qtdAtend = m.pontoPorVirgula(itemV.QtdAtend);
                        Decimal qtdAtendD = Decimal.Round(Decimal.Parse(qtdAtend), 0);

                        Decimal preUnit = Decimal.Round(itemV.PrecoUnit, 2);
                        String preUnitS = m.formatarDecimal(preUnit);

                        Decimal desconto = itemV.Desconto;

                        //Decimal comis = itemV.Comis;

                        totPed += preUnit * qtdSolicD;
                        totAtend += preUnit * qtdAtendD;

                        totPedS = m.formatarDecimal(totPed);
                        totAtendS = m.formatarDecimal(totAtend);

                        totPend = totPed - totAtend;
                        totPendS = m.formatarDecimal(totPend);

                        totPedS = m.formatarDecimal(totPed);
                        corpoEmail += "<tr>" +
                            "<td style=\"border: 1px solid #777;text-align:left;\">" + itemV.CodItem + " </td>" +
                            "<td style=\"border: 1px solid #777;text-align:left;\">" + itemV.NomeItem + " </td>" +
                            "<td style = \"border: 1px solid #777;text-align:right;\">" + qtdSolicD + " </td>" +
                            //"<td style = \"text-align:center;\">" + qtdCancelD + " </td>" +
                            //"<td style = \"text-align:center;\">" + qtdAtendD + " </td>" +
                            "<td style=\"border: 1px solid #777;text-align:right;\">" + desconto + "%</td>" +
                            "<td style=\"border: 1px solid #777;text-align:right;\">" + preUnitS + "</td>" +
                        //"<td>R$" + totAtend + "</td>" +
                        //"<td>R$" + totPed + "</td>" +
                        //"<td style = \"text-align:right;\">" + itemV.PrzEntrega + "</td>" +
                        "</tr>";
                    }
                    corpoEmail += "<tr>" +

                                    "<td colspan = \"2\" style=\"border: 1px solid #777;background-color: #fff; color:#000;\">Total Pedido: </td>" +
                                    //"<td colspan = \"1\" style=\"background-color: #fff; color:#000;\"></td>" +
                                    //"<td colspan = \"1\" style=\"background-color: #fff;\"> </td>" +
                                    "<td colspan = \"3\" style=\"border: 1px solid #777;background-color: #fff; text-align:right; color:#000;\">" + totPedS + "</td>" +
                                "</tr>" +
                            "</table>" +
                        "</td>" +
                    "</tr>";
                }
                totGeralS = m.formatarDecimal(totGeral);
                corpoEmail += "</table></div></body></div>";


                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ti@similar.ind.br", "Simi1717");
                MailMessage mm = new MailMessage("ti@similar.ind.br", re.email, "Rel. Pedidos CRM - "+re.nome, corpoEmail);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            resultado = "ENVIADO COM SUCESSO";
        }
    }
}