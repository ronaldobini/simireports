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
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((int)Session["key"] <= 0 || Session["key"] == null)
            {
                Response.Redirect("login.aspx");
            }

        }
    }
}