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
                
        public string postDatInicio = "";
        public string postDatFim = "";
        public string postCliente = "";
        public string postPctComiss = "";
        public string postValor = "";
        public string postUnidade = "";
        public string postRepres = "";
        public string postSitPgto = "T";
        public string sqlview = "";

        public Metodos m = new Metodos();
        public String mesPassado = DateTime.Today.AddMonths(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");
        public string represChange = "nao";
        


        public static Decimal totComiss = 0.0M;
        public static String totComissS = "";
        public String erro = " ";


        public List<Comissao> comissoes = new List<Comissao> { };
        
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
                    erro = "Você não tem permissão para esta pagina";
                    Response.Redirect("index.aspx");
                }
            }

            
            //first execution
            if ((int)Session["first"] == 1)
            {
                Session["first"] = 0;
                //executarRelatorio();
            }
        }

        protected void filtrarComiss_Click(object sender, EventArgs e)
        {
            postDatInicio = datInicio.Value;
            if (postDatInicio == "") postDatInicio = mesPassado;
            
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
            
            postCliente = cliente.Value.ToUpper();
            postUnidade = unidade.Value;


            postSitPgto = sitPgto.Value.ToUpper();
            executarRelatorio();
        }
        
        protected void executarRelatorio()
        {
            postCliente = m.configCoringas(postCliente);
            postRepres = m.configCoringas(postRepres);
            
            IfxConnection conn = new BancoLogix().abrir();    
            string sql = "SELECT d.cod_empresa," +
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
                                        "WHERE r.nom_repres like '%" + postRepres + "%' " +
                                        "AND dp.dat_pgto >= '" + postDatInicio + "' " +
                                        "AND dp.dat_pgto <= '" + postDatFim + "' " +
                                        "AND ies_pgto_docum = '" + postSitPgto + "' " +
                                        "AND cl.nom_cliente like '%" + postCliente + "%' " +
                                        //"AND d.pct_comis_1 = " + postPctComiss + " " +
                                        //"AND d.val_bruto like " + postValor + " " +
                                        "AND d.cod_empresa like '%" + postUnidade + "%' " +
                                        "AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' order by d.cod_repres_1";

            //sqlview = sql; //ativa a exibicao do sql na tela
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totComiss = 0.0M;
            if (reader != null && reader.HasRows)
            {
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
                    
                    if (!pctComissaoS.Equals(""))
                    {
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
                totComiss = decimal.Round(totComiss, 2);
                totComissS = m.formatarDecimal(totComiss);
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
                        comissoes.Add(comissao);
                    }
                }
                catch(Exception ex)
                {
                    erro = "---" + ex;
                    Comissao comissao = new Comissao("NULL", erro, "-", "-", "-", 0, 0, 0, "-", new DateTime(), new DateTime(), new DateTime(), 'T', "-");
                    comissoes.Add(comissao);
                }
                
            }
        }
        
    }
}