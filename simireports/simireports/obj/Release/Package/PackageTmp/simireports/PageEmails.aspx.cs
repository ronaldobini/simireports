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
    public partial class PageEmails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        Metodos m = new Metodos();
        String ontem = DateTime.Today.AddDays(-1).ToString("d");
        String hoje = DateTime.Today.ToString("d");
        List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };
        
        decimal totGeral = 0.0m;
        string totGeralS = "0,00";

            Decimal totPed = 0.0m;
            string totPedS = "";

            String corpoEmail = "";
     
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres " +

                                   " FROM pedidos a" +

                                   " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa" +

                                   " JOIN clientes c on c.cod_cliente = a.cod_cliente" +

                                   " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + "" + "%'" +

                                   " AND c.nom_cliente LIKE '%" + "" + "%'" +

                                   " AND r.nom_repres LIKE '%" + "" + "%'" +

                                   " AND a.dat_alt_sit >= '" + ontem + "'" +

                                   " AND a.dat_alt_sit <= '" + hoje + "'" +

                                   " AND b.cod_item like '%" + "" + "%'" +

                                   " AND a.cod_empresa LIKE '%" + "" + "%'" +

                                   " AND ies_sit_pedido = 'N' AND cod_nat_oper<> 9001" +

                                   "" +

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +
                                   " GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres " +
                                   " ORDER BY a.dat_alt_sit desc, a.num_pedido";

            //sqlview = sql; //ativa a exibicao do sql na tela

            IfxDataReader reader = new BancoLogix().consultar(sql, conn);
            IfxDataReader reader2;

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    DateTime dat = reader.GetDateTime(1);
                    string codCliente = reader.GetString(2);
                    string numPed = reader.GetString(3);
                    string cliente = reader.GetString(4);
                    string repres = reader.GetString(5);
                    List<Item> itens = new List<Item>();

                    //conn2 = new BancoLogix().abrir();
                    reader2 = new
                    BancoLogix().consultar("SELECT b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit" +
                    "                                                    FROM ped_itens b join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa" +
                    "                                                    WHERE b.num_pedido = " + numPed + " and b.cod_empresa = " + codEmpresa, conn);
                    if (reader2 != null)
                    {
                        while (reader2.Read())
                        {
                            string qtdSolic = reader2.GetString(0);
                            string qtdCancel = reader2.GetString(1);
                            string qtdAtend = reader2.GetString(2);
                            string nomeItem = reader2.GetString(3);
                            string przEntregaS = reader2.GetString(4);
                            string codItem = reader2.GetString(5);
                            string preUnitS = reader2.GetString(6);
                            preUnitS = m.pontoPorVirgula(preUnitS);
                            Decimal preUnit = Decimal.Round(Decimal.Parse(preUnitS), 2);
                            qtdSolic = m.pontoPorVirgula(qtdSolic);
                            Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                            totGeral += (qtdSolicD * preUnit);
                            totGeralS = m.formatarDecimal(totGeral);
                            Item item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);
                            itens.Add(item);
                        }

                        PedidoEfetivado pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                        pedsEfets.Add(pedEfet);
                    }
                    //new BancoLogix().fechar(conn2);
                    reader2.Close();

                }
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
            corpoEmail = "<body style=\"background-color:#222;\">Mostrando " + pedsEfets.Count + "resultados, de " + ontem + " a " + hoje + " -Total R$ " + totGeralS + "<br/>" +
                " <table bgcolor = \"\" style = \"max-width:95%; color:white; font-size: 12px;\">";
                
            foreach (var pedEfet in pedsEfets)
            {
                corpoEmail += " <tr style=\"background-color:#222;\">" +
                        "<td>" +
                            "-" +
                        "</td>" +
                    "</tr>  " +
                    "<thead style = \"background-color: #070a0e; color:white;\">" +
                "<tr>" +
                    "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> Data </th>" +
                    "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> Unidade </th>" +
                    "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Pedido </th>" +
                    "<th scope = \"col\"style = \"width: 5%; text-align:center;background-color: #070a0e;\"> CNPJ </th>" +
                    "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Cliente </th>" +
                    "<th scope = \"col\"style = \"width: 10%; text-align:center;background-color: #070a0e;\"> Representante </th>" +
                "</thead>" +
                "</tr>" +
                "<tr>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.Dat + "</b></td>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.CodEmpresa + "</b></td>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.NumPed + "</b></td>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.CodCliente + "</b></td>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.Cliente + "</b></td>" +
                    "<td style = \"text-align:center;\"><b>" + pedEfet.Repres + "</b></td>" +
                "</tr>" +
                "<tr>" +
                    "<td colspan = \"6\">" +
                    "<table style=\"background-color:#3f4142; width:100%; color:white; font-size: 12px;\">" +
                        "<tr>" +
                            "<th style = \"width: 10%;\" > Cod. do Item</th>" +
                            "<th style = \"width: 5%;\" > Solic </th>" +
                            "<th style=\"width: 5%;\">Cancel</th>" +
                            "<th style = \"width: 5%;\" > Atend </th>" +
                            "<th style=\"width: 55%;\">Desc.Item</th>" +
                            "<th style = \"width: 10%;\" > Preço Unit</th>" +
                            "<th style = \"width: 20%;\" > Prazo </th>" +
                        "</tr>" +
                        "</thead>";

                totPed = 0.0m;
                totPedS = m.formatarDecimal(totPed);
                foreach (var item in pedEfet.Itens)
                {
                    string qtdSolic = m.pontoPorVirgula(item.QtdSolic);
                    Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);

                    string qtdCancel = m.pontoPorVirgula(item.QtdCancel);
                    Decimal qtdCancelD = Decimal.Round(Decimal.Parse(qtdCancel), 0);

                    string qtdAtend = m.pontoPorVirgula(item.QtdAtend);
                    Decimal qtdAtendD = Decimal.Round(Decimal.Parse(qtdAtend), 0);

                    Decimal preUnit = Decimal.Round(item.PrecoUnit, 2);
                    String preUnitS = m.formatarDecimal(preUnit);

                    totPed += preUnit* qtdSolicD;
                    totPedS = m.formatarDecimal(totPed);
                    corpoEmail += "<tr>" +
                        "<td>"+item.CodItem+"</td>" +
                        "<td style = \"text-align:center;\">" + qtdSolicD +" </td>" +
                        "<td style = \"text-align:center;\">" + qtdCancelD +" </td>" +
                        "<td style = \"text-align:center;\">" + qtdAtendD +" </td>" +
                        "<td>" + item.NomeItem+" </td>" +
                        "<td style = \"text-align:right;\">" + "R$" + preUnitS + "</td>" +
                        "<td style = \"text-align:right;\">" + item.PrzEntrega+"</td>" +
                    "</tr>";
                }
                corpoEmail += "<tr>" +
                                "<td colspan = \"7\" style=\"background-color: #070a0e; color:white;\"><b>Total Pedido: R$"+ totPedS +"</td>" +
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
            MailMessage mm = new MailMessage("ti@similar.ind.br", "ti@similar.ind.br", "relatorio kkkkkkkkkkk", corpoEmail);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = true;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}