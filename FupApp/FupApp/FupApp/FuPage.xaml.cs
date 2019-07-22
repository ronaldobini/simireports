using FupApp.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FupApp
{
    public partial class FuPage : ContentPage
    {
        //private Picker picker;
        private string login;
        //private int count = 0;
        private string erro;
        private string quem;
        private string sup;
        //private string prop;

        public FuPage()
        {
            InitializeComponent();
            //NavigationPage.SetTitleIcon(this, "imgs/syss.png");
        }

        public FuPage(string login)
        {
            InitializeComponent();
            //NavigationPage.SetTitleIcon(this, "imgs/syss.png");
            this.login = login;
            //this.prop = prop;
            quem = login;
            Metodos m = new Metodos();
            pickerSup.Title = "Selecione um Representante";
            quemLabel.Text = login;
            List<String> represes = new List<String>();

            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT Nome from Usuarios order by Nome asc";
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);
            string errosql = new BancoAzure().consultarErros(sql, conn);
            represes.Add("");
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    represes.Add(reader.GetString(0));
                }
            }
            new BancoAzure().fechar(conn);
            foreach (string repres in represes)
            {
                pickerSup.Items.Add(repres);
            }



            string hojeMenosSete = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6).ToString();
            
            fupsProp.Children.Clear();
            conn = new BancoAzure().abrir();
            sql = "SELECT CodProp,FUP,DataFUP,Quem,QuemSup " +
                "from PropostasFUP where Quem = '" + quem + "' AND DataFUP >= '" + m.configDataHuman2Banco(hojeMenosSete) + "' order by DataFUP desc ";
            reader = new BancoAzure().consultar(sql, conn);

            if (reader != null && reader.HasRows)
            {
                List<FollowUp> fupa = new List<FollowUp>();
                FollowUp fup = null;
                while (reader.Read())
                {
                    string codProp = reader.GetString(0);
                    string fups = reader.GetString(1);
                    DateTime dataFup = reader.GetDateTime(2);
                    string quem = reader.GetString(3);
                    string quemSup = reader.GetString(4);
                    //codProp += ", " + dataFup.ToString() + ", Quem:" + quem + ", QuemSup:" + quemSup;
                    fup = new FollowUp(codProp, fups, dataFup, quem, quemSup);
                    fupa.Add(fup);
                }
                reader.Close();
                new BancoAzure().fechar(conn);
                if (fupa.Count > 0)
                {
                    foreach (FollowUp f in fupa)
                    {
                        Grid gridFups = new Grid
                        {
                            //BackgroundColor = Color.FromHex("#222")
                            ColumnSpacing = 1,
                            RowSpacing = 1
                        };
                        gridFups.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
                        gridFups.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        Label ldata = new Label
                        {
                            Text = f.Data.ToString(),
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0),
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                        gridFups.Children.Add(ldata, 0, 0);
                        //Grid.SetColumnSpan(ldata, 2);

                        Label lquem = new Label
                        {
                            Text = "Quem: " + f.Quem,
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0)
                        };
                        gridFups.Children.Add(lquem, 1, 0);
                        //Grid.SetColumnSpan(lquem, 2);

                        Label lquemsup = new Label
                        {
                            Text = "Sup: " + f.QuemSup,
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0)
                        };
                        gridFups.Children.Add(lquemsup, 2, 0);
                        //Grid.SetColumnSpan(lquemsup, 2);

                        Label lfup = new Label
                        {
                            Text = f.Fup,
                            FontSize = 20,
                            //TextColor = Color.FromHex(""),
                            //BackgroundColor = Color.FromHex("#777"),
                            Margin = new Thickness(10, 10, 10, 20)
                        };
                        gridFups.Children.Add(lfup, 0, 1);
                        Grid.SetColumnSpan(lfup, 3);

                        Label line = new Label
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            FontSize = 1,
                            BackgroundColor = Color.FromHex("#888")
                        };

                        fupsProp.Children.Add(line);
                        fupsProp.Children.Add(gridFups);
                    }
                }

            }











        }

        private void pickerSelected(object sender, EventArgs e)
        {
            sup = pickerSup.Items[pickerSup.SelectedIndex].ToString();
            //DisplayAlert(sup, "Foi o item Selecionado", "OK");

            //DisplayAlert(quem, "Foi o item Selecionado", "OK");
        }

        private void clicadoBotao(object sender, EventArgs e)
        {
            string prop = propec.Text;
            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT UFO,UFD,cod_cliente,nom_cliente,Representante " +
                "from PROPOSTAS where CodProp = '" + prop + "'";
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);
            string errosql = new BancoAzure().consultarErros(sql, conn);

            if (reader != null && reader.HasRows)
            {
                Proposta proposta = null;
                while (reader.Read())
                {
                    string codProp = prop;
                    string ufo = reader.GetString(0);
                    string ufd = reader.GetString(1);
                    string codCliente = reader.GetString(2);
                    string nomeCliente = reader.GetString(3);
                    string repres = reader.GetString(4);
                    proposta = new Proposta(codProp, ufo, ufd, codCliente, nomeCliente, repres);
                }
                reader.Close();
                if (proposta != null)
                {
                    prop = prop.ToUpper();
                    string str = DateTime.Now.ToString();
                    //DateTime dataFup = DateTime.Parse(str, new CultureInfo("en-US"));
                    string[] strA = str.Split('/');
                    string dataFup = strA[1]+"/"+strA[0]+"/"+strA[2];

                    sql = "INSERT INTO PropostasFUP" +
                        "(CodProp,DataFUP,DataAlarme,SetAlarme,FUP,Quem,QuemSup,cod_cliente,UFO,UFD,Representante,nom_cliente)" +
                        " values('" + prop + "','" + dataFup + "','" + dataFup + "','OFF','" + fup.Text + "','" + quem + "','" + sup + "'," +
                        "'" + proposta.CodCliente + "','" + proposta.Ufo + "','" + proposta.Ufd + "','" + proposta.Repres + "','" + proposta.NomeCliente + "');";

                    //errosql = new BancoAzure().consultarErros(sql, conn);
                    new BancoAzure().executar(sql, conn);
                }

                DisplayAlert("Incluido", "FUP incluido com Sucesso", "OK");
            }
            else
            {
                erro = "Proposta invalida";
                DisplayAlert("", erro, "OK");
            }
            fup.Text = "";
            new BancoAzure().fechar(conn);
            //++count;
            //((Button)sender).Text = $"{count}";
            pesqFups( sender,  e);
        }



        private void pesqFups(object sender, EventArgs e)
        {
            fupsProp.Children.Clear();
            string prop = propec.Text;
            SqlConnection conn = new BancoAzure().abrir();
            string sql = "SELECT CodProp,FUP,DataFUP,Quem,QuemSup " +
                "from PropostasFUP where CodProp = '" + prop + "' order by DataFUP desc";
            SqlDataReader reader = new BancoAzure().consultar(sql, conn);
            string errosql = new BancoAzure().consultarErros(sql, conn);

            if (reader != null && reader.HasRows)
            {
                List<FollowUp> fupa = new List<FollowUp>();
                FollowUp fup = null;
                while (reader.Read())
                {
                    string codProp = reader.GetString(0);
                    string fups = reader.GetString(1);
                    DateTime dataFup = reader.GetDateTime(2);
                    string quem = reader.GetString(3);
                    string quemSup = reader.GetString(4);
                    //codProp += ", " + dataFup.ToString() + ", Quem:" + quem + ", QuemSup:" + quemSup;
                    fup = new FollowUp(codProp, fups, dataFup, quem, quemSup);
                    fupa.Add(fup);
                }
                reader.Close();
                new BancoAzure().fechar(conn);
                if (fupa.Count > 0)
                {
                    foreach (FollowUp f in fupa)
                    {
                        Grid gridFups = new Grid
                        {
                            //BackgroundColor = Color.FromHex("#222")
                            ColumnSpacing = 1,
                            RowSpacing = 1
                        };
                        gridFups.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
                        gridFups.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        gridFups.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        Label ldata = new Label
                        {
                            Text = f.Data.ToString(),
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0),
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                        gridFups.Children.Add(ldata, 0, 0);
                        //Grid.SetColumnSpan(ldata, 2);

                        Label lquem = new Label
                        {
                            Text = "Quem: " + f.Quem,
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0)
                        };
                        gridFups.Children.Add(lquem, 1, 0);
                        //Grid.SetColumnSpan(lquem, 2);

                        Label lquemsup = new Label
                        {
                            Text = "Sup: " + f.QuemSup,
                            FontSize = 15,
                            //BackgroundColor = Color.FromHex("#777")
                            Margin = new Thickness(3, 3, 0, 0)
                        };
                        gridFups.Children.Add(lquemsup, 2, 0);
                        //Grid.SetColumnSpan(lquemsup, 2);

                        Label lfup = new Label
                        {
                            Text = f.Fup,
                            FontSize = 20,
                            //TextColor = Color.FromHex(""),
                            //BackgroundColor = Color.FromHex("#777"),
                            Margin = new Thickness(10, 10, 10, 20)
                        };
                        gridFups.Children.Add(lfup, 0, 1);
                        Grid.SetColumnSpan(lfup, 3);

                        Label line = new Label
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            FontSize = 1,
                            BackgroundColor = Color.FromHex("#888")
                        };
                        
                        fupsProp.Children.Add(line);
                        fupsProp.Children.Add(gridFups);
                    }
                }
                
            }
            else
            {
                erro = "Proposta invalida";
                DisplayAlert("", erro, "OK");
            }
            //++count;
            //((Button)sender).Text = $"{count}";
        }



    }
}