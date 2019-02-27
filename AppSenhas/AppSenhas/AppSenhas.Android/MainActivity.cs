using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppSenhas.Droid
{

    [Activity(Label = "SenhApp", Icon = "@mipmap/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            Title = "SenhApps";
            Button bt = FindViewById<Button>(Resource.Id.MyButton);
            EditText edit = FindViewById<EditText>(Resource.Id.Digitar);
            edit.InputType = Android.Text.InputTypes.ClassNumber;
            bt.Click += Matemagica;
        }
        public void Matemagica(object sender, EventArgs e)
        {
            //Int32 nu = FindViewById<EditText>(Resource.Id.digitar).Text;
            string aa = FindViewById<EditText>(Resource.Id.Digitar).Text;
            //aa += ".00";
            if (aa != "")
            {
                double numero = Convert.ToDouble(aa);
                double calculo = Math.Sqrt((numero*15));
                aa = Convert.ToString(calculo);
                int virg = aa.IndexOf(",") + 1;
                string result = "";
                for (int i = virg; i < virg + 6 && i < aa.Length; ++i)
                {
                    result += aa[i];
                }
                FindViewById<TextView>(Resource.Id.Resultado).Text = result;
            }
        }

    }
}