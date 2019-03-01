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
    public partial class PageDevolucoes : System.Web.UI.Page
    {

        public static int first = 1;
        public static Metodos m = new Metodos();
        public static string postUnidade = "";
        public static string postNumNF = "";
        public static string postAvisoRec = "";
        public static string postValor = "";
        public static string postDatInicio = "01/02/2019";
        public static string postDatFim = "28/02/2019";
        public static string postCodItem = "";
        public static string postPreUnit = "";
        public static string postCodOper = "DEVC";

        
        public List<Devolucao> devolucoes = new List<Devolucao> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (first == 1)
            {
                first = 0;
                executarRelatorio();
            }
        }

        protected void filtrarDevolucoes_Click(object sender, EventArgs e)
        {
            postUnidade = unidade.Value;
            postNumNF = numNF.Value;
            if (!postNumNF.Equals(""))
            {
                postNumNF = "AND ns.num_nf = " +  postNumNF + " ";
            }
            postAvisoRec = avisoRec.Value;
            if (!postAvisoRec.Equals(""))
            {
                postAvisoRec = "AND ns.num_aviso_rec = " + postAvisoRec + " ";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodOper = codOper.Value.ToUpper();
            postDatInicio = datIni.Value;
            postDatFim = datFim.Value;
            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "select ns.cod_empresa, ns.num_nf, ns.num_aviso_rec, ns.val_tot_nf_c, ns.dat_emis_nf, ar.cod_item, ar.pre_unit_nf, arc.cod_operacao " +
                            "from nf_sup ns " +
                            "JOIN aviso_rec ar ON(ns.cod_empresa = ar.cod_empresa and ns.num_aviso_rec = ar.num_aviso_rec) " +
                            "JOIN aviso_rec_compl arc ON(ar.cod_empresa = arc.cod_empresa and ar.num_aviso_rec = arc.num_aviso_rec) " +
                            "where arc.cod_operacao like '%" + postCodOper + "%' " + 
                            "AND ns.cod_empresa like '%" + postUnidade + "%' " + 
                            postNumNF +
                            postAvisoRec +
                            "AND ns.dat_emis_nf >= '" + postDatInicio + "' " +
                            "AND ns.dat_emis_nf <= '" + postDatFim + "' " +
                            "AND ar.cod_item like  '%" + postCodItem + "%' " +
                            "order by dat_emis_nf desc";

            IfxDataReader reader = new BancoLogix().consultar(sql, conn);
            
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    string numNF = reader.GetString(1);
                    string avisoRec = reader.GetString(2);
                    string valorS = reader.GetString(3);
                    valorS = m.pontoPorVirgula(valorS);
                    Decimal valor = Decimal.Parse(valorS);
                    DateTime datEmiss = reader.GetDateTime(4);
                    string codItem = reader.GetString(5);
                    string precoUnitS = reader.GetString(6);
                    precoUnitS = m.pontoPorVirgula(precoUnitS);
                    Decimal precoUnit = Decimal.Parse(precoUnitS);
                    string codOper = reader.GetString(7);

                    Devolucao devolucao = new Devolucao(codEmpresa,numNF,avisoRec,valor,datEmiss,codItem,precoUnit,codOper);
                    devolucoes.Add(devolucao);

                }
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
        }

    }
}
