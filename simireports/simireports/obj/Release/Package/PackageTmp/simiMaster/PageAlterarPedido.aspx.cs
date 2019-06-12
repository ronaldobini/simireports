using IBM.Data.Informix;
using simireports.simireports.Classes;
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
           
            while (reader.Read())
            {

                numPedido = Int32.Parse(postPedido);
                numEmpresa = Int32.Parse(postEmpresa);
                
               
            }
        }
    }
}