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
        

        public List<OMPendente> omsps = new List<OMPendente>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //VERIFICACAO DE SESSAO E NIVEL
            if ((int)Session["key"] <= 0)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                //VERFICA NIVEL
                if ((int)Session["key"] >= 2)
                {
                    //OK
                }
                else
                {                    
                    Response.Redirect("index.aspx");
                }
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
            IfxDataReader reader = new
            BancoLogix().consultar("SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, a.ies_tip_entrega, c.nom_cliente " +
            "                                    FROM pedidos a " +
            "                                    JOIN ped_itens b " +
            "                                        ON a.num_pedido = b.num_pedido" +
            "                                        AND a.cod_empresa = b.cod_empresa" +
            "                                        AND(b.qtd_pecas_solic - b.qtd_pecas_cancel - b.qtd_pecas_atend) > 0" +
            "                                        AND b.qtd_pecas_romaneio = 0" +
            "                                    JOIN clientes c" +
            "                                        ON c.cod_cliente = a.cod_cliente" +
            "                                    JOIN estoque d" +
            "                                        ON b.cod_item = d.cod_item" +
            "                                        AND b.cod_empresa = d.cod_empresa" +
            "                                        AND d.qtd_liberada > 0" +
            "                                    WHERE a.ies_sit_pedido = 'N'" +
            "                                    AND a.cod_repres <> 9007 " +
            "                                    AND a.cod_nat_oper <> 9001" +
            "                                    AND "+ postTipoEntrega +
            "                                    AND "+ postEmpresa +
            "                                    GROUP BY a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, a.ies_tip_entrega, c.nom_cliente" +
            "                                    ORDER BY a.dat_alt_sit desc ", conn);
            IfxDataReader reader2;
            //IfxConnection conn2;
            if (reader != null)
            {
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    DateTime datAltSit = reader.GetDateTime(1);
                    string codCliente = reader.GetString(2);
                    string numPed = reader.GetString(3);
                    string tipoEntrega = reader.GetString(4);
                    string cliente = reader.GetString(5);

                    List<Item> itens = new List<Item>();

                    //conn2 = new BancoLogix().abrir();
                    reader2 = new
                    BancoLogix().consultar("SELECT b.qtd_pecas_solic, b.qtd_pecas_cancel, b.qtd_pecas_atend, i.den_item, b.prz_entrega, b.cod_item" +
                    "                                                    FROM ped_itens b join item i on i.cod_item = b.cod_item and i.cod_empresa = b.cod_empresa" +
                    "                                                    WHERE b.num_pedido = " + numPed + " and b.cod_empresa = " + codEmpresa, conn);
                    if (reader2 != null)
                    {
                        while (reader2.Read())
                        {
                            string qtdSolic = reader2.GetString(0);
                            string qtdCancel = reader2.GetString(1);
                            string qtdAtend = reader2.GetString(2);
                            string nomeItem = reader2.GetString(3);
                            //DateTime przEntregaS = reader.GetDateTime(4);
                            string przEntregaS = reader2.GetString(4);
                            //DateTime przEntrega = Convert.ToDateTime(przEntregaS);
                            string codItem = reader2.GetString(5);
                            Item item = new Item(qtdSolic, qtdCancel, qtdAtend, nomeItem, przEntregaS,codItem);
                            itens.Add(item);
                        }

                        OMPendente omp = new OMPendente(codEmpresa, datAltSit, codCliente, cliente, numPed, tipoEntrega, itens);
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