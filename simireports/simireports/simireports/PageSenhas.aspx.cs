using simireports.simireports.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports
{
    public partial class PageSenhas : System.Web.UI.Page
    {
        public String senha;
        public Metodos m = new Metodos();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void gerarSenha(object sender, EventArgs e)
        {
            string preSenha = preS.Value;
            string erro = "";
            for (int i = 0; i < 6; ++i)
            {
                char c = preSenha[i];
                if (!Char.IsDigit(preSenha,i))
                {
                    erro = "Senha invalida, insira somente digitos.";
                    break;
                }
            }
            //string senha;
            if (!erro.Equals(""))
            {
                senha = erro;
            }
            else
            {
                if ((int)Session["key"] >= 8)
                {
                    senha = m.senhaNv1p0(preSenha);
                }
                else if ((int)Session["key"] >= 7)
                {
                    senha = m.senhaNv1p5(preSenha);
                }
                else if ((int)Session["key"] == 5)
                {
                    senha = m.senhaNv2p0(preSenha);
                }
                else
                {
                    senha = "Nao permitido para seu usuario.";
                }
            }
        }
    }
}