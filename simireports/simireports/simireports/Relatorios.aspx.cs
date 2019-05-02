using simireports.simireports.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simireports
{
    public partial class Relatorios : System.Web.UI.Page
    {

        public string erro = " ";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["key"] != null)
            {
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }

            

        }
    }
}