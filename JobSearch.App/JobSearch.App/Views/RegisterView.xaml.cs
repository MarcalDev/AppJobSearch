using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Load;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
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
    public partial class RegisterView : ContentPage
    {
        private UserService _service;
        public RegisterView()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveAction(object sender, EventArgs e)
        {
            TxtMessages.Text = String.Empty;

            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            User user = new User() { Name = name, Email = email, Password = password };

            await Navigation.PushPopupAsync(new Load());

            // Faz uma require (Método AddUser) com os parametros informados
            ResponseService<User> responseService = await _service.AddUser(user);

            if (responseService.IsSucess)
            {
                // Serialização de objeto com informações de usuário e retorna através de Json
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));

                // Salvar propriedades alteradas
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new StartView());
            }
            else
            {
                // Erros de cadastro
                if (responseService.StatusCode == 400)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach(var dicKey in responseService.Errors)
                    {
                        foreach(var message in dicKey.Value)
                        {
                            sb.AppendLine(message);
                        }
                    }
                    TxtMessages.Text = sb.ToString();
                }
                // Erro de servidor
                else
                {
                    await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "OK");
                }

            }
            await Navigation.PopAllPopupAsync();
        }
    }
}