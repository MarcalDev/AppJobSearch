using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using JobSearch.App.Utility.Load;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private UserService _service;
        public LoginView()
        {
            InitializeComponent();

            _service = new UserService();
        }

        private void GoRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterView());
        }

        private async void GoStart(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;

            await Navigation.PushPopupAsync(new Load());

            // Faz uma require (Método GetUser) com os parametros informados
            ResponseService<User> responseService = await _service.GetUser(email, password);

            if(responseService.IsSucess)
            {
                // Serialização de objeto com informações de usuário e retorna através de Json
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));

                // Salvar propriedades alteradas
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new StartView());
            }
            else
            {               
                if(responseService.StatusCode == 404)
                {
                    await DisplayAlert("Erro!", "Nenhum usuários encontrado", "OK");

                }
                else
                {
                    await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "OK");
                }

            }
            await Navigation.PopAllPopupAsync();
        }
    }
}