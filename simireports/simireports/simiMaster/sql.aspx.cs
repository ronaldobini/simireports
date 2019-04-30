using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simiMaster
{
    public partial class sql : System.Web.UI.Page
    {

        public string result = "...";

        protected void Page_Load(object sender, EventArgs e)
        {

            //VERIFICACAO DE SESSAO E NIVEL
            if (Session["key"] != null)
            {
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
            else
            {
                Response.Redirect("../index.aspx");
            }


        }

   


        protected void exe_Click(object sender, EventArgs e)
        {

            string postSql = sqlExe.Value;

            SqlConnection conn = new BancoAzure().abrir();
            string sql = postSql;

            string host = Dns.GetHostName();
            string ip = simireports.Classes.Metodos.pegarIP();

            result = new BancoAzure().executar(sql, conn);
            result = result + " - " + host + " - " + ip;

            new BancoAzure().fechar(conn);

        }



        



    }
}