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
    public partial class RelReportBagio : System.Web.UI.Page
    {

        public string postValorMenor = "";
        public string postValorMaior = "";
        public string postDatInicio;
        public string postDatFim;
        public string postChance = "";
        
        public Metodos m = new Metodos();
        //public String mesPassado = DateTime.Today.AddMonths(-1).ToString("d");
        //public String hoje = DateTime.Today.ToString("d");
        public String mesPassado = "28/" + (DateTime.Now.Month - 1).ToString("d2") + "/" + (DateTime.Now.Year);
        public String hoje = DateTime.Now.Day.ToString("d2") + "/" + DateTime.Now.Month.ToString("d2") + "/" + (DateTime.Now.Year);
        public string represChange = "nao";
        
        public String erro = " ";


        public List<ReportBrady> reports = new List<ReportBrady> { };


        public List<DateTime> dataEm = new List<DateTime>();
        public List<String> clProp = new List<String>();
        public List<String> codProp = new List<String>();
        public List<String> nomCliente = new List<String>();
        public List<String> nomContato = new List<String>();
        public List<String> depto = new List<String>();
        public List<String> email = new List<String>();
        public List<String> telefone = new List<String>();
        public List<String> cpf = new List<String>();
        public List<String> endCliente = new List<String>();
        public List<String> cidade = new List<String>();
        public List<String> item = new List<String>();
        public List<String> itemReduz = new List<String>();
        public List<int> qtd = new List<int>();
        public List<String> repres = new List<String>();


        protected void Page_Load(object sender, EventArgs e)
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
                    if ((int)Session["key"] >= 5 || (string)Session["nome"]=="Luis")
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

            }
            else
            {

                Response.Redirect("login.aspx");
            }

        }

        protected void filtrarReportClick(object sender, EventArgs e)
        {
            //postValorMenor = valorMenor.Value;
            postValorMaior = valorMaior.Value;
            if (postValorMaior.Equals(""))
            {
                postValorMaior = "0";
            }
            if (postValorMaior.Contains(","))
            {
                postValorMaior = m.virgulaPorPonto(postValorMaior);
            }
            postChance = chance.Value;
            if (postChance.Equals("Todas"))
            {
                postChance = "AAC' or PROPOSTAS.CLProp like 'AMC' or PROPOSTAS.CLProp like 'ABC";
            }
            
            postDatInicio = datInicio.Value;
            if (postDatInicio == "") postDatInicio = mesPassado;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";
            postChance = m.configCoringas(postChance);
            

            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT PROPOSTAS.DataEm, PROPOSTAS.CLProp, PROPOSTAS.CodProp, LgxCLIENTES.nom_cliente," +
                " PROPOSTAS.nom_contato, PROPOSTAS.Depto, PROPOSTAS.Email, LgxCLIENTES.num_telefone," +
                " LgxCLIENTES.num_cgc_cpf, LgxCLIENTES.end_cliente, LgxCidades.den_cidade," +
                " LgxPRODUTOS.den_item, LgxPRODUTOS.den_item_reduz, PropostasItens.Qtd, PROPOSTAS.Representante " +
                " FROM PropostasItens inner JOIN LgxPRODUTOS ON PropostasItens.cod_item = LgxPRODUTOS.cod_item" +
                " inner JOIN PROPOSTAS ON PropostasItens.CodProp = PROPOSTAS.CodProp" +
                " inner JOIN LgxCLIENTES ON PROPOSTAS.cod_cliente = LgxCLIENTES.cod_cliente" +
                " INNER JOIN LgxCidades ON LgxCLIENTES.cod_cidade = LgxCidades.cod_cidade" +
                " WHERE (PROPOSTAS.CLProp like '"+postChance+"')" +
                " and(LgxPRODUTOS.ELP Like '30%' Or LgxPRODUTOS.ELP Like '03%' Or LgxPRODUTOS.ELP Like '31%' Or LgxPRODUTOS.ELP Like '97%')" +
                " AND PROPOSTAS.TotalProp > " + postValorMaior + "" +
                " AND PROPOSTAS.Finalidade <> 'REVENDA'" + 
                " AND PROPOSTAS.DataEm >= '" + m.configDataHuman2Banco(postDatInicio) + " 00:00:00'" +
                " AND PROPOSTAS.DataEm <= '" + m.configDataHuman2Banco(postDatFim) + " 23:59:59' " +
                " ORDER BY PROPOSTAS.DataEm desc";

            string errosql = "mama mia";
            //errosql = errosql;
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);

            errosql = " " + new BancoAzure().consultarErros(sql, conn);
            

            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Report Baggio", (string)Session["nome"],"");
                while (reader.Read())
                {

                    dataEm.Add(reader.GetDateTime(0));
                    clProp.Add(reader.GetString(1));
                    codProp.Add(reader.GetString(2));
                    nomCliente.Add(reader.GetString(3));
                    nomContato.Add(reader.GetString(4));
                    if (!reader.IsDBNull(5))
                    {
                        depto.Add(reader.GetString(5));
                    }
                    else
                    {
                        depto.Add("");
                    }
                    if (!reader.IsDBNull(6))
                    {
                        email.Add(reader.GetString(6));
                    }
                    else
                    {
                        email.Add("");
                    }
                    if (!reader.IsDBNull(7))
                    {
                        telefone.Add(reader.GetString(7));
                    }
                    else
                    {
                        telefone.Add("");
                    }
                    cpf.Add(reader.GetString(8));
                    endCliente.Add(reader.GetString(9));
                    cidade.Add(reader.GetString(10));
                    item.Add(reader.GetString(11));
                    if (!reader.IsDBNull(12))
                    {
                        itemReduz.Add(reader.GetString(12));
                    }
                    else
                    {
                        itemReduz.Add("");
                    }
                    qtd.Add(Convert.ToInt32(reader.GetDouble(13)));
                    repres.Add(reader.GetString(14));
                }
            }
            else
            {
                String erro = "null";
                try
                {
                    erro = new BancoLogix().abrirErros();
                    if (!erro.Equals("sem erros"))
                    {
                        ReportBrady repBug = new ReportBrady("", erro, 0, "", "");
                        reports.Add(repBug);
                    }
                }
                catch (Exception ex)
                {
                    erro = "---" + ex;
                    ReportBrady repBug = new ReportBrady("", erro, 0, "", "");
                    reports.Add(repBug);
                }

            }

            new BancoAzure().fechar(conn);

        }

    }
}