using System;
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
    public partial class PagePedidosEfetivadosCRM : System.Web.UI.Page
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
        public TimeSpan demora;
        public DateTime dtInicio;
        public DateTime dtFim;
        public Metodos m = new Metodos();
        public String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");
        public string represChange = "nao";

        public List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            //VERIFICACAO DE SESSAO E NIVEL
            if (Session["key"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                //VERFICA NIVEL
                if ((int)Session["key"] >= 1)
                {
                    if ((int)Session["key"] >= 4)
                    {
                        represChange = "sim";
                    }
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
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
                postNumPed = " AND a.CodPed = " + postNumPed + "";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodItem = m.configCoringas(postCodItem);

            postDatInicio = datIni.Value;
            if (postDatInicio == "") postDatInicio = ontem;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            if ((int)Session["key"] >= 4)
            {
                postRepres = repres.Value.ToUpper();
            }
            else
            {
                postRepres = (string)Session["nome"];
                postRepres = postRepres.ToUpper();
            }

            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            dtInicio = DateTime.Now;
            Session["firstJ"] = "0";

            SqlDataReader reader = null;
            //SqlDataReader reader2 = null;

            SqlConnection conn = new BancoAzure().abrir();
            //SqlConnection conn2 = new BancoAzure().abrir();
            postDatFim = m.configDataHuman2Banco(postDatFim);
            postDatInicio = m.configDataHuman2Banco(postDatInicio);
            string sql = "SELECT a.Unidade, a.dat_pedido, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante," +

                               " b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.LgxPedNum, b.Seq" +

                                        " FROM PrePEDIDOS a" +

                                        " INNER JOIN PrePedidosItens b on a.CodPed = b.CodPed" +

                                        " inner join LgxPRODUTOS i on i.cod_item = b.cod_item" +

                                        " WHERE a.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                        " AND a.nom_cliente LIKE '%" + postCliente + "%'" +

                                        " AND a.Representante LIKE '%" + postRepres + "%'" +

                                        " AND a.dat_pedido >= '" + postDatInicio + "'" +
                                        //" AND a.DataUlt >= '2019-03-25 00:00:00'" +

                                        " AND a.dat_pedido <= '" + postDatFim + "'" +
                                        //" AND a.DataUlt <= '2019-03-26 23:59:59'" +

                                        " AND b.cod_item like '%" + postCodItem + "%'" +

                                        " AND a.Unidade LIKE '%" + postUnidade + "%'" +

                                        " AND a.CLProp NOT like 'E%'" +
                                        " AND a.CLProp NOT like 'LAC'" +
                                        " AND a.CLProp NOT like 'LBq'" +
                                        " AND a.CLProp NOT like 'LCd'" +
                                        " AND a.CLProp NOT like 'LEP'" +

                                        postNumPed +

                                        " GROUP BY a.Unidade, a.dat_pedido, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante,b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.LgxPedNum, b.Seq " +
                                        " ORDER BY a.dat_pedido desc, a.CodPed, b.Seq";

            sqlview = sql; //ativa a exibicao do sql na tela
            //String errosql = new BancoAzure().consultarErros(sql,conn);
            reader = new BancoAzure().consultar(sql, conn);
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
                    //numPed = reader.GetString(3);
                    cliente = reader.GetString(4);
                    repres = reader.GetString(5);
                    repres = repres.Substring(0, repres.IndexOf(","));
                    //itens = new List<Item>();
                    //string sql2 = "SELECT b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit" +
                    //"                                                    FROM PrePedidosItens b inner join LgxPRODUTOS i on i.cod_item = b.cod_item" +
                    //"                                                    WHERE b.CodPed = " + numPed;

                    //String errosql = new BancoAzure().consultarErros(sql2, conn);

                    //reader2 = new
                    // BancoAzure().consultar(sql2, conn2);

                    //if (reader2 != null && reader2.HasRows)
                    //{
                    //    while (reader2.Read())
                    //    {
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
                    string preUnitS = Math.Round(preUnitD,2).ToString();
                    Decimal preUnit = Decimal.Parse(preUnitS);

                    //qtdSolic = m.pontoPorVirgula(qtdSolic);
                    //Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                    totGeral += ((Decimal)qtdSolic * preUnit);
                    totGeralS = m.formatarDecimal(totGeral);
                    Double desconto = reader.GetDouble(13);

                    int pedLogix = 0;
                    if (!reader.IsDBNull(14))
                    {
                        pedLogix = reader.GetInt32(14);
                    }
                    
                    item = new Item(qtdSolic.ToString(), qtdCancel.ToString(), qtdAtend.ToString(), nomeItem, przEntregaS, codItem, preUnit, Decimal.Round((((Decimal)desconto)*100m)),pedLogix);

                    //    itens.Add(item);
                    //}

                    //PedidoEfetivado pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                    //pedsEfets.Add(pedEfet);
                    //reader2.Close();
                    //}
                    //new BancoAzure().fechar(conn2);

                    //pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                    //pedsEfets.Add(pedEfet);
                }
                itens.Add(item);
                pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                pedsEfets.Add(pedEfet);

                reader.Close();
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

            new BancoAzure().fechar(conn);

            DateTime dtFim = DateTime.Now;
            demora = dtFim - dtInicio;
        }

    }
}
