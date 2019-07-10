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
    public partial class RelReportBrady : System.Web.UI.Page
    {

        public string postDatInicio = "";
        public string postDatFim = "";
        public string postDestino = "";

        public Metodos m = new Metodos();
        //public String mesPassado = DateTime.Today.AddMonths(-1).ToString("d");
        //public String hoje = DateTime.Today.ToString("d");
        public String mesPassado = "28/" + (DateTime.Now.Month - 1).ToString("d2") + "/" + (DateTime.Now.Year);
        public String hoje = DateTime.Now.Day.ToString("d2") + "/" + DateTime.Now.Month.ToString("d2") + "/" + (DateTime.Now.Year);
        public string represChange = "nao";
        
        public static Decimal totNota = 0.0M;
        public static String totNotaS = "";
        public String erro = " ";


        public List<ReportBrady> reports = new List<ReportBrady> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            mesPassado = "28/" + (DateTime.Now.Month - 1).ToString("d2") + "/";
            if (DateTime.Now.Month.ToString("d2").Equals("01"))
            {
                mesPassado += DateTime.Now.Year - 1;
            }
            else
            {
                mesPassado += DateTime.Now.Year;
            }
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
                    if ((int)Session["key"] >= 5)
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
            postDatInicio = datInicio.Value;
            if (postDatInicio == "") postDatInicio = mesPassado;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;
            
            postDestino = destino.Value.ToUpper();
            

        executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";
            postDestino = m.configCoringas(postDestino);
            
            string datas = "";

            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT c.nom_cliente, nfm.cliente,c.cod_cidade,sum(nfi.val_bruto_item), nfi.pedido, nfm.nota_fiscal, nfm.empresa " +
                " FROM fat_nf_mestre nfm" +
                " JOIN clientes c on nfm.cliente = c.cod_cliente" +
                " JOIN fat_nf_item nfi on nfi.trans_nota_fiscal = nfm.trans_nota_fiscal and nfi.empresa = nfm.empresa" +
                " JOIN pedidos p on p.num_pedido = nfi.pedido and nfi.empresa = p.cod_empresa" +
                " JOIN item it ON (nfi.item = it.cod_item and nfi.empresa = it.cod_empresa)" +
                " WHERE c.cod_cidade like '%" +postDestino+"%'" +
                " AND dat_hor_emissao >= '" + m.configDataHuman2Banco(postDatInicio) + " 00:00:00' " +
                " AND dat_hor_emissao <= '" + m.configDataHuman2Banco(postDatFim) + " 23:59:59'" +
                " AND nfi.natureza_operacao > 1000" +
                " AND nfm.sit_nota_fiscal = 'N'" +
                " AND (it.cod_familia = '03' or it.cod_familia = '30' or it.cod_familia = '31' or " +
                                "it.cod_familia = '32' or it.cod_familia = '33' or it.cod_familia = '97')" +
                " GROUP BY c.nom_cliente, nfm.cliente,c.cod_cidade,nfm.val_mercadoria, nfi.pedido, nfm.nota_fiscal, nfm.empresa " +
                " ORDER BY nfi.pedido desc";
            string errosql = new BancoLogix().consultarErros(sql, conn);
            //sqlview = sql; //ativa a exibicao do sql na tela
            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            totNota = 0.0M;
            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Report Brady", (string)Session["nome"], postDatInicio + " | " + postDatFim);
                while (reader.Read())
                {
                    string cliente = reader.GetString(0);
                    string codCliente = reader.GetString(1);
                    string codCidade = reader.GetString(2);

                    string valorS = reader.GetString(3);
                    if (valorS.Contains("."))
                        valorS = valorS.Replace(".", ",");
                    Decimal valor = Decimal.Parse(valorS);

                    string pedido = reader.GetString(4);
                    
                    totNota = totNota + valor;

                    ReportBrady repBdy = new ReportBrady(cliente,codCliente,valor,pedido,"");
                    reports.Add(repBdy);

                }
                totNota = decimal.Round(totNota, 2);
                totNotaS = m.formatarDecimal(totNota);
            }
            else
            {
                String erro = "null";
                try
                {
                    erro = new BancoLogix().abrirErros();
                    if (!erro.Equals("sem erros"))
                    {
                        ReportBrady repBug = new ReportBrady("",erro,0,"","");
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

            new BancoLogix().fechar(conn);

        }

    }
}