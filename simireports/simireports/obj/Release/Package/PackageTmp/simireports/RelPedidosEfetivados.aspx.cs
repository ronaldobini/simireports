﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IBM.Data.Informix;
using simireports.simireports.Classes;

namespace simireports
{
    public partial class PagePedidosEfetivados : System.Web.UI.Page
    {

        public string postDatInicio = "";
        public string postDatFim = "";
        public string postUnidade = "";
        public string postCodCliente = "";
        public string postCliente = "";
        public string postRepres = "";
        public string postNumPed = "";
        public string postCodItem = "";
        public string postPreUnit = "";
        public string sqlview = "";
        public decimal totGeral = 0.0m;
        public string totGeralS = "0,00";
        public decimal totGeralP = 0.0m;
        public string totGeralPS = "0,00";
        public decimal totGeralA = 0.0m;
        public string totGeralAS = "0,00";
        public string postSit = "";
        public string postFam = "";

        public Boolean fPostAberto = false;
        public Boolean fPedAberto = false;
        public Boolean fPostItem = false;
        public Boolean fItem = false;

        public Metodos m = new Metodos();
        public String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");
        public string represChange = "nao";
        public string tipoData = "dat_alt_sit";

        public List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            //VERIFICACAO DE SESSAO E NIVEL
            if (Session["key"] != null)
            {
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //VERFICA NIVEL
                    if ((int)Session["key"] >= 1)
                    {
                        if ((int)Session["key"] >= 2)
                        {
                            represChange = "sim";
                        }
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }


            if ((int)Session["first"] == 1)
            {
                postRepres = (string)Session["nome"];
                postRepres = postRepres.ToUpper();
                Session["first"] = 0;
                //executarRelatorio();
            }


        }



