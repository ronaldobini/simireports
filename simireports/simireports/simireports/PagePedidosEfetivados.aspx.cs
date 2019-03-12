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
    public partial class PagePedidosEfetivados : System.Web.UI.Page
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
                postNumPed = " AND a.num_pedido = " + postNumPed + "";
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
            
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres " +

                                   " FROM pedidos a"+
                                    " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa" +
                                    " JOIN clientes c on c.cod_cliente = a.cod_cliente" +

                                   " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                   " AND c.nom_cliente LIKE '%" + postCliente + "%'" +

                                   " AND r.nom_repres LIKE '%" + postRepres + "%'" +

                                   " AND a.dat_alt_sit >= '" + postDatInicio + "'" +

                                   " AND a.dat_alt_sit <= '" + postDatFim + "'" +

                                   " AND b.cod_item like '%" + postCodItem + "%'" +

                                   " AND a.cod_empresa LIKE '%" + postUnidade +"%'" +

                                   " AND ies_sit_pedido = 'N' AND cod_nat_oper<> 9001" +

                                   postNumPed + 

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +
                                   " GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, c.nom_cliente, r.nom_repres " +
                                   " ORDER BY a.dat_alt_sit desc, a.num_pedido";

            //sqlview = sql; //ativa a exibicao do sql na tela

            IfxDataReader reader = new BancoLogix().consultar(sql, conn);
            IfxDataReader reader2;
            IfxConnection conn2;
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

                    conn2 = new BancoLogix().abrir();
                    reader2 = new
                    BancoLogix().consultar("SELECT b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, b.pre_unit" +
                    "                                                    FROM ped_itens b join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa" +
                    "                                                    WHERE b.num_pedido = " + numPed + " and b.cod_empresa = " + codEmpresa, conn2);
                    if (reader2 != null)
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
                            Decimal preUnit = Decimal.Parse(preUnitS);
                            Item item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS, codItem, preUnit);
                            itens.Add(item);
                        }

                        PedidoEfetivado pedEfet = new PedidoEfetivado(codEmpresa, dat, codCliente, numPed, itens, cliente, repres);
                        pedsEfets.Add(pedEfet);
                    }
                    new BancoLogix().fechar(conn2);
                    reader2.Close();

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
