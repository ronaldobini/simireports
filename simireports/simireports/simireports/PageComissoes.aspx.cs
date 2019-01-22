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
    public partial class PageComissoes : System.Web.UI.Page
    {
        public static int key = 0;


        public List<Comissao> comissoes = new List<Comissao> { };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            IfxConnection conn = new BancoLogix().abrir();
            IfxDataReader reader = new BancoLogix().consultar
                ("SELECT num_pedido, dat_alt_sit, pct_comissao, cod_repres, cod_cliente, cod_empresa " +
                "FROM pedidos " +
                "WHERE dat_alt_sit >= '21/01/2019' order by dat_alt_sit desc", conn);

            while (reader.Read())
            {
                string numPed = reader.GetString(0);                
                string tempo = reader.GetString(1);
                string comiss = reader.GetString(2).ToString();
                string repres = reader.GetString(3);
                string codCliente = reader.GetString(4);
                string codEmpresa = reader.GetString(5);
                Comissao comissao = new Comissao(numPed, tempo, comiss, repres, codCliente, codEmpresa);
                comissoes.Add(comissao);
            }



        }
    }
}