using IBM.Data.Informix;
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

            try
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
                    }
                    else
                    {

                        Response.Redirect("Relatorios.aspx");
                    }
                }

                double totSimilar = totComissRepres("SIMILAR", "26/03/2019", "25/04/2019", "");
                double similar8 = totSimilar / 8;
                comissSimilar8 = Math.Round(similar8, 2).ToString();

                double totVendin = totComissRepres("VENDAINT", "26/03/2019", "25/04/2019", "");
                double vendin8 = totVendin / 8;
                comissVendin8 = Math.Round(vendin8,2).ToString();
                

                double comissKarol = (totComissRepres("RAFAEL", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("VOLPI", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("AGOSTINI", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("THOME", "26/03/2019", "25/04/2019", "")) / 10;
                comissKarolS = Math.Round(comissKarol,2).ToString();

                double comissLuana = (totComissRepres("VANESSA", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("LARSEN", "26/03/2019", "25/04/2019", "")) / 10;
                comissLuanaS = Math.Round(comissLuana,2).ToString();

                double comissDayane = (totComissRepres("VINICIUS", "26/03/2019", "25/04/2019", "")) / 10;
                comissDayaneS = Math.Round(comissDayane,2).ToString();

                double comissDanielli = (totComissRepres("ALEX", "26/03/2019", "25/04/2019", "")+
                                         totComissRepres("FELIPE", "26/03/2019", "25/04/2019", "")) / 10;
                comissDanielliS = Math.Round(comissDanielli,2).ToString();

                double comissLuciana = (totComissRepres("FABIANO", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("GUSTAVO P", "26/03/2019", "25/04/2019", "")) / 10;
                comissLucianaS = Math.Round(comissLuciana,2).ToString();

                double comissPriscila = (totComissRepres("MARCELO", "26/03/2019", "25/04/2019", "") +
                                     totComissRepres("EDUARDO", "26/03/2019", "25/04/2019", "")) / 10;
                comissPriscilaS = Math.Round(comissPriscila,2).ToString();

                comissRafaS = "0,00";
                comissLaineS = "0,00";


                double comissVanessa = (totComissRepres("VANESSA", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("FELIPE", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("VINICIUS", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("ALEX", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("RAFAEL", "26/03/2019", "25/04/2019", "")) * 0.15;
                comissVanessaS = Math.Round(comissVanessa, 2).ToString();

                double comissFabiano = (totComissRepres("FABIANO", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("GUSTAVO P", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("VOLPI", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("THOME", "26/03/2019", "25/04/2019", "") +
                                        totComissRepres("AGOSTINI", "26/03/2019", "25/04/2019", "")) * 0.15;
                comissFabianoS = Math.Round(comissFabiano, 2).ToString();



            }
            catch (Exception ex)
            {
                String erro = ex + "    ERRO";
            }

        }//fim pageload





        public double totComissRepres(String represNome, String datInicio, String datFim, String codEmpresa)
        {
            double totComiss;
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT d.cod_empresa," +
                                        "d.val_bruto," +
                                        "d.pct_comis_1 " +
                                        "FROM docum d " +
                                        "JOIN representante r on r.cod_repres = d.cod_repres_1 " +
                                        "LEFT JOIN docum_pgto dp on d.num_docum = dp.num_docum and d.cod_empresa = dp.cod_empresa " +
                                        "WHERE r.nom_repres like '%" + represNome + "' " +
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
            }catch(Exception errs)
            {
                string erros = "erro: " + errs;
            }

            return totComiss;
        }







        protected void filtrarComissEspec_Click(object sender, EventArgs e)
        {
            //filtrar click
        }





















    }
}