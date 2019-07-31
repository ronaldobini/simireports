using FupApp.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IBM.Data.Informix;

namespace FupApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EstoquePage : ContentPage
	{
        Metodos m = new Metodos();
		public EstoquePage ()
		{
			InitializeComponent ();
		}
        public void pesqEstoque(object sender, EventArgs e)
        {
            estoque.Children.Clear();
            string erro = "";
            if (codProd.Text != null || descProd.Text != null)
            {
                string codItem = codProd.Text;
                codItem = m.configCoringas(codItem);
                if (codItem == null)
                {
                    codItem = "";
                }
                string descItem = descProd.Text;
                descItem = m.configCoringas(descItem);
                if (descItem == null)
                {
                    descItem = "";
                }
                //SqlConnection conn = new BancoAzure().abrir();
                //string sql = "SELECT TOP 100 ps.cod_item, ps.cod_empresa, ps.qtd_liberada, ps.qtd_reservada, ps.qtd_ped, ps.qtd_oc, ps.DataUltSP, p.den_item " +
                //    "FROM LgxProdutosSum ps JOIN LgxPRODUTOS p on ps.cod_item = p.cod_item " +
                //    "WHERE ps.cod_item LIKE '%" + codItem + "%' AND p.den_item LIKE '%" + descItem + "%' ORDER BY ps.cod_item ";

                //SqlDataReader reader = new BancoAzure().consultar(sql, conn);
                //string errosql = new BancoAzure().consultarErros(sql, conn);

                IfxConnection conn = new BancoLogix().abrir();
                string sql = "select e.cod_empresa, e.cod_item, i.den_item, e.qtd_liberada, e.qtd_reservada, eis.qtd_oc, eis.qtd_ped " +
                    "from estoque e " +
                    "join item i on e.cod_item = i.cod_item and e.cod_empresa = i.cod_empresa" +
                    "join eis_t_pos_itens eis on eis.cod_item = e.cod_item and eis.cod_empresa = e.cod_empresa" +
                    "WHERE e.cod_item LIKE '%" + codItem + "%' AND i.den_item LIKE '%" + descItem + "%' ORDER BY e.cod_item " +
                    "limit 100";

                IfxDataReader reader = new BancoLogix().consultar(sql, conn);

                string errosql = new BancoLogix().consultarErros(sql, conn);

                if (reader != null && reader.HasRows)
                {
                    List<Estoque> estList = new List<Estoque>();
                    Estoque est = null;
                    while (reader.Read())
                    {
                        string empresa = reader.GetString(0);
                        codItem = reader.GetString(1);
                        string desc = reader.GetString(2);
                        double qtdLiberadad = reader.GetDouble(3);
                        double qtdReservadad = reader.GetDouble(4);
                        double qtdOcd = reader.GetDouble(5);
                        double qtdPedd = reader.GetDouble(6);
                        int qtdLiberada = Convert.ToInt32(qtdLiberadad);
                        int qtdReservada = Convert.ToInt32(qtdReservadad);
                        int qtdPed = Convert.ToInt32(qtdPedd);
                        int qtdOc = Convert.ToInt32(qtdOcd);
                        DateTime dataUltSP = new DateTime();
                        //try
                        //{
                        //    dataUltSP = reader.GetDateTime(6);
                        //}
                        //catch (Exception ero)
                        //{
                        //    //caguei pro erro

                        //}

                        est = new Estoque(codItem, empresa, qtdLiberada, qtdReservada, qtdPed, qtdOc, dataUltSP,desc);
                        //est.Desc = desc;
                        estList.Add(est);
                    }
                    reader.Close();
                    new BancoLogix().fechar(conn);
                    if (estList.Count > 0)
                    {
                        foreach (Estoque es in estList)
                        {
                            Grid gridEstoque = new Grid
                            {
                                //BackgroundColor = Color.FromHex("#222")
                                ColumnSpacing = 1,
                                RowSpacing = 1
                            };
                            gridEstoque.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
                            gridEstoque.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
                            gridEstoque.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            gridEstoque.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            gridEstoque.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            gridEstoque.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            gridEstoque.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            string textoData = es.DataUltSP.ToString();
                            if (textoData.Equals("01/01/0001 00:00:00"))
                            {
                                textoData = "---";
                            }
                            Label ldata = new Label
                            {
                                Text = textoData,
                                FontSize = 15,
                                //BackgroundColor = Color.FromHex("#777")
                                Margin = new Thickness(3, 3, 0, 0),
                                HorizontalTextAlignment = TextAlignment.Center
                            };
                            gridEstoque.Children.Add(ldata, 0, 0);
                            //Grid.SetColumnSpan(ldata, 2);

                            Label lcodItem = new Label
                            {
                                Text = es.CodItem,
                                FontSize = 20,
                                HorizontalOptions = LayoutOptions.Center,
                                //BackgroundColor = Color.FromHex("#777")
                                Margin = new Thickness(3, 3, 0, 0)
                            };
                            gridEstoque.Children.Add(lcodItem, 1, 0);
                            Grid.SetColumnSpan(lcodItem, 2);

                            Label lempresa = new Label
                            {
                                Text = es.Empresa,
                                FontSize = 15,
                                HorizontalOptions = LayoutOptions.EndAndExpand,
                                //BackgroundColor = Color.FromHex("#777")
                                Margin = new Thickness(3, 3, 0, 0)
                            };
                            gridEstoque.Children.Add(lempresa, 3, 0);
                            //Grid.SetColumnSpan(lquemsup, 2);

                            Label descric = new Label
                            {
                                Text = es.Desc,
                                FontSize = 20,
                                HorizontalOptions = LayoutOptions.Center,
                                //TextColor = Color.FromHex(""),
                                //BackgroundColor = Color.FromHex("#777"),
                                Margin = new Thickness(10, 5, 10, 5)
                            };
                            gridEstoque.Children.Add(descric, 0, 2);
                            Grid.SetColumnSpan(descric, 4);

                            Label lqtdL = new Label
                            {
                                Text = "L: " + es.QtdLiberada.ToString(),
                                FontSize = 25,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                //TextColor = Color.FromHex(""),
                                //BackgroundColor = Color.FromHex("#777"),
                                Margin = new Thickness(10, 5, 10, 5)
                            };
                            gridEstoque.Children.Add(lqtdL, 0, 3);
                            Grid.SetColumnSpan(lqtdL, 2);
                            Label lqtdR = new Label
                            {
                                Text = "R: " + es.QtdReservada.ToString(),
                                FontSize = 25,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                //TextColor = Color.FromHex(""),
                                //BackgroundColor = Color.FromHex("#777"),
                                Margin = new Thickness(0, 5, 0, 0)
                            };
                            gridEstoque.Children.Add(lqtdR, 2, 3);
                            Grid.SetColumnSpan(lqtdR, 2);
                            //Label lqtdP = new Label
                            //{
                            //    Text = "P:" + es.QtdPed.ToString(),
                            //    FontSize = 20,
                            //    //TextColor = Color.FromHex(""),
                            //    //BackgroundColor = Color.FromHex("#777"),
                            //    //Margin = new Thickness(10, 10, 10, 20)
                            //};
                            //gridEstoque.Children.Add(lqtdP, 2, 1);
                            //Label lqtdOc = new Label
                            //{
                            //    Text = "OC:" + es.QtdOc.ToString(),
                            //    FontSize = 20,
                            //    //TextColor = Color.FromHex(""),
                            //    //BackgroundColor = Color.FromHex("#777"),
                            //    //Margin = new Thickness(10, 10, 10, 20)
                            //};
                            //gridEstoque.Children.Add(lqtdOc, 3, 1);

                            Label line = new Label
                            {
                                HorizontalOptions = LayoutOptions.Fill,
                                FontSize = 1,
                                BackgroundColor = Color.FromHex("#888")
                            };

                            estoque.Children.Add(line);
                            estoque.Children.Add(gridEstoque);
                        }
                    }
                }
                else
                {
                    erro = "Produto nao encontrado ou sem estoque";
                    DisplayAlert("", erro, "OK");
                }
            }
            else
            {
                erro = "Entre com um produto.";
                DisplayAlert("", erro, "OK");
            }
    

        }
	}
}