using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartView : ContentPage
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void GoVisualizer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisualizerView());
        }

        private void GoRegisterJobView(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterJobView());
        }

        private void FocusWord(object sender, EventArgs e)
        {
            TxtWord.Focus();
        }

        private void FocusCityState(object sender, EventArgs e)
        {
            TxtCityState.Focus();
        }

        private void Logout(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("User");
            App.Current.SavePropertiesAsync();
            App.Current.MainPage = new LoginView();
        }
    }
}