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
        public static string postDatInicio = "23/01/2019";
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
            postDatInicio = datInicio.Value;
            postRepres = repres.Value;
            executarRelatorio();

        }
        
        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            //IfxDataReader reader = new BancoLogix().consultar
            //    ("SELECT num_pedido, dat_alt_sit, pct_comissao, cod_repres, cod_cliente, cod_empresa " +
            //    "FROM pedidos " +
            //    "WHERE dat_alt_sit >= '21/01/2019' order by dat_alt_sit desc", conn);
            IfxDataReader reader = new BancoLogix().consultar("select d.cod_empresa," +
                                                                "     d.num_docum," +
                                                                "     d.num_docum_origem," +
                                                                "     d.num_pedido," +
                                                                "     cl.nom_cliente," +
                                                                "     d.val_bruto," +
                                                                "     d.pct_comis_1," +
                                                                "     r.nom_repres," +
                                                                "     d.dat_emis," +
                                                                "     d.dat_vencto_s_desc," +
                                                                "     d.ies_pgto_docum " +
                                                                "from docum d " +
                                                                "join representante r on r.cod_repres = d.cod_repres_1 " +
                                                                "join clientes cl on cl.cod_cliente = d.cod_cliente " +
                                                                "where d.dat_vencto_s_desc = '"+ postDatInicio +"' AND r.nom_repres like '%"+ postRepres +"%' AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' order by d.dat_vencto_s_desc desc", conn);
            while (reader.Read())
            {
                string codEmpresa = reader.GetString(0);
                string numDocum = reader.GetString(1);
                string numDocumOrigem = reader.GetString(2);
                string numPedido = reader.GetString(3);
                string nomCliente = reader.GetString(4);

                string valBrutoS = reader.GetString(5);
                if (valBrutoS.Contains("."))
                    valBrutoS = valBrutoS.Replace(".", ",");

                string pctComissaoS = reader.GetString(6);
                if (pctComissaoS.Contains("."))
                    pctComissaoS = pctComissaoS.Replace(".", ",");

                Decimal valBruto = Decimal.Parse(valBrutoS);
                Decimal pctComissao;
                Decimal comiss;
                if (!pctComissaoS.Equals("")) {
                    pctComissao = Decimal.Parse(pctComissaoS);
                    comiss = valBruto * (pctComissao / 100);
                }
                else
                {
                    pctComissao = 0.0M;
                    comiss = 0.0M;
                }

                string nomRepres = reader.GetString(7);
                DateTime datEmiss = reader.GetDateTime(8);
                DateTime datVcto = reader.GetDateTime(9);
                char iesPgtoDocum = reader.GetChar(10);

                Comissao comissao = new Comissao(codEmpresa, numDocum, numDocumOrigem, numPedido, nomCliente, valBruto, pctComissao, comiss, nomRepres, datEmiss, datVcto, iesPgtoDocum);
                comissoes.Add(comissao);
            }
        }
    }
}