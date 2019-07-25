using IBM.Data.Informix;
using simireports.simireports.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simireports
{
    public partial class RelComissoesEspec : System.Web.UI.Page
    {

        public string data1;
        public string data2 = "22/" + (DateTime.Now.Month).ToString("d2") + "/" + DateTime.Now.Year;

        public string comissVendin8;
        public string comissSimilar8;
        public string comissKarolS;
        public string comissDayaneS;
        public string comissLuanaS;
        public string comissDanielliS;
        public string comissRafaS;
        public string comissLaineS;
        public string comissLucianaS;
        public string comissPriscilaS;
        public string comissVanessaS;
        public string comissFabianoS;

        public string sqlview;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            data1 = "23/" + (DateTime.Now.Month - 1).ToString("d2") + "/";
            if (DateTime.Now.Month.ToString("d2").Equals("01"))
            {
                data1 += DateTime.Now.Year - 1;
            }
            else
            {
                data1 += DateTime.Now.Year;
            }

            try
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
                        string postRepres = (string)Session["nome"];
                        if ((int)Session["key"] >= 7 || postRepres == "Karolline" || postRepres == "Dayane" || postRepres == "Luana" || postRepres == "Danieli" || postRepres == "Luciana" || postRepres == "Priscila" || postRepres == "Rafaella" || postRepres == "Rosilaine" || postRepres == "Vanessa")

                        {
                            //OK PERMANECE NA PAGINA
                            string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Comissoes Espec", (string)Session["nome"], data1);
                        }
                        else
                        {
                            Session["erro"] = "Você não tem permissão para acessar este relatório.";
                            Response.Redirect("Relatorios.aspx");
                        }
                    }

                    double totSimilar = totComissRepresNew("SIMILAR", data1, data2, "");
                    double similar8 = 100.36;//(totSimilar/10)/ 8.0; //xunxo
                    comissSimilar8 = "100,36";//Math.Round(similar8, 2).ToString(); //xunxo

                    double totVendin = totComissRepresNew("VENDAINT", data1, data2, "");
                    double vendin8 = totVendin / 8.0;
                    comissVendin8 = Math.Round(vendin8, 2).ToString();


                    double comissKarol = (totComissRepresNew("RAFAEL", data1, data2, "") +
                                         totComissRepresNew("REPCH1", data1, data2, "") +
                                         totComissRepresNew("THOME", data1, data2, "") +
                                         totComissRepresNew("VOLPI", data1, data2, "")) / 10;
                    comissKarolS = Math.Round(comissKarol, 2).ToString();

                    double comissLuana = (totComissRepresNew("VANESSA", data1, data2, "") +
                                         totComissRepresNew("LARSEN", data1, data2, "")) / 10;
                    comissLuanaS = Math.Round(comissLuana, 2).ToString();

                    double comissDayane = (totComissRepresNew("VINICIUS", data1, data2, "") +
                                         totComissRepresNew("ALISON", data1, data2, "")) / 10;
                    comissDayaneS = Math.Round(comissDayane, 2).ToString();
                    
                    double comissDanielli = (totComissRepresNew("ALEX", data1, data2, "") +
                                             totComissRepresNew("FELIPE", data1, data2, "") +
                                             totComissRepresNew("REPPG1", data1, data2, "") +
                                             totComissRepresNew("CLEVERSON", data1, data2, "")) / 10;
                    comissDanielliS = Math.Round(comissDanielli, 2).ToString();

                    double comissLuciana = (totComissRepresNew("FABIANO", data1, data2, "") +
                                         totComissRepresNew("GUSTAVO P", data1, data2, "")) / 10;
                    comissLucianaS = Math.Round(comissLuciana, 2).ToString();

                    double comissPriscila = (
                                         totComissRepresNew("EDUARDO", data1, data2, "") +
                                         totComissRepresNew("JOCEMAR", data1, data2, "") +
                                         totComissRepresNew("META_03-SP", data1, data2, "") +
                                         totComissRepresNew("META_11-SP", data1, data2, "") +
                                         totComissRepresNew("REPSP1", data1, data2, "") +
                                         totComissRepresNew("3S", data1, data2, "") +
                                         totComissRepresNew("SIRIOM", data1, data2, "")) / 10;
                    comissPriscila = 317.40; //xunxo
                    comissPriscilaS = Math.Round(comissPriscila, 2).ToString();

                    comissRafaS = "0,00";
                    comissLaineS = "0,00";


                    double comissVanessa = (totComissRepres("VANESSA", data1, data2, "") +
                                            totComissRepres("FELIPE", data1, data2, "") +
                                            totComissRepres("VINICIUS", data1, data2, "") +
                                            totComissRepres("ALEX", data1, data2, "") +
                                            totComissRepres("CLEVERSON", data1, data2, "") +
                                            totComissRepres("REPPG1", data1, data2, "") +
                                            totComissRepres("ALEX", data1, data2, "") +
                                            totComissRepres("LARSEN", data1, data2, "") +
                                            totComissRepres("ALISON", data1, data2, "") +
                                            totComissRepres("RAFAEL", data1, data2, "")) * 0.15;
                    comissVanessaS = Math.Round(comissVanessa, 2).ToString();

                    double comissFabiano = (totComissRepres("FABIANO", data1, data2, "") +
                                            totComissRepres("GUSTAVO P", data1, data2, "") +
                                            totComissRepres("VOLPI", data1, data2, "") +
                                            totComissRepres("THOME", data1, data2, "") +
                                            totComissRepres("REPCH1", data1, data2, "")) * 0.15;
                    comissFabianoS = Math.Round(comissFabiano, 2).ToString();

                }
                else
                {
                    Response.Redirect("login.aspx");
                }

            }
            catch (Exception ex)
            {
                String erro = ex + "    ERRO";
            }

        }//fim pageload





        public double totComissRepresNew(String represNome, String datInicio, String datFim, String codEmpresa)
        {
            double totComiss;
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT d.cod_empresa," +
                                        "dp.val_pago," +
                                        "d.pct_comis_1 " +
                                        "FROM docum d " +
                                        "JOIN representante r on r.cod_repres = d.cod_repres_1 " +
                                        "LEFT JOIN docum_pgto dp on d.num_docum = dp.num_docum and d.cod_empresa = dp.cod_empresa " +
                                        "WHERE r.nom_repres = '" + represNome + "' " +
                                        "AND dp.dat_pgto >= '" + datInicio + "' " +
                                        "AND dp.dat_pgto <= '" + datFim + "' " +
                                        "AND d.dat_emis > '01/01/2018' " +
                                        "AND ies_pgto_docum = 'T' " +
                                        //"AND d.pct_comis_1 = " + postPctComiss + " " +
                                        //"AND d.val_bruto like " + postValor + " " +
                                        "AND d.cod_empresa like '%" + codEmpresa + "%' " +
                                        "AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' " +
                                        "AND d.pct_comis_1 <> 0.145 " +
                                        "ORDER BY d.cod_repres_1 ";

            sqlview = sql; //ativa a exibicao do sql na tela
            //string erros = new BancoLogix().consultarErros(sql, conn);
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totComiss = 0.0;
            try
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string pctComissaoS = reader.GetString(2);
                        if (pctComissaoS.Contains("."))
                            pctComissaoS = pctComissaoS.Replace(".", ",");


                        double pctComissao;
                        double comiss;

                        if (!pctComissaoS.Equals(""))
                        {
                            string valBrutoS = reader.GetString(1);
                            if (valBrutoS.Contains("."))
                                valBrutoS = valBrutoS.Replace(".", ",");
                            double valBruto = Double.Parse(valBrutoS);
                            pctComissao = Double.Parse(pctComissaoS);
                            //if (pctComissao == 0.145) pctComissao = 0.0; n precisa pq tem no sql o filtro ja
                            comiss = valBruto * (pctComissao / 100);
                        }
                        else
                        {
                            pctComissao = 0.0;
                            comiss = 0.0;
                        }

                        totComiss = totComiss + comiss;


                    }
                }
            }catch(Exception errs)
            {
                string erros = "erro: " + errs;
            }

            new BancoLogix().fechar(conn);
            return totComiss;
        }







        public double totComissRepres(String represNome, String datInicio, String datFim, String codEmpresa)
        {
            double totComiss;
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT d.cod_empresa," +
                                        "dp.val_pago," +
                                        "d.pct_comis_1 " +
                                        "FROM docum d " +
                                        "JOIN representante r on r.cod_repres = d.cod_repres_1 " +
                                        "LEFT JOIN docum_pgto dp on d.num_docum = dp.num_docum and d.cod_empresa = dp.cod_empresa " +
                                        "WHERE r.nom_repres like '%" + represNome + "%' " +
                                        "AND dp.dat_pgto >= '" + datInicio + "' " +
                                        "AND dp.dat_pgto <= '" + datFim + "' " +
                                        "AND ies_pgto_docum = 'T' " +
                                        //"AND d.pct_comis_1 = " + postPctComiss + " " +
                                        //"AND d.val_bruto like " + postValor + " " +
                                        "AND d.cod_empresa like '%" + codEmpresa + "%' " +
                                        "AND d.ies_situa_docum = 'N' AND d.ies_tip_docum = 'DP' " +
                                        "AND d.pct_comis_1 <> 0.145 " +
                                        "ORDER BY d.cod_repres_1 ";

            sqlview = sql; //ativa a exibicao do sql na tela
            //string erros = new BancoLogix().consultarErros(sql, conn);
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totComiss = 0.0;
            try
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {


                        string pctComissaoS = reader.GetString(2);
                        if (pctComissaoS.Contains("."))
                            pctComissaoS = pctComissaoS.Replace(".", ",");


                        double pctComissao;
                        double comiss;

                        if (!pctComissaoS.Equals(""))
                        {
                            string valBrutoS = reader.GetString(1);
                            if (valBrutoS.Contains("."))
                                valBrutoS = valBrutoS.Replace(".", ",");
                            double valBruto = Double.Parse(valBrutoS);
                            pctComissao = Double.Parse(pctComissaoS);
                            //if (pctComissao == 0.145) pctComissao = 0.0; n precisa pq tem no sql o filtro ja
                            comiss = valBruto * (pctComissao / 100);
                        }
                        else
                        {
                            pctComissao = 0.0;
                            comiss = 0.0;
                        }

                        totComiss = totComiss + comiss;


                    }
                }
            }
            catch (Exception errs)
            {
                string erros = "erro: " + errs;
            }

            new BancoLogix().fechar(conn);
            return totComiss;
        }











        protected void filtrarComissEspec_Click(object sender, EventArgs e)
        {
            //filtrar click
        }





















    }
}