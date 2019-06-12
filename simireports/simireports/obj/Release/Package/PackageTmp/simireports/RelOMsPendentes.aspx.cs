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
    public partial class PageOMsPendentes : System.Web.UI.Page
    {
        public static int first = 1;

        public static string postTipoEntrega;
        public static string postEmpresa;
        public static Metodos m = new Metodos();
        

        public List<OMPendente> omsps = new List<OMPendente>();

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
                    if ((int)Session["key"] >= 5 || (string)Session["nome"] == "Dayane" || (string)Session["nome"] == "Karolline")
                    {
                        //OK
                        
                    }
                    else
                    {
                        Session["erro"] = "Você não tem permissão para acessar este relatório.";
                        Response.Redirect("Relatorios.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void filtrarOMs_Click(object sender, EventArgs e)
        {
            if (!tipoEntrega.Value.Equals("0"))
            {
                postTipoEntrega = "a.ies_tip_entrega = " + tipoEntrega.Value;
            }
            else
            {
                postTipoEntrega = "(a.ies_tip_entrega = 1 or a.ies_tip_entrega = 2 or a.ies_tip_entrega = 3 or a.ies_tip_entrega = 4)";
            }
            if (!empresa.Value.Equals("0"))
            {
                postEmpresa = "a.cod_empresa = " + empresa.Value;
            }
            else
            {
                postEmpresa = "(a.cod_empresa = 2 or a.cod_empresa = 3 or a.cod_empresa = 5)";
            }
            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, a.ies_tip_entrega, c.nom_cliente, c2.nom_cliente " +
            "                                    FROM pedidos a " +
            "                                    JOIN ped_itens b " +
            "                                        ON a.num_pedido = b.num_pedido" +
            "                                        AND a.cod_empresa = b.cod_empresa" +
            "                                        AND(b.qtd_pecas_solic - b.qtd_pecas_cancel - b.qtd_pecas_atend) > 0" +
            "                                        AND b.qtd_pecas_romaneio < b.qtd_pecas_solic-b.qtd_pecas_cancel-b.qtd_pecas_atend" +
            "                                    JOIN clientes c" +
            "                                        ON c.cod_cliente = a.cod_cliente" +
            "                                    JOIN clientes c2" +
            "                                        ON c2.cod_cliente = a.cod_transpor" +
            "                                    JOIN estoque d" +
            "                                        ON b.cod_item = d.cod_item" +
            "                                        AND b.cod_empresa = d.cod_empresa" +
            "                                        AND d.qtd_liberada > 0" +
            "                                    WHERE a.ies_sit_pedido = 'N'" +
            "                                    AND a.cod_repres <> 9007 " +
            "                                    AND a.cod_nat_oper <> 9001" +
            "                                    AND " + postTipoEntrega +
            "                                    AND " + postEmpresa +
            "                                    GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, a.ies_tip_entrega, c.nom_cliente, c2.nom_cliente" +
            "                                    ORDER BY a.dat_alt_sit desc ";
            IfxDataReader reader = new
            BancoLogix().consultar(sql, conn);
            IfxDataReader reader2;
            IfxDataReader reader3;
            string errosql = new BancoLogix().consultarErros(sql, conn);
            //IfxConnection conn2;
            if (reader != null)
            {
                string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel OMs Pendentes", (string)Session["nome"], " ");
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    DateTime datAltSit = reader.GetDateTime(1);
                    string codCliente = reader.GetString(2);
                    string numPed = reader.GetString(3);
                    string tipoEntrega = reader.GetString(4);
                    string cliente = reader.GetString(5);
                    string trans = reader.GetString(6);
                    List<Item> itens = new List<Item>();
                    List<OrdemCompra> OCs = new List<OrdemCompra>();

                    //conn2 = new BancoLogix().abrir();
                    reader2 = new
                    BancoLogix().consultar("SELECT b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item, " +
                    "                                                    b.qtd_pecas_romaneio, e.qtd_liberada, e.qtd_reservada " +
                    "                                                    FROM ped_itens b " +
                    "                                                    join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa" +
                    "                                                    JOIN estoque e on e.cod_item = b.cod_item and e.cod_empresa = b.cod_empresa " +
                    "                                                    WHERE b.num_pedido = " + numPed + " " +
                            "                                                    and b.cod_empresa = " + codEmpresa, conn);
                    if (reader2 != null)
                    {
                        while (reader2.Read() && reader2.HasRows)
                        {
                            int qtdSolic = m.qtdLogixToInt(reader2.GetString(0));
                            int qtdCancel = m.qtdLogixToInt(reader2.GetString(1));
                            int qtdAtend = m.qtdLogixToInt(reader2.GetString(2));
                            string nomeItem = reader2.GetString(3);
                            string przEntregaS = reader2.GetString(4);
                            string codItem = reader2.GetString(5);
                            int qtdRoma = m.qtdLogixToInt(reader2.GetString(6));
                            int qtdLib = m.qtdLogixToInt(reader2.GetString(7));
                            int qtdReserv = m.qtdLogixToInt(reader2.GetString(8));
                            Item item = new Item(codItem,qtdSolic,qtdCancel,qtdAtend,nomeItem,0,przEntregaS,0,0,0, qtdRoma, qtdLib, qtdReserv);
                            if (item.QtdSolic != item.QtdAtend + item.QtdCancel)
                            {
                                string sql2 = "SELECT oc.cod_empresa, oc.num_oc, oc.dat_entrega_prev, oc.num_docum, oc.qtd_solic " +
                                "                                                    FROM ordem_sup oc " +
                                "                                                    WHERE oc.cod_item = '" + codItem + "' AND ies_situa_oc = 'R'" +
                                "                                                    AND ies_versao_atual = 'S'" +
                                "                                                    order by num_oc desc ";
                                reader3 = new BancoLogix().consultar(sql2, conn);
                                if (reader3 != null)
                                {
                                    while (reader3.Read() && reader3.HasRows)
                                    {
                                        string codEmpresaOC = reader3.GetString(0);
                                        string numOC = reader3.GetString(1);
                                        string dataEntregaOC = reader3.GetString(2);
                                        string numDocumOC = reader3.GetString(3);
                                        int qtdOC = m.qtdLogixToInt(reader3.GetString(4));
                                        OrdemCompra OC = new OrdemCompra(numOC, codItem, codEmpresaOC, DateTime.Parse(dataEntregaOC), qtdOC, numDocumOC);
                                        OCs.Add(OC);
                                    }
                                    item.OCs1 = OCs;
                                    OCs = new List<OrdemCompra>();
                                    reader3.Close();
                                }
                            }
                            itens.Add(item);
                        }

                        OMPendente omp = new OMPendente(codEmpresa, datAltSit, codCliente, cliente, numPed, tipoEntrega, itens);
                        omp.Trans = trans;
                        omsps.Add(omp);
                    }
                    //new BancoLogix().fechar(conn2);
                    reader2.Close();
                }
            }

            new BancoLogix().fechar(conn);

        }






    }
}