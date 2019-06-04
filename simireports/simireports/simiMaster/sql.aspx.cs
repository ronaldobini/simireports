using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simireports.simireports.Classes;

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

            string inisql = sql.Substring(0,6);
            if(inisql == "select")
            {
                result = result + "<table>";
                SqlDataReader reader = null;
                reader = new BancoAzure().consultar(sql, conn);
                if (reader != null && reader.HasRows)
                {
                    // string resultLog = Metodos.inserirLog((int)Session["idd"], "Master SQL", (string) Session["nome"],sql);
                    while (reader.Read())
                    {
                        result = result + 
                            "<tr>" +
                                "<td>" + reader.GetValue(0) + "</td>" +
                                "<td>" + reader.GetValue(1) + "</td>" +
                            "</tr>";
                    }
                }

                        result = result + " </table><br><br><br><br><br> " + host + " - " + ip;
            }
            else if(inisql == "update")
            {
                result = new BancoAzure().executar(sql, conn);
                result = result + " - " + host + " - " + ip;
            }


           

            new BancoAzure().fechar(conn);

        }



        



    }
}