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

        public int resCount;

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

   


        protected void exe_Click(object sender, EventArgs e)
        {


            string postSql = sqlExe.Value;

            SqlConnection conn = new BancoAzure().abrir();
            string sql = postSql;

            string host = Dns.GetHostName();
            string ip = simireports.Classes.Metodos.pegarIP();

            string inisql = sql.Substring(0,6);

            if((inisql).Equals("select", StringComparison.OrdinalIgnoreCase))
            {
                result = result + "<table style='white-space: nowrap;border-collapse: collapse;'>";
                //String erro = new BancoAzure().consultarErros(sql, conn);
                resCount = 0;
                SqlDataReader reader = null;
                reader = new BancoAzure().consultar(sql, conn);
                if (reader != null && reader.HasRows)
                {
                    // string resultLog = Metodos.inserirLog((int)Session["idd"], "Master SQL", (string) Session["nome"],sql);
                    while (reader.Read())
                    {
                        result = result + "<tr>";
                        int i = 0;
                        while (i < reader.FieldCount)
                        {
                            result = result + "<td style='border-style: solid; border-width: 1px;'>" + reader.GetValue(i) + "</td>";
                            i++;
                        }
                        result = result + "</tr>";
                        ++resCount;
                    }
                }
                else
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    String erro = new BancoAzure().consultarErros(sql, conn);
                    result = result + "<div style='color:red;'>" + erro +"</div>";
                }
                result = result + " </table><br><br><br><br><br> " + host + " - " + ip;
                if (reader!= null)
                {
                    reader.Close();
                }
            }
            else
            {
                result = new BancoAzure().executar(sql, conn);
                result = result + " - " + host + " - " + ip;
            }


           
            
            new BancoAzure().fechar(conn);

        }



        



    }
}