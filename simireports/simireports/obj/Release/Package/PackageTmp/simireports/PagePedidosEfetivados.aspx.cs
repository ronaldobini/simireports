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

        public static int first = 1;
        public static Metodos m = new Metodos();
        public static string postUnidade = "";
        public static string postCodCliente = "";
        public static string postCliente = "";
        public static string postRepres = "";
        public static string postNumPed = "";
        public static String ontem = DateTime.Today.AddDays(-1).ToString("d");
        public static String hoje = DateTime.Today.ToString("d");
        public static string dataPesqIni = ontem;
        public static string dataPesqFim = hoje;
        public static string postDatInicio = "AND dat_alt_sit >= '" + ontem + "'";
        public static string postDatFim = "AND dat_alt_sit <= '" + hoje + "'";
        public static string postCodItem = "";
        public static string postPreUnit = "";

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
                    //OK
                }
                else
                {
                    
                    Response.Redirect("index.aspx");
                }
            }
            if (first == 1)
            {
                first = 0;
                executarRelatorio();
            }
        }

        protected void filtrarPedEfet_Click(object sender, EventArgs e)
        {
            postUnidade = unidade.Value;
            postCodCliente = codCliente.Value;
            //if (!postCodCliente.Equals(""))
            //{
            //    postCodCliente = "AND ns.num_nf = " + postCodCliente + " ";
                    postCodCliente = m.configCoringas(postCodCliente);
            //}

            postCliente = cliente.Value.ToUpper();
            postCliente = m.configCoringas(postCliente);
            
            postRepres = repres.Value.ToUpper();
            postRepres = m.configCoringas(postRepres);

            postNumPed = numPed.Value;
            if (!postNumPed.Equals(""))
            {
                postNumPed = "AND a.num_pedido = " + postNumPed + " ";
            }
            postCodItem = codItem.Value.ToUpper();
            postCodItem = m.configCoringas(postCodItem);

            postDatInicio = datIni.Value;
            if (!postDatInicio.Equals(""))
            {
                postDatInicio = "AND dat_alt_sit >= '" + postDatInicio + "' ";
                dataPesqIni = datIni.Value;
            }
            else
            {
                dataPesqIni = "-";
            }
            postDatFim = datFim.Value;
            if (!postDatFim.Equals(""))
            {
                postDatFim = "AND dat_alt_sit <= '" + postDatFim + "' ";
                dataPesqFim = datFim.Value;
            }
            else
            {
                dataPesqFim = "-";
            }
            executarRelatorio();
        }

        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            string sql = "SELECT a.cod_empresa, a.dat_alt_sit, a.cod_cliente, a.num_pedido, b.qtd_pecas_solic," +
                                   " b.qtd_pecas_cancel, b.cod_item, b.pre_unit, c.nom_cliente, r.nom_repres " +

                                   " FROM pedidos a"+
                                    " JOIN ped_itens b on a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa" +
                                    " JOIN clientes c on c.cod_cliente = a.cod_cliente" +

                                   " JOIN representante r on r.cod_repres = a.cod_repres " +

                                   " WHERE c.cod_cliente LIKE '%" + postCodCliente + "%'" +

                                   " AND c.nom_cliente LIKE '%" + postCliente + "%'" +

                                   " AND r.nom_repres LIKE '%" + postRepres + "%'" +

                                   postDatInicio +
                                   
                                   postDatFim +

                                   postNumPed +

                                   " AND b.cod_item like '%" + postCodItem + "%'" +

                                   " AND a.cod_empresa LIKE '%" + postUnidade +"%'" +

                                   " AND ies_sit_pedido = 'N' AND cod_nat_oper<> 9001" +

                                   " AND a.num_pedido = b.num_pedido AND a.cod_empresa = b.cod_empresa AND c.cod_cliente = a.cod_cliente" +

                                   " ORDER BY a.dat_alt_sit desc, a.num_pedido";

            IfxDataReader reader = new BancoLogix().consultar(sql, conn);

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    string codEmpresa = reader.GetString(0);
                    DateTime dat = reader.GetDateTime(1);
                    string codCliente = reader.GetString(2);
                    string numPed = reader.GetString(3);
                    string qtdPecasSolic = reader.GetString(4);
                    string qtdPecasCancel = reader.GetString(5);
                    string codItem = reader.GetString(6);
                    string precoUnitS = reader.GetString(7);
                    precoUnitS = m.pontoPorVirgula(precoUnitS);
                    Decimal precoUnit = Decimal.Parse(precoUnitS);
                    string cliente = reader.GetString(8);
                    string repres = reader.GetString(9);

                    PedidoEfetivado pedEfet = new PedidoEfetivado(codEmpresa,dat,codCliente,numPed,qtdPecasSolic,qtdPecasCancel,codItem,precoUnit,cliente,repres);
                    pedsEfets.Add(pedEfet);

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
