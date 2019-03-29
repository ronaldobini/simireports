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

        public string getp = "index";

        protected void Page_Load(object sender, EventArgs e)
        {

            getp = Request.QueryString["p"];
            //postp = Request.Form["p"];

            Session["firstJ"] = 1; 
            Session["first"] = 1; 
            


            
        }
    }
}