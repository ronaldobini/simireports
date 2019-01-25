using IBM.Data.Informix;
using simireports.simiMaster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simiMaster
{
    public partial class PageAlterarPedido : System.Web.UI.Page
    {

        public Pedido pedido;
        public static string postPedido;
        public static string postEmpresa;
        public static int clicado;
        protected void Page_Load(object sender, EventArgs e)
        {
            clicado = 0;
        }
        protected void filtrarPedido_Click(object sender, EventArgs e)
        {
            clicado = 1;
            postPedido = pedidoP.Value;
            postEmpresa = empresa.Value;
            executarRelatorio();
        }
        protected void filtrarAltClick(object sender, EventArgs e)
        {

        }
        protected void executarRelatorio()
        {
            IfxConnection conn = new BancoLogix().abrir();
            IfxDataReader reader = new
            BancoLogix().consultar("select num_pedido, num_sequencia, cod_item, pre_unit, qtd_pecas_solic from ped_itens" +
                                    " where cod_empresa = " + postEmpresa + " and num_pedido = " + postPedido, conn);
            int numPedido = 0;
            int numEmpresa = 0;
            List<ItemPedido> itens = new List<ItemPedido>();
            while (reader.Read())
            {

                numPedido = Int32.Parse(postPedido);
                numEmpresa = Int32.Parse(postEmpresa);
                
                string precoS = reader.GetString(3);

                if (precoS.Contains("."))
                    precoS = precoS.Replace(".", ",");
                Decimal preco = Decimal.Parse(precoS);

                string codItem = reader.GetString(2);
                Item item = new Item(codItem, preco);

                string qntS = reader.GetString(4);
                if (qntS.Contains("."))
                    qntS = qntS.Replace(".", ",");
                Decimal qntD = Decimal.Parse(qntS);
                int qnt = Decimal.ToInt32(qntD);

                string seqS = reader.GetString(1);
                if (seqS.Contains("."))
                    seqS = seqS.Replace(".", ",");
                Decimal seqD = Decimal.Parse(seqS);
                int seq = Decimal.ToInt32(seqD);

                ItemPedido itemPed = new ItemPedido(item, qnt, seq);
                itens.Add(itemPed);
            }
            pedido = new Pedido(numPedido, numEmpresa, itens);
        }
    }
}