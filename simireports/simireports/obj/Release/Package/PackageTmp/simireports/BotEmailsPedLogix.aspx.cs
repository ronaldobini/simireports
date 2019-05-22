using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using IBM.Data.Informix;
using simireports.simireports.Classes;

namespace simireports.simireports
{
    public struct RepresEmails
    {
        public string nome;
        public string email;
        public RepresEmails(string n, string e)
        {
            nome = n;
            email = e;
        }
    }
    public partial class PageEmails : System.Web.UI.Page
    {
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
                ontem = DateTime.Today.AddDays(-1).ToString("d");
            }
            else
            {
                anteontem = DateTime.Today.AddDays(-2).ToString("d");
                ontem = DateTime.Today.AddDays(-1).ToString("d");
            }
            //Necessario por o nome dos representantes em maiuscula, toUpper nao funciona dentro do for each porque nao pode mudar variavel de iteraçao
            List <RepresEmails> represEmails = new List<RepresEmails>{
                new RepresEmails("VENDAINT","ti@similar.ind.br"),
                new RepresEmails("VANESSA","ronaldo.bini@similar.ind.br")};

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
                
                IfxConnection conn = new BancoLogix().abrir();
                string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, " +
                " b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit, b.num_sequencia " +
                                   " FROM pedidos a" +
                                    " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa" +
                                    " JOIN clientes c on c.cod_cliente = a.cod_cliente" +
                                    " join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa " +
                                   " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + "" + "%'" +

                                   " AND c.nom_cliente LIKE '%" + "" + "%'" +

                                   " AND r.nom_repres LIKE '%" + re.nome + "%'" +

                                   " AND a.dat_alt_sit >= '" + ontem + "'" +

                                   " AND a.dat_alt_sit <= '" + ontem + "'" +

                                   " AND b.cod_item like '%" + "" + "%'" +

                                   " AND a.cod_empresa LIKE '%" + "" + "%'" +

                                   " AND ies_sit_pedido = 'N' AND cod_nat_oper<> 9001" +

                                   "" +

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +
                                   " GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit,b.num_sequencia" +
                                   " ORDER BY a.dat_alt_sit desc, a.num_pedido,b.num_sequencia";

                //sqlview = sql; //ativa a exibicao do sql na tela

                IfxDataReader reader = new BancoLogix().consultar(sql, conn);
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

                        int qtdSolic = Convert.ToInt32(reader.GetDouble(6));
                        int qtdCancel = Convert.ToInt32(reader.GetDouble(7));
                        int qtdAtend = Convert.ToInt32(reader.GetDouble(8));

