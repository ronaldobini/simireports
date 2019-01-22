using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simireports.simireports.Classes;

namespace simireports
{
    public partial class prePedidos : System.Web.UI.Page
    {
        public static int key = 0;

        
        public List<PrePedido> pedidos = new List<PrePedido> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (key == 0) { Response.Redirect("erro.aspx");  }

            SqlConnection conn = new BancoAzure().abrir();
            SqlDataReader reader = new BancoAzure().consultar("SELECT codped, title, dataped, representante from PrePedidos where dataped >= '2019-01-21 00:00:00' order by dataped desc", conn);

            while (reader.Read())
            {
                string codPed = reader.GetString(0);
                string criador = reader.GetString(1);
                DateTime tempo = reader.GetDateTime(2);
                string repres = reader.GetString(3).Substring(0,10);
                PrePedido ped = new PrePedido(codPed, tempo,criador,repres);
                pedidos.Add(ped);
            }

            

        }
    }
}