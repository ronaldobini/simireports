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
    public partial class PageComissoes : System.Web.UI.Page
    {
        public static int first = 1;
        public static string postDatInicio = "2019-01-22 00:00:00";
        public static string postRepres = "";

        public List<Comissao> comissoes = new List<Comissao> { };





        protected void Page_Load(object sender, EventArgs e)
        {

            if (first == 1)
            {
                first = 0;
                executarRelatorio();
            }
        }




        protected void filtrarComiss_Click(object sender, EventArgs e)
        {
            postDatInicio = datInicio.Value + " 00:00:00";
            postRepres = repres.Value;
            executarRelatorio();

        }


        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            IfxDataReader reader = new BancoLogix().consultar("select nfm.nota_fiscal, nfi.pedido, nfi.item, cl.nom_cliente, nfi.des_item, " +
                    "nfi.qtd_item, nfi.preco_unit_bruto, p.pct_comissao, r.nom_repres,p.dat_alt_sit, nfm.dat_hor_emissao " +
                    "from fat_nf_item nfi " +
                    "join pedidos p on p.num_pedido = nfi.pedido and p.cod_empresa = nfi.empresa " +
                    "join representante r on r.cod_repres = p.cod_repres " +
                    "join fat_nf_mestre nfm on nfi.trans_nota_fiscal = nfm.trans_nota_fiscal and nfm.empresa = nfi.empresa " +
                    "join clientes cl on cl.cod_cliente = p.cod_cliente " +
                    "WHERE nfm.dat_hor_emissao >= '" + postDatInicio + "' " +
                    "AND r.nom_repres LIKE '%" + postRepres + "%'" +
                    "order by nfm.dat_hor_emissao desc", conn);

            while (reader.Read())
            {
                int notaFiscal = reader.GetInt32(0);
                int numPed = reader.GetInt32(1);
                string item = reader.GetString(2);
                string nomCliente = reader.GetString(3);
                string desItem = reader.GetString(4);
                string qtdItem = reader.GetString(5);
                if (qtdItem.Contains("."))
                    qtdItem = qtdItem.Replace(".", ",");
                string precoUnitBruto = reader.GetString(6);
                if (precoUnitBruto.Contains("."))
                    precoUnitBruto = precoUnitBruto.Replace(".", ",");
                double preTotal = float.Parse(qtdItem) * float.Parse(precoUnitBruto);
                string pctComissao = reader.GetString(7);
                if (pctComissao.Contains("."))
                    pctComissao = pctComissao.Replace(".", ",");
                double comiss = (float.Parse(pctComissao) / 100) * preTotal;
                string nomRepres = reader.GetString(8);
                DateTime datAltSit = reader.GetDateTime(9);
                DateTime datHorEmiss = reader.GetDateTime(10);

                Comissao comissao = new Comissao(notaFiscal, numPed, item, nomCliente, desItem, qtdItem, precoUnitBruto, preTotal, pctComissao, comiss, nomRepres, datAltSit, datHorEmiss);
                comissoes.Add(comissao);
            }
        }






    }
}