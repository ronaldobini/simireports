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
        private string loginPost = "-";
        public string senhaPost = "-";
        public static LoginS logado = null;
        public string nome = "MERDA";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void Logar_Click(object sender, EventArgs e)
        {
            senhaPost = senha.Value;
            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT Nome,Senha,Idx FROM Usuarios WHERE Senha = '" + senhaPost + "'";
            
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);


            reader.Read();
            if (reader.HasRows)
            {

                logado = new LoginS((reader.GetString(0)), (reader.GetString(1)), (reader.GetDouble(2)));

                //logado.Nome = reader.GetString(0);
                //logado.Senha = reader.GetString(1);
                //logado.Nivel = reader.GetDouble(2);

            }
            else
            {
                nome = "reader nulo k7";
            }
        }

    }
}