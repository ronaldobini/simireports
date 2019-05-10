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
    public partial class RelFaturamento : System.Web.UI.Page
    {

        public string postDatInicio = "";
        public string postDatFim = "";
        public string postUnidade = "";
        public string postNota = "";
        public string postItem = "";
        public string postDesc = "";
        public string postCliente = "";
        public string postNomCli = "";
        public string postNatur = "";
        public string postPed = "";
        public string postPedCli = "";
        public string postTrans = "";
        public string sqlview = "";

        public Metodos m = new Metodos();
        public String mesPassado = "25/04/2019";
        public String hoje = "22/05/2019";
        public string represChange = "nao";



        public static Decimal totFat = 0.0M;
        public static String totFatS = "";
        public String erro = " ";


        public List<Faturamento> fats = new List<Faturamento> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["key"] != null)
            {
                //VERIFICACAO DE SESSAO E NIVEL
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //VERFICA NIVEL
                    if ((int)Session["key"] >= 1)
                    {
                        if ((int)Session["key"] >= 5)
                        {
                            represChange = "sim";
                        }
                    }
                    else
                    {
                        erro = "Você não tem permissão para esta pagina";
                        Response.Redirect("index.aspx");
                    }
                }

            }
            else
            {

                Response.Redirect("login.aspx");
            }

        }

        protected void detalhes_Click(object sender, EventArgs e)
        {

        }

        protected void filtrarComiss_Click(object sender, EventArgs e)
        {
            postDatInicio = datInicio.Value;
            if (postDatInicio == "") postDatInicio = mesPassado;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            postUnidade = empresa.Value;
            postNota = nota.Value;
            postItem = codItem.Value;
            postDesc = descItem.Value;
            postCliente = cliente.Value;
            postNomCli = nomCli.Value;
            postNatur = natureza.Value;
            postPed = pedido.Value;
            postPedCli = pedCli.Value;
            postTrans = trans.Value;

            if (!postNatur.Equals("Todas"))
            {
                postNatur = " AND nfi.natureza_operacao = " + postNatur + " ";
            }
            else
            {
                postNatur = " AND nfi.natureza_operacao in (2001,2021,4001,4021,7000,9001) ";
            }

            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";
            postNota = m.configCoringas(postNota);
            postItem = m.configCoringas(postItem);
            postDesc = m.configCoringas(postDesc);
            postCliente = m.configCoringas(postCliente);
            postNomCli = m.configCoringas(postNomCli);

            IfxConnection conn = new BancoLogix().abrir();
            string sql = "select nf.dat_hor_emissao," +
                " nfi.empresa," +
                " nf.nota_fiscal," +
                " nfi.item," +
                " it.den_item_reduz," +
                " cli.cliente, " +
                " cli.nom_cliente, " +
                " nfi.preco_unit_liquido as valor_unit," +
                " nfi.qtd_item," +
                " nfi.val_bruto_item as valor_total," +
                " nfi.natureza_operacao," +
                " nfi.pedido," +
                " ped.num_pedido_cli," +
                " ped.cod_transpor " +
                " from fat_nf_mestre nf" +
                " JOIN fat_nf_item nfi ON(nf.trans_nota_fiscal = nfi.trans_nota_fiscal)" +
                " JOIN item it ON(nfi.item = it.cod_item and nfi.empresa = it.cod_empresa)" +
                " JOIN pedidos ped ON(nfi.pedido = ped.num_pedido and nfi.empresa = ped.cod_empresa)" +
                " JOIN clientes cli on(nf.cliente = cli.cod_cliente)" +
                " where nf.dat_hor_emissao >= '"+ postDatInicio +
                " AND nf.dat_hor_emissao <= '" + postDatFim +
                " AND nf.sit_nota_fiscal = 'N'" +
                " AND nfi.empresa = " + postUnidade + " " +
                " AND nfi.item like'" + postItem + "' " +
                " AND nfi.den_item_reduz like'" + postDesc + "' " +
                " AND cli.cod_cliente like'" + postCliente + "' " +
                " AND cli.nom_cliente like'" + postNomCli + "' " +
                postNatur + 
                " AND nfi.pedido like '" + postPed + "' " +
                " AND ped.num_pedido_cli like '" + postPedCli + "' " +
                " AND pped.cod_transpor like '" + postTrans + "' " +
                " ORDER BY nf.dat_hor_emissao, empresa, nota_fiscal desc ";

            //sqlview = sql; //ativa a exibicao do sql na tela
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totFat = 0.0M;

            DateTime dat = new DateTime();
            string empresa = "";
            string nota = "";
            string codItem = "";
            string descItem = "";
            string cpfCli = "";
            string cliente = "";
            string nat = "";
            string pedido = "";
            string pedCli = "";
            string trans = "";

            List<Item> itens = new List<Item>();
            Item item = null;
            Faturamento fat = null;
            bool primeiro = true;
            string notAnt = "";

            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Faturamento", (string)Session["nome"], postDatInicio + " | " + postCliente);
                while (reader.Read())
                {

                    nota = reader.GetString(2);

                    if (primeiro)
                    {
                        notAnt = reader.GetString(2);
                        primeiro = false;
                    }
                    else
                    {
                        if (nota.Equals(notAnt))
                        {
                            itens.Add(item);
                        }
                        else
                        {
                            itens.Add(item);
                            fat = new Faturamento(empresa,nota,itens,natureza,pedido,pedCli,cliente,nomCli,trans);
                            fats.Add(fat);
                            itens = new List<Item>();
                            notAnt = nota;
                        }
                    }
                    //" nfi.preco_unit_liquido as valor_unit," +
                    //" nfi.qtd_item," +
                    //" nfi.val_bruto_item as valor_total," +
                    dat = new DateTime();
                    empresa = "";
                    nota = "";
                    codItem = "";
                    descItem = "";
                    cpfCli = "";
                    cliente = "";
                    nat = "";
                    pedido = "";
                    pedCli = "";
                    trans = "";
                    item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);
                }
                totGeralS = m.formatarDecimal(totGeral);

                itens.Add(item);
                fat = new Faturamento(empresa, nota, itens, natureza, pedido, pedCli, cliente, nomCli, trans);
                fats.Add(fat);
            }
            else
            {
                String erro = "null";
                try
                {
                    erro = new BancoLogix().abrirErros();
                    if (!erro.Equals("sem erros"))
                    {
                        Comissao comissao = new Comissao("NULL", erro, "-", "-", "-", 0, 0, 0, "-", new DateTime(), new DateTime(), new DateTime(), 'T', "-");
                        //comissoes.Add(comissao);
                    }
                }
                catch (Exception ex)
                {
                    erro = "---" + ex;
                    Comissao comissao = new Comissao("NULL", erro, "-", "-", "-", 0, 0, 0, "-", new DateTime(), new DateTime(), new DateTime(), 'T', "-");
                    //comissoes.Add(comissao);
                }

            }
            new BancoLogix().fechar(conn);
        }

    }
}