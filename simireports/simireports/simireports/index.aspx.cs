using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports
{
    public partial class Index : System.Web.UI.Page
    {

        public string result = "-";

        protected void Page_Load(object sender, EventArgs e)
        {
            result = new BancoAzure().consultar("SELECT nome FROM Usuarios");
            Console.WriteLine(result);
        }
    }
}