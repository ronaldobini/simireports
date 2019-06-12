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

       

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["key"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("login.aspx");
                }
            }            
                
                    
            

        }












    }
}