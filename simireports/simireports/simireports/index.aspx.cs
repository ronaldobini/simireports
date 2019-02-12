using IBM.Data.Informix;
using simireports.simireports.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports
{
    public partial class Index : System.Web.UI.Page
    {

        public string result;
        public string result2 = "-";
        public string result3 = "-";
        public string result4 = "-";
        public string result5 = "-";
        public string result6 = "-";

        protected void Page_Load(object sender, EventArgs e)
        {

            
            IfxConnection conn = new BancoLogix().abrir();
            IfxDataReader reader = new BancoLogix().consultar("SELECT * FROM pedidos where dat_alt_sit >= '16/01/2019' order by dat_alt_sit desc", conn);

            reader.Read();
            result = reader.GetString(1);
            reader.Read();
            result2 = reader.GetString(1);
            reader.Read();
            result3 = reader.GetString(1);
            reader.Read();
            result4 = reader.GetString(1);
            reader.Read();
            result5 = reader.GetString(1);
            reader.Read();
            result6 = reader.GetString(1);

            new BancoLogix().fechar(conn);


            
        }
    }
}