        protected void filtrarPedEfet_Click(object sender, EventArgs e)
        {
            postUnidade = unidade.Value;
            postCodCliente = codCliente.Value;

            postCodCliente = m.configCoringas(postCodCliente);

            postCliente = cliente.Value.ToUpper();
            postCliente = m.configCoringas(postCliente);


            postNumPed = numPed.Value;
            if (!postNumPed.Equals(""))
            {
                postNumPed = " AND a.num_pedido = " + postNumPed + "";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodItem = m.configCoringas(postCodItem);

            //if(postCodItem.Length > 0)
            //{
            //    fPostItem = true;
            //}

            postDatInicio = datIni.Value;
            if (postDatInicio == "") postDatInicio = hoje;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            if ((int)Session["key"] >= 2)
            {
                postRepres = repres.Value.ToUpper();
            }
            else
            {
                postRepres = (string)Session["nome"];
                postRepres = postRepres.ToUpper();
            }

            postSit = openclose.Value;

            if (postSit == "-1") //TODOS
            {
                postSit = " ";
                tipoData = "dat_alt_sit";
            }
            else if (postSit == "0") //EFETIVADOS
            {
                postSit = " AND ies_sit_pedido = 'N' ";
                tipoData = "dat_alt_sit";
            }
            else if (postSit == "1") //ABERTO
            {
                postSit = " AND ies_sit_pedido = 'N' ";
                fPostAberto = true;
                tipoData = "dat_alt_sit";
            }
            else if (postSit == "2") //FECHADO
            {
                postSit = " AND ies_sit_pedido = 'N' AND b.qtd_pecas_solic = (b.qtd_pecas_atend+b.qtd_pecas_cancel)";
                tipoData = "dat_alt_sit";
            }
            else if (postSit == "3") //ATRASADO
            {
                fPostAberto = true;
                postSit = " AND ies_sit_pedido = 'N' AND b.prz_entrega < '" + hoje+"'";
                tipoData = "dat_alt_sit";
            }
            else if (postSit == "4") //REPROVADO
            {
                postSit = " AND ies_sit_pedido = 'R' ";
                tipoData = "dat_pedido";
            }
            else if (postSit == "5") //CANCELADO
            {
                postSit = " AND ies_sit_pedido = '9' ";
                tipoData = "dat_cancel";
            }


            postFam = familia.Value;
            if (postFam == "00")
            {
                postFam = "";
            }
            else if (postFam == "01")
            {
                postFam = " AND i.cod_familia = '01'";
            }
            else if (postFam == "02")
            {
                postFam = " AND i.cod_familia = '02'";
            }
            else if (postFam == "03")
            {
                postFam = " AND (i.cod_familia = '03' or i.cod_familia = '30' or i.cod_familia = '31' or " +
                                "i.cod_familia = '32' or i.cod_familia = '33' or i.cod_familia = '97')";
            }
            else if (postFam == "09")
            {
                postFam = " AND i.cod_familia = '09'";
            }
            else if (postFam == "34")
            {
                postFam = " AND i.cod_familia = '34'";
            }
            else if (postFam == "50")
            {
                postFam = " AND i.cod_familia = '50'";
            }
            else if (postFam == "99")
            {
                postFam = " AND i.cod_familia = '99'";
            }
            else
            {
                postFam = "";
            }

            executarRelatorio();
        }

        protected void executarRelatorio()
        {

            Session["firstJ"] = "0";
            IfxConnection conn = new BancoLogix().abrir();

            string sql = "SELECT a.cod_empresa, a."+ tipoData + ", a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, " +
                " b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit, b.num_sequencia, a.num_pedido_cli " +
                                   " FROM pedidos a" +
                                    " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa " +
                                    " JOIN clientes c on c.cod_cliente = a.cod_cliente" +
                                    " JOIN item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa " +
                                    " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                   " AND c.nom_cliente LIKE '%" + postCliente + "%'" +

                                   " AND r.nom_repres LIKE '%" + postRepres + "%'" +

                                   " AND a."+ tipoData + " >= '" + postDatInicio + "'" +

                                   " AND a." + tipoData + " <= '" + postDatFim + "'" +

                                   " AND a." + tipoData + " >= '01/01/2016'" +

                                   " AND b.cod_item LIKE '%" + postCodItem + "%'" +

                                   " AND a.cod_empresa LIKE '%" + postUnidade + "%'" +

                                   postNumPed +

                                   postSit +

                                   postFam +

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +
                                   " GROUP BY a.cod_empresa, a." + tipoData + ", a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit,b.num_sequencia,a.num_pedido_cli" +
                                   " ORDER BY a." + tipoData + " desc, a.num_pedido,b.num_sequencia";

            //sqlview = sql; //ativa a exibicao do sql na tela

            IfxDataReader reader = new BancoLogix().consultar(sql, conn);
            //IfxDataReader reader2;
            List<Item> itens = new List<Item>();
            string pedAnt = "zaburska";

            string codEmpresa = "";
            DateTime dat = new DateTime();
            string codCliente = "";
            string cliente = "";
            string repres = "";
            string pedCli = "";
            string numPed = "";
            Item item = null;
            bool primeiro = true;
            PedidoEfetivado pedEfet = null;

            //string errosql = new BancoLogix().consultarErros(sql,conn);


            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel PedEfetiv", (string)Session["nome"], postRepres + " | " + postDatInicio + " | " + postCliente);
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
                            if (fPostAberto) //USUARIO QUER ABERTOS
                            {
                                if (fPedAberto) //ADD PEDIDO SÓ SE TIVER ABERTO
                                {
                                    pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres, pedCli);
                                    pedsEfets.Add(pedEfet);
                                    fPedAberto = false;
                                }
                            }
                            else //FLUXO NORMAL
                            {
                                pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres, pedCli);
                                pedsEfets.Add(pedEfet);
                            }
                            itens = new List<Item>();
                            pedAnt = numPed;
                        }
                    }


                    codEmpresa = reader.GetString(0);
                    dat = reader.GetDateTime(1);
                    codCliente = reader.GetString(2);
                    cliente = reader.GetString(4);
                    repres = reader.GetString(5);
                    pedCli = reader.GetString(14);


                    string qtdSolic = reader.GetString(6);
                    string qtdCancel = reader.GetString(7);
                    string qtdAtend = reader.GetString(8);
                    string nomeItem = reader.GetString(9);
                    string przEntregaS = reader.GetString(10);
                    string codItem = reader.GetString(11);
                    string preUnitS = reader.GetString(12);


                    preUnitS = m.pontoPorVirgula(preUnitS);
                    Decimal preUnit = Decimal.Round(Decimal.Parse(preUnitS), 2);
                    qtdSolic = m.pontoPorVirgula(qtdSolic);
                    qtdCancel = m.pontoPorVirgula(qtdCancel);
                    qtdAtend = m.pontoPorVirgula(qtdAtend);
                    Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                    Decimal qtdCancelD = Decimal.Round(Decimal.Parse(qtdCancel), 0);
                    Decimal qtdAtendD = Decimal.Round(Decimal.Parse(qtdAtend), 0);
                    totGeral += (qtdSolicD * preUnit);
                    totGeralP += ((qtdSolicD - qtdCancelD - qtdAtendD) * preUnit);
                    totGeralA += (qtdAtendD * preUnit);


                    if (qtdSolicD > (qtdAtendD + qtdCancelD))
                    {
                        fPedAberto = true;
                    }

                    if (codItem == postCodItem)
                    {
                        fItem = true;
                    }

                    item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);


                }
                totGeralS = m.formatarDecimal(totGeral);
                totGeralPS = m.formatarDecimal(totGeralP);
                totGeralAS = m.formatarDecimal(totGeralA);


                // ULTIMO PEDIDO DO SELECT
                itens.Add(item);
                if (fPostAberto) //SÓ ABERTOS
                {
                    if (fPedAberto) //ADD PEDIDO SÓ SE TIVER ABERTO
                    {
                        pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres, pedCli);
                        pedsEfets.Add(pedEfet);
                        fPedAberto = false;
                    }
                }
                else //FLUXO NORMAL
                {
                    pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres, pedCli);
                    pedsEfets.Add(pedEfet);
                }
                //----------

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

        }

    }
}
