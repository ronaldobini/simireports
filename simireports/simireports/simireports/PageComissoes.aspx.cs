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

        public static string postDatInicio = "01/01/2019";
        public static string postDatFim = "31/01/2019";
        public static string postRepres = "";
        public static string postSitPgto = "T";


        public static decimal totComiss = 0.0M;

        

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
            postDatFim = datFim.Value;
            postRepres = repres.Value.ToUpper();
            postSitPgto = sitPgto.Value.ToUpper();
            executarRelatorio();
        }
        
        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();           
            IfxDataReader reader = new 
            BancoLogix().consultar("SELECT d.cod_empresa," +
                                        "d.num_docum," +
                                        "d.num_docum_origem," +
                                        "d.num_pedido," +
                                        "cl.nom_cliente," +
                                        "d.val_bruto," +
                                        "d.pct_comis_1," +
                                        "r.nom_repres," +
                                        "d.dat_emis," +
                                        "d.dat_vencto_s_desc," +
                                        "dp.dat_pgto," +
                                        "d.ies_pgto_docum, " +
                                        "d.cod_repres_1 " +
                                        "FROM docum d " +
                                        "JOIN representante r on r.cod_repres = d.cod_repres_1 " +
                                        "JOIN clientes cl on cl.cod_cliente = d.cod_cliente " +
                                        "LEFT JOIN docum_pgto dp on d.num_docum = dp.num_docum and d.cod_empresa = dp.cod_empresa " +
                                        "WHERE dp.dat_pgto >= '" + postDatInicio + "' " +
                                        "AND dp.dat_pgto <= '" + postDatFim + "' " +
                                        "AND r.nom_repres like '%" + postRepres +"%' " +
                                        "AND ies_pgto_docum = '"+postSitPgto+"' " +
                                        "AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' order by d.cod_repres_1", conn);

            totComiss = 0.0M;
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
                        if (pctComissao == 3.8m)
                        {
                            //pctComissao = 1.5m;
                        }
                        comiss = valBruto * (pctComissao / 100);
                    }
                    else
                    {
                        pctComissao = 0.0M;
                        comiss = 0.0M;
                    }

                    totComiss = totComiss + comiss;

                    string nomRepres = reader.GetString(7);
                    DateTime datEmiss = reader.GetDateTime(8);
                    DateTime datVcto = reader.GetDateTime(9);
                    DateTime datPgto = new DateTime();

                    if (postSitPgto == "T")
                    {
                        datPgto = reader.GetDateTime(10);
                    }
                    
                    char iesPgtoDocum = reader.GetChar(11);
                    string codRepres = reader.GetString(12);

                    Comissao comissao = new Comissao(codEmpresa, numDocum, numDocumOrigem, numPedido, nomCliente, valBruto, pctComissao, comiss, nomRepres, datEmiss, datVcto, datPgto, iesPgtoDocum, codRepres);
                    comissoes.Add(comissao);
                    
                }
        }






    }
}