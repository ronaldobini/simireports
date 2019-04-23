using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simiMaster
{
    public partial class sql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //VERIFICACAO DE SESSAO E NIVEL
            if ((int)Session["key"] <= 0)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                //VERFICA NIVEL
                if ((int)Session["key"] >= 1)
                {
                    if ((int)Session["key"] >= 11)
                    {
                        //OK
                    }
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }


        }

   


        protected void exe_Click(object sender, EventArgs e)
        {


        }



    }
}