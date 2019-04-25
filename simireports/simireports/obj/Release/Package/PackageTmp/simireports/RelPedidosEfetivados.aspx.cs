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


        public Metodos m = new Metodos();
        public String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");        
        public string represChange = "nao";

        public List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

        protected void Page_Load(object sender, EventArgs e)
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

            postDatInicio = datIni.Value;
            if (postDatInicio == "") postDatInicio = ontem;

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

            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, " +
                " b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit, b.num_sequencia "+
                                   " FROM pedidos a" +
                                    " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa" +
                                    " JOIN clientes c on c.cod_cliente = a.cod_cliente" +
                                    " join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa " +
                                   " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                   " AND c.nom_cliente LIKE '%" + postCliente + "%'" +

                                   " AND r.nom_repres LIKE '%" + postRepres + "%'" +

                                   " AND a.dat_alt_sit >= '" + postDatInicio + "'" +

                                   " AND a.dat_alt_sit <= '" + postDatFim + "'" +

                                   " AND b.cod_item like '%" + postCodItem + "%'" +

                                   " AND a.cod_empresa LIKE '%" + postUnidade +"%'" +

                                   " AND ies_sit_pedido = 'N' AND cod_nat_oper<> 9001" +

                                   postNumPed + 

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +
                                   " GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres, b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit,b.num_sequencia" +
                                   " ORDER BY a.dat_alt_sit desc, a.num_pedido,b.num_sequencia";

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
            string numPed = "";
            Item item = null;
            bool primeiro = true;
            PedidoEfetivado pedEfet = null;

            //string errosql = new BancoLogix().consultarErros(sql,conn);


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

                    //conn2 = new BancoLogix().abrir();
                    //reader2 = new
                    //BancoLogix().consultar("SELECT b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit" +
                    //"                                                    FROM ped_itens b join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa" +
                    //"                                                    WHERE b.num_pedido = " + numPed + " and b.cod_empresa = " + codEmpresa, conn);
                    //if (reader2 != null)
                    //{
                    //    while (reader2.Read())
                    //    {
                    
                            string qtdSolic = reader.GetString(6);
                            string qtdCancel = reader.GetString(7);
                            string qtdAtend = reader.GetString(8);
                            string nomeItem = reader.GetString(9);
                            string przEntregaS = reader.GetString(10);
                            string codItem = reader.GetString(11);
                            string preUnitS = reader.GetString(12);
                            preUnitS = m.pontoPorVirgula(preUnitS);                            
                            Decimal preUnit = Decimal.Round(Decimal.Parse(preUnitS),2);
                            qtdSolic = m.pontoPorVirgula(qtdSolic);
                            Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                            totGeral += (qtdSolicD * preUnit);
                            totGeralS = m.formatarDecimal(totGeral);
                            item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);


                    //}

                    //new BancoLogix().fechar(conn2);
                    //reader2.Close();

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

        }

    }
}
