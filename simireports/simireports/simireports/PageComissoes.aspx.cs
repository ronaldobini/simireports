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
        public static int key = 0;


        public List<Comissao> comissoes = new List<Comissao> { };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            IfxConnection conn = new BancoLogix().abrir();
            //IfxDataReader reader = new BancoLogix().consultar
            //    ("SELECT num_pedido, dat_alt_sit, pct_comissao, cod_repres, cod_cliente, cod_empresa " +
            //    "FROM pedidos " +
            //    "WHERE dat_alt_sit >= '21/01/2019' order by dat_alt_sit desc", conn);
            IfxDataReader reader = new BancoLogix().consultar("select nfm.nota_fiscal, nfi.pedido, nfi.item, cl.nom_cliente, nfi.des_item, nfi.qtd_item, nfi.preco_unit_bruto, nfi.qtd_item*nfi.preco_unit_bruto as pre_total,p.pct_comissao, (p.pct_comissao / 100) * (nfi.qtd_item * nfi.preco_unit_bruto) as comiss, r.nom_repres,p.dat_alt_sit, nfm.dat_hor_emissao from fat_nf_item nfi join pedidos p on p.num_pedido = nfi.pedido and p.cod_empresa = nfi.empresa join representante r on r.cod_repres = p.cod_repres join fat_nf_mestre nfm on nfi.trans_nota_fiscal = nfm.trans_nota_fiscal and nfm.empresa = nfi.empresa join clientes cl on cl.cod_cliente = p.cod_cliente order by nfm.dat_hor_emissao desc",conn);
            while (reader.Read())
            {
                int notaFiscal = reader.GetInt32(0);
                int numPed = reader.GetInt32(1);                
                string item = reader.GetString(2);
                string nomCliente = reader.GetString(3);
                string desItem = reader.GetString(4);
                string qtdItem = reader.GetString(5);
                string precoUnitBruto = reader.GetString(6);
                double preTotal = reader.GetDouble(7);
                string pctComissao = reader.GetString(8);
                double comiss = reader.GetDouble(9);
                string nomRepres = reader.GetString(10);
                DateTime datAltSit = reader.GetDateTime(11);
                DateTime datHorEmiss = reader.GetDateTime(12);

                Comissao comissao = new Comissao(notaFiscal,numPed,item,nomCliente,desItem,qtdItem,precoUnitBruto,preTotal,pctComissao, comiss, nomRepres,datAltSit,datHorEmiss);
                comissoes.Add(comissao);
            }



        }
    }
}