using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simiweb.simireports
{
    public partial class RelatoriosEspec : System.Web.UI.Page
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
                if ((int)Session["key"] >= 4)
                {
                   
                }
                else
                {
                   
                    Response.Redirect("Relatorios.aspx");
                }
            }


        }//fim pageload






    }
}