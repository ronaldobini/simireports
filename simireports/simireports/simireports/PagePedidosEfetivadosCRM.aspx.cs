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
        public string sqlview = "";
        public decimal totGeral = 0.0m;
        public string totGeralS = "0,00";


        public Metodos m = new Metodos();
        public String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public String hoje = DateTime.Today.ToString("d");        
        public string represChange = "nao";

        public List<PedidoEfetivado> pedsEfets = new List<PedidoEfetivado> { };

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
                    Response.Redirect("index.aspx");
                }
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
                postNumPed = " AND a.CodPed = " + postNumPed + "";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodItem = m.configCoringas(postCodItem);

            postDatInicio = datIni.Value;
            if (postDatInicio == "") postDatInicio = ontem;

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

            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            Session["firstJ"] = "0";

            SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            SqlConnection conn = new BancoAzure().abrir();
            SqlConnection conn2 = new BancoAzure().abrir();

            string sql = "SELECT a.Unidade, a.DataUlt, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante " +

                                        " FROM PrePEDIDOS a" +

                                        " INNER JOIN PrePedidosItens b on a.CodPed = b.CodPed" +

                                        " WHERE a.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                        " AND a.nom_cliente LIKE '%" + postCliente + "%'" +

                                        " AND a.Representante LIKE '%" + postRepres + "%'" +

                                        //" AND a.DataUlt >= '" + postDatInicio + "'" +

                                        //" AND a.DataUlt <= '" + postDatFim + "'" +

                                        " AND b.cod_item like '%" + postCodItem + "%'" +

                                        " AND a.Unidade LIKE '%" + postUnidade + "%'" +

                                        //" AND a.CLProp NOT like 'E%'"+

                                        postNumPed +

                                        " GROUP BY a.Unidade, a.DataUlt, a.cod_cliente, a.CodPed, a.nom_cliente, a.Representante " +
                                        " ORDER BY a.DataUlt desc, a.CodPed";

            //sqlview = sql; //ativa a exibicao do sql na tela
            //String errosql = new BancoAzure().consultarErros(sql,conn);
            reader = new BancoAzure().consultar(sql, conn);
            
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    DateTime dat = reader.GetDateTime(1);
                    string codCliente = reader.GetString(2);
                    string numPed = reader.GetString(3);
                    string cliente = reader.GetString(4);
                    string repres = reader.GetString(5);
                    List<Item> itens = new List<Item>();
                    string sql2 = "SELECT b.Qtd, b.QtdC, b.QtdA, i.den_item, b.Prazo, b.cod_item, b.vlrUnit" +
                    "                                                    FROM PrePedidosItens b inner join LgxPRODUTOS i on i.cod_item = b.cod_item" +
                    "                                                    WHERE b.CodPed = " + numPed;
                    
                    String errosql = new BancoAzure().consultarErros(sql2, conn);

                    reader2 = new
                     BancoAzure().consultar(sql2, conn2);

                    if (reader2 != null && reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            string qtdSolic = reader2.GetString(0);
                            string qtdCancel = reader2.GetString(1);
                            string qtdAtend = reader2.GetString(2);
                            string nomeItem = reader2.GetString(3);
                            string przEntregaS = reader2.GetString(4);
                            string codItem = reader2.GetString(5);
                            string preUnitS = reader2.GetString(6);
                            preUnitS = m.pontoPorVirgula(preUnitS);                            
                            Decimal preUnit = Decimal.Round(Decimal.Parse(preUnitS),2);
                            qtdSolic = m.pontoPorVirgula(qtdSolic);
                            Decimal qtdSolicD = Decimal.Round(Decimal.Parse(qtdSolic), 0);
                            totGeral += (qtdSolicD * preUnit);
                            totGeralS = m.formatarDecimal(totGeral);
                            Item item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);
                            itens.Add(item);
                        }

                        PedidoEfetivado pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                        pedsEfets.Add(pedEfet);
                        reader2.Close();
                    }
                    new BancoAzure().fechar(conn2);

                }
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

        }

    }
}
