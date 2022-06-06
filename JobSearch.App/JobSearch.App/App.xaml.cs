using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //App.Current.Properties.Remove("User"); - remove usuario
            if (App.Current.Properties.ContainsKey("User"))
            {
                MainPage = new NavigationPage(new Views.StartView());
            }
            else
            {
                MainPage = new NavigationPage(new Views.LoginView());
            }

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
