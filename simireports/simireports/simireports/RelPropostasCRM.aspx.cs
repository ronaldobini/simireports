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
    public partial class PagePedidosEfetivadosCRM : System.Web.UI.Page
    {

        public string postDatInicio = "";
        public string postDatFim = "";
        public string postUnidade = "";
        public string postCodCliente = "";
        public string postCliente = "";
        public string postRepres = "";
        public string postNumPed = "";
        public string postCodItem = "";
        public string postPreUnit = "";
        public string postClass = "";
        public string postUF = "";
        public string sqlview = "";
        public decimal totGeral = 0.0m;
        public string totGeralS = "0,00";
        public TimeSpan demora;
        public DateTime dtInicio;
        public DateTime dtFim;
        public Metodos m = new Metodos();
        public String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");
        public string represChange = "nao";
        public string errosql = "";

        public List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            //VERIFICACAO DE SESSAO E NIVEL
            if (Session["key"] != null)
            {
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //VERFICA NIVEL
                    if ((int)Session["key"] >= 1)
                    {
                        if ((int)Session["key"] >= 2)
                        {
                            represChange = "sim";
                        }
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }


            if ((int)Session["first"] == 1)
            {
                postRepres = (string)Session["nome"];
                postRepres = postRepres.ToUpper();
                Session["first"] = 0;
                //executarRelatorio();
            }


        }



        protected void filtrarPedEfet_Click(object sender, EventArgs e)
        {
            postUnidade = unidade.Value;
            postCodCliente = codCliente.Value;

            postCodCliente = m.configCoringas(postCodCliente);

            postCliente = cliente.Value.ToUpper();
            postCliente = m.configCoringas(postCliente);


            postNumPed = numPed.Value;
            if (!postNumPed.Equals(""))
            {
                postNumPed = " AND a.CodProp like '%" + postNumPed + "%'";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodItem = m.configCoringas(postCodItem);

            postDatInicio = datIni.Value;
            if (postDatInicio == "") postDatInicio = hoje;

            postDatFim = datFim.Value;
            if (postDatFim == "") postDatFim = hoje;

            if ((int)Session["key"] >= 2)
            {
                postRepres = repres.Value.ToUpper();
            }
            else
            {
                postRepres = (string)Session["nome"];
                postRepres = postRepres.ToUpper();
            }

            postClass = classprop.Value;
            if(postClass == "1"){
                postClass = " "; 
            }else if(postClass == "2"){
                postClass = " AND a.CLProp like 'AAC' ";
            }else if(postClass == "3"){
                postClass = " AND a.CLProp like 'AMC' ";            
            }else if(postClass == "4"){
                postClass = " AND a.CLProp like 'ABC' ";
            }
            else if (postClass == "5")
            {
                postClass = " AND (a.CLProp like 'FTo' OR a.CLProp like 'FPa') ";
            }
            else if (postClass == "6")
            {
                postClass = " AND (a.CLProp like 'AAC' OR a.CLProp like 'AMC' OR a.CLProp like 'ABC') ";
            }
            postUF = UF.Value;
            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            dtInicio = DateTime.Now;
            Session["firstJ"] = "0";

            SqlDataReader reader = null;
            //SqlDataReader reader2 = null;

            SqlConnection conn = new BancoAzure().abrir();
            //SqlConnection conn2 = new BancoAzure().abrir();
            postDatFim = m.configDataHuman2Banco(postDatFim);
            postDatInicio = m.configDataHuman2Banco(postDatInicio);
            string sql = "SELECT a.Unidade, a.DataProp, a.cod_cliente, a.CodProp, a.nom_cliente, a.Representante, a.CLProp," +

                               " b.Qtd, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.Seq, b.Comis,a.UFO " +

                                        " FROM PROPOSTAS a" +

                                        " INNER JOIN PropostasItens b on a.CodProp = b.CodProp" +

                                        " inner join LgxPRODUTOS i on i.cod_item = b.cod_item" +

                                        " WHERE a.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                        " AND a.nom_cliente LIKE '%" + postCliente + "%'" +

                                        " AND a.Representante LIKE '%" + postRepres + "%'" +

                                        " AND a.DataProp >= '" + postDatInicio + "  00:00:00'" +

                                        " AND a.DataProp <= '" + postDatFim + "  23:59:59'" +

                                        " AND b.cod_item like '%" + postCodItem + "%'" +

                                        " AND a.Unidade LIKE '%" + postUnidade + "%'" +

                                        " AND a.CLProp NOT like 'E%'" +
                                        " AND a.CLProp NOT like 'S%'" +
                                        " AND a.CLProp NOT like 'P%'" +

                                        postClass +

                                        postNumPed +

                                        " AND a.UFO  like '%"+postUF+"%'" +

                                        " GROUP BY a.Unidade, a.DataProp, a.cod_cliente, a.CodProp, a.nom_cliente, a.Representante, a.CLProp," +
                                        " b.Qtd, i.den_item, b.Prazo, b.cod_item, b.vlrUnit, b.Desconto, b.Seq, b.Comis, a.UFO " +
                                        " ORDER BY a.DataProp desc, a.CodProp, b.Seq";

            //sqlview = sql; //ativa a exibicao do sql na tela
            //errosql = new BancoAzure().consultarErros(sql,conn);
            reader = new BancoAzure().consultar(sql, conn);
            List<Item> itens = new List<Item>();
            string pedAnt = "zaburska";

            string codEmpresa = "";
            DateTime dat = new DateTime();
            string codCliente = "";
            string cliente = "";
            string repres = "";
            string numPed = "";
            string clprop = "";
            string ufo = "";
            Item item = null;
            bool primeiro = true;
            PedidoEfetivado pedEfet = null;

            if (reader != null && reader.HasRows)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Prop CRM", (string)Session["nome"], postRepres);
                while (reader.Read())
                {
                    numPed = reader.GetString(3);

                    if (primeiro)
                    {
                        pedAnt = reader.GetString(3);
                        primeiro = false;
                    }
                    else
                    {
                        if (numPed.Equals(pedAnt))
                        {
                            itens.Add(item);
                        }
                        else
                        {
                            itens.Add(item);
                            pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, pedAnt, itens, cliente, repres, clprop);
                            pedEfet.Ufo = ufo;
                            pedsEfets.Add(pedEfet);
                            itens = new List<Item>();
                            pedAnt = numPed;
                        }
                    }
                    codEmpresa = reader.GetString(0);
                    dat = reader.GetDateTime(1);
                    codCliente = reader.GetString(2);
                    cliente = reader.GetString(4);
                    repres = reader.GetString(5);
                    clprop = reader.GetString(6);

                    ufo = reader.GetString(15);

                    int qtdSolic = Convert.ToInt32(reader.GetDouble(7));
                    string nomeItem = reader.GetString(8);

                    string przEntregaS = "";
                    if (!reader.IsDBNull(9))
                    {
                        przEntregaS = reader.GetString(9);
                    }

                    string codItem = reader.GetString(10);
                    Double preUnitD = reader.GetDouble(11);
                    string preUnitS = Math.Round(preUnitD,2).ToString();
                    Decimal preUnit = Decimal.Parse(preUnitS);

                    //qtdSolic = m.pontoPorVirgula(qtdSolic);
                    //Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                    totGeral += ((Decimal)qtdSolic * preUnit);
                    totGeralS = m.formatarDecimal(totGeral);
                    Double desconto = reader.GetDouble(12);
                    Double comiss = reader.GetDouble(14);
                    if (comiss == 0.14) comiss = 0.0;
                    comiss *= 100;
                    item = new Item().itemPropostasCRM(qtdSolic,nomeItem,przEntregaS,codItem,preUnit,desconto,comiss);
                    
                }
                itens.Add(item);
                pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres, clprop);
                pedsEfets.Add(pedEfet);

                reader.Close();
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

            new BancoAzure().fechar(conn);

            DateTime dtFim = DateTime.Now;
            demora = dtFim - dtInicio;
        }

    }
}
