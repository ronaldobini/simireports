using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simireports
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        public string wLogin = "";

        private string loginPost = "-";

        protected void Page_Load(object sender, EventArgs e)
        {
            wLogin = "Digite seu Login";
        }


        protected void logar_Click(object sender, EventArgs e)
        {            
            loginPost = login.Value;
            if (loginPost == "bini")
            {
                prePedidos.key = 1;
                Response.Redirect("PagePrePedidos.aspx");
            }
            else
            {
                wLogin = "Login incorreto";
            }
        }

    }
}