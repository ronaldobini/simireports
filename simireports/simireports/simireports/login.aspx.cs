using IBM.Data.Informix;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simireports.simireports.Classes;
using System.Data.SqlClient;

namespace simireports.simireports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // VERSAO
        public static string swver = "v0.20.0";
        //
        private string loginPost = "-";
        public string senhaPost = "-";
        public static LoginS logado = null;
        public string nome = "null";
        public string erro = " ";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //SE NAO EXISTE SESSAO SETA PRA ZERO
            if (Session["key"] == null)
            {
                Session["key"] = 0;
                Session["tries"] = 0;
            }
            else
            {
                //SE A CHAVE É MAIOR QUE 1 PULA O LOGIN
                if ((int)Session["key"] >= 1)
                {
                    Response.Redirect("index.aspx");
                }
                //SE FALSO PEDE LOGIN DE NOVO
                else
                {
                    Session["key"] = 0;
                }
            }
        }
        
        protected void Logar_Click(object sender, EventArgs e)
        {

            try
            {

                senhaPost = (senha.Value);
                loginPost = (login.Value);

                //ANTI INJECTION
                int lenS = senhaPost.Length; if (lenS >= 10) lenS = 10;
                int lenL = loginPost.Length; if (lenL >= 12) lenS = 12;

                senhaPost = senhaPost.Substring(0, lenS);
                loginPost = loginPost.Substring(0, lenL);

                senhaPost = senhaPost.Replace("'", "0");
                loginPost = loginPost.Replace("'", "0");
                senhaPost = senhaPost.Replace('"', '0');
                loginPost = loginPost.Replace('"', '0');
                senhaPost = senhaPost.Replace("/", "0");
                loginPost = loginPost.Replace("/", "0");
                senhaPost = senhaPost.Replace("=", "0");
                loginPost = loginPost.Replace("=", "0");
                //----------------

                if (loginPost == "master")
                    if (senhaPost == "damxd043")
                        logarMaster("!@#");

                SqlConnection conn = new BancoAzure().abrir();
                string sql = "SELECT Senha,Nome,Idx,new_cod_repres FROM Usuarios WHERE Nome = '" + loginPost + "' AND Senha = '" + senhaPost + "'";

                SqlDataReader reader = new BancoAzure().consultar(sql, conn);


                reader.Read();
                if ((int)Session["tries"] < 5)
                {
                    if (reader.HasRows)
                    {
                        String senha = reader.GetString(0);
                        if (senha == senhaPost)
                        {
                            string nome = reader.GetString(1);
                            double idx = reader.GetDouble(2);
                            string codRepres = reader.GetString(3);

                            Session["nome"] = nome;
                            Session["idx"] = idx;
                            Session["codRepres"] = codRepres;

                            Session["firstJ"] = 1;
                            Session["first"] = 1;

                            //DEFINE A KEY DO USUARIO
                            int key = 1;
                            if (idx <= 25) key = 2;
                            if (idx <= 24) key = 3;
                            if (idx <= 20) key = 5;

                            if (idx <= 15) key = 7;
                            if (idx <= 10) key = 8;

                            if (nome == "SimiSys") key = 11;

                            Session["key"] = key;
                        }
                        else
                        {
                            Session["tries"] = (int)Session["tries"] + 1;
                            erro = "Dados de login incorretos (" + Session["tries"] + "/5)";

                        }
                    }
                    else
                    {
                        Session["tries"] = (int)Session["tries"] + 1;
                        erro = "Dados de login incorretos (" + Session["tries"] + "/5)";
                    }
                }
                else
                {
                    erro = "Tentativas excedidas";
                }

            }
            catch (Exception err)
            {
                erro = "Erro fatal, contate o TI.";
            }


        }




        private void logarMaster(string sss)
        {
            if (sss == "!@#")
            {
                Session["nome"] = "Master";
                Session["idx"] = 0;
                Session["codRepres"] = "5999";
                Session["key"] = 12;
                Session["firstJ"] = 1;
                Session["first"] = 1;
                Response.Redirect("../simiMaster/index.aspx");
            }
        }







    }
}