                        string nomeItem = reader.GetString(9);
                        string przEntregaS = reader.GetString(10);
                        string codItem = reader.GetString(11);
                        string preUnitS = reader.GetString(12);
                        preUnitS = m.pontoPorVirgula(preUnitS);
                        Decimal preUnit = Decimal.Round(Decimal.Parse(preUnitS), 2);
                        totGeral += (qtdSolic * preUnit);
                        totGeralS = m.formatarDecimal(totGeral);
                        item = new Item(codItem,qtdSolic,qtdCancel,qtdAtend,nomeItem,preUnit,przEntregaS,0,0,0,0,0,0);

                    }

                    itens.Add(item);
                    pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres);
                    pedsEfets.Add(pedEfet);
                }
                
                else
                {
                    String erro = "null";
                    try
                    {
                        erro = new BancoLogix().abrirErros();
                    }
                    catch (Exception ex)
                    {
                        erro = "---" + ex;
                    }

                }
                new BancoLogix().fechar(conn);

                corpoEmail = "<body style=\"background-color:#222;\">Mostrando " + pedsEfets.Count + " resultados, de " + anteontem + " a " + ontem + " -Total R$ " + totGeralS + "<br/>" +
                    " <table style = \"max-width:95%; color:white; font-size: 12px;\">";

                foreach (var pedE in pedsEfets)
                {
                    corpoEmail += "<tr>" +
                            "<td style=\"color:#222;\">" +
                                "-" +
                            "</td>" +
                        "</tr>"+
                        "<thead style = \"background-color: #222; color:white;\">" +
                    "<tr>" +
                        "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> Data </th>" +
                        "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> Unidade </th>" +
                        "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Pedido </th>" +
                        "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> CNPJ </th>" +
                        "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Cliente </th>" +
                        "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Representante </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tr>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.Dat + "</b></td>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.CodEmpresa + "</b></td>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.NumPed + "</b></td>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.CodCliente + "</b></td>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.Cliente + "</b></td>" +
                        "<td style = \"text-align:center;\"><b>" + pedE.Repres + "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td colspan = \"6\">" +
                        "<table style=\"background-color:#3f4142; width:100%; color:white; font-size: 12px;\">" +
                            "<tr>" +
                                "<th style = \"width: 5%;\" > Cod. do Item</th>" +
                                "<th style=\"width: 40%;\">Desc.Item</th>" +
                                "<th style = \"width: 5%;\" > Solic </th>" +
                                "<th style=\"width: 5%;\">Cancel</th>" +
                                "<th style = \"width: 5%;\" > Atend </th>" +
                                "<th style = \"width: 10%;\" > Preço Unit</th>" +
                                "<th style=\"width:10%;\">Preço Total Atend</th>" +
                                "<th style=\"width:10%;\">Preço Total Solic</th>" +
                                "<th style = \"width: 20%;\" > Prazo </th>" +
                            "</tr>";

                    totPed = 0.0m;
                    totPed = 0.0m;
                    totAtend = 0.0m;
                    totPedS = m.formatarDecimal(totPed);
                    totAtendS = m.formatarDecimal(totPed);
                    foreach (var itemV in pedE.Itens)
                    {

                        Decimal preUnit = Decimal.Round(itemV.PrecoUnit, 2);
                        String preUnitS = m.formatarDecimal(preUnit);

                        totPed += preUnit * itemV.QtdSolic;
                        totAtend += preUnit * itemV.QtdAtend;

                        totPedS = m.formatarDecimal(totPed);
                        totAtendS = m.formatarDecimal(totAtend);

                        totPend = totPed - totAtend;
                        totPendS = m.formatarDecimal(totPend);
                        
                        totPedS = m.formatarDecimal(totPed);
                        corpoEmail += "<tr>" +
                            "<td>" + itemV.CodItem + "</td>" +
                            "<td>" + itemV.NomeItem + " </td>" +
                            "<td style = \"text-align:center;\">" + itemV.QtdSolic + " </td>" +
                            "<td style = \"text-align:center;\">" + itemV.QtdCancel + " </td>" +
                            "<td style = \"text-align:center;\">" + itemV.QtdAtend + " </td>" +
                            "<td>R$" + preUnitS + "</td>" +
                            "<td>R$" + totAtend + "</td>" +
                            "<td>R$" + totPed + "</td>" +
                            "<td style = \"text-align:right;\">" + itemV.PrzEntrega + "</td>" +
                        "</tr>";
                    }
                    corpoEmail += "<tr>" +
                                    "<td colspan = \"3\" style=\"background-color: #070a0e; color:white;\"><b>Total Pedido: R$" + totPedS + "</td>" +
                                    "<td colspan = \"3\" style=\"background-color: #070a0e; color:white;\"><b>Total Atendido: R$" + totAtendS + "</td>" +
                                    "<td colspan = \"3\" style=\"background-color: #070a0e; color:white;\"><b>Total Pendente: R$" + totPendS + "</td>" +
                                "</tr>" +
                            "</table>" +
                        "</td>" +
                    "</tr>";
                }
                totGeralS = m.formatarDecimal(totGeral);
                corpoEmail += "</table></body>";


                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ti@similar.ind.br", "Simi1717");
                MailMessage mm = new MailMessage("ti@similar.ind.br", re.email, "Relatorio Logix", corpoEmail);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
        }
    }
}