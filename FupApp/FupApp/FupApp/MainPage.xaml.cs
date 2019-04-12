using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FupApp
{
    public partial class MainPage : ContentPage
    {
        private String[] menu = {"FUP","Sair"};
        private string login;
        public MainPage(string login)
        {
            InitializeComponent();
            listView.ItemsSource = menu;
            listView.ItemSelected += OnItemSelected;
            this.login = login;
        }
        
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            String tools = (String)e.SelectedItem;
            listView.SelectedItem = null;
            Page page = null;

            if (tools == null)
            {
                return;
            }

            else if(tools.Equals("FUP"))
            {
                page = new FuPage(login);
            }

            else if(tools.Equals("Sair"))
            {
                page = new Login();
            }

            if (page != null)
            {
                Navigation.PushAsync(page);
                if (tools.Equals("Sair"))
                {
                    Navigation.RemovePage(this);
                }
            }
        }



        async Task DandoUmTempo(int valor)
        {
            await Task.Delay(valor);
        }
    }
}
