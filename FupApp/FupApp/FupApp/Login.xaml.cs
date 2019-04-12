using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FupApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            // Define o binding context
            this.BindingContext = this;
            // Define a propriedade
            this.IsBusy = false;
            //Define o evento Click do botão de login
            BtnLogin.Clicked += BtnLogin_Clicked;
        }

        private string loginPost;
        private string senhaPost;
        private string erro;
        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            /*

            senhaPost = senha.Text;
            loginPost = login.Text;
            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT Senha,Nome,Idx,new_cod_repres FROM Usuarios WHERE Nome = '" + loginPost + "' AND Senha = '" + senhaPost + "'";

            SqlDataReader reader = new BancoAzure().consultar(sql, conn);


            reader.Read();

            //if ((int)Session["tries"] < 5)
            //{
            if (reader.HasRows)
            {
                String senha = reader.GetString(0);
                if (senha == senhaPost)
                {
                    string nome = reader.GetString(1);
                    double idx = reader.GetDouble(2);
                    string codRepres = reader.GetString(3);
                    
            login = nome;
                    */

            this.IsBusy = true;
            //MainPage page = new MainPage(nome);
            MainPage page = new MainPage("SimiSys");
            Application.Current.MainPage = new NavigationPage(page);

            /*

                    //Session["nome"] = nome;
                    //Session["idx"] = idx;
                    //Session["codRepres"] = codRepres;

                    //Session["firstJ"] = 1;
                    //Session["first"] = 1;

                    ////DEFINE A KEY DO USUARIO
                    //int key = 1;
                    //if (idx <= 25) key = 2;
                    //if (idx <= 24) key = 3;
                    //if (idx <= 20) key = 5;

                    //if (idx <= 15) key = 7;
                    //if (idx <= 10) key = 8;

                    //if (nome == "SimiSys") key = 11;

                    //Session["key"] = key;
                }
                else
                {
                    //Session["tries"] = (int)Session["tries"] + 1;
                    //erro = "Dados de login incorretos (" + Session["tries"] + "/5)";
                    erro = "Dados de login incorretos.";

                }
            }
            else
            {
                //Session["tries"] = (int)Session["tries"] + 1;
                //erro = "Dados de login incorretos (" + Session["tries"] + "/5)";
                erro = "Dados de login incorretos.";
            }
            Console.Write(erro);
        }
        //else
        //{
        //    erro = "Tentativas excedidas";
        //}
        //Dá uma pausa de ~valor~ segundos
        async Task DandoUmTempo(int valor)
        {
            await Task.Delay(valor);
        }

    */
        }
    }
}