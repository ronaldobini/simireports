using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simiMaster
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //VERIFICACAO DE SESSAO E NIVEL
            if (Session["key"] != null)
            {
                if ((int)Session["key"] <= 0)
                {
                    Response.Redirect("../simireports/login.aspx");
                }
                else
                {
                    //VERFICA NIVEL
                    
                    if ((int)Session["key"] >= 11)
                    {
                        //OK
                    }                    
                    else
                    {
                        Response.Redirect("../simireports/index.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("../simireports/index.aspx");
            }



        }




    }
}