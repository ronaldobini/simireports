using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.Reflection;
namespace SenhApp
{
    public partial class MainPage : ContentPage
    {
        private static int nivel = 0;
        private string erro;
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(Image).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }
            if (nivel == 0)
            {
                titulo.Text = "Senha 1.0";
            }
            else if (nivel == 1)
            {
                titulo.Text = "Senha 1.5";
            }
            else if (nivel == 2)
            {
                titulo.Text = "Senha 2.0";
            }
        }

        //private global::Xamarin.Forms.Editor entrada;

        public void Matemagica(object sender, EventArgs e)
        {
            string aa = "";
            if (entrada.Text.Length != 6)
            {
                erro = "Pre Senha Incorreta";
                DisplayAlert("", erro, "OK");
            }
            else
            {
                if (nivel == 0)
                {
                    aa = senhaNv1p0(entrada.Text);
                }
                else if (nivel == 1)
                {
                    aa = senhaNv1p5(entrada.Text);
                }
                else if (nivel == 2)
                {
                    aa = senhaNv2p0(entrada.Text);
                }

                result.Text = aa;
            }

            //Antigos comentarios:
            //Int32 nu = FindViewById<EditText>(Resource.Id.digitar).Text;
            //entrada = NameScopeExtensions.FindByName<Editor>(this, "entrada");
            //this.FindByName("entrada").ToString();
            //aa += ".00";

        }
        public string senhaNv1p0(string aa)
        {
            string resultado = "";
            //Calculo: raiz de (n*15), e pega os 6 numeros depois da virgula
            if (aa != "")
            {
                double numero = Convert.ToDouble(aa);
                double calculo = Math.Sqrt((numero * 15));
                aa = Convert.ToString(calculo);
                int virg = aa.IndexOf(",") + 1;
                for (int i = virg; i < virg + 6 && i < aa.Length; ++i)
                {
                    resultado += aa[i];
                }
            }
            return resultado;
        }


        public string senhaNv1p5(string aa)
        {
            string resultado = "";
            //Calculo: raiz de (n), e pega os 6 numeros depois da virgula
            if (aa != "")
            {
                double numero = Convert.ToDouble(aa);
                double calculo = Math.Sqrt((numero));
                aa = Convert.ToString(calculo);
                int virg = aa.IndexOf(",") + 1;
                for (int i = virg; i < virg + 6 && i < aa.Length; ++i)
                {
                    resultado += aa[i];
                }
            }
            return resultado;
        }


        public string senhaNv2p0(string aa)
        {
            string resultado = "";
            //Valor do dia da semana: 
            //Sabado e Domingo - 0
            //Segunda - 1
            //Terca - 2
            //Quarta - 3
            //Quinta - 4
            //Sexta - 5
            //Calculo: Adiciona em todos os digitos o valor do digito na posicao <valor do dia da semana>
            if (aa != "")
            {
                DayOfWeek dow = DateTime.Now.DayOfWeek;
                int posSoma;
                if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                {
                    posSoma = 0;
                }
                else if (dow == DayOfWeek.Monday)
                {
                    posSoma = 1;
                }
                else if (dow == DayOfWeek.Tuesday)
                {
                    posSoma = 2;
                }
                else if (dow == DayOfWeek.Wednesday)
                {
                    posSoma = 3;
                }
                else if (dow == DayOfWeek.Thursday)
                {
                    posSoma = 4;
                }
                else
                {
                    posSoma = 5;
                }
                int somador = Convert.ToInt32(aa[posSoma].ToString());
                for (int i = 0; i < 6; ++i)
                {
                    int valor = int.Parse(aa[i].ToString()) + somador;
                    if (valor >= 10)
                    {
                        valor = 0;
                    }
                    resultado += valor;
                }
            }
            return resultado;
        }
    }
}