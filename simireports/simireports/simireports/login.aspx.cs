﻿using IBM.Data.Informix;
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
            senhaPost = senha.Value;
            loginPost = login.Value;
            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT Senha,Nome,Idx,new_cod_repres FROM Usuarios WHERE Nome = '" + loginPost + "' AND Senha = '"+senhaPost+"'";
            
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);


            reader.Read();
            if (reader.HasRows)
            {
                String senha = reader.GetString(0);
                if(senha == senhaPost)
                {
                    string nome = reader.GetString(1);
                    double idx = reader.GetDouble(2);
                    string codRepres = reader.GetString(3);

                    Session["nome"] = nome;
                    Session["idx"] = idx;
                    Session["codRepres"] = codRepres;

                    //DEFINE A KEY DO USUARIO
                    int key = 1;
                    if (idx <= 25) key = 2;
                    if (idx <= 20) key = 3;
                    if (idx <= 15) key = 4;
                    if (idx <= 10) key = 5;

                    if (nome == "SimiSys") key = 11;

                    Session["key"] = key;
                }
                else
                {
                    erro = "Dados de login incorretos (2)";
                }
            }
            else
            {
                erro = "Dados de login incorretos (1)";
            }
        }

    }
}