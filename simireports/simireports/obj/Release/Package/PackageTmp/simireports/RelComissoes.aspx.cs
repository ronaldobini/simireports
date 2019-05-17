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
        public string postSitPgto = "";
        public int postDetalhes = 0;
        public string sqlview = "";

        public Metodos m = new Metodos();
        //public String mesPassado = DateTime.Today.AddMonths(-1).ToString("d");
        //public String hoje = DateTime.Today.ToString("d");
        public String mesPassado = "25/04/2019";
        public String hoje = "22/05/2019";
        public string represChange = "nao";



        public static Decimal totComiss = 0.0M;
        public static String totComissS = "";
        public String erro = " ";


        public List<Comissao> comissoes = new List<Comissao> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["key"]!=null)
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


                //first execution
                if ((int)Session["first"] == 1)
                {
                    postRepres = (string)Session["nome"];
                    postRepres = postRepres.ToUpper();
                    Session["first"] = 0;
                    //executarRelatorio();
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
            if (detalhes.Checked == true)
            {
                postDetalhes = 1;
            }
            else
            {
                postDetalhes = 0;
            }

            postDatInicio = datInicio.Value;
            if (postDatInicio == "") postDatInicio = mesPassado;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            if ((int)Session["key"] >= 5)
            {
                postRepres = repres.Value.ToUpper();
            }
            else
            {
                postRepres = (string)Session["nome"];
                if (postRepres == "Karolline" || postRepres == "Dayane" || postRepres == "Luana" || postRepres == "Danieli" || postRepres == "Luciana" || postRepres == "Priscila" || postRepres == "Rafaella" || postRepres == "Rosilaine")
                    postRepres = "VendaInt";
                postRepres = postRepres.ToUpper();

            }

            postCliente = cliente.Value.ToUpper();
            postUnidade = unidade.Value;


            postSitPgto = sitPgto.Value.ToUpper();
            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";
            postCliente = m.configCoringas(postCliente);
            postRepres = m.configCoringas(postRepres);

            string datas = "";
            if (postSitPgto == "T")
            {
                datas = "AND dp.dat_pgto >= '" + postDatInicio + "' " +
                        "AND dp.dat_pgto <= '" + postDatFim + "' ";
            }
            else
            {
                datas = "";
            }

            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT d.cod_empresa," +
                                        "d.num_docum," +
                                        "d.num_docum_origem," +
                                        "d.num_pedido," +
                                        "cl.nom_cliente," +
                                        "dp.val_pago," +
                                        "d.pct_comis_1," +
                                        "r.nom_repres," +
                                        "d.dat_emis," +
                                        "d.dat_vencto_s_desc," +
                                        "dp.dat_pgto," +
                                        "d.ies_pgto_docum, " +
                                        "d.cod_repres_1, " +
                                        "d.val_bruto " +
                                        "FROM docum d " +
                                        "JOIN representante r on r.cod_repres = d.cod_repres_1 " +
                                        "JOIN clientes cl on cl.cod_cliente = d.cod_cliente " +
                                        "LEFT JOIN docum_pgto dp on d.num_docum = dp.num_docum and d.cod_empresa = dp.cod_empresa " +
                                        "WHERE r.nom_repres like '%" + postRepres + "' " +
                                         datas +
                                        "AND d.dat_emis > '01/01/2018' " +
                                        "AND ies_pgto_docum like '%" + postSitPgto + "%' " +
                                        "AND cl.nom_cliente like '%" + postCliente + "%' " +
                                        //"AND d.pct_comis_1 = " + postPctComiss + " " +
                                        //"AND d.val_bruto like " + postValor + " " +
                                        "AND d.cod_empresa like '%" + postUnidade + "%' " +
                                        "AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' " +
                                        //"AND d.pct_comis_1 <> 0.145 " +
                                        "ORDER BY d.cod_repres_1 ";

            //sqlview = sql; //ativa a exibicao do sql na tela
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totComiss = 0.0M;
            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Comissoes", (string)Session["nome"], postRepres + " | " + postDatInicio + " | " + postDatFim);
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    string numDocum = reader.GetString(1);
                    string numDocumOrigem = reader.GetString(2);
                    string numPedido = reader.GetString(3);
                    string nomCliente = reader.GetString(4);

                    Decimal valPago = 0.0m;

                    if (postSitPgto == "T")
                    {
                        string valPagoS = reader.GetString(5);
                        if (valPagoS.Contains("."))
                            valPagoS = valPagoS.Replace(".", ",");
                        valPago = Decimal.Parse(valPagoS);
                    }
                    else
                    {
                        string valBrutoS = reader.GetString(13);
                        if (valBrutoS.Contains("."))
                            valBrutoS = valBrutoS.Replace(".", ",");
                        valPago = Decimal.Parse(valBrutoS);
                    }

                    string pctComissaoS = reader.GetString(6);
                    if (pctComissaoS.Contains("."))
                        pctComissaoS = pctComissaoS.Replace(".", ",");


                    Decimal pctComissao;
                    Decimal comiss;

                    if (!pctComissaoS.Equals(""))
                    {
                        pctComissao = Decimal.Parse(pctComissaoS);
                        if (pctComissao == 0.145M) pctComissao = 0.0M;
                        if (pctComissao == 0.14M) pctComissao = 0.0M;
                        comiss = valPago * (pctComissao / 100);
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



                    Comissao comissao = new Comissao(codEmpresa, numDocum, numDocumOrigem, numPedido, nomCliente, valPago, pctComissao, Decimal.Round(comiss,3), nomRepres, datEmiss, datVcto, datPgto, iesPgtoDocum, codRepres);
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
                catch (Exception ex)
                {
                    erro = "---" + ex;
                    Comissao comissao = new Comissao("NULL", erro, "-", "-", "-", 0, 0, 0, "-", new DateTime(), new DateTime(), new DateTime(), 'T', "-");
                    comissoes.Add(comissao);
                }

            }

            new BancoLogix().fechar(conn);

        }

    }